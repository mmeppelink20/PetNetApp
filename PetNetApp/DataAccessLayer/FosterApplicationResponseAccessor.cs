// Created by Asa Armstrong
// Created on 2023/03/23
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
    public class FosterApplicationResponseAccessor : IFosterApplicationResponseAccessor
    {
        public int InsertFosterApplicationResponse(FosterApplicationResponse fosterApplicationResponse)
        {
            int rowsAffected = 0;

            var conn = new DBConnection().GetConnection();
            var cmdText = "sp_insert_foster_application_response_by_foster_application_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FosterApplicationId", fosterApplicationResponse.FosterApplicationId);
            cmd.Parameters.AddWithValue("@UsersId", fosterApplicationResponse.UsersId);
            cmd.Parameters.AddWithValue("@Approved", fosterApplicationResponse.Approved);

            //cmd.Parameters.AddWithValue("@FosterApplicationResponseNotes", fosterApplicationResponse.FosterApplicationResponseNotes);
            if (fosterApplicationResponse.FosterApplicationResponseNotes == null || fosterApplicationResponse.FosterApplicationResponseNotes.Length == 0)
            {
                cmd.Parameters.AddWithValue("@FosterApplicationResponseNotes", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FosterApplicationResponseNotes", fosterApplicationResponse.FosterApplicationResponseNotes);
            }

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

        public FosterApplicationResponseVM SelectFosterApplicationResponseByFosterApplicationId(int fosterApplicationId)
        {
            FosterApplicationResponseVM responseVM = new FosterApplicationResponseVM();

            var conn = new DBConnection().GetConnection();
            var cmdText = "sp_select_foster_application_response_by_foster_application_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FosterApplicationId", fosterApplicationId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    if(reader.Read())
                    {
                        responseVM.FosterApplicationResponseId = reader.GetInt32(0);
                        responseVM.FosterApplicationId = reader.GetInt32(1);
                        responseVM.UsersId = reader.GetInt32(2);
                        responseVM.Approved = reader.GetBoolean(3);
                        responseVM.FosterApplicationResponseDate = reader.GetDateTime(4);
                        responseVM.FosterApplicationResponseNotes = reader.IsDBNull(5) ? null : reader.GetString(5);
                        responseVM.ApplicantId = reader.GetInt32(6);
                        responseVM.FosterApplicantGivenName = reader.IsDBNull(7) ? null : reader.GetString(7);
                        responseVM.FosterApplicantFamilyName = reader.GetString(8);
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

            return responseVM;
        }

        public int UpdateFosterApplicationResponse(FosterApplicationResponse newFosterApplicationResponse, FosterApplicationResponse oldFosterApplicationResponse)
        {
            int rowsAffected = 0;

            var conn = new DBConnection().GetConnection();
            var cmdText = "sp_update_foster_application_response";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FosterApplicationResponseId", oldFosterApplicationResponse.FosterApplicationResponseId);
            cmd.Parameters.AddWithValue("@FosterApplicationId", oldFosterApplicationResponse.FosterApplicationId);
            cmd.Parameters.AddWithValue("@UsersId", oldFosterApplicationResponse.UsersId);

            cmd.Parameters.AddWithValue("@OldApproved", oldFosterApplicationResponse.Approved);
            //cmd.Parameters.AddWithValue("@OldFosterApplicationResponseNotes", oldFosterApplicationResponse.FosterApplicationResponseNotes);
            if(oldFosterApplicationResponse.FosterApplicationResponseNotes == null || oldFosterApplicationResponse.FosterApplicationResponseNotes.Length == 0)
            {
                cmd.Parameters.AddWithValue("@OldFosterApplicationResponseNotes", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OldFosterApplicationResponseNotes", oldFosterApplicationResponse.FosterApplicationResponseNotes);
            }

            cmd.Parameters.AddWithValue("@NewApproved", newFosterApplicationResponse.Approved);
            //cmd.Parameters.AddWithValue("@NewFosterApplicationResponseNotes", newFosterApplicationResponse.FosterApplicationResponseNotes);
            if(newFosterApplicationResponse.FosterApplicationResponseNotes == null || newFosterApplicationResponse.FosterApplicationResponseNotes.Length == 0)
            {
                cmd.Parameters.AddWithValue("@NewFosterApplicationResponseNotes", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NewFosterApplicationResponseNotes", newFosterApplicationResponse.FosterApplicationResponseNotes);
            }

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rowsAffected;
        }
    }
}
