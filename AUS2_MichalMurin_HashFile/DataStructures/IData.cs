using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUS2_MichalMurin_HashFile.DataStructures
{
    public interface IData<T>: IRecord<T>
    {
        public BitArray GetHash();
        public bool MyEquals(T data);
        public T CreateClass();

        public List<string> GetStrings();
    }
}
