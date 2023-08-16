using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class ResourceAddRequest
    {
        public int ResourceAddRequestId { get; set; }
        public int ShelterId { get; set; }
        public int UsersId { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public bool Active { get; set; }
    }

    public class ResourceAddRequestVM : ResourceAddRequest
    {
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
    }
}
