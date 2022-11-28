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
        public FileStream File { get; set; }
        public int BlockFactor { get; set; }

        public Hashing(string pFileName, int pBlockFactor)
        {
            BlockFactor = pBlockFactor;
            try
            {
                File = new FileStream(pFileName,FileMode.OpenOrCreate, FileAccess.ReadWrite);
            }
            catch (FileNotFoundException)
            {
                throw new FieldAccessException($"File {pFileName} is not acceccible!");
            }
        }
        protected (Block<T>, long) FindBlock(T data)
        {
            BitArray hash = data.GetHash(); // na zaklade hashu ziskam adresu bloku - zalezi od typu hashovania
            var offset = GetOffset(hash);
            var block = TryReadBlockFromFile(offset);
            return (block, offset);
        }
        public T? Find(T data)
        {
            var result = FindBlock(data);
            var block = result.Item1;
            for (int i = 0; i < block.Records.Count; i++)
            {
                if (i < block.ValidCount)
                {
                    if (data.MyEquals(block.Records[i]))
                        return block.Records[i];
                }
            }
            return default(T);
        }
        public abstract bool Insert(T data);

        public abstract bool Delete(T data);

        protected abstract long GetOffset(BitArray hash);

        protected void TryWriteBlockToFile(long offset, Block<T> block)
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

        protected Block<T> TryReadBlockFromFile(long offset)
        {
            var block = new Block<T>(BlockFactor) ;
            var blockSize = block.GetSize();
            byte[] blockBytes = new byte[blockSize];
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
            return block;
        }

        public void ConsoleWriteSequence()
        {
            File.Seek(0, SeekOrigin.Begin);
            // TODO Otestuj ci sedi pocwt blokov
            Block<T> block = new Block<T>(BlockFactor);
            long blockCount = File.Length/block.GetSize();
            for (long i = 0; i < blockCount; i++)
            {
                //Block<T> block = new Block<T>(BlockFactor);
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
                Console.WriteLine($"ADRESA: {File.Position}");
                Console.WriteLine($"Blok cislo {i}: Valid count = {block.ValidCount}");
                foreach (var rec in block.Records)
                {
                    Console.WriteLine($"\t{rec.ToString()}");
                }
            }
        }

        public void DisposeFile()
        {
            File.Dispose();
        }
    }
}
