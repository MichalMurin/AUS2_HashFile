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

        public T? Find(T data)
        {
            var result = FindBlock(data, OperationType.Find);
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
            var result = FindBlock(data, OperationType.Insert);
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
            var result = FindBlock(data, OperationType.Delete);
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

        private (Block<T>?, long) FindBlock(T data, OperationType opType)
        {
            Block<T> block = new Block<T>(BlockFactor);
            int blockSize = block.GetSize();
            BitArray hash = data.GetHash(); // na zaklade hashu ziskam adresu bloku - zalezi od typu hashovania
            if(opType == OperationType.Find || this.GetType() == typeof(StaticHashing<T>))
            {
                var result = GetOffset(hash, blockSize, opType);
                byte[] blockBytes = new byte[blockSize];
                try
                {
                    File.Seek(result.Item2, SeekOrigin.Begin);
                    File.Read(blockBytes);
                }
                catch (IOException e)
                {
                    throw new IOException($"Exception found during reading the file: {e.Message}");
                }
                block.FromByteArray(blockBytes);
                return (block, result.Item2);
            }
            else if (opType == OperationType.Insert)
            {
                // v dynamickom hashing zistime ci sa da vlozit, ak ano hned vratime blok , ak nie musime prehashovat a vratit uz novy blok, kde sa data zmestia
                // vraciame blok v ktorom uz su veci ktore tam maju byt..staci uz iba pridat novo vkladane data
                return GetOffset(hash, blockSize, opType);
            }
            else if (opType == OperationType.Delete)
            {
                // TODO ohendlovat ako sa to bude spravat pri delte
            }
           
        }

        protected abstract (Block<T>?, long) GetOffset(BitArray hash, int blockSize, OperationType opType);

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
                Console.WriteLine($"Blok cislo {i}: Valid count = {block.ValidCount}");
                foreach (var rec in block.Records)
                {
                    Console.WriteLine($"\t{rec.ToString()}");
                }
            }
        }
    }
}
