using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IUsersManager
    {
        List<UsersVM> RetrieveUserByRole(string roleId, int shelterId);
        List<UsersVM> RetriveAllEmployees();

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/23
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/14
        /// 
        /// FinalQA
        /// </remarks>
        /// <param name="UsersId"></param>
        /// <returns></returns>
        Users RetrieveUserByUsersId(int UsersId);

        /// <summary>
        /// created 02/26/2023
        /// created by Barry Mikulas
        /// Sets user account suspend status to true
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="UserId"></param>
        /// <returns>bool of success status</returns>
        bool SuspendUserAccount(int UserId);

        /// <summary>
        /// created 02/26/2023
        /// created by Barry Mikulas
        /// Sets user account suspend status to false
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="UserId"></param>
        /// <returns>bool of success status</returns>
        bool UnsuspendUserAccount(int UserId);

        /// <summary>
        /// created 02/26/2023
        /// created by Barry Mikulas
        /// Returns count of active\unsuspended users for a given role type
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="UserId"></param>
        /// <returns>int</returns>
        int RetrieveCountActiveUnsuspendUserAccountsByRoleId(string RoleId);

        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UsersVM LoginUser(string email, string password);
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="source"></param>
        /// <returns></returns>
        string HashSha256(string source);
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <returns></returns>
        List<string> RetrieveGenders();
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <returns></returns>
        List<string> RetrievePronouns();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldUser"></param>
        /// <param name="updatedUser"></param>
        /// <returns></returns>
        bool EditUserDetails(Users oldUser, Users updatedUser);
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="email"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ResetPassword(string email, string oldPassword, string newPassword);
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="oldEmail"></param>
        /// <param name="newEmail"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        bool UpdateEmail(string oldEmail, string newEmail, string passwordHash);
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <returns></returns>
        List<string> RetrieveAllRoles();
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="usersId"></param>
        /// <returns></returns>
        List<string> RetrieveRolesByUsersId(int usersId);
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="email"></param>
        /// <returns></returns>
        bool RetrieveUserByEmail(string email);
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="email"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        UsersVM AuthenticateUser(string email, string passwordHash);
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="usersId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        bool AddUserRole(int usersId, string role);
        /// <summary>
        /// Mads Rhea
        /// Created: 2023/02/05
        /// 
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/28
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="usersId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        bool DeleteUserRole(int usersId, string role);

        // Alex Oetken
        bool DeactivateUserAccount(int UserId);
        bool AddUser(Users user, string password);

        /// <summary>
        /// 
        /// Zaid Rachman
        /// Created: 2023/02/15
        /// Retrieves list of users by userId
        /// Used to see if user exists.
        /// 
        /// 
        /// </summary>
        ///  <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// 
        /// <param name="usersId"></param>
        /// <returns></returns>
        List<UsersVM> RetrieveUsersByUsersId(int usersId);

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/02/14
        /// 
        /// 
        /// </summary>
        /// Retrieves a users with given usersId
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// 
        /// </remarks>
        /// <param userId="UsersId"></param>
        /// <param active="Active"></param
        int EditUserActive(int userId, bool active);

        
      
        /// <summary>
        /// Chris Dreismeier
        /// Created: 2023/04/13
        /// 
        /// Updates users shelterid
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// 
        /// </remarks>
        /// <param name="usersId"></param>
        /// <param name="shelterId"></param>
        /// <param name="oldShelterId"></param>
        bool EditUserShelterId(int userId, int shelterId, int? oldShelterId);

        /// Teft Francisco
        /// Created: 2023/02/14
        /// 
        /// 
        /// </summary>
        /// Retrieves a user's adoption records by their user ID.
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// 
        /// </remarks>
        /// <param userId="usersId"></param>
        List<UsersAdoptionRecords> RetrieveAdoptionRecordsByUserID(int usersId);
        UsersVM RetrieveUserByUserEmail(string email);

        /// <summary>
        /// Asa Armstrong
        /// Created: 2023/04/13
        /// 
        /// Retrieves a user object by an email
        /// </summary>
        /// <param name="email">email to search</param>
        Users RetrieveUserObjectByEmail(string email);
    }
}
