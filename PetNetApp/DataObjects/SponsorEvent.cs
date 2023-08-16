using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class SponsorEvent
    {
        public String Address { get; set; }
        public String CompanyName { get; set; }
        public String Title { get; set; }
        public DateTime StartDate { get; set; }
        public int FundraisingEventId { get; set; }
        public int InstitutionalEntityId { get; set; }
        public string ContactType { get; set; }
    }
}
