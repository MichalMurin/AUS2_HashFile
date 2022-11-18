using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace AUS2_MichalMurin_HashFile.DataStructures
{
    internal class Hashing<T> where T: IData<T>
    {
        public int TotalBlockCount { get; set; }
        public FileStream File { get; set; }
        public int BlockFactor { get; set; }

        public Hashing(string pFileName, int pBlockFactor, int pBlockCount)
        {
            TotalBlockCount = pBlockCount;
            BlockFactor = pBlockFactor;
            int size =   new Block<T>(BlockFactor, typeof(T)).GetSize() * TotalBlockCount;
            try
            {
                File = new FileStream(pFileName,FileMode.OpenOrCreate, FileAccess.ReadWrite);
            }
            catch (FileNotFoundException)
            {
                throw new FieldAccessException($"File {pFileName} is not acceccible!");
            }
        }

        public T? Find(T data)
        {
            var result = FindBlock(data);
            var block = result.Item1;
            for (int i = 0; i < block.Records.Count; i++)
            {
                if(i < block.ValidCount)
                {
                    if (data.MyEquals(block.Records[i]))
                        return block.Records[i];
                }
            }
            return default(T);
        }

        public bool Insert(T data)
        {
            var result = FindBlock(data);
            var block = result.Item1;
            var offset = result.Item2;
            bool success = block.InsertRecord(data);
            if (success)
            {
                try
                {
                    File.Seek(offset, SeekOrigin.Begin);
                    File.Write(block.ToByteArray());
                }
                catch (IOException e)
                {
                    throw new IOException($"Exception found during writing to file: {e.Message}");
                }
            }
            return success;
        }

        public bool Delete(T data)
        {
            var result = FindBlock(data);
            var block = result.Item1;
            var offset = result.Item2;
            bool success = block.RemoveRecord(data);
            if (success)
            {
                try
                {
                    File.Seek(offset, SeekOrigin.Begin);
                    File.Write(block.ToByteArray());
                }
                catch (IOException e)
                {
                    throw new IOException($"Exception found during writing to file: {e.Message}");
                }
            }
            return success;
        }

        private (Block<T>, long) FindBlock(T data)
        {
            Block<T> block = new Block<T>(BlockFactor, data.GetType());
            long hash = data.GetHash(); // na zaklade hashu ziskam adresu bloku - zalezi od typu hashovania
            var offset = (hash % TotalBlockCount) * block.GetSize();
            byte[] blockBytes = new byte[block.GetSize()];
            try
            {
                File.Seek(offset, SeekOrigin.Begin);
                File.Read(blockBytes);
            }
            catch (IOException e)
            {
                throw new IOException($"Exception found during reading the file: {e.Message}");
            }
            block.FromByteArray(blockBytes);
            return (block, offset);
        }

        public void ConsoleWriteSequence()
        {
            File.Seek(0, SeekOrigin.Begin);
            for (int i = 0; i < TotalBlockCount; i++)
            {
                Block<T> block = new Block<T>(BlockFactor, typeof(T));
                byte[] blockBytes = new byte[block.GetSize()];
                try
                {
                    File.Read(blockBytes);
                }
                catch (Exception)
                {
                    throw;
                }
                block.FromByteArray(blockBytes);
                Console.WriteLine($"Blok cislo {i}: Valid count = {block.ValidCount}");
                foreach (var rec in block.Records)
                {
                    Console.WriteLine($"\t{rec.ToString()}");
                }
            }
        }
    }
}
