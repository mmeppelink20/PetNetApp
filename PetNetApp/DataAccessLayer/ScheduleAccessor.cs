/// <summary>
/// Chris Dreismeier
/// Created: 2023/02/09
/// 
/// 
/// Class for Accessing data from the database
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayer
{
    public class ScheduleAccessor : IScheduleAccessor
    {
        public int DeleteScheduleVM(int scheduleId)
        {
            int rows = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            //  command text
            var cmdText = "sp_delete_schedule_by_scheduleid";

            // command 
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameter objects to the command 
            cmd.Parameters.AddWithValue("@ScheduleId", scheduleId);


            try
            {
                // open the connection
                conn.Open();
                // execute command
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // close the connection
                conn.Close();
            }


            return rows;
        }

        public int InsertSchedulebyUserid(ScheduleVM scheduleVM)
        {
            int rowsAffected = 0;

            var conn = new DBConnection().GetConnection();

            var cmdText = "sp_insert_schedule";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UsersId", SqlDbType.Int);
            cmd.Parameters.Add("@StartTime", SqlDbType.DateTime);
            cmd.Parameters.Add("@EndTime", SqlDbType.DateTime);

            cmd.Parameters["@UsersId"].Value = scheduleVM.UserId;
            cmd.Parameters["@StartTime"].Value = scheduleVM.StartTime;
            cmd.Parameters["@EndTime"].Value = scheduleVM.EndTime;

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
        public List<ScheduleVM> SelectScheduleByDate(DateTime selectedDate)
        {
            var schedules = new List<ScheduleVM>();

            var conn = new DBConnection().GetConnection();

            var cmdtext = "sp_select_schedule_by_date";

            var cmd = new SqlCommand(cmdtext, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@selectedDate", SqlDbType.DateTime);

            cmd.Parameters["@selectedDate"].Value = selectedDate;

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
                        ScheduleVM schedule = new ScheduleVM();
                        schedule.ScheduleId = reader.GetInt32(0);
                        schedule.UserId = reader.GetInt32(1);
                        schedule.JobId = reader.IsDBNull(2) ? null : (int?)reader.GetInt32(2);
                        schedule.StartTime = reader.GetDateTime(3);
                        schedule.EndTime = reader.GetDateTime(4);
                        schedule.GivenName = reader.GetString(5);
                        schedule.FamilyName = reader.GetString(6);
                        schedules.Add(schedule);
                    }
                }
                // close reader
                reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return schedules;
        }
        public List<ScheduleVM> SelectScheduleByUser(int userId)
        {
            var schedules = new List<ScheduleVM>();

            var conn = new DBConnection().GetConnection();

            var cmdtext = "sp_select_schedule_by_userId";

            var cmd = new SqlCommand(cmdtext, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserId", SqlDbType.Int);

            cmd.Parameters["@UserId"].Value = userId;

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
                        ScheduleVM schedule = new ScheduleVM();
                        schedule.ScheduleId = reader.GetInt32(0);
                        schedule.UserId = reader.GetInt32(1);
                        schedule.JobId = reader.IsDBNull(2) ? null : (int?)reader.GetInt32(2);
                        schedule.StartTime = reader.GetDateTime(3);
                        schedule.EndTime = reader.GetDateTime(4);
                        schedule.GivenName = reader.GetString(5);
                        schedule.FamilyName = reader.GetString(6);
                        schedules.Add(schedule);
                    }
                }
                // close reader
                reader.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return schedules;
        }

        public int UpdateScheduleVM(ScheduleVM oldSchedule, ScheduleVM newSchedule)
        {
            int rows = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            //  command text
            var cmdText = "sp_update_schedule";

            // command 
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameter objects to the command 
            cmd.Parameters.Add("@UsersId", SqlDbType.Int);
            cmd.Parameters.Add("@ScheduleId", SqlDbType.Int);
            cmd.Parameters.Add("@StartTime", SqlDbType.DateTime);
            cmd.Parameters.Add("@EndTime", SqlDbType.DateTime);
            cmd.Parameters.Add("@OldStartTime", SqlDbType.DateTime);
            cmd.Parameters.Add("@OldEndTime", SqlDbType.DateTime);

            // set the values for the parameter objects
            cmd.Parameters["@UsersId"].Value = newSchedule.UserId;
            cmd.Parameters["@ScheduleId"].Value = newSchedule.ScheduleId;
            cmd.Parameters["@StartTime"].Value = newSchedule.StartTime;
            cmd.Parameters["@EndTime"].Value = newSchedule.EndTime;
            cmd.Parameters["@OldStartTime"].Value = oldSchedule.StartTime;
            cmd.Parameters["@OldEndTime"].Value = oldSchedule.EndTime;

            // now that the command is set up, we can invoke it in a try-catch block
            try
            {
                // open the connection
                conn.Open();
                // execute command
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // close the connection
                conn.Close();
            }


            return rows;
        }
    }
}
