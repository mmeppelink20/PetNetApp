using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LogicLayer;
using DataAccessLayerFakes;
using DataObjects;
using System.Collections.Generic;

namespace LogicLayerTest
{
    [TestClass]
    public class RequestManagerTests
    {
        private RequestManager _requestManager = null;

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/15
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            _requestManager = new RequestManager(new RequestAccessorFakes());
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/15
        /// </summary> 
        [TestMethod]
        public void TestRetrieveRequestsByShelterIdReturnsTheCorrectNumberOfRequests()
        {
            int expectedNumberOfRequests = 2;
            int shelterId = 55;
            List<RequestVM> requests = null;

            requests = _requestManager.RetrieveRequestsByShelterId(shelterId);

            Assert.AreEqual(expectedNumberOfRequests, requests.Count);
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/03/15
        /// </summary> 
        [TestMethod]
        public void TestRetrieveRequestsByShelterIdGivesTheCorrectNumberOfRequestLinesToEachRequest()
        {
            int expectedNumberOfRequestLines = 3;
            int indexToCheck = 1;
            int shelterId = 55;
            List<RequestVM> requests = null;

            requests = _requestManager.RetrieveRequestsByShelterId(shelterId);

            Assert.AreEqual(expectedNumberOfRequestLines, requests[indexToCheck].RequestLines.Count);
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/06
        /// </summary> 
        [TestMethod]
        public void TestAddInventoryItemRequestRturnsTrueWhenAddingSuccessful()
        {
            RequestVM request = new RequestVM() { RecievingShelterId = 5 };

            Assert.IsTrue(_requestManager.AddInventoryItemRequest(request));
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/06
        /// </summary> 
        [TestMethod]
        public void TestAddInventoryItemRequestAddsRequestsCrorrectly()
        {
            int shelterId = 5;
            int expectedRequests = 1;
            int expectedRequestLines = 2;
            List<RequestVM> returnedRequests = null;
            RequestVM request = new RequestVM() { RecievingShelterId = shelterId, RequestLines = new List<RequestResourceLine>(),
                RequestDate = DateTime.Now};
            request.RequestLines.Add(new RequestResourceLine() { ItemId = "food" });
            request.RequestLines.Add(new RequestResourceLine() { ItemId = "water" });

            _requestManager.AddInventoryItemRequest(request);
            returnedRequests = _requestManager.RetrieveRequestsByShelterId(shelterId);

            Assert.AreEqual(expectedRequests, returnedRequests.Count);
            Assert.AreEqual(expectedRequestLines, returnedRequests[0].RequestLines.Count);
        }

        [TestMethod]
        public void TestEditRequestAcknowledge()
        {
            bool expectedResult = true;
            bool actualResult = false;
            Request request = new Request();
            request.RequestId = 1;

            bool newAcknowledged = true;


            actualResult = _requestManager.EditRequestAcknowledge(request.RequestId, newAcknowledged, request.Acknowledged);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
