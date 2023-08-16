using DataAccessLayerInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerFakes
{
    public class AnimalAccessorFakes : IAnimalAccessor
    {
        List<Animal> animals = new List<Animal>();
        Dictionary<string, List<string>> breeds = new Dictionary<string, List<string>>();
        List<string> genders = new List<string>();
        List<string> types = new List<string>();
        List<string> statuses = new List<string>();
        List<AnimalVM> fakeAnimals = new List<AnimalVM>();
        List<Animal> fakeAnimals1 = new List<Animal>();
        private AnimalVM fakeAnimalVM = new AnimalVM();
        List<Applicant> fakeApplicants = new List<Applicant>();
        List<FosterPlacement> fakeFosterPlacements = new List<FosterPlacement>();
        List<FosterPlacementRecord> fakeFosterPlacementRecords = new List<FosterPlacementRecord>();
        private List<AnimalVM> _fakeFundraisingEventAnimals = AnimalFakeData.Animals;
        private List<Tuple<int, int>> _fakefundraiserAnimal = AnimalFakeData.FundraiserAnimal;

        public AnimalAccessorFakes()
        {
            fakeAnimalVM = new AnimalVM()
            {
                AnimalId = 100000,
                AnimalName = "Chip",
                AnimalGender = "Male",
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

            animals.Add(new AnimalVM
            {
                AnimalName = "Rufus",
                AnimalGender = "Male",
                AnimalTypeId = "Dog",
                AnimalBreedId = "Lab",
                Personality = "Friendly",
                Description = "this is a sample description",
                BroughtIn = new DateTime(),
                MicrochipNumber = "S/N-3234529",
                Aggressive = false
            });

            fakeAnimals.Add(new AnimalVM
            {
                AnimalId = 999999,
                AnimalName = "Test name 1",
                AnimalGender = "Test gender 1",
                AnimalTypeId = "Test type 1",
                AnimalBreedId = "Test breed 1",
                KennelName = "Test kennel 1",
                Personality = "Test personality 1",
                Description = "Test description 1",
                AnimalStatusId = "Test status 1",
                AnimalStatusDescription = "Test status description 1",
                BroughtIn = DateTime.Parse("2023-06-01"),
                MicrochipNumber = "Test SN",
                Aggressive = false,
                AggressiveDescription = "Not aggressive",
                ChildFriendly = true,
                NeuterStatus = true,
                Notes = "N/A"
            });

            fakeAnimals.Add(new AnimalVM
            {
                AnimalId = 999998,
                AnimalShelterId = 100000,
                AnimalName = "Test name 2",
                AnimalGender = "Test gender 2",
                AnimalTypeId = "Test type 2",
                AnimalBreedId = "Test breed 2",
                KennelName = "Test kennel 2",
                Personality = "Test personality 2",
                Description = "Test description 2",
                AnimalStatusId = "Test status 2",
                AnimalStatusDescription = "Test status description 2",
                BroughtIn = DateTime.Parse("2023-06-02"),
                MicrochipNumber = "Test SN",
                Aggressive = true,
                AggressiveDescription = "Bites",
                ChildFriendly = false,
                NeuterStatus = false,
                Notes = "N/A"
            });

            fakeAnimals.Add(new AnimalVM
            {
                AnimalId = 999997,
                AnimalShelterId = 100000,
                AnimalName = "Test name 3",
                AnimalGender = "Test gender 3",
                AnimalTypeId = "Test type 3",
                AnimalBreedId = "Test breed 3",
                KennelName = "Test kennel 1",
                Personality = "Test personality 3",
                Description = "Test description 3",
                AnimalStatusId = "Test status 3",
                AnimalStatusDescription = "Test status description 3",
                BroughtIn = DateTime.Parse("2023-06-03"),
                MicrochipNumber = "Test SN",
                Aggressive = false,
                AggressiveDescription = "Not aggressive",
                ChildFriendly = true,
                NeuterStatus = true,
                Notes = "old notes"
            });

            fakeAnimals1.Add(new Animal
            {
                AnimalId = 100001,
                AnimalShelterId = 100000,
                AnimalName = "Remy",
                Personality = "Gay",
                Description = "Brown and White",
                BroughtIn = DateTime.Today,
                MicrochipNumber = "111111111111111",
                Aggressive = false,
                AggressiveDescription = null,
                ChildFriendly = true,
                NeuterStatus = true,
                Notes = null,
                AnimalTypeId = "Dog",
                AnimalBreedId = "Lab",
                AnimalStatusId = "Available"
            });

            fakeAnimals1.Add(new Animal
            {
                AnimalId = 100002,
                AnimalShelterId = 100000,
                AnimalName = "Jack",
                Personality = "Nice",
                Description = "Black and White",
                BroughtIn = DateTime.Today,
                MicrochipNumber = "111111111111121",
                Aggressive = false,
                AggressiveDescription = null,
                ChildFriendly = true,
                NeuterStatus = true,
                Notes = null,
                AnimalTypeId = "Dog",
                AnimalBreedId = "Lab",
                AnimalStatusId = "Available"
            });

            fakeAnimals1.Add(new Animal
            {
                AnimalId = 100002,
                AnimalShelterId = 100000,
                AnimalName = "Kyle",
                Personality = "Mean",
                Description = "Brown and White",
                BroughtIn = DateTime.Today,
                MicrochipNumber = "111111111111115",
                Aggressive = false,
                AggressiveDescription = null,
                ChildFriendly = true,
                NeuterStatus = true,
                Notes = null,
                AnimalTypeId = "Dog",
                AnimalBreedId = "Lab",
                AnimalStatusId = "Available"
            });

            fakeAnimals1.Add(new Animal
            {
                AnimalId = 100003,
                AnimalShelterId = 100000,
                AnimalName = "Kate",
                Personality = "Gay",
                Description = "Brown and White",
                BroughtIn = DateTime.Today,
                MicrochipNumber = "111111111111811",
                Aggressive = false,
                AggressiveDescription = null,
                ChildFriendly = true,
                NeuterStatus = true,
                Notes = null,
                AnimalTypeId = "Dog",
                AnimalBreedId = "Lab",
                AnimalStatusId = "Available"
            });

            fakeAnimals1.Add(new Animal
            {
                AnimalId = 100004,
                AnimalShelterId = 100000,
                AnimalName = "Matt",
                Personality = "Gay",
                Description = "Brown and White",
                BroughtIn = DateTime.Today,
                MicrochipNumber = "111119111111111",
                Aggressive = false,
                AggressiveDescription = null,
                ChildFriendly = true,
                NeuterStatus = true,
                Notes = null,
                AnimalTypeId = "Dog",
                AnimalBreedId = "Lab",
                AnimalStatusId = "Available"
            });

            fakeAnimals1.Add(new Animal
            {
                AnimalId = 100005,
                AnimalShelterId = 100000,
                AnimalName = "Gaylord",
                Personality = "Gay",
                Description = "Brown and White",
                BroughtIn = DateTime.Today,
                MicrochipNumber = "211111111111111",
                Aggressive = false,
                AggressiveDescription = null,
                ChildFriendly = true,
                NeuterStatus = true,
                Notes = null,
                AnimalTypeId = "Dog",
                AnimalBreedId = "Lab",
                AnimalStatusId = "Available"
                
            });

            breeds.Add("Test breed 1", new List<string> { "Test type 1" });
            breeds.Add("Test breed 2", new List<string> { "Test type 2" });
            genders.Add("Test gender 1");
            genders.Add("Test gender 2");
            types.Add("Test type 1");
            types.Add("Test type 2");
            statuses.Add("Test status 1");
            statuses.Add("Test status 2");

            fakeApplicants.Add(new Applicant 
            { 
                ApplicantId = 100000,
                UserId = 100000
            });

            fakeFosterPlacements.Add(new FosterPlacement
            {
                FosterPlacementId = 100000,
                AnimalId = 100000,
                ApplicantId = 100000
            });

            fakeFosterPlacements.Add(new FosterPlacement
            {
                FosterPlacementId = 100001,
                AnimalId = 100001,
                ApplicantId = 100000
            });

            fakeFosterPlacements.Add(new FosterPlacement
            {
                FosterPlacementId = 100002,
                AnimalId = 100002,
                ApplicantId = 100000
            });

            fakeFosterPlacementRecords.Add(new FosterPlacementRecord
            {
                FosterPlacementRecordId = 100000,
                FosterPlacementId = 100000,
                FosterPlacementRecordNotes = "This is a note"
            });

        }

        public int InsertAnimal(AnimalVM animal)
        {
            fakeAnimals.Add(animal);
            int rows = 0;

            for (int i = 0; i < fakeAnimals.Count; i++)
            {
                if (fakeAnimals[i].AnimalId == animal.AnimalId)
                {
                    rows = 1;
                }
            }
            return rows;
        }

        public List<Animal> SelectAllAnimals(String animalName)
        {
            return animals.Where(a => a.AnimalName == animalName).ToList();
        }

        public AnimalVM SelectAnimalMedicalProfileByAnimalId(int animalId)
        {
            AnimalVM animalVM = null;
            animalVM = fakeAnimalVM;
            return animalVM;
        }

        public AnimalVM SelectAnimalByAnimalId(int animalId, int shelterId)
            {
            AnimalVM animalVM = new AnimalVM();

            foreach (AnimalVM fakeAnimal in fakeAnimals)
            {
                if(fakeAnimal.AnimalId == animalId && fakeAnimal.AnimalShelterId == shelterId)
                {
                    animalVM = fakeAnimal;
                    return animalVM;
                }
            }
            if (animalVM == null)
            {
                throw new ApplicationException("Animal not found");
            }
            return animalVM;
        }

        public Dictionary<string, List<string>> SelectAllAnimalBreeds()
        {
            return breeds;
        }

        public List<string> SelectAllAnimalGenders()
        {
            return genders;
        }

        public List<string> SelectAllAnimalTypes()
        {
            return types;
        }

        public List<string> SelectAllAnimalStatuses()
        {
            return statuses;
        }

        public int UpdateAnimal(AnimalVM oldAnimal, AnimalVM newAnimal)
        {
            int result = 0;

            for (int i = 0; i < fakeAnimals.Count; i++)
            {
                if(fakeAnimals[i].AnimalId == oldAnimal.AnimalId)
                {
                    // the real database will check for every editable field in the stored procedure
                    fakeAnimals[i].Notes = fakeAnimals[i].Notes == oldAnimal.Notes ?  fakeAnimals[i].Notes = newAnimal.Notes : oldAnimal.Notes;

                    result++;
                    break;
                }
            }

            return result;
        }

        public List<Animal> SelectAllAnimals(int shelterId)
        {
            return fakeAnimals1.Where(animal => animal.AnimalShelterId == shelterId).ToList();

        }

        public List<Animal> SelectAllAnimalsNotInKennel()
        {
            throw new NotImplementedException();
        }

        public AnimalVM SelectAnimalAdoptableProfile(int animalId)
        {
            AnimalVM animalVM = new AnimalVM();

            foreach (AnimalVM animal in fakeAnimals)
            {
                if (animal.AnimalId == animalId)
                {
                    animalVM = animal;
                    break;
                }
            }

            return animalVM;
        }

        public List<AnimalVM> SelectAdoptedAnimalByUserId(int usersId)
        {
            List<AnimalVM> animals = new List<AnimalVM>();

            foreach (FosterPlacement fosterPlacement in fakeFosterPlacements)
            {
                foreach (Applicant applicant in fakeApplicants)
                {
                    if (applicant.UserId == usersId && applicant.ApplicantId == fosterPlacement.ApplicantId)
                    {
                        animals.Add(new AnimalVM());
                        break;
                    }
                }
            }

            return animals;
        }

        public FosterPlacementRecord SelectFosterPlacementRecordNotes(int animalId)
        {
            FosterPlacementRecord fosterPlacementRecord = null;

            foreach (FosterPlacement fosterPlacement in fakeFosterPlacements)
            {
                foreach (FosterPlacementRecord placementRecord in fakeFosterPlacementRecords)
                {
                    if (placementRecord.FosterPlacementId == fosterPlacement.FosterPlacementId && fosterPlacement.AnimalId == animalId)
                    {
                        fosterPlacementRecord = placementRecord;
                        break;
                    }
                }
            }

            return fosterPlacementRecord;
        }

        public List<AnimalVM> SelectAnimalsByFundraisingEventId(int fundraisingEventId)
        {
            //throw new NotImplementedException();
            var fundraisingEventAnimals = from animalFundraisingEventRecord in _fakeFundraisingEventAnimals
                                          join fundraisingEventAnimalRecord in _fakefundraiserAnimal on animalFundraisingEventRecord.AnimalId equals fundraisingEventAnimalRecord.Item2
                                          where fundraisingEventAnimalRecord.Item1 == fundraisingEventId
                                          select animalFundraisingEventRecord;

            return fundraisingEventAnimals.ToList();
        }

        public List<AnimalVM> SelectAllAdoptableAnimals()
        {
            return fakeAnimals.Where(animal => animal.AnimalStatusId == "Test status 3").ToList();
        }
    }
}
