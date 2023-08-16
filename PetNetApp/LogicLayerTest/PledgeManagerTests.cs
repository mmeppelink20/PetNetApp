using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LogicLayer;
using DataAccessLayerFakes;
using DataObjects;

namespace LogicLayerTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class PledgeManagerTests 
    {
        private PledgeManager _manager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _manager = new PledgeManager(new PledgeAccessorFakes());
        }

        [TestMethod]
        public void TestRetrieveAllPledgesByEventId()
        {
            // Arrange
            PledgeAccessorFakes fakes = new PledgeAccessorFakes();
            int eventId = 100000;
            int expectedCount;
            int actualCount;

            // Act
            expectedCount = fakes.SelectAllPledgesByEventId(eventId).Count;

            actualCount = _manager.RetrieveAllPledgesByEventId(eventId).Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestRetrieveSpecificPledgerByUserId()
        {
            // Arrange
            PledgeAccessorFakes fakes = new PledgeAccessorFakes();
            int userId = 100000;
            int expectedCount;
            int actualCount;

            // Act
            expectedCount = fakes.SelectPledgerByUserId(userId).Count;

            actualCount = _manager.RetrieveSpecificPledgerByUserId(userId).Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestCreatePledger()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult;
            PledgeVM pledgeVM = new PledgeVM()
            {
                UserId = 100000,
                FundraisingEventId = 100000,
                Amount = 100,
                Target = "Dog",
                Requirement = "Goal: $1000",
                Message = "For my dog",
                GivenName = "John",
                FamilyName = "Don",
                Phone = "7141234566",
                Email = "jonDon@company.com",
                IsContactPreferencePhone = false
            };

            // Act
            actualResult = _manager.CreatePledge(pledgeVM);

            // Arrange
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void TestRetrieveAllPledges()
        {
            int expected = 100;

            int actual = _manager.RetrieveAllPledges().Count;

            Assert.AreEqual(expected, actual);
        } 
    }
}
