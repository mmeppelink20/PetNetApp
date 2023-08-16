using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    public class TicketAccessorFakes : ITicketAccessor
    {
        List<TicketVM> fakeTickVMs = new List<TicketVM>();
        TicketVM fakeTicketVM = new TicketVM();
        List<string> fakeTicketStatusId = new List<string>();

        public TicketAccessorFakes()
        {
            fakeTickVMs.Add(new TicketVM
            {
                TicketId = 100000,
                UserId = 100000,
                TicketStatusId = "Open",
                TicketTitle = "Want to speak with admin",
                TicketDate = DateTime.Now,
                TicketActive = true,
                Email = "fakeEmail@company.com"
            });

            fakeTickVMs.Add(new TicketVM
            {
                TicketId = 100001,
                UserId = 100001,
                TicketStatusId = "Open",
                TicketTitle = "Want to speak with admin",
                TicketDate = DateTime.Now,
                TicketActive = true,
                Email = "fakeEmail@company.com"
            });

            fakeTickVMs.Add(new TicketVM
            {
                TicketId = 100002,
                UserId = 100002,
                TicketStatusId = "Open",
                TicketTitle = "Want to speak with admin",
                TicketDate = DateTime.Now,
                TicketActive = true,
                Email = "fakeEmail@company.com"
            });

            fakeTicketStatusId.Add("Open");
            fakeTicketStatusId.Add("Closed");
        }

        public int InsertTicket(int UserId, string TicketStatusId, string TicketTitle, string TicketContext)
        {
            int result = 0;
            int existing = fakeTickVMs.Count;
            fakeTickVMs.Add(fakeTicketVM);
            result = fakeTickVMs.Count - existing;
            return result;
        }

        public List<TicketVM> SelectAllTickets()
        {
            return fakeTickVMs;
        }

        public int UpdateTicketStatus(Ticket newTicket, Ticket oldTicket)
        {
            int result = 0;

            for (int i = 0; i < fakeTickVMs.Count; i++)
            {
                if (fakeTickVMs[i].TicketId == oldTicket.TicketId)
                {
                    fakeTickVMs[i].TicketStatusId = newTicket.TicketStatusId;

                    result++;
                    break;
                }
            }

            return result;
        }

        public List<string> SelectAllTicketStatusId()
        {
            return fakeTicketStatusId;
        }

        public List<string> SelectEmailsByTickets()
        {
            List<string> emails = new List<string>();

            foreach (var ticket in fakeTickVMs)
            {
                emails.Add(ticket.Email);
            }

            return emails;
        }

        public List<TicketVM> SelectTicketsByEmail(string email)
        {
            List<TicketVM> tickets = new List<TicketVM>();

            foreach (var ticket in fakeTickVMs)
            {
                if (ticket.Email == email) { tickets.Add(ticket); }
            }

            return tickets;

        }

        public List<TicketVM> SelectTicketsByTicketStatusId(string ticketStatus)
        {
            List<TicketVM> tickets = new List<TicketVM>();

            foreach (var ticket in fakeTickVMs)
            {
                if (ticket.TicketStatusId == ticketStatus) { tickets.Add(ticket); }
            }

            return tickets;
        }

        public List<TicketVM> SelectTicketsByDate(string startDate, string endDate = null)
        {
            List<TicketVM> tickets = new List<TicketVM>();
            DateTime sDate = Convert.ToDateTime(startDate);
            DateTime eDate = Convert.ToDateTime(endDate);

            foreach (var ticket in fakeTickVMs)
            {
                if (ticket.TicketDate.ToShortDateString().Equals(startDate) || (ticket.TicketDate >= sDate && ticket.TicketDate <= eDate)) { tickets.Add(ticket); }
            }

            return tickets;
        }
    }
}
