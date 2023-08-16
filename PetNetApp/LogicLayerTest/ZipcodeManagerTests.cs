using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LogicLayer;
using DataAccessLayerFakes;
using DataObjects;


namespace LogicLayerTest
{
    [TestClass]
    public class ZipcodeManagerTests
    {
        private ZipcodeManager _zipcodeManager = null;

        [TestInitialize]
        public void SetupTests()
        {
            _zipcodeManager = new ZipcodeManager(new ZipcodeAccessorFakes());
        }

        [TestCleanup]
        public void TeardownTests()
        {
            _zipcodeManager = null;
        }


        [TestMethod]
        public void TestRetrieveCityStateLatLongByZipcode()
        {
            //arrange
            string zipcode = "50207";
            string expectedCity = "New Sharon";
            string expectedState = "IA";
            decimal expectedLatitude = 41.47000m;
            decimal expectedLongitude = -92.65000m;
            Zipcode zipcodeObj = null;

            // act
            zipcodeObj = _zipcodeManager.RetrieveCityStateLatLongByZipcode(zipcode);

            // assert
            Assert.AreEqual(expectedCity, zipcodeObj.City);
            Assert.AreEqual(expectedState, zipcodeObj.State);
            Assert.AreEqual(expectedLatitude, zipcodeObj.Latitude);
            Assert.AreEqual(expectedLongitude, zipcodeObj.Longitude);
        }
    }
}
