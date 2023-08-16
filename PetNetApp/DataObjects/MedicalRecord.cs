using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class MedicalRecord
    {
        public int MedicalRecordId { get; set; }
        public int AnimalId { get; set; }
        public DateTime Date { get; set; }
        public string MedicalNotes { get; set; }
        public bool IsProcedure { get; set; }
        public bool IsTest { get; set; }
        public bool IsVaccination { get; set; }
        public bool IsPrescription { get; set; }
        public bool Images { get; set; }
        public bool QuarantineStatus { get; set; }
        public string Diagnosis { get; set; }
    }

    public class MedicalRecordVM : MedicalRecord
    {
        public ProcedureVM Procedure { get; set; }
        public TestVM Test { get; set; }
        public VaccinationVM Vaccination { get; set; }
        public PrescriptionVM Prescription { get; set; }
        public List<Images> AnimalImages { get; set; }
    }
}
