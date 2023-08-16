using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class FilterDonation
    {
        public string FilterCategory { get; set; }
        public string FilterOption { get; set; }
        public int DateRange { get; set; }
        public List<DonationVM> DonoationList { get; set; }
        public decimal AmountRange { get; set; }
        public bool HasMessage { get; set; }
        public bool HasNoMessage { get; set; }
        public bool ShowFinancialDonations { get; set; }
        public bool ShowInKindDonations { get; set; }
        public bool ShowAnonymousDonations { get; set; }
    }
}
