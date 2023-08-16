/// <summary>
/// Zaid Rachman
/// Created: 2023/03/19
/// 
/// Unit test class for ItemManager
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;
using DataAccessLayerFakes;
using System.Collections.Generic;

namespace LogicLayerTest
{
    [TestClass]
    public class ItemManagerTests
    {
        ItemManager _itemManager = null;
        [TestInitialize]
        public void TestSetup()
        {
            _itemManager = new ItemManager(new ItemAccessorFakes());
        }

        [TestMethod]
        public void TestRetrieveItemByItemIdReturnsCorrectList()
        {
            string itemId = "Dog Food";
            int expectedResult = 4;
            int actualResult = _itemManager.RetrieveItemByItemId(itemId).CategoryId.Count;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestRetrieveAllCategories()
        {
            //arrange
            List<string> expected = new List<string> { "Cat", "Dog", "Bird", "Food", "Healthy", "Test" };

            //act
            List<string> actual = _itemManager.RetrieveAllCategories();

            //assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
            Assert.AreEqual(expected[4], actual[4]);
            Assert.AreEqual(expected[5], actual[5]);
        }

        [TestMethod]
        public void TestAddItemCategory()
        {
            //arrange
            string itemId = "Cat Food";
            string category = "Healthy";
            bool expected = true;
            bool actual;

            //act
            actual = _itemManager.AddItemCategory(itemId, category);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAddCategory()
        {
            //arrange
            string category = "Cleaning";
            bool expected = true;
            bool actual;

            //act
            actual = _itemManager.AddCategory(category);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestRemoveItemCategory()
        {
            //arrange
            string itemId = "Dog Food";
            string category = "Healthy";
            bool expected = true;
            bool actual;

            //act
            actual = _itemManager.RemoveItemCategory(itemId, category);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAddItem()
        {
            //arrange
            string itemId = "Dog Toy";
            bool expected = true;
            bool actual;

            //act
            actual = _itemManager.AddItem(itemId);

            //assert
            Assert.AreEqual(expected, actual);
        }
    }

}
