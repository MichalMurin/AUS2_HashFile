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

        Test test = new Test(5, HashType.DynamicHash);
         test.runTest(1000, 1000, 1, true);
        Test test2 = new Test(5, HashType.StaticHash, 10000);
        test2.runTest(1000, 1000, 1, true);
        return 0;
    }


}