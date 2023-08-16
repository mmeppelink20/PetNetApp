using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LogicLayer;
using DataAccessLayerFakes;
using DataObjects;
using System.Collections.Generic;

namespace LogicLayerTest
{
    [TestClass]
    public class MedicalRecordManagerTests
    {
        private MedicalRecordManager _medicalRecordManager = null;
        private MedicalRecordAccessorFakes fake = null;

        [TestInitialize]
        public void TestSetup()
        {
            _medicalRecordManager = new MedicalRecordManager(new MedicalRecordAccessorFakes());
            fake = new MedicalRecordAccessorFakes();
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/08
        /// </summary>
        [TestMethod]
        public void TestSelectLastMedicalRecordIdByAnimalIdReturnsCorrectNumber()
        {
            int expectedResult = 61;
            int animalId = 51;
            int acctualResult;

            acctualResult = _medicalRecordManager.RetrieveLastMedicalRecordIdByAnimalId(animalId);

            Assert.AreEqual(expectedResult, acctualResult);
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/08
        /// </summary>
        [TestMethod]
        public void TestSelectLastMedicalRecordIdByAnimalIdReturnsZeroIfNoMedicalRecordForAnimal()
        {
            int expectedResult = 0;
            int animalId = 49;
            int acctualResult;

            acctualResult = _medicalRecordManager.RetrieveLastMedicalRecordIdByAnimalId(animalId);

            Assert.AreEqual(expectedResult, acctualResult);
        }

        [TestMethod]
        public void TestRetrieveMedicalDiagnosisByAnimalId()
        {
            int animalId = 100000;
            int expectedCount = 1;
            int actualCount = 0;

            var medicalRecords = _medicalRecordManager.RetrieveMedicalRecordDiagnosisByAnimalId(animalId);
            actualCount = medicalRecords.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void UpdateTreatmentByMedicalRecordId()
        {
            int animalId = 100000;
            bool expectedResult = true;

            bool actualResult = _medicalRecordManager.EditTreatmentByMedicalRecordId(animalId, "New Diagnosis Name", "New Diagnosis Notes", "old Diagnosis Name", "old Diagnosis Notes");


            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/27
        /// </summary>
        [TestMethod]
        public void TestAddMedicalRecordAddsMedicalRecordReturningMedicalRecordId()
        {
            int returnedValue = 0;
            int actualMedicalRecordId = 56;
            int animalId = 3;
            MedicalRecordVM medicalRecord = new MedicalRecordVM()
            {
                MedicalRecordId = actualMedicalRecordId,
                AnimalId = animalId
            };
            List<MedicalRecordVM> recordsReturned;
            int recordsReturnedExpectedCount = 1;

            returnedValue = _medicalRecordManager.AddMedicalRecord(medicalRecord);
            recordsReturned = _medicalRecordManager.RetrieveMedicalRecordDiagnosisByAnimalId(animalId);

            Assert.AreEqual(actualMedicalRecordId, returnedValue);
            Assert.AreEqual(recordsReturnedExpectedCount, recordsReturned.Count);
        }

        [TestMethod]
        public void TestEditQuarantineStatusWithCorrectOldQuarantineStatus()
        {
            // Arrange
            const int id = 100000;
            const bool quarantineStatus = true;
            const bool oldQurantineStatus = false;
            bool expected = true;
            bool actual;

            // Act
            actual = _medicalRecordManager.EditQuarantineStatusByMedicalRecordId(id, quarantineStatus, oldQurantineStatus);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void FailTestEditQuarantineStatusWithIncorrectOldQuarantineStatus()
        {
            // Arrange
            const int id = 100000;
            const bool quarantineStatus = true;
            const bool oldQurantineStatus = true;
            bool actual;

            // Act
            actual = _medicalRecordManager.EditQuarantineStatusByMedicalRecordId(id, quarantineStatus, oldQurantineStatus);

        }

        [TestMethod]
        public void TestAddTestMedicalRecordByAnimalIdWithCorrectAnimalId()
        {
            // Arrange
            const int animalid = 100001;
            const string notes = "test";
            const bool test = true;
            const string diagnosis = "none";
            int expected = 100002;
            int actual;

            // Act
            actual = _medicalRecordManager.AddTestMedicalRecordByAnimalId(animalid, notes, test, diagnosis);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddTestMedicalRecordByAnimalIdWithInorrectAnimalId()
        {
            // Arrange
            const int animalid = 0;
            const string notes = "test";
            const bool test = true;
            const string diagnosis = "none";
            int actual;

            // Act
            actual = _medicalRecordManager.AddTestMedicalRecordByAnimalId(animalid, notes, test, diagnosis);
        }

        [TestMethod]
        public void TestRetrieveMedicalRecordsByAnimalId()
        {
            int animalId = 100000;
            int expectedCount = 1;
            int actualCount = _medicalRecordManager.RetrieveAllMedicalRecordsByAnimalId(animalId).Count;

            Assert.AreEqual(expectedCount, actualCount);

        }
        [TestMethod]
        public void TestSelectMedicalRecordByAnimal()
        {
            int animalId = 100000;
            int expectedCount = 1;
            int actualCount = 0;

            var medicalRecords = _medicalRecordManager.SelectMedicalRecordByAnimal(animalId);
            actualCount = medicalRecords.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }
        [TestMethod]
        public void TestEditMedicalRecord()
        {
            int expectedResult = 1;
            int actualResult = 0;
            bool realResult = _medicalRecordManager.EditMedicalRecord(fake.oldmedicalRecord, fake.newmedicalRecord);
            if (realResult)
            {
                actualResult = 1;
            }
            else
            {
                actualResult = 0;
            }
            Assert.AreEqual(actualResult, expectedResult);
        }
        [TestMethod]
        public void TestAddMedicalNote()
        {
            bool realResult = _medicalRecordManager.AddMedicalNote(fake.addmedicalRecord);

            //int r = _medicalRecordManager.AddMedicalNote(fake.InsertMedicalRecords).c
            Assert.IsTrue( realResult);
        }
    }
}
