using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataAccessLayer;
using DataObjects;
using LogicLayer;
using DataAccessLayerInterfaces;
using DataAccessLayerFakes;
using System.Collections.Generic;
using LogicLayerInterfaces;

namespace LogicLayerTest
{
    [TestClass]
    public class AnimalUpdatesTests
    {
        private IAnimalUpdatesManager _animalUpdatesManager = null;


        [TestInitialize]
        public void TestSetup()
        {
            _animalUpdatesManager = new AnimalUpdatesManager(new AnimalUpdatesFakes()); // Fake Data
            //_animalUpdatesManager = new AnimalUpdatesManager(new AnimalUpdatesAccessor()); // Actual Data
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _animalUpdatesManager = null;
        }

        [TestMethod]
        public void TestAddAnimalUpdatesByAnimalId()
        {
            bool actualResult = _animalUpdatesManager.AddAnimalUpdatesByAnimalId(100000, "This is test note");
            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public void TestRetrieveAnimalUpdatesByAnimal()
        {
            string actualResult = _animalUpdatesManager.RetrieveAnimalUpdatesByAnimal(100000);
            string expectedResult = "This is test note";

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
