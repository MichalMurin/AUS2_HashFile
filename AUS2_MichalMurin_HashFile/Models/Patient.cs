using AUS2_MichalMurin_HashFile.DataStructures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AUS2_MichalMurin_HashFile.Models
{
    public class Patient: IData<Patient>
    {
        private const int MAX_NAME_LENGHT = 15;
        private const int MAX_SURENAME_LENGTH = 20;
        private const int MAX_BIRTHNUM_LENGHT = 10;
        private const int MAX_NUMBER_OF_HOSPITALIZATION = 10;
        internal string Name { get; set; }
        internal string Surename { get; set; }
        internal string BirthNum { get; set; }
        internal DateTime BirthDate { get; set; }
        internal byte HelathInsuranceCode { get; set; }
        internal Hospitalization?[] Hospitalizations { get; set; }
        private int _actualLengthOfname;
        private int _actualLengthOfsurename;
        private int _actualLengthOfbirthnumber;


        public Patient()
        {
            Hospitalizations = new Hospitalization[MAX_NUMBER_OF_HOSPITALIZATION];
            for (int i = 0; i < MAX_NUMBER_OF_HOSPITALIZATION; i++)
            {
                Hospitalizations[i] = new Hospitalization();
            }
            Name = "";
            Surename = "";
            BirthNum = "";
            BirthDate = DateTime.Now;
            HelathInsuranceCode = 0;
        }
        internal Patient(string birthNum)
        {
            Hospitalizations = new Hospitalization[MAX_NUMBER_OF_HOSPITALIZATION];
            for (int i = 0; i < MAX_NUMBER_OF_HOSPITALIZATION; i++)
            {
                Hospitalizations[i] = new Hospitalization();
            }
            Name = "";
            Surename = "";
            BirthNum = birthNum;
            BirthDate = GetBirthDate(birthNum);
            HelathInsuranceCode = 0;
        }
        internal Patient(string name, string surename, string birthnum, byte insuranceCompanyCode)
        {
            if (name.Length > MAX_NAME_LENGHT ||
                surename.Length > MAX_SURENAME_LENGTH ||
                birthnum.Length > MAX_BIRTHNUM_LENGHT)
                throw new ArgumentException("Parameters are too long");
            else
            {
                Hospitalizations = new Hospitalization[MAX_NUMBER_OF_HOSPITALIZATION];
                for (int i = 0; i < MAX_NUMBER_OF_HOSPITALIZATION; i++)
                {
                    Hospitalizations[i] = new Hospitalization();
                }
                Name = name;
                Surename = surename;
                BirthNum = birthnum;
                HelathInsuranceCode = insuranceCompanyCode;
                _actualLengthOfname = Name.Length;
                _actualLengthOfsurename = Surename.Length;
                _actualLengthOfbirthnumber = BirthNum.Length;
                BirthDate = GetBirthDate(birthnum);
            }
        }

        private DateTime GetBirthDate(string birthnum)
        {
            if(birthnum.Length > 6)
            {
                string yearstr = birthnum.Substring(0, 2);
                string monthstr = birthnum.Substring(2, 2);
                string daystr = birthnum.Substring(4, 2);
                int year, month, day;
                int.TryParse(yearstr, out year);
                int.TryParse(monthstr, out month);
                int.TryParse(daystr, out day);
                year += 2000;
                if (year > DateTime.Now.Year)
                    year -= 100; 
                if(month > 50)
                    month -= 50;
                if(day > 0 && day <=31 && month > 0  && month <= 12 && year > 0)
                    return new DateTime(year, month, day);
            }
            return DateTime.MinValue;
        }

        public BitArray GetHash()
        {
            return new BitArray(Encoding.Default.GetBytes(BirthNum));
        }

        public bool MyEquals(Patient data)
        {
            return this.BirthNum == data.BirthNum;
        }

        public Patient CreateClass()
        {
            return new Patient();
        }

         public byte[] ToByteArray()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(_actualLengthOfname);
                    writer.Write(_actualLengthOfsurename);
                    writer.Write(_actualLengthOfbirthnumber);
                    writer.Write(HelathInsuranceCode);
                    writer.Write(BirthDate.Ticks);
                    writer.Write(Name + string.Concat(Enumerable.Repeat("0", MAX_NAME_LENGHT - Name.Length)));
                    writer.Write(Surename + string.Concat(Enumerable.Repeat("0", MAX_SURENAME_LENGTH - Surename.Length)));
                    writer.Write(BirthNum + string.Concat(Enumerable.Repeat("0", MAX_BIRTHNUM_LENGHT - BirthNum.Length)));
                    foreach (var hosp in Hospitalizations)
                    {
                        writer.Write(hosp!.Id);
                        writer.Write(hosp.startDate.Ticks);
                        writer.Write(hosp.endDate.Ticks);
                        writer.Write(hosp.ActualDiagnosisLength);
                        writer.Write(hosp.Diagnosis + string.Concat(Enumerable.Repeat("0", Hospitalization.MaxDiagnosisLenght - hosp.Diagnosis.Length)));
                    }
                }
                return stream.ToArray();
            }
        }
        public void FromByteArray(byte[] pArray)
        {
            using (MemoryStream stream = new MemoryStream(pArray))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    this._actualLengthOfname = reader.ReadInt32();
                    this._actualLengthOfsurename = reader.ReadInt32();
                    this._actualLengthOfbirthnumber = reader.ReadInt32();
                    this.HelathInsuranceCode = reader.ReadByte();
                    this.BirthDate = new DateTime(reader.ReadInt64());
                    this.Name = reader.ReadString().Substring(0, _actualLengthOfname);
                    this.Surename = reader.ReadString().Substring(0, _actualLengthOfsurename);
                    this.BirthNum = reader.ReadString().Substring(0, _actualLengthOfbirthnumber);
                    for (int i = 0; i < MAX_NUMBER_OF_HOSPITALIZATION; i++)
                    {
                        Hospitalizations[i]!.Id = reader.ReadInt32();
                        Hospitalizations[i]!.startDate = new DateTime(reader.ReadInt64());
                        Hospitalizations[i]!.endDate = new DateTime(reader.ReadInt64());
                        Hospitalizations[i]!.ActualDiagnosisLength = reader.ReadInt32();
                        Hospitalizations[i]!.Diagnosis = reader.ReadString().Substring(0, Hospitalizations[i]!.ActualDiagnosisLength);
                    }                    
                }
            }
        }

        // v kazdom stringu sa uklada o jeden bajt navyse, pretoze BinaryReader uklada aj dlzku stringu
        public int GetSize()
        {
            return (MAX_BIRTHNUM_LENGHT + MAX_NAME_LENGHT + MAX_SURENAME_LENGTH) + 3
                + sizeof(int) * 3 + sizeof(byte) + sizeof(long) + MAX_NUMBER_OF_HOSPITALIZATION * Hospitalization.Size; 
        }

        public List<string> GetStrings()
        {
            List<string> result = new List<string>();
            result.Add($"Pacient {Name} {Surename}, rodne cislo: {BirthNum},narodeny: {BirthDate.ToString("dd.MM.yyyy")} poistovna: {HelathInsuranceCode}");
            result.Add(" Hospitalizacie:");
            foreach (var hosp in Hospitalizations)
            {
                if (hosp!.Id != 0)
                    result.AddRange(hosp.GetStrings());
            }
            return result;
        }

        internal Hospitalization? GetHospitalizationById(int id)
        {
            for (int i = 0; i < Hospitalizations.Length; i++)
            {
                if (Hospitalizations[i]!.Id == id)
                    return Hospitalizations[i];
            }
            return null;
        }

        internal bool TryToAddHospitalization(Hospitalization hosp)
        {
            if (IsActuallyHospitalized())
                return false;
            for (int i = 0; i < Hospitalizations.Length; i++)
            {
                if (Hospitalizations[i]!.Id == 0)
                {
                    hosp.Id = i+1;
                    Hospitalizations[i] = hosp;
                    return true;
                }
            }
            return false;
        }

        internal bool IsActuallyHospitalized()
        {
            for (int i = 0; i < Hospitalizations.Length; i++)
            {
                if (Hospitalizations[i]!.Id != 0 && Hospitalizations[i]!.endDate == DateTime.MaxValue)
                {
                    return true;
                }
            }
            return false;
        }
        internal bool TryToEndHospitalization(DateTime end)
        {
            for (int i = 0; i < Hospitalizations.Length; i++)
            {
                if (Hospitalizations[i]!.Id != 0 && Hospitalizations[i]!.endDate == DateTime.MaxValue)
                {
                    Hospitalizations[i]!.endDate = end;
                    return true;          
                }
            }
            return false;
        }

        internal bool TryToDeleteHospitalization(int id)
        {
            for (int i = 0; i < Hospitalizations.Length; i++)
            {
                if (Hospitalizations[i]!.Id == id)
                {
                    Hospitalizations[i]!.Id = 0;
                    return true;
                }
            }
            return false;
        }

    }
}
