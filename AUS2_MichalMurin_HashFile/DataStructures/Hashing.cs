using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace AUS2_MichalMurin_HashFile.DataStructures
{
    public abstract class Hashing<T> where T: IData<T>
    {
        public FileStream HashFile { get; set; }
        public int BlockFactor { get; set; }
        public int BlockSize { get; private set; }
        public static string _pathToBaseData { get; } = "baseData.csv";
        public Hashing(string pFileName, int pBlockFactor)
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
        public T? Find(T data)
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

        public bool UpdateData(T data)
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
        public abstract bool Insert(T data);

        public abstract bool Delete(T data);

        protected abstract long GetOffset(BitArray hash);

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

       public abstract void ExportAppDataToFile();

       public void SaveBaseDataToFile(string path)
        {
            File.WriteAllText(path, $"{BlockFactor};{HashFile.Name}");
        }

        // vraciam blok faktor a cestu k suboru
        public static (int, string) LoadBaseDataFromFile()
        {
            string line = File.ReadAllText(_pathToBaseData);
            var results = line.Split(";");
            int BlFactor;
            int.TryParse(results[0], out BlFactor);
            return (BlFactor, results[1]);
        }

        public void ConsoleWriteSequence()
        {
            HashFile.Seek(0, SeekOrigin.Begin);
            // TODO Otestuj ci sedi pocwt blokov
            Block<T> block = new Block<T>(BlockFactor);
            long blockCount = HashFile.Length/block.GetSize();
            for (long i = 0; i < blockCount; i++)
            {
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
                Console.WriteLine($"ADRESA: {HashFile.Position}");
                Console.WriteLine($"Blok cislo {i}: Valid count = {block.ValidCount}");
                foreach (var rec in block.Records)
                {
                    Console.WriteLine($"\t{rec.ToString()}");
                }
            }
        }

        public List<string> GetSequenceOfBlocks()
        {
            var result = new List<string>();
            HashFile.Seek(0, SeekOrigin.Begin);
            Block<T> block = new Block<T>(BlockFactor);
            long blockCount = HashFile.Length / block.GetSize();
            for (long i = 0; i < blockCount; i++)
            {
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
                    result.Add($"\t{rec.ToString()}");
                }
            }
            return result;
        }

        public void DisposeFile()
        {
            HashFile.Dispose();
        }
    }
}
