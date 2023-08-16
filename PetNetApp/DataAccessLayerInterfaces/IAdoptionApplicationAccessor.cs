using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IAdoptionApplicationAccessor
    {
        /// <summary>
        /// Molly Meister
        /// Created: 2023/03/19
        /// 
        /// Inserts an adoption application.
        /// Returns rows affected.
        /// </summary>
        /// <param name="adoptionApplication">the AdoptionApplicationVM object to insert</param>
        /// <exception cref="SQLException">insert fails</exception>
        /// <returns>Rows affected.</returns>
        int InsertAdoptionApplication(AdoptionApplicationVM adoptionApplication);

        /// <summary>
        /// Molly Meister
        /// Created: 2023/03/19
        /// 
        /// Retrieves all home types.
        /// Returns a list of strings that represent the home types.
        /// </summary>
        /// 
        /// <exception cref="SQLException">retrieval fails</exception>
        /// <returns>List<string></string></returns>
        List<string> SelectAllHomeTypes();

        /// <summary>
        /// Molly Meister
        /// Created: 2023/03/19
        /// 
        /// Retrieves all home ownership types.
        /// Returns a list of strings that represent the home ownership types.
        /// </summary>
        /// 
        /// <exception cref="SQLException">retrieval fails</exception>
        /// <returns>List<string></string></returns>
        List<string> SelectAllHomeOwnershipTypes();

        /// <summary>
        /// Molly Meister
        /// Created: 2023/04/03
        /// 
        /// Retrieves all adoption applications for an animal.
        /// Returns rows affected.
        /// </summary>
        /// <param name="animalId">the ID of the animal to get applications for.</param>
        /// <exception cref="SQLException">retrieval fails</exception>
        /// <returns>List of AdoptionApplicationVM</returns>
        List<AdoptionApplicationVM> SelectAllAdoptionApplicationsByAnimalId(int animalId);

        /// <summary>
        /// Molly Meister
        /// Created: 2023/04/03
        /// 
        /// Updates all pending applications for an animal to denied after an approved application.
        /// Returns rows affected.
        /// </summary>
        /// <param name="animalId">the ID of the animal to update applications for.</param>
        /// /// <param name="response">the adoption application response to be inserted</param>
        /// <exception cref="SQLException">update fails</exception>
        /// <returns>rows affected</returns>
        int  UpdateAdoptionApplicationStatusByAnimalIdForApprovedApplication(AdoptionApplicationResponse response);

        /// <summary>
        /// Molly Meister
        /// Created: 2023/04/19
        /// 
        /// Retrieves all adoption applications for a user.
        /// Returns rows affected.
        /// </summary>
        /// <param name="usersId">the ID of the user to get applications for.</param>
        /// <exception cref="SQLException">retrieval fails</exception>
        /// <returns>List of AdoptionApplicationVM</returns>
        List<AdoptionApplicationVM> SelectAllAdoptionApplicationsByUsersId(int usersId);
    }
}
