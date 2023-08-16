using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IAnimalUpdatesManager
    {
        bool AddAnimalUpdatesByAnimalId(int animalId, string animalRecordNotes);
        string RetrieveAnimalUpdatesByAnimal(int animalId);
        List<AnimalUpdates> RetrieveAllAnimalUpdatesByAnimalId(int animalId);
    }
}
