using DataAccessLayer;
using DataAccessLayerInterfaces;
using DataObjects;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// By: Barry Mikulas
    /// Created: 2023/02/11
    /// </summary>
    public class RoleManager : IRoleManager
    {
        private IRoleAccessor _roleAccessor = null;

        public RoleManager()
        {
            _roleAccessor = new RoleAccessor();
        }

        public RoleManager(IRoleAccessor roleAccessor)
        {
            _roleAccessor = roleAccessor;
        }

        // Created By: Asa Armstrong
        public bool RemoveRoleByUsersIdAndRoleId(int usersId, string roleId)
        {
            bool wasRemoved = false;

            try
            {
                int rowsAffected = _roleAccessor.DeleteRoleByUsersIdAndRoleId(usersId, roleId);
                if (rowsAffected > 0)
                {
                    wasRemoved = true;
                }
                else if (rowsAffected == -1)
                {
                    throw new ApplicationException("Cannot remove the last 'Admin' Role.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return wasRemoved;
        }
        /// <summary>
        /// By: Barry Mikulas
        /// Created: 2023/02/11
        /// </summary>
        public bool AddRoleByUsersId(Role role, int usersId)
        {
            //throw new NotImplementedException();
            bool wasAdded = false;
            try
            {
                wasAdded = 0 < _roleAccessor.InsertRoleByUsersId(role, usersId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to add role to user.", ex);
            }

            return wasAdded;
        }
        /// <summary>
        /// By: Barry Mikulas
        /// Created: 2023/02/11
        /// </summary>
        public List<Role> RetrieveAllRoles()
        {
            List<Role> roles = new List<Role>();
            try
            {
                roles = _roleAccessor.SelectAllRoles();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found", ex);
            }
            return roles;
            //throw new NotImplementedException();
        }
        /// <summary>
        /// By: Barry Mikulas
        /// Created: 2023/02/11
        /// </summary>
        public List<Role> RetrieveRoleListByUserId(int userId)
        {
            List<Role> roles = new List<Role>();
            try
            {
                roles = _roleAccessor.SelectAllRolesByUserId(userId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found", ex);
            }
            return roles;
            //throw new NotImplementedException();
        }
    }
}
