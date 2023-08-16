using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataAccessLayer;
using DataObjects;
using LogicLayer;
using DataAccessLayerInterfaces;
using DataAccessLayerFakes;
using System.Collections.Generic;
using LogicLayerInterfaces;


namespace LogicLayerTest
{
    /// <summary>
    /// Mads Rhea
    /// Created: 2023/01/27
    /// 
    /// Logic Layer testing class for Users.
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd
    /// </remarks>

    [TestClass]
    public class UsersManagerTest
    {
        private IUsersManager _userManager = null;


        [TestInitialize]
        public void TestSetup()
        {
            _userManager = new UsersManager(new UsersAccessorFakes());
        }

        [TestCleanup]
        public void TestTearDown()
        {
            _userManager = null;
        }

        // Chris
        [TestMethod]
        public void TestRetrieveUserByRole()
        {
            // arrange
            const int expectedCount = 3;
            string role = "Volunteer";
            int shelterId = 1;
            int actualCount = 0;

            // act
            actualCount = _userManager.RetrieveUserByRole(role, shelterId).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        // Hoang
        [TestMethod]
        public void TestSelectAllEmployee()
        {
            List<UsersVM> listUser = null;
            listUser = _userManager.RetriveAllEmployees();

            int actualResult = listUser.Count;
            int expectedResult = 6;

            Assert.AreEqual(expectedResult, actualResult);
        }

        // Alex
        [TestMethod]
        public void TestDeactivateAccount()
        {
            //arrage 
            int userId = 1000;
            bool actualResult = false;

            //act 
            actualResult = _userManager.DeactivateUserAccount(userId);

            //assert
            Assert.IsTrue(actualResult);
        }

        // Alex
        [TestMethod]
        public void TestCreateUser()
        {
            //arrage 
            bool actualResult = false;

            //act 
            actualResult = _userManager.AddUser(new Users(), "fakePassword");

            //assert
            Assert.IsTrue(actualResult);

        }

        /// <summary>
        /// Zaid Rachman
        /// 2023/02/15
        /// 
        /// Tests SelectUsersByUsersId by inputing a sample usersId
        /// </summary>
        [TestMethod]
        /*This test method is used to test if the user exists*/
        public void TestSelectUsersByUsersId()
        {
            int sampleId = 1000;
            int expectedResult = 1;

            int actualResult = _userManager.RetrieveUsersByUsersId(sampleId).Count;
            Assert.AreEqual(expectedResult, actualResult);

        }

        /// <summary>
        /// [Barry Mikulas - 2023/02/17]
        /// Tests to see if a user can be found within the Users table based off UsersId
        /// </summary>
        [TestMethod]
        public void TestSelectUserByUsersId()
        {
            //arrange
            Users expectedUser;
            expectedUser = new Users()
            {
                UsersId = 1000,
                GivenName = "Stephan",
                FamilyName = "technowiz",
                Email = "Stephan@company.com",
                Address = "4150 riverview road",
                Zipcode = "52411",
                Phone = "319-123-1325",
                Active = true

            };

            int usersId = 1000;

            //act
            var actualUser = _userManager.RetrieveUserByUsersId(usersId);

            //assert
            Assert.AreEqual(actualUser.GivenName, expectedUser.GivenName);
            Assert.AreEqual(actualUser.FamilyName, expectedUser.FamilyName);
            Assert.AreEqual(actualUser.Email, expectedUser.Email);
            Assert.AreEqual(actualUser.Address, expectedUser.Address);
            Assert.AreEqual(actualUser.Zipcode, expectedUser.Zipcode);
            Assert.AreEqual(actualUser.Phone, expectedUser.Phone);
            Assert.AreEqual(actualUser.Active, expectedUser.Active);
        }

        /// <summary>
        /// [Barry Mikulas - 2023/02/26]
        /// Tests to see if a user can be marked suspended
        /// </summary>
        [TestMethod]
        public void TestSuspendAccountByUsersId()
        {
            //arrange 
            int userId = 1000;
            bool actualResult = false;

            //act 
            actualResult = _userManager.SuspendUserAccount(userId);

            //assert
            Assert.IsTrue(actualResult);
        }
        /// <summary>
        /// [Barry Mikulas - 2023/02/26]
        /// Tests to see if a user can be marked unsuspended
        /// </summary>
        [TestMethod]
        public void TestUnsuspendAccountByUsersId()
        {
            //arrange 
            int userId = 1000;
            bool actualResult = false;

            //act 
            actualResult = _userManager.UnsuspendUserAccount(userId);

            //assert
            Assert.IsTrue(actualResult);
        }
        [TestMethod]
        public void TestReturnsCorrectCountOfActiveUnsuspendedUsersId()
        {
            //arrange
            string roleId = "Admin";
            int actualResult = 0;
            int expectedResult = 2; //there are 2 unsuspended admins in fake data 1 suspended

            //act
            actualResult = _userManager.RetrieveCountActiveUnsuspendUserAccountsByRoleId(roleId);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// [Mads Rhea - 2023/02/15]
        /// Tests to see if a user can be found within the Users table based off username and password
        /// </summary>
        [TestMethod]
        public void TestAuthenticateUserPassesWithCorrectUsernameAndPassword()
        {
            const string email = "mads@company.com";
            const string password = "newuser";
            int expectedResult = 999995;
            int actualResult = 0;

            UsersVM testUser = _userManager.LoginUser(email, password);
            actualResult = testUser.UsersId;

            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// [Mads Rhea - 2023/02/15]
        /// Tests to see an error will be thrown if the incorrect email is given
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAutheticateUserFailsWithIncorrectEmail()
        {
            const string email = "mmadsrhea@yahoo.com";
            const string password = "newuser";

            UsersVM testUser = _userManager.LoginUser(email, password);

        }

        /// <summary>
        /// [Mads Rhea - 2023/02/15]
        /// Tests to see if SHA256 returns the correct value
        /// </summary>
        [TestMethod]
        public void TestGetSHA256ReturnsCorrectHashValue()
        {
            const string password = "newuser";
            const string expectedResult = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";
            string result = "";

            result = _userManager.HashSha256(password);

            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// [Mads Rhea - 2023/02/15]
        /// Tests to see an error will be thrown if the SHA256 is given null input
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGetSHA256ThrowsArgumentNullExceptionForMissingInput()
        {
            const string password = null;

            _userManager.HashSha256(password);
        }

        /// <summary>
        /// [Mads Rhea - 2023/02/15]
        /// Tests to see an error will be thrown if the SHA256 is given an empty string
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGetSHA256ThrowsArgumentNullExceptionForEmptyString()
        {
            const string password = "";

            _userManager.HashSha256(password);
        }

        /// <summary>
        /// [Mads Rhea - 2023/02/24]
        /// Tests to see if the method successfully returns all rows from gender.
        /// </summary>
        [TestMethod]
        public void TestSelectAllGenders()
        {
            List<string> genders = null;
            genders = _userManager.RetrieveGenders();

            int actualResult = genders.Count;
            int expectedResult = 4;

            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// [Mads Rhea - 2023/02/24]
        /// Tests to see if the method successfully returns all rows from pronouns.
        /// </summary>
        [TestMethod]
        public void TestSelectAllPronouns()
        {
            List<string> pronouns = null;
            pronouns = _userManager.RetrievePronouns();

            int actualResult = pronouns.Count;
            int expectedResult = 4;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestEditUserActive()
        {
            // Arrange
            int rowsAffected;

            // Act
            rowsAffected = _userManager.EditUserActive(1001, false);

            int expectedRowResult = 1;

            // Assert
            Assert.AreEqual(rowsAffected, expectedRowResult);
        }

        [TestMethod]
        public void TestSelectAdoptionRecordsByUserId()
        {
            // Arrange
            List<UsersAdoptionRecords> adoptionRecords = null;
            int expectedRowResult = 3;

            // Act
            adoptionRecords = _userManager.RetrieveAdoptionRecordsByUserID(1001);
            int actualRowResult = adoptionRecords.Count;

            // Assert
            Assert.AreEqual(expectedRowResult, actualRowResult);
        }

        [TestMethod]
        public void TestEditUserShelterId()
        {
            // Arrange
            bool wasAffected = false;
            int userId = 1001;
            int shelterId = 3;
            int oldShelterId = 1;


            // Act
            wasAffected = _userManager.EditUserShelterId(userId, shelterId, oldShelterId);

            // Assert
            Assert.IsTrue(wasAffected);
        }

        [TestMethod]
        public void TestUpdateUserDetailsSuccessfully()
        {
            bool results = false;

            Users oldUser = new Users()
            {
                UsersId = 1000,
                GivenName = "Stephan",
                FamilyName = "technowiz",
                Email = "Stephan@company.com",
                Address = "4150 riverview road",
                Zipcode = "52411",
                Phone = "319-123-1325",
                Active = true,
                Suspend = false
            };
            Users newUser = new Users()
            {
                UsersId = 1000,
                ShelterId = 1,
                GivenName = "Stephanie",
                FamilyName = "technowitch",
                Email = "Stephanie@company.com",
                Address = "4150 riverview road",
                Zipcode = "52411",
                Phone = "319-123-1325",
                Active = true,
                Suspend = false
            };

            results = _userManager.EditUserDetails(oldUser, newUser);

            Assert.IsTrue(results);
        }

        [TestMethod]
        public void TestUpdateUserEmailSuccessfully()
        {
            const string oldEmail = "mads@company.com";
            const string newEmail = "avery@company.com";
            const string passwordHash = "newuser";

            bool results = false;

            results = _userManager.UpdateEmail(oldEmail, newEmail, passwordHash);

            Assert.IsFalse(results);
        }

        [TestMethod]
        public void TestUpdateUserEmailWithInvalidEmail()
        {
            const string oldEmail = "mads@company.com";
            const string newEmail = "avery company com";
            const string passwordHash = "newuser";

            bool results = true;

            results = _userManager.UpdateEmail(oldEmail, newEmail, passwordHash);

            Assert.IsFalse(results);
        }

        [TestMethod]
        public void TestUpdateUserEmailWithWrongPassword()
        {
            const string oldEmail = "mads@company.com";
            const string newEmail = "avery@company.com";
            const string passwordHash = "P@ssw0rd";

            bool results = true;

            results = _userManager.UpdateEmail(oldEmail, newEmail, passwordHash);

            Assert.IsFalse(results);
        }

        [TestMethod]
        public void TestSelectAllRoles()
        {
            List<string> roles = null;
            roles = _userManager.RetrieveAllRoles();

            int actualResult = roles.Count;
            int expectedResult = 3;

            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}
