using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    public class MedicalRecordAccessorFakes : IMedicalRecordAccessor
    {
        public MedicalRecord oldmedicalRecord = new MedicalRecord();
        public MedicalRecord newmedicalRecord = new MedicalRecord();
        public MedicalRecord addmedicalRecord = new MedicalRecord();
        public List<MedicalRecord> addmedicalRecords = new List<MedicalRecord>();

        private Dictionary<int, int> medicalRecordRepresentation = new Dictionary<int, int>()
        {
            {50, 60 },
            {51, 61 }
        };


        public int SelectLastMedicalRecordIdByAnimalId(int animalId)
        {
            int medicalRecordId = 0;
            if (medicalRecordRepresentation.ContainsKey(animalId))
            {
                medicalRecordId = medicalRecordRepresentation[animalId];
            }
            return medicalRecordId;
        }
        List<MedicalRecordVM> medicalRecords = new List<MedicalRecordVM>();
        List<AnimalVM> animals = new List<AnimalVM>();

        public MedicalRecordAccessorFakes()
        {
            addmedicalRecords.Add(new MedicalRecord
            {
                MedicalNotes = "help",
                AnimalId=2
            });
            medicalRecords.Add(new MedicalRecordVM
            {
                MedicalRecordId = 100000,
                AnimalId = 100000,
                Date = DateTime.Now,
                MedicalNotes = "These are sample medical notes",
                IsProcedure = true,
                IsTest = true,
                IsVaccination = true,
                IsPrescription = false,
                Images = false,
                QuarantineStatus = false,
                Diagnosis = "Sample Diagnosis 1"
            });

            medicalRecords.Add(new MedicalRecordVM
            {
                MedicalRecordId = 100001,
                AnimalId = 100001,
                Date = DateTime.Now,
                MedicalNotes = "These are sample medical notes",
                IsProcedure = true,
                IsTest = true,
                IsVaccination = true,
                IsPrescription = false,
                Images = false,
                QuarantineStatus = true,
                Diagnosis = "Sample Diagnosis 2"
            });

            animals.Add(new AnimalVM
            {
                AnimalId = 100000,
                AnimalName = "Rufus",
                AnimalGender = "Male",
                AnimalTypeId = "Dog",
                AnimalBreedId = "Lab",
                Personality = "Friendly",
                Description = "this is a sample description",
                BroughtIn = new DateTime(),
                MicrochipNumber = "S/N-3234529",
                Aggressive = false
            });

            animals.Add(new AnimalVM
            {
                AnimalId = 100001,
                AnimalName = "Roo",
                AnimalGender = "Male",
                AnimalTypeId = "Dog",
                AnimalBreedId = "Retriever",
                Personality = "Friendly",
                Description = "this is a sample description",
                BroughtIn = new DateTime(),
                MicrochipNumber = "S/N-3234528",
                Aggressive = false
            });
            oldmedicalRecord.MedicalRecordId = 100000;
            newmedicalRecord.MedicalRecordId = 100000;
            addmedicalRecord.Diagnosis = "good";
            addmedicalRecord.AnimalId = 100000;
            addmedicalRecord.MedicalNotes = "this is a add note";
            
        }

        public List<MedicalRecordVM> SelectMedicalRecordDiagnosisByAnimalId(int animalId)
        {
            return medicalRecords.Where(m => m.AnimalId == animalId).ToList();
        }

        public int UpdateMedicalTreatmentByMedicalrecordId(int medicalRecordId, string newDiagnosis, string newMedicalNotes, string oldDiagnosis, string oldMedicalNotes)
        {
            int result = 0;

            for (int i = 0; i < medicalRecords.Count; i++)
            {
                if (medicalRecords[i].MedicalRecordId == medicalRecordId)
                {
                    medicalRecords[i].Diagnosis = newDiagnosis;
                    medicalRecords[i].MedicalNotes = newMedicalNotes;

                    result++;
                }
            }
            return result;
        }

        public int InsertMedicalRecord(MedicalRecordVM medicalRecord)
        {
            int medicalRecordId = 0;
            medicalRecords.Add(medicalRecord);
            medicalRecordId = medicalRecord.MedicalRecordId;
            return medicalRecordId;
        }
        public List<MedicalRecordVM> SelectMedicalRecordByAnimal(int animalId)
        {
            return medicalRecords.Where(m => m.AnimalId == animalId).ToList();
        }

        public int UpdateMedicalRecord(MedicalRecord oldmedicalRecord, MedicalRecord medicalRecord)
        {
            int result = 0;

            if (oldmedicalRecord.MedicalRecordId == medicalRecord.MedicalRecordId)
            {
                result = 1;
            }
            return result;
        }

        public int AddMedicalNotes(MedicalRecord medicalRecord)
        {
            int row;
            int row2;
            row = medicalRecords.Count;
       
            MedicalRecordVM m = new MedicalRecordVM();
            m.MedicalNotes = medicalRecord.MedicalNotes;
            medicalRecords.Add(m);
            row2 = medicalRecords.Count - row;
            return row2;
            
        }

        public int UpdateQuarantineStatusByMedicalRecordId(int medicalRecordId, bool quarantineStatus, bool oldQuarantineStatus)
        {
            // loop through medicalRecords and change the one you need to change

            var m = medicalRecords.Find(md => md.MedicalRecordId == medicalRecordId);
            if (m.QuarantineStatus == oldQuarantineStatus)
            {
                return 1;
            }
            else
            {
                throw new ApplicationException();
            }

            // if it's old status is what was passed
            // return 1 for 1 row affected, or throw exception if you didn't find one
        }

        public int InsertTestMedicalRecordByAnimalId(int animalId, string medicalNotes, bool test, string diagnosis)
        {
            MedicalRecordVM newTestRecord = new MedicalRecordVM();
            bool found = false;
            foreach (Animal a in animals)
            {
                if (a.AnimalId == animalId)
                {
                    found = true;
                    newTestRecord.MedicalRecordId = medicalRecords[medicalRecords.Count - 1].MedicalRecordId + 1;
                    newTestRecord.AnimalId = animalId;
                    newTestRecord.MedicalNotes = medicalNotes;
                    newTestRecord.IsTest = test;
                    newTestRecord.Diagnosis = diagnosis;

                    medicalRecords.Add(newTestRecord);
                }
            }
            if (found == false)
            {
                throw new ApplicationException();
            }
            return newTestRecord.MedicalRecordId;
        }

        public List<MedicalRecordVM> SelectAllMedicalRecordsByAnimald(int animalId)
        {
            return medicalRecords.Where(m => m.AnimalId == animalId).ToList();
        }
    }
}
