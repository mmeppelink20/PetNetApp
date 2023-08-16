/// <summary>
/// Your Name
/// Created: 2023/02/28
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class ShelterItemTransaction
    {
        public int ShelterItemTransactionId { get; set; }
        public int ShelterId { get; set; }
        public string ItemId { get; set; }
        public int ChangedByUsersId { get; set; }
        public string InventoryChangeReasonId { get; set; }
        public int QuantityIncrement { get; set; }
        public DateTime DateChanged { get; set; }
    }

    public class ShelterItemTransactionVM : ShelterItemTransaction
    {
        public string ShelterName { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string ReasonDescription { get; set; }
    }
}
