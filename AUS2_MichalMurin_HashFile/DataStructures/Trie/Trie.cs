﻿using System;
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
        internal InternNode? Root { get; private set; }

        /// <summary>
        /// Bezparametricky konstruktor
        /// </summary>
        public Trie(int pBlockFactor, int pBlockSize)
        {
            Root = new InternNode();
            Root.LeftSon = new ExternNode(0, 0, Root);
            Root.RightSon = new ExternNode(pBlockSize*pBlockFactor, 0, Root);
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
                return (false, null, -1);
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
                return (false, null, -1);
            }
        }

        private IEnumerable<TrieNode> LevelOrder()
        {
            // Zacinam s rootom
            if (Root == null)
                yield break;
            Queue<TrieNode> helperQueue = new Queue<TrieNode>();
            helperQueue.Enqueue(Root);
            while (helperQueue.Count != 0)
            {
                TrieNode currentNode = helperQueue.Dequeue();
                yield return currentNode;
                // vlozime laveho syna
                if (currentNode.GetType() == typeof(InternNode) && ((InternNode)currentNode).HasLeftSon())
                {
                    helperQueue.Enqueue(((InternNode)currentNode!).LeftSon);
                }
                // vlozime praveho syna
                if (currentNode.GetType() == typeof(InternNode) && ((InternNode)currentNode).HasRightSon())
                {
                    helperQueue.Enqueue(((InternNode)currentNode).RightSon);
                }
            }
        }
    }
}

