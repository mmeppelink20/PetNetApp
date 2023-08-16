// Created By Asa Armstrong
// Created On 2023/02/02

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayerInterfaces;
using DataAccessLayerFakes;
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;

namespace LogicLayerTest.Animals
{
    [TestClass]
    public class AnimalDeathTests
    {
        private IDeathManager _deathManager = null;

        private DeathVM _death = new DeathVM()
        {
            UsersId = 100000,
            AnimalId = 100000,
            DeathDate = DateTime.Now,
            DeathCause = "death cause",
            DeathDisposal = "death disposal",
            DeathDisposalDate = DateTime.Now,
            DeathNotes = "death notes"
        };

        [TestInitialize]
        public void TestSetup()
        {
            _deathManager = new DeathManager(new DeathAccessorFake());
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _deathManager = null;
        }

        [TestMethod]
        public void TestAddAnimalDOD()
        {
            var expected = true;
            var actual = _deathManager.AddAnimalDeath(_death);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestRetrieveAnimalDeath()
        {
            _deathManager.AddAnimalDeath(_death);
            Assert.AreEqual(_death, _deathManager.RetrieveAnimalDeath(new Animal() { AnimalId = _death.AnimalId }));
        }

        [TestMethod]
        public void TestEditAnimalDeath()
        {
            _deathManager.AddAnimalDeath(_death);
            Death death = _death;
            _death.DeathNotes = "new death notes";

            Assert.AreEqual(true, _deathManager.EditAnimalDeath(_death, death));
        }
    }
}
