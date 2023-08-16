/// <summary>
/// Chris Dreismeier
/// Created: 2023/02/09
/// 
/// Unit Test class to test all the logic of the ScheduleManager
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using LogicLayer;
using DataAccessLayerInterfaces;
using DataAccessLayerFakes;
using System.Collections.Generic;
using LogicLayerInterfaces;

namespace LogicLayerTest
{
    [TestClass]
    public class ScheduleManagerTests
    {
        private IScheduleManager _scheduleManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _scheduleManager = new ScheduleManager(new ScheduleAccessorFakes());
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _scheduleManager = null;
        }



        /// <summary>
        ///  Chris Dreismeier
        ///  2023/02/09
        /// </summary>
        [TestMethod]
        public void TestRetrieveScheduleByDate()
        {
            // arrange
            const int expectedCount = 2;
            DateTime selectedDate = new DateTime(DateTime.Now.Year, 2, 10, 7, 0, 0);
            int actualCount = 0;

            // act
            actualCount = _scheduleManager.RetrieveScheduleByDate(selectedDate).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);

        }

        [TestMethod]
        public void TestRetrieveScheduleByUserId()
        {
            // arrange
            const int expectedCount = 3;
            int userId = 100000;
            int actualCount = 0;

            // act
            actualCount = _scheduleManager.RetrieveScheduleByUserId(userId).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);

        }

        [TestMethod]
        public void TestAddSchedule()
        {
            // arrange
            ScheduleVM scheduleVM = new ScheduleVM();
            scheduleVM.UserId = 100000;
            scheduleVM.StartTime = DateTime.Now;
            scheduleVM.EndTime = DateTime.Now.AddHours(10);

            // act
            bool success = _scheduleManager.AddSchedulebyUserId(scheduleVM);

            // assert
            Assert.AreEqual(true, success);

        }

        [TestMethod]
        public void TestUpdateSchedule()
        {
            ScheduleVM oldScheduleVM = new ScheduleVM
            {
                ScheduleId = 100000,
                UserId = 100000,
                StartTime = new DateTime(DateTime.Now.Year, 2, 10, 7, 0, 0),
                EndTime = new DateTime(DateTime.Now.Year, 2, 10, 15, 0, 0),
                GivenName = "Test",
                FamilyName = "User"
            };
            ScheduleVM newScheduleVM = new ScheduleVM
            {
                ScheduleId = 100000,
                UserId = 100000,
                StartTime = new DateTime(DateTime.Now.Year, 2, 10, 7, 0, 0),
                EndTime = new DateTime(DateTime.Now.Year, 2, 10, 13, 30, 0),
                GivenName = "Test",
                FamilyName = "User"
            };

            bool actualResult = _scheduleManager.EditScheduleVM(oldScheduleVM, newScheduleVM);


            Assert.IsTrue(actualResult);

        }
    }
}
