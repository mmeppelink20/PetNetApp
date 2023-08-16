/// <summary>
/// Brian Collum
/// Created: 2023/22/23
/// 
/// LibraryManagerTests tests the functionality of LibraryManager methods
/// 
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using DataAccessLayerFakes;
using LogicLayer;
using DataObjects;


namespace LogicLayerTest
{
    [TestClass]
    public class LibraryManagerTests
    {
        private LibraryManager _libraryManager = null;
        private List<Item> _libraryItemList = null;
        [TestInitialize]
        public void TestSetup()
        {
            _libraryManager = new LibraryManager(new LibraryAccessorFakes());
        }
        [TestMethod]
        public void TestGetLibraryItemList()
        {
            List<Item> testListFresh = new List<Item>();
            testListFresh.Add(new Item
            {
                ItemId = "Library Item One",
                CategoryId = new List<string> { "i1 Tag One", "i1 Tag Two" }
            });
            testListFresh.Add(new Item
            {
                ItemId = "Library Item Two",
                CategoryId = new List<string> { "i2 Tag One", "i2 Tag Two" }
            });

            List<Item> testListFakes = new List<Item>();
            testListFakes = _libraryManager.GetLibraryItemList();

            int freshListCount = testListFresh.Count;
            int fakeListCount = testListFakes.Count;

            Assert.AreEqual(fakeListCount, freshListCount);
            // List item 1
            Assert.AreEqual(testListFakes[0].ItemId, testListFresh[0].ItemId);
            Assert.AreEqual(testListFakes[0].CategoryId[0], testListFresh[0].CategoryId[0]);
            Assert.AreEqual(testListFakes[0].CategoryId[1], testListFresh[0].CategoryId[1]);
            // List item 2
            Assert.AreEqual(testListFakes[1].ItemId, testListFresh[1].ItemId);
            Assert.AreEqual(testListFakes[1].CategoryId[0], testListFresh[1].CategoryId[0]);
            Assert.AreEqual(testListFakes[1].CategoryId[1], testListFresh[1].CategoryId[1]);
        }
    }
}
