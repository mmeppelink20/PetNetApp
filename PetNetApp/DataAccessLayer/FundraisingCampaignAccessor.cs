/// <summary>
/// Author: Stephen Jaurigue
/// Date: 2023/04/21
/// 
/// </summary>
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/27
/// 
/// Final QA
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
    /// <summary>
    /// Stephen Jaurigue
    /// Created: 2023/02/23
    /// 
    /// The data access layer class to access fundraising campaign information from the database
    /// </summary>
    public class FundraisingCampaignAccessor : IFundraisingCampaignAccessor
    {
        public List<FundraisingCampaignVM> SelectAllFundraisingCampaignsByShelterId(int shelterId)
        {
            List<FundraisingCampaignVM> fundraisingCampaigns = new List<FundraisingCampaignVM>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_all_fundraising_campaigns_by_shelterId";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ShelterId", SqlDbType.Int).Value = shelterId;

            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            fundraisingCampaigns.Add(new FundraisingCampaignVM()
                            {
                                FundraisingCampaignId = reader.GetInt32(0),
                                UsersId = reader.GetInt32(1),
                                ShelterId = reader.GetInt32(2),
                                Title = reader.GetString(3),
                                StartDate = reader.IsDBNull(4) ? new DateTime?() : reader.GetDateTime(4),
                                EndDate = reader.IsDBNull(5) ? new DateTime?() : reader.GetDateTime(5),
                                Description = reader.IsDBNull(6) ? null : reader.GetString(6),
                                Complete = reader.GetBoolean(7),
                                Active = reader.GetBoolean(8),
                                AmountRaised = reader.GetDecimal(9),
                                NumOfAttendees = reader.GetInt32(10),
                                NumAnimalsAdopted = reader.GetInt32(11),
                                Sponsors = new List<InstitutionalEntity>()
                            });
                        }
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
            return fundraisingCampaigns;
        }

        public int UpdateFundraisingCampaignDetails(FundraisingCampaignVM oldFundraisingCampaignVM, FundraisingCampaignVM newFundraisingCampaignVM)
        {
            int rows = 0;


            var fundraisingCampaignEntitiesToAdd = newFundraisingCampaignVM.Sponsors.Where(newEntity => !oldFundraisingCampaignVM.Sponsors.Exists(oldEntity => newEntity.InstitutionalEntityId == oldEntity.InstitutionalEntityId));
            var fundraisingCampaignEntitiesToRemove = oldFundraisingCampaignVM.Sponsors.Where(oldEntity => !newFundraisingCampaignVM.Sponsors.Exists(newEntity => newEntity.InstitutionalEntityId == oldEntity.InstitutionalEntityId));

            DBConnection factory = new DBConnection();
            var conn = factory.GetConnection();
            SqlTransaction trans = null;

            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                var cmdText = "sp_update_fundraising_campaign_details_by_campaignId";
                var cmd = new SqlCommand(cmdText, conn, trans);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@FundraisingCampaignId", SqlDbType.Int).Value = oldFundraisingCampaignVM.FundraisingCampaignId;

                cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 50).Value = newFundraisingCampaignVM.Title;
                cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = newFundraisingCampaignVM.StartDate == null ? (object)DBNull.Value : newFundraisingCampaignVM.StartDate.Value;
                cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = newFundraisingCampaignVM.EndDate == null ? (object)DBNull.Value : newFundraisingCampaignVM.EndDate.Value;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 250).Value = newFundraisingCampaignVM.Description;
                cmd.Parameters.Add("@Complete", SqlDbType.Bit).Value = newFundraisingCampaignVM.Complete;
                cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = newFundraisingCampaignVM.Active;


                cmd.Parameters.Add("@OldTitle", SqlDbType.NVarChar, 50).Value = oldFundraisingCampaignVM.Title;
                cmd.Parameters.Add("@OldStartDate", SqlDbType.DateTime).Value = oldFundraisingCampaignVM.StartDate == null ? (object)DBNull.Value : oldFundraisingCampaignVM.StartDate.Value;
                cmd.Parameters.Add("@OldEndDate", SqlDbType.DateTime).Value = oldFundraisingCampaignVM.EndDate == null ? (object)DBNull.Value : oldFundraisingCampaignVM.EndDate.Value;
                cmd.Parameters.Add("@OldDescription", SqlDbType.NVarChar, 250).Value = oldFundraisingCampaignVM.Description;
                cmd.Parameters.Add("@OldComplete", SqlDbType.Bit).Value = oldFundraisingCampaignVM.Complete;
                cmd.Parameters.Add("@OldActive", SqlDbType.Bit).Value = oldFundraisingCampaignVM.Active;
                rows = cmd.ExecuteNonQuery();

                foreach (var item in fundraisingCampaignEntitiesToAdd)
                {
                    var insertcmd = new SqlCommand("sp_insert_campaign_sponsor", conn, trans);
                    insertcmd.CommandType = CommandType.StoredProcedure;
                    insertcmd.Parameters.Add("@FundraisingCampaignId", SqlDbType.Int).Value = oldFundraisingCampaignVM.FundraisingCampaignId;
                    insertcmd.Parameters.Add("@InstitutionalEntityId", SqlDbType.Int).Value = item.InstitutionalEntityId;
                    rows += insertcmd.ExecuteNonQuery();
                }
                foreach (var item in fundraisingCampaignEntitiesToRemove)
                {
                    var removecmd = new SqlCommand("sp_delete_campaign_sponsor", conn, trans);
                    removecmd.CommandType = CommandType.StoredProcedure;
                    removecmd.Parameters.Add("@FundraisingCampaignId", SqlDbType.Int).Value = oldFundraisingCampaignVM.FundraisingCampaignId;
                    removecmd.Parameters.Add("@InstitutionalEntityId", SqlDbType.Int).Value = item.InstitutionalEntityId;
                    rows += removecmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans?.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public int InsertFundraisingCampaign(FundraisingCampaignVM fundraisingCampaign)
        {
            int campaignId = 0;

            DBConnection factory = new DBConnection();
            var conn = factory.GetConnection();
            SqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                var cmd = new SqlCommand("sp_insert_fundraising_campaign", conn, trans);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsersId", SqlDbType.Int).Value = fundraisingCampaign.UsersId;
                cmd.Parameters.Add("@ShelterId", SqlDbType.Int).Value = fundraisingCampaign.ShelterId;
                cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 50).Value = fundraisingCampaign.Title;
                cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = fundraisingCampaign.StartDate == null ? (object)DBNull.Value : fundraisingCampaign.StartDate.Value;
                cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = fundraisingCampaign.EndDate == null ? (object)DBNull.Value : fundraisingCampaign.EndDate.Value;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 250).Value = fundraisingCampaign.Description;

                campaignId = Convert.ToInt32(cmd.ExecuteScalar());

                foreach (var item in fundraisingCampaign.Sponsors)
                {
                    var insertcmd = new SqlCommand("sp_insert_campaign_sponsor", conn, trans);
                    insertcmd.CommandType = CommandType.StoredProcedure;
                    insertcmd.Parameters.Add("@FundraisingCampaignId", SqlDbType.Int).Value = campaignId;
                    insertcmd.Parameters.Add("@InstitutionalEntityId", SqlDbType.Int).Value = item.InstitutionalEntityId;
                    insertcmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans?.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return campaignId;
        }

        public FundraisingCampaignVM SelectFundraisingCampaignByFundraisingCampaignId(int fundraisingCampaignId)
        {
            FundraisingCampaignVM fundraisingCampaign = null;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_fundraising_campaign_by_campaignId";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FundraisingCampaignId", SqlDbType.Int).Value = fundraisingCampaignId;

            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        fundraisingCampaign = new FundraisingCampaignVM()
                        {
                            FundraisingCampaignId = reader.GetInt32(0),
                            UsersId = reader.GetInt32(1),
                            ShelterId = reader.GetInt32(2),
                            Title = reader.GetString(3),
                            StartDate = reader.IsDBNull(4) ? new DateTime?() : reader.GetDateTime(4),
                            EndDate = reader.IsDBNull(5) ? new DateTime?() : reader.GetDateTime(5),
                            Description = reader.IsDBNull(6) ? null : reader.GetString(6),
                            Complete = reader.GetBoolean(7),
                            Active = reader.GetBoolean(8),
                            AmountRaised = reader.GetDecimal(9),
                            NumOfAttendees = reader.GetInt32(10),
                            NumAnimalsAdopted = reader.GetInt32(11),
                            Sponsors = new List<InstitutionalEntity>()
                        };
                    }
                    else
                    {
                        throw new ApplicationException("No Fundraising Campaigns with id " + fundraisingCampaignId);
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
            return fundraisingCampaign;
        }

        public int DeleteFundraisingCampaign(FundraisingCampaignVM fundraisingCampaign)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_delete_fundraising_campaign_by_campaignId";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FundraisingCampaignId", SqlDbType.Int).Value = fundraisingCampaign.FundraisingCampaignId;

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

        public int InsertCampaignUpdate(CampaignUpdate campaignUpdate)
        {
            int id = 0;

            DBConnection factory = new DBConnection();
            var conn = factory.GetConnection();
            var cmdText = "sp_insert_campaign_update";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CampaignId", campaignUpdate.CampaignId);
            cmd.Parameters.AddWithValue("@UpdateTitle", campaignUpdate.UpdateTitle);
            cmd.Parameters.AddWithValue("@UpdateDescription", campaignUpdate.UpdateDescription);

            try
            {
                conn.Open();
                id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return id;
        }

        public int UpdateFundraisingCampaignResults(FundraisingCampaignVM oldFundraisingCampaignVM, FundraisingCampaignVM newFundraisingCampaignVM)
        {
            int rows = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // cmdText
            var cmdText = "sp_update_fundraising_campaign_results";

            //command
            var cmd = new SqlCommand(cmdText, conn);

            // type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@FundraisingCampaignId", SqlDbType.Int).Value = oldFundraisingCampaignVM.FundraisingCampaignId;
            cmd.Parameters.Add("@ShelterId", SqlDbType.Int).Value = oldFundraisingCampaignVM.ShelterId;

            cmd.Parameters.AddWithValue("@OldComplete",oldFundraisingCampaignVM.Complete);
            cmd.Parameters.AddWithValue("@OldAmountRaised", oldFundraisingCampaignVM.AmountRaised);
            cmd.Parameters.AddWithValue("@OldNumOfAttendees", oldFundraisingCampaignVM.NumOfAttendees);
            cmd.Parameters.AddWithValue("@OldNumAnimalsAdopted", oldFundraisingCampaignVM.NumAnimalsAdopted);
            
            cmd.Parameters.AddWithValue("@NewComplete",newFundraisingCampaignVM.Complete);
            cmd.Parameters.AddWithValue("@NewAmountRaised", newFundraisingCampaignVM.AmountRaised);
            cmd.Parameters.AddWithValue("@NewNumOfAttendees", newFundraisingCampaignVM.NumOfAttendees);
            cmd.Parameters.AddWithValue("@NewNumAnimalsAdopted", newFundraisingCampaignVM.NumAnimalsAdopted);
            
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

        public List<FundraisingCampaignVM> SelectAllActiveFundraisingCampaigns()
        {
            List<FundraisingCampaignVM> fundraisingCampaigns = new List<FundraisingCampaignVM>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_all_active_fundraising_campaigns";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            fundraisingCampaigns.Add(new FundraisingCampaignVM()
                            {
                                FundraisingCampaignId = reader.GetInt32(0),
                                UsersId = reader.GetInt32(1),
                                ShelterId = reader.GetInt32(2),
                                Title = reader.GetString(3),
                                StartDate = reader.IsDBNull(4) ? new DateTime?() : reader.GetDateTime(4),
                                EndDate = reader.IsDBNull(5) ? new DateTime?() : reader.GetDateTime(5),
                                Description = reader.IsDBNull(6) ? null : reader.GetString(6),
                                Complete = reader.GetBoolean(7),
                                Active = reader.GetBoolean(8),
                                Sponsors = new List<InstitutionalEntity>()
                            });
                        }
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
            return fundraisingCampaigns;
        }

        public List<FundraisingCampaignVM> SelectAllActiveFundraisingCampaignsByShelterId(int shelterId)
        {
            List<FundraisingCampaignVM> fundraisingCampaigns = new List<FundraisingCampaignVM>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_all_active_fundraising_campaigns_by_shelterId";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ShelterId", SqlDbType.Int).Value = shelterId;

            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            fundraisingCampaigns.Add(new FundraisingCampaignVM()
                            {
                                FundraisingCampaignId = reader.GetInt32(0),
                                UsersId = reader.GetInt32(1),
                                ShelterId = reader.GetInt32(2),
                                Title = reader.GetString(3),
                                StartDate = reader.IsDBNull(4) ? new DateTime?() : reader.GetDateTime(4),
                                EndDate = reader.IsDBNull(5) ? new DateTime?() : reader.GetDateTime(5),
                                Description = reader.IsDBNull(6) ? null : reader.GetString(6),
                                Complete = reader.GetBoolean(7),
                                Active = reader.GetBoolean(8),
                                Sponsors = new List<InstitutionalEntity>()
                            });
                        }
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
            return fundraisingCampaigns;
        }
    }
}
