using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Barry Mikulas
    /// Created: 2023/03/20
    /// 
    /// This is a class containing fake data to be used inside of fake Accessors that need to affect the same data for unit tests
    /// </summary>
    public static class AnimalFakeData
    {
        public static List<AnimalVM> Animals { get; set; }
        /// <summary>
        /// List<Tuple<FundraisingEventId, AnimalId>>
        /// </summary>
        public static List<Tuple<int, int>> FundraiserAnimal { get; set; }

        static AnimalFakeData()
        {
            ResetFakeAnimalData();
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/03/20
        /// 
        /// This method resets the data stored inside the properties to their initial values,
        /// this is needed when using performing unit test cleanup so the data resets in between
        /// </summary>
        public static void ResetFakeAnimalData()
        {
            Animals = new List<AnimalVM>()
            {
                new AnimalVM()
                {
                    AnimalId = 1000,
                    AnimalShelterId = 100000,
                    AnimalName = "Spot",
                    AnimalGender = "Male",
                    AnimalTypeId = "Dog",
                    AnimalBreedId = "English Bulldog",
                    AnimalStatusId = "Available",
                    KennelName = "Test kennel 1",
                    Personality = "Test personality 3",
                    Description = "Test description 3",
                    AnimalStatusDescription = "Test status description 3",
                    BroughtIn = DateTime.Parse("2023-06-03"),
                    MicrochipNumber = "Test SN",
                    Aggressive = false,
                    AggressiveDescription = null,
                    ChildFriendly = true,
                    NeuterStatus = true,
                    Notes = "old notes"
                },
                new AnimalVM()
                {
                    AnimalId = 1001,
                    AnimalShelterId = 100000,
                    AnimalName = "Spot",
                    AnimalGender = "Male",
                    AnimalTypeId = "Dog",
                    AnimalBreedId = "English Bulldog",
                    AnimalStatusId = "Available",
                    KennelName = "Test kennel 1",
                    Personality = "Test personality 3",
                    Description = "Test description 3",
                    AnimalStatusDescription = "Test status description 3",
                    BroughtIn = DateTime.Parse("2023-06-03"),
                    MicrochipNumber = "Test SN",
                    Aggressive = false,
                    AggressiveDescription = null,
                    ChildFriendly = true,
                    NeuterStatus = true,
                    Notes = "old notes"
                },
                new AnimalVM()
                {
                    AnimalId = 1002,
                    AnimalShelterId = 100000,
                    AnimalName = "Spot",
                    AnimalGender = "Male",
                    AnimalTypeId = "Dog",
                    AnimalBreedId = "English Bulldog",
                    AnimalStatusId = "Available",
                    KennelName = "Test kennel 1",
                    Personality = "Test personality 3",
                    Description = "Test description 3",
                    AnimalStatusDescription = "Test status description 3",
                    BroughtIn = DateTime.Parse("2023-06-03"),
                    MicrochipNumber = "Test SN",
                    Aggressive = false,
                    AggressiveDescription = null,
                    ChildFriendly = true,
                    NeuterStatus = true,
                    Notes = "old notes"
                },
                new AnimalVM()
                {
                    AnimalId = 1003,
                    AnimalShelterId = 100000,
                    AnimalName = "Spot",
                    AnimalGender = "Male",
                    AnimalTypeId = "Dog",
                    AnimalBreedId = "English Bulldog",
                    AnimalStatusId = "Available",
                    KennelName = "Test kennel 1",
                    Personality = "Test personality 3",
                    Description = "Test description 3",
                    AnimalStatusDescription = "Test status description 3",
                    BroughtIn = DateTime.Parse("2023-06-03"),
                    MicrochipNumber = "Test SN",
                    Aggressive = false,
                    AggressiveDescription = null,
                    ChildFriendly = true,
                    NeuterStatus = true,
                    Notes = "old notes"
                }
            };

            // List<Tuple<FundraisingEventId, AnimalId>>
            FundraiserAnimal = new List<Tuple<int, int>>()
            {
                new Tuple<int,int>(100000,1000),
                new Tuple<int,int>(100000,1001),
                new Tuple<int,int>(100000,1002),
                new Tuple<int,int>(100000,1003),
                new Tuple<int,int>(100001,1000),
                new Tuple<int,int>(100001,1001),
                new Tuple<int,int>(100002,1000),
                new Tuple<int,int>(100002,1003)
            };
        }
    }
}
