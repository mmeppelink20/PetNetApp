using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer;
using DataObjects;
using DataAccessLayerInterfaces;
using DataAccessLayerFakes;
using System.Collections.Generic;

namespace LogicLayerTest
{
    [TestClass]
    public class FosterManagerTests
    {
        private IFosterManager _fosterManager = null;


        [TestInitialize]
        public void TestSetup()
        {
            _fosterManager = new FosterManager();
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _fosterManager = null;
        }


        [TestMethod]
        public void TestRetrieveCorrectNumberOfAnimalsFostererHas()
        {
            
        }

        [TestMethod]
        public void TestRetrieveCorrectNumberOfAnimalsFostererIsAllowed()
        {
        }

        // fails
        [TestMethod]
        public void TestRetrieveNullNumberOfAnimalsFostererHas()
        {
        }

        [TestMethod]
        public void TestRetrieveNegativeNumberOfAnimalsFostererHas()
        {
        }

        [TestMethod]
        public void TestRetrieveNullNumberOfAnimalsFostererIsAllowed()
        {
        }

        [TestMethod]
        public void TestRetrieveNegativeNumberOfAnimalsFostererIsAllowed()
        {
        }
    }
}
