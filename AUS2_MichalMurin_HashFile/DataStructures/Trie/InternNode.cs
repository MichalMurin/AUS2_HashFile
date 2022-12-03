using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUS2_MichalMurin_HashFile.DataStructures.Trie
{
    public class InternNode: TrieNode
    {
        /// <summary>
        /// Lavy syn vrcholu
        /// </summary>
        public TrieNode? LeftSon { get; set; }
        /// <summary>
        /// Pravy syn vrcholu
        /// </summary>
        public TrieNode? RightSon { get; set; }

        public InternNode(InternNode? parent = null) : base(parent)
        {
            LeftSon = null;
            RightSon = null;
        }

        public bool ReplaceSon(TrieNode oldSon, TrieNode newSon)
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

        public void insertLeftSon(TrieNode newLeftSon)
        {
            if (LeftSon != null)
            {
                LeftSon.Parent = null;
            }
            LeftSon = newLeftSon;
            newLeftSon.Parent = this;
        }
        public void insertRightSon(TrieNode newRightSon)
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
        public bool HasLeftSon()
        {
            return LeftSon != null;
        }
        /// <summary>
        /// Metoda na zistenie, ci ma vrchol praveho syna
        /// </summary>
        /// <returns>True - ak ma praveho syna, inak False</returns>
        public bool HasRightSon()
        {
            return RightSon != null;
        }
    }
}
