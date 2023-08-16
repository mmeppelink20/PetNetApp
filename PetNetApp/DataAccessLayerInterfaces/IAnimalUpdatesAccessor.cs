using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IAnimalUpdatesAccessor
    {
        /// <summary>
        /// Hoang Chu
        /// Created: 2023/02/17
        /// 
        /// Insert Animal note to the database
        /// </summary>
        /// <param name="animalId"></param>
        /// <param name="animalRecordNotes"></param>
        /// <returns></returns>
        int InsertAnimalUpdatesByAnimalId(int animalId, string animalRecordNotes);

        /// <summary>
        /// Hoang Chu
        /// Created: 2023/02/17
        /// 
        /// Select Animal note by animalId
        /// </summary>
        /// <param name="animalId"></param>
        /// <returns></returns>
        string SelectAnimalUpdatesByAnimal(int animalId);
        List<AnimalUpdates> SelectAllAnimalUpdateByAnimalId(int animalId);
    }
}
