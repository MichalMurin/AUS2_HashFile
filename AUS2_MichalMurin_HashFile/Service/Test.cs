using AUS2_MichalMurin_HashFile.DataStructures;
using AUS2_MichalMurin_HashFile.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AUS2_MichalMurin_HashFile.Service
{
    internal class Test
    {
        private Hashing<Patient> hash;
        private DataGenerator generator = new DataGenerator();
        private Random rand = new Random();
        private HashSet<string> setOfBirthNums = new HashSet<string>();
        internal Test(int blockFactor, HashType type, int blockCount = 0)
        {
            if (File.Exists(@"TESTING"))
            {
                File.Delete(@"TESTING");
            }
            if (type == HashType.StaticHash && blockCount > 0)
                hash = new StaticHashing<Patient>("TESTING", blockFactor, blockCount);
            else if (type == HashType.DynamicHash)
                hash = new DynamicHashing<Patient>("TESTING", blockFactor);
            else
                throw new ArgumentException("Invalid hash type");
        }        

        private Patient GetPatient()
        {
            while(true)
            {
                var pat = generator.getRndPatient();
                if (!setOfBirthNums.Add(pat.BirthNum))
                    continue;
                return pat;
            }
        }

        private List<Patient> FillListFromTestFile(int size)
        {
            var listofPatients = new List<Patient>(size);
            var tmp = File.ReadAllLines("patients.csv");
            for (int i = 0; i < tmp.Length; i++)
            {
                var items = tmp[i].Split(";");
                listofPatients.Add(new Patient(items[0], items[1], items[2],  1));
            }
            return listofPatients;
        }

        private bool checkAllDataInFile(List<Patient> checkList, string errorMsg)
        {
            bool ret = true;
            var listofPatientsInFile = GetListOfBirthunmsInFile();
            foreach (var pat in checkList)
            {
                if (!listofPatientsInFile.Contains(pat.BirthNum))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(errorMsg);
                    Console.ResetColor();
                    ret = false;
                }
            }
            return ret;
            
        }

        private List<string> GetListOfBirthunmsInFile()
        {
            List<string> retList = new List<string>();
            hash.HashFile.Seek(0, SeekOrigin.Begin);
            Block<Patient> block = new Block<Patient>(hash.BlockFactor);
            long blockCount = hash.HashFile.Length / block.GetSize();
            for (long i = 0; i < blockCount; i++)
            {
                byte[] blockBytes = new byte[block.GetSize()];
                try
                {
                    hash.HashFile.Read(blockBytes);
                }
                catch (Exception)
                {
                    throw;
                }
                block.FromByteArray(blockBytes);
                for (int j = 0; j < block.ValidCount; j++)
                {
                    retList.Add(block.Records[j].BirthNum);
                }
            }
            return retList;
        }
        internal bool runTest(int initialSize, int numberOfOperations, int step = 0, bool checkAllDataAfterEachOperation = false)
        {
            var type = hash.GetType() == typeof(StaticHashing<Patient>) ? "STATICKY" : "DYNAMICKY";
            Console.WriteLine($"START TESTU - testujeme {type} hash file o inicializacnej velkosti {initialSize}, pocet operacii: {numberOfOperations}");
            Stopwatch stopwatchInsert = new Stopwatch();
            Stopwatch stopwatchFind = new Stopwatch();
            Stopwatch stopwatchDelete = new Stopwatch();

            //var listofPatients = FillListFromTestFile(initialSize);
            //for (int i = 0; i < initialSize; i++)
            //{
            //    var patient = listofPatients[i];
            //    bool success = hash.Insert(patient);
            //    Patient? patient2 = hash.Find(patient);
            //    if (!success || patient2 == null || patient2.BirthNum != patient.BirthNum)
            //    {
            //        Console.ForegroundColor = ConsoleColor.Red;
            //        Console.WriteLine("Nepodarilo sa pridat prvok - TEST ZLYHAL - Insert()");
            //        Console.ResetColor();
            //    }
            //}
            var listofPatients = new List<Patient>(initialSize);
            Console.WriteLine("Zacinam naplnovat subor nahodnymi datami");
            while (listofPatients.Count != initialSize)
            {
                var patient = GetPatient();
                listofPatients.Add(patient);
                bool success = hash.Insert(patient);
                if (hash.GetType() == typeof(StaticHashing<Patient>) && !success)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("STATICKY SUBOR -> Nepodarilo sa pridat prvok, pretoze je uz plny blok");
                    Console.ResetColor();
                }
                else
                {
                    Patient? patient2 = hash.Find(patient);
                    if (!success || patient2 == null || !patient2.MyEquals(patient))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Nepodarilo sa pridat prvok - TEST ZLYHAL - Insert()");
                        Console.ResetColor();
                    }
                }                
            }
            if (checkAllDataAfterEachOperation)
            {
                checkAllDataInFile(listofPatients, "Pri inicializacnom naplnani dat nastala chyba! - INSERT()");
            }

            int numberOfInsert = 0;
            int numberOfFind = 0;
            int numberOfDelete = 0;
            Patient data;
            int operationNumber = 0;
            bool result;
            int rndIndex = 0;

            Console.WriteLine("Subor je naplneny na inicializacnu velkost, zacinam testovat operacie");
            //for (int i = 0; i < 100000; i++)
            //{
            //    rndIndex = rand.Next(0, listofPatients.Count);
            //    var dataToFind = listofPatients[rndIndex];
            //    stopwatchFind.Start();
            //    var item = hash.Find(dataToFind);
            //    stopwatchFind.Stop();
            //    numberOfFind++;
            //    if (item == null || item.BirthNum != dataToFind.BirthNum)
            //    {
            //        Console.ForegroundColor = ConsoleColor.Red;
            //        Console.WriteLine("Nepodarilo sa najst prvok - TEST ZLYHAL - Find()");
            //        Console.ResetColor();
            //        return false;
            //    }
            //}

            for (int i = 0; i < numberOfOperations; i++)
            {
                for (int j = 0; j < step; j++)
                {
                    data = GetPatient();
                    if (hash.Insert(data))
                        listofPatients.Add(data);
                }
                // 0=insert 1=find 2=delete
                operationNumber = rand.Next(0, 3);
                if (operationNumber == 0)
                {
                    data = GetPatient();
                    stopwatchInsert.Start();
                    result = hash.Insert(data);
                    stopwatchInsert.Stop();
                    numberOfInsert++;
                    if (result || hash.GetType() == typeof(DynamicHashing<Patient>))
                    {
                        listofPatients.Add(data);
                        var patientToCompare = hash.Find(data);
                        if (patientToCompare == null || !patientToCompare.MyEquals(data))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Nepodarilo sa pridat prvok - TEST ZLYHAL - Insert()");
                            Console.ResetColor();
                            return false;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Nepodarilo sa pridat prvok, pretoze je uz plny blok");
                        Console.ResetColor();
                    }
                    if (checkAllDataAfterEachOperation)
                    {
                        checkAllDataInFile(listofPatients, "|ERROR| - Kontrola vsetkych dat - Pri operaci INSERT() nastala chyba!");
                    }
                }
                else if (operationNumber == 1)
                {
                    rndIndex = rand.Next(0, listofPatients.Count);
                    var dataToFind = listofPatients[rndIndex];
                    stopwatchFind.Start();
                    var item = hash.Find(dataToFind);
                    stopwatchFind.Stop();
                    numberOfFind++;
                    if (item == null || item.MyEquals(dataToFind))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Nepodarilo sa najst prvok - TEST ZLYHAL - Find()");
                        Console.ResetColor();
                        return false;
                    }
                    if (checkAllDataAfterEachOperation)
                    {
                        checkAllDataInFile(listofPatients, "|ERROR| - Kontrola vsetkych dat - Pri operaci FIND() nastala chyba!");
                    }
                }
                else if (operationNumber == 2)
                {
                    rndIndex = rand.Next(0, listofPatients.Count);
                    if (listofPatients.Count == 0)
                    {
                        data = GetPatient();
                        if (hash.Insert(data))
                            listofPatients.Add(data);
                    }
                    var dataToDelete = listofPatients[rndIndex];
                    stopwatchDelete.Start();
                    result = hash.Delete(dataToDelete);
                    stopwatchDelete.Stop();
                    numberOfDelete++;
                    listofPatients.RemoveAt(rndIndex);
                    var resultData = hash.Find(dataToDelete);
                    if (resultData != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Nepodarilo sa vymazat prvok - TEST ZLYHAL - Delete()");
                        Console.ResetColor();
                        return false;
                    }
                    if (checkAllDataAfterEachOperation)
                    {
                        checkAllDataInFile(listofPatients, "|ERROR| - Kontrola vsetkych dat - Pri operaci DELETE() nastala chyba!");
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Vsetky testy prebehli uspesne");
            Console.WriteLine("Priemerny cas pre insert: " + stopwatchInsert.ElapsedMilliseconds / (double)numberOfInsert + " ms");
            Console.WriteLine("Priemerny cas pre find: " + stopwatchFind.ElapsedMilliseconds / (double)numberOfFind + " ms");
            Console.WriteLine("Priemerny cas pre delete: " + stopwatchDelete.ElapsedMilliseconds / (double)numberOfDelete + " ms");
            Console.ResetColor();
            hash.DisposeAndCloseFile();
            return true;
        }
    }
}
