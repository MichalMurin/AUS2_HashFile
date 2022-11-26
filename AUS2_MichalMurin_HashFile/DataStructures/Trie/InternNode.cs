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

        public InternNode(TrieNode? parent = null) : base(parent)
        {
            LeftSon = null;
            RightSon = null;
        }
    }
}
