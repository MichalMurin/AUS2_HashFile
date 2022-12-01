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

        //if (File.Exists(@"Test"))
        //{
        //    File.Delete(@"Test");
        //}

        //var hash = new DynamicHashing<Patient>("Test", 1);
        //var list = new List<Patient>();
        //list.Add(new Patient("Michal", "Murin", "0003284292", DateTime.Now, 25));
        //list.Add(new Patient("Jozef", "Murin", "9903284292", DateTime.Now, 25));
        //list.Add(new Patient("Peter", "Murin", "5403284292", DateTime.Now, 25));
        //list.Add(new Patient("Marek", "Murin", "7803184292", DateTime.Now, 25));
        //list.Add(new Patient("Karol", "Murin", "0003084292", DateTime.Now, 25));
        //list.Add(new Patient("Lojzo", "Murin", "0003294292", DateTime.Now, 25));

        //foreach (var item in list)
        //{
        //    hash.Insert(item);
        //   // hash.ConsoleWriteSequence();
        //}
        //hash.ConsoleWriteSequence();
        //hash.Delete(list[4]);
        //Console.WriteLine("-------------------------------------------------");
        //hash.ConsoleWriteSequence();
        //hash.Delete(list[0]);
        //Console.WriteLine("-------------------------------------------------");
        //hash.ConsoleWriteSequence();




        Test test = new Test(3, 1000);
         test.runTest(1000, 10000, 1);
        return 0;
    }


}