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
        public FileStream File { get; set; }
        public int BlockFactor { get; set; }

        public Hashing(string pFileName, int pBlockFactor)
        {
            BlockFactor = pBlockFactor;
            try
            {
                File = new FileStream(pFileName,FileMode.Open, FileAccess.ReadWrite);
            }
            catch (FileNotFoundException)
            {
                throw new FieldAccessException($"File {pFileName} was not found!");
            }
        }

        public T Find(T data)
        {
            Block<T> block;
            BitArray hash = data.GetHash(); // na zaklade hashu ziskam adresu bloku - zalezi od typu hashovania

            block = new Block<T>(BlockFactor, data.GetType());
            byte[] blockBytes = new byte[block.GetSize()];
            try
            {
                //File.Seek(adresaBloku);
            }
            catch (IOException e)
            {
                throw new IOException($"Exception found during file seeking: {e.Message}");
            }
            block.FromByteArray(blockBytes);

            // TODO Kontrola ValidCount => treba skontrolovat kolko blokov je validnych - cize ak bude validnych iba prvych 3 tak beriem iba tie
            foreach (T record in block.Records)
            {
                if (data.MyEquals(record))
                    return record;
            }
            return default(T);
        }

        public bool Insert(T data)
        {
            // File.Seek(adresaBloku);
            // File.Read(blok);
            //  pozriem sa do bloku cije valid count mensi ako blockfactor -. ak ano zvysim valid count a na index validCountu vlozim novy prvok do listu recordov
            Block<T> block = new Block<T>(BlockFactor, data.GetType());

            return true;            
        }

        public bool Delete(T data)
        {
            // pri mazani presuniem mazany zaznam na koniec listu recordov ... a zmensim validCount -> nepotrebujem mazat nic
            Block<T> block = new Block<T>(BlockFactor, data.GetType());

            return true;
        }
    }
}
