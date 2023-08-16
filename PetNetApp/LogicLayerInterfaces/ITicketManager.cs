using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface ITicketManager
    {
        /// <summary>
        /// William Rients
        /// Created: 2023/02/17
        /// 
        /// Selects a list of tickets
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <exception cref="Exception">No tickets to be selected</exception>
        /// <returns>List of ticket objects</returns>	
        List<TicketVM> RetrieveAllTickets();

        /// <summary>
        /// William Rients
        /// Created: 2023/03/03
        /// 
        /// Creates a ticket
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <exception cref="Exception">Creating ticket failed</exception>
        /// <returns>True or false if ticket was created</returns>
        bool CreateNewTicket(int UserId, string TicketStatusId, string TicketTitle, string TicketContext);

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/03/23
        /// 
        /// updates a ticket's status
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <exception cref="Exception">problem updating ticket</exception>
        /// <returns>List of ticket objects</returns>	
        bool EditTicketStatus(Ticket newTicket, Ticket oldTicket);

        /// <summary>
        /// Mads Rhea
        /// Created: 2023/04/20
        /// 
        /// Selects a list of all ticket status ids
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <exception cref="Exception">No ticketstatusids returned</exception>
        /// <returns>List of strings</returns>
        List<string> RetrieveAllTicketStatusId();

        /// <summary>
        /// Mads Rhea
        /// Created: 2023/04/20
        /// 
        /// Selects a list of all emails that've made tickets
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <exception cref="Exception">No tickets to be selected</exception>
        /// <returns>List of strings</returns>
        List<string> RetrieveAllEmailsFromTickets();

        /// <summary>
        /// Mads Rhea
        /// Created: 2023/04/20
        /// 
        /// Selects a list of all tickets by ticketstatusid
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <exception cref="Exception">No tickets to be selected</exception>
        /// <returns>List of TicketVM</returns>
        List<TicketVM> RetrieveTicketsByTicketStatusId(string ticketStatus);

        /// <summary>
        /// Mads Rhea
        /// Created: 2023/04/20
        /// 
        /// Selects a list of all tickets by email
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <exception cref="Exception">No tickets to be selected</exception>
        /// <returns>List of TicketVM</returns>
        List<TicketVM> RetrieveTicketsByEmail(string email);

        /// <summary>
        /// Mads Rhea
        /// Created: 2023/04/20
        /// 
        /// Selects a list of all tickets by date
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <exception cref="Exception">No tickets to be selected</exception>
        /// <returns>List of TicketVM</returns>
        List<TicketVM> RetrieveTicketsByDate(string startDate, string endDate = null);
    }
}
