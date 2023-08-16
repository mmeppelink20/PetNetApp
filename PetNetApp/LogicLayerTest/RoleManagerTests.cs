using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayerInterfaces;
using DataAccessLayerFakes;
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;

namespace LogicLayerTest
{
    [TestClass]
    public class RoleManagerTests
    {
        private IRoleManager _roleManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            _roleManager = new RoleManager(new RoleAccessorFakes());
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _roleManager = null;
        }

        // Created By: Asa Armstrong
        [TestMethod]
        public void TestRemoveRoleByUsersIdAndRoleIdPassRemoving1OfManyAdmin()
        {
            Assert.AreEqual(true, _roleManager.RemoveRoleByUsersIdAndRoleId(100000, "Admin"));
        }

        // Created By: Asa Armstrong
        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Cannot remove the last 'Admin' Role.")]
        public void TestRemoveRoleByUsersIdAndRoleIdRemoving1Of1AdminThrowsException()
        {
            _roleManager.RemoveRoleByUsersIdAndRoleId(100000, "Admin");
            _roleManager.RemoveRoleByUsersIdAndRoleId(100001, "Admin");
        }

        // Created By: Asa Armstrong
        [TestMethod]
        public void TestRemoveRoleByUsersIdAndRoleIdPassRemovingNonAdminRoleId()
        {
            Assert.AreEqual(true, _roleManager.RemoveRoleByUsersIdAndRoleId(100000, "Vet"));
        }
        //created by Barry Mikulas
        [TestMethod]
        public void TestReturnsCorrectRoleList()
        {
            //arrange
            const int expectedCount = 2;
            int actualCount;

            //act
            actualCount = _roleManager.RetrieveAllRoles().Count;

            //assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        //created by Barry Mikulas
        [TestMethod]
        public void TestReturnsCorrectRoleListByUsersId()
        {
            //arrange
            const int expectedCount = 1;
            int actualCount;
            int usersId = 100001;

            //act
            actualCount = _roleManager.RetrieveRoleListByUserId(usersId).Count;

            //assert
            Assert.AreEqual(expectedCount, actualCount);
        }


        //created by Barry Mikulas
        [TestMethod]
        public void TestAddRoleListToUserWorksWithCorrectData()
        {
            //arrange
            bool actualResult;
            int usersId = 100001;
            Role newRole = new Role();

            newRole.RoleId = "Veternarian";

            //act
            actualResult = _roleManager.AddRoleByUsersId(newRole, usersId);

            //assert
            Assert.IsTrue(actualResult);
        }
    }
}
