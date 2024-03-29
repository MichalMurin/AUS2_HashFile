﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUS2_MichalMurin_HashFile.DataStructures.Trie
{
    internal class TrieNode
    {
        /// <summary>
        /// Rodic vrcholu, pre Root je rodic = null
        /// </summary>
        internal InternNode? Parent { get; set; }
        //public bool IsExternNode { get; protected set; }

        /// <summary>
        /// Konstruktor, ktory vytvori vrchol a vlozi do neho data
        /// </summary>
        internal TrieNode(InternNode? parent = null)
        {
            Parent = parent;
        }



    }
}
