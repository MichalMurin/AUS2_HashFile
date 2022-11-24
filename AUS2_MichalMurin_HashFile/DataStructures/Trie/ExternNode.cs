using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUS2_MichalMurin_HashFile.DataStructures.Trie
{
    internal class ExternNode: TrieNode
    {
        public int RecordsCount { get; set; }
        public long Offset { get; set; }

        public ExternNode(long pOffset, int pRecordsCount, TrieNode? parent = null) : base(parent)
        {
            IsExternNode = true;
            Offset = pOffset;
            RecordsCount = pRecordsCount;
        }
    }
}
