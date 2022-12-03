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
    internal class Patient: IData<Patient>
    {
        private const int MAX_NAME_LENGHT = 15;
        private const int MAX_SURENAME_LENGTH = 20;
        private const int MAX_BIRTHNUM_LENGHT = 10;
        private const int MAX_NUMBER_OF_HOSPITALIZATION = 10;
        public string Name { get; set; }
        public string Surename { get; set; }
        public string BirthNum { get; set; }
        public DateTime BirthDate { get; set; }
        public byte HelathInsuranceCode { get; set; }
        public Hospitalization?[] Hospitalizations { get; set; }
        private int _actualNumberOfHospitalizations = 0;
        private int _actualLengthOfname;
        private int _actualLengthOfsurename;
        private int _actualLengthOfbirthnumber;


        public Patient()
        {
            Hospitalizations = new Hospitalization[MAX_NUMBER_OF_HOSPITALIZATION];
            //Array.Fill(Hospitalizations, new Hospitalization());
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
        public Patient(string birthNum)
        {
            Hospitalizations = new Hospitalization[MAX_NUMBER_OF_HOSPITALIZATION];
            //Array.Fill(Hospitalizations, new Hospitalization());
            for (int i = 0; i < MAX_NUMBER_OF_HOSPITALIZATION; i++)
            {
                Hospitalizations[i] = new Hospitalization();
            }
            Name = "";
            Surename = "";
            BirthNum = birthNum;
            BirthDate = DateTime.Now;
            HelathInsuranceCode = 0;
        }
        public Patient(string name, string surename, string birthnum, byte insuranceCompanyCode)
        {
            if (name.Length > MAX_NAME_LENGHT ||
                surename.Length > MAX_SURENAME_LENGTH ||
                birthnum.Length > MAX_BIRTHNUM_LENGHT)
                throw new ArgumentException("Parameters are too long");
            else
            {
                Hospitalizations = new Hospitalization[MAX_NUMBER_OF_HOSPITALIZATION];
                //Array.Fill(Hospitalizations, new Hospitalization());
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
                BirthDate = DateTime.Now;
            }
        }

        public bool AddHospitalization(Hospitalization hosp)
        {
            if (_actualNumberOfHospitalizations < MAX_NUMBER_OF_HOSPITALIZATION)
            {
                Hospitalizations[_actualNumberOfHospitalizations] = hosp;
                _actualNumberOfHospitalizations++;
                return true;
            }
            return false;
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

        // [<dlzka mena>, <dlzka priezviska>, <dlzka rod. cisla>, <pocet hospitalizacii>, <kod poistovne>, <datum narodenia - long ticks>, <meno>, <priezvisko>, <rod cislo>, ...HOSPITALIZACIE]
        public byte[] ToByteArray()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(_actualLengthOfname);
                    writer.Write(_actualLengthOfsurename);
                    writer.Write(_actualLengthOfbirthnumber);
                    writer.Write(_actualNumberOfHospitalizations);
                    writer.Write(HelathInsuranceCode);
                    writer.Write(BirthDate.Ticks);
                    writer.Write(Name + string.Concat(Enumerable.Repeat("0", MAX_NAME_LENGHT - Name.Length)));
                    writer.Write(Surename + string.Concat(Enumerable.Repeat("0", MAX_SURENAME_LENGTH - Surename.Length)));
                    writer.Write(BirthNum + string.Concat(Enumerable.Repeat("0", MAX_BIRTHNUM_LENGHT - BirthNum.Length)));
                    foreach (var hosp in Hospitalizations)
                    {
                        writer.Write(hosp.Id);
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
                    this._actualNumberOfHospitalizations = reader.ReadInt32();
                    this.HelathInsuranceCode = reader.ReadByte();
                    this.BirthDate = new DateTime(reader.ReadInt64());
                    this.Name = reader.ReadString().Substring(0, _actualLengthOfname);
                    this.Surename = reader.ReadString().Substring(0, _actualLengthOfsurename);
                    this.BirthNum = reader.ReadString().Substring(0, _actualLengthOfbirthnumber);
                    for (int i = 0; i < MAX_NUMBER_OF_HOSPITALIZATION; i++)
                    {
                        Hospitalizations[i].Id = reader.ReadInt32();
                        Hospitalizations[i].startDate = new DateTime(reader.ReadInt64());
                        Hospitalizations[i].endDate = new DateTime(reader.ReadInt64());
                        Hospitalizations[i].ActualDiagnosisLength = reader.ReadInt32();
                        Hospitalizations[i].Diagnosis = reader.ReadString().Substring(0, Hospitalizations[i].ActualDiagnosisLength);
                    }
                    //foreach (var hosp in this.Hospitalizations)
                    //{
                    //    hosp.Id = reader.ReadInt32();
                    //    hosp.startDate = new DateTime(reader.ReadInt64());
                    //    hosp.endDate = new DateTime(reader.ReadInt64());
                    //    hosp.ActualDiagnosisLength = reader.ReadInt32();
                    //    hosp.Diagnosis = reader.ReadString().Substring(0, hosp.ActualDiagnosisLength);
                    //}
                }
            }
        }

        // v kazdom stringu sa uklada o jeden bajt navyse, pretoze BinaryReader uklada aj dlzku stringu
        public int GetSize()
        {
            return (MAX_BIRTHNUM_LENGHT + MAX_NAME_LENGHT + MAX_SURENAME_LENGTH) + 3
                + sizeof(int) * 4 + sizeof(byte) + sizeof(long) + MAX_NUMBER_OF_HOSPITALIZATION * Hospitalization.Size; 
        }

        public override string ToString()
        {
            string result = $"Pacient {Name} {Surename}, rodne cislo: {BirthNum},narodeny: {BirthDate} poistovna: {HelathInsuranceCode} , Hospitalizacie:\n";
            foreach (var hosp in Hospitalizations)
            {
                if(hosp.Id != 0)
                    result += hosp.ToString() + "\n\t";
            }
            return result;
        }

        public string getFileRepresentation()
        {
            return $"{Name};{Surename};{BirthNum}";
        }

        public Hospitalization? GetHospitalizationById(int id)
        {
            for (int i = 0; i < Hospitalizations.Length; i++)
            {
                if (Hospitalizations[i].Id == id)
                    return Hospitalizations[i];
            }
            return null;
        }

        public bool TryToAddHospitalization(Hospitalization hosp)
        {
            for (int i = 0; i < Hospitalizations.Length; i++)
            {
                if (Hospitalizations[i].Id == 0)
                {
                    hosp.Id = i+1;
                    Hospitalizations[i] = hosp;
                    return true;
                }
            }
            return false;
        }

        public bool TryToEndHospitalization(DateTime end)
        {
            for (int i = 0; i < Hospitalizations.Length; i++)
            {
                if (Hospitalizations[i].Id != 0 && Hospitalizations[i].endDate == DateTime.MaxValue)
                {
                    Hospitalizations[i].endDate = end;
                    return true;          
                }
            }
            return false;
        }

        public bool TryToDeleteHospitalization(int id)
        {
            for (int i = 0; i < Hospitalizations.Length; i++)
            {
                if (Hospitalizations[i].Id == id)
                {
                    Hospitalizations[i].Id = 0;
                    return true;
                }
            }
            return false;
        }

    }
}
