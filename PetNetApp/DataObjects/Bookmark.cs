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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Bookmark
    {
        public int AnimalId { get; set; }

        public string AnimalName { get; set; }

        public int UserId { get; set; }

    }

    public class BookmarkVM : Bookmark
    {
        public List<string> BookmarkList { get; set; }
    }
}
