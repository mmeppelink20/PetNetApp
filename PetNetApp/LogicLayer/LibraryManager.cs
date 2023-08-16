/// <summary>
/// Brian Collum
/// Created: 2023/03/23
/// 
/// Library Manager: Implements logic layer methods for working with Library Item objects
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
using LogicLayerInterfaces;
using DataAccessLayerInterfaces;

namespace LogicLayer
{
    public class LibraryManager : ILibraryManager
    {
        private ILibraryAccessor _libraryAccessor = null;
        public LibraryManager()
        {
            _libraryAccessor = new DataAccessLayer.LibraryAccessor();
        }
        // Unit test constructor
        public LibraryManager(ILibraryAccessor libraryAccessor)
        {
            _libraryAccessor = libraryAccessor;
        }

        public List<Item> GetLibraryItemList()
        {
            List<Item> returnList = new List<Item>();
            try
            {
                returnList = _libraryAccessor.RetrieveLibraryItemList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve the list of Library items", ex);
            }
            return returnList;
        }
    }
}
