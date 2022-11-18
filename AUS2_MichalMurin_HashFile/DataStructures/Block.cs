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
        public int BlockFactor { get; set; }
        public int ValidCount { get; set; }
        public List<T> Records { get; set; }
        // Podla mna redundantny atribut, kedze si typ triedy zistim z generika T
        public Type ClassType { get; set; }

        public Block(int pBlockFactor, Type pClassType)
        {
            BlockFactor = pBlockFactor;
            ClassType = pClassType;
            Records = new List<T>(pBlockFactor);
            for (int i = 0; i < pBlockFactor; i++)
            {
                try
                {
                    // TODO vytvorit instanciu triedy podla pClassType - neviem ci mozem vytvarat aj podla T
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
            throw new IndexOutOfRangeException("Too many record in the block!");
            //return false;
        }

        public bool RemoveRecord(T pRecord)
        {
            for (int i = 0; i < Records.Count; i++)
            {
                if (pRecord.MyEquals(Records[i]))
                {
                    // swapnem posledny prvok s tym co chcem vymazat
                    (Records[i], Records[Records.Count - 1]) = (Records[Records.Count - 1], Records[i]);
                    ValidCount--;
                    return true;
                }
            }
            return false;
            
        }
        //https://stackoverflow.com/questions/1446547/how-to-convert-an-object-to-a-byte-array-in-c-sharp
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
            //using (var memStream = new MemoryStream())
            //{
            //    var binForm = new BinaryFormatter();
            //    memStream.Write(pArray, 0, pArray.Length);
            //    memStream.Seek(0, SeekOrigin.Begin);
            //    var obj = binForm.Deserialize(memStream);
            //    return obj;
            //}
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

        //https://stackoverflow.com/questions/1446547/how-to-convert-an-object-to-a-byte-array-in-c-sharp
        public byte[] ToByteArray()
        {
            // ValidCount = prve 4 bajty
            List<byte> result = new List<byte>();
            result.AddRange(BitConverter.GetBytes(ValidCount));
            foreach (var record in Records)
            {
                result.AddRange(record.ToByteArray());
            }
            return result.ToArray();
        }
    }
}
