/// <summary>
/// Oleksiy Fedchuk
/// Created: 2023/02/23
/// 
/// </summary>
/// <remarks>
/// Zaid Rachman
/// Updated: 2023/04/24
/// 
/// Final QA
/// </remarks>
using DataAccessLayerInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class VolunteerAccessor : IVolunteerAccessor
    {
        public int DeleteVolunteerFromEventbyUsersIdAndFundraisingEventId(int UsersId, int FundraisingEventId)
        {
            int rowsAffected = 0;

            var conn = new DBConnection().GetConnection();

            var cmdtext = "sp_delete_user_from_event_by_user_id_and_event_id";

            var cmd = new SqlCommand(cmdtext, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UsersId", SqlDbType.Int);
            cmd.Parameters.Add("@FundraisingEventId", SqlDbType.Int);

            cmd.Parameters["@UsersId"].Value = UsersId;
            cmd.Parameters["@FundraisingEventId"].Value = FundraisingEventId;

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

        public int InsertVolunteerToEventbyVolunteerAndEventId(int userId, int fundraisingEventId)
        {
            int rowsAffected = 0;

            var conn = new DBConnection().GetConnection();

            var cmdText = "sp_insert_volunteer_into_event_by_volunteerID_and_eventID";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UsersId", userId);
            cmd.Parameters.AddWithValue("@FundraisingEventId", fundraisingEventId);

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

        public List<int> SelectAllVolunteers()
        {
            List<int> volunteers = new List<int>();

            var conn = new DBConnection().GetConnection();

            var cmdText = "sp_select_all_volunteers";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                int tempId = 0;

                while (reader.Read())
                {
                    tempId = reader.GetInt32(0);
                    volunteers.Add(tempId);
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

            return volunteers;
        }

        public List<VolunteerVM> SelectVolunteersbyFundraisingEventId(int fundraisingEventId)
        {
            List<VolunteerVM> volunteers = new List<VolunteerVM>();

            var conn = new DBConnection().GetConnection();

            var cmdtext = "sp_select_volunteers_by_fundraising_event_id";

            var cmd = new SqlCommand(cmdtext, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FundraisingEventId", SqlDbType.Int);
            cmd.Parameters["@FundraisingEventId"].Value = fundraisingEventId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        VolunteerVM volunteer = new VolunteerVM();
                        volunteer.FundraisingEventId = reader.GetInt32(0);
                        volunteer.UsersId = reader.GetInt32(1);
                        volunteer.GivenName = reader.GetString(2);
                        volunteer.FamilyName = reader.GetString(3);

                        volunteers.Add(volunteer);
                    }
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

            return volunteers;
        }
    }
}