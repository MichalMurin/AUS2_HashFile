using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUS2_MichalMurin_HashFile.DataStructures
{
    internal class DynamicHashing<T> : Hashing<T> where T : IData<T>
    {
        public DynamicHashing(string pFileName, int pBlockFactor) : base(pFileName, pBlockFactor)
        {
        }

        protected override long GetOffset(BitArray hash, int blockSize)
        {
            throw new NotImplementedException();
        }
    }
}
