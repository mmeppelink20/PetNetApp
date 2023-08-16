/// <summary>
/// Alexis Oetken
/// Created: 2023/04/20
/// 
/// Bookmark Object Class
/// 
/// </summary>
///
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/23
/// 
/// Final QA
/// </remarks>

using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IBookmarkManager
    {
        /// <summary>
        /// Alexis Oetken
        /// Created: 2023/04/20
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/23
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="UserId"></param>
        /// <returns></returns>
        List<Bookmark> RetrieveAllBookmarks(int UserId);
        /// <summary>
        /// Alexis Oetken
        /// Created: 2023/04/20
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/23
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="UserId"></param>
        /// <param name="AnimalId"></param>
        /// <returns></returns>
        bool AddBookmark(int UserId, int AnimalId);
        /// <summary>
        /// Alexis Oetken
        /// Created: 2023/04/20
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/23
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="UserId"></param>
        /// <param name="AnimalId"></param>
        /// <returns></returns>
        bool DeleteBookmark(int UserId, int AnimalId);

    }
}
