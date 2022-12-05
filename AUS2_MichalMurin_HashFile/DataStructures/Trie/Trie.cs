using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AUS2_MichalMurin_HashFile.DataStructures.Trie
{
    internal class Trie
    {
        internal InternNode? Root { get; private set; }

        internal Trie()
        {
            Root = new InternNode();
            Root.LeftSon = new ExternNode(-1, 0, Root);
            Root.RightSon = new ExternNode(-1, 0, Root);
        }
        internal Trie(List<(ExternNode, BitArray)> listOfExternNodes)
        {
            if (listOfExternNodes.Count < 2)
            {
                throw new ArgumentException("Too low number of extern nodes, please use another constructor!");
            }

            Root = new InternNode();
            foreach (var item in listOfExternNodes)
            {
                InternNode current = Root;
                ExternNode nodeToInsert = item.Item1;
                BitArray bitPath = item.Item2;
                for (int i = 0; i < bitPath.Count; i++)
                {
                    // ideme doprava
                    if (bitPath[i])
                    {
                        // uz vkladame externNode
                        if (i == bitPath.Count - 1)
                        {
                            current.insertRightSon(nodeToInsert);
                        }
                        // vkladame este interny node
                        else
                        {
                            if (!current.HasRightSon())
                            {
                                current.RightSon = new InternNode(current);
                            }
                            current = ((InternNode)current.RightSon!);
                        }
                    }
                    else
                    {
                        // uz vkladame externNode
                        if (i == bitPath.Count - 1)
                        {
                            current.insertLeftSon(nodeToInsert);
                        }
                        // vkladame este interny node
                        else
                        {
                            if (!current.HasLeftSon())
                            {
                                current.LeftSon = new InternNode(current);
                            }
                            current = ((InternNode)current.LeftSon!);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Pomocna metoda na najdenie externeho vrcholu v trie
        /// </summary>
        /// <param name="pData">Hladane data</param>
        /// <returns>(True, hladany vrchol, kolky bit sa pouziva, pouzity bit) - ak je hladanie uspesne</returns>
        internal (bool, ExternNode?, int) FindExternNode(BitArray pData)
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

        /// <summary>
        /// Metoda na najdenie vsetkych listov stromu
        /// </summary>
        /// <returns>Zoznam listov stromu</returns>
        private List<(ExternNode, BitArray)> GetAllLeafs()
        {
            List<(ExternNode, BitArray)> resultList = new List<(ExternNode, BitArray)>();
            if (Root == null)
            {
                return resultList;
            }
            Stack<(TrieNode, BitArray)> helperStack = new Stack<(TrieNode, BitArray)>();
            helperStack.Push((Root,new BitArray(0)));

            while (helperStack.Count != 0)
            {
                var item = helperStack.Pop();
                TrieNode node = item.Item1;
                BitArray nodesBits = item.Item2;
                if (node.GetType() == typeof(InternNode) && ((InternNode)node).HasRightSon())
                {
                    var newBits = new BitArray(nodesBits);
                    newBits.Length++;
                    newBits.Set(newBits.Length - 1, true);
                    helperStack.Push((((InternNode)node).RightSon!, newBits));
                }
                if (node.GetType() == typeof(InternNode) && ((InternNode)node).HasLeftSon())
                {
                    var newBits = new BitArray(nodesBits);
                    newBits.Length++;
                    newBits.Set(newBits.Length - 1, false);
                    helperStack.Push((((InternNode)node).LeftSon!,newBits));
                }
                if (node.GetType() == typeof(ExternNode))
                    resultList.Add((((ExternNode)node), nodesBits));
            }
            return resultList;
        }

        internal void SaveToFile(string path)
        {
            List<string> content = new List<string>();
            var leafes = GetAllLeafs();
            foreach (var item in leafes)
            {
                string bitPath = "";
                for (int i = 0; i < item.Item2.Count; i++)
                {
                    // R = rigth direction
                    if (item.Item2[i])
                        bitPath += "R";
                    // L = left direction
                    else
                        bitPath += "L";
                }
                content.Add($"{item.Item1.Offset};{item.Item1.RecordsCount};{bitPath}");
            }
            File.WriteAllLines(path, content);
        }

        internal static List<(ExternNode, BitArray)> GetLeafesFromFile(string path)
        {
            List<(ExternNode, BitArray)> resultList = new List<(ExternNode, BitArray)>();
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var parts = line.Split(";");
                long offset;
                int recCount;
                long.TryParse(parts[0], out offset);
                int.TryParse(parts[1], out recCount);
                BitArray bits = new BitArray(parts[2].Length);
                for (int i = 0; i < parts[2].Length; i++)
                {
                    if (parts[2][i] == 'R')
                        bits.Set(i, true);
                    else
                        bits.Set(i, false);
                }
                resultList.Add((new ExternNode(offset, recCount), bits));
            }
            return resultList;
        }
    }
}

