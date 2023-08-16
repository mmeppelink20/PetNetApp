using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public int MedicalRecordId { get; set; }
        public int UserId { get; set; }
        public string PrescriptionTypeId { get; set; }
        public string PrescriptionName { get; set; }
        public string PrescriptionDosage { get; set; }
        public string PrescriptionFrequency { get; set; }
        public int PrescriptionDuration { get; set; }
        public string PrescriptionNotes { get; set; }
        public DateTime DatePrescribed { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class PrescriptionVM : Prescription
    {
        public string PerscriberGivenName { get; set; }
        public string PerscriberFamilyName { get; set; }

    }
}
