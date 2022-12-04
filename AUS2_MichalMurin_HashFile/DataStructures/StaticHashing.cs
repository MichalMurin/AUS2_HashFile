using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUS2_MichalMurin_HashFile.DataStructures
{
    internal class StaticHashing<T> : Hashing<T> where T : IData<T>
    {
        internal int TotalBlockCount { get; set; }
        internal static string _pathForStaticFileData { get; } = "staticHashData.csv";
        internal StaticHashing(string pFileName, int pBlockFactor, int pBlockCount) : base(pFileName, pBlockFactor)
        {
            TotalBlockCount = pBlockCount;
        }

        protected override long GetOffset(BitArray hash)
        {
            var blockSize = new Block<T>(BlockFactor).GetSize();
            var array = new byte[(hash.Length - 1) / 8 + 1];
            hash.CopyTo(array, 0);
            var longHash = BitConverter.ToInt64(array, 0);
            var offset = (longHash % TotalBlockCount) * blockSize;
            return offset;
        }

        internal override bool Insert(T data)
        {
            var result = FindBlock(data);
            var block = result.Item1;
            var offset = result.Item2;
            bool success = block!.InsertRecord(data);
            if (success)
            {
                TryWriteBlockToFile(offset, block);
            }            
            return success;
        }


        internal override bool Delete(T data)
        {
            var result = FindBlock(data);
            var block = result.Item1;
            var offset = result.Item2;
            bool success = block!.RemoveRecord(data);
            if (success)
            {
                TryWriteBlockToFile(offset, block);
            }
            return success;
        }

        internal static int LoadStaticDataFromFile()
        {
            string line = File.ReadAllText(_pathForStaticFileData);
            int BlockCount;
            int.TryParse(line, out BlockCount);
            return BlockCount;
        }

        internal override void ExportAppDataToFile()
        {
            File.WriteAllText(_pathForStaticFileData, $"{TotalBlockCount}");
        }
    }
}
