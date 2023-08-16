using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataObjects;
using DataAccessLayerFakes;
using LogicLayer;
using System.Collections.Generic;

namespace LogicLayerTest
{
    [TestClass]
   public  class PrescriptionManagerTests
    {
        private PrescriptionManager _prescriptionManager = null; 

        [TestInitialize]
        public void TestSetup()
        {
            _prescriptionManager = new PrescriptionManager(new PrescriptionAccessorFakes());
        }



        /// <summary>
        /// Tyler Hand
        /// Created : 2023/04/25
        /// </summary>
        [TestMethod]
        public void TestAddPrescriptionByMedicalRecordIdReturnsTrueIfProcedureIsSaved()
        {
            Prescription prescription = new Prescription
            {
                PrescriptionId = 333,
                MedicalRecordId = 333,
                UserId = 333,
            };
            int medicalRecordId = 333;
            bool expectedResult = true; 

            bool actualResult = false;
            if(_prescriptionManager.AddPresciptionByMedicalRecordId(prescription, medicalRecordId))
            {
                actualResult = true;
            }

            Assert.AreEqual(expectedResult, actualResult);
        }



        /// <summary>
        /// Tyler Hand
        /// Created : 2023/04/25
        /// </summary>
        [TestMethod]
        public void TestGetPrescripionsByAnimalIdReturnsCorectNumberOfProcedures()
        {
            int expectedResult = 2;
            int actualResult = 0;
            int animalId = 8;
            List<PrescriptionVM> fakePresciprions;

            fakePresciprions = _prescriptionManager.RetrievePrescriptions(animalId);
            actualResult = fakePresciprions.Count;

            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}
