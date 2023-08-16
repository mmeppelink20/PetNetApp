using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Ticket
    {
        public int TicketId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string TicketStatusId { get; set; }
        [Required]
        public string TicketTitle { get; set; }
        [Required]
        public string TicketContext { get; set; }
        public DateTime TicketDate { get; set; }
        public bool TicketActive { get; set; }
    }

    public class TicketVM : Ticket
    {
        public string Email { get; set; }
    }
}
