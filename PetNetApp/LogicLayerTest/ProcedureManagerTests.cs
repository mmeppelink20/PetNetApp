/// <summary>
/// Andrew Cromwell
/// Created: 2023/01/08
/// 
/// Class that tests that the ProcedureManager methods work properly.
/// </summary>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataObjects;
using DataAccessLayerFakes;
using LogicLayer;
using System.Collections.Generic;

namespace LogicLayerTest
{
    [TestClass]
    public class ProcedureManagerTests
    {
        private ProcedureManager _procedureManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _procedureManager = new ProcedureManager(new ProcedureAccessorFakes());
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/08
        /// </summary>
        [TestMethod]
        public void TestAddProcedureByMedicalRecordIdReturnsTrueIfProcedureIsSaved()
        {
            Procedure procedure = new Procedure
            {
                ProcedureId = 0,
                MedicalRecordId = 0,
                UserId = 0,
            };
            int medicalRecordId = 0;

            bool result = _procedureManager.AddProcedureByMedicalRecordId(procedure, medicalRecordId);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/14
        /// </summary>
        [TestMethod]
        public void TestGetProceduresByAnimalIdReturnsCorectNumberOfProcedures()
        {
            int expectedResult = 3;
            int actualResult = 0;
            int animalId = 8;
            List<ProcedureVM> fakeprocedures;
                        
            fakeprocedures = _procedureManager.RetrieveProceduresByAnimalId(animalId);
            actualResult = fakeprocedures.Count;

            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/14
        /// </summary>
        [TestMethod]
        public void TestEditProcedureByProcedureIdChangesDataCorectly()
        {
            Procedure procedure = new Procedure()
            {
                ProcedureId = 666,
                MedicalRecordId = 666,
                UserId = 666,
                ProcedureName = "This sould replace the name on one of the fakes",
                MedicationsAdministered = "some meds were used.",
                ProcedureNotes = "Notes that override previous notes",
                ProcedureDate = DateTime.Now
            };
            Procedure oldProcedure = new Procedure()
            {
                ProcedureId = 666,
                MedicalRecordId = 666,
                UserId = 999,
                ProcedureName = "procedure name",
                MedicationsAdministered = "some meds were used.",
                ProcedureNotes = "notes to be overriden",
                ProcedureDate = DateTime.Parse("2021-01-22")
            };
            bool methodResult = false;
            int animalId = 5;
            List<ProcedureVM> fakeprocedures;
            
            methodResult = _procedureManager.EditProcedureByProcedureId(procedure, oldProcedure);
            fakeprocedures = _procedureManager.RetrieveProceduresByAnimalId(animalId);

            Assert.IsTrue(methodResult);
            Assert.AreEqual(fakeprocedures[1].ProcedureName, "This sould replace the name on one of the fakes");
        }

        [TestMethod]
        public void RetrieveProcedureByMedicalRecordId()
        {
            int medicalRecordId = 666;
            int expectedProcId = 666;
            int actualProcId = _procedureManager.RetrieveProcedureByMedicalRecordId(medicalRecordId).ProcedureId;

            Assert.AreEqual(expectedProcId, actualProcId);

        }
    }
}
