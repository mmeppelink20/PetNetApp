/// <summary>
/// Brian Collum
/// Created: 2023/03/09
/// 
/// ILibraryManager interface governing access to the LibraryManager class in LogicLayer
/// Correlates to ILibraryAccessor in DataAccessLayerInterfaces
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

using DataObjects;

namespace LogicLayerInterfaces
{
    public interface ILibraryManager
    {
        /// <summary>
        /// Brian Collum
        /// Created: 2023/03/09
        /// This returns the list of all Library inventory items so that the Library UI can be populated
        /// </summary>
        /// <exception cref="SQLException">This will call the DataAccessLayer, which can throw an SQL Exception if retrieval fails</exception>
        /// <returns>A list of LibraryItems</returns>
        List<Item> GetLibraryItemList();
    }
}
