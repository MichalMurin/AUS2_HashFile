using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUS2_MichalMurin_HashFile.DataStructures.Trie
{
    internal class InternNode: TrieNode
    {
        /// <summary>
        /// Lavy syn vrcholu
        /// </summary>
        internal TrieNode? LeftSon { get; set; }
        /// <summary>
        /// Pravy syn vrcholu
        /// </summary>
        internal TrieNode? RightSon { get; set; }

        internal InternNode(InternNode? parent = null) : base(parent)
        {
            LeftSon = null;
            RightSon = null;
        }

        internal bool ReplaceSon(TrieNode oldSon, TrieNode newSon)
        {
            if(RightSon == oldSon)
            {
                RightSon = newSon;
                newSon.Parent = this;
                return true;
            }
            else if(LeftSon == oldSon)
            {
                LeftSon = newSon;
                newSon.Parent = this;
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void insertLeftSon(TrieNode newLeftSon)
        {
            if (LeftSon != null)
            {
                LeftSon.Parent = null;
            }
            LeftSon = newLeftSon;
            newLeftSon.Parent = this;
        }
        internal void insertRightSon(TrieNode newRightSon)
        {
            if (RightSon != null)
            {
                RightSon.Parent = null;
            }
            RightSon = newRightSon;
            newRightSon.Parent = this;
        }

        /// <summary>
        /// Metoda na zistenie, ci ma vrchol laveho syna
        /// </summary>
        /// <returns>True - ak ma laveho syna, inak False</returns>
        internal bool HasLeftSon()
        {
            return LeftSon != null;
        }
        /// <summary>
        /// Metoda na zistenie, ci ma vrchol praveho syna
        /// </summary>
        /// <returns>True - ak ma praveho syna, inak False</returns>
        internal bool HasRightSon()
        {
            return RightSon != null;
        }
    }
}
