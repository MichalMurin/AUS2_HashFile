using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faker;
using AUS2_MichalMurin_HashFile.Models;
using System.Collections;
using System.Reflection.Emit;

namespace AUS2_MichalMurin_HashFile.Service
{
    /// <summary>
    /// Trieda sluziaca na generovanie nahodnych dat do databazy
    /// </summary>
    internal class DataGenerator
    {
        private Random r;
        internal DataGenerator()
        {
            r = new Random();
        }

        internal Patient getRndPatient()
        {
            var birthDate = GetRandomDate(DateTime.Now.AddYears(-100), DateTime.Now);
            var birthNum = getBirthNum(birthDate);
            var name = GetRandomName();
            var surename = GetRandomSurname();

            var patient = new Patient(name, surename, birthNum, BitConverter.GetBytes(r.Next(255))[0]);
            int numberOfHospitalizations = r.Next(patient.Hospitalizations.Length - 1);
            string diagnosis;
            DateTime start, end;
            int rndNum;
            for (int i = 0; i < numberOfHospitalizations; i++)
            {
                diagnosis = GetRandomDiagnosis();
                start = GetRandomDate(DateTime.Now.AddYears(-r.Next(1, 5)), DateTime.Now);
                rndNum = r.Next(0, 100) % 2;
                end = GetRandomDate(start, DateTime.Now);
                if (i == numberOfHospitalizations - 1)
                    end = DateTime.MaxValue; // poslednej hospitalizacii nastavime ze je este aktualna
                patient.Hospitalizations[i] = new Hospitalization(diagnosis, start, end);
                patient.Hospitalizations[i]!.Id = i+1;
            }
            return patient;
        }

        internal DateTime GetRandomDate(DateTime start, DateTime end)
        {
            int range = (end - start).Days;
            return start.AddDays(r.Next(range));
        }
        internal string GetRandomDiagnosis()
        {
            Random r = new Random();
            int stringlen = r.Next(4, 20);
            int randValue;
            string str = "";
            char letter;
            for (int i = 0; i < stringlen; i++)
            {
                // Generating a random number.
                randValue = r.Next(0, 26);

                // Generating random character by converting
                // the random number into character.
                letter = Convert.ToChar(randValue + 65);

                // Appending the letter to string.
                str = str + letter;
            }
            return str;
        }
        internal string GetRandomName()
        {
            return Name.First();
        }
        internal string GetRandomSurname()
        {
            return Name.Last();
        }
        internal string GetRandomCompanyName()
        {
            return Company.Name();
        }
        internal string GetRandomHospital()
        {
            return Address.City() + "-Hospital";
        }
        internal string getBirthNum(DateTime birthDate)
        {
            int rnd = r.Next(0, 100) % 2;
            var year = birthDate.Year.ToString();
            int monthInt = birthDate.Month;
            if (rnd == 0)
                monthInt += 50;
            var month = monthInt.ToString("00");
            var day = birthDate.Day.ToString("00");
            if (birthDate.Year < 1950)
                rnd = r.Next(100, 1000);
            else
                rnd = r.Next(1000, 10000);
            var str = year.Substring(2) + month + day + rnd;
            return str;
        }
    }
}

