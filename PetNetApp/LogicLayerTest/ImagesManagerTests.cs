using DataAccessLayerFakes;
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LogicLayerTest
{
    [TestClass]
    public class ImagesManagerTests
    {
        private IImagesManager _imagesManager = null;
        
        [TestInitialize]
        public void TestSetup()
        {
            _imagesManager = new ImagesManager(new ImagesAccessorFakes());
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _imagesManager = null;
        }

        [TestMethod]
        public void TestSelectImagesByAnimalId()
        {
            const int expectedResult = 2;
            int animalId = 5;
            int actualResult = 0;

            actualResult = _imagesManager.RetrieveMedicalImagesByAnimalId(animalId).Count;

            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public void TestInsertMedicalImageByAnimalId()
        {
            bool expectedResult = true;
            int animalId = 1;
            string imagefileName = "beepboop.png";
            bool actualResult = _imagesManager.AddMedicalImageByAnimalId(animalId, imagefileName);

            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        public void AddImagesByUris()
        {
            List<string> newUris = new List<string>() { @"C:\images\image.png", @"C:/images/image2.png" };
            int expectedResult = newUris.Count;
            int actualResult = 0;

            List<Images> newImages = _imagesManager.AddImagesByUris(newUris);
            actualResult = newImages.Count;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void AddImageByUri()
        {
            string newUri = @"C:\images\image.png";
            int expectedResult = 1;
            int actualResult = 0;

            Images newImage = _imagesManager.AddImageByUri(newUri);
            actualResult = newImage != null ? 1 : 0;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestRetrieveMedicalImagesByAnimalId()
        {
            int animalId = 1;
            int expectedResult = 1;
            int actualResult = 0;

            actualResult = _imagesManager.RetrieveMedicalImagesByAnimalId(animalId).Count;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void AddMedicalImagesByAnimalId()
        {
            int animalId = 1;
            List<string> newUris = new List<string>() { @"C:\images\image.png", @"C:/images/image2.png" };
            int expectedResult = _imagesManager.RetrieveMedicalImagesByAnimalId(animalId).Count + newUris.Count;
            int actualResult = 0;

            _imagesManager.AddMedicalImagesByAnimalId(animalId, newUris);
            actualResult = _imagesManager.RetrieveMedicalImagesByAnimalId(animalId).Count;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestInsertAnimalImageByAnimalId()
        {
            bool expectedResult = true;
            int animalId = 1;
            string imagefileName = "beepboop.png";
            bool actualResult = _imagesManager.AddAnimalImageByAnimalId(animalId, imagefileName);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestInsertAnimalImagesByAnimalId()
        {
            int animalId = 1;
            List<string> newUris = new List<string>() { @"C:\images\image.png", @"C:/images/image2.png" };
            int expectedResult = _imagesManager.RetrieveAnimalImagesByAnimalId(animalId).Count + newUris.Count;
            int actualResult = 0;

            _imagesManager.AddAnimalImagesByAnimalId(animalId, newUris);
            actualResult = _imagesManager.RetrieveAnimalImagesByAnimalId(animalId).Count;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestRetrieveAnimalImagesByAnimalId()
        {
            int animalId = 2;
            int expectedResult = 1;
            int actualResult = 0;

            actualResult = _imagesManager.RetrieveAnimalImagesByAnimalId(animalId).Count;

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
