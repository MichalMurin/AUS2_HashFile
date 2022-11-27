using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUS2_MichalMurin_HashFile.DataStructures.Trie
{
    public class ExternNode: TrieNode
    {
        public int RecordsCount { get; set; }
        public long Offset { get; set; }

        public ExternNode(long pOffset, int pRecordsCount, InternNode? parent = null) : base(parent)
        {
            Offset = pOffset;
            RecordsCount = pRecordsCount;
        }
        public bool IsLeftSon()
        {
            return ((InternNode)Parent!).LeftSon == this;
        }

        public ExternNode? GetBrother()
        {
            if(IsLeftSon() && this.Parent!.RightSon != null && this.Parent.RightSon.GetType() == typeof(ExternNode))
            {
                return (ExternNode)this.Parent.RightSon;
            }
            else if (!IsLeftSon() && this.Parent!.LeftSon != null && this.Parent.LeftSon.GetType() == typeof(ExternNode))
            {
                return (ExternNode)this.Parent.LeftSon;
            }
            else
            {
                return null;
            }
        }
    }

   
}
