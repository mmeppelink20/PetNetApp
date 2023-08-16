/// <summary>
/// Brian Collum
/// Created: 2023/03/09
/// 
/// ILibraryAccessor interface governing access to the LibraryAccessor class in DataAccessLayer
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

namespace DataAccessLayerInterfaces
{
    public interface ILibraryAccessor
    {
        /// <summary>
        /// Brian Collum
        /// Created: 2023/03/09
        /// This returns the list of all Library inventory items so that the Library UI can be populated
        /// </summary>
        /// <exception cref="SQLException">Can throw an SQL Exception if retrieval fails</exception>
        /// <returns>A list of LibraryItems</returns>
        List<Item> RetrieveLibraryItemList();
    }
}
