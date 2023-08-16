using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataAccessLayerFakes;
using LogicLayer;
using DataObjects;
using System.Collections.Generic;

namespace LogicLayerTest
{
    [TestClass]
    public class FundraisingCampaignManagerTests
    {
        private FundraisingCampaignManager _fundraisingCampaignManager = null;
        private InstitutionalEntityManager _institutionalEntityManager = null;

        [TestInitialize]
        public void SetupTests()
        {
            _institutionalEntityManager = new InstitutionalEntityManager(new InstitutionalEntityAccessorFake());
            _fundraisingCampaignManager = new FundraisingCampaignManager(new FundraisingCampaignAccessorFake());
        }

        [TestCleanup]
        public void TeardownTests()
        {
            _fundraisingCampaignManager = null;
            _institutionalEntityManager = null;
            FundraisingFakeData.ResetFakeFundraisingCampaignData();
        }

        [TestMethod]
        public void TestRetrieveFundraisingCampaignsByShelterId()
        {
            int expectedResult = 7;
            int actualResult = 0;
            int shelterId = 100003;

            actualResult = _fundraisingCampaignManager.RetrieveAllFundraisingCampaignsByShelterId(shelterId).Count;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestUpdateFundraisingCampaign()
        {
            int expectedSponsors = 5;
            int actualSponsors = 0;
            FundraisingCampaignVM oldFundraisingCampaign = _fundraisingCampaignManager.RetrieveFundraisingCampaignByFundraisingCampaignId(100005);
            oldFundraisingCampaign.Sponsors = _institutionalEntityManager.RetrieveFundraisingSponsorsByCampaignId(100005);
            FundraisingCampaignVM newFundraisingCampaign = oldFundraisingCampaign.Copy();
            newFundraisingCampaign.Title = "a whole new title";

            newFundraisingCampaign.Sponsors.Remove(newFundraisingCampaign.Sponsors.Find(sponsor => sponsor.InstitutionalEntityId == 10));
            newFundraisingCampaign.Sponsors.Remove(newFundraisingCampaign.Sponsors.Find(sponsor => sponsor.InstitutionalEntityId == 12));
            newFundraisingCampaign.Sponsors.Add(new InstitutionalEntity() { InstitutionalEntityId = 19 });
            newFundraisingCampaign.Sponsors.Add(new InstitutionalEntity() { InstitutionalEntityId = 12 });
            newFundraisingCampaign.Sponsors.Add(new InstitutionalEntity() { InstitutionalEntityId = 13 });
            bool updateSuccessfully = _fundraisingCampaignManager.EditFundraisingCampaignDetails(oldFundraisingCampaign, newFundraisingCampaign);
            actualSponsors = _institutionalEntityManager.RetrieveFundraisingSponsorsByCampaignId(100005).Count;

            Assert.IsTrue(updateSuccessfully);
            Assert.AreEqual("a whole new title", _fundraisingCampaignManager.RetrieveFundraisingCampaignByFundraisingCampaignId(100005).Title);
            Assert.AreEqual(expectedSponsors, actualSponsors);
        }

        [TestMethod]
        public void TestAddFundraisingCampaign()
        {
            int expectedResult = 1;
            int actualResult = 0;
            int expectedSponsors = 2;
            int newCampaignId = 120000;
            int actualSponsors = 0;
            int campaignsBefore = _fundraisingCampaignManager.RetrieveAllFundraisingCampaignsByShelterId(100000).Count;

            _fundraisingCampaignManager.AddFundraisingCampaign(new FundraisingCampaignVM()
            {
                FundraisingCampaignId = newCampaignId,
                Title = "Hallo",
                Description = "World",
                ShelterId = 100000,
                Sponsors = new List<InstitutionalEntity>()
                {
                    new InstitutionalEntity() {InstitutionalEntityId = 10},
                    new InstitutionalEntity() {InstitutionalEntityId = 11}
                }
            });

            actualResult = _fundraisingCampaignManager.RetrieveAllFundraisingCampaignsByShelterId(100000).Count - campaignsBefore;
            actualSponsors = _institutionalEntityManager.RetrieveFundraisingSponsorsByCampaignId(newCampaignId).Count;

            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(expectedSponsors, actualSponsors);
        }

        [TestMethod]
        public void TestRetrieveFundraisingCampaignByFundraisingCampaignId()
        {
            int expectedCampaignerId = 100000;
            int actualCampaignId = 0;

            actualCampaignId = _fundraisingCampaignManager.RetrieveFundraisingCampaignByFundraisingCampaignId(expectedCampaignerId).FundraisingCampaignId;

            Assert.AreEqual(expectedCampaignerId, actualCampaignId);
        }

        [TestMethod]
        public void TestRemoveFundraisingCampaign()
        {
            int campaignId = 100000;
            var campaign = _fundraisingCampaignManager.RetrieveFundraisingCampaignByFundraisingCampaignId(campaignId);

            _fundraisingCampaignManager.RemoveFundraisingCampaign(campaign);
            var updatedCampaign = _fundraisingCampaignManager.RetrieveFundraisingCampaignByFundraisingCampaignId(campaignId);

            Assert.IsFalse(updatedCampaign.Active);

        }

        [TestMethod]
        public void TestAddCampaignUpdate()
        {
            CampaignUpdate campaignUpdate = new CampaignUpdate()
            {
                CampaignUpdateId = 100004,
                CampaignId = 100000,
                UpdateTitle = "Update 4",
                UpdateDescription = "Campaign is successful."
            };

            bool actualResult = _fundraisingCampaignManager.AddCampaignUpdate(campaignUpdate);

            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public void TestUpdateFundraisingCampaignResults()
        {
            // Arrange
            FundraisingCampaignVM oldFundraisingCampaign = _fundraisingCampaignManager.RetrieveFundraisingCampaignByFundraisingCampaignId(110000);
            FundraisingCampaignVM newFundraisingCampaign = new FundraisingCampaignVM
            {
                ShelterId = 100000,
                Description = "Garble",
                Complete = true,
                AmountRaised = 10000,
                NumOfAttendees = 110,
                NumAnimalsAdopted = 15
            };

            // Act
            bool actualResult = _fundraisingCampaignManager.EditFundraisingCampaignResults(oldFundraisingCampaign, newFundraisingCampaign);

            // Assert
            Assert.IsTrue(actualResult);
        }
    }
}
