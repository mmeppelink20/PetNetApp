using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    /// <summary>
    /// Mads Rhea
    /// Created: 2023/01/27
    /// 
    /// Accessor for all stored procedures relating to Users.
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd
    /// </remarks>
    public class UsersAccessor : IUsersAccessor
    {

        public List<UsersVM> SelectUserByRole(string roleId, int shelterId)
        {
            var users = new List<UsersVM>();

            var conn = new DBConnection().GetConnection();

            var cmdtext = "sp_select_users_by_roleId";

            var cmd = new SqlCommand(cmdtext, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@RoleId", SqlDbType.NVarChar);
            cmd.Parameters.Add("@ShelterId", SqlDbType.Int);

            cmd.Parameters["@RoleId"].Value = roleId;
            cmd.Parameters["@ShelterId"].Value = shelterId;

            try
            {
                // open connection
                conn.Open();

                // execute and get a SqlDataReader
                var reader = cmd.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UsersVM user = new UsersVM();
                        user.UsersId = reader.GetInt32(0);
                        user.GenderId = reader.GetString(1);
                        user.PronounId = reader.IsDBNull(2) ? null : reader.GetString(2);
                        user.ShelterId = reader.IsDBNull(3) ? null : (int?)reader.GetInt32(3);
                        user.GivenName = reader.IsDBNull(4) ? null : reader.GetString(4);
                        user.FamilyName = reader.GetString(5);
                        user.Email = reader.GetString(6);
                        user.Address = reader.IsDBNull(7) ? null : reader.GetString(7);
                        user.Address2 = reader.IsDBNull(8) ? null : reader.GetString(8);
                        user.Zipcode = reader.GetString(9);
                        user.Phone = reader.IsDBNull(10) ? null : reader.GetString(10);
                        user.CreationDate = reader.GetDateTime(11);
                        user.Active = reader.GetBoolean(12);
                        user.Suspend = reader.GetBoolean(13);
                        users.Add(user);
                    }



                }
                // close reader
                reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return users;


        }

        public List<UsersVM> SelectAllEmployees()
        {
            List<UsersVM> employeeList = new List<UsersVM>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_all_employees";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    UsersVM user = new UsersVM();
                    // [UsersId], [GenderId], [PronounId], [ShelterId], [GivenName], [FamilyName],
                    // [Email], [PasswordHash], [Address], [Address2], [Zipcode], [Phone], [CreationDate], 
                    // [Active], [Suspended]

                    user.UsersId = reader.GetInt32(0);
                    user.GenderId = reader.GetString(1);
                    user.PronounId = reader.IsDBNull(2) ? "N/A" : reader.GetString(2);
                    user.ShelterId = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                    user.GivenName = reader.IsDBNull(4) ? null : reader.GetString(4);
                    user.FamilyName = reader.GetString(5);
                    user.Email = reader.GetString(6);
                    user.Address = reader.IsDBNull(7) ? null : reader.GetString(7);
                    user.Address2 = reader.IsDBNull(8) ? null : reader.GetString(8);
                    user.Zipcode = reader.GetString(9);
                    user.Phone = reader.IsDBNull(10) ? null : reader.GetString(10);
                    user.CreationDate = reader.GetDateTime(11);
                    user.Active = reader.GetBoolean(12);
                    user.Suspend = reader.GetBoolean(13);

                    employeeList.Add(user);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return employeeList;
        }

        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_authenticate_user";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            try
            {
                conn.Open();

                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public UsersVM SelectUserByEmail(string email)
        {
            UsersVM user = null;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_user_by_email";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 254);

            cmd.Parameters["@Email"].Value = email;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    user = new UsersVM
                    {
                        UsersId = reader.GetInt32(0),
                        GenderId = reader.GetString(1),
                        PronounId = reader.GetString(2),
                        // nullable
                        GivenName = reader.GetString(4),
                        FamilyName = reader.GetString(5),
                        Email = reader.GetString(6),
                        // nullable
                        // nullable
                        Zipcode = reader.GetString(9),
                        Phone = reader.GetString(10),
                        CreationDate = reader.GetDateTime(11),
                        Active = reader.GetBoolean(12),
                        Suspend = reader.GetBoolean(13),
                        Roles = new List<string>()
                    };
                    if (reader.IsDBNull(3))
                    {
                        user.ShelterId = null;
                    }
                    else
                    {
                        user.ShelterId = reader.GetInt32(3);
                    }
                    if (reader.IsDBNull(8))
                    {
                        user.ShelterId = null;
                    }
                    else
                    {
                        user.Address2 = reader.GetString(8);
                    }
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }
                reader.Close();
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }

            return user;
        }

        public List<string> SelectRolesByUserID(int userId)
        {
            List<string> roles = new List<string>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_roles_by_userid";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UsersId", SqlDbType.Int);

            cmd.Parameters["@UsersId"].Value = userId;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                }

                reader.Close();
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }

            return roles;
        }

        public List<string> SelectAllPronouns()
        {
            List<string> pronouns = new List<string>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_all_pronouns";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        pronouns.Add(reader.GetString(0));
                    }
                }
                else
                {
                    throw new ArgumentException("Cannot retrieve pronouns.");
                }

                reader.Close();
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }

            return pronouns;
        }

        public List<string> SelectAllGenders()
        {
            List<string> genders = new List<string>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_all_genders";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        genders.Add(reader.GetString(0));
                    }
                }
                else
                {
                    throw new ArgumentException("Cannot retrieve genders.");
                }

                reader.Close();
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }

            return genders;
        }

        public int CreateNewUser(Users user, string PasswordHash)
        {

            int rows = 0;

            //connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            //cmdText
            var cmdText = "sp_insert_new_user";

            //command
            var cmd = new SqlCommand(cmdText, conn);

            //type
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //Parameters

            cmd.Parameters.AddWithValue("@GenderId", user.GenderId);
            cmd.Parameters.AddWithValue("@PronounId", user.PronounId);
            cmd.Parameters.AddWithValue("@GivenName", user.GivenName);
            cmd.Parameters.AddWithValue("@FamilyName", user.FamilyName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@PasswordHash", PasswordHash);
            cmd.Parameters.AddWithValue("@Zipcode", user.Zipcode);
            cmd.Parameters.AddWithValue("@Phone", user.Phone);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        public int DeactivateUserAccount(int UserId)
        {
            int rows = 0;

            DBConnection connectionFactory = new DBConnection();

            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_deactivate_account";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UsersId", SqlDbType.Int);
            cmd.Parameters["@UsersId"].Value = UserId;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        public List<UsersVM> SelectUsersByUsersId(int usersId)
        {
            List<UsersVM> users = new List<UsersVM>();
            //connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            //command text
            var cmdText = "sp_select_users_by_users_id";
            //command
            var cmd = new SqlCommand(cmdText, conn);
            //Command Type

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UsersId", SqlDbType.Int);

            cmd.Parameters["@UsersId"].Value = usersId;

            try
            {
                // open connection
                conn.Open();

                // execute and get a SqlDataReader
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        UsersVM user = new UsersVM();
                        user.UsersId = reader.GetInt32(0); users.Add(user);
                        user.GenderId = reader.GetString(1);
                        user.PronounId = reader.IsDBNull(2) ? null : reader.GetString(2);
                        user.ShelterId = reader.IsDBNull(3) ? null : (int?)reader.GetInt32(3);
                        user.GivenName = reader.IsDBNull(4) ? null : reader.GetString(4);
                        user.FamilyName = reader.GetString(5);
                        user.Email = reader.GetString(6);
                        //user.PasswordHash = reader.GetString(7);
                        user.Address = reader.IsDBNull(7) ? null : reader.GetString(7);
                        user.Address2 = reader.IsDBNull(8) ? null : reader.GetString(8);
                        user.Zipcode = reader.GetString(10);
                        user.Phone = reader.IsDBNull(10) ? null : reader.GetString(10);
                        user.CreationDate = reader.GetDateTime(12);
                        user.Active = reader.GetBoolean(13);
                        user.Suspend = reader.GetBoolean(14);
                        users.Add(user);

                    }
                }
                // close reader
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return users;


        }

        public Users SelectUserByUsersId(int UsersId)
        {
            var user = new Users();

            var conn = new DBConnection().GetConnection();

            var cmdtext = "sp_select_user_by_usersId";

            var cmd = new SqlCommand(cmdtext, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UsersId", SqlDbType.Int);

            cmd.Parameters["@UsersId"].Value = UsersId;

            try
            {
                // open connection
                conn.Open();

                // execute and get a SqlDataReader
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    //// [GivenName], [FamilyName],[UserName],[gender], [Email]
                    //while (reader.Read())
                    //{
                    user.UsersId = reader.GetInt32(0);
                    user.GenderId = reader.GetString(1);
                    user.PronounId = reader.GetString(2);
                    user.ShelterId = reader.GetInt32(3);
                    user.GivenName = reader.GetString(4);
                    user.FamilyName = reader.GetString(5);
                    user.Email = reader.GetString(6);
                    user.Address = reader.GetString(7);
                    user.Address2 = reader.GetString(8);
                    user.Zipcode = reader.GetString(9);
                    user.Phone = reader.GetString(10);
                    user.CreationDate = reader.GetDateTime(11);
                    user.Active = reader.GetBoolean(12);
                    user.Suspend = reader.GetBoolean(13);
                    //}
                }
                // close reader
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return user;
        }

        public UsersVM SelectUserByUsersIdWithRoles(int UsersId)
        {
            throw new NotImplementedException();
        }

        public int UpdateUserActive(int userId, bool active)
        {
            int rows;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_update_user_active_by_user_id";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UsersId", SqlDbType.Int);
            cmd.Parameters["@UsersId"].Value = userId;
            cmd.Parameters.Add("@Active", SqlDbType.Int);
            cmd.Parameters["@Active"].Value = active;


            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        public int UpdateUserDetails(Users oldUser, Users updatedUser)
        {


            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_update_user_details";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UsersId", oldUser.UsersId);
            cmd.Parameters.AddWithValue("@OldGivenName", oldUser.GivenName);
            cmd.Parameters.AddWithValue("@OldFamilyName", oldUser.FamilyName);
            cmd.Parameters.AddWithValue("@OldGenderId", oldUser.GenderId);
            cmd.Parameters.AddWithValue("@OldPronounId", oldUser.PronounId);
            cmd.Parameters.AddWithValue("@OldAddress", oldUser.Address);
            cmd.Parameters.AddWithValue("@OldAddress2", oldUser.Address2);
            cmd.Parameters.AddWithValue("@OldPhone", oldUser.Phone);
            cmd.Parameters.AddWithValue("@OldZipcode", oldUser.Zipcode);

            cmd.Parameters.Add("@NewGivenName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewGivenName"].Value = updatedUser.GivenName;
            cmd.Parameters.Add("@NewFamilyName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewFamilyName"].Value = updatedUser.FamilyName;
            cmd.Parameters.Add("@NewGenderId", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewGenderId"].Value = updatedUser.GenderId;
            cmd.Parameters.Add("@NewPronounId", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewPronounId"].Value = updatedUser.PronounId;
            cmd.Parameters.Add("@NewAddress", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewAddress"].Value = updatedUser.Address;
            cmd.Parameters.Add("@NewAddress2", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewAddress2"].Value = updatedUser.Address2;
            cmd.Parameters.Add("@NewPhone", SqlDbType.NVarChar, 13);
            cmd.Parameters["@NewPhone"].Value = updatedUser.Phone;
            cmd.Parameters.Add("@NewZipcode", SqlDbType.Char, 9);
            cmd.Parameters["@NewZipcode"].Value = updatedUser.Zipcode;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }

            return rows;

        }

        public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash)
        {
            int rowsAffected = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            string cmdText = "sp_update_passwordHash";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@OldPasswordHash"].Value = oldPasswordHash;
            cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();

            }

            return rowsAffected;
        }

        public int UpdateUserEmail(string oldEmail, string newEmail, string passwordHash)
        {
            int rowsAffected = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            string cmdText = "sp_update_user_email";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@OldEmail", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewEmail", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            cmd.Parameters["@OldEmail"].Value = oldEmail;
            cmd.Parameters["@NewEmail"].Value = newEmail;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        public int UpdateUserSuspend(int usersId, bool suspend)
        {
            int rowsAffected = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            string cmdText = "sp_update_user_suspend_by_user_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UsersId", SqlDbType.Int);
            cmd.Parameters.Add("@Suspended", SqlDbType.Bit);

            cmd.Parameters["@UsersId"].Value = usersId;
            cmd.Parameters["@Suspended"].Value = suspend;

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
            // throw new NotImplementedException();
        }

        public int SelectCountActiveUnsuspendedUsersByRole(string roleId)
        {

            int roleCount = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            string cmdText = "sp_select_count_active_unsuspended_users_by_roleId";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@RoleId", SqlDbType.NVarChar, 50);

            cmd.Parameters["@RoleId"].Value = roleId;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader .Read();
                    roleCount = reader.GetInt32(0);
                    
                }
                // close reader
                reader.Close();

            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }

            return roleCount;
            // throw new NotImplementedException();
        }

        public List<UsersAdoptionRecords> SelectAdoptionRecordsByUserID(int usersId)
        {
            var adoptionRecordsList = new List<UsersAdoptionRecords>();


            var conn = new DBConnection().GetConnection();

            var cmdtext = "sp_select_adoption_records_by_user_id";

            var cmd = new SqlCommand(cmdtext, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UsersId", SqlDbType.Int);

            cmd.Parameters["@UsersId"].Value = usersId;



            try
            {

                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var adoptionRecords = new UsersAdoptionRecords();

                        adoptionRecords.animalName = reader.GetString(0);
                        adoptionRecords.animalSpecies = reader.GetString(1);
                        adoptionRecords.animalBreed = reader.GetString(2);
                        adoptionRecords.oldAnimalId = reader.GetInt32(3);

                        adoptionRecordsList.Add(adoptionRecords);
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return adoptionRecordsList;
        }

        public List<string> SelectAllRoles()
        {
            List<string> roles = new List<string>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_all_roles";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                }
                else
                {
                    throw new ArgumentException("Cannot retrieve roles.");
                }

                reader.Close();
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }

            return roles;
        }

        public UsersVM AuthenticateUser(string email, string passwordHash)
        {
            UsersVM result = null;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmd = new SqlCommand("sp_authenticate_user");
            cmd.Connection = conn;

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters for the procedure
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            // set the values for the parameters
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            // now that the command is set up, we can execute it
            try
            {
                // open the connection
                conn.Open();

                // execute the command
                if (1 == Convert.ToInt32(cmd.ExecuteScalar()))
                {
                    // if the command worked correctly, get a user
                    // object
                    result = SelectUserByEmail(email);
                }
                else
                {
                    throw new ApplicationException("User not found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }


        public int UpdateUserShelterid(int userid, int shelterid, int? oldShelterId)
        {
            int rowsAffected = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            string cmdText = "sp_update_usershelter";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Usersid", userid);
            if(oldShelterId == null)
            {
                cmd.Parameters.AddWithValue("@OldShelterid", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OldShelterid", oldShelterId);
            }
            
            cmd.Parameters.AddWithValue("@NewShelterid", shelterid);

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        public int InsertOrDeleteUserRole(int usersId, string role, bool delete = false)
        {
            int rows = 0;

            string cmdText = delete ? "sp_delete_user_role" : "sp_insert_user_role";

            DBConnection connectionFactory = new DBConnection();

            var conn = connectionFactory.GetConnection();
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UsersId", usersId);
            cmd.Parameters.AddWithValue("@RoleId", role);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

    }
}



