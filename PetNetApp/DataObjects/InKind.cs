using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class InKind
    {
        public int InKindId { get; set; }
        public int DonationId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Target { get; set; }
        public bool Recieved { get; set; }
    }
}
