/// <summary>
/// Zaid Rachman
/// Created: 2023/03/19
/// 
/// ItemAccessor Fake
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;

namespace DataAccessLayerFakes
{
    public class ItemAccessorFakes : IItemAccessor
    {
        List<Item> fakeItems = new List<Item>();
        List<string> fakeCategorys = new List<string>();
        public ItemAccessorFakes()
        {
            fakeItems.Add(new Item
            {
                ItemId = "Cat Food",
                CategoryId = new List<string> { "Cat", "Food", "Testing CategoryId" }
            });
            fakeItems.Add(new Item
            {
                ItemId = "Dog Food",
                CategoryId = new List<string> { "Dog", "Food", "Testing CategoryId", "Healthy" }
            });
            fakeItems.Add(new Item
            {
                ItemId = "Bird Food",
                CategoryId = new List<string> { "Bird", "Food", "Testing CategoryId" }
            });
            fakeCategorys.Add("Cat");
            fakeCategorys.Add("Dog");
            fakeCategorys.Add("Bird");
            fakeCategorys.Add("Food");
            fakeCategorys.Add("Healthy");
            fakeCategorys.Add("Test");
        }
        public Item SelectItemByItemId(string ItemId)
        {
            Item itemReturn = null;
            foreach (Item fakeItem in fakeItems)
            {
                if (fakeItem.ItemId == ItemId)
                {
                    itemReturn = fakeItem;
                    break;
                }
            }
            return itemReturn;
        }

        public int InsertItem(string itemId)
        {
            int result = 0;
            Item _item = new Item();
            _item.ItemId = itemId;
            fakeItems.Add(_item);
            foreach (Item i in fakeItems)
            {
                if (i.ItemId == itemId)
                {
                    result = 1;
                    break;
                }
            }

            return result;
        }

        public int InsertItemCategory(string itemId, string category)
        {
            int result = 0;
            foreach (Item i in fakeItems)
            {
                if (i.ItemId == itemId)
                {
                    i.CategoryId.Add(category);
                }
            }
            foreach (Item i in fakeItems)
            {
                if (i.ItemId == itemId)
                {
                    foreach (string c in i.CategoryId)
                    {
                        if (c == category)
                        {
                            result = 1;
                            break;
                        }
                    }
                }
            }

            return result;
        }

        public int DeleteItemCategory(string itemId, string category)
        {
            int result = 0;
            foreach (Item i in fakeItems)
            {
                if (i.ItemId == itemId)
                {
                    i.CategoryId.Remove(category);
                }
            }
            foreach (Item i in fakeItems)
            {
                if (i.ItemId == itemId)
                {
                    if (!i.CategoryId.Contains(category))
                    {
                        result = 1;
                        break;
                    }
                }
            }

            return result;
        }

        public List<string> SelectAllCategories()
        {
            List<string> categories = new List<string>();
            foreach (string c in fakeCategorys)
            {
                categories.Add(c);
            }

            return categories;
        }

        public int InsertCategory(string categoryId)
        {
            int number = 0;
            fakeCategorys.Add(categoryId);
            foreach (var c in fakeCategorys)
            {
                if (c == categoryId)
                {
                    number++;
                }
            }

            return number;
        }
    }
}
