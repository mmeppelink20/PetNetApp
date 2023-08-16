 using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using LogicLayer;
using DataAccessLayerFakes;
using DataObjects;

namespace LogicLayerTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class AdoptionApplicationManagerTests
    {
        private AdoptionApplicationManager _manager = null;
        
        [TestInitialize]
        public void TestSetup()
        {
            _manager = new AdoptionApplicationManager(new AdoptionApplicationAccessorFakes());
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestRetrieveHomeOwnershipTypes()
        {
            int expectedCount = 2;
            int actualCount = 0;

            actualCount = _manager.RetrieveAllHomeOwnershipTypes().Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestRetrieveHomeTypes()
        {
            int expectedCount = 2;
            int actualCount = 0;

            actualCount = _manager.RetrieveAllHomeTypes().Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestAddAdoptionApplication()
        {
            bool expectedResult = true;
            bool actualResult = false;
            AdoptionApplicationVM application = new AdoptionApplicationVM()
            {
                AdoptionApplicationId = 1,
                ApplicantId = 1,
                AnimalId = 2,
                ApplicationStatusId = "Pending",
                AdoptionApplicationDate = DateTime.Today,
                AdoptionAnimal = new Animal(),
                Response = new AdoptionApplicationResponse(),
                AdoptionApplicant = new Applicant(),
                Status = new ApplicationStatus()
            };

            actualResult = _manager.AddAdoptionApplication(application);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestRetrieveAllAdoptionApplicationsByAnimalId()
        {
            int expectedResult = 1;
            int animalId = 1;

            int actualResult = _manager.RetrieveAllAdoptionApplicationsByAnimalId(animalId).Count;

            Assert.AreEqual(expectedResult, actualResult);

        }

        public void TestEditAdoptionApplicationStatusByAnimalIdForApprovedApplication()
        {
            bool expectedResult = true;
            AdoptionApplicationResponse response = new AdoptionApplicationResponse()
            {
                AdoptionApplicationResponseId = 1,
                AdoptionApplicationId = 1,
                ResponderUserId = 1,
                Approved = true,
                AdoptionApplicationResponseDate = DateTime.Now,
                AdoptionApplicationResponseNotes = "Approved"
            };

            bool actualResult = _manager.EditAdoptionApplicationStatusByAnimalIdForApprovedApplication(response);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
