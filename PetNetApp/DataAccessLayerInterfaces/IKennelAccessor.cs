using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerInterfaces
{
    public interface IKennelAccessor
    {
        /// <summary>
        /// Gwen Arman
        /// Created: 2023/02/01
        /// 
        /// Methods retrieves kennels from the database with the associated shelter id
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="ShelterId">A description of the parameter that this method takes</param>
        /// <exception cref="SQLException"></exception>
        /// <returns>List<KennelVM></returns>
        List<KennelVM> SelectKennels(int ShelterId);

        /// <summary>
        /// Gwen Arman
        /// Created: 2023/02/01
        /// 
        /// Methods selects animal types from the database
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <exception cref="SQLException"></exception>
        /// <returns>List<KennelVM></returns>
        List<string> SelectAnimalTypes();

        /// <summary>
        /// Gwen Arman
        /// Created: 2023/02/01
        /// 
        /// Methods inserts a kennel
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="kennel">A description of the parameter that this method takes</param>
        /// <exception cref="SQLException"></exception>
        /// <returns>List<KennelVM></returns>
        int InsertKennel(Kennel kennel);

        /// <summary>
        /// Gwen Arman
        /// Created: 2023/02/01
        /// 
        /// Methods updates kennel status from the database with the associated kennel id
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="KennelId">A description of the parameter that this method takes</param>
        /// <exception cref="SQLException"></exception>
        /// <returns>List<KennelVM></returns>
        int UpdateKennelStatusByKennelId(int KennelId);

        /// <summary>
        /// Gwen Arman
        /// Created: 2023/02/01
        /// 
        /// Methods delete animalkennel from the database with the associated kennel id
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="KennelId">A description of the parameter that this method takes</param>
        /// <exception cref="SQLException"></exception>
        /// <returns>List<KennelVM></returns>
        int DeleteAnimalKennelingByKennelId(int KennelId);

        /// <summary>
        /// William Rients
        /// Created: 2023/02/10
        /// 
        /// Selects a specific kennel with an AnimalId
        /// </summary>
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="AnimalId">int for the the specific kennel</param>
        /// <exception cref="Exception">No kennel is retrived witht that AnimalId</exception>
        /// <returns>Kennel Object</returns>
        Kennel SelectKennelIdByAnimalId(int AnimalId);

        /// <summary>
        /// William Rients
        /// Created: 2023/02/10
        /// 
        /// Inserts an animal into a specific kennel
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="KennelId">int for the the specific kennel</param>
        /// /// <param name="AnimalId">int for the the specific animal</param>
        /// <exception cref="Exception">Failed to insert animal into kennel</exception>
        /// <returns>Rows affected</returns>
        int InsertAnimalIntoKennelByAnimalId(int KennelId, int AnimalId);
        // List<Animal> SelectAllAnimalsForKennel(int ShelterId);
        
        /// <summary>
        /// William Rients
        /// Created: 2023/02/10
        /// 
        /// Gets a list of animals available to 
        /// be assigned to a kennel
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="ShelterId">int for the the specific shelter</param>
        /// <exception cref="Exception">Failed to retrived a list of animals</exception>
        /// <returns>List of animals</returns>
        List<Animal> SelectAllAnimalsForKennel(int ShelterId);

        /// <summary>
        /// Asa Armstrong
        /// Created: 2023/02/24
        /// 
        /// Deletes the Animal Kenneling record to remove the animal from the kennel.
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
        /// <param name="kennelId">kennelId</param>
        /// <param name="animalId">animalId</param>
        /// <exception cref="SQLException">Delete fails.</exception>
        /// <returns>Rows edited</returns>
        int DeleteAnimalKennelingByKennelIdAndAnimalId(int kennelId, int animalId);

        /// <summary>
        /// Author: GWen Arman
        /// Date: 2023/03/08
        /// Description; Retrieves an image for by animal id
        /// </summary>
        /// <param name="animalId"></param>
        /// <returns></returns>
        Images SelectImageByAnimalId(int animalId);

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/03/07
        /// 
        /// Selects all empty kennels
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="shelterId">The Id of the shelter</param>
        /// <exception cref="SQLException"></exception>
        /// <returns>List<Kennel></returns>
        List<Kennel> SelectAllEmptyKennels(int shelterId);
    }
}
