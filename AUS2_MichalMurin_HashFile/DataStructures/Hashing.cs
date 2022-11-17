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
            //int size = Activator.CreateInstance<Block<T>>().GetSize() * TotalBlockCount;
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

        public T Find(T data)
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
                throw new IOException($"Exception found during file seeking: {e.Message}");
            }
            block.FromByteArray(blockBytes);

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
            // File.Seek(adresaBloku);
            // File.Read(blok);
            //  pozriem sa do bloku cije valid count mensi ako blockfactor -. ak ano zvysim valid count a na index validCountu vlozim novy prvok do listu recordov
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

            bool success = block.InsertRecord(data);
            if (success)
            {
                try
                {
                    File.Seek(offset, SeekOrigin.Begin);
                    File.Write(block.ToByteArray());
                    // File.Flush(true);
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
            // pri mazani presuniem mazany zaznam na koniec listu recordov ... a zmensim validCount -> nepotrebujem mazat nic
            Block<T> block = new Block<T>(BlockFactor, data.GetType());

            return true;
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
