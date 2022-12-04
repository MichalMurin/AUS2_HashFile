using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUS2_MichalMurin_HashFile.DataStructures.Trie
{
    internal class ExternNode: TrieNode
    {
        internal int RecordsCount { get; set; }
        internal long Offset { get; set; }

        internal ExternNode(long pOffset, int pRecordsCount, InternNode? parent = null) : base(parent)
        {
            Offset = pOffset;
            RecordsCount = pRecordsCount;
        }
        internal ExternNode(ExternNode otherNode)
        {
            RecordsCount = otherNode.RecordsCount;
            Offset = otherNode.Offset;
            this.Parent = otherNode.Parent;
        }
        internal bool IsLeftSon()
        {
            return ((InternNode)Parent!).LeftSon == this;
        }

        internal ExternNode? GetBrother()
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
