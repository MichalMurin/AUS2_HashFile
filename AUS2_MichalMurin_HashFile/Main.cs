using AUS2_MichalMurin_HashFile.DataStructures;
using AUS2_MichalMurin_HashFile.DataStructures.Trie;
using AUS2_MichalMurin_HashFile.Models;
using AUS2_MichalMurin_HashFile.Service;
using System;
using System.Buffers.Binary;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;

class Program
{
    public static int Main()
    {
        DataGenerator gen = new DataGenerator();

        var pat = gen.getRndPatient();
        var size = pat.GetSize();
        var array = pat.ToByteArray();

        var secondpat = new Patient();

        secondpat.FromByteArray(array);



        if (File.Exists(@"Test"))
        {
            File.Delete(@"Test");
        }

        var hash = new DynamicHashing<Patient>("Test", 1);
        var list = new List<Patient>();
        list.Add(new Patient("Michal", "Murin", "0003284292",  25));
        list.Add(new Patient("Jozef", "Murin", "9903284292",  25));
        list.Add(new Patient("Peter", "Murin", "5403284292", 25));
        list.Add(new Patient("Marek", "Murin", "7803184292", 25));
        list.Add(new Patient("Karol", "Murin", "0003084292",25));
        list.Add(new Patient("Lojzo", "Murin", "0003294292", 25));

        


        foreach (var item in list)
        {
            hash.Insert(item);
            // hash.ConsoleWriteSequence();
        }

        hash.ConsoleWriteSequence();
        hash.Delete(list[4]);
        Console.WriteLine("-------------------------------------------------");
        hash.ConsoleWriteSequence();
        hash.Delete(list[0]);
        Console.WriteLine("-------------------------------------------------");
        hash.ConsoleWriteSequence();




        Test test = new Test(5, HashType.DynamicHash,100);
         test.runTest(1000, 1000, 1, true);
        return 0;
    }


}