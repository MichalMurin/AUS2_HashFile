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
    /// <summary>
    /// Trieda na spravu statickeho hesovacieho suboru
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class StaticHashing<T> : Hashing<T> where T : IData<T>
    {
        /// <summary>
        /// Celkovy pocet blokov v subore
        /// </summary>
        internal int TotalBlockCount { get; set; }
        /// <summary>
        /// Cesta k ulozeniu aplikacnych dat statickeho suboru
        /// </summary>
        internal static string _pathForStaticFileData { get; } = "staticHashData.csv";
        /// <summary>
        /// Konstruktor triedy
        /// </summary>
        /// <param name="pFileName">cesta k binarnemu suboru</param>
        /// <param name="pBlockFactor">Blokovaci faktor</param>
        /// <param name="pBlockCount">pocet blokov v subore</param>
        internal StaticHashing(string pFileName, int pBlockFactor, int pBlockCount) : base(pFileName, pBlockFactor)
        {
            TotalBlockCount = pBlockCount;
        }

        /// <summary>
        /// Metoda, ktora vrati adresu dat v subore
        /// </summary>
        /// <param name="hash">hash hladanych dat</param>
        /// <returns>adresu</returns>
        protected override long GetOffset(BitArray hash)
        {
            var blockSize = new Block<T>(BlockFactor).GetSize();
            var array = new byte[(hash.Length - 1) / 8 + 1];
            hash.CopyTo(array, 0);
            var longHash = BitConverter.ToInt64(array, 0);
            var offset = (longHash % TotalBlockCount) * blockSize;
            return offset;
        }
        /// <summary>
        /// Metoda na vlozenie dat do struktury
        /// </summary>
        /// <param name="data">vkladane data</param>
        /// <returns>true ak sa vkladanie podarilo</returns>
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

        /// <summary>
        /// Metoda na vymazanie dat zo struktury
        /// </summary>
        /// <param name="data">mazane data</param>
        /// <returns>true ak sa mazanie podarilo</returns>
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

        /// <summary>
        /// Metoda na nacitanie aplikacnych dat zo suboru
        /// </summary>
        /// <returns></returns>
        internal static int LoadStaticDataFromFile()
        {
            string line = File.ReadAllText(_pathForStaticFileData);
            int BlockCount;
            int.TryParse(line, out BlockCount);
            return BlockCount;
        }

        /// <summary>
        /// Metoda na export apliacnych dat
        /// </summary>
        internal override void ExportAppDataToFile()
        {
            File.WriteAllText(_pathForStaticFileData, $"{TotalBlockCount}");
        }
    }
}
