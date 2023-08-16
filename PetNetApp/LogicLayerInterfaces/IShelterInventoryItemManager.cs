/// <summary>
/// Zaid Rachman
/// Created: 2023/03/19
/// 
/// Interface for ShelterInventoryItemManager
/// </summary>
///
/// <remarks>
/// Brian Collum
/// Updated: 2023/04/06
/// 
/// Updated description of RetrieveInventoryByShelterId to omit disabled ShelterInventoryItems
/// Added AddToShelterInventory, EnableShelterInventoryItem, and DisableShelterInventoryItem methods
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IShelterInventoryItemManager
    {
        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/03/19
        /// 
        /// Retrieves list of inventory items by shelterId. Takes in ShelterId as a parameter
        /// Does NOT display items that have been disabled from the shelter's inventory
        /// Full inventory display is still available in ShelterInventoryItemManager.RetrieveFullInventoryByShelterId(int shelterId)
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/19
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="shelterId"></param>
        /// <returns></returns>
        List<ShelterInventoryItemVM> RetrieveInventoryByShelterId(int shelterId);
        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/03/19
        /// 
        /// Retrieves list of inventory items by shelterId and ItemId as. Takes in ShelterId and ItemId as parameters.
        /// </summary>
        /// <param name="shelterId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        ShelterInventoryItemVM RetrieveInventoryItemByShelterIdAndItemId(int shelterId, string itemId);
        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/03/19
        /// 
        /// Edits ShelterInventoryItem. Takes in the oldShelterInventoryItemVM and newShelterInventoryItemVM objects as parameters.
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/20
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="oldShelterInventoryItemVM"></param>
        /// <param name="newShelterInventoryItemVM"></param>
        /// <returns></returns>
        bool EditShelterInventoryItem(ShelterInventoryItemVM oldShelterInventoryItemVM, ShelterInventoryItemVM newShelterInventoryItemVM);

        /// <summary>
        ///  
        /// Brian Collum
        /// Created: 2023/04/06
        /// 
        /// Adds a new ShelterInventoryItem to a shelter's inventory by instantiating an item from the Library
        /// 
        /// </summary>
        /// <param name="shelterID">The ID of the shelter that will recieve the new ShelterInventoryItem</param>
        /// <param name="itemID">The ItemID of the Library item that will be instantiated as a new ShelterInventoryItem</param>
        /// <returns>Returns true if item was added</returns>
        bool AddToShelterInventory(int shelterID, string itemID);

        /// <summary>
        ///  
        /// Brian Collum
        /// Created: 2023/04/06
        /// 
        /// Enables a ShelterInventoryItem's ItemDisabled field
        /// This is to re-enable display of an item if its removed, and then re-added to a shelter's inventory
        /// 
        /// </summary>
        /// <param name="shelterID">The ID of the Shelter of the item to enable</param>
        /// <param name="itemID">The ID of the ShelterInventoryItem to enable</param>
        /// <returns>returns true if item's ItemDisabled was updated</returns>
        bool EnableShelterInventoryItem(int shelterID, string itemID);

        /// <summary>
        ///  
        /// Brian Collum
        /// Created: 2023/04/06
        /// 
        /// Disables a ShelterInventoryItem's ItemDisabled field
        /// This is to disable display of an item from a shelter's inventory
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/19
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="shelterID">The ID of the Shelter of the item to disable</param>
        /// <param name="itemID">The ID of the ShelterInventoryItem to disable</param>
        /// <returns>returns true if item's ItemDisabled was updated</returns>
        bool DisableShelterInventoryItem(int shelterID, string itemID);
    }
}
