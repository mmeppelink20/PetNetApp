using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessLayerInterfaces;
using DataAccessLayer;

namespace LogicLayer
{
    public class TicketManager : ITicketManager
    {
        private ITicketAccessor _ticketAccessor = null;

        public TicketManager()
        {
            _ticketAccessor = new TicketAccessor();
        }

        public TicketManager(ITicketAccessor ticketAccessor)
        {
            _ticketAccessor = ticketAccessor;
        }

        public bool CreateNewTicket(int UserId, string TicketStatusId, string TicketTitle, string TicketContext)
        {
            bool result = false;

            try
            {
                if (0 < _ticketAccessor.InsertTicket(UserId, TicketStatusId, TicketTitle, TicketContext))
                {
                    result = true;
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to create new ticket.", e);
            }
            return result;
        }

        public List<TicketVM> RetrieveAllTickets()
        {
            try
            {
                return _ticketAccessor.SelectAllTickets();
            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to retrieve tickets", e);
            }
        }

        public bool EditTicketStatus(Ticket newTicket, Ticket oldTicket)
        {
            bool result = false;
            try
            {
                result = 1 == _ticketAccessor.UpdateTicketStatus(newTicket, oldTicket);
                if (!result)
                {
                    throw new ApplicationException("\na data concurrency error occured, refreshing page; try again.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        public List<string> RetrieveAllTicketStatusId()
        {
            try
            {
                return _ticketAccessor.SelectAllTicketStatusId();
            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to retrieve ticket statuses", e);
            }
        }

        public List<string> RetrieveAllEmailsFromTickets()
        {
            try
            {
                return _ticketAccessor.SelectEmailsByTickets();
            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to retrieve emails", e);
            }
        }

        public List<TicketVM> RetrieveTicketsByTicketStatusId(string ticketStatus)
        {
            try
            {
                return _ticketAccessor.SelectTicketsByTicketStatusId(ticketStatus);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to retrieve tickets", e);
            }
        }

        public List<TicketVM> RetrieveTicketsByEmail(string email)
        {
            try
            {
                return _ticketAccessor.SelectTicketsByEmail(email);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to retrieve tickets", e);
            }
        }

        public List<TicketVM> RetrieveTicketsByDate(string startDate, string endDate = null)
        {
            try
            {
                return _ticketAccessor.SelectTicketsByDate(startDate, endDate);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to retrieve tickets", e);
            }
        }
    }
}
