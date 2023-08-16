using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayerInterfaces;
using DataAccessLayerFakes;
using LogicLayer;
using DataObjects;
using System;

namespace LogicLayerTest
{
    [TestClass]
    public class TestManagerTests
    {
        private ITestManager _testManager = null;


        [TestInitialize]
        public void TestSetup()
        {
            _testManager = new TestManager(new TestAccessorFake());
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _testManager = null;
        }

        [TestMethod]
        public void TestSelectTestsByAnimalId()
        {
            // arrange
            const int expectedCount = 2;
            int animalId = 1;
            int actualCount = 0;

            // act
            actualCount = _testManager.RetrieveTestsByAnimalId(animalId).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);

        }

        [TestMethod]
        public void TestAddTestByMedicalRecordIdUsingCorrectMedicalId()
        {
            // Arrange
            Test test = new Test();
            test.TestName = "New Test";
            test.TestDate = DateTime.Now;
            test.TestAcceptableRange = "Range";
            test.TestResult = "Results";
            test.TestNotes = "Notes";
            const int medicalId = 3;
            const bool expected = true;
            bool actual;

            // Act
            actual = _testManager.AddTestByMedicalRecordId(test, medicalId);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddTestByMedicalRecordIdUsingInorrectMedicalId()
        {
            // Arrange
            Test test = new Test();
            test.TestName = "New Test";
            test.TestDate = DateTime.Now;
            test.TestAcceptableRange = "";
            test.TestResult = "Results";
            test.TestNotes = "";
            const int medicalId = 0;
            bool actual;

            // Act
            actual = _testManager.AddTestByMedicalRecordId(test, medicalId);
        }

        [TestMethod]
        public void TestSelectTestByMedicalRecordId()
        {
            int medicalRecordId = 1;
            int expectedTestId = 1;
            int actualTestId = _testManager.RetrieveTestByMedicalRecordId(medicalRecordId).TestId;

            Assert.AreEqual(expectedTestId, actualTestId);
        }
    }
}
