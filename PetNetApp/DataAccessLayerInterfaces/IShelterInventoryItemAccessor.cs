/// <summary>
/// Zaid
/// Created: 2023/03/19
/// 
/// Interface for ShelterInventoryAccessor
/// </summary>
///
/// <remarks>
/// Brian Collum
/// Updated: 2023/04/06
/// 
/// Added InsertNewShelterInventoryItemFromLibrary and EnableOrDisableShelterInventoryItem methods and descriptions
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IShelterInventoryItemAccessor
    {
        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/03/19
        /// 
        /// Retrieves list of inventory items by shelterId. Takes in ShelterId as a parameter
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
        List<ShelterInventoryItemVM> SelectInventoryByShelter(int shelterId);
        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/03/19
        /// 
        /// Retrieves list of inventory items by shelterId and ItemId as. Takes in ShelterId and ItemId as parameters.
        /// </summary>
        /// <param name="shelterId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        ShelterInventoryItemVM SelectShelterInventoryItemByShelterIdAndItemId(int shelterId, string itemId);
        /// <summary>
        ///  
        /// Zaid Rachman
        /// Created: 2023/03/19
        /// 
        /// Edits ShelterInventoryItem. Takes in the oldShelterInventoryItemVM and newShelterInventoryItemVM objects as parameters.
        /// <remarks>
        /// Nathan Zumsande
        /// Updated: 2023/04/19
        /// 
        /// Updated the method in the accessor to check if the custom flag for the updated item
        /// is null otherwise set the parameter to the passed value
        /// </remarks>
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
        int UpdateShelterInventoryItem(ShelterInventoryItemVM oldShelterInventoryItemVM, ShelterInventoryItemVM newShelterInventoryItemVM);

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
        /// <returns>Number of rows affected</returns>
        int InsertNewShelterInventoryItemFromLibrary(int shelterID, string itemID);

        /// <summary>
        ///  
        /// Brian Collum
        /// Created: 2023/04/06
        /// 
        /// Enables or Disables a ShelterInventoryItem's ItemDisabled field
        /// This determines if the item will appear in a shelter's inventory
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/19
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="shelterID">The ID of the shelter that will recieve the new ShelterInventoryItem</param>
        /// <param name="itemID">The ItemID of the Library item that will be instantiated as a new ShelterInventoryItem</param>
        /// <param name="disableItem">if disableItem is true, this will set disableItem to true. If it is false, it will set it to false.</param>
        /// <returns>Number of rows affected</returns>
        int EnableOrDisableShelterInventoryItem(int shelterID, string itemID, bool disableItem = false);
    }
}
