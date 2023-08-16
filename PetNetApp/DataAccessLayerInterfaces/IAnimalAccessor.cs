using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerInterfaces
{
    public interface IAnimalAccessor
    {
        /// <summary>
        /// John
        /// Created: N/A
        /// 
        /// Inserts animal profile record into the database
        /// </summary>
        ///
        /// <remarks>
        /// Andrew Schneider
        /// Updated: 2023/02/19
        /// Added shelter Id
        /// </remarks>
        /// <param name="animal">The animal object to be added</param>
        /// <exception cref="Exception">Insert Fails</exception>
        /// <returns>Rows affected</returns>
        int InsertAnimal(AnimalVM animal);

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/01
        /// 
        /// Selects an animal VM by animal Id and shelter Id
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example:  Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="animalId">The animal Id of the animal VM to be returned</param>
        /// <param name="shelterId">The shelter Id of the animal VM to be returned</param>
        /// <exception cref="Exception">Select Fails</exception>
        /// <returns>AnimalVM</returns>
        AnimalVM SelectAnimalByAnimalId(int animalId, int shelterId);
        List<Animal> SelectAllAnimals(String animalName);

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/08
        /// 
        /// Selects all animal breeds to populate add/edit animal profile combo boxes
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example:  Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param>No parameters</param>
        /// <exception cref="Exception">Select Fails</exception>
        /// <returns>A  list of all animal breeds</returns>
        Dictionary<string, List<string>> SelectAllAnimalBreeds();
        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/08
        /// 
        /// Selects all animal genders to populate add/edit animal profile combo boxes
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example:  Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param>No parameters</param>
        /// <exception cref="Exception">Select Fails</exception>
        /// <returns>A  list of all animal genders</returns>
        List<string> SelectAllAnimalGenders();
        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/08
        /// 
        /// Selects all animal statuses to populate add/edit animal profile combo boxes
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example:  Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param>No parameters</param>
        /// <exception cref="Exception">Select Fails</exception>
        /// <returns>A  list of all animal statuses</returns>
        List<string> SelectAllAnimalStatuses();
        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/08
        /// 
        /// Selects all animal types to populate add/edit animal profile combo boxes
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example:  Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param>No parameters</param>
        /// <exception cref="Exception">Select Fails</exception>
        /// <returns>A  list of all animal types</returns>
        List<string> SelectAllAnimalTypes();

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/02/08
        /// 
        /// Updates an animal profile record using an "old" animal VM
        /// object and a "new" edited animal VM object
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example:  Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="oldAnimal">AnimalVM object holding old data</param>
        /// <param name="newAnimal">AnimalVM object holding new edited data</param>
        /// <exception cref="Exception">Update Fails</exception>
        /// <returns>Rows edited</returns>
        int UpdateAnimal(AnimalVM oldAnimal, AnimalVM newAnimal);

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// </summary>
        /// <param name="shelterId"></param>
        /// <returns></returns>
        List<Animal> SelectAllAnimals(int shelterId);
        List<Animal> SelectAllAnimalsNotInKennel();

        /// <summary>
        /// Hoang Chu
        /// Created: 2023/02/17
        /// 
        /// Select Animal Adoptable Profile
        /// </summary>
        /// <param name="animalId"></param>
        /// <returns></returns>
        AnimalVM SelectAnimalAdoptableProfile(int animalId);

        /// <summary>
        /// William Rients
        /// Created: 2023/02/10
        /// 
        /// Selects a specific animalVM model by animal Id
        /// for the medical profile
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="AnimalId">int for the animal</param>
        /// <exception cref="Exception">No animal is retrived with that Id</exception>
        /// <returns>AnimalVM object</returns>	
        AnimalVM SelectAnimalMedicalProfileByAnimalId(int AnimalId);

        List<AnimalVM> SelectAdoptedAnimalByUserId(int usersId);
        FosterPlacementRecord SelectFosterPlacementRecordNotes(int animalId);

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/20
        /// 
        /// Selects a list of animalVM models for a given fundraising event id
        /// </summary>
        /// 
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="fundraisingEventId">int for the fundraising event id</param>
        /// <exception cref="Exception">No animals retrieved with that event id</exception>
        /// <returns>List of AnimalVM objects</returns>
        List<AnimalVM> SelectAnimalsByFundraisingEventId(int fundraisingEventId);

        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/04/12
        /// 
        /// Selects a list of all animals available for
        /// adoption at all shelters.
        /// </summary>
        /// 
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <exception cref="Exception">No animals retrieved with that event id</exception>
        /// <returns>List of AnimalVM objects</returns>
        List<AnimalVM> SelectAllAdoptableAnimals();
    }
}
