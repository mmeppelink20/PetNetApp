// Created By Asa Armstrong
// Created On 2023/04/04
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataAccessLayerFakes;
using DataObjects;
using LogicLayer;

namespace LogicLayerTest
{
    [TestClass]
    public class AdoptionApplicationResponseTests
    {
        private AdoptionApplicationResponseManager _manager = null;
        private AdoptionApplicationResponseVM _response = null;

        [TestInitialize]
        public void TestSetup()
        {
            _manager = new AdoptionApplicationResponseManager(new AdoptionApplicationResponseAccessorFakes());

            _response = new AdoptionApplicationResponseVM() 
            {
                AdoptionApplicationResponseId = 1,
                AdoptionApplicationId = 1,
                ResponderUserId = 1,
                Approved = false,
                AdoptionApplicationResponseDate = DateTime.Now,
                AdoptionApplicationResponseNotes = "denied, too stinky",
                Application = new AdoptionApplication(),
                Responder = new Users()
            };
        }


        [TestMethod]
        public void TestAddAdoptionApplicationResponseByAdoptionApplicationId()
        {
            bool expectedResult = true;
            bool actualResult = false;
            

            actualResult = _manager.AddAdoptionApplicationResponseByAdoptionApplicationId(_response);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestRetrieveAdoptionApplicationResponse()
        {
            var response = _manager.RetrieveAdoptionApplicationResponse(_response.AdoptionApplicationId);
            Assert.AreEqual(response.AdoptionApplicationResponseId, _response.AdoptionApplicationResponseId);
        }

        [TestMethod]
        public void TestAdoptionApplicationResponseEquals()
        {
            var response2 = new AdoptionApplicationResponseVM()
            {
                AdoptionApplicationResponseId = 1,
                AdoptionApplicationId = 1,
                ResponderUserId = 1,
                Approved = false,
                AdoptionApplicationResponseDate = DateTime.Now,
                AdoptionApplicationResponseNotes = "denied, too stinky",
                Application = new AdoptionApplication(),
                Responder = new Users()
            };
            Assert.IsTrue(_response.Equals(response2));
        }

        [TestMethod]
        public void TestEditAdoptionApplicationResponse()
        {
            _manager.AddAdoptionApplicationResponseByAdoptionApplicationId(_response);
            AdoptionApplicationResponse response = _response;
            response.AdoptionApplicationResponseNotes = "new comments";

            Assert.IsTrue(_manager.EditAdoptionApplicationResponse(response, _response));
        }
    }
}
