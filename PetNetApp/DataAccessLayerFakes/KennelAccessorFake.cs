using DataAccessLayerInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerFakes
{
    public class KennelAccessorFake : IKennelAccessor
    {
        private List<KennelVM> fakeKennelVMs = new List<KennelVM>();
        private Kennel fakeKennel = new Kennel();
        private List<Animal> fakeAnimals = new List<Animal>();
        private Animal fakeAnimal = new Animal();
        private KennelVM fakeKennelVM = new KennelVM();
        private List<string> fakeAnimalTypes;
        private List<Kennel> fakeKennels = new List<Kennel>();
        private List<Tuple<Animal, Kennel>> fakeAnimalKenneling = new List<Tuple<Animal, Kennel>>();
        private Images fakeImage = new Images() { ImageId = "ImageID", ImageFileName = "FileName" };
        public KennelAccessorFake()
        {
            fakeKennelVM = new KennelVM()
            {
                KennelId = 1,
                ShelterId = 1,
                KennelName = "Kennel1",
                KennelActive = true,
                AnimalTypeId = "Dog",
                ShelterName = "Shelter1",
                Animal = new AnimalVM()
            };

            fakeAnimal = new Animal()
            {
                AnimalId = 100000,
                AnimalName = "Chip",
                AnimalTypeId = "Dog",
                AnimalBreedId = "Lab",
                Personality = "Calm",
                Description = "Needs Attention",
                AnimalStatusId = "Healthy",
                BroughtIn = DateTime.Now,
                MicrochipNumber = "dog12345",
                Aggressive = false,
                AggressiveDescription = "Not Aggressive",
                ChildFriendly = true,
                NeuterStatus = true,
                Notes = "No notes"
            };

            fakeAnimals.Add( new Animal
            {
                AnimalId = 100000,
                AnimalName = "Chip",
                AnimalTypeId = "Dog",
                AnimalBreedId = "Lab",
                Personality = "Calm",
                Description = "Needs Attention",
                AnimalStatusId = "Healthy",
                BroughtIn = DateTime.Now,
                MicrochipNumber = "dog12345",
                Aggressive = false,
                AggressiveDescription = "Not Aggressive",
                ChildFriendly = true,
                NeuterStatus = true,
                Notes = "No notes"
            });

            fakeKennel = new Kennel()
            {
                KennelId = 100000,
                ShelterId = 100000,
                KennelName = "The shelter",
                KennelSpace = 5,
                KennelActive = true
            };

            fakeKennelVMs.Add(new KennelVM
            {
                KennelId = 1,
                ShelterId = 1,
                KennelName = "Kennel1",
                KennelActive = true,
                AnimalTypeId = "Dog",
                ShelterName = "Shelter1",
                Animal = new AnimalVM()
            });
            fakeKennelVMs.Add(new KennelVM
            {
                KennelId = 2,
                ShelterId = 1,
                KennelName = "Kennel2",
                KennelActive = true,
                AnimalTypeId = "Dog",
                ShelterName = "Shelter2",
                Animal = new AnimalVM()
            });
            fakeKennelVMs.Add(new KennelVM
            {
                KennelId = 3,
                ShelterId = 1,
                KennelName = "Kennel3",
                KennelActive = true,
                AnimalTypeId = "Dog",
                ShelterName = "Shelter3",
                //Animal = new Animal()
            });

            fakeAnimalTypes = new List<string>() { "Panda", "Boar", "Fish"};

            fakeAnimalKenneling.Add(
                new Tuple<Animal, Kennel>(new Animal(), new Kennel() { KennelId = 1 }
            ));
            fakeAnimalKenneling.Add(
                new Tuple<Animal, Kennel>(new Animal(), new Kennel() { KennelId = 2 }
            ));


        }

        public int DeleteAnimalKennelingByKennelId(int KennelId)
        {
            int result = fakeAnimalKenneling.Count;
            for (int i = fakeAnimalKenneling.Count-1; i > -1; i--)
            {
                if (fakeAnimalKenneling[i].Item2.KennelId == KennelId)
                {
                    fakeAnimalKenneling.Remove(fakeAnimalKenneling[i]);
                    result -= fakeAnimalKenneling.Count;
                }
            }
            return result;
        }

        public int InsertKennel(Kennel kennel)
        {
            fakeKennels.Add(kennel);
            int rows = 0;

            for (int i = 0; i < fakeKennels.Count; i++)
            {
                if (fakeKennels[i].KennelId == kennel.KennelId)
                {
                    rows = 1;
                }
            }
            return rows;
        }

        public List<string> SelectAnimalTypes()
        {
            return fakeAnimalTypes;
        }

        /// <summary>
        /// Gwen Arman
        /// Created: 2023/02/01
        /// 
        /// Methods retrieves fake kennels from the kennel accessor fakes with the associated shelter id
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="shelterid">A description of the parameter that this method takes</param>
        /// <returns>List<KennelVM></returns>
        public List<KennelVM> SelectKennels(int shelterid)
        {
            return fakeKennelVMs.Where(ken => ken.ShelterId == shelterid).ToList();
        }

        public Kennel SelectKennelIdByAnimalId(int animalId)
        {
            Kennel kennel = null;
            kennel = fakeKennel;
            return kennel;
        }

        public int InsertAnimalIntoKennelByAnimalId(int KennelId, int AnimalId)
        {
            int result = fakeAnimalKenneling.Count;
            for (int i = 0; i < fakeAnimalKenneling.Count; i++)
            {
                if (fakeAnimalKenneling[i].Item2.KennelId == KennelId)
                {
                    fakeAnimalKenneling.Add(fakeAnimalKenneling[i]);
                    result += fakeAnimalKenneling.Count;
                }
            }
            return result;

            //int result = fakeKennelVMs.Count();
            //fakeKennelVMs.Add(fakeKennelVM);
            //result = fakeKennelVMs.Count() - result;
            //return result;
        }

        public List<Animal> SelectAllAnimalsForKennel(int ShelterId)
        {
            List<Animal> animals = null;
            animals = fakeAnimals;
            return animals;
        }

        public int UpdateKennelStatusByKennelId(int KennelId)
        {
            int rows = 0;

            for (int i = 0; i < fakeKennelVMs.Count; i++)
            {
                if (fakeKennelVMs[i].KennelId == KennelId)
                {
                    fakeKennelVMs[i].KennelActive = false;
                    rows = 1;
                }
                
            }
            return rows;
        }

        // Created By: Asa Armstrong
        // Actually removes a Kennel instead of an AnimalKenneling
        public int DeleteAnimalKennelingByKennelIdAndAnimalId(int kennelId, int animalId)
        {
            int rowsAffected = 0;

            if (fakeKennelVMs.Remove(fakeKennelVMs.FirstOrDefault(k => k.KennelId == kennelId)))
            {
                rowsAffected = 1;
            }

            return rowsAffected;
        }

        public List<Kennel> SelectAllEmptyKennels(int shelterId)
        {
            var kennelsWithoutAnimals = fakeKennelVMs.Where(
                kennel => !fakeAnimalKenneling.Exists(animalKenneling => kennel.KennelId == animalKenneling.Item2.KennelId));

            return kennelsWithoutAnimals.Select(kennelVM => (Kennel)kennelVM).ToList();
        }

        public Images SelectImageByAnimalId(int animalId)
        {
            return fakeImage;
        }
    }
}
