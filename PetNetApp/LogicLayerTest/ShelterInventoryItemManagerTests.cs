/// <summary>
/// Zaid Rachman
/// Created: 2023/03/19
/// 
/// This is the unit test class for the ShelterInventoryItemManager
/// </summary>
///
/// <remarks>
/// Brian Collum
/// Updated: 2023/04/06
/// Added support for ItemDisabled property of ShelterInventoryItem objects
/// Differentiated RetrieveInventoryByShelterId and RetrieveFullInventoryByShelterId by showing disabled items
/// </remarks>
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using DataObjects;
using LogicLayer;
using DataAccessLayerFakes;

namespace LogicLayerTest
{
    [TestClass]
    public class ShelterInventoryItemManagerTests
    {
        ShelterInventoryItemManager _shelterInventoryItemManager = null;
        [TestInitialize]
        public void TestSetUp()
        {
            _shelterInventoryItemManager = new ShelterInventoryItemManager(new ShelterInventoryItemFakes());
        }
        [TestMethod]
        public void TestRetrieveShelterInventoryItemReturnsCorrectList()
        {
            int shelterId = 999999;
            int expectedResult = 1;
            int actualResult = _shelterInventoryItemManager.RetrieveInventoryByShelterId(shelterId).Count;
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void TestRetrieveFullShelterInventoryItemReturnsCorrectList()
        {
            int shelterId = 999999;
            int expectedResult = 2;
            int actualResult = _shelterInventoryItemManager.RetrieveFullInventoryByShelterId(shelterId).Count;
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void TestRetrieveShelterInventoryItemByShelterIdandItemIdReturnsCorrectList()
        {
            int shelterId = 999999;
            string itemId = "Apple";
            ShelterInventoryItemVM testShelterInventoryItemVM = new ShelterInventoryItemVM();
            testShelterInventoryItemVM = _shelterInventoryItemManager.RetrieveInventoryItemByShelterIdAndItemId(shelterId, itemId);
            int expectedResult = 999999;
            int actualResult = testShelterInventoryItemVM.ShelterId;
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void TestEditShelterInventoryItem()
        {
            ShelterInventoryItemVM testOldShelterInventoryItemVM = new ShelterInventoryItemVM
            {
                ShelterId = 999999,
                ItemId = "Apple",
                Quantity = 0,
                UseStatistic = 7.1m,
                LastUpdated = new DateTime(2000, 12, 12),
                LowInventoryThreshold = 5,
                HighInventoryThreshold = 10,
                InTransit = false,
                Urgent = false,
                Processing = false,
                DoNotOrder = false,
                CustomFlag = "Other information that may be important for the inventory item.",
                //VM
                ShelterName = "Shelter1",
                ItemDisabled = true
            };
            ShelterInventoryItemVM testNewShelterInventoryItemVM = new ShelterInventoryItemVM
            {
                ShelterId = 999999,
                ItemId = "Apple",
                Quantity = 37,
                UseStatistic = 7.1m,
                LastUpdated = DateTime.Now,
                LowInventoryThreshold = 5,
                HighInventoryThreshold = 10,
                InTransit = true,
                Urgent = false,
                Processing = false,
                DoNotOrder = false,
                CustomFlag = "Other information that may be important for the inventory item.",
                //VM
                ShelterName = "Shelter1",
                ItemDisabled = true
            };

        }
        [TestMethod]
        public void TestEnableShelterInventoryItem()
        {
            List<ShelterInventoryItemVM> fakesList = _shelterInventoryItemManager.RetrieveFullInventoryByShelterId(999999);
            List<ShelterInventoryItemVM> localList = new List<ShelterInventoryItemVM>();
            ShelterInventoryItemVM testItemOne = new ShelterInventoryItemVM
            {
                ShelterId = 999999,
                ItemId = "Apple",
                Quantity = 0,
                UseStatistic = 7.1m,
                LastUpdated = new DateTime(2000, 12, 12),
                LowInventoryThreshold = 5,
                HighInventoryThreshold = 10,
                InTransit = false,
                Urgent = false,
                Processing = false,
                DoNotOrder = false,
                CustomFlag = "Other information that may be important for the inventory item.",
                //VM
                ShelterName = "Shelter1",
                //CategoryId = "Food"
                ItemDisabled = false
            };
            localList.Add(testItemOne);
            ShelterInventoryItemVM testItemTwo = new ShelterInventoryItemVM
            {
                ShelterId = 999999,
                ItemId = "Orange",
                Quantity = 0,
                UseStatistic = 7.1m,
                LastUpdated = new DateTime(2000, 12, 12),
                LowInventoryThreshold = 5,
                HighInventoryThreshold = 10,
                InTransit = false,
                Urgent = false,
                Processing = false,
                DoNotOrder = false,
                CustomFlag = "Other information that may be important for the inventory item.",
                //VM
                ShelterName = "Shelter2",
                //CategoryId = "Food"
                ItemDisabled = true
            };
            localList.Add(testItemTwo);
            // Confirm local list matches fakes
            Assert.AreEqual(localList[0].ShelterId, fakesList[0].ShelterId);
            Assert.AreEqual(localList[0].ItemId, fakesList[0].ItemId);
            Assert.AreEqual(localList[0].ItemDisabled, fakesList[0].ItemDisabled);
            Assert.AreEqual(localList[1].ShelterId, fakesList[1].ShelterId);
            Assert.AreEqual(localList[1].ItemId, fakesList[1].ItemId);
            Assert.AreEqual(localList[1].ItemDisabled, fakesList[1].ItemDisabled);

            // Attempt to enable item zero in the fake list (It is already enabled)
            _shelterInventoryItemManager.EnableShelterInventoryItem(fakesList[0].ShelterId, fakesList[0].ItemId);
            // Confirm that fake item zero is still enabled by comparing it to local item zero (unchanged)
            Assert.AreEqual(localList[0].ItemDisabled, fakesList[0].ItemDisabled);

            // Attempt to enable item one in the fake list (It is disabled)
            _shelterInventoryItemManager.EnableShelterInventoryItem(fakesList[1].ShelterId, fakesList[1].ItemId);
            // Confirm that fake item one is still enabled by comparing it to local item one (unchanged)
            Assert.AreNotEqual(localList[1].ItemDisabled, fakesList[1].ItemDisabled);

        }
        [TestMethod]
        public void TestDisableShelterInventoryItem()
        {
            List<ShelterInventoryItemVM> fakesList = _shelterInventoryItemManager.RetrieveFullInventoryByShelterId(999999);
            List<ShelterInventoryItemVM> localList = new List<ShelterInventoryItemVM>();
            ShelterInventoryItemVM testItemOne = new ShelterInventoryItemVM
            {
                ShelterId = 999999,
                ItemId = "Apple",
                Quantity = 0,
                UseStatistic = 7.1m,
                LastUpdated = new DateTime(2000, 12, 12),
                LowInventoryThreshold = 5,
                HighInventoryThreshold = 10,
                InTransit = false,
                Urgent = false,
                Processing = false,
                DoNotOrder = false,
                CustomFlag = "Other information that may be important for the inventory item.",
                //VM
                ShelterName = "Shelter1",
                //CategoryId = "Food"
                ItemDisabled = false
            };
            localList.Add(testItemOne);
            ShelterInventoryItemVM testItemTwo = new ShelterInventoryItemVM
            {
                ShelterId = 999999,
                ItemId = "Orange",
                Quantity = 0,
                UseStatistic = 7.1m,
                LastUpdated = new DateTime(2000, 12, 12),
                LowInventoryThreshold = 5,
                HighInventoryThreshold = 10,
                InTransit = false,
                Urgent = false,
                Processing = false,
                DoNotOrder = false,
                CustomFlag = "Other information that may be important for the inventory item.",
                //VM
                ShelterName = "Shelter2",
                //CategoryId = "Food"
                ItemDisabled = true
            };
            localList.Add(testItemTwo);
            // Confirm local list matches fakes
            Assert.AreEqual(localList[0].ShelterId, fakesList[0].ShelterId);
            Assert.AreEqual(localList[0].ItemId, fakesList[0].ItemId);
            Assert.AreEqual(localList[0].ItemDisabled, fakesList[0].ItemDisabled);
            Assert.AreEqual(localList[1].ShelterId, fakesList[1].ShelterId);
            Assert.AreEqual(localList[1].ItemId, fakesList[1].ItemId);
            Assert.AreEqual(localList[1].ItemDisabled, fakesList[1].ItemDisabled);

            // Attempt to disable item zero in the fake list (It is enabled)
            _shelterInventoryItemManager.DisableShelterInventoryItem(fakesList[0].ShelterId, fakesList[0].ItemId);
            // Confirm that fake item zero is disabled by comparing it to local item zero (unchanged)
            Assert.AreNotEqual(localList[0].ItemDisabled, fakesList[0].ItemDisabled);

            // Attempt to disable item one in the fake list (It is already disabled)
            _shelterInventoryItemManager.DisableShelterInventoryItem(fakesList[1].ShelterId, fakesList[1].ItemId);
            // Confirm that fake item one is still disabled by comparing it to local item one (unchanged)
            Assert.AreEqual(localList[1].ItemDisabled, fakesList[1].ItemDisabled);
        }
        [TestMethod]
        public void TestAddToShelterInventory()
        {
            // Test library item to add (Mirrored in fakes)
            Item testLibraryItem = new Item { ItemId = "Salamander food", CategoryId = new List<string> { "Salamander", "food" } };

            // Test Shelter
            List<ShelterInventoryItemVM> fakesList = _shelterInventoryItemManager.RetrieveFullInventoryByShelterId(999999);
            Assert.AreEqual(fakesList[0].ItemId, "Apple");
            Assert.AreEqual(fakesList[0].ItemDisabled, false);
            Assert.AreEqual(fakesList[1].ItemId, "Orange");
            Assert.AreEqual(fakesList[1].ItemDisabled, true);
            // Confirm adding an existing library item to a shelter that has it already works properly
            // Adding an item that exists and is enabled should do nothing
            Assert.IsFalse(_shelterInventoryItemManager.AddToShelterInventory(999999, "Apple"));    // Attempting to enable item returns false
            Assert.AreEqual(fakesList[0].ItemDisabled, false);  // Item is still enabled
            // Adding an item that exists and is disabled enable the item
            Assert.IsTrue(_shelterInventoryItemManager.AddToShelterInventory(999999, "Orange"));   // Attempting to enable item returns true
            Assert.AreEqual(fakesList[1].ItemDisabled, false);  // Item is now enabled

            // Test adding new item
            // Confirm item is not in shelter inventory
            Assert.AreEqual(fakesList.Count, 2);
            // Add new item
            _shelterInventoryItemManager.AddToShelterInventory(999999, testLibraryItem.ItemId);
            // refresh shelter inventory and confirm it has grown
            fakesList = _shelterInventoryItemManager.RetrieveFullInventoryByShelterId(999999);
            Assert.AreEqual(fakesList.Count, 3);
            // Create a local fake to mirror what the item should look like
            ShelterInventoryItemVM localFakeItem = new ShelterInventoryItemVM
            {
                ShelterId = 999999,
                ItemId = "Salamander food",
                Quantity = 0,
                UseStatistic = null,
                LastUpdated = DateTime.Now,
                LowInventoryThreshold = null,
                HighInventoryThreshold = null,
                InTransit = false,
                Urgent = false,
                Processing = false,
                DoNotOrder = false,
                CustomFlag = null,
                ItemDisabled = false
            };
            // Confirm properties of newly added item with local fake
            Assert.AreEqual(localFakeItem.ShelterId, fakesList[2].ShelterId);
            Assert.AreEqual(localFakeItem.ItemId, fakesList[2].ItemId);
            Assert.AreEqual(localFakeItem.Quantity, fakesList[2].Quantity);
            Assert.AreEqual(localFakeItem.UseStatistic, fakesList[2].UseStatistic);
            // Assert.AreEqual(localFakeItem.LastUpdated, fakesList[2].LastUpdated);   // Since these are generated with DateTime.Now, they are not necessarily equal
            Assert.AreEqual(localFakeItem.LowInventoryThreshold, fakesList[2].LowInventoryThreshold);
            Assert.AreEqual(localFakeItem.HighInventoryThreshold, fakesList[2].HighInventoryThreshold);
            Assert.AreEqual(localFakeItem.InTransit, fakesList[2].InTransit);
            Assert.AreEqual(localFakeItem.Urgent, fakesList[2].Urgent);
            Assert.AreEqual(localFakeItem.Processing, fakesList[2].Processing);
            Assert.AreEqual(localFakeItem.DoNotOrder, fakesList[2].DoNotOrder);
            Assert.AreEqual(localFakeItem.CustomFlag, fakesList[2].CustomFlag);
            Assert.AreEqual(localFakeItem.ItemDisabled, fakesList[2].ItemDisabled);
            // Item successfully added
        }
    }
}
