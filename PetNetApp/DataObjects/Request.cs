/// <summary>
/// Andrew Cromwell
/// Created: 2023/03/24
/// 
/// Class for the creation of Request Objects with set data fields.
/// This is the type of request for one shelter to send items to another shelter.
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Request
    {
        public int RequestId { get; set; }
        public int RecievingShelterId { get; set; }
        public int RequestedByUserId { get; set; }
        public DateTime RequestDate { get; set; }
        public bool Acknowledged { get; set; }
    }

    public class RequestResourceLine
    {
        public int RequestId { get; set; }
        public string ItemId { get; set; }
        public int QuantityRequested { get; set; }
        public int QuantityAvailable { get; set; }
        public string Notes { get; set; }
    }

    public class RequestVM : Request
    {
        public string RequestingShelterName { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public List<RequestResourceLine> RequestLines { get; set; } 
    }
}
