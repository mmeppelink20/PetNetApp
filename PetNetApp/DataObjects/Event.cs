using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Event
    {
        public int Eventid { get; set; }
        public String EventTypeid { get; set; }
        public int Shelterid { get; set; }
        public String EventTitle { get; set; }
        public String EventDescription { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
        public String EventAddress { get; set; }
        public String EventZipcode { get; set; }
        public bool EventVisible { get; set; }
        public String Zipcode { get; set; }
    }
    public class EventVM : Event
    {

    }
}
