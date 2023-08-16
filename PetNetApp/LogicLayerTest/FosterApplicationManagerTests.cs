using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LogicLayer;
using DataAccessLayerFakes;

namespace LogicLayerTest
{
    [TestClass]
    public class FosterApplicationManagerTests
    {
        private FosterApplicationManager _manager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _manager = new FosterApplicationManager(new FosterApplicationAccessorFakes());
        }

        [TestMethod]
        public void TestRetrieveAllFosterApplicationsByUsersId()
        {
            int expectedResult = 1;
            int userId = 1;

            int actualResult = _manager.RetrieveAllFosterApplicationsByUsersId(userId).Count;

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
