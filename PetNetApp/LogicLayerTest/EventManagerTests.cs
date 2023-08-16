using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using DataAccessLayerFakes;
using DataObjects;


namespace LogicLayerTest
{
    [TestClass]
    public class EventManagerTests
    {
        private EventManager _eventManager = null;
        private EventAccessorFakes fake = null;
        //private List<Event> fakelist = null;

        [TestInitialize]
        public void TestSetup()
        {
            _eventManager = new EventManager(new EventAccessorFakes());
            fake = new EventAccessorFakes();
            //fakelist = fake.fakeEvents;
        }

        [TestMethod]
        public void TestSelectAllEvent()
        {
            int expectedCount = 2;
            int actualCount = 0;

            actualCount = fake.SelectAllEvent().Count;

            Assert.AreEqual(expectedCount, actualCount);
        }
        [TestMethod]
        public void TestDeleteEventByEventId()
        {

            Assert.AreEqual(true, _eventManager.DeleteEventByEventId(10000));
        }
        [TestMethod]
        public void TestUpdateEvent()
        {
            Event oldevent = new Event();
            oldevent.Eventid = 10000;
            oldevent.EventTitle = "sad";

            Event newevent = new Event();
            newevent.Eventid = 10000;
            newevent.EventTitle = "happy"; 

            bool expectedCount = true;
            bool actualCount = _eventManager.EditEvent( oldevent,newevent);


            Assert.AreEqual(expectedCount, actualCount);
        }
        [TestMethod]
        public void TestSelectAllShelter()
        {
            int expectedCount = 1;
            int actualCount = 0;

            actualCount = fake.SelectAllShelter().Count;

            Assert.AreEqual(expectedCount, actualCount);
        }
        [TestMethod]
        public void TestSelectAllEventType()
        {
            int expectedCount = 1;
            int actualCount = 0;

            actualCount = fake.SelectAllEventType().Count;

            Assert.AreEqual(expectedCount, actualCount);
        }
        [TestMethod]
        public void TestAddEvents()
        {
           

            bool expectedCount = true;
            bool actualCount = false;

            actualCount = _eventManager.AddEvent(fake.fakeEvents[0]);

            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
