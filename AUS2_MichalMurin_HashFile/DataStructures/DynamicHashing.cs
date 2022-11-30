using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using AUS2_MichalMurin_HashFile.DataStructures.Trie;

namespace AUS2_MichalMurin_HashFile.DataStructures
{
    public class DynamicHashing<T> : Hashing<T> where T : IData<T>
    {
        public Trie.Trie trie { get; set; }
        public List<long> EmptyBlocksOffsetes { get; set; }
        public DynamicHashing(string pFileName, int pBlockFactor) : base(pFileName, pBlockFactor)
        {            
            trie = new Trie.Trie(pBlockFactor, new Block<T>(pBlockFactor).GetSize());
            EmptyBlocksOffsetes = new List<long>();
        }


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

        public override bool Insert(T data)
        {
            var hash = data.GetHash();
            var result = trie.FindExternNode(hash);
            if (result.Item1)
            {
                var exNode = result.Item2;
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
                    bool success = block.InsertRecord(data);
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
        public override bool Delete(T data)
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
                    var exNodesbrother = exNode!.GetBrother();
                    if (exNodesbrother != null && exNodesbrother.RecordsCount + exNode.RecordsCount <= BlockFactor)
                    {
                        if (exNode.Parent == trie.Root && exNodesbrother.Parent == trie.Root)
                        {
                            // nebudeme bloky mergovat, nakolko uz su to posledne dva externe vrcholy v strome
                            TryWriteBlockToFile(exNode.Offset, block);
                            return true;
                        }
                        else
                        {
                            // mozeme sa zlucit s blokom vedla
                            var blockToMerge = TryReadBlockFromFile(exNodesbrother.Offset);
                            return MergeBlocks(exNode, block, exNodesbrother, blockToMerge);
                        }
                    }
                    else
                    {
                        TryWriteBlockToFile(exNode.Offset, block);
                        return true;
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

        private bool MergeBlocks(ExternNode firstNode, Block<T> firstBlock, ExternNode secondNode, Block<T> secondBlock)
        {
            // prepisovat bloky z toho kde ich je menej tam kde ich je viac
            for (int i = 0; i < secondBlock.ValidCount; i++)
            {
                firstBlock.InsertRecord(secondBlock.Records[0]);
                secondBlock.RemoveRecord(secondBlock.Records[0]);
            }

            ExternNode newExternNode = new ExternNode(firstNode.Offset, firstBlock.ValidCount, firstNode.Parent!.Parent);
            firstNode.Parent.Parent!.ReplaceSon(firstNode.Parent, newExternNode);
            // ak je blok na konci suboru, zmensim subor.. nemalo by sa stat ze by som odstranil inicializcne dva bloky,
            // pretoze vtedy by som sa nemal dostat ani na vykonanie tejto metody
            HandleEmptyBlocks(secondBlock, secondNode.Offset);
            TryWriteBlockToFile(newExternNode.Offset, firstBlock);
            return true;


        }

        private void HandleEmptyBlocks(Block<T> emptyBlock, long blockOffset)
        {
            var blockSize = emptyBlock.GetSize();
            var fileLength = File.Length;
            if (fileLength - blockSize == blockOffset)
            {
                // zmensim velkost suboru
                fileLength -= blockSize;
                // kontrolujem ci neexistuje prazdny blok ktory pred prvotnym zmensenim nebol na konci suboru
                // dalo by sa to robit efektivnejsie ak by boli adresy zoradene (pouzit BVS??)
                while (EmptyBlocksOffsetes.Contains(fileLength - blockSize))
                {
                    fileLength -= blockSize;
                    EmptyBlocksOffsetes.Remove(fileLength - blockSize);

                }
                // prazdne bloky by nemali byt v strome trie, takze ich nemusime odtial odstranovat
                File.SetLength(fileLength);
                // ked som zmensil subor, nepotrebujem do neho uz zapisovat prazdny blok
            }
            else
            {
                // zapisem prazdnu adresu do zoznamu prazdnych blokov
                EmptyBlocksOffsetes.Add(blockOffset);
                // prazdny blok zapiseme naspat do suboru, uz s valid count 0
                TryWriteBlockToFile(blockOffset, emptyBlock);
            }


        }
        private bool TryRehashAndInsertData(BitArray newHash, T data, ExternNode exNode, int currentBit)
        {
            var FullBlock = TryReadBlockFromFile(exNode.Offset);
            InternNode inNode;
            ExternNode leftNode;
            ExternNode rightNode;
            BitArray hash;
            bool newDataRight = false;
            // umelo navysime recordsCount .. ako keby sa tam pridal uz prvok ktroy sa tam nezmesti, aby nam aspon raz prebehol cyklus
            exNode.RecordsCount++;
            if (exNode.IsLeftSon())
            {
                leftNode = exNode;
                rightNode = new ExternNode(0, 0);
            }
            else
            {
                rightNode = exNode;
                leftNode = new ExternNode(0, 0);
            }
            while (leftNode.RecordsCount > BlockFactor || rightNode.RecordsCount > BlockFactor)
            {
                currentBit++;
                if (leftNode.RecordsCount > BlockFactor)
                {
                    inNode = new InternNode(leftNode.Parent);
                    ((InternNode)leftNode.Parent!).LeftSon = inNode;

                }
                else
                {
                    inNode = new InternNode(rightNode.Parent);
                    ((InternNode)rightNode.Parent!).RightSon = inNode;
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
            long adressForNewBlock;
            if (EmptyBlocksOffsetes.Count > 0)
            {
                // ak mame volnu adresu, berieme adresu zo zoznamu volnych adries
                adressForNewBlock = EmptyBlocksOffsetes[EmptyBlocksOffsetes.Count - 1];
                EmptyBlocksOffsetes.RemoveAt(EmptyBlocksOffsetes.Count - 1);
            }
            else
            {
                // ak nemame volnu adresu, zvacsujeme subor
                adressForNewBlock = File.Length;
                File.SetLength(File.Length + FullBlock.GetSize());
            }
            if (newDataRight)
            {
                // lavy blok zapiseme naspat do suboru a pravy blok vratime aby sa do neho insertli nove data
                // RecordsCount by uz ma byt spravne nastaveny z cyklu
                leftNode.Offset = exNode.Offset;
                TryWriteBlockToFile(leftNode.Offset, NewLeftBlock);
                // Nastavit adresu podla volneho bloku v manazmente volnych blokov
                rightNode.Offset = adressForNewBlock;
                NewRightBlock.InsertRecord(data);
                TryWriteBlockToFile(rightNode.Offset, NewRightBlock);
                return true;
            }
            else
            {
                // lavy blok zapiseme naspat do suboru a pravy blok vratime aby sa do neho insertli nove data
                // RecordsCount by uz ma byt spravne nastaveny z cyklu
                rightNode.Offset = exNode.Offset;
                TryWriteBlockToFile(rightNode.Offset, NewRightBlock);
                // Nastavit adresu podla volneho bloku v manazmente volnych blokov
                leftNode.Offset = adressForNewBlock;
                NewLeftBlock.InsertRecord(data);
                TryWriteBlockToFile(leftNode.Offset, NewLeftBlock);
                return true;
            }
        }
    }
}
