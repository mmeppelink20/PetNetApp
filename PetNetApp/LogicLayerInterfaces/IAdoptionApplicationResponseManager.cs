/// <summary>
/// Asa Armstrong
/// Created: 2023/03/30
/// 
/// Adoption Application Response Manager class to CRUD Adoption Application Response objects.
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
    public interface IAdoptionApplicationResponseManager
    {
        bool AddAdoptionApplicationResponseByAdoptionApplicationId(AdoptionApplicationResponseVM adoptionApplicationResponseVM);

        /// <summary>
        /// Asa Armstrong
        /// Created: 2023/03/30
        /// 
        /// Retrieves an Adoption Application Response record.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="adoptionApplicationId">int adoptionApplicationId</param>
        /// <exception cref="SQLException">Select fails.</exception>
        /// <returns>AdoptionApplicationResponseVM</returns>
        AdoptionApplicationResponseVM RetrieveAdoptionApplicationResponse(int adoptionApplicationId);

        /// <summary>
        /// Asa Armstrong
        /// Created: 2023/03/30
        /// 
        /// Edits an Adoption Application Response record.
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// <param name="newAdoptionApplicationResponse">The new AdoptionApplicationResponse record to replace the old record</param>
        /// <param name="oldAdoptionApplicationResponse">The old record</param>
        /// <exception cref="SQLException">Update fails.</exception>
        /// <returns>True if the record was edited</returns>
        bool EditAdoptionApplicationResponse(AdoptionApplicationResponse newAdoptionApplicationResponse, AdoptionApplicationResponse oldAdoptionApplicationResponse);
    }
}
