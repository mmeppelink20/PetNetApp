using DataAccessLayer;
using DataAccessLayerInterfaces;
using DataObjects;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class AnimalManager : IAnimalManager
    {
        private IAnimalAccessor _animalAccessor = null;

        public AnimalManager()
        {
            _animalAccessor = new DataAccessLayer.AnimalAccessor();
        }

        public AnimalManager(IAnimalAccessor animalAccessor)
        {
            _animalAccessor = animalAccessor;
        }
       
        public bool AddAnimal(AnimalVM animal)
        {
            int id = 0;
            try
            {
                id = _animalAccessor.InsertAnimal(animal);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Animal record failed to be added", ex);
            }
            if(id != 0)
            {
                animal.AnimalId = id;
            }
            return id != 0;
        }

        public List<Animal> RetrieveAllAnimals(String animalName)
        {
            List<Animal> animals = null;
            try
            {
                animals = _animalAccessor.SelectAllAnimals(animalName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an error retrieving animal data.", ex);
        }
            return animals;
        }

        public AnimalVM RetrieveAnimalByAnimalId(int animalId, int shelterId)
        {
            AnimalVM animalVM = new AnimalVM();
            try
            {
                animalVM = _animalAccessor.SelectAnimalByAnimalId(animalId, shelterId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Animal record not found.", ex);
            }
            return animalVM;
        }


        public Dictionary<string, List<string>> RetrieveAllAnimalBreeds()
        {
            try
            {
                return _animalAccessor.SelectAllAnimalBreeds();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an error retrieving breeds.", ex);
            }
        }

        public List<string> RetrieveAllAnimalGenders()
        {
            try
            {
                return _animalAccessor.SelectAllAnimalGenders();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an error retrieving genders.", ex);
            }
        }

        public List<string> RetrieveAllAnimalStatuses()
        {
            try
            {
                return _animalAccessor.SelectAllAnimalStatuses();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an error retrieving statuses.", ex);
            }
        }
       
        public List<string> RetrieveAllAnimalTypes()
        {
            try
            {
                return _animalAccessor.SelectAllAnimalTypes();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an error retrieving animal types.", ex);
            }
        }

        public bool EditAnimal(AnimalVM oldAnimal, AnimalVM newAnimal)
        {
            try
            {
                return 1 == _animalAccessor.UpdateAnimal(oldAnimal, newAnimal);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating record.", ex);
            }
        }

        public List<Animal> RetrieveAllAnimals(int shelterId)
        {
            List<Animal> animals;

            try
            {
                animals = _animalAccessor.SelectAllAnimals(shelterId);
            }
            catch (Exception up)
            {
                throw new ApplicationException("Data not found.", up);
            }
            return animals;
        }

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
        /// <param name="animalId">int for the animal</param>
        /// <exception cref="Exception">No animal is retrived with that Id</exception>
        /// <returns>AnimalVM object</returns>	
        public AnimalVM RetrieveAnimalMedicalProfileByAnimalId(int AnimalId)
        {
            try
            {
                return _animalAccessor.SelectAnimalMedicalProfileByAnimalId(AnimalId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Animal medical profile not found", ex);
            }
        }
        public AnimalVM RetrieveAnimalAdoptableProfile(int animalId)
        {
            AnimalVM animalVM;

            try
            {
                animalVM = _animalAccessor.SelectAnimalAdoptableProfile(animalId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }

            return animalVM;
        }

        public List<AnimalVM> RetrieveAdoptedAnimalByUserId(int userId)
        {
            List<AnimalVM> animals;

            try
            {
                animals = _animalAccessor.SelectAdoptedAnimalByUserId(userId);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Data not found.", ex);
            }

            return animals;
        }

        public FosterPlacementRecord RetrieveFosterPlacementRecordNotes(int animalId)
        {
            FosterPlacementRecord fosterPlacementRecord;

            try
            {
                fosterPlacementRecord = _animalAccessor.SelectFosterPlacementRecordNotes(animalId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.",ex);
            }

            return fosterPlacementRecord;
        }

        public List<AnimalVM> RetrieveAnimalsByFundrasingEventId(int fundraisingEventId)
        {
            //throw new NotImplementedException();
            List<AnimalVM> animals;
            try
            {
                animals = _animalAccessor.SelectAnimalsByFundraisingEventId(fundraisingEventId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Animals for fundraising event not found.", ex);
            }

            return animals;
        }

        public List<AnimalVM> RetrieveAllAdoptableAnimals()
        {

            List<AnimalVM> animals = null;
            try
            {
                animals = _animalAccessor.SelectAllAdoptableAnimals();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an error retrieving animal data.", ex);
            }
            return animals;
        }
    }
}