using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LogicLayerInterfaces;
using LogicLayer;
using DataAccessLayerFakes;
using DataObjects;

namespace LogicLayerTest
{
    [TestClass]
    public class InstitutionalEntityManagerTests
    {
        private IInstitutionalEntityManager _institutionalEntityManager = null;

        [TestInitialize]
        public void SetupTests()
        {
            _institutionalEntityManager = new InstitutionalEntityManager(new InstitutionalEntityAccessorFake());
            FundraisingFakeData.ResetFakeFundraisingCampaignData();
        }

        [TestCleanup]
        public void TeardownTests()
        {
            _institutionalEntityManager = null;
            FundraisingFakeData.ResetFakeFundraisingCampaignData();
        }

        [TestMethod]
        public void TestSelectFundraisingSponsorsByCampaignId()
        {
            int campaignId1 = 100000;
            int expectedResult1 = 5;
            int actualResult1 = 0;

            int campaignId2 = 100001;
            int expectedResult2 = 0;
            int actualResult2 = 0;

            int campaignId3 = 100004;
            int expectedResult3 = 3;
            int actualResult3 = 0;

            actualResult1 = _institutionalEntityManager.RetrieveFundraisingSponsorsByCampaignId(campaignId1).Count;
            actualResult2 = _institutionalEntityManager.RetrieveFundraisingSponsorsByCampaignId(campaignId2).Count;
            actualResult3 = _institutionalEntityManager.RetrieveFundraisingSponsorsByCampaignId(campaignId3).Count;

            Assert.AreEqual(expectedResult1, actualResult1);
            Assert.AreEqual(expectedResult2, actualResult2);
            Assert.AreEqual(expectedResult3, actualResult3);

        }

        [TestMethod]
        public void TestRetrievesCorrectNumberOfInstitutionalEntitiesByShelterIdAndEntityType()
        {
            InstitutionalEntityAccessorFake fakes = new InstitutionalEntityAccessorFake();
            string entityType = "Host";
            int shelterId = 100000;
            // arrange
            int expectedResult = fakes._institutionalEntitiesWithShelterId.FindAll(i => i.ContactType == entityType && i.ShelterId == shelterId).Count;

            // act
            int actualResult = _institutionalEntityManager.RetrieveAllInstitutionalEntitiesByShelterIdAndEntityType(shelterId, entityType).Count;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestAddNewInstitutionalEntity()
        {
            InstitutionalEntity entity = new InstitutionalEntity()
            {
                InstitutionalEntityId = 10000,
                CompanyName = "SpaceX",
                GivenName = "Glen",
                FamilyName = "Musk",
                Email = "glen@spacex.com",
                Phone = "1239876541",
                Address = "1233 Boca Chica Blvd",
                Address2 = null,
                Zipcode = "78520",
                ContactType = "Sponsor",
                ShelterId = 100000
            };

            bool actualResult = _institutionalEntityManager.AddInstitutionalEntity(entity);

            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public void TestUpdateInstitutionalEntityWorksWithCorrectData()
        {
            // Arrange
            InstitutionalEntity oldEntity = _institutionalEntityManager.RetrieveInstitutionalEntityByInstitutionalEntityId(1009);
            InstitutionalEntity newEntity = new InstitutionalEntity
            {
                InstitutionalEntityId = 1009,
                CompanyName = "SpaceX",
                GivenName = "Nole",
                FamilyName = "Musk",
                Email = "nole@spacex.com",
                Phone = "1339876541",
                Address = "1323 Boca Chico Blvd",
                Address2 = null,
                Zipcode = "78520",
                ContactType = "Contact",
                ShelterId = 100000
            };

            // Act
            bool actualResult = _institutionalEntityManager.EditInstitutionalEntity(oldEntity, newEntity);

            // Assert
            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public void TestUpdateInstitutionalEntityFailsWithBadData()
        {
            // Arrange
            InstitutionalEntity badEntity = new InstitutionalEntity
            {
                InstitutionalEntityId = 10008,
                CompanyName = "SpaceX",
                GivenName = "Nole",
                FamilyName = "Musk",
                Email = "nole@spacex.com",
                Phone = "1339876541",
                Address = "1323 Boca Chico Blvd",
                Address2 = null,
                Zipcode = "78520",
                ContactType = "Contact",
                ShelterId = 100000
            };
            InstitutionalEntity newEntity = badEntity;

            // Act
            bool actualResult = _institutionalEntityManager.EditInstitutionalEntity(badEntity, newEntity);

            // Assert
            Assert.IsFalse(actualResult);
        }

        [TestMethod]
        public void TestSelectFundraisingEventsInstitutionalEntitiesByEventIdAndEntityType()
        {
            //arrange
            string entityType = "Contact";
            string entityType2 = "Sponsor";
            string entityType3 = "Host";
            int eventId1 = 100000;
            int expectedResult1 = 1;
            int actualResult1;
            int expectedResult4 = 5;
            int actualResult4;
            int expectedResult5 = 2;
            int actualResult5;

            int eventId2 = 100001;
            int expectedResult2 = 2;
            int actualResult2;
            int expectedResult6 = 2;
            int actualResult6;
            int expectedResult7 = 0;
            int actualResult7;

            int eventId3 = 100002;
            int expectedResult3 = 1;
            int actualResult3;
            int expectedResult8 = 3;
            int actualResult8;
            int expectedResult9 = 1;
            int actualResult9;


            //act  eventId by Contact\Sponsor\Host
            actualResult1 = _institutionalEntityManager.RetrieveFundraisingInstitutionalEntitiesByEventIdAndContactType(eventId1, entityType).Count;
            actualResult4 = _institutionalEntityManager.RetrieveFundraisingInstitutionalEntitiesByEventIdAndContactType(eventId1, entityType2).Count;
            actualResult5 = _institutionalEntityManager.RetrieveFundraisingInstitutionalEntitiesByEventIdAndContactType(eventId1, entityType3).Count;

            actualResult2 = _institutionalEntityManager.RetrieveFundraisingInstitutionalEntitiesByEventIdAndContactType(eventId2, entityType).Count;
            actualResult6 = _institutionalEntityManager.RetrieveFundraisingInstitutionalEntitiesByEventIdAndContactType(eventId2, entityType2).Count;
            actualResult7 = _institutionalEntityManager.RetrieveFundraisingInstitutionalEntitiesByEventIdAndContactType(eventId2, entityType3).Count;

            actualResult3 = _institutionalEntityManager.RetrieveFundraisingInstitutionalEntitiesByEventIdAndContactType(eventId3, entityType).Count;
            actualResult8 = _institutionalEntityManager.RetrieveFundraisingInstitutionalEntitiesByEventIdAndContactType(eventId3, entityType2).Count;
            actualResult9 = _institutionalEntityManager.RetrieveFundraisingInstitutionalEntitiesByEventIdAndContactType(eventId3, entityType3).Count;


            //assert
            Assert.AreEqual(expectedResult1, actualResult1);
            Assert.AreEqual(expectedResult4, actualResult4);
            Assert.AreEqual(expectedResult5, actualResult5);

            Assert.AreEqual(expectedResult2, actualResult2);
            Assert.AreEqual(expectedResult6, actualResult6);
            Assert.AreEqual(expectedResult7, actualResult7);

            Assert.AreEqual(expectedResult3, actualResult3);
            Assert.AreEqual(expectedResult8, actualResult8);
            Assert.AreEqual(expectedResult9, actualResult9);

        }

        [TestMethod]
        public void TestRetrievesCorrectInstitutionalEntityByEventIdAndEntityType()
        {
            //arrange
            string entityType = "Host";
            string entityType2 = "Sponsor";
            string entityType3 = "Contact";

            int eventId1 = 100000;

            InstitutionalEntity expectedResult = new InstitutionalEntity() {
                InstitutionalEntityId = 14,
                Address = "123 ave. Cedar Rapids",
                Zipcode = "52001",
                Address2 = "Apt 1",
                CompanyName = "Awesome Business",
                ContactType = "Host",
                Email = "awesome@gmail.com",
                FamilyName = "Meltzner",
                GivenName = "Euguene",
                Phone = "1234567890"
            };
            InstitutionalEntity expectedResult2 = new InstitutionalEntity()
            {
                InstitutionalEntityId = 10,
                Address = "123 ave. Cedar Rapids",
                Zipcode = "52001",
                Address2 = "Apt 1",
                CompanyName = "Awesome Business",
                ContactType = "Sponsor",
                Email = "awesome@gmail.com",
                FamilyName = "Money",
                GivenName = "Made of",
                Phone = "1234567890"
            };
            InstitutionalEntity expectedResult3 = new InstitutionalEntity()
            {
                InstitutionalEntityId = 15,
                Address = "123 ave. Cedar Rapids",
                Zipcode = "52001",
                Address2 = "Apt 1",
                CompanyName = "Awesome Business",
                ContactType = "Contact",
                Email = "awesome@gmail.com",
                FamilyName = "Energy",
                GivenName = "Made of",
                Phone = "1234567890"
            };

            //act
            InstitutionalEntity actualResult = _institutionalEntityManager.RetrieveInstitutionalEntityByEventIdAndContactType(eventId1, entityType);
            InstitutionalEntity actualResult2 = _institutionalEntityManager.RetrieveInstitutionalEntityByEventIdAndContactType(eventId1, entityType2);
            InstitutionalEntity actualResult3 = _institutionalEntityManager.RetrieveInstitutionalEntityByEventIdAndContactType(eventId1, entityType3);

            //assert
            Assert.AreEqual(expectedResult.Address, actualResult.Address);
            Assert.AreEqual(expectedResult.Zipcode, actualResult.Zipcode);
            Assert.AreEqual(expectedResult.Address2, actualResult.Address2);
            Assert.AreEqual(expectedResult.CompanyName, actualResult.CompanyName);
            Assert.AreEqual(expectedResult.ContactType, actualResult.ContactType);
            Assert.AreEqual(expectedResult.Email, actualResult.Email);
            Assert.AreEqual(expectedResult.FamilyName, actualResult.FamilyName);
            Assert.AreEqual(expectedResult.GivenName, actualResult.GivenName);
            Assert.AreEqual(expectedResult.Phone, actualResult.Phone);

            Assert.AreEqual(expectedResult2.Address, actualResult2.Address);
            Assert.AreEqual(expectedResult2.Zipcode, actualResult2.Zipcode);
            Assert.AreEqual(expectedResult2.Address2, actualResult2.Address2);
            Assert.AreEqual(expectedResult2.CompanyName, actualResult2.CompanyName);
            Assert.AreEqual(expectedResult2.ContactType, actualResult2.ContactType);
            Assert.AreEqual(expectedResult2.Email, actualResult2.Email);
            Assert.AreEqual(expectedResult2.FamilyName, actualResult2.FamilyName);
            Assert.AreEqual(expectedResult2.GivenName, actualResult2.GivenName);
            Assert.AreEqual(expectedResult2.Phone, actualResult2.Phone);

            Assert.AreEqual(expectedResult3.Address, actualResult3.Address);
            Assert.AreEqual(expectedResult3.Zipcode, actualResult3.Zipcode);
            Assert.AreEqual(expectedResult3.Address2, actualResult3.Address2);
            Assert.AreEqual(expectedResult3.CompanyName, actualResult3.CompanyName);
            Assert.AreEqual(expectedResult3.ContactType, actualResult3.ContactType);
            Assert.AreEqual(expectedResult3.Email, actualResult3.Email);
            Assert.AreEqual(expectedResult3.FamilyName, actualResult3.FamilyName);
            Assert.AreEqual(expectedResult3.GivenName, actualResult3.GivenName);
            Assert.AreEqual(expectedResult3.Phone, actualResult3.Phone);
        }
        [TestMethod]
        public void TestSelectSponsorEventByName()
        {
            String name = "US Animals";
            int expectedCount = 0;
            int actualCount = 0;

            var _sponsorEvents = _institutionalEntityManager.RetrieveSponsorEventByName(name);
            actualCount = _sponsorEvents.Count;

            Assert.AreEqual(expectedCount, actualCount);
          
        }
    }
}
