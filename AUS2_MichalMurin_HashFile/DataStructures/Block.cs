using AUS2_MichalMurin_HashFile.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AUS2_MichalMurin_HashFile
{
    public class Block<T> : IRecord<T> where T : IData<T>
    {
        public int BlockFactor { get; private set; }
        public int ValidCount { get; set; }
        public List<T> Records { get; set; }

        public Block(int pBlockFactor)
        {
            BlockFactor = pBlockFactor;
            Records = new List<T>(pBlockFactor);
            for (int i = 0; i < pBlockFactor; i++)
            {
                try
                {
                    Records.Add(Activator.CreateInstance<T>().CreateClass());
                }
                catch (Exception e)
                {
                    throw new Exception($"Execption occured: {e.Message}");
                }
            }
            ValidCount = 0;
        }
        public bool InsertRecord(T pNew)
        {
            if(ValidCount < BlockFactor)
            {
                Records[ValidCount] = pNew;
                ValidCount++;
                return true;
            }
            //throw new IndexOutOfRangeException("Too many record in the block!");
            return false;
        }

        public bool RemoveRecord(T pRecord)
        {
            for (int i = 0; i < ValidCount; i++)
            {
                if (pRecord.MyEquals(Records[i]))
                {
                    // swapnem posledny prvok s tym co chcem vymazat
                    (Records[i], Records[ValidCount-1]) = (Records[ValidCount-1], Records[i]);
                    ValidCount--;
                    return true;
                }
            }
            return false;
            
        }
        public void FromByteArray(byte[] pArray)
        {
            // precitame si valid count - prve 4 bajty
            this.ValidCount = BitConverter.ToInt32(pArray, 0);
            for (int i = 0; i < BlockFactor; i++)
            {
                byte[] tmpArray = new byte[Records[i].GetSize()];
                Array.Copy(pArray, i * Records[i].GetSize() + sizeof(int), tmpArray, 0, Records[i].GetSize());
                Records[i].FromByteArray(tmpArray);
            }
        }

        public int GetSize()
        {
            try
            {
                // na konci este pripocitavame velkost intu = ValidCount
                return Activator.CreateInstance<T>().GetSize() * BlockFactor + sizeof(int);
            }
            catch (Exception e)
            {
                throw new Exception($"Exception occured while getting size of Block: {e.Message}");
            }
        }

        public byte[] ToByteArray()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    // prve 4 bajty - ValidCount
                    writer.Write(ValidCount);
                    foreach (var record in Records)
                    {
                        writer.Write(record.ToByteArray());
                    }
                }
                return stream.ToArray();
            }
        }
    }
}
