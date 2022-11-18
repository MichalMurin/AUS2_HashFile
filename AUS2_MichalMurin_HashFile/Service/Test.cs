using AUS2_MichalMurin_HashFile.DataStructures;
using AUS2_MichalMurin_HashFile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AUS2_MichalMurin_HashFile.Service
{
    internal class Test
    {
        Hashing<Patient> hash;
        DataGenerator generator = new DataGenerator();
        Random rand = new Random();

        public Test()
        {
            if (File.Exists(@"TESTING"))
            {
                File.Delete(@"TESTING");
            }
            hash = new Hashing<Patient>("TESTING", 50, 1000);
        }

        public bool runTest()
        {
            var listofPatients = new List<Patient>(10000);
            while (listofPatients.Count != 10000)
            {
                var name = generator.GetRandomName();
                var surename = generator.GetRandomSurname();
                var birthDate = generator.GetRandomDate(DateTime.Now.AddYears(-100), DateTime.Now);
                var birthNum = generator.getBirthNum(birthDate);
                var patient = new Patient(name, surename, birthNum, birthDate, BitConverter.GetBytes(rand.Next(255))[0]);
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

            for (int i = 0; i < 1000; i++)
            {
                int index = rand.Next(listofPatients.Count);
                var tmp = listofPatients[index];
                Patient? patient = hash.Find(tmp);
                if (patient == null || patient.BirthNum != tmp.BirthNum)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nepodarilo sa najst prvok - TEST ZLYHAL - Find()");
                    Console.ResetColor();
                }
                else
                {
                    hash.Delete(patient);
                    var patient2 = hash.Find(patient);
                    if (patient2 != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Nepodarilo sa vymazat prvok - TEST ZLYHAL - Delete()");
                        Console.ResetColor();
                    }
                }
                listofPatients.RemoveAt(index);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("KONIEC TESTOVANIA!");
            Console.ResetColor();
            return true;
        }
    }
}
