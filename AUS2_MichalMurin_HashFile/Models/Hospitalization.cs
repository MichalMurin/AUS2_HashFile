using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUS2_MichalMurin_HashFile.Models
{
    internal class Hospitalization
    {
        internal static int MaxDiagnosisLenght { get
            {
                return 20;
            } 
        }
        internal static int Size
        {
            get
            {
                return sizeof(int) * 2 + sizeof(long) * 2 + MaxDiagnosisLenght + 1;
            }
        }
        internal int ActualDiagnosisLength { get; set; }
        internal int Id { get; set; }
        internal DateTime startDate { get; set; }
        internal DateTime endDate { get; set; }
        internal string Diagnosis { get; set; }

        internal Hospitalization(string diagnosis, DateTime startDate, DateTime endDate)
        {
            Id = 0;
            Diagnosis = diagnosis;
            this.startDate = startDate;
            this.endDate = endDate;
            ActualDiagnosisLength = Diagnosis.Length;
        }

        internal Hospitalization()
        {
            Id = 0;
            startDate = DateTime.MinValue;
            endDate = DateTime.MaxValue;
            Diagnosis = "";
            ActualDiagnosisLength = Diagnosis.Length;
        }

        public override string ToString()
        {
            return $"\tID: {Id}, zaciatok: {startDate.ToString("dd.MM.yyyy")}, koniec: {endDate.ToString("dd.MM.yyyy")} \t diagnoza: {Diagnosis}";
        }

        public List<string> GetStrings()
        {
            List<string> result = new List<string>();
            result.Add($"\tID: {Id}, zaciatok: {startDate.ToString("dd.MM.yyyy")}, koniec: {endDate.ToString("dd.MM.yyyy")}");
            result.Add($"\t\tdiagnoza: {Diagnosis}");
            return result;
        }
    }
}
