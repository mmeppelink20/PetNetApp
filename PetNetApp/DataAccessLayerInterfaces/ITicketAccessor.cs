using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerInterfaces
{
    public interface ITicketAccessor
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
        List<TicketVM> SelectAllTickets();

        /// <summary>
        /// William Rients
        /// Created: 2023/03/03
        /// 
        /// Inserts a ticket
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <exception cref="Exception">Inserting ticket failed</exception>
        /// <returns>Rows Affected</returns>
        int InsertTicket(int UserId, string TicketStatusId, string TicketTitle, string TicketContext);

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/02/16
        /// 
        /// updates a ticket status by TicketID
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="newTicket">ticket to be updated</param>
        /// <param name="oldTicket">old ticket </param>
        /// <exception cref="Exception">Update Fails</exception>
        /// <returns>Rows affected</returns>	
        int UpdateTicketStatus(Ticket newTicket, Ticket oldTicket);

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
        List<string> SelectAllTicketStatusId();

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
        List<string> SelectEmailsByTickets();

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
        List<TicketVM> SelectTicketsByTicketStatusId(string ticketStatus);

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
        List<TicketVM> SelectTicketsByEmail(string email);

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
        List<TicketVM> SelectTicketsByDate(string startDate, string endDate = null);
    }
}
