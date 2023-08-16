using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class InstitutionalEntity
    {
        public int InstitutionalEntityId { get; set; }
        public string CompanyName { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Zipcode { get; set; }
        public string ContactType { get; set; }
        public int ShelterId { get; set; }
    }
}
