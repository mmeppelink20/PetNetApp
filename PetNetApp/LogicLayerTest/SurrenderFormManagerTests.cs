using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerFakes;

namespace LogicLayerTest
{
    [TestClass]
    public class SurrenderFormManagerTests
    {
        private SurrenderFormManager surrenderFormManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            surrenderFormManager = new SurrenderFormManager(new SurrenderFormFakes());
        }


        [TestMethod]
        public void TestInsertSurrenderForm()
        {
            bool expected = true;
            bool actual = false;
            actual = surrenderFormManager.InsertSurrenderForm("Cat", "Bye", false, "3192222222", "person@email.com");

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestRetrieveAllSurrenderForms()
        {
            int expected = 3;
            int actual = 0;

            actual = surrenderFormManager.RetrieveAllSurrenderForms().Count();

            Assert.AreEqual(expected, actual);

        }


        [TestMethod]
        public void TestRemoveSurrenderForm()
        {
            bool test = true;

            bool actual = surrenderFormManager.RemoveSurrenderForm(100000);

            Assert.AreEqual(test, actual);
        }

    }
}
