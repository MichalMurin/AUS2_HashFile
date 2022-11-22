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
        Test test = new Test(100, 1000);
        test.runTest(1000, 10000, 1);
        return 0;
    }
}