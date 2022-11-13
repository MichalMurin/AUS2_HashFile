using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUS2_MichalMurin_HashFile.DataStructures
{
    public interface IRecord<T>
    {
        public byte[] ToByteArray();
        public void FromByteArray(byte[] pArray);
        public int GetSize();
    }
}
