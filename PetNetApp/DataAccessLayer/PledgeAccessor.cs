using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class PledgeAccessor : IPledgeAccessor
    {
        public int InsertPledge(PledgeVM pledgeVM)
        {
            int result = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_insert_pledger";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            if (pledgeVM.UserId == 0)
            {
                cmd.Parameters.AddWithValue("@UsersId", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@UsersId", pledgeVM.UserId);
            }            
            cmd.Parameters.AddWithValue("@FundraisingEventId", pledgeVM.FundraisingEventId);
            cmd.Parameters.AddWithValue("@Amount", pledgeVM.Amount);
            cmd.Parameters.AddWithValue("@Target", pledgeVM.Target);
            cmd.Parameters.AddWithValue("@Requirement", pledgeVM.Requirement);
            cmd.Parameters.AddWithValue("@Message", pledgeVM.Message);
            cmd.Parameters.AddWithValue("@GivenName", pledgeVM.GivenName);
            cmd.Parameters.AddWithValue("@FamilyName", pledgeVM.FamilyName);
            cmd.Parameters.AddWithValue("@Phone", pledgeVM.Phone);
            cmd.Parameters.AddWithValue("@Email", pledgeVM.Email);
            cmd.Parameters.AddWithValue("@IsContactPreferencePhone", pledgeVM.IsContactPreferencePhone);

            try
            {
                conn.Open();

                result = cmd.ExecuteNonQuery();
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

        public List<PledgeVM> SelectAllPledges()
        {
            List<PledgeVM> pledges = new List<PledgeVM>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_all_pledges";

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
                        PledgeVM pledgeVM = new PledgeVM();
                        pledgeVM.PledgeId = reader.GetInt32(0);
                        pledgeVM.DonationId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                        pledgeVM.UserId = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                        pledgeVM.FundraisingEventId = reader.GetInt32(3);
                        pledgeVM.Date = reader.GetDateTime(4);
                        pledgeVM.Amount = reader.GetDecimal(5);

                        pledgeVM.Message = reader.IsDBNull(6) ? null : reader.GetString(6);
                        pledgeVM.Target = reader.IsDBNull(7) ? null : reader.GetString(7);
                        pledgeVM.Requirement = reader.IsDBNull(8) ? null : reader.GetString(8);
                        pledgeVM.RequirementMet = reader.IsDBNull(9) ? false : reader.GetBoolean(9); //If value is null in the database it can be assumed to be false


                        pledgeVM.GivenName = reader.GetString(10);
                        pledgeVM.FamilyName = reader.GetString(11);
                        pledgeVM.Phone = reader.IsDBNull(12) ? null : reader.GetString(12);
                        pledgeVM.Email = reader.IsDBNull(13) ? null : reader.GetString(13);


                        pledgeVM.IsContactPreferencePhone = reader.IsDBNull(14) ? false : reader.GetBoolean(14); //If value is null in the database it can be assumed to be false
                        pledgeVM.ReminderSent = reader.IsDBNull(14) ? false : reader.GetBoolean(14); //If value is null in the database it can be assumed to be false
                       
                        pledges.Add(pledgeVM);
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
            return pledges;
        }

        public List<PledgeVM> SelectAllPledgesByEventId(int eventId)
        {
            List<PledgeVM> _pledgeVMs = new List<PledgeVM>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_pledgers_by_fundraising_event_id";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EventId", eventId);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //_animal.MicrochipNumber = reader.IsDBNull(4) ? null : reader.GetString(4);
                        PledgeVM pledgeVM = new PledgeVM();
                        pledgeVM.PledgeId = reader.GetInt32(0);
                        pledgeVM.DonationId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                        pledgeVM.UserId = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                        pledgeVM.FundraisingEventId = reader.GetInt32(3);
                        pledgeVM.Date = reader.GetDateTime(4);
                        pledgeVM.Amount = reader.GetDecimal(5);
                        pledgeVM.Message = reader.IsDBNull(6) ? null : reader.GetString(6);
                        pledgeVM.Target = reader.IsDBNull(7) ? null : reader.GetString(7);
                        pledgeVM.GivenName = reader.GetString(8);
                        pledgeVM.FamilyName = reader.GetString(9);
                        pledgeVM.Phone = reader.IsDBNull(10) ? null : reader.GetString(10);
                        pledgeVM.Email = reader.IsDBNull(11) ? null : reader.GetString(11);
                        pledgeVM.DonationAmount = reader.IsDBNull(12) ? 0 : reader.GetDecimal(12);

                        _pledgeVMs.Add(pledgeVM);
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
            return _pledgeVMs;
        }

        public List<PledgeVM> SelectPledgerByUserId(int userId)
        {
            List<PledgeVM> _pledgerVMs = new List<PledgeVM>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_pledger_by_fundraising_user_id";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", userId);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PledgeVM pledgeVM = new PledgeVM();
                        pledgeVM.PledgeId = reader.GetInt32(0);
                        pledgeVM.DonationId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                        pledgeVM.UserId = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                        pledgeVM.FundraisingEventId = reader.GetInt32(3);
                        pledgeVM.Date = reader.GetDateTime(4);
                        pledgeVM.Amount = reader.GetDecimal(5);
                        pledgeVM.Message = reader.IsDBNull(6) ? null : reader.GetString(6);
                        pledgeVM.GivenName = reader.GetString(7);
                        pledgeVM.FamilyName = reader.GetString(8);
                        pledgeVM.Phone = reader.IsDBNull(9) ? null : reader.GetString(9);
                        pledgeVM.Email = reader.IsDBNull(10) ? null : reader.GetString(10);
                        pledgeVM.DonationAmount = reader.IsDBNull(11) ? 0 : reader.GetDecimal(11);
                        pledgeVM.DonationDate = reader.IsDBNull(12) ? DateTime.Now : reader.GetDateTime(12);
                        _pledgerVMs.Add(pledgeVM);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _pledgerVMs;
        }
    }
}
