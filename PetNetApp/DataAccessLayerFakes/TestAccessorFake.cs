using DataAccessLayerInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerFakes
{

    public class TestAccessorFake : ITestAccessor
    {
        private List<Test> _tests;
        private List<MedicalRecordVM> _medicalRecords;
        
        public TestAccessorFake()
        {
            _tests = new List<Test>()
            {
                new TestVM()
                {
                    TestId = 1,
                    MedicalRecordId = 1,
                    UserId = 21,
                    TestName = "Rabies Test",
                    TestAcceptableRange = "",
                    TestResult = "Positive",
                    TestNotes = "Animal has been put down",
                    TestDate = DateTime.Now
                },
                new TestVM()
                {
                    TestId = 2,
                    MedicalRecordId = 2,
                    UserId = 23,
                    TestName = "Rabies Test",
                    TestAcceptableRange = "",
                    TestResult = "Negative",
                    TestNotes = "",
                    TestDate = DateTime.Now
                },
                new TestVM()
                {
                    TestId = 3,
                    MedicalRecordId = 3,
                    UserId = 21,
                    TestName = "Dead Test",
                    TestAcceptableRange = "",
                    TestResult = "Positive",
                    TestNotes = "Shot animal a few more times, no response",
                    TestDate = DateTime.Now
                },
                new TestVM()
                {
                    TestId = 4,
                    MedicalRecordId = 4,
                    UserId = 22,
                    TestName = "Flea Test",
                    TestAcceptableRange = "",
                    TestResult = "Positive",
                    TestNotes = "Animal has recieved a vaccination",
                    TestDate = DateTime.Now
                }
            };
            _medicalRecords = new List<MedicalRecordVM>()
            {
                new MedicalRecordVM()
                {
                    MedicalRecordId = 1,
                    AnimalId = 1,
                    Date = DateTime.Now,
                    MedicalNotes = "Animal has rabies",
                    IsTest = true,
                    IsVaccination = false,
                    IsPrescription = false,
                    QuarantineStatus = true,
                    Diagnosis = "Has Rabies"
                },
                new MedicalRecordVM()
                {
                    MedicalRecordId = 3,
                    AnimalId = 1,
                    Date = DateTime.Now,
                    MedicalNotes = "Animal has passed",
                    IsTest = true,
                    IsVaccination = false,
                    IsPrescription = false,
                    QuarantineStatus = true,
                    Diagnosis = "Is Dead"
                },
                new MedicalRecordVM()
                {
                    MedicalRecordId = 6,
                    AnimalId = 1,
                    Date = DateTime.Now,
                    MedicalNotes = "Animal has passed",
                    IsTest = false,
                    IsVaccination = false,
                    IsPrescription = false,
                    QuarantineStatus = true,
                    Diagnosis = "Is Dead"
                },
                new MedicalRecordVM()
                {
                    MedicalRecordId = 2,
                    AnimalId = 3,
                    Date = DateTime.Now,
                    MedicalNotes = "Animal doesn't have rabies",
                    IsTest = true,
                    IsVaccination = false,
                    IsPrescription = false,
                    QuarantineStatus = true,
                    Diagnosis = "Clean"
                },
                new MedicalRecordVM()
                {
                    MedicalRecordId = 4,
                    AnimalId = 5,
                    Date = DateTime.Now,
                    MedicalNotes = "Animal has fleas",
                    IsTest = true,
                    IsVaccination = false,
                    IsPrescription = false,
                    QuarantineStatus = true,
                    Diagnosis = "Injected anti flea shot"
                }
            };
        }

        public TestVM SelectTestByMedicalRecordId(int medicalRecordId)
        {
            TestVM test = null;
            foreach (var t in _tests)
            {
                if (t.MedicalRecordId == medicalRecordId)
                {
                    test = (TestVM)t;
                }
            }
            return test;
        }

        public List<Test> SelectTestsByAnimalId(int animalId)
        {
            var animalsMedicalTestRecords = _medicalRecords.Where((record) => record.AnimalId == animalId && record.IsTest).Select((record) => record.MedicalRecordId);
            return _tests.Where((test) => animalsMedicalTestRecords.Contains(test.TestId)).ToList();
        }

        public int InsertTestByMedicalRecordId(Test test, int medicalRecordId)
        {
            Test _test;
            int results = 0;
            foreach (MedicalRecord m in _medicalRecords)
            {
                if (m.MedicalRecordId == medicalRecordId)
                {
                    _test = test;
                    _test.MedicalRecordId = medicalRecordId;
                    results = 1;
                }
            }
            if (results == 0)
            {
                throw new ApplicationException();
            }
            return results;
        }
    }
}
