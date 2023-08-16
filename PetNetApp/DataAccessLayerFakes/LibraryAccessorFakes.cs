/// <summary>
/// Brian Collum
/// Created: 2023/03/23
/// 
/// LibraryAccessorFakes contains fake data for LibraryAccessor unit tests
/// 
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

using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    public class LibraryAccessorFakes : ILibraryAccessor
    {
        List<Item> libraryItemList = new List<Item>();

        public LibraryAccessorFakes()
        {
            libraryItemList.Add(new Item {
                ItemId = "Library Item One",
                CategoryId = new List<string> { "i1 Tag One", "i1 Tag Two" }
            });
            libraryItemList.Add(new Item
            {
                ItemId = "Library Item Two",
                CategoryId = new List<string> { "i2 Tag One", "i2 Tag Two" }
            });
        }
        public List<Item> RetrieveLibraryItemList()
        {
            return libraryItemList;
        }
    }
}
