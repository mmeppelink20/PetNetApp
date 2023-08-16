// Created By Asa Armstrong
// Created On 2023/03/23

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayerInterfaces;
using DataAccessLayerFakes;
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;

namespace LogicLayerTest
{
    [TestClass]
    public class FosterApplicationResponseTests
    {
        private IFosterApplicationResponseManager _responseManager = null;
        static DateTime dt = DateTime.Now;

        private FosterApplicationResponse _response = new FosterApplicationResponse()
        {
            FosterApplicationResponseId = 999_998,
            FosterApplicationId = 999_998,
            UsersId = 999_998,
            Approved = false,
            FosterApplicationResponseDate = dt,
            FosterApplicationResponseNotes = "notes"
        };

        private FosterApplicationResponse _response2 = new FosterApplicationResponse()
        {
            FosterApplicationResponseId = 999_998,
            FosterApplicationId = 999_998,
            UsersId = 999_998,
            Approved = false,
            FosterApplicationResponseDate = dt,
            FosterApplicationResponseNotes = "notes"
        };

        [TestInitialize]
        public void TestSetup()
        {
            _responseManager = new FosterApplicationResponseManager(new FosterApplicationResponseAccessorFakes());
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _responseManager = null;
        }

        [TestMethod]
        public void TestAddFosterApplicationResponse()
        {
            Assert.AreEqual(_responseManager.AddFosterApplicationResponse(_response), true);
        }

        [TestMethod]
        public void TestRetrieveFosterApplicationResponse()
        {
            _responseManager.AddFosterApplicationResponse(_response);
            Assert.IsTrue(_responseManager.RetrieveFosterApplicationResponse(_response.FosterApplicationId).Equals(_response));
        }

        [TestMethod]
        public void TestFosterApplicationResponseEquals()
        {
            Assert.IsTrue(_response.Equals(_response2));
        }

        [TestMethod]
        public void TestEditFosterApplicationResponse()
        {
            _responseManager.AddFosterApplicationResponse(_response);
            FosterApplicationResponse response = _response;
            response.FosterApplicationResponseNotes = "new comments";

            Assert.IsTrue(_responseManager.EditFosterApplicationResponse(response, _response));
        }
    }
}
