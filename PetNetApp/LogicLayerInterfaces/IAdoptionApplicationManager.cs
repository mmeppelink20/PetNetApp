using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IAdoptionApplicationManager
    {
        /// <summary>
        /// Molly Meister
        /// Created: 2023/03/19
        /// 
        /// Passes an adoptionApplication to the AdoptionApplicationAccessor to add an adoption application into the database.
        /// Returns a bool if the insert passes or fails
        /// </summary>
        /// <param name="adoptionApplication">the animalId of the animal inserting an adoption application for</param>
        /// <exception cref="ApplicationException">Add Fails</exception>
        /// <returns>bool</returns>
        bool AddAdoptionApplication(AdoptionApplicationVM adoptionApplication);

        /// <summary>
        /// Molly Meister
        /// Created: 2023/03/19
        /// 
        /// Calls the Accessor method to retrieve all home types.
        /// </summary>
        /// <exception cref="ApplicationException">If the retrieval fails</exception>
        /// <returns>List</returns>
        List<string> RetrieveAllHomeTypes();

        /// <summary>
        /// Molly Meister
        /// Created: 2023/03/19
        /// 
        /// Calls the Accessor method to retrieve all home ownership types.
        /// </summary>
        /// <exception cref="ApplicationException">If the retrieval fails</exception>
        /// <returns>List</returns>
        List<string> RetrieveAllHomeOwnershipTypes();

        /// <summary>
        /// Molly Meister
        /// Created: 2023/04/03
        /// 
        /// Calls the Accessor method to retrieve all adoption applications for an animal.
        /// </summary>
        /// /// <param name="animalId">the animalId of the animal retrieving adoption applications for</param>
        /// <exception cref="ApplicationException">If the retrieval fails</exception>
        /// <returns>List of AdoptionApplicationVM</returns>
        List<AdoptionApplicationVM> RetrieveAllAdoptionApplicationsByAnimalId(int animalId);

        /// <summary>
        /// Molly Meister
        /// Created: 2023/04/03
        /// 
        /// Calls the Accessor method to edit all adoption application status for an animal.
        /// </summary>
        /// /// <param name="response">the adoption application response to update an application status for</param>
        /// <exception cref="ApplicationException">If the retrieval fails</exception>
        /// <returns>bool</returns>
        bool EditAdoptionApplicationStatusByAnimalIdForApprovedApplication(AdoptionApplicationResponse response);

        /// <summary>
        /// Molly Meister
        /// Created: 2023/04/19
        /// 
        /// Calls the Accessor method to retrieve all adoption applications for an animal.
        /// </summary>
        /// /// <param name="usersId">the animalId of the animal retrieving adoption applications for</param>
        /// <exception cref="ApplicationException">If the retrieval fails</exception>
        /// <returns>List of AdoptionApplicationVM</returns>
        List<AdoptionApplicationVM> RetrieveAllAdoptionApplicationsByUsersId(int usersId);
    }
}
