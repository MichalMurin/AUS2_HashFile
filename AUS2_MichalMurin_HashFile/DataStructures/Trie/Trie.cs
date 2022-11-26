using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUS2_MichalMurin_HashFile.DataStructures.Trie
{
    public class Trie
    {
        internal int BlockFactor { get; set; }
        /// <summary>
        /// Koren stromu
        /// </summary>

        public int BlockSize { get; set; }
        internal TrieNode? Root { get; private set; }

        /// <summary>
        /// Bezparametricky konstruktor
        /// </summary>
        public Trie(int pBlockFactor, int pBlockSize)
        {
            Root = new InternNode();
            ((InternNode)Root).LeftSon = new ExternNode(0, 0, Root);
            ((InternNode)Root).LeftSon = new ExternNode(pBlockSize*pBlockFactor, 0, Root);
            BlockFactor = pBlockFactor;
            BlockSize = pBlockSize;
        }

        /// <summary>
        /// Pomocna metoda na najdenie dat v BVS
        /// </summary>
        /// <param name="pData">Hladane data</param>
        /// <returns>(True, hladany vrchol, kolky bit sa pouziva) - ak je hladanie uspesne, inak (False, Vrchol kde hladanie skoncilo)</returns>
        public (bool, ExternNode?, int) FindExternNode(BitArray pData)
        {
            TrieNode? resultNode = Root;
            if (resultNode == null)
            {
                return (false, null, 0);
            }
            else
            {
                for (int i = 0; i < pData.Count; i++)
                {
                    if (resultNode!.GetType() == typeof(ExternNode))
                        return (true, (ExternNode)resultNode, i-1);
                    if (pData[i])
                    {
                        // ideme doprava
                        resultNode = ((InternNode)resultNode).RightSon;
                    }
                    else
                    {
                        // ideme dolava
                        resultNode = ((InternNode)resultNode).LeftSon;
                    }
                }
                return (false, null, 0);
            }
        }
        /// <summary>
        /// Metoda na na najdenie vrcholu stromu s hladanymi datami
        /// </summary>
        /// <param name="pData">Hladane data</param>
        /// <returns>hladane data zo stromu</returns>
        /// <exception cref="Exception">Vynimka ak sa data nenachadzaju v strome</exception>
        public ExternNode? Find(BitArray pData)
        {
            var result = FindExternNode(pData);
            if (result.Item1)
            {
                return result.Item2;
            }
            else
            {
                throw new Exception("No such data in Trie!");
            }
        }
    }
}

