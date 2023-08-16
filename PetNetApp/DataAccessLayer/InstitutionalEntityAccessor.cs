/// <summary>
/// Asa Armstrong
/// Created: 2023/03/01
/// 
/// Data Accessor class to CRUD Institutional Entity objects.
/// </summary>
///
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
    /// The data access layer class to access institutional entity information from the database
    /// </summary>
    public class InstitutionalEntityAccessor : IInstitutionalEntityAccessor
    {
        public List<InstitutionalEntity> SelectAllContact()
        {
            List<InstitutionalEntity> contacts = new List<InstitutionalEntity>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_all_fundraising_contacts";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InstitutionalEntity institutionalEntity = new InstitutionalEntity();

                        institutionalEntity.InstitutionalEntityId = reader.GetInt32(0);
                        institutionalEntity.CompanyName = reader.IsDBNull(1) ? null : reader.GetString(1);
                        institutionalEntity.GivenName = reader.GetString(2);
                        institutionalEntity.FamilyName = reader.GetString(3);
                        institutionalEntity.Email = reader.GetString(4);
                        institutionalEntity.Phone = reader.GetString(5);
                        institutionalEntity.Address = reader.GetString(6);
                        institutionalEntity.Address2 = reader.IsDBNull(7) ? null : reader.GetString(7);
                        institutionalEntity.Zipcode = reader.GetString(8);
                        institutionalEntity.ContactType = reader.GetString(9);

                        contacts.Add(institutionalEntity);
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

            return contacts;
        }

        public List<InstitutionalEntity> SelectAllHosts()
        {
            List<InstitutionalEntity> hosts = new List<InstitutionalEntity>();


            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_all_fundraising_hosts";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InstitutionalEntity institutionalEntity = new InstitutionalEntity();

                        institutionalEntity.InstitutionalEntityId = reader.GetInt32(0);
                        institutionalEntity.CompanyName = reader.IsDBNull(1) ? null : reader.GetString(1);
                        institutionalEntity.GivenName = reader.GetString(2);
                        institutionalEntity.FamilyName = reader.GetString(3);
                        institutionalEntity.Email = reader.GetString(4);
                        institutionalEntity.Phone = reader.GetString(5);
                        institutionalEntity.Address = reader.GetString(6);
                        institutionalEntity.Address2 = reader.IsDBNull(7) ? null : reader.GetString(7);
                        institutionalEntity.Zipcode = reader.GetString(8);
                        institutionalEntity.ContactType = reader.GetString(9);

                        hosts.Add(institutionalEntity);
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

            return hosts;
        }

        public List<InstitutionalEntity> SelectAllSponsors()
        {
            List<InstitutionalEntity> sponsors = new List<InstitutionalEntity>();


            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_all_fundraising_sponsors";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InstitutionalEntity institutionalEntity = new InstitutionalEntity();

                        institutionalEntity.InstitutionalEntityId = reader.GetInt32(0);
                        institutionalEntity.CompanyName = reader.IsDBNull(1) ? null : reader.GetString(1);
                        institutionalEntity.GivenName = reader.GetString(2);
                        institutionalEntity.FamilyName = reader.GetString(3);
                        institutionalEntity.Email = reader.GetString(4);
                        institutionalEntity.Phone = reader.GetString(5);
                        institutionalEntity.Address = reader.GetString(6);
                        institutionalEntity.Address2 = reader.IsDBNull(7) ? null : reader.GetString(7);
                        institutionalEntity.Zipcode = reader.GetString(8);
                        institutionalEntity.ContactType = reader.GetString(9);

                        sponsors.Add(institutionalEntity);
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

            return sponsors;
        }

        public List<InstitutionalEntity> SelectFundraisingSponsorsByCampaignId(int fundraisingCampaignId)
        {
            List<InstitutionalEntity> sponsors = new List<InstitutionalEntity>();


            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_fundraising_sponsors_by_campaignId";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.Parameters.Add("@FundraisingCampaignId", SqlDbType.Int).Value = fundraisingCampaignId;

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InstitutionalEntity institutionalEntity = new InstitutionalEntity();

                        institutionalEntity.InstitutionalEntityId = reader.GetInt32(0);
                        institutionalEntity.CompanyName = reader.IsDBNull(1) ? null : reader.GetString(1);
                        institutionalEntity.GivenName = reader.GetString(2);
                        institutionalEntity.FamilyName = reader.GetString(3);
                        institutionalEntity.Email = reader.GetString(4);
                        institutionalEntity.Phone = reader.GetString(5);
                        institutionalEntity.Address = reader.GetString(6);
                        institutionalEntity.Address2 = reader.IsDBNull(7) ? null : reader.GetString(7);
                        institutionalEntity.Zipcode = reader.GetString(8);
                        institutionalEntity.ContactType = reader.GetString(9);

                        sponsors.Add(institutionalEntity);
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

            return sponsors;
        }


        public List<InstitutionalEntity> SelectAllInstitutionalEntitiesByShelterIdAndEntityType(int shelterId, string entityType)
        {
            List<InstitutionalEntity> institutionalEntities = new List<InstitutionalEntity>();

            var conn = new DBConnection().GetConnection();
            var cmdText = "sp_select_all_institutionalEntities_by_shelterId_and_entityType";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ShelterId", shelterId);
            cmd.Parameters.AddWithValue("@EntityType", entityType);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        institutionalEntities.Add(new InstitutionalEntity
                        {
                            InstitutionalEntityId = reader.GetInt32(0),
                            CompanyName = (reader.IsDBNull(1) ? null : reader.GetString(1)),
                            GivenName = reader.GetString(2),
                            FamilyName = reader.GetString(3),
                            Email = reader.GetString(4),
                            Phone = reader.GetString(5),
                            Address = reader.GetString(6),
                            Address2 = (reader.IsDBNull(7) ? null : reader.GetString(7)),
                            Zipcode = reader.GetString(8).Substring(0, 5),
                            ContactType = reader.GetString(9),
                            ShelterId = reader.GetInt32(10)
                        });
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

            return institutionalEntities;
        }

        public int UpdateInstitutionalEntity(InstitutionalEntity oldEntity, InstitutionalEntity newEntity)
        {
            int rows = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // cmdText
            var cmdText = "sp_update_institutional_entity";

            //command
            var cmd = new SqlCommand(cmdText, conn);

            // type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@InstitutionalEntityId", oldEntity.InstitutionalEntityId);
            cmd.Parameters.AddWithValue("@ShelterId", oldEntity.ShelterId);
            cmd.Parameters.AddWithValue("@OldGivenName", oldEntity.GivenName);
            cmd.Parameters.AddWithValue("@OldFamilyName", oldEntity.FamilyName);
            cmd.Parameters.AddWithValue("@OldEmail", oldEntity.Email);
            cmd.Parameters.AddWithValue("@OldPhone", oldEntity.Phone);
            cmd.Parameters.AddWithValue("@OldAddress", oldEntity.Address);
            cmd.Parameters.AddWithValue("@OldZipcode", oldEntity.Zipcode);
            cmd.Parameters.AddWithValue("@OldContactType", oldEntity.ContactType);

            if (oldEntity.CompanyName == null)
            {
                cmd.Parameters.AddWithValue("@OldCompanyName", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OldCompanyName", oldEntity.CompanyName);
            }

            if (oldEntity.Address2 == null)
            {
                cmd.Parameters.AddWithValue("@OldAddress2", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OldAddress2", oldEntity.Address2);
            }

            cmd.Parameters.AddWithValue("@NewGivenName", newEntity.GivenName);
            cmd.Parameters.AddWithValue("@NewFamilyName", newEntity.FamilyName);
            cmd.Parameters.AddWithValue("@NewEmail", newEntity.Email);
            cmd.Parameters.AddWithValue("@NewPhone", newEntity.Phone);
            cmd.Parameters.AddWithValue("@NewAddress", newEntity.Address);
            cmd.Parameters.AddWithValue("@NewZipcode", newEntity.Zipcode);
            cmd.Parameters.AddWithValue("@NewContactType", newEntity.ContactType);

            if (newEntity.CompanyName == null)
            {
                cmd.Parameters.AddWithValue("@NewCompanyName", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NewCompanyName", newEntity.CompanyName);
            }

            if (newEntity.Address2 == null)
            {
                cmd.Parameters.AddWithValue("@NewAddress2", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NewAddress2", newEntity.Address2);
            }


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

        public int InsertInstitutionalEntity(InstitutionalEntity institutionalEntity)
        {
            int id = 0;

            DBConnection factory = new DBConnection();
            var conn = factory.GetConnection();
            var cmdText = "sp_insert_institutional_entity";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@GivenName", institutionalEntity.GivenName);
            cmd.Parameters.AddWithValue("@FamilyName", institutionalEntity.FamilyName);
            cmd.Parameters.AddWithValue("@Email", institutionalEntity.Email);
            cmd.Parameters.AddWithValue("@Phone", institutionalEntity.Phone);
            cmd.Parameters.AddWithValue("@Address", institutionalEntity.Address);
            cmd.Parameters.AddWithValue("@Zipcode", institutionalEntity.Zipcode);
            cmd.Parameters.AddWithValue("@ContactType", institutionalEntity.ContactType);
            cmd.Parameters.AddWithValue("@ShelterId", institutionalEntity.ShelterId);

            if (institutionalEntity.CompanyName == null)
            {
                cmd.Parameters.AddWithValue("@CompanyName", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CompanyName", institutionalEntity.CompanyName);
            }

            if (institutionalEntity.Address2 == null)
            {
                cmd.Parameters.AddWithValue("@Address2", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Address2", institutionalEntity.Address2);
            }

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

        public List<InstitutionalEntity> SelectFundraisingEventInstitutionalEntitiesByEventIdAndContactType(int fundraisingEventId, string contactType)
        {
            List<InstitutionalEntity> institutionalEntities = new List<InstitutionalEntity>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_fundraising_institutional_entities_by_eventId_contactType";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.Parameters.Add("@FundraisingEventId", SqlDbType.Int).Value = fundraisingEventId;
            cmd.Parameters.Add("@ContactType", SqlDbType.NVarChar).Value = contactType;

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InstitutionalEntity institutionalEntity = new InstitutionalEntity();

                        institutionalEntity.InstitutionalEntityId = reader.GetInt32(0);
                        institutionalEntity.CompanyName = reader.IsDBNull(1) ? null : reader.GetString(1);
                        institutionalEntity.GivenName = reader.GetString(2);
                        institutionalEntity.FamilyName = reader.GetString(3);
                        institutionalEntity.Email = reader.GetString(4);
                        institutionalEntity.Phone = reader.GetString(5);
                        institutionalEntity.Address = reader.GetString(6);
                        institutionalEntity.Address2 = reader.IsDBNull(7) ? null : reader.GetString(7);
                        institutionalEntity.Zipcode = reader.GetString(8);
                        institutionalEntity.ContactType = reader.GetString(9);

                        institutionalEntities.Add(institutionalEntity);
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

            return institutionalEntities;
        }

        public InstitutionalEntity SelectInstitutionalEntityByFundraisingEventIdAndContactType(int fundraisingEventId, string contactType)
        {
            //throw new NotImplementedException();

            InstitutionalEntity institutionalEntity = new InstitutionalEntity();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_institutional_entity_by_event_id_and_contact_type";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.Parameters.Add("@FundraisingEventId", SqlDbType.Int).Value = fundraisingEventId;
            cmd.Parameters.Add("@ContactType", SqlDbType.NVarChar).Value = contactType;

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        institutionalEntity.InstitutionalEntityId = reader.GetInt32(0);
                        institutionalEntity.CompanyName = reader.IsDBNull(1) ? null : reader.GetString(1);
                        institutionalEntity.GivenName = reader.GetString(2);
                        institutionalEntity.FamilyName = reader.GetString(3);
                        institutionalEntity.Email = reader.GetString(4);
                        institutionalEntity.Phone = reader.GetString(5);
                        institutionalEntity.Address = reader.GetString(6);
                        institutionalEntity.Address2 = reader.IsDBNull(7) ? null : reader.GetString(7);
                        institutionalEntity.Zipcode = reader.GetString(8);
                        institutionalEntity.ContactType = reader.GetString(9);
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

            return institutionalEntity;
        }

        public InstitutionalEntity SelectInstitutionalEntityByInstitutionalEntityId(int institutionalEntityId)
        {
            InstitutionalEntity institutionalEntity = new InstitutionalEntity();


            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_institutional_entity_by_institutionalId";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.Parameters.Add("@InstitutionalEntityId", SqlDbType.Int).Value = institutionalEntityId;

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        institutionalEntity.InstitutionalEntityId = reader.GetInt32(0);
                        institutionalEntity.CompanyName = reader.IsDBNull(1) ? null : reader.GetString(1);
                        institutionalEntity.GivenName = reader.GetString(2);
                        institutionalEntity.FamilyName = reader.GetString(3);
                        institutionalEntity.Email = reader.GetString(4);
                        institutionalEntity.Phone = reader.GetString(5);
                        institutionalEntity.Address = reader.GetString(6);
                        institutionalEntity.Address2 = reader.IsDBNull(7) ? null : reader.GetString(7);
                        institutionalEntity.Zipcode = reader.GetString(8);
                        institutionalEntity.ContactType = reader.GetString(9);

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

            return institutionalEntity;
        }

        public List<SponsorEvent> SelectSponsorEventByName(String name) 
        {
            List<SponsorEvent> institutionalEntities = new List<SponsorEvent>();
            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_all_fundraising_sponsors_By_name";

            // command 
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyName", name);
            //cmd.AddW["@AnimalId"].Value = name;
            // try-catch-finally
            try
            {
                // open a connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var sponsorEvent = new SponsorEvent();

                        sponsorEvent.FundraisingEventId = reader.GetInt32(0);
                        sponsorEvent.Title = reader.GetString(1);
                        sponsorEvent.StartDate = reader.GetDateTime(2);
                        sponsorEvent.InstitutionalEntityId = reader.GetInt32(3);
                        sponsorEvent.ContactType = reader.GetString(4);
                        sponsorEvent.CompanyName = reader.GetString(5);
                        sponsorEvent.Address = reader.GetString(6);

                        institutionalEntities.Add(sponsorEvent);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return institutionalEntities;
        }
    }
}
