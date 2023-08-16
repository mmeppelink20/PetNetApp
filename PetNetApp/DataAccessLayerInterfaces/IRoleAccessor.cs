using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// By: Barry Mikulas
/// Created: 2023/02/11
/// </summary>
namespace DataAccessLayerInterfaces
{
    public interface IRoleAccessor
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
        /// <returns>Rows edited</returns>
        int DeleteRoleByUsersIdAndRoleId(int usersId, string roleId);

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/11
        /// 
        /// Retrieves a list of all Role objects
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <returns>List of Role objects</returns>
        List<Role> SelectAllRoles();

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/11
        /// 
        /// Takes a usersId and returns a List of Role objects
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="usersId">The userId being retrieved</param>
        /// <returns>List of Role objects</returns>
        List<Role> SelectAllRolesByUserId(int userID);

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/11
        /// 
        /// Takes a usersId and a Role obj and updates the UserRole table
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="usersId">The userId being retrieved</param>
        /// <param name="role">Role object</param>
        /// <returns>int count of records inserted - will be 1 unless error</returns>
        int InsertRoleByUsersId(Role role, int usersId);
    }
}
