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
        //test.GeneratePatientsInFile(1000000, "patients.csv");
        Test test = new Test(5, HashType.DynamicHash);
        test.GenerateStatistics(100000);
        //test.runTest(1000, 1000, 1, true);
        Test test2 = new Test(5, HashType.StaticHash, 100000);
        test2.GenerateStatistics(100000);
        //test2.runTest(1000, 1000, 1, true);


        Test test3 = new Test(100, HashType.DynamicHash);
        test3.GenerateStatistics(100000);
        Test test4 = new Test(100, HashType.StaticHash, 20000);
        test4.GenerateStatistics(100000);
        return 0;
    }


}