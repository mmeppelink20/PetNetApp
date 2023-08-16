/// <summary>
/// Asa Armstrong
/// Created: 2023/02/24
/// 
/// Death Accessor class to CRUD Death objects.
/// </summary>
///
/// <remarks>
/// Asa Armstrong
/// Updated: 2023/02/24
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IDeathAccessor
    {
        /// <summary>
        /// Asa Armstrong
        /// Created: 2023/02/24
        /// 
        /// Inserts an animal death record.
        /// </summary>
        ///
        /// <remarks>
        /// Asa Armstrong
        /// Updated: 2023/02/24
        /// Added Comment.
        /// </remarks>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// FinalQA
        /// </remarks>
        /// <param name="death">death</param>
        /// <exception cref="SQLException">Insert fails.</exception>
        /// <returns>Rows edited</returns>
        int InsertAnimalDeath(Death death);
        
        /// <summary>
        /// Asa Armstrong
        /// Created: 2023/02/24
        /// 
        /// Selects an animal death record.
        /// </summary>
        ///
        /// <remarks>
        /// Asa Armstrong
        /// Updated: 2023/02/24
        /// Added Comment.
        /// </remarks>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// FinalQA
        /// </remarks>
        /// <param name="animal">animal</param>
        /// <exception cref="SQLException">Select fails.</exception>
        /// <returns>DeathVM</returns>
        DeathVM SelectAnimalDeath(Animal animal);

        /// <summary>
        /// Asa Armstrong
        /// Created: 2023/02/24
        /// 
        /// Updates an animal death record
        /// </summary>
        ///
        /// <remarks>
        /// Asa Armstrong
        /// Updated: 2023/02/24
        /// Added Comment.
        /// </remarks>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/27
        /// 
        /// FinalQA
        /// </remarks>
        /// <param name="newDeath">The new death record to replace the old record</param>
        /// <param name="oldDeath">The old death record</param>
        /// <exception cref="SQLException">Update fails.</exception>
        /// <returns>Rows edited</returns>
        int UpdateAnimalDeath(Death newDeath, Death oldDeath);
    }
}
