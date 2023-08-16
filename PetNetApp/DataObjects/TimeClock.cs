using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class TimeClock
    {
        public int TimeClockId { get; set; }
        public int UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }

    public class TimeClockVM
    {
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
    }
}
