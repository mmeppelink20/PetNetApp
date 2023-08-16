using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;


using DataAccessLayerFakes;
using LogicLayer;
using DataObjects;

namespace LogicLayerTest
{
    /// <summary>
    /// Brian Collum
    /// Created: 2023/02/23
    /// ShelterManagerTests tests the functionality of ShelterManager methods
    /// </summary>
    [TestClass]
    public class ShelterManagerTests
    {
        private ShelterManager _shelterManager = null;
        private List<Shelter> _sheterList = null;

        [TestInitialize]
        public void TestSetup()
        {
            _shelterManager = new ShelterManager(new ShelterAccessorFakes());
        }
        [TestMethod]
        public void TestGetShelterList()
        {
            List<Shelter> testList = new List<Shelter>();

            int listCountExpected = 3;
            int listCountActual = 0;

            Shelter testShelterZero = new Shelter
            {
                ShelterId = 0,
                ShelterName = "Test Shelter 00",
                Address = "Fake area 00",
                Address2 = "Fake address 00",
                ZipCode = "50001",
                Phone = "000-000-0000",
                Email = "zero@zero.zerp",
                AreasOfNeed = "There is no shelter here",
                ShelterActive = false
            };
            Shelter testShelterOne = new Shelter
            {
                ShelterId = 000001,
                ShelterName = "Test Shelter 01",
                Address = "Fake area 01",
                Address2 = "Fake address 01",
                ZipCode = "50001",
                Phone = "555-666-7777",
                Email = "fake@fake.fake",
                AreasOfNeed = "Need fake things",
                ShelterActive = true
            };
            Shelter testShelterTwo = new Shelter
            {
                ShelterId = 000002,
                ShelterName = "Test Shelter 02",
                Address = "Fake area 02",
                Address2 = null,
                ZipCode = "50002",
                Phone = null,
                Email = null,
                AreasOfNeed = null,
                ShelterActive = false
            };
            testList = _shelterManager.GetShelterList();
            listCountActual = testList.Count;

            Assert.AreEqual(listCountExpected, listCountActual);
            Assert.AreEqual(testList[0].ShelterId, testShelterZero.ShelterId);
            Assert.AreEqual(testList[0].ShelterName, testShelterZero.ShelterName);
            Assert.AreEqual(testList[0].Address, testShelterZero.Address);
            Assert.AreEqual(testList[0].Address2, testShelterZero.Address2);
            Assert.AreEqual(testList[0].ZipCode, testShelterZero.ZipCode);
            Assert.AreEqual(testList[0].Phone, testShelterZero.Phone);
            Assert.AreEqual(testList[0].Email, testShelterZero.Email);
            Assert.AreEqual(testList[0].AreasOfNeed, testShelterZero.AreasOfNeed);
            Assert.AreEqual(testList[0].ShelterActive, testShelterZero.ShelterActive);

            Assert.AreEqual(testList[1].ShelterId, testShelterOne.ShelterId);
            Assert.AreEqual(testList[1].ShelterName, testShelterOne.ShelterName);
            Assert.AreEqual(testList[1].Address, testShelterOne.Address);
            Assert.AreEqual(testList[1].Address2, testShelterOne.Address2);
            Assert.AreEqual(testList[1].ZipCode, testShelterOne.ZipCode);
            Assert.AreEqual(testList[1].Phone, testShelterOne.Phone);
            Assert.AreEqual(testList[1].Email, testShelterOne.Email);
            Assert.AreEqual(testList[1].AreasOfNeed, testShelterOne.AreasOfNeed);
            Assert.AreEqual(testList[1].ShelterActive, testShelterOne.ShelterActive);

            Assert.AreEqual(testList[2].ShelterId, testShelterTwo.ShelterId);
            Assert.AreEqual(testList[2].ShelterName, testShelterTwo.ShelterName);
            Assert.AreEqual(testList[2].Address, testShelterTwo.Address);
            Assert.AreEqual(testList[2].Address2, testShelterTwo.Address2);
            Assert.AreEqual(testList[2].ZipCode, testShelterTwo.ZipCode);
            Assert.AreEqual(testList[2].Phone, testShelterTwo.Phone);
            Assert.AreEqual(testList[2].Email, testShelterTwo.Email);
            Assert.AreEqual(testList[2].AreasOfNeed, testShelterTwo.AreasOfNeed);
            Assert.AreEqual(testList[2].ShelterActive, testShelterTwo.ShelterActive);
        }
        [TestMethod]
        public void TestRetrieveShelterVMByShelterID()
        {
            ShelterVM testShelter = new ShelterVM();

            testShelter = _shelterManager.RetrieveShelterVMByShelterID(1);
            Assert.AreEqual(000001, testShelter.ShelterId);
            Assert.AreEqual("Test Shelter 01", testShelter.ShelterName);
            Assert.AreEqual("Fake area 01", testShelter.Address);
            Assert.AreEqual("Fake address 01", testShelter.Address2);
            Assert.AreEqual("50001", testShelter.ZipCode);
            Assert.AreEqual("555-666-7777", testShelter.Phone);
            Assert.AreEqual("fake@fake.fake", testShelter.Email);
            Assert.AreEqual("Need fake things", testShelter.AreasOfNeed);
            Assert.AreEqual(true, testShelter.ShelterActive);

        }
        [TestMethod]
        public void TestAddShelter()
        {
            int listCountExpected = 4;
            _sheterList = _shelterManager.GetShelterList();

            Shelter testShelterThree = new Shelter
            {
                ShelterId = 000003,
                ShelterName = "Test Shelter 03",
                Address = "Fake area 03",
                Address2 = "Fake address 03",
                ZipCode = "50002",
                Phone = "444-555-6666",
                Email = "fake3@fake3.fake3",
                AreasOfNeed = "Need three fake things",
                ShelterActive = false
            };
            if (_shelterManager.AddShelter("Test Shelter 03", "Fake area 03", "Fake address 03", "50002", "444-555-6666", "fake3@fake3.fake3", "Need three fake things", false))
            {
                int listCountActual = _sheterList.Count;
                Assert.AreEqual(listCountExpected, listCountActual);

                Assert.AreEqual(_sheterList[0].ShelterName, "Test Shelter 00");
                Assert.AreEqual(_sheterList[1].ShelterName, "Test Shelter 01");
                Assert.AreEqual(_sheterList[2].ShelterName, "Test Shelter 02");
                Assert.AreEqual(_sheterList[3].ShelterName, testShelterThree.ShelterName);

                Assert.AreEqual(_sheterList[3].ShelterId, testShelterThree.ShelterId);
                Assert.AreEqual(_sheterList[3].ShelterName, testShelterThree.ShelterName);
                Assert.AreEqual(_sheterList[3].Address, testShelterThree.Address);
                Assert.AreEqual(_sheterList[3].Address2, testShelterThree.Address2);
                Assert.AreEqual(_sheterList[3].ZipCode, testShelterThree.ZipCode);
                Assert.AreEqual(_sheterList[3].Phone, testShelterThree.Phone);
                Assert.AreEqual(_sheterList[3].Email, testShelterThree.Email);
                Assert.AreEqual(_sheterList[3].AreasOfNeed, testShelterThree.AreasOfNeed);
                Assert.AreEqual(_sheterList[3].ShelterActive, testShelterThree.ShelterActive);
            }
            else
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void TestEditShelterName()
        {
            _sheterList = _shelterManager.GetShelterList();
            int idTarget = 1;
            int idIndex = -1;

            for (int i = 0; i < _sheterList.Count; i++)
            {
                if (_sheterList[i].ShelterId == idTarget)
                {
                    idIndex = i;
                    break;
                    //return;
                }
            }
            if (idIndex >= 0)
            {
                Assert.AreEqual("Test Shelter 01", _sheterList[idIndex].ShelterName);
                if (_shelterManager.EditShelterName(_sheterList[idIndex], "Test Shelter 666"))
                {
                    Assert.AreEqual("Test Shelter 666", _sheterList[idIndex].ShelterName);
                }
                else
                {
                    Assert.Fail();
                }
            }
            else
            {
                Assert.Fail();
            }


        }
        [TestMethod]
        public void TestEditAddress()
        {
            _sheterList = _shelterManager.GetShelterList();
            int idTarget = 2;
            int idIndex = -1;

            for (int i = 0; i < _sheterList.Count; i++)
            {
                if (_sheterList[i].ShelterId == idTarget)
                {
                    idIndex = i;
                    break;
                    //return;
                }
            }
            if (idIndex >= 0)
            {
                Assert.AreEqual("Fake area 02", _sheterList[idIndex].Address);
                if (_shelterManager.EditAddress(_sheterList[idIndex], "Fake area 666"))
                {
                    Assert.AreEqual("Fake area 00", _sheterList[0].Address);
                    Assert.AreEqual("Fake area 01", _sheterList[1].Address);
                    Assert.AreEqual("Fake area 666", _sheterList[idIndex].Address);
                }
                else
                {
                    Assert.Fail();
                }
            }
            else
            {
                Assert.Fail();
            }

        }
        [TestMethod]
        public void TestEditAddress2()
        {
            _sheterList = _shelterManager.GetShelterList();
            int idTarget = 1;
            int idIndex = -1;

            for (int i = 0; i < _sheterList.Count; i++)
            {
                if (_sheterList[i].ShelterId == idTarget)
                {
                    idIndex = i;
                    break;
                    //return;
                }
            }
            if (idIndex >= 0)
            {
                Assert.AreEqual("Fake address 01", _sheterList[idIndex].Address2);
                if (_shelterManager.EditAddress2(_sheterList[idIndex], "Fake address 666"))
                {
                    Assert.AreEqual("Fake address 666", _sheterList[idIndex].Address2);
                }
                else
                {
                    Assert.Fail();
                }
            }
            else
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void TestEditZipCode()
        {
            _sheterList = _shelterManager.GetShelterList();
            int idTarget = 1;
            int idIndex = -1;
            for (int i = 0; i < _sheterList.Count; i++)
            {
                if (_sheterList[i].ShelterId == idTarget)
                {
                    idIndex = i;
                    break;
                    //return;
                }
            }
            if (idIndex >= 0)
            {
                Assert.AreEqual("50001", _sheterList[idIndex].ZipCode);
                if (_shelterManager.EditZipCode(_sheterList[idIndex], "90210"))
                {
                    Assert.AreEqual("90210", _sheterList[idIndex].ZipCode);
                }
                else
                {
                    Assert.Fail();
                }
            }
            else
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void TestEditPhone()
        {
            _sheterList = _shelterManager.GetShelterList();
            int idTarget = 1;
            int idIndex = -1;
            for (int i = 0; i < _sheterList.Count; i++)
            {
                if (_sheterList[i].ShelterId == idTarget)
                {
                    idIndex = i;
                    break;
                    //return;
                }
            }
            if (idIndex >= 0)
            {
                Assert.AreEqual("555-666-7777", _sheterList[idIndex].Phone);
                if (_shelterManager.EditPhone(_sheterList[idIndex], "111-222-333"))
                {
                    Assert.AreEqual("111-222-333", _sheterList[idIndex].Phone);
                }
                else
                {
                    Assert.Fail();
                }
            }
            else
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void TestEditEmail()
        {
            _sheterList = _shelterManager.GetShelterList();
            int idTarget = 1;
            int idIndex = -1;
            for (int i = 0; i < _sheterList.Count; i++)
            {
                if (_sheterList[i].ShelterId == idTarget)
                {
                    idIndex = i;
                    break;
                    //return;
                }
            }
            if (idIndex >= 0)
            {
                Assert.AreEqual("fake@fake.fake", _sheterList[idIndex].Email);
                if (_shelterManager.EditEmail(_sheterList[idIndex], "not@real.email"))
                {
                    Assert.AreEqual("not@real.email", _sheterList[idIndex].Email);
                }
                else
                {
                    Assert.Fail();
                }
            }
            else
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void TestEditAreasOfNeed()
        {
            _sheterList = _shelterManager.GetShelterList();
            int idTarget = 1;
            int idIndex = -1;
            for (int i = 0; i < _sheterList.Count; i++)
            {
                if (_sheterList[i].ShelterId == idTarget)
                {
                    idIndex = i;
                    break;
                    //return;
                }
            }
            if (idIndex >= 0)
            {
                Assert.AreEqual("Need fake things", _sheterList[idIndex].AreasOfNeed);
                if (_shelterManager.EditAreasOfNeed(_sheterList[idIndex], "Need something, anything!"))
                {
                    Assert.AreEqual("Need something, anything!", _sheterList[idIndex].AreasOfNeed);
                }
                else
                {
                    Assert.Fail();
                }
            }
            else
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void TestEditActiveStatus()
        {
            _sheterList = _shelterManager.GetShelterList();
            int idTarget = 1;
            int idIndex = -1;
            for (int i = 0; i < _sheterList.Count; i++)
            {
                if (_sheterList[i].ShelterId == idTarget)
                {
                    idIndex = i;
                    break;
                    //return;
                }
            }
            if (idIndex >= 0)
            {
                Assert.IsTrue(_sheterList[idIndex].ShelterActive);
                if (_shelterManager.EditActiveStatus(_sheterList[idIndex], false))
                {
                    Assert.IsFalse(_sheterList[idIndex].ShelterActive);
                }
                else
                {
                    Assert.Fail();
                }
                Assert.IsFalse(_sheterList[idIndex].ShelterActive);
                if (_shelterManager.EditActiveStatus(_sheterList[idIndex], true))
                {
                    Assert.IsTrue(_sheterList[idIndex].ShelterActive);
                }
                else
                {
                    Assert.Fail();
                }
            }
            else
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void TestDeactivateShelter()
        {
            _sheterList = _shelterManager.GetShelterList();
            int idTarget = 1;
            int idIndex = -1;
            for (int i = 0; i < _sheterList.Count; i++)
            {
                if (_sheterList[i].ShelterId == idTarget)
                {
                    idIndex = i;
                    break;
                    //return;
                }
            }
            if (idIndex >= 0)
            {
                Assert.IsTrue(_sheterList[idIndex].ShelterActive);
                if (_shelterManager.DeactivateShelter(_sheterList[idIndex]))
                {
                    Assert.IsFalse(_sheterList[idIndex].ShelterActive);
                }
                else
                {
                    Assert.Fail();
                }
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestRetrieveHoursOfOperationByShelterID()
        {
            ShelterVM testShelter = new ShelterVM();
            List<HoursOfOperation> fakeHoursOfOperation = new List<HoursOfOperation>
                {
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("09:00:00"), CloseHour = TimeSpan.Parse("05:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("10:00:00"), CloseHour = TimeSpan.Parse("06:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("10:00:00"), CloseHour = TimeSpan.Parse("06:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("09:00:00"), CloseHour = TimeSpan.Parse("05:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("11:00:00"), CloseHour = TimeSpan.Parse("07:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("11:00:00"), CloseHour = TimeSpan.Parse("07:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("09:00:00"), CloseHour = TimeSpan.Parse("05:00:00")}
                };
            testShelter = _shelterManager.RetrieveShelterVMByShelterID(0);
            Assert.AreEqual(testShelter.HoursOfOperation[0].OpenHour.ToString(), fakeHoursOfOperation[0].OpenHour.ToString());
            Assert.AreEqual(testShelter.HoursOfOperation[0].CloseHour.ToString(), fakeHoursOfOperation[0].CloseHour.ToString());


        }

        [TestMethod]
        public void TestEditHoursOfOperationByShelterID()
        {
            bool updateSuccessful = false;
            ShelterVM testShelter = new ShelterVM();
            List<HoursOfOperation> fakeHoursOfOperation = new List<HoursOfOperation>
                {
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("09:00:00"), CloseHour = TimeSpan.Parse("05:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("10:00:00"), CloseHour = TimeSpan.Parse("06:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("10:00:00"), CloseHour = TimeSpan.Parse("06:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("09:00:00"), CloseHour = TimeSpan.Parse("05:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("11:00:00"), CloseHour = TimeSpan.Parse("07:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("11:00:00"), CloseHour = TimeSpan.Parse("07:00:00")},
                    new HoursOfOperation { OpenHour = TimeSpan.Parse("09:00:00"), CloseHour = TimeSpan.Parse("05:00:00")}
                };
            HoursOfOperation hoursToUpdate = new HoursOfOperation { OpenHour = TimeSpan.Parse("02:00:00"), CloseHour = TimeSpan.Parse("04:00:00") };
            testShelter = _shelterManager.RetrieveShelterVMByShelterID(0);

            updateSuccessful = _shelterManager.EditHoursOfOperationByShelterID(0, 1, hoursToUpdate);
            testShelter = _shelterManager.RetrieveShelterVMByShelterID(0);
            Assert.IsTrue(updateSuccessful);

        }
    }
}
