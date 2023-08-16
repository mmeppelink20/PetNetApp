/// <summary>
/// Zaid Rachman
/// Created: 2023/03/19
/// 
/// Logic layer for ShelterInventoryManager
/// </summary>
///
/// <remarks>
/// Brian Collum
/// Updated: 2023/04/07
/// 
/// Split RetrieveInventoryByShelterId into RetrieveInventoryByShelterId and RetrieveFullInventoryByShelterId
/// Added AddToShelterInventory, EnableShelterInventoryItem, and DisableShelterInventoryItem
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessLayer;
using DataAccessLayerInterfaces;

namespace LogicLayer
{
    public class ShelterInventoryItemManager : IShelterInventoryItemManager
    {
        private IShelterInventoryItemAccessor _shelterInventoryItemAccessor = null;
        public ShelterInventoryItemManager()
        {
            _shelterInventoryItemAccessor = new ShelterInventoryItemAccessor();
        }
        public ShelterInventoryItemManager(IShelterInventoryItemAccessor shelterInventoryItemAccessor)
        {
            _shelterInventoryItemAccessor = shelterInventoryItemAccessor;
        }

        public bool EditShelterInventoryItem(ShelterInventoryItemVM oldShelterInventoryItemVM, ShelterInventoryItemVM newShelterInventoryItemVM)
        {
            bool result = false;
            try
            {
                result = (1 == _shelterInventoryItemAccessor.UpdateShelterInventoryItem(oldShelterInventoryItemVM, newShelterInventoryItemVM));
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        public List<ShelterInventoryItemVM> RetrieveInventoryByShelterId(int shelterId)
        {

            List<ShelterInventoryItemVM> shelterInventoryItemVMs = null;
            try
            {
                shelterInventoryItemVMs = _shelterInventoryItemAccessor.SelectInventoryByShelter(shelterId);
                shelterInventoryItemVMs.RemoveAll(item => item.ItemDisabled == true);   // Remove all items that have been disabled
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Data not found", ex);
            }
            return shelterInventoryItemVMs;
        }
        public List<ShelterInventoryItemVM> RetrieveFullInventoryByShelterId(int shelterId)
        {

            List<ShelterInventoryItemVM> shelterInventoryItemVMs = null;
            try
            {
                shelterInventoryItemVMs = _shelterInventoryItemAccessor.SelectInventoryByShelter(shelterId);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Data not found", ex);
            }
            return shelterInventoryItemVMs;
        }

        public ShelterInventoryItemVM RetrieveInventoryItemByShelterIdAndItemId(int shelterId, string itemId)
        {
            ShelterInventoryItemVM shelterInventoryItemVMs = null;
            try
            {
                shelterInventoryItemVMs = _shelterInventoryItemAccessor.SelectShelterInventoryItemByShelterIdAndItemId(shelterId, itemId);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Data not found", ex);
            }
            return shelterInventoryItemVMs;
        }

        public bool AddToShelterInventory(int shelterID, string itemID)
        {
            bool result = false;
            // First, check if the item is already in the inventory
            List<ShelterInventoryItemVM> fullInventoryList = RetrieveFullInventoryByShelterId(shelterID);
            if (fullInventoryList.Exists(x => x.ItemId == itemID))
            {
                result = EnableShelterInventoryItem(shelterID, itemID);   // Item already exists in db, enable it
            }
            else
            {
                try
                {
                    result = (1 == _shelterInventoryItemAccessor.InsertNewShelterInventoryItemFromLibrary(shelterID, itemID));
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Failed to add shelter", ex);
                }
            }
            return result;
        }

        public bool EnableShelterInventoryItem(int shelterID, string itemID)
        {
            bool result = false;
            try
            {
                result = (1 == _shelterInventoryItemAccessor.EnableOrDisableShelterInventoryItem(shelterID, itemID));
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public bool DisableShelterInventoryItem(int shelterID, string itemID)
        {
            bool result = false;
            try
            {
                result = (1 == _shelterInventoryItemAccessor.EnableOrDisableShelterInventoryItem(shelterID, itemID, disableItem: true));
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}
