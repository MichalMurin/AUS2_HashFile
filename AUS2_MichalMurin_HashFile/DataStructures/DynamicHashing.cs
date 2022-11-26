using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AUS2_MichalMurin_HashFile.DataStructures.Trie;

namespace AUS2_MichalMurin_HashFile.DataStructures
{
    public class DynamicHashing<T> : Hashing<T> where T : IData<T>
    {
        public AUS2_MichalMurin_HashFile.DataStructures.Trie.Trie trie { get; set; }
        public DynamicHashing(string pFileName, int pBlockFactor) : base(pFileName, pBlockFactor)
        {            
            trie = new Trie.Trie(pBlockFactor, new Block<T>(pBlockFactor).GetSize());
        }

        protected override (Block<T>?,long) GetOffset(BitArray hash, int blockSize, OperationType opType)
        {
            var result = trie.FindExternNode(hash);
            var exNode = result.Item2;
            if(exNode != null)
            {
                if (opType == OperationType.Find) return (null, exNode.Offset);
                else if (opType == OperationType.Insert && exNode.RecordsCount < BlockFactor) return (null, exNode.Offset);
                else if (opType == OperationType.Insert && exNode.RecordsCount >= BlockFactor)
                {
                    var blockInfo = AddBlock(exNode, blockSize, result.Item3, hash);
                    return blockInfo;
                }
            }

        }

        private (Block<T>?, long) AddBlock(ExternNode exNode, int blockSize, int currentBit, BitArray newHash)
        {
            var offset = exNode.Offset;
            byte[] blockBytes = new byte[blockSize];
            try
            {
                File.Seek(offset, SeekOrigin.Begin);
                File.Read(blockBytes);
            }
            catch (IOException e)
            {
                throw new IOException($"Exception found during reading the file: {e.Message}");
            }
            var FullBlock = new Block<T>(BlockFactor);
            FullBlock.FromByteArray(blockBytes);
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
                if(leftNode.RecordsCount > BlockFactor)
                {
                    inNode = new InternNode(leftNode.Parent);
                    ((InternNode)leftNode.Parent!).LeftSon = inNode;

                }
                else
                {
                    inNode = new InternNode(rightNode.Parent);
                    ((InternNode)leftNode.Parent!).RightSon = inNode;
                }
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
            if (newDataRight)
            {
                // lavy blok zapiseme naspat do suboru a pravy blok vratime aby sa do neho insertli nove data
                File.Seek(exNode.Offset, SeekOrigin.Begin);
                File.Write(NewLeftBlock.ToByteArray());
                // RecordsCount by uz ma byt spravne nastaveny z cyklu
                leftNode.Offset = exNode.Offset;
                // TODO Nastavit adresu podla volneho bloku v manazmente volnych blokov
                rightNode.Offset = File.Length;
                return (NewRightBlock, rightNode.Offset);
            }
            else
            {
                // lavy blok zapiseme naspat do suboru a pravy blok vratime aby sa do neho insertli nove data
                File.Seek(exNode.Offset, SeekOrigin.Begin);
                File.Write(NewRightBlock.ToByteArray());
                // RecordsCount by uz ma byt spravne nastaveny z cyklu
                rightNode.Offset = exNode.Offset;
                // TODO Nastavit adresu podla volneho bloku v manazmente volnych blokov
                leftNode.Offset = File.Length;
                return (NewLeftBlock, leftNode.Offset);
            }
        }

    }
}
