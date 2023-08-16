using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DataAccessLayerFakes;
using LogicLayer;
using DataObjects;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerTest
{
    /// <summary>
    /// Summary description for AnimalManagerTests
    /// </summary>
    [TestClass]
    public class AnimalManagerTests
    {
        private AnimalManager _animalManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            //_animalManager = new AnimalManager();
            _animalManager = new AnimalManager(new AnimalAccessorFakes());
        }

        [TestCleanup]
        public void testTearDown()
        {
            _animalManager = null;
            AnimalFakeData.ResetFakeAnimalData();
        }

        [TestMethod]
        public void TestRetrieveAllAnimals()
        {
            int expectedCount = 1;
            int actualCount = 0;
            string animal = "Rufus";

            var animals = _animalManager.RetrieveAllAnimals(animal);
            actualCount = animals.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestRetrieveAnimalByAnimalIdReturnsCorrectAnimal()
        {
            // Arrange
            const int animalId = 999998;
            const int shelterId = 100000;
            const string expectedAnimalName = "Test name 2";
            string actualAnimalName = "";

            // Act
            Animal animal = _animalManager.RetrieveAnimalByAnimalId(animalId, shelterId);
            actualAnimalName = animal.AnimalName;

            // Assert
            Assert.AreEqual(expectedAnimalName, actualAnimalName);
        }

        [TestMethod]
        public void TestRetrieveAllAnimalBreeds()
        {
            // Arrange
            int expectedCount = 2;
            int actualCount = 0;

            // Act
            var animals = _animalManager.RetrieveAllAnimalBreeds();
            actualCount = animals.Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestRetrieveAllAnimalGenders()
        {
            // Arrange
            int expectedCount = 2;
            int actualCount = 0;

            // Act
            var animals = _animalManager.RetrieveAllAnimalGenders();
            actualCount = animals.Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestRetrieveAllAnimalTypes()
        {
            // Arrange
            int expectedCount = 2;
            int actualCount = 0;

            // Act
            var animals = _animalManager.RetrieveAllAnimalTypes();
            actualCount = animals.Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestRetrieveAllAnimalStatuses()
        {
            // Arrange
            int expectedCount = 2;
            int actualCount = 0;

            // Act
            var animals = _animalManager.RetrieveAllAnimalStatuses();
            actualCount = animals.Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestUpdateAnimalWorksWithCorrectData()
        {
            // Arrange
            AnimalVM oldAnimal = _animalManager.RetrieveAnimalByAnimalId(999997, 100000);
            AnimalVM newAnimal = new AnimalVM
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
                Notes = "new notes"
            };

            // Act
            bool actualResult = _animalManager.EditAnimal(oldAnimal, newAnimal);

            // Assert
            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public void TestUpdateAnimalFailsWithBadData()
        {
            // Arrange
            AnimalVM badAnimal = new AnimalVM
            {
                AnimalId = 899997,
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
                Notes = "new notes"
            };
            AnimalVM newAnimal = badAnimal;

            // Act
            bool actualResult = _animalManager.EditAnimal(badAnimal, newAnimal);

            // Assert
            Assert.IsFalse(actualResult);
        }

        [TestMethod]
        public void TestRetrieveAllAnimalsReturnsCorrectList()
        {
            // arrange
            const int expectedCount = 6;
            int shelterId = 100000;
            int actualcount = 0;

            // act
            actualcount = _animalManager.RetrieveAllAnimals(shelterId).Count;

            // assert 
            Assert.AreEqual(expectedCount, actualcount);
        }

        [TestMethod]
        public void TestRetrieveAllAdoptableAnimalsReturnsCorrectList()
        {
            // arrange
            const int expectedCount = 1;
            int actualcount = 0;

            // act
            actualcount = _animalManager.RetrieveAllAdoptableAnimals().Count;

            // assert 
            Assert.AreEqual(expectedCount, actualcount);
        }

        [TestMethod]
        public void TestRetrieveAnimalMedicalProfileByAnimalId()
        {
            //arrange 
            int animalId = 100000;
            string expectedAnimalName = "Chip";
            string actualName;

            // act
            var animal = _animalManager.RetrieveAnimalMedicalProfileByAnimalId(animalId);
            actualName = animal.AnimalName;

            // assert
            Assert.AreEqual(expectedAnimalName, actualName);

        }

        [TestMethod]
        public void TestAddAnimal()
        {
            // arrange
            AnimalVM animal = new AnimalVM()
            {
                AnimalId = 899997,
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
                Notes = "new notes"
            };

            bool actualResult = _animalManager.AddAnimal(animal);

            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public void TestSelectAnimalAdoptableProfile()
        {
            string expectedResult = "Test name 1";
            string actualResult = "";

            actualResult = _animalManager.RetrieveAnimalAdoptableProfile(999999).AnimalName;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestSelectAdoptedAnimalByUserId()
        {
            int expectedResult = 3;
            int actualResult = 0;

            actualResult = _animalManager.RetrieveAdoptedAnimalByUserId(100000).Count();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestSelectFosterPlacementRecordNotes()
        {
            string expectedResult = "This is a note";
            string actualResult = "";

            actualResult = _animalManager.RetrieveFosterPlacementRecordNotes(100000).FosterPlacementRecordNotes;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestRetrieveAllAnimalsByFundraisingEventReturnsCorrectList()
        {
            //arrange
            int fundraisingEventId = 100000;
            int expectedResult = 4;
            int actualResult;
            int fundraisingEventId2 = 100001;
            int expectedResult2 = 2;
            int actualResult2;
            int fundraisingEventId3 = 100002;
            int expectedResult3 = 2;
            int actualResult3;

            //act
            actualResult = _animalManager.RetrieveAnimalsByFundrasingEventId(fundraisingEventId).Count;
            actualResult2 = _animalManager.RetrieveAnimalsByFundrasingEventId(fundraisingEventId2).Count;
            actualResult3 = _animalManager.RetrieveAnimalsByFundrasingEventId(fundraisingEventId3).Count;

            //assert
            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(expectedResult2, actualResult2);
            Assert.AreEqual(expectedResult3, actualResult3);
        }
    }
}
