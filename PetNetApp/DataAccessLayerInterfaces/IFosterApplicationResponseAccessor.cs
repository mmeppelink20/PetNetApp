/// <summary>
/// Asa Armstrong
/// Created: 2023/03/23
/// 
/// Foster Application Response Accessor class to CRUD Foster Application Response objects.
/// </summary>
///
/// <remarks>
/// Asa Armstrong
/// Updated: 2023/03/23
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;


namespace DataAccessLayerInterfaces
{
    public interface IFosterApplicationResponseAccessor
    {
        /// <summary>
        /// Asa Armstrong
        /// Created: 2023/03/23
        /// 
        /// Inserts a Foster Application Response record.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="FosterApplicationResponse">FosterApplicationResponse</param>
        /// <exception cref="SQLException">Insert fails.</exception>
        /// <returns>Rows edited</returns>
        int InsertFosterApplicationResponse(FosterApplicationResponse fosterApplicationResponse);

        /// <summary>
        /// Asa Armstrong
        /// Created: 2023/03/23
        /// 
        /// Updates a Foster Application Response record.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="newFosterApplicationResponse">The new FosterApplicationResponse record to replace the old record</param>
        /// <param name="oldFosterApplicationResponse">The old FosterApplicationResponse record</param>
        /// <exception cref="SQLException">Update fails.</exception>
        /// <returns>Rows edited</returns>
        int UpdateFosterApplicationResponse(FosterApplicationResponse newFosterApplicationResponse, FosterApplicationResponse oldFosterApplicationResponse);

        /// <summary>
        /// Asa Armstrong
        /// Created: 2023/03/23
        /// 
        /// Selects a Foster Application Response record.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="fosterApplicationId">int FosterApplicationId</param>
        /// <exception cref="SQLException">Select fails.</exception>
        /// <returns>FosterApplicationResponseVM</returns>
        FosterApplicationResponseVM SelectFosterApplicationResponseByFosterApplicationId(int fosterApplicationId);
    }
}
