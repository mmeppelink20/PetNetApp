/// <summary>
/// Zaid Rachman
/// Created: 2023/03/19
/// 
/// ShelterInventoryItemAccessor fakes
/// </summary>
///
/// <remarks>
/// Brian Collum
/// Updated: 2023/04/07
/// Added ItemDisabled field to
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    public class ShelterInventoryItemFakes : IShelterInventoryItemAccessor
    {
        private List<ShelterInventoryItemVM> fakeShelterInventoryItemVMs = new List<ShelterInventoryItemVM>();

        public ShelterInventoryItemFakes()
        {
            fakeShelterInventoryItemVMs.Add(new ShelterInventoryItemVM
            {
                ShelterId = 999999,
                ItemId = "Apple",
                Quantity = 0,
                UseStatistic = 7.1m,
                LastUpdated = new DateTime(2000, 12, 12),
                LowInventoryThreshold = 5,
                HighInventoryThreshold = 10,
                InTransit = false,
                Urgent = false,
                Processing = false,
                DoNotOrder = false,
                CustomFlag = "Other information that may be important for the inventory item.",
                //VM
                ShelterName = "Shelter1",
                //CategoryId = "Food"
                ItemDisabled = false
            });
            fakeShelterInventoryItemVMs.Add(new ShelterInventoryItemVM
            {
                ShelterId = 999999,
                ItemId = "Orange",
                Quantity = 0,
                UseStatistic = 7.1m,
                LastUpdated = new DateTime(2000, 12, 12),
                LowInventoryThreshold = 5,
                HighInventoryThreshold = 10,
                InTransit = false,
                Urgent = false,
                Processing = false,
                DoNotOrder = false,
                CustomFlag = "Other information that may be important for the inventory item.",
                //VM
                ShelterName = "Shelter2",
                //CategoryId = "Food"
                ItemDisabled = true
            });
            fakeShelterInventoryItemVMs.Add(new ShelterInventoryItemVM
            {
                ShelterId = 999998,
                ItemId = "Water",
                Quantity = 0,
                UseStatistic = 7.1m,
                LastUpdated = new DateTime(2000, 12, 12),
                LowInventoryThreshold = 5,
                HighInventoryThreshold = 10,
                InTransit = false,
                Urgent = false,
                Processing = false,
                DoNotOrder = false,
                CustomFlag = "Other information that may be important for the inventory item.",
                //VM
                ShelterName = "Shelter3",
                //CategoryId = "liquid"
                ItemDisabled = false
            });
        }

        public List<ShelterInventoryItemVM> SelectInventoryByShelter(int shelterId)
        {
            List<ShelterInventoryItemVM> shelterInventoryItemVMs = new List<ShelterInventoryItemVM>();
            foreach (ShelterInventoryItemVM shelterInventoryItemVM in fakeShelterInventoryItemVMs)
            {
                if (shelterInventoryItemVM.ShelterId == shelterId)
                {
                    shelterInventoryItemVMs.Add(shelterInventoryItemVM);
                }
            }
            return shelterInventoryItemVMs;
        }

        public ShelterInventoryItemVM SelectShelterInventoryItemByShelterIdAndItemId(int shelterId, string itemId)
        {
            ShelterInventoryItemVM shelterInventoryItemVMs = new ShelterInventoryItemVM();
            foreach (ShelterInventoryItemVM shelterInventoryItemVM in fakeShelterInventoryItemVMs)
            {
                if (shelterInventoryItemVM.ShelterId == shelterId && shelterInventoryItemVM.ItemId == itemId)
                {
                    return shelterInventoryItemVM;
                }
            }
            return shelterInventoryItemVMs; //Will return null if shelter is not found
        }

        public int UpdateShelterInventoryItem(ShelterInventoryItemVM oldShelterInventoryItemVM, ShelterInventoryItemVM newShelterInventoryItemVM)
        {
            int rowsAffected = 0;
            for (int i = 0; i < fakeShelterInventoryItemVMs.Count; i++)
            {
                if (fakeShelterInventoryItemVMs[i].ShelterId == oldShelterInventoryItemVM.ShelterId &&
                   fakeShelterInventoryItemVMs[i].ItemId == oldShelterInventoryItemVM.ItemId)
                {
                    fakeShelterInventoryItemVMs[i].LowInventoryThreshold = newShelterInventoryItemVM.LowInventoryThreshold;
                    fakeShelterInventoryItemVMs[i].HighInventoryThreshold = newShelterInventoryItemVM.HighInventoryThreshold;
                    fakeShelterInventoryItemVMs[i].InTransit = newShelterInventoryItemVM.InTransit;
                    fakeShelterInventoryItemVMs[i].LastUpdated = newShelterInventoryItemVM.LastUpdated;
                    fakeShelterInventoryItemVMs[i].Processing = newShelterInventoryItemVM.Processing;
                    fakeShelterInventoryItemVMs[i].Quantity = newShelterInventoryItemVM.Quantity;
                    fakeShelterInventoryItemVMs[i].ShelterName = newShelterInventoryItemVM.ShelterName;
                    fakeShelterInventoryItemVMs[i].CustomFlag = newShelterInventoryItemVM.CustomFlag;
                    fakeShelterInventoryItemVMs[i].DoNotOrder = newShelterInventoryItemVM.DoNotOrder;
                    fakeShelterInventoryItemVMs[i].Urgent = newShelterInventoryItemVM.Urgent;
                    fakeShelterInventoryItemVMs[i].UseStatistic = newShelterInventoryItemVM.UseStatistic;
                    rowsAffected++;
                }
            }
            return rowsAffected;

        }
        public int EnableOrDisableShelterInventoryItem(int shelterID, string itemID, bool disableItem = false)
        {
            try
            {
                ShelterInventoryItemVM targetItem = SelectShelterInventoryItemByShelterIdAndItemId(shelterID, itemID);
                bool itemIsDisabled = targetItem.ItemDisabled;
                if (disableItem) // Trying to disable item
                {
                    if (itemIsDisabled) // Trying to disable item, item is already disabled, do nothing
                    {
                        return 0;
                    }
                    else
                    {
                        // Trying to disable item and item is enabled, disable it
                    }
                    {
                        targetItem.ItemDisabled = true;
                        return 1;
                    }
                }
                else // Trying to enable item
                {
                    if (itemIsDisabled) // Trying to enable item, item is disabled, enable it
                    {
                        targetItem.ItemDisabled = false;
                        return 1;
                    }
                    else // Trying to enable item, item is disabled, enable it
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertNewShelterInventoryItemFromLibrary(int shelterID, string itemID)
        {
            int result = 0;
            // Create a new Library Item to try to add
            Item testLibraryItem = new Item { ItemId = itemID, CategoryId = null };
            ShelterInventoryItemVM testShelterInventoryItemVM = new ShelterInventoryItemVM
            {
                ShelterId = shelterID,
                ItemId = testLibraryItem.ItemId,    // Get ItemId from fictional libraryItem
                Quantity = 0,
                UseStatistic = null,
                LastUpdated = DateTime.Now,
                LowInventoryThreshold = null,
                HighInventoryThreshold = null,
                InTransit = false,
                Urgent = false,
                Processing = false,
                DoNotOrder = false,
                CustomFlag = null,
                ItemDisabled = false
            };
            // Select shelter to add the item to
            List<ShelterInventoryItemVM> shelterInventory = SelectInventoryByShelter(shelterID);
            // Confirm item does not exist in the selected shelter's inventory
            if (shelterInventory.Exists(x => x.ItemId == itemID))
            {
                throw new ApplicationException("Item already exists!");
            }
            // Add the item to the main fakes list
            else
            {
                fakeShelterInventoryItemVMs.Add(testShelterInventoryItemVM);
                result++;
            }
            return result;
        }
    }
}
