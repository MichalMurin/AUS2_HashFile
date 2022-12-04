using AUS2_MichalMurin_HashFile.DataStructures;
using AUS2_MichalMurin_HashFile.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUS2_MichalMurin_HashFile.Models
{
    internal class HealthCard
    {
        public Hashing<Patient> Patients { get; set; }
        private static string _dataFilePath = "PATIENTS";

        internal HealthCard(HashType type, int blockFactor, int blockCount=-1)
        {
            DeleteData();
            if (type == HashType.StaticHash)
            {
                Patients = new StaticHashing<Patient>(_dataFilePath, blockFactor, blockCount);
            }
            else
            {
                 Patients = new DynamicHashing<Patient>(_dataFilePath, blockFactor);
            }
        }

        // Konstruktor ktory nataha data zo suboru
        // Na vykonanie tohto konstruktora sa dostaneme vzdy ked vieme, ze config subory naozaj existuju
        internal HealthCard(HashType type)
        {
            var baseData = Hashing<Patient>.LoadBaseDataFromFile();
            if (type == HashType.StaticHash)
            {
                var blockCount = StaticHashing<Patient>.LoadStaticDataFromFile();
                Patients = new StaticHashing<Patient>(baseData.Item2, baseData.Item1, blockCount);
            }
            else
            {
                var dynamicData = DynamicHashing<Patient>.LoadDynamicDataFromFile();
                Patients = new DynamicHashing<Patient>(dynamicData.Item1, dynamicData.Item2, baseData.Item2, baseData.Item1);
            }
        }



        /// <summary>
        /// metoda na generovanie nahodnych dat
        /// </summary>
        /// <param name="patientsNumber">pocet pacientov</param>
        internal void generateRandomData(int patientsNumber)
        {
            Patient patient;
            DataGenerator gen = new DataGenerator();
            HashSet<string> setOfBirthNums = new HashSet<string>();
            for (int i = 0; i < patientsNumber; i++)
            {
                while(true)
                {
                    patient = gen.getRndPatient();
                    if(setOfBirthNums.Add(patient.BirthNum))
                        break;
                }
                Patients.Insert(patient);
            }
        }

        //1
        /// <summary>
        /// Vyhľadanie záznamov pacienta (identifikovaný svojím rodným číslom). Po nájdení
        /// pacienta je potrebné zobraziť všetky evidované údaje vrátane všetkých hospitalizácií
        /// </summary>
        /// <param name="birthNum">rodne cislo</param>
        /// <returns>data</returns>
        internal (bool, List<string>?) GetPatientsData(string birthNum)
        {
            var patient = Patients.Find(new Patient(birthNum));
            if (patient != null)
                return (true, patient.GetStrings());
            else
                return (false, null);
        }

        //2
        /// <summary>
        /// vyhľadanie záznamov pacienta (identifikovaný svojím rodným číslom) v zadanej
        /// nemocnici(identifikovaná svojím názvom). Po nájdení pacienta je potrebné zobraziť
        /// všetky evidované údaje.
        /// </summary>
        /// <param name="patientStr">string pacienta</param>
        /// <param name="hospitalName">nazov nemocnice</param>
        /// <returns>zoznam hospitalizacii pacienta</returns>
        internal (bool, List<string>?) getHospitalization(string birthNum, int id)
        {
            var patient = Patients.Find(new Patient(birthNum));
            if (patient != null)
            {
                var hosp = patient.GetHospitalizationById(id);
                if (hosp == null)
                    return (false, null);
                else
                    return (true, hosp.GetStrings());
            }
            else
                return (false, null);
        }

        //3
        /// <summary>
        /// vykonanie záznamu o začiatku hospitalizácie pacienta (identifikovaný svojím rodným
        /// číslom) v nemocnici(identifikovaná svojím názvom)
        /// </summary>
        /// <param name="patientStr">string pacienta</param>
        /// <param name="hospitalName">nazov nemocnice</param>
        /// <param name="diagnosis">diagnoza</param>
        /// <param name="start">zaciatok</param>
        /// <param name="end">koniec</param>      
        /// <returns>true ak sa podarilo pridat nemocnicu</returns>
        internal bool addHospitalization(string birthNum, string diagnosis, DateTime start, DateTime end)
        {
            var hosp = new Hospitalization(diagnosis,start, end);
            var patient = Patients.Find(new Patient(birthNum));
            if (patient != null)
            {
                if (patient.TryToAddHospitalization(hosp))
                {
                    Patients.UpdateData(patient);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        //4
        /// <summary>
        /// vykonanie záznamu o ukončení hospitalizácie pacienta (identifikovaný svojím rodným
        /// číslom) v nemocnici(identifikovaná svojím názvom)
        /// </summary>
        /// <param name="patientStr">retazec pacienta</param>
        /// <param name="hospitalName">nazov nemocnice</param>
        /// <param name="endDate">datum ukoncenia</param>
        /// <returns>true ak sa podarilo ukoncit hospitalizaciu</returns>
        internal bool addEndOfHospitalization(string birthNum, DateTime endDate)
        {
            var patient = Patients.Find(new Patient(birthNum));
            if (patient != null)
            {
                if (patient.TryToEndHospitalization(endDate))
                {
                    Patients.UpdateData(patient);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        //6
        /// <summary>
        /// pridanie pacienta
        /// </summary>
        /// <param name="name">meno</param>
        /// <param name="surname">priezvisko</param>
        /// <param name="birthNumber">rodne cislo</param>
        /// <param name="insuranceCompanyCode">kod poistovne</param>
        /// <returns>true ak sa podarilo pridat pacienta</returns>
        internal bool addPatient(string name, string surname, string birthNumber, byte insuranceCompanyCode)
        {
            Patient patient = new Patient(name, surname, birthNumber, insuranceCompanyCode);
            return Patients.Insert(patient);
        }

        internal bool deletePatient(string birthNum)
        {
            return Patients.Delete(new Patient(birthNum));
        }

        internal bool deleteHospitalization(string birthNum, int idHosp)
        {
            var patient = Patients.Find(new Patient(birthNum));
            if (patient != null)
            {
                if (patient.TryToDeleteHospitalization(idHosp))
                {
                    Patients.UpdateData(patient);
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public List<string> SequencePrint()
        {
            return Patients.GetSequenceOfBlocks();
        }

        public static (bool, HashType?) FindConfigFiles()
        {
            if (File.Exists(_dataFilePath) && File.Exists(Hashing<Patient>._pathToBaseData))
            {
                if (File.Exists(DynamicHashing<Patient>._pathForTrieData) && File.Exists(DynamicHashing<Patient>._pathForEmptyBlocksData))
                {
                    return (true, HashType.DynamicHash);
                }
                if (File.Exists(StaticHashing<Patient>._pathForStaticFileData))
                {
                    return (true, HashType.StaticHash);
                }
            }
            return (false, null);
        }

        public void DeleteData()
        {
            if(Patients != null)
                Patients.DisposeAndCloseFile(); 
            if (File.Exists(_dataFilePath))
            {
                File.Delete(_dataFilePath);
            }
            if (File.Exists(DynamicHashing<Patient>._pathForTrieData))
            {
                File.Delete(DynamicHashing<Patient>._pathForTrieData);
            }
            if (File.Exists(DynamicHashing<Patient>._pathForEmptyBlocksData))
            {
                File.Delete(DynamicHashing<Patient>._pathForEmptyBlocksData);
            }
            if (File.Exists(StaticHashing<Patient>._pathForStaticFileData))
            {
                File.Delete(StaticHashing<Patient>._pathForStaticFileData);
            }
            if (File.Exists(Hashing<Patient>._pathToBaseData))
            {
                File.Delete(Hashing<Patient>._pathToBaseData);
            }
        }

        public void Save()
        {
            Patients.SaveBaseDataToFile();
            Patients.ExportAppDataToFile();
        }
    }
}
