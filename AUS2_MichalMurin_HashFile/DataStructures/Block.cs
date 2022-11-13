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
            // TODO vlozit do listu novy record
            return true;
        }
        //https://stackoverflow.com/questions/1446547/how-to-convert-an-object-to-a-byte-array-in-c-sharp
        public void FromByteArray(byte[] pArray)
        {
            // implementovat este ValidCount!!
            for (int i = 0; i < BlockFactor; i++)
            {
                byte[] tmpArray = new byte[(i + 1) * Records[i].GetSize()];
                Array.Copy(pArray, i * Records[i].GetSize(), tmpArray, 0, (i + 1) * Records[i].GetSize());
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
            // niekde este dat ValidCount
            List<byte> result = new List<byte>();
            foreach (var record in Records)
            {
                result.Concat(record.ToByteArray());
            }
            return result.ToArray();


            //BinaryFormatter bf = new BinaryFormatter();
            //using (var ms = new MemoryStream())
            //{
            //    bf.Serialize(ms, this);
            //    return ms.ToArray();
            //}
        }
    }
}
