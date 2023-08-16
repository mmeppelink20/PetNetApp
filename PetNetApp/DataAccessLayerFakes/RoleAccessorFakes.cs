using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    public class RoleAccessorFakes : IRoleAccessor
    {
        List<UserRoles> _userRoles = new List<UserRoles>();

        /// <summary>
        /// By: Barry Mikulas
        /// Created: 2023/02/11
        /// </summary>
        private List<Role> _fakeRoles = new List<Role>();

        /// <summary>
        /// This is the list of roles to popluate combo box
        /// </summary>
        public RoleAccessorFakes()
        {
            _fakeRoles.Add(new Role()
            {
                RoleId = "Admin",
                Description = "Underpaid serf."
            });
            _fakeRoles.Add(new Role()
            {
                RoleId = "Volunteer",
                Description = "Work for free."
            });
            _userRoles.Add(new UserRoles() { UsersId = 100000, RoleId = "Admin" });
            _userRoles.Add(new UserRoles() { UsersId = 100001, RoleId = "Admin" });
            _userRoles.Add(new UserRoles() { UsersId = 100000, RoleId = "Vet" });
        }

        // Created By: Asa Armstrong
        public int DeleteRoleByUsersIdAndRoleId(int usersId, string roleId)
        {
            int rowsAffected = 0;

            try
            {
                if ( !roleId.Equals("Admin") || ("Admin".Equals(roleId) && _userRoles.FindAll(ur => ur.RoleId == "Admin").Count > 1))
                {
                    if (_userRoles.Remove(_userRoles.FirstOrDefault(ur => ur.RoleId == roleId && ur.UsersId == usersId)))
                    {
                        rowsAffected++;
                    }
                }
                else
                {
                    rowsAffected = -1; // there is only one admin and won't delete
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rowsAffected;
        }
        

        public int InsertRoleByUsersId(Role role, int usersId)
        {
            //throw new NotImplementedException();

            int result = _fakeRoles.Count;

            try
            {
                _fakeRoles.Add(new Role()
                {
                    RoleId = role.RoleId,
                    Description = usersId.ToString()
                });
                result = _fakeRoles.Count - result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }

        public List<Role> SelectAllRoles()
        {
            // red test
            //throw new NotImplementedException();
            // green test
            return _fakeRoles;
        }
        //updated 2023/04/27 by Zaid Rachman 
        public List<Role> SelectAllRolesByUserId(int userID)
        {
            List<Role> fakeUsersRoles = new List<Role>();
            
            foreach(UserRoles userRoles in _userRoles)
            {
                if(userRoles.UsersId == userID)
                {
                    foreach(Role role in _fakeRoles)
                    {
                        if (role.RoleId.Equals(userRoles.RoleId))
                        {
                            fakeUsersRoles.Add(role);
                        }
                        
                    }
                }
            }
            return fakeUsersRoles;
        }

    }
}
