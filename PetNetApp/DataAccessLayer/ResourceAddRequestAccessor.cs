/// <summary>
/// Andrew Schneider
/// Created: 2023/03/30
/// 
/// Access layer for ResourceAddRequest
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class ResourceAddRequestAccessor : IResourceAddRequestAccessor
    {
        public int InsertResourceAddRequest(ResourceAddRequest resourceAddRequest)
        {
            int rows = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_ResourceAddRequest";

            //command
            var cmd = new SqlCommand(cmdText, conn);

            // type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@shelterid", resourceAddRequest.ShelterId);
            cmd.Parameters.AddWithValue("@usersid", resourceAddRequest.UsersId);
            cmd.Parameters.AddWithValue("@title", resourceAddRequest.Title);
            cmd.Parameters.AddWithValue("@note", resourceAddRequest.Note);
            cmd.Parameters.AddWithValue("@active", resourceAddRequest.Active);

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
    

        public List<ResourceAddRequest> SelectActiveResourceAddRequestsByShelterId(int shelterId)
        {
            List<ResourceAddRequest> resourceAddRequests = new List<ResourceAddRequest>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_active_resource_add_requests_by_shelter_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ShelterId", shelterId);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var resourceAddRequest = new ResourceAddRequest();

                        resourceAddRequest.ResourceAddRequestId = reader.GetInt32(0);
                        resourceAddRequest.UsersId = reader.GetInt32(1);
                        resourceAddRequest.Title = reader.GetString(2);
                        resourceAddRequest.Note = reader.GetString(3);
                        resourceAddRequest.Active = reader.GetBoolean(4);
                        resourceAddRequest.ShelterId = reader.GetInt32(5);
                        resourceAddRequests.Add(resourceAddRequest);
                    }
                }
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }

            return resourceAddRequests;
        }

        public int UpdateResourceAddRequestActiveField(ResourceAddRequest oldResourceAddRequest, ResourceAddRequest newResourceAddRequest)
        {
            int rows = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_update_resource_add_request_active_field";

            //command
            var cmd = new SqlCommand(cmdText, conn);

            // type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@ResourceAddRequestId", oldResourceAddRequest.ResourceAddRequestId);
            cmd.Parameters.AddWithValue("@ShelterId", oldResourceAddRequest.ShelterId);
            cmd.Parameters.AddWithValue("@OldActive", oldResourceAddRequest.Active);
            cmd.Parameters.AddWithValue("@NewActive", newResourceAddRequest.Active);

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
