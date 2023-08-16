/// <summary>
/// Andrew Schneider
/// Created: 2023/03/30
/// 
/// Unit tests for ResourceAddRequestManager
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DataAccessLayerFakes;
using LogicLayer;
using DataObjects;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LogicLayerTest
{
    [TestClass]
    public class ResourceAddRequestManagerTests
    {
        ResourceAddRequestManager _resourceAddRequestManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _resourceAddRequestManager = new ResourceAddRequestManager(new ResourceAddRequestAccessorFake());
        }

        [TestCleanup]
        public void TeardownTests()
        {
            _resourceAddRequestManager = null;
        }

        [TestMethod]
        public void TestRetrieveAllActiveResourceAddRequestsByShelterId()
        {
            int expectedCount = 2;
            int actualCount = 0;
            int shelterId = 100000;

            var resourceAddRequests = _resourceAddRequestManager.RetrieveActiveResourceAddRequestsByShelterId(shelterId);
            actualCount = resourceAddRequests.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestUpdateResourceAddRequestActiveField()
        {
            // Arrange
            ResourceAddRequest oldResourceAddRequest = new ResourceAddRequest
            {
                ResourceAddRequestId = 100000,
                ShelterId = 100000,
                UsersId = 100000,
                Title = "Dog toys",
                Note = "Special toys to keep dogs occupied",
                Active = true
            };
            ResourceAddRequest newResourceAddRequest = new ResourceAddRequest
            {
                ResourceAddRequestId = 100000,
                ShelterId = 100000,
                UsersId = 100000,
                Title = "Cat toys",
                Note = "Special toys to keep cats occupied",
                Active = false
            };

            // Act
            bool actualResult = _resourceAddRequestManager.EditResourceAddRequestActiveField(oldResourceAddRequest, newResourceAddRequest);

            // Assert
            Assert.IsTrue(actualResult);
        }


        [TestMethod]
        public void TestAddResourceAddRequestReturnsTrueWhenRequestIsAdded()
        {
            int expectedResourceAddRequestCountAfterAddition = _resourceAddRequestManager.RetrieveActiveResourceAddRequestsByShelterId(100000).Count + 1;
            int actualCount;
            ResourceAddRequest requestToAdd = new ResourceAddRequest()
            {
                ShelterId = 100000,
                UsersId = 100000,
                Title = "Grapes",
                Note = "An annoying duck keeps asking for these",
                Active = true
            };

            bool added = _resourceAddRequestManager.AddResourceAddRequest(requestToAdd);
            actualCount = _resourceAddRequestManager.RetrieveActiveResourceAddRequestsByShelterId(100000).Count;

            Assert.IsTrue(added);
            Assert.AreEqual(expectedResourceAddRequestCountAfterAddition, actualCount);
        }
    }
}
