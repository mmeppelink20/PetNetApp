using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DataAccessLayerFakes;
using LogicLayer;
using DataObjects;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerTest
{
    /// <summary>
    /// Summary description for TicketManagerTests
    /// </summary>
    [TestClass]
    public class TicketManagerTests
    {
        private TicketManager _ticketManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _ticketManager = new TicketManager(new TicketAccessorFakes());
        }

        [TestCleanup]
        public void testTearDown()
        {
            _ticketManager = null;
        }

        [TestMethod]
        public void TestRetrieveAllTickets()
        {
            // Arrange
            int expectedCount = 3;
            int actualCount = 0;

            // Act
            var tickets = _ticketManager.RetrieveAllTickets();
            actualCount = tickets.Count();

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestCreateTicket()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult;
            int userId = 100000;
            string ticketStatusId = "Open";
            string ticketTitle = "Broken mouse";
            string ticketContext = "My mouse is not working";

            // Act
            actualResult = _ticketManager.CreateNewTicket(userId, ticketStatusId, ticketTitle, ticketContext);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void EditTicketStatus()
        {
            bool result = false;
            bool expectedResult = true;
            TicketVM oldTicket = new TicketVM
            {
                TicketId = 100000,
                TicketStatusId = "Open"
            };
            TicketVM newTicket = new TicketVM
            {
                TicketId = 100000,
                TicketStatusId = "Closed"
            };

            result = _ticketManager.EditTicketStatus(newTicket, oldTicket);

            Assert.AreEqual(result, expectedResult);
        }

        [TestMethod]
        public void TestSelectAllTicketStatusId()
        {
            int result = 0;
            int expected = 2;

            result = _ticketManager.RetrieveAllTicketStatusId().Count;


            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestSelectTicketsByStatusId()
        {
            int result = 0;
            const string status = "Open";
            int expected = 3;

            result = _ticketManager.RetrieveTicketsByTicketStatusId(status).Count();

            Assert.AreEqual(result, expected);

        }

        [TestMethod]
        public void TestSelectTicketsByEmail()
        {
            int result = 0;
            const string email = "fakeEmail@company.com";
            int expected = 3;

            result = _ticketManager.RetrieveTicketsByEmail(email).Count();

            Assert.AreEqual(result, expected);

        }

        [TestMethod]
        public void TestSelectTicketsByEmailNotInDatabase()
        {
            int result = 0;
            const string email = "fakerEmail@company.com";
            int expected = 0;

            result = _ticketManager.RetrieveTicketsByEmail(email).Count();

            Assert.AreEqual(result, expected);

        }

        [TestMethod]
        public void TestSelectTicketsOnDate()
        {
            int result = 0;
            DateTime d = DateTime.Now;
            string date = d.ToShortDateString();
            int expected = 3;

            result = _ticketManager.RetrieveTicketsByDate(date).Count();

            Assert.AreEqual(result, expected);

        }

        [TestMethod]
        public void TestSelectTicketsBetweenStartDateAndEndDate()
        {
            int result = 0;
            DateTime sdate = DateTime.Now.AddHours(-1);
            DateTime edate = DateTime.Now.AddHours(1);
            string startDate = sdate.ToShortDateString();
            string endDate = edate.ToShortDateString();
            int expected = 3;

            result = _ticketManager.RetrieveTicketsByDate(startDate, endDate).Count();

            Assert.AreEqual(expected, result);

        }

        [TestMethod]
        public void TestSelectTicketsBetweenStartDateAndEndDateNoTickets()
        {
            int result = 0;
            DateTime sdate = DateTime.Now.AddDays(-2);
            DateTime edate = DateTime.Now.AddDays(-1);
            string startDate = sdate.ToShortDateString();
            string endDate = edate.ToShortDateString();
            int expected = 0;

            result = _ticketManager.RetrieveTicketsByDate(startDate, endDate).Count();

            Assert.AreEqual(expected, result);

        }
    }
}
