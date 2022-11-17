using AUS2_MichalMurin_HashFile.DataStructures;
using AUS2_MichalMurin_HashFile.Models;
using System;
using System.Buffers.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;

class Program
{
    public static int Main()
    {
        Hashing<Patient> hash = new Hashing<Patient>("TestFile", 5, 10);
        Patient pat = new Patient("Michal", "Murin", "0003284292", DateTime.Now, 25);
        hash.Find(pat);
        hash.Insert(pat);
        hash.Insert(pat);
        hash.Insert(pat);
        hash.Insert(pat);
        hash.Insert(pat);
        hash.Insert(pat);
        hash.Insert(pat);
        hash.ConsoleWriteSequence();

        var test = pat.ToByteArray();
        int size = pat.GetSize();
        Patient second = new Patient();
        second.FromByteArray(test);
        return 0;


        //var str = "";
        //str += string.Concat(Enumerable.Repeat("x", 6));
        //int num = 99;
        //using (MemoryStream stream = new MemoryStream())
        //{
        //    using (BinaryWriter writer = new BinaryWriter(stream))
        //    {
        //        writer.Write(Encoding.Default.GetBytes(str));
        //    }
        //    var array = stream.ToArray();
        //    int index = 0;
        //    string value = ASCIIEncoding.ASCII.GetString(array, index, 6);
        //    int number = BitConverter.ToInt16(array, 6);
        //    Console.WriteLine(value);

        //}
        //return 0;
    }

}