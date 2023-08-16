using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessLayer;
using DataAccessLayerInterfaces;

namespace LogicLayer
{
    public class AnimalUpdatesManager : IAnimalUpdatesManager
    {
        IAnimalUpdatesAccessor _animalUpdatesAccessor = null;

        public AnimalUpdatesManager()
        {
            _animalUpdatesAccessor = new AnimalUpdatesAccessor();
        }

        public AnimalUpdatesManager(IAnimalUpdatesAccessor aua)
        {
            _animalUpdatesAccessor = aua;
        }

        public bool AddAnimalUpdatesByAnimalId(int animalId, string animalRecordNotes)
        {
            bool isSuccess = false;

            try
            {
                if (_animalUpdatesAccessor.InsertAnimalUpdatesByAnimalId(animalId, animalRecordNotes) == 1)
                {
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add animal updates", ex);
            }

            return isSuccess;
        }

        public List<AnimalUpdates> RetrieveAllAnimalUpdatesByAnimalId(int animalId)
        {
            List<AnimalUpdates> animalNotes = new List<AnimalUpdates>();

            try
            {
                animalNotes = _animalUpdatesAccessor.SelectAllAnimalUpdateByAnimalId(animalId);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Can not get animal note updates.", ex);
            }

            return animalNotes;
        }

        public string RetrieveAnimalUpdatesByAnimal(int animalId)
        {
            string result = "";

            try
            {
                result = _animalUpdatesAccessor.SelectAnimalUpdatesByAnimal(animalId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Can not find data", ex);
            }

            return result;
        }
    }
}
