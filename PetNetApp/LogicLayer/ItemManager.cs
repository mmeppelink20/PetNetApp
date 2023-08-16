/// <summary>
/// Zaid Rachman
/// Created: 2023/03/19
/// 
/// Logic layer for ItemManager
/// </summary>
///
/// <remarks>
/// Nathan Zumsande
/// Updated: 2023/03/31
/// Added methods AddItem, RetrieveAllCategories
/// AddItemCategory, RemoveItemCategory, AddCategory
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessLayerInterfaces;
using DataAccessLayer;

namespace LogicLayer
{
    public class ItemManager : IItemManager
    {
        private IItemAccessor _itemAccessor = null;
        public ItemManager()
        {
            _itemAccessor = new ItemAccessor();
        }
        public ItemManager(IItemAccessor itemAccessor)
        {
            _itemAccessor = itemAccessor;

        }

        public bool AddItem(string itemId)
        {
            bool result = false;
            try
            {
                result = (1 == _itemAccessor.InsertItem(itemId));
                if (!result)
                {
                    throw new ApplicationException("Insert failed for item " + itemId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool AddItemCategory(string itemId, string category)
        {
            bool result = false;
            try
            {
                result = (1 == _itemAccessor.InsertItemCategory(itemId, category));
                if (!result)
                {
                    throw new ApplicationException("Insert Item Category failed for item " + itemId + " with the category of " + category);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<string> RetrieveAllCategories()
        {
            List<string> categories = new List<string>();
            try
            {
                categories = _itemAccessor.SelectAllCategories();
                if(categories == null)
                {
                    throw new ApplicationException("Items not found");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An Error Occured while retrieving categories", ex);
            }

            return categories;
        }

        public bool RemoveItemCategory(string itemId, string category)
        {
            bool result = false;
            try
            {
                result = (1 == _itemAccessor.DeleteItemCategory(itemId, category));
                if (!result)
                {
                    throw new ApplicationException("Delete Item Category failed for item " + itemId + " with the category of " + category);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Item RetrieveItemByItemId(string itemId)
        {
            Item item = null;
            try
            {
                item = _itemAccessor.SelectItemByItemId(itemId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Items not found", ex);
            }
            return item;
        }

        public bool AddCategory(string categoryId)
        {
            bool result = false;
            try
            {
                result = (1 == _itemAccessor.InsertCategory(categoryId));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return result;
        }
    }
}
