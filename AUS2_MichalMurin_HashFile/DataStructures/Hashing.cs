using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace AUS2_MichalMurin_HashFile.DataStructures
{
    /// <summary>
    /// Trieda predstavujuca hashovaci subor
    /// </summary>
    /// <typeparam name="T">data udrziavane v udajovej strukture</typeparam>
    internal abstract class Hashing<T> where T: IData<T>
    {
        /// <summary>
        /// Binarny subor s datami
        /// </summary>
        internal FileStream HashFile { get; set; }
        /// <summary>
        /// Pocet zaznamov v bloku
        /// </summary>
        internal int BlockFactor { get; set; }
        /// <summary>
        /// Velkost bloku v bajtoch
        /// </summary>
        internal int BlockSize { get; private set; }
        /// <summary>
        /// Cesta k ulozeniu aplikacnych dat
        /// </summary>
        internal static string _pathToBaseData { get; } = "baseData.csv";
        /// <summary>
        /// Konstruktor tiredy
        /// </summary>
        /// <param name="pFileName">cesta k binarnemu suboru</param>
        /// <param name="pBlockFactor">pocet zaznamov v bloku</param>
        /// <exception cref="FieldAccessException"></exception>
        internal Hashing(string pFileName, int pBlockFactor)
        {
            BlockSize = new Block<T>(pBlockFactor).GetSize();
            BlockFactor = pBlockFactor;
            try
            {
                HashFile = new FileStream(pFileName,FileMode.OpenOrCreate, FileAccess.ReadWrite);
            }
            catch (FileNotFoundException)
            {
                throw new FieldAccessException($"HashFile {pFileName} is not acceccible!");
            }
        }
        /// <summary>
        /// Metoda ktora najde blok na zaklade hladanych dat
        /// </summary>
        /// <param name="data">hladane data</param>
        /// <returns>blok a jeho adresu</returns>
        protected (Block<T>?, long) FindBlock(T data)
        {
            BitArray hash = data.GetHash(); // na zaklade hashu ziskam adresu bloku - zalezi od typu hashovania
            var offset = GetOffset(hash);
            if (offset != -1)
            {
                var block = TryReadBlockFromFile(offset);
                return (block, offset);
            }
            else
                return (null, -1);
        }
        /// <summary>
        /// metoda ktora najde hladane data v udajovej strukture
        /// </summary>
        /// <param name="data">hladane data</param>
        /// <returns>Najdede data</returns>
        internal T? Find(T data)
        {
            var result = FindBlock(data);
            var block = result.Item1;
            if(block != null)
            {
                for (int i = 0; i < block.Records.Count; i++)
                {
                    if (i < block.ValidCount)
                    {
                        if (data.MyEquals(block.Records[i]))
                            return block.Records[i];
                    }
                }
            }
            return default(T);
        }

        /// <summary>
        /// Metoda na aktualizovanie dat v udajovej strukture
        /// </summary>
        /// <param name="data">aktualizovane data</param>
        /// <returns>Rrue ak sa update podaril, inak false</returns>
        internal bool UpdateData(T data)
        {
            var result = FindBlock(data);
            var block = result.Item1;
            if (block != null)
            {
                for (int i = 0; i < block.Records.Count; i++)
                {
                    if (i < block.ValidCount)
                    {
                        if (data.MyEquals(block.Records[i]))
                        {
                            block.Records[i] = data;
                            TryWriteBlockToFile(GetOffset(data.GetHash()), block);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Abstraktna metoda na pridanie dat do struktury
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal abstract bool Insert(T data);

        /// <summary>
        /// Abstraktna metoda na vymazanie dat zo struktury
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal abstract bool Delete(T data);

        /// <summary>
        /// Abstraktna metoda na ziskanie adresy dat v subore
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        protected abstract long GetOffset(BitArray hash);

        /// <summary>
        /// Metoda na zapisanie dat do suboru
        /// </summary>
        /// <param name="offset">adresa</param>
        /// <param name="block">blok dat</param>
        /// <exception cref="IOException"></exception>
        protected void TryWriteBlockToFile(long offset, Block<T> block)
        {
            try
            {
                HashFile.Seek(offset, SeekOrigin.Begin);
                HashFile.Write(block.ToByteArray());
            }
            catch (IOException e)
            {
                throw new IOException($"Exception found during writing to file: {e.Message}");
            }
        }

        /// <summary>
        /// Metoda na citanie dat zo suboru
        /// </summary>
        /// <param name="offset">adresa</param>
        /// <returns>precitany blok dat</returns>
        /// <exception cref="IOException"></exception>
        protected Block<T> TryReadBlockFromFile(long offset)
        {
            var block = new Block<T>(BlockFactor) ;
            var blockSize = block.GetSize();
            byte[] blockBytes = new byte[blockSize];
            try
            {
                HashFile.Seek(offset, SeekOrigin.Begin);
                HashFile.Read(blockBytes);
            }
            catch (IOException e)
            {
                throw new IOException($"Exception found during reading the file: {e.Message}");
            }
            block.FromByteArray(blockBytes);
            return block;
        }

        /// <summary>
        /// Abstraktna metoda na export aplikacnych dat
        /// </summary>
        internal abstract void ExportAppDataToFile();

        /// <summary>
        /// Metoda na ulozenie spolocnych dat do csv suboru
        /// </summary>
        internal void SaveBaseDataToFile()
        {
            File.WriteAllText(_pathToBaseData, $"{BlockFactor};{HashFile.Name}");
        }

        /// <summary>
        /// Metoda na nacitanie spolocnych dat
        /// </summary>
        /// <returns>blokfaktor a cestu k binarnemu suboru</returns>
        internal static (int, string) LoadBaseDataFromFile()
        {
            string line = File.ReadAllText(_pathToBaseData);
            var results = line.Split(";");
            int BlFactor;
            int.TryParse(results[0], out BlFactor);
            return (BlFactor, results[1]);
        }

        /// <summary>
        /// Metoda na sekvencny vypis suboru
        /// </summary>
        /// <returns>zoznam retazcov predstavujucich data v subore</returns>
        internal List<string> GetSequenceOfBlocks()
        {
            var result = new List<string>();
            HashFile.Seek(0, SeekOrigin.Begin);
            Block<T> block = new Block<T>(BlockFactor);
            long blockCount = HashFile.Length / block.GetSize();
            int valids = 0;
            for (long i = 0; i < blockCount; i++)
            {
                result.Add("========================================================================");
                result.Add($"ADRESA: {HashFile.Position}");
                byte[] blockBytes = new byte[block.GetSize()];
                try
                {
                    HashFile.Read(blockBytes);
                }
                catch (Exception)
                {
                    throw;
                }
                block.FromByteArray(blockBytes);
                result.Add($"Blok cislo {i}: Valid count = {block.ValidCount}");
                foreach (var rec in block.Records)
                {
                    result.AddRange(rec.GetStrings());
                }
                valids += block.ValidCount;
            }
            result.Insert(0, $"Pocet Validnych dat = {valids}");
            return result;
        }

        /// <summary>
        /// Metoda na zatvroenie binarneho suboru
        /// </summary>
        internal void DisposeAndCloseFile()
        {
            HashFile.Dispose();
            HashFile.Close();
        }
    }
}
