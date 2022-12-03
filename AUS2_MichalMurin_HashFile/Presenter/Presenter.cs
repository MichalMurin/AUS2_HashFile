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
        /// metoda na generovanie dat
        /// </summary>
        /// <param name="patientsNumber">pocet pacientov</param>
        /// <param name="hospitalNumber">pocet nemocnic</param>
        /// <param name="hospitalizationNumber">pocet hospitalizacii</param>
        public void generateData(int patientsNumber)
        {
            healthCard.generateRandomData(patientsNumber);
        }

        
        public (bool, string?) GetPatientData(string birthNum)
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
        /// <returns></returns>
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
        /// <param name="hospitalName">nazov nemocnice</param>
        /// <param name="end">koniec hospitalizacie</param>
        /// <returns></returns>
        public bool endHospitalization(string patientStr, DateTime end)
        {
            return healthCard.addEndOfHospitalization(patientStr, end);
        }

        public bool deleteHospitalization(string birthNum, int idHosp)
        {
            return healthCard.deleteHospitalization(birthNum, idHosp);
        }

        public (bool, string?) getHospitalization(string birthNum, int idHosp)
        {
            return healthCard.getHospitalization(birthNum, idHosp);
        }

        public bool deletePatient(string birthNum)
        {
            return healthCard.deletePatient(birthNum);
        }

        public List<string> GetSequenceData()
        {
            return healthCard.SequencePrint();
        }

        public bool ArePresentAppConfigFiles()
        {
            return healthCard.FindConfigFiles();
        }
    }
}
