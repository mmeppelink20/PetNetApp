using DataAccessLayerFakes;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataObjects;
using LogicLayerInterfaces;

namespace LogicLayerTest
{
    [TestClass]
    public class FundraisingEventManagerTests
    {
        private IFundraisingEventManager _fundraisingEventManager = null;

        [TestInitialize]
        public void SetupTests()
        {
            _fundraisingEventManager = new FundraisingEventManager(new FundraisingEventAccessorFakes());
        }

        [TestCleanup]
        public void TeardownTests()
        {
            _fundraisingEventManager = null;
            FundraisingFakeData.ResetFakeFundraisingEventData();
        }

        [TestMethod]
        public void TestRetrieveFundraisingEventsByShelterId()
        {
            //arrange
            FundraisingEventAccessorFakes fakes = new FundraisingEventAccessorFakes();
            int shelterId = 100006;
            int actualResult = 0;
            int expectedResult = 6;

            // act
            actualResult = fakes.SelectAllFundraisingEventsByShelterId(shelterId).Count;
            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestRetrieveFundraisingEventsByCampaignId()
        {
            int expected = 4;
            int actual = 0;
            int campaignId = 100000;

            actual = _fundraisingEventManager.RetrieveAllFundraisingEventsByCampaignId(campaignId).Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestUpdateFundraisingEventResults()
        {
            //arrange
            bool successfulUpdate = false;
            FundraisingEventVM oldFundraisingEvent = _fundraisingEventManager.RetrieveFundraisingEventByFundraisingEventId(100000);
            //FundraisingEventVM newFundraisingEvent = oldFundraisingEvent.Copy();
            FundraisingEventVM newFundraisingEvent = new FundraisingEventVM();

            newFundraisingEvent.Cost = 120.00m;
            newFundraisingEvent.NumOfAttendees = 70;
            newFundraisingEvent.NumAnimalsAdopted = 5;
            newFundraisingEvent.UpdateNotes = "Event costs low, adoptions good";
            newFundraisingEvent.FundraisingEventId = 100000;

            //act
            successfulUpdate = _fundraisingEventManager.EditFundraisingEventResults(oldFundraisingEvent, newFundraisingEvent);

            //assert
            Assert.IsTrue(successfulUpdate);
            Assert.AreEqual(120.00m, _fundraisingEventManager.RetrieveFundraisingEventByFundraisingEventId(100000).Cost);
            Assert.AreEqual(70, _fundraisingEventManager.RetrieveFundraisingEventByFundraisingEventId(100000).NumOfAttendees);
            Assert.AreEqual(5, _fundraisingEventManager.RetrieveFundraisingEventByFundraisingEventId(100000).NumAnimalsAdopted);
            Assert.AreEqual("Event costs low, adoptions good", _fundraisingEventManager.RetrieveFundraisingEventByFundraisingEventId(100000).UpdateNotes);
        }

        [TestMethod]
        public void RetrieveActiveFundraisingEventsByShelterIdReturnsTheCorrectList()
        {
            //arrange
            int expectedEventCount = 1;
            int actualEventCount = 0;
            int shelterId = 100005;

            //act
            actualEventCount = _fundraisingEventManager.RetrieveAllActiveFundraisingEventsByShelterId(shelterId).Count;
            //assert

            Assert.AreEqual(expectedEventCount, actualEventCount);

        }

        [TestMethod]
        public void RetrieveAllActiveFundraisingEventsReturnsTheCorrectList()
        {
            //arrange
            int expectedEventCount = 4;
            int actualEventCount = 0;

            //act
            actualEventCount = _fundraisingEventManager.RetrieveAllActiveFundraisingEvents().Count;
            //assert

            Assert.AreEqual(expectedEventCount, actualEventCount);
        }
    }
}
