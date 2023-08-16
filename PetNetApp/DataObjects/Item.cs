/// <summary>
/// Zaid Rachman
/// Created: 2023/03/19
/// 
/// Items object
/// </summary>
///
/// <remarks>
/// Brian Collum
/// Updated: 2023/03/24
/// Updated Item's default getter to always initialize the list of tags
/// Updated: 2023/04/01
/// Removed explicit tag initialization, not needed for lists
/// 
/// Oleksiy Fedchuk
/// Updated: 2023/04/13
/// 
/// FinalQA
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /*This object is for inventory related items*/
    public class Item
    {
        public string ItemId { get; set; }  // This is the name of the item, also item.itemid in the DB. NVarchar(50)
        public List<string> CategoryId { get; set; }
    }
}
