using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AUS2_MichalMurin_HashFile.Models;
using AUS2_MichalMurin_HashFile.Service;

namespace AUS2_MichalMurin_HashFile.Presenter
{
    public class Presenter
    {
        /// <summary>
        /// instancia zdavotnej karty
        /// </summary>
        private HealthCard healthCard { get; set; }
        /// <summary>
        /// Konstruktor triedy
        /// </summary>
        public Presenter(HashType type, int blockFactor, int blockCount = -1)
        {
            healthCard = new HealthCard(type, blockFactor, blockCount);
        }
        /// <summary>
        /// Beyparametricky konstruktor
        /// </summary>
        public Presenter()
        {
        }

        /// <summary>
        /// Metoda, ktora sa pokusi inicializvoat zdravotnu kartu zo suboru
        /// </summary>
        /// <returns></returns>
        public bool TryToInitializeFromFile()
        {
            var result = HealthCard.FindConfigFiles();
            if (result.Item1)
            {
                healthCard = new HealthCard((HashType)result.Item2!);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Metoda ktora incializuje zdravotnu kartu na zaklade vstupnych parametrov
        /// </summary>
        /// <param name="type">typ hesovania</param>
        /// <param name="blockFactor">blokovaci faktor</param>
        /// <param name="blockCount">pocet blokov v subore</param>
        public void InitializeHealthCard(HashType type, int blockFactor, int blockCount = -1)
        {
            if (healthCard == null)
            {
                healthCard = new HealthCard(type, blockFactor, blockCount);
            }
        }

        /// <summary>
        /// metoda na generovanie dat
        /// </summary>
        /// <param name="patientsNumber">pocet pacientov</param>
        /// <param name="hospitalNumber">pocet nemocnic</param>
        /// <param name="hospitalizationNumber">pocet hospitalizacii</param>
        public void generateData(int patientsNumber)
        {
            healthCard.generateRandomData(patientsNumber);
        }

        /// <summary>
        /// Metoda na ziskanie dat pacienta
        /// </summary>
        /// <param name="birthNum">rodne cislo</param>
        /// <returns>zoznam retazcov reprezentujuce data pacienta</returns>
        public (bool, List<string>?) GetPatientData(string birthNum)
        {
            return healthCard.GetPatientsData(birthNum);
        }

        /// <summary>
        /// metoda na pridanie noveho pacienta
        /// </summary>
        /// <param name="pName">meno</param>
        /// <param name="pSurname">priezvisko</param>
        /// <param name="pBirthNumber">rodne cislo</param>
        /// <param name="pPnsuranceCompany">poistovna</param>
        /// <returns>true ak sa pridanie podarilo</returns>
        public bool addPatient(string pName, string pSurname, string pBirthNumber, byte pPnsuranceCompany)
        {
            return healthCard.addPatient(pName, pSurname, pBirthNumber, pPnsuranceCompany);
        }

        /// <summary>
        /// metoda na pridanie hospitalizacie
        /// </summary>
        /// <param name="patientStr">string pacienta</param>
        /// <param name="diagnosis">diagnoza</param>
        /// <param name="start">zaciatok</param>
        /// <param name="end">koniec (optional)</param>
        /// <returns></returns>
        public bool addHospitalization(string patientStr, string diagnosis, DateTime start, DateTime end)
        {
            return healthCard.addHospitalization(patientStr, diagnosis, start, end);
        }

        /// <summary>
        /// metoda na ukoncenie hospitalizacie
        /// </summary>
        /// <param name="patientStr">string pacienta</param>
        /// <param name="end">koniec hospitalizacie</param>
        /// <returns>true ak sa pridanie podarilo</returns>
        public bool endHospitalization(string patientStr, DateTime end)
        {
            return healthCard.addEndOfHospitalization(patientStr, end);
        }
        /// <summary>
        /// Metoda na vymazanie hospitalizacie
        /// </summary>
        /// <param name="birthNum">rodne cislo</param>
        /// <param name="idHosp">id hospitalizacie</param>
        /// <returns>true ak sa mazanie podarilo</returns>
        public bool deleteHospitalization(string birthNum, int idHosp)
        {
            return healthCard.deleteHospitalization(birthNum, idHosp);
        }
        /// <summary>
        /// Metoda na ziskanie konkretnej hospitalizacie
        /// </summary>
        /// <param name="birthNum">rodne cislo</param>
        /// <param name="idHosp">id hospitalizacie</param>
        /// <returns></returns>
        public (bool, List<string>?) getHospitalization(string birthNum, int idHosp)
        {
            return healthCard.getHospitalization(birthNum, idHosp);
        }
        /// <summary>
        /// Metoda na vymazanie pacienta
        /// </summary>
        /// <param name="birthNum">rodne cislo</param>
        /// <returns>true ak sa mazanie podarilo</returns>
        public bool deletePatient(string birthNum)
        {
            return healthCard.deletePatient(birthNum);
        }
        /// <summary>
        /// Metoda na ziskanie sekvencneho vypisu suboru
        /// </summary>
        /// <returns>Skenvencny vypis</returns>
        public List<string> GetSequenceData()
        {
            return healthCard.SequencePrint();
        }
        /// <summary>
        /// Metoda na vymazanie vsetkych dat
        /// </summary>
        public void DeleteAllFiles()
        {
            healthCard.DeleteData();
        }
        /// <summary>
        /// Metoda na ulozenie vsetkych dat
        /// </summary>
        public void SaveAllData()
        {
            healthCard.Save();
        }
    }
}
