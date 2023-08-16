/// <summary>
/// Zaid Rachman
/// Created: 2023/03/19
/// 
/// ShelterInventoryItem and VM Object
/// </summary>
///
/// <remarks>
/// Brian Collum
/// Updated: 2023/04/06
/// Added ItemDisabled property, which indicates whether or not an item is visible in a shelter's inventory
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class ShelterInventoryItem
    {
        public int ShelterId { get; set; }
        public string ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal? UseStatistic { get; set; }
        public DateTime LastUpdated { get; set; }
        public int? LowInventoryThreshold { get; set; }
        public int? HighInventoryThreshold { get; set; }
        public bool InTransit { get; set; }
        public bool Urgent { get; set; }
        public bool Processing { get; set; }
        public bool DoNotOrder { get; set; }
        public string CustomFlag { get; set; }
        public bool ItemDisabled { get; set; }
    }
    public class ShelterInventoryItemVM : ShelterInventoryItem
    {
        public string ShelterName { get; set; }
        public string DisplayFlags { get; set; } //This is not a value in the database. Used for display purposes only
    }
}
