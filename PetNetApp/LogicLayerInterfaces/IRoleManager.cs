using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IRoleManager
    {
        /// <summary>
        /// Asa Armstrong
        /// Created: 2023/02/24
        /// 
        /// Deletes the role assigned to a user by the user's Id and role's Id.
        /// </summary>
        ///
        /// <remarks>
        /// Asa Armstrong
        /// Updated: 2023/02/24
        /// Added Comment.
        /// </remarks>
        /// <param name="usersId">usersId</param>
        /// <param name="roleId">roleId</param>
        /// <exception cref="SQLException">Delete fails.</exception>
        /// <returns>True if the record was removed</returns>
        bool RemoveRoleByUsersIdAndRoleId(int usersId, string roleId);

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/11
        /// 
        /// Retrieves a List of Role objects
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <returns>List of Role Objects</returns>
        List<Role> RetrieveAllRoles();

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/11
        /// 
        /// Retrieves a List of Role objects for a usersId
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="usersId"></param>
        /// <returns>List of Role Objects</returns>
        List<Role> RetrieveRoleListByUserId(int usersId);

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/11
        /// 
        /// Adds a role for a users.
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="role">Role object</param>
        /// <param name="usersId">int for usersId</param>
        /// <returns>bool of success of adding the role for a user</returns>
        bool AddRoleByUsersId(Role role, int usersId);
    }
}
