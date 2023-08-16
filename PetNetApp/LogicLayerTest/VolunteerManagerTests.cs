/// <summary>
/// Oleksiy Fedchuk
/// Created: 2023/02/24
/// 
/// 
/// Class for testing the VolunteerManager class
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using DataAccessLayerFakes;
using DataObjects;
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
    public class VolunteerManagerTests
    {
        private VolunteerManager _volunteerManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _volunteerManager = new VolunteerManager(new VolunteerAccessorFakes());
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _volunteerManager = null;
        }

        [TestMethod]
        public void TestRetrieveVolunteersbyFundraisingId()
        {
            int expectedCount = 3;
            int actualCount = 0;
            int fundraisingId = 100000;

            var volunteers = _volunteerManager.RetrieveVolunteersbyFundraisingEventId(fundraisingId);
            actualCount = volunteers.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestRetrieveAllVolunteers()
        {
            int expectedCount = 5;
            int actualCount = 0;

            actualCount = _volunteerManager.RetrieveAllVolunteers().Count();

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestAddVolunteerToEventbyVolunteerAndEventId()
        {
            bool expected = true;
            bool actual = false;

            actual = _volunteerManager.AddVolunteerToEventbyVolunteerAndEventId(100010, 100000);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestRemoveVolunteerFromEventbyUsersIdAndFundraisingEventId()
        {
            bool expected = true;

            bool actual = _volunteerManager.RemoveVolunteerFromEventbyUsersIdAndFundraisingEventId(100000, 100000);

            Assert.AreEqual(expected, actual);
        }
    }
}
