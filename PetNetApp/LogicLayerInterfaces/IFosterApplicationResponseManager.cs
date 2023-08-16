/// <summary>
/// Asa Armstrong
/// Created: 2023/03/23
/// 
/// Foster Application Response Manager class to CRUD Foster Application Response objects.
/// </summary>
///
/// <remarks>
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IFosterApplicationResponseManager
    {
        /// <summary>
        /// Asa Armstrong
        /// Created: 2023/03/23
        /// 
        /// Adds a Foster Application Response record.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="fosterApplicationResponse">FosterApplicationResponse</param>
        /// <exception cref="SQLException">Insert fails.</exception>
        /// <returns>True if the record was added</returns>
        bool AddFosterApplicationResponse(FosterApplicationResponse fosterApplicationResponse);

        /// <summary>
        /// Asa Armstrong
        /// Created: 2023/03/23
        /// 
        /// Retrieves a Foster Application Response record.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="fosterApplicationId">int fosterApplicationId</param>
        /// <exception cref="SQLException">Select fails.</exception>
        /// <returns>FosterApplicationResponseVM</returns>
        FosterApplicationResponseVM RetrieveFosterApplicationResponse(int fosterApplicationId);

        /// <summary>
        /// Asa Armstrong
        /// Created: 2023/03/23
        /// 
        /// Edits a Foster Application Response record.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="newFosterApplicationResponse">The new FosterApplicationResponse record to replace the old record</param>
        /// <param name="oldFosterApplicationResponse">The old record</param>
        /// <exception cref="SQLException">Update fails.</exception>
        /// <returns>True if the record was edited</returns>
        bool EditFosterApplicationResponse(FosterApplicationResponse newFosterApplicationResponse, FosterApplicationResponse oldFosterApplicationResponse);
    }
}
