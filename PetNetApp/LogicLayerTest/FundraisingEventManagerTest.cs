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
    public class FundraisingEventManagerTest
    {
        private IFundraisingEventManager _fundraisingEventManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _fundraisingEventManager = new FundraisingEventManager(new FundraisingEventAccessorFakes());
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _fundraisingEventManager = null;
        }

        [TestMethod]
        public void TestInsertFundraiserAnimal()
        {
            // arrange
            int fundraisingEventId = 100000;
            int animalId = 100000;

            // act
            bool actualResult = _fundraisingEventManager.AddFundraiserAnimal(fundraisingEventId, animalId);

            // assert
            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public void InsertFundraisingEventEntity()
        {
            // arrange
            int fundraisingEventId = 100000;
            int contactId = 100000;

            // act
            bool actualResult = _fundraisingEventManager.AddFundraisingEventEntity(fundraisingEventId, contactId);

            // assert
            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public void InsertFundraisingEvent()
        {
            // arrange
            FundraisingEvent fundraisingEvent = new FundraisingEvent { 
                FundraisingEventId = 100000,
                Title = "This is a test",
                UsersId = 100000,
                ShelterId = 100000,
                ImageId = "100000",
                Hidden = false,
                Complete = false
            };


            // act
            int rowAffected = _fundraisingEventManager.AddFundraisingEvent(fundraisingEvent);

            // assert
            Assert.IsTrue(rowAffected == 1);
        }
    }
}
