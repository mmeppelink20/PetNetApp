 using DataAccessLayerInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Barry Mikulas
    /// Created: 2023/02/03
    /// 
    /// </summary>
    /// 
    /// Selects all roles and descriptions
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// 
    /// </remarks>
    
    public class RoleAccessor : IRoleAccessor
    {

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/03
        /// 
        /// Inserts new roles for a UsersId
        /// </summary>
        /// 
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// 
        /// 
        /// </remarks>
        /// <param name="role">Role obj to be added</param>
        /// <param name="usersId">UsersId of User getting new role</param>
        /// <returns>int for sucess of insert </returns>
        public int InsertRoleByUsersId(Role role, int usersId)
        {
            //throw new NotImplementedException();
            int rowsAffected = 0;

            var conn = new DBConnection().GetConnection();
            var cmdText = "sp_insert_role_by_usersId";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UsersId", usersId);
            cmd.Parameters.AddWithValue("@RoleId", role.RoleId);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/03
        /// 
        /// 
        /// </summary>
        /// Selects all roles and descriptions
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// 
        /// </remarks>
        public List<Role> SelectAllRoles()
        {
            List<Role> roles = new List<Role>();

            // build data connection and called stored procedure
            // process data
            var conn = new DBConnection().GetConnection();
            
            var cmdText = "sp_select_roles";
            // command object
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(new Role()
                        {
                            RoleId = reader.GetString(0),
                            Description = reader.IsDBNull(1) | reader.GetString(1) == "" ? "same as role name" : reader.GetString(1)
                        }); ;

                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return roles;

            //red test
            // throw new NotImplementedException();
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/02/09
        /// 
        /// 
        /// </summary>
        /// Selects all roles for a user given their usersId
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// 
        /// </remarks>
        /// <param name="UsersId"></param>
        public List<Role> SelectAllRolesByUserId(int usersId)
        {
            List<Role> userRoleList = new List<Role>();

            // build data connection and called stored procedure
            // process data
            var conn = new DBConnection().GetConnection();

            var cmdText = "sp_select_user_roles_by_usersId";
            // command object
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UsersId", SqlDbType.NVarChar);

            cmd.Parameters["@UsersId"].Value = usersId;
            //

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userRoleList.Add(new Role()
                        {
                            RoleId = reader.GetString(0),
                            Description = reader.IsDBNull(1) | reader.GetString(1) == "" ? "same as role name" : reader.GetString(1)
                        });

                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return userRoleList;
            //red test
            //throw new NotImplementedException();
        }

        // Created By: Asa Armstrong
        public int DeleteRoleByUsersIdAndRoleId(int usersId, string roleId)
        {
            int rowsAffected = 0;

            var conn = new DBConnection().GetConnection();
            var cmdText = "sp_delete_role_by_user_id_and_role_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UsersId", usersId);
            cmd.Parameters.AddWithValue("@RoleId", roleId);

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
                /*if (rowsAffected == -1)
                {
                    throw new ApplicationException("Cannot remove the last 'Admin' Role.");
                }*/
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rowsAffected;
        }

    }
}
