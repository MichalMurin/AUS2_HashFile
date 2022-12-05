using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using AUS2_MichalMurin_HashFile.DataStructures.Trie;
using AUS2_MichalMurin_HashFile.Service;

namespace AUS2_MichalMurin_HashFile.DataStructures
{
    /// <summary>
    /// Trieda pre spravu dynamickeho hesovacieho suboru
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class DynamicHashing<T> : Hashing<T> where T : IData<T>
    {
        /// <summary>
        /// Znakovy strom
        /// </summary>
        internal Trie.Trie trie { get; set; }
        /// <summary>
        /// Zoznam prazdnych adries
        /// </summary>
        internal List<long> EmptyBlocksOffsetes { get; set; }
        /// <summary>
        /// cesta na ulozenie znakoveho stromu
        /// </summary>
        internal static string _pathForTrieData { get; } = "Trie.csv";
        /// <summary>
        /// Cesta na ulozenie dat prazdnych blokov
        /// </summary>
        internal static string _pathForEmptyBlocksData { get; } = "EmptyBlocks.csv";
        /// <summary>
        /// Konstruktor triedy
        /// </summary>
        /// <param name="pFileName">cesta k binarnemu suboru</param>
        /// <param name="pBlockFactor">blokovaci faktor</param>
        internal DynamicHashing(string pFileName, int pBlockFactor) : base(pFileName, pBlockFactor)
        {            
            trie = new Trie.Trie();
            EmptyBlocksOffsetes = new List<long>();
        }

        /// <summary>
        /// Konstruktor triedy, sluziaci pri nacitani aplikacnych dat pri spusteni aplikacie
        /// </summary>
        /// <param name="trie">znakovy strom</param>
        /// <param name="emptyBlocksOffsetes">zoznam prazdnych adries</param>
        /// <param name="pFileName">cesta k binarnemu suboru</param>
        /// <param name="pBlockFactor">blokovaci faktor</param>
        internal DynamicHashing(Trie.Trie trie, List<long> emptyBlocksOffsetes, string pFileName, int pBlockFactor) : base(pFileName, pBlockFactor)
        {
            this.trie = trie;
            EmptyBlocksOffsetes = emptyBlocksOffsetes;
        }

        /// <summary>
        /// Metoda vracajuca adresu dat v subore
        /// </summary>
        /// <param name="hash">hesh hladanych dat</param>
        /// <returns>adresu dat</returns>
        /// <exception cref="ArgumentException"></exception>
        protected override long GetOffset(BitArray hash)
        {
            var result = trie.FindExternNode(hash);
            if (result.Item1)
            {
                return result.Item2!.Offset;
            }
            else
                throw new ArgumentException("Hash sa v trie nenasiel!");

        }

        /// <summary>
        /// Metoda na pridanie dat do struktury
        /// </summary>
        /// <param name="data">pridavane data</param>
        /// <returns>true ak sa vlozenie podarilo</returns>
        internal override bool Insert(T data)
        {
            var hash = data.GetHash();
            var result = trie.FindExternNode(hash);
            if (result.Item1)
            {
                var exNode = result.Item2;
                if (exNode!.Offset == -1)
                {
                    AsignOffsetToNode(exNode);
                }
                if (exNode!.RecordsCount >= BlockFactor)
                {
                    // Blok uz je plny, musime prehashovat data ktore v nom su a vytvorit novy blok
                    return TryRehashAndInsertData(hash, data, exNode, result.Item3);
                }
                else
                {
                    // V bloku je este miesto, data tam jednoducho zapiseme:
                    var foundBlcokData = FindBlock(data);
                    var block = foundBlcokData.Item1;
                    var offset = foundBlcokData.Item2;
                    bool success = block!.InsertRecord(data);
                    if (success)
                    {
                        TryWriteBlockToFile(offset, block);
                        exNode.RecordsCount++;
                    }
                    return success;
                }
            }
            return false;

        }
        /// <summary>
        /// Metoda na vymazanie prvku zo struktury
        /// </summary>
        /// <param name="data">mazane data</param>
        /// <returns>true ak bolo mazanie uspesne inak false</returns>
        internal override bool Delete(T data)
        {
            var hash = data.GetHash();
            var result = trie.FindExternNode(hash);
            if (result.Item1)
            {
                var exNode = result.Item2;
                var block = TryReadBlockFromFile(exNode!.Offset);
                bool successfulyDeleted = block.RemoveRecord(data);
                if (successfulyDeleted)
                {
                    exNode.RecordsCount--;
                    // ak sme syn roota, nebudeme mergovat bloky
                    if (exNode.Parent == trie.Root)
                    {
                        if (exNode.RecordsCount == 0)
                        {
                            HandleEmptyBlocks(block, exNode);
                            return true;
                        }
                        else
                        {
                            TryWriteBlockToFile(exNode.Offset, block);
                            return true;
                        }
                    }
                    else
                    {
                        ExternNode? exNodesbrother;
                        while (true)
                        {
                            exNodesbrother = exNode!.GetBrother();
                            if (exNodesbrother != null && exNodesbrother.Offset != -1 && exNodesbrother.RecordsCount + exNode.RecordsCount <= BlockFactor && exNode.Parent != trie.Root)
                            {
                                // mozeme sa zlucit s blokom vedla
                                var blockToMerge = TryReadBlockFromFile(exNodesbrother.Offset);
                                (exNode,block) = MergeBlocks(exNode, block, exNodesbrother, blockToMerge);
                            }
                            else
                            {
                                if (exNode.RecordsCount == 0)
                                {
                                    HandleEmptyBlocks(block, exNode);
                                }
                                else
                                    TryWriteBlockToFile(exNode.Offset, block);
                                return true;
                            }
                        }                    
                    }                   
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Metoda ktora zluci dva bloky a ich vrcholy
        /// </summary>
        /// <param name="firstNode">prvy vrchol </param>
        /// <param name="firstBlock">prvy blok</param>
        /// <param name="secondNode">druhy vrchol</param>
        /// <param name="secondBlock">druhy blok</param>
        /// <returns></returns>
        private (ExternNode, Block<T>) MergeBlocks(ExternNode firstNode, Block<T> firstBlock, ExternNode secondNode, Block<T> secondBlock)
        {
            ExternNode toBeEmptyNode, toBeFullNode;
            Block<T> toBeEmptyBlock, toBeFullBlock;

            if(firstNode.Offset > secondNode.Offset)
            {
                toBeEmptyBlock = firstBlock;
                toBeEmptyNode = firstNode;
                toBeFullBlock = secondBlock;
                toBeFullNode = secondNode;
            }
            else
            {
                toBeEmptyBlock = secondBlock;
                toBeEmptyNode = secondNode;
                toBeFullBlock = firstBlock;
                toBeFullNode = firstNode;
            }

            int tmpValidCount = toBeEmptyBlock.ValidCount;
            for (int i = 0; i < tmpValidCount; i++)
            {
                toBeFullBlock.InsertRecord(toBeEmptyBlock.Records[0]);
                toBeEmptyBlock.RemoveRecord(toBeEmptyBlock.Records[0]);
            }

            ExternNode newExternNode = new ExternNode(toBeFullNode.Offset, toBeFullBlock.ValidCount, toBeFullNode.Parent!.Parent);
            toBeFullNode.Parent.Parent!.ReplaceSon(toBeFullNode.Parent, newExternNode);
            // ak je blok na konci suboru, zmensim subor.. nemalo by sa stat ze by som odstranil inicializcne dva bloky,
            // pretoze vtedy by som sa nemal dostat ani na vykonanie tejto metody
            toBeEmptyNode.RecordsCount = toBeEmptyBlock.ValidCount;
            HandleEmptyBlocks(toBeEmptyBlock, toBeEmptyNode);
            return (newExternNode, toBeFullBlock);


        }

        /// <summary>
        /// Metoda na spravu volnych blokov
        /// </summary>
        /// <param name="emptyBlock">prazdny blok</param>
        /// <param name="exNode">externy vrchol prazdneho bloku</param>
        private void HandleEmptyBlocks(Block<T> emptyBlock, ExternNode exNode)
        {
            var blockSize = emptyBlock.GetSize();
            var fileLength = HashFile.Length;
            if (fileLength - blockSize == exNode.Offset)
            {
                // zmensim velkost suboru
                fileLength -= blockSize;
                // kontrolujem ci neexistuje prazdny blok ktory pred prvotnym zmensenim nebol na konci suboru
                while (EmptyBlocksOffsetes.Contains(fileLength - blockSize))
                {
                    fileLength -= blockSize;
                    EmptyBlocksOffsetes.Remove(fileLength - blockSize);
                }
                // prazdne bloky by nemali byt v strome trie, takze ich nemusime odtial odstranovat
                HashFile.SetLength(fileLength);
                // ked som zmensil subor, nepotrebujem do neho uz zapisovat prazdny blok
            }
            else
            {
                // zapisem prazdnu adresu do zoznamu prazdnych blokov
                EmptyBlocksOffsetes.Add(exNode.Offset);
                // prazdny blok zapiseme naspat do suboru, uz s valid count 0
                TryWriteBlockToFile(exNode.Offset, emptyBlock);
            }
            exNode.Offset = -1;
        }

        /// <summary>
        /// Metoda ktora priradi vrcholu novu volnu adresu
        /// </summary>
        /// <param name="node">externy vrchol ziadajuci o adresu</param>
        private void AsignOffsetToNode(ExternNode node)
        {
            long adressForNewBlock = 0;
            if (EmptyBlocksOffsetes.Count > 0)
            {
                // ak mame volnu adresu, berieme adresu zo zoznamu volnych adries
                adressForNewBlock = EmptyBlocksOffsetes[EmptyBlocksOffsetes.Count - 1];
                EmptyBlocksOffsetes.RemoveAt(EmptyBlocksOffsetes.Count - 1);
            }
            else
            {
                // ak nemame volnu adresu, zvacsujeme subor
                adressForNewBlock = HashFile.Length;
                HashFile.SetLength(HashFile.Length + BlockSize);
            }
            node.Offset = adressForNewBlock;
        }
        /// <summary>
        /// Metoda, ktora rehesuje data ak je sa vkladane data uz nezmestia do bloku
        /// </summary>
        /// <param name="newHash">hash vkladanych dat</param>
        /// <param name="data">vkladane data</param>
        /// <param name="exNodeToSplit">externy vrcho ktory sa ide delit</param>
        /// <param name="currentBit">aktualny bit v strome</param>
        /// <returns>true ak sa rehesovanie podarilo inak false</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        private bool TryRehashAndInsertData(BitArray newHash, T data, ExternNode exNodeToSplit, int currentBit)
        {
            var FullBlock = TryReadBlockFromFile(exNodeToSplit.Offset);
            long freeOffset = exNodeToSplit.Offset;
            InternNode inNode;
            ExternNode? leftNode = null;
            ExternNode? rightNode = null;
            BitArray hash;
            bool newDataRight = false;
            // umelo navysime recordsCount .. ako keby sa tam pridal uz prvok ktroy sa tam nezmesti, aby nam aspon raz prebehol cyklus
            exNodeToSplit.RecordsCount++;
            Direction newInternNodeDirection;
            
            while (exNodeToSplit.RecordsCount > BlockFactor)
            {
                currentBit++;
                if (exNodeToSplit.IsLeftSon())
                {
                    newInternNodeDirection = Direction.Left;
                }
                else
                {
                    newInternNodeDirection = Direction.Right;
                }
                rightNode = new ExternNode(-1,0);
                leftNode = new ExternNode(-1, 0);
                if (newInternNodeDirection == Direction.Left)
                {
                    inNode = new InternNode(exNodeToSplit.Parent);
                    ((InternNode)exNodeToSplit.Parent!).LeftSon = inNode;                    
                }
                else
                {
                    inNode = new InternNode(exNodeToSplit.Parent);
                    ((InternNode)exNodeToSplit.Parent!).RightSon = inNode;
                }
                inNode.LeftSon = leftNode;
                inNode.RightSon = rightNode;
                leftNode.Parent = inNode;
                rightNode.Parent = inNode;

                leftNode.RecordsCount = 0;
                rightNode.RecordsCount = 0;

                foreach (var rec in FullBlock.Records)
                {
                    hash = rec.GetHash();
                    if (currentBit < hash.Count)
                    {
                        if (hash[currentBit])
                            rightNode.RecordsCount++;
                        else
                            leftNode.RecordsCount++;
                    }
                    else
                    {
                        throw new IndexOutOfRangeException("Nepoadarilo sa prehesovat blok, prvok sa nepodarilo vlozit do Dynamickeho hasovacieho suboru!");
                    }
                }
                // pridame este hash noveho vkladaneho prvku
                if (currentBit < newHash.Count)
                {
                    if (newHash[currentBit])
                    {
                        newDataRight = true;
                        rightNode.RecordsCount++;
                    }
                    else
                    {
                        newDataRight = false;
                        leftNode.RecordsCount++;
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException("Nepoadarilo sa prehesovat blok, prvok sa nepodarilo vlozit do Dynamickeho hasovacieho suboru!");
                }
                if (rightNode.RecordsCount > BlockFactor)
                    exNodeToSplit = rightNode;
                else if (leftNode.RecordsCount > BlockFactor)
                    exNodeToSplit = leftNode;
                else
                    exNodeToSplit.RecordsCount = 0;
            }

            var NewRightBlock = new Block<T>(BlockFactor);
            var NewLeftBlock = new Block<T>(BlockFactor);
            foreach (var rec in FullBlock.Records)
            {
                hash = rec.GetHash();
                if (currentBit < hash.Count)
                {
                    if (hash[currentBit])
                        NewRightBlock.InsertRecord(rec);
                    else
                        NewLeftBlock.InsertRecord(rec);
                }
                else
                {
                    throw new IndexOutOfRangeException("Nepoadarilo sa prehesovat blok, prvok sa nepodarilo vlozit do Dynamickeho hasovacieho suboru!");
                }
            }
            if (newDataRight)
            {
                // lavy blok zapiseme naspat do suboru a pravy blok vratime aby sa do neho insertli nove data
                // RecordsCount by uz ma byt spravne nastaveny z cyklu
                leftNode!.Offset = freeOffset;
                TryWriteBlockToFile(leftNode.Offset, NewLeftBlock);
                // Nastavit adresu podla volneho bloku v manazmente volnych blokov
                AsignOffsetToNode(rightNode!);
                NewRightBlock.InsertRecord(data);
                TryWriteBlockToFile(rightNode!.Offset, NewRightBlock);
                return true;
            }
            else
            {
                // lavy blok zapiseme naspat do suboru a pravy blok vratime aby sa do neho insertli nove data
                // RecordsCount by uz ma byt spravne nastaveny z cyklu
                rightNode!.Offset = freeOffset;
                TryWriteBlockToFile(rightNode.Offset, NewRightBlock);
                // Nastavit adresu podla volneho bloku v manazmente volnych blokov
                AsignOffsetToNode(leftNode!);
                NewLeftBlock.InsertRecord(data);
                TryWriteBlockToFile(leftNode!.Offset, NewLeftBlock);
                return true;
            }
        }

        /// <summary>
        /// Metoda na export aplikacnych dat do csv suboru
        /// </summary>
        internal override void ExportAppDataToFile()
        {
            trie.SaveToFile(_pathForTrieData);
            var offsetsString = new List<string>();
            foreach (var item in EmptyBlocksOffsetes)
            {
                offsetsString.Add(item.ToString());
            }
            File.WriteAllLines(_pathForEmptyBlocksData, offsetsString);
        }

        /// <summary>
        /// Metoda na nacitanie aplikacnych dat dynamickeho suboru
        /// </summary>
        /// <returns>Znakovy strom trie a zoznam volnych adries</returns>
        internal static (Trie.Trie, List<long>) LoadDynamicDataFromFile()
        {
            var itemsForTrie = Trie.Trie.GetLeafesFromFile(_pathForTrieData);
            var trie = new Trie.Trie(itemsForTrie);
            var linesOfEmptyBlocks = File.ReadLines(_pathForEmptyBlocksData);
            List<long> emptyOffsets = new List<long>();
            long tmp;
            foreach (var line in linesOfEmptyBlocks)
            {
                long.TryParse(line, out tmp);
                emptyOffsets.Add(tmp);
            }
            return (trie, emptyOffsets);
        }
    }
}
