using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUS2_MichalMurin_HashFile.DataStructures.Trie
{
    internal class Trie
    {
        internal int BlockFactor { get; set; }
        /// <summary>
        /// Koren stromu
        /// </summary>
        internal TrieNode? Root { get; private set; }

        /// <summary>
        /// Bezparametricky konstruktor
        /// </summary>
        public Trie(int blockFactor)
        {
            Root = null;
            BlockFactor = blockFactor;
        }
        /// <summary>
        /// Parametricky konstruktor, pri vytvarani stromu vlozi koren do stromu
        /// </summary>
        /// <param name="root">koren stromu</param>
        public Trie(TrieNode? root, int blockFactor)
        {
            Root = root;
            BlockFactor = blockFactor;
        }

        /// <summary>
        /// Pomocna metoda na najdenie dat v BVS
        /// </summary>
        /// <param name="pData">Hladane data</param>
        /// <returns>(True, hladany vrchol) - ak je hladanie uspesne, inak (False, Vrchol kde hladanie skoncilo)</returns>
        private (bool, ExternNode?) FindExternNode(BitArray pData)
        {
            TrieNode? resultNode = Root;
            if (resultNode == null)
            {
                return (false, null);
            }
            else
            {
                for (int i = 0; i < pData.Count; i++)
                {
                    if (resultNode!.GetType() == typeof(ExternNode))
                        return (true, (ExternNode)resultNode);
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
                return (false, null);
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
        /// <summary>
        /// Metoda na vlozenie prvkov do BVS
        /// </summary>
        /// <param name="pData">Vkladane data</param>
        /// <returns>True, ak bolo vkladanie uspesne, inak False - ak uz data v strome su</returns>
        public bool Insert(BitArray pData)
        {
            var result = FindExternNode(pData);
            bool found = result.Item1;
            ExternNode? externNode = result.Item2;
            if (!found)
            {
                if(externNode.RecordsCount < BlockFactor)
                {
                    // do tohto bloku sa prida novy record
                    externNode.RecordsCount++;
                    return true;
                }
                else
                {
                    // vytvorit novy externnode a prehashovat rekordy -> to ale bude treba robit v dynamickom hashingu a nie sem
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Metoda na vymazanie dat zo stromu
        /// </summary>
        /// <param name="pData">data, ktore chcme vymazat</param>
        /// <returns>True, ak sa vymazanie podarilo, inak False</returns>
        /// <exception cref="Exception">Vyhodena vynimka, ak sa data v strome nenachadzaju</exception>
        public bool Delete(BitArray pData)
        {
            var result = FindBSTNode(pData);
            bool found = result.Item1;
            TrieNode? nodeToDelete = result.Item2;
            if (found)
            {
                // DELETE
                BSTNode<T>? parent = nodeToDelete.Parent;
                BSTNode<T>? replacingNode = null;
                int sonsNum = 0;
                if (nodeToDelete.HasLeftSon())
                    sonsNum++;
                if (nodeToDelete.HasRightSon())
                    sonsNum++;
                switch (sonsNum)
                {
                    case 1:
                        // ak má lavého syna, nastaví sa na nullptr, inak sa nastaví pravý na nullptr
                        if (nodeToDelete.HasLeftSon())
                        {
                            replacingNode = nodeToDelete.LeftSon;
                            nodeToDelete.LeftSon = null;
                        }
                        else
                        {
                            replacingNode = nodeToDelete.RightSon;
                            nodeToDelete.RightSon = null;
                        }
                        break;
                    case 2:
                        replacingNode = nodeToDelete.LeftSon;
                        // najdeme najpravejsieho predchodcu
                        while (replacingNode.HasRightSon())
                        {
                            replacingNode = replacingNode.RightSon;
                        }
                        // ak sa vnorime pri hladani najpravejsieho predka
                        if (replacingNode != nodeToDelete.LeftSon)
                        {
                            if (replacingNode.HasLeftSon())
                            {
                                replacingNode.Parent.RightSon = replacingNode.LeftSon;
                                replacingNode.LeftSon.Parent = replacingNode.Parent;
                            }
                            else
                            {
                                replacingNode.Parent.RightSon = null;
                            }
                            replacingNode.LeftSon = nodeToDelete.LeftSon;
                            nodeToDelete.LeftSon.Parent = replacingNode;
                        }
                        replacingNode.RightSon = nodeToDelete.RightSon;
                        nodeToDelete.RightSon.Parent = replacingNode;
                        break;
                    default:
                        break;
                }
                // ak mazeme Root
                if (parent == null)
                {
                    Root = replacingNode;
                }
                else
                {
                    if (nodeToDelete.IsLeftSon())
                    {
                        parent.LeftSon = replacingNode;
                    }
                    else
                    {
                        parent.RightSon = replacingNode;
                    }
                }
                // ak nevymazavame list - nastavime noveho rodica novemu node-ovi
                if (replacingNode != null)
                {
                    replacingNode.Parent = parent;
                }
                nodeToDelete.Parent = null;
                nodeToDelete.LeftSon = null;
                nodeToDelete.RightSon = null;
                Size--;
                return true;
            }
            else
            {
                throw new Exception("No such data in BST, can not delete not existing data!");
            }
        }
    }
}

