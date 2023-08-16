using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessLayerInterfaces;
using DataAccessLayer;
using DataAccessLayerFakes;
using System.Security.Cryptography;

namespace LogicLayer
{
    /// <summary>
    /// Mads Rhea
    /// Created: 2023/01/27
    /// 
    /// Logic layer manager for Users.
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd
    /// </remarks>

    public class UsersManager : IUsersManager
    {
        IUsersAccessor _userAccessor = null;

        public UsersManager()
        {
            _userAccessor = new UsersAccessor();
        }

        public UsersManager(IUsersAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }


        /// <summary>
        /// Chris Dreismeier
        /// Created: 2023/03/02
        /// 
        /// 
        /// </summary>
        /// Retrieves all users with a certain role
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="RoleId"></param>
        public List<UsersVM> RetrieveUserByRole(string roleId, int shelterId)
        {
            List<UsersVM> users = new List<UsersVM>();

            try
            {
                users = _userAccessor.SelectUserByRole(roleId,shelterId);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Could not retrieve volunteers", ex);
            }

            return users;
        }

        /// Hoang Chu
        /// Created: 2023/02/01
        ///         
        /// <returns>List<Users></returns>
        public List<UsersVM> RetriveAllEmployees()
        {
            List<UsersVM> employeeList = null;

            try
            {
                employeeList = _userAccessor.SelectAllEmployees();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found", ex);
            }

            return employeeList;
        }

        /// <summary>
        /// [Mads Rhea - 2023/02/10]
        /// Converts string into a HashSha256
        /// </summary>
        /// <returns>string</returns>
        public string HashSha256(string source)
        {
            string result = "";

            if (source == null || source == "")
            {
                throw new ArgumentNullException("Missing input");
            }

            byte[] data;

            using (SHA256 sha256hasher = SHA256.Create())
            {
                data = sha256hasher.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            var s = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }

            result = s.ToString();

            return result;
        }

        /// <summary>
        /// [Mads Rhea - 2023/02/10]
        /// Passes Email and Password and returns a UsersVM if values match a record found within the Users table.
        /// </summary>
        /// <returns>UsersVM</returns>
        public UsersVM LoginUser(string email, string password)
        {
            UsersVM user = null;

            try
            {
                password = HashSha256(password);
                if (1 == _userAccessor.AuthenticateUserWithEmailAndPasswordHash(email, password))
                {
                    user = _userAccessor.SelectUserByEmail(email);
                    try
                    {
                        user.Roles = _userAccessor.SelectRolesByUserID(user.UsersId);
                    }
                    catch (Exception up)
                    {
                        throw new ApplicationException("Unable to load roles for user.", up);
                    }
                    if (user.Roles.Count == 0)
                    {
                        throw new ApplicationException("You don't have permissions to use this application");
                    }
                }
                else
                {
                    throw new ApplicationException("Bad username or password.");
                }

            }
            catch (Exception up)
            {
                throw new ApplicationException("Something went wrong logging you in.", up);
            }

            return user;
        }

        /// <summary>
        /// [Mads Rhea - 2023/02/10]
        /// Returns all entries from Gender table.
        /// </summary>
        /// <returns>List of strings</returns>
        public List<string> RetrieveGenders()
        {
            List<string> genders = new List<string>();

            try
            {
                genders = _userAccessor.SelectAllGenders();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return genders;
        }

        /// <summary>
        /// [Mads Rhea - 2023/02/10]
        /// Returns all entries from Pronoun table.
        /// </summary>
        /// <returns>List of strings</returns>
        public List<string> RetrievePronouns()
        {
            List<string> pronouns = new List<string>();

            try
            {
                pronouns = _userAccessor.SelectAllPronouns();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return pronouns;
        }

        /// <summary>
        /// [Mads Rhea - 2023/02/10]
        /// Injects updated User details into the Users table.
        /// </summary>
        /// <returns>bool</returns>
        public bool EditUserDetails(Users oldUser, Users updatedUser)
        {
            bool result = false;

            try
            {
                result = 1 == _userAccessor.UpdateUserDetails(oldUser, updatedUser);
            }
            catch (Exception up)
            {

                throw up;
            }

            return result;
        }

        /// <summary>
        /// [Mads Rhea - 2023/02/10]
        /// Injects updated User Password into the Users table.
        /// </summary>
        /// <returns>bool</returns>
        public bool ResetPassword(string email, string oldPassword, string newPassword)
        {
            bool result = false;

            try
            {
                result = 1 == _userAccessor.UpdatePasswordHash(email, HashSha256(oldPassword), HashSha256(newPassword));

            }
            catch (Exception up)
            {

                throw new ApplicationException("Incorrect password", up);
            }

            return result;
        }

        /// <summary>
        /// [Alex Oetken - 2023/02/??]
        /// Updates User in the Users table to inactive.
        /// </summary>
        /// <returns>int</returns>
        public bool DeactivateUserAccount(int UserId)
        {
            bool result = false;

            try
            {
                result = (1 == _userAccessor.DeactivateUserAccount(UserId));
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }

        /// <summary>
        /// [Alex Oetken - 2023/02/??]
        /// Injects new User into the Users table.
        /// </summary>
        /// <returns>int</returns>
        public bool AddUser(Users user, string Password)
        {
            bool result = false;

            Password = HashSha256(Password);

            try
            {
                result = (1 == _userAccessor.CreateNewUser(user, Password));
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }

        public List<UsersVM> RetrieveUsersByUsersId(int usersId)
        {
            List<UsersVM> usersList = new List<UsersVM>();

            try
            {
                usersList = _userAccessor.SelectUsersByUsersId(usersId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("User not found", ex);
            }
            return usersList;
        }
        public Users RetrieveUserByUsersId(int UsersId)
        {
            //throw new NotImplementedException();

            Users _user = new Users();
            try
            {
                _user = _userAccessor.SelectUserByUsersId(UsersId);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Data not found", ex);
            }
            return _user;

        }

        // Teft Francisco
        public int EditUserActive(int userId, bool active)
        {
            int result = 0;
            try
            {
                if (1 == _userAccessor.UpdateUserActive(userId, active))
                {
                    result = 1;
                }
                return result;
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error has occured", e);
            }
        }

        /// <summary>
        /// [Mads Rhea - 2023/02/24]
        /// Updates User email in the Users table.
        /// </summary>
        /// <returns>bool</returns>
        public bool UpdateEmail(string oldEmail, string newEmail, string passwordHash)
        {
            bool result = false;

            passwordHash = HashSha256(passwordHash);
            if (1 == _userAccessor.AuthenticateUserWithEmailAndPasswordHash(oldEmail, passwordHash))
            {
                try
                {
                    result = 1 == _userAccessor.UpdateUserEmail(oldEmail, newEmail, passwordHash);
                }
                catch (Exception up)
                {
                    throw new ApplicationException("Unable to update user email.", up);
                }
            }

            return result;
        }

        /// <summary>
        /// [Barry Mikulas - 2023/02/26]
        /// Sets user suspend status to true
        /// </summary>
        /// <returns>bool</returns>
        public bool SuspendUserAccount(int UserId)
        {
            //throw new NotImplementedException();
            bool result = false;

            try
            {
                result = 1 == _userAccessor.UpdateUserSuspend(UserId, true);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Suspend user failed.", ex);
            }

            return result;
        }

        /// <summary>
        /// [Barry Mikulas - 2023/02/26]
        /// Sets user suspend status to false
        /// </summary>
        /// <returns>bool</returns>
        public bool UnsuspendUserAccount(int UserId)
        {
            bool result = false;

            try
            {
                result = 1 == _userAccessor.UpdateUserSuspend(UserId, false);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Unsuspend user failed.", ex);
            }

            return result;
            //throw new NotImplementedException();
        }

        public int RetrieveCountActiveUnsuspendUserAccountsByRoleId(string RoleId)
        {
            int usersIdCount = 0;
            try
            {
                usersIdCount = _userAccessor.SelectCountActiveUnsuspendedUsersByRole(RoleId);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Unable to retrieve count of unsuspended accounts.", ex);
            }

            return usersIdCount;
            // return 2; //green test
            //throw new NotImplementedException(); //red test
        }

        public List<UsersAdoptionRecords> RetrieveAdoptionRecordsByUserID(int usersId)
        {
            List<UsersAdoptionRecords> userAdoptionRecords = new List<UsersAdoptionRecords>();
            try
            {
                userAdoptionRecords = _userAccessor.SelectAdoptionRecordsByUserID(usersId);
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error has occured", e);
            }
            return userAdoptionRecords;
        }

        public List<string> RetrieveRolesByUsersId(int usersId)
        {
            List<string> userRoles = new List<string>();

            try
            {
                userRoles = _userAccessor.SelectRolesByUserID(usersId);
            }
            catch (Exception e)
            {

                throw new ApplicationException("An error has occured", e);
            }

            return userRoles;
        }

        public List<string> RetrieveAllRoles()
        {
            List<string> roles = new List<string>();

            try
            {
                roles = _userAccessor.SelectAllRoles();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return roles;
        }

        public bool RetrieveUserByEmail(string email)
        {
            try
            {
                return _userAccessor.SelectUserByEmail(email) != null;
            }
            catch (ApplicationException ae) 
            {
                if (ae.Message == "User not found.")
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            catch (Exception up)
            {
                throw new ApplicationException("Database Error.", up);
            }
        }

        public UsersVM AuthenticateUser(string email, string passwordHash)
        {
            UsersVM result = null;

            var password = HashSha256(passwordHash);
            passwordHash = null;

            try
            {
               result = _userAccessor.AuthenticateUser(email, password);
            }
            catch (Exception up)
            {
                throw new ApplicationException("Login failed!", up);
            }

            return result;
        }

        public UsersVM RetrieveUserByUserEmail(string email)
        {
            UsersVM userVM = new UsersVM();

            try
            {
                userVM = _userAccessor.SelectUserByEmail(email);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed retrieving a user.", ex);
            }

            return userVM;
        }

        public bool EditUserShelterId(int userId, int shelterId, int? oldShelterId)
        {
            bool wasAdded = false;

            try
            {
                wasAdded = 1 == _userAccessor.UpdateUserShelterid(userId, shelterId, oldShelterId);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database Error.", ex);
            }

            return wasAdded;
        }

        public bool AddUserRole(int usersId, string role)
        {
            bool result = false;
            try
            {
                result = (1 == _userAccessor.InsertOrDeleteUserRole(usersId, role));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Role not added!", ex);
            }
            return result;
        }

        public bool DeleteUserRole(int usersId, string role)
        {
            bool result = false;
            try
            {
                result = (1 == _userAccessor.InsertOrDeleteUserRole(usersId, role, delete: true));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Role not removed!", ex);
            }
            return result;
        }

        public Users RetrieveUserObjectByEmail(string email)
        {
            try
            {
                return _userAccessor.SelectUserByEmail(email);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve User Object by Email. ", ex);
            }
        }
    }
}
