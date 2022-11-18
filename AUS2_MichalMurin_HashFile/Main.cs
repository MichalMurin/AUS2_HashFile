using AUS2_MichalMurin_HashFile.DataStructures;
using AUS2_MichalMurin_HashFile.Models;
using AUS2_MichalMurin_HashFile.Service;
using System;
using System.Buffers.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;

class Program
{
    public static int Main()
    {
        Test test = new Test();
        test.runTest();

        //Hashing<Patient> hash = new Hashing<Patient>("TestFile", 5, 10);
        //Patient pat1 = new Patient("Michal", "Murin", "0003284292", DateTime.Now.AddYears(-14), 24);
        //Patient pat2 = new Patient("Natalia", "Murin", "8005328492", DateTime.Now.AddYears(-40), 25);
        //Patient pat3 = new Patient("Nikola", "Murin", "9953284224", DateTime.Now.AddYears(-5), 26);
        //Patient pat4 = new Patient("Pavol", "Murin", "0003284272", DateTime.Now.AddYears(-66), 25);
        //hash.Insert(pat1);
        //hash.Insert(pat2);
        //hash.Insert(pat3);
        //hash.Insert(pat4);
        //hash.ConsoleWriteSequence();
        //hash.Find(pat4);
        //hash.Delete(pat4);
        //hash.Find(pat4);
        //hash.ConsoleWriteSequence();

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