using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUS2_MichalMurin_HashFile.DataStructures
{
    internal class StaticHashing<T> : Hashing<T> where T : IData<T>
    {

        public int TotalBlockCount { get; set; }
        public StaticHashing(string pFileName, int pBlockFactor, int pBlockCount) : base(pFileName, pBlockFactor)
        {
            TotalBlockCount = pBlockCount;
        }

        protected override (Block<T>?, long) GetOffset(BitArray hash, int blockSize, OperationType opType)
        {
            var array = new byte[(hash.Length - 1) / 8 + 1];
            hash.CopyTo(array, 0);
            var longHash = BitConverter.ToInt64(array, 0);
            var offset = (longHash % TotalBlockCount) * blockSize;
            return (null,offset);
        }
    }
}
