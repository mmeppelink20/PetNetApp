/// <summary>
/// Tyler Hand 
/// Created: 2023/25/02
/// </summary>
/// <remarks>
/// Oleksiy Fedchuk
/// Updated : 2023/04/28
///  Final QA
/// </remarks>


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;

namespace DataAccessLayerFakes
{
    public class PrescriptionAccessorFakes : IPrescriptionAccessor
    {
        private int expectedPrescriptionIdForTest = 0;
        private int expectedMedicalRecordIdForTest = 0;
        private int expectedUserIdForTest = 0;
        private List<PrescriptionVM> _prescription = new List<PrescriptionVM>();
            public PrescriptionAccessorFakes()
            {
                 _prescription.Add(new PrescriptionVM
                 {
                     PrescriptionId = 111,
                     MedicalRecordId = 111,
                     UserId = 111,
                     PrescriptionFrequency = "Prescription Frequency ",
                     PrescriptionName = "medican",
                     PrescriptionDosage = "Prescription Dosage",
                     PrescriptionDuration = 30,
                     PrescriptionNotes = "Notes",
                     DatePrescribed = DateTime.Parse("2021-01-22"),
                     EndDate = DateTime.Parse("2021-01-22"),
                 });
                 _prescription.Add(new PrescriptionVM
                 {
                     PrescriptionId = 333,
                     MedicalRecordId = 333,
                     UserId = 333,
                     PrescriptionFrequency = "Prescription Frequency ",
                     PrescriptionName = "medican",
                     PrescriptionDosage = "Prescription Dosage",
                     PrescriptionDuration = 30,
                     PrescriptionNotes = "Notes",
                     DatePrescribed = DateTime.Parse("2021-01-22"),
                     EndDate = DateTime.Parse("2021-01-22"),
                 });
                 _prescription.Add(new PrescriptionVM
                 {
                     PrescriptionId = 222,
                     MedicalRecordId = 222,
                     UserId = 222,
                     PrescriptionFrequency = "Prescription Frequency ",
                     PrescriptionName = "medican",
                     PrescriptionDosage = "Prescription Dosage",
                     PrescriptionDuration = 30,
                     PrescriptionNotes = "Notes",
                     DatePrescribed = DateTime.Parse("2021-01-22"),
                     EndDate = DateTime.Parse("2021-01-22"),
                 });
            }
             private Dictionary<int, List<int>> AnimalIdToMedicalRecordIdRepresentation = new Dictionary<int, List<int>>()
             {
                 {5, new List<int>() { 111, 333, 222 }},
                 {8, new List<int>() { 333, 777, 222 }}
             };

        public int InsetPrescriptionByMedicalRecordId(Prescription prescription, int medicalRecordId)
        {
            int prescriptionId = 0;
            int result = 0;

            foreach(PrescriptionVM prescriptionVM in _prescription)
            {
                if(prescriptionVM.MedicalRecordId == medicalRecordId)
                {
                    prescriptionId = prescriptionVM.PrescriptionId;
                    result++;
                }
            }

            return result; 
        }

        public List<PrescriptionVM> SelectAllPrescriptions(int animalId)
        {
            List<PrescriptionVM> returnList = new List<PrescriptionVM>();
            List<int> medicalRecordIds = AnimalIdToMedicalRecordIdRepresentation[animalId];
            var proceduresToReturn = _prescription.Where(p => medicalRecordIds.Contains(p.MedicalRecordId));
            foreach (PrescriptionVM prescription in proceduresToReturn)
            {
                returnList.Add(prescription);
            }
            return returnList;
        }
    }
 }

