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
        public Test(int blockFactor, int blockCount)
        {
            if (File.Exists(@"TESTING"))
            {
                File.Delete(@"TESTING");
            }
            hash = new Hashing<Patient>("TESTING", blockFactor, blockCount);
        }

        private Patient getRndPatient()
        {
            while (true)
            {
                var birthDate = generator.GetRandomDate(DateTime.Now.AddYears(-100), DateTime.Now);
                var birthNum = generator.getBirthNum(birthDate);
                if (!setOfBirthNums.Add(birthNum))
                    continue;
                var name = generator.GetRandomName();
                var surename = generator.GetRandomSurname();
                return new Patient(name, surename, birthNum, birthDate, BitConverter.GetBytes(rand.Next(255))[0]);
            }
        }

        public bool runTest(int initialSize, int numberOfOperations, int step = 0)
        {
            Console.WriteLine("START TESTU - testujeme Static hash file o inicializacnej velkosti " + initialSize +
                " pocet operacii: " + numberOfOperations);
            Stopwatch stopwatchInsert = new Stopwatch();
            Stopwatch stopwatchFind = new Stopwatch();
            Stopwatch stopwatchDelete = new Stopwatch();
            var listofPatients = new List<Patient>(10000);
            Console.WriteLine("Zacinam naplnovat subor nahodnymi datami");
            while (listofPatients.Count != initialSize)
            {
                var patient = getRndPatient();
                listofPatients.Add(patient);
                bool success = hash.Insert(patient);
                Patient? patient2 = hash.Find(patient);
                if (!success || patient2 == null || patient2.BirthNum != patient.BirthNum)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nepodarilo sa pridat prvok - TEST ZLYHAL - Insert()");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("Subor je naplneny na inicializacnu velkost, zacinam testovat operacie");

            int numberOfInsert = 0;
            int numberOfFind = 0;
            int numberOfDelete = 0;
            Patient data;
            int operationNumber = 0;
            bool result;
            int rndIndex = 0;
            for (int i = 0; i < numberOfOperations; i++)
            {
                for (int j = 0; j < step; j++)
                {
                    data = getRndPatient();
                    if (hash.Insert(data))
                        listofPatients.Add(data);
                }
                // 0=insert 1=find 2=delete
                operationNumber = rand.Next(0, 3);
                if (operationNumber == 0)
                {
                    data = getRndPatient();
                    stopwatchInsert.Start();
                    result = hash.Insert(data);
                    stopwatchInsert.Stop();
                    numberOfInsert++;
                    if (result)
                    {
                        listofPatients.Add(data);
                        var patientToCompare = hash.Find(data);
                        if (patientToCompare == null || patientToCompare.BirthNum != data.BirthNum)
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
                }
                else if (operationNumber == 1)
                {
                    rndIndex = rand.Next(0, listofPatients.Count);
                    var dataToFind = listofPatients[rndIndex];
                    stopwatchFind.Start();
                    var item = hash.Find(dataToFind);
                    stopwatchFind.Stop();
                    numberOfFind++;
                    if (item == null || item.BirthNum != dataToFind.BirthNum)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Nepodarilo sa najst prvok - TEST ZLYHAL - Find()");
                        Console.ResetColor();
                        return false;
                    }
                }
                else if (operationNumber == 2)
                {
                    rndIndex = rand.Next(0, listofPatients.Count);
                    if (listofPatients.Count == 0)
                    {
                        data = getRndPatient();
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
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Vsetky testy prebehli uspesne");
            Console.WriteLine("Priemerny cas pre insert: " + stopwatchInsert.ElapsedMilliseconds / (double)numberOfInsert + " ms");
            Console.WriteLine("Priemerny cas pre find: " + stopwatchFind.ElapsedMilliseconds / (double)numberOfFind + " ms");
            Console.WriteLine("Priemerny cas pre delete: " + stopwatchDelete.ElapsedMilliseconds / (double)numberOfDelete + " ms");
            Console.ResetColor();
            return true;
        }
    }
}
