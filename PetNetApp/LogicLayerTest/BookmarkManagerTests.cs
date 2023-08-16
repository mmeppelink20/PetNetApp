using DataAccessLayerFakes;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerTest
{
    [TestClass]


    public class BookmarkManagerTests
    {
   
        private BookmarkManager bookmarkManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            bookmarkManager = new BookmarkManager(new BookmarkFakes());
        }

        [TestMethod]
        public void TestInsertBookmark()
        {
            bool expected = true;
            bool actual = false;
            actual = bookmarkManager.AddBookmark(100001, 100000);

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void TestRetrieveAllBookmarks()
        {
            int expected = 3;
            int actual = 0;

            actual = bookmarkManager.RetrieveAllBookmarks(100001).Count();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestRemoveBookmark()
        {
            bool test = true;

            bool actual = bookmarkManager.DeleteBookmark(100031, 100002);

            Assert.AreEqual(test, actual);
        }

     
    }
}
