using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IUsersAccessor
    {
        /// <summary>
        /// Chris Dreismeier
        /// Created: 2023/03/02
        /// 
        /// 
        /// </summary>
        /// Selects all users with a certain role
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="RoleId"></param>
        List<UsersVM> SelectUserByRole(string roleId, int shelterId);

        /// <summary>
        ///  /// Hoang Chu
        /// Created: 2023/02/17
        /// 
        /// Select all employess
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <returns></returns>
        List<UsersVM> SelectAllEmployees();

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/02/14
        /// 
        /// Updates a user's active status with their user ID and active status as a boolean.
        /// </summary>
        ///
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param userId="UsersId"></param>
        /// /// <param active="Active"></param>
        int UpdateUserActive(int userId, bool active);

        /// <summary>
        /// [Mads Rhea - 2023/02/15]
        /// Confirms if given Email and PasswordHash match a User within the Users table.
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <returns>int</returns>
        int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash);

        /// <summary>
        /// [Mads Rhea - 2023/02/15]
        /// Returns user from the Users table based off of matching email.
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <returns>UsersVM</returns>
        UsersVM SelectUserByEmail(string email);

        /// <summary>
        /// [Mads Rhea - 2023/02/15]
        /// Returns all roles connected to the UsersId in the UserRoles table.
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <returns>List of strings</returns>
        List<string> SelectRolesByUserID(int userId);

        /// <summary>
        /// [Mads Rhea - 2023/03/29]
        /// Returns all RoleIDs from the Role table.
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <returns>List of strings</returns>
        List<string> SelectAllRoles();

        /// <summary>
        /// [Mads Rhea - 2023/02/15]
        /// Confirms if given Email and PasswordHash match a User within the Users table.
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <returns>UsersVM</returns>
        UsersVM AuthenticateUser(string email, string passwordHash);

        /// <summary>
        /// [Mads Rhea - 2023/02/15]
        /// Adds or removes user role within Users table.
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <returns>int/returns>
        int InsertOrDeleteUserRole(int usersId, string role, bool delete = false);

        /// <summary>
        /// [Mads Rhea - 2023/02/15]
        /// Returns all PronounId values from Pronoun table.
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <returns>List of strings</returns>
        List<string> SelectAllPronouns();

        /// <summary>
        /// [Mads Rhea - 2023/02/15]
        /// Returns all GenderId values from Gender table.
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <returns>List of strings</returns>
        List<string> SelectAllGenders();

        /// <summary>
        /// [Mads Rhea - 2023/02/15]
        /// Injects updated user info into the Users table where the UsersId, GivenName, FamilyName, GenderId, PronounId, Address, Address2, Phone, and Zipcode match.
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <returns>int</returns>
        int UpdateUserDetails(Users oldUser, Users updatedUser);

        /// <summary>
        /// [Mads Rhea - 2023/02/15]
        /// Injects updated PasswordHash into Users table where the Email and old PasswordHash match.
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <returns>int</returns>
        int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash);

        /// <summary>
        /// [Mads Rhea - 2023/02/24]
        /// Updates User email in the Users table.
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <returns>int</returns>
        int UpdateUserEmail(string oldEmail, string newEmail, string passwordHash);

        /// <summary>
        /// [Alex Oetken - 2023/02/??]
        /// Injects new user into the Users table.
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <returns>int</returns>
        int CreateNewUser(Users user, string PasswordHash);

        /// <summary>
        /// [Alex Oetken - 2023/02/??]
        /// Updates User active status to false based on UserId.
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <returns>int</returns>
        int DeactivateUserAccount(int UserId);

        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/02/12
        /// 
        /// Takes a list of usersVM by the userID
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/13
        /// 
        /// FinalQA
        /// </remarks>
        List<UsersVM> SelectUsersByUsersId(int usersId);

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/12
        /// 
        /// Takes a usersId and returns a users object
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="usersId">The userId being retrieved</param>
        /// <returns>Users object</returns>
        Users SelectUserByUsersId(int usersId);

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/12
        /// 
        /// Takes a usersId and returns a users object
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="usersId">The userId being retrieved</param>
        /// <returns>UsersVM object</returns>
        UsersVM SelectUserByUsersIdWithRoles(int usersId);

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/26
        /// 
        /// Takes a usersId and changes the suspend status to the value of suspend parameter
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="usersId">The userId being updated</param>
        /// <param name="suspend">True if suspending, false if unsuspending</param>
        /// <returns>int count of updated users - should be 1</returns>
        int UpdateUserSuspend(int usersId, bool suspend);

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/26
        /// 
        /// Takes a roleId and counts the number active, unsuspended accounts with that roleId
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="roleId"></param>
        /// <returns>int count of accounts</returns>
        int SelectCountActiveUnsuspendedUsersByRole(string roleId);

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/03/03
        /// 
        /// Takes a user's user id and returns previous adoption records associated with that user.
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="roleId"></param>
        /// <returns>int count of accounts</returns>
        List<UsersAdoptionRecords> SelectAdoptionRecordsByUserID(int usersId);

        /// <summary>
        /// Chris Dreismeier
        /// Created: 2023/04/13
        /// 
        /// takes in a userid and shelterid and assigns that shelterid to the user
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="shelterid"></param>
        /// <param name="userid"></param>
        /// <returns>int count rows affected</returns>
        int UpdateUserShelterid(int userid, int shelterid, int? oldShelterId);
    }
}
