using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    public class AnimalUpdatesFakes : IAnimalUpdatesAccessor
    {
        private List<AnimalUpdatesVM> fakeAnimalUpdates = new List<AnimalUpdatesVM>();
        private int animalRecordIndex = 100003;

        public AnimalUpdatesFakes()
        {
            fakeAnimalUpdates.Add(new AnimalUpdatesVM()
            {
                AnimalRecordId = 100000,
                AnimalId = 100000,
                AnimalRecordNotes = "This is note 1",
                AnimalRecordDate = DateTime.Now,
                AnimalName = "Dog A"
            });

            fakeAnimalUpdates.Add(new AnimalUpdatesVM()
            {
                AnimalRecordId = 100001,
                AnimalId = 100000,
                AnimalRecordNotes = "This is note 2",
                AnimalRecordDate = DateTime.Now,
                AnimalName = "Dog A"
            });

            fakeAnimalUpdates.Add(new AnimalUpdatesVM()
            {
                AnimalRecordId = 100002,
                AnimalId = 100001,
                AnimalRecordNotes = "This is note 3",
                AnimalRecordDate = DateTime.Now,
                AnimalName = "Dog B"
            });

            fakeAnimalUpdates.Add(new AnimalUpdatesVM()
            {
                AnimalRecordId = 100003,
                AnimalId = 100000,
                AnimalRecordNotes = "This is test note",
                AnimalRecordDate = DateTime.Now,
                AnimalName = "Dog a"
            });
        }

        public int InsertAnimalUpdatesByAnimalId(int animalId, string animalRecordNotes)
        {
            int rowAffected = 0;

            try
            {
                fakeAnimalUpdates.Add(new AnimalUpdatesVM()
                {
                    AnimalRecordId = animalRecordIndex,
                    AnimalId = animalId,
                    AnimalRecordNotes = animalRecordNotes,
                    AnimalRecordDate = DateTime.Now,
                    AnimalName = "No Name"
                });
                animalRecordIndex++;
                rowAffected = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rowAffected;
        }

        public List<AnimalUpdates> SelectAllAnimalUpdateByAnimalId(int animalId)
        {
            throw new NotImplementedException();
        }

        public string SelectAnimalUpdatesByAnimal(int animalId)
        {
            string result = "";

            foreach (AnimalUpdatesVM animalUpdatesVM in fakeAnimalUpdates)
            {
                if(animalUpdatesVM.AnimalId == animalId)
                {
                    result = animalUpdatesVM.AnimalRecordNotes;
                }
            }

            return result;
        }
    }
}
