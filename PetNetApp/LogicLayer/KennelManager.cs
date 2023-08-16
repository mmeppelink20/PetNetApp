using DataAccessLayer;
using DataAccessLayerInterfaces;
using DataObjects;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerFakes;

namespace LogicLayer
{
    public class KennelManager : IKennelManager
    {
        private IKennelAccessor kennelAccessor = null;

        public KennelManager()
        {
            kennelAccessor = new KennelAccessor();
        }

        public KennelManager(IKennelAccessor ka)
        {
            kennelAccessor = ka;
        }

        public bool AddKennel(Kennel kennel)
        {
            int result = 0;
            try
            {
                result = kennelAccessor.InsertKennel(kennel);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Kennel failed to be added", ex);
            }
            return result == 1;
        }

        public bool EditKennelStatusByKennelId(int KennelId)
        {
            int result = 0;
            try
            {
                result = kennelAccessor.UpdateKennelStatusByKennelId(KennelId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Kennel failed to be edited", ex);
            }
            return result == 1;
        }

        public bool RemoveAnimalKennlingByKennelId(int KennelId)
        {
            int result = 0;
            try
            {
                result = kennelAccessor.DeleteAnimalKennelingByKennelId(KennelId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to remove animal from kennel", ex);
            }
            return result == 1;
        }

        public List<string> RetrieveAnimalTypes()
        {
            try
            {
                return kennelAccessor.SelectAnimalTypes();
            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to retrieve animal types", e);
            }
        }

        public List<KennelVM> RetrieveKennels(int ShelterId)
        {
            try
            {
                return kennelAccessor.SelectKennels(ShelterId);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to retrieve kennels", e);
            }
                      
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/02/10
        /// 
        /// Selects a specific kennel with an AnimalId
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="AnimalId">int for the the specific kennel</param>
        /// <exception cref="Exception">No kennel is retrived witht that AnimalId</exception>
        /// <returns>Kennel Object</returns>	
        public Kennel RetrieveKennelIdByAnimalId(int AnimalId)
        {
            try
            {
                return kennelAccessor.SelectKennelIdByAnimalId(AnimalId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve kennel", ex);
            }
        }

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
        /// <returns>Bool if animal was assigned to a kennel</returns>
        public bool AddAnimalIntoKennelByAnimalId(int KennelId, int AnimalId)
        {
            try
            {
                return 0 < kennelAccessor.InsertAnimalIntoKennelByAnimalId(KennelId, AnimalId);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to insert animal into kennel", ex);
            }
        }

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
        /// /// <param name="AnimalTypeId">string for the the specific type of animal</param>
        /// <exception cref="Exception">Failed to retrived a list of animals</exception>
        /// <returns>List of animals</returns>
        public List<Animal> RetrieveAllAnimalsForKennel(int ShelterId)
        {
            try
            {
                return kennelAccessor.SelectAllAnimalsForKennel(ShelterId);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to retrieve animals.", ex);
            }
        }

        public bool RemoveAnimalKennelingByKennelIdAndAnimalId(int kennelId, int animalId)
        {
            try
            {
                return (0 < kennelAccessor.DeleteAnimalKennelingByKennelIdAndAnimalId(kennelId, animalId));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not delete animal kenneling.", ex);
            }
        }

        public Images RetrieveImageByAnimalId(int animalId)
        {
            try
            {
                return kennelAccessor.SelectImageByAnimalId(animalId);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to retrieve image", e);
            }
        }

        public List<Kennel> RetrieveAllEmptyKennels(int shelterId)
        {
            try
            {
                return kennelAccessor.SelectAllEmptyKennels(shelterId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve kennels.", ex);
            }
        }
    }
}
