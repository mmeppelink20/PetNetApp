/// <summary>
/// Your Name
/// Created: 2023/03/01
/// </summary>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LogicLayerInterfaces;
using DataAccessLayerInterfaces;
using DataAccessLayerFakes;
using LogicLayer;
using DataObjects;
using System.Collections.Generic;

namespace LogicLayerTest
{
    [TestClass]
    public class ShelterItemTransactionManagerTests
    {
        private IShelterItemTransactionManager _shelterItemTransactionManager;

        [TestInitialize]
        public void TestSetup()
        {
            _shelterItemTransactionManager = new ShelterItemTransactionManager(new ShelterItemTransactionAccessorFakes());
        }

        /// <summary>
        /// Your Name
        /// Created: 2023/03/01
        /// </summary>
        [TestMethod]
        public void TestRetrieveInventoryTransactionByShelterIdReturnsTheCorrectNumberOfRecords()
        {
            List<ShelterItemTransactionVM> shelterItemTransactions;
            int expectedCount = 2;
            int shelterId = 55;

            shelterItemTransactions = _shelterItemTransactionManager.RetrieveInventoryTransactionByShelterId(shelterId);

            Assert.AreEqual(expectedCount, shelterItemTransactions.Count);
        }

        [TestMethod]
        public void TestAddItemTransaction()
        {
            ShelterItemTransaction transaction = new ShelterItemTransaction
            {
                ShelterItemTransactionId = 5,
                ShelterId = 55,
                ItemId = "Food",
                ChangedByUsersId = 2,
                InventoryChangeReasonId = "Check-in",
                QuantityIncrement = 8,
                DateChanged = DateTime.Now
            };

            bool expected = true;
            bool actual;

            actual = _shelterItemTransactionManager.AddItemTransaction(transaction);

            Assert.AreEqual(expected, actual);
        }
    }
}
