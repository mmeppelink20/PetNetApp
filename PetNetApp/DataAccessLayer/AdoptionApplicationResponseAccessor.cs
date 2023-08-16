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
    public class AdoptionApplicationResponseAccessor : IAdoptionApplicationResponseAccessor
    {
        public int InsertAdoptionApplicationResponseByAdoptionApplicationId(AdoptionApplicationResponseVM adoptionApplicationResponseVM)
        {
            int result = 0;

            DBConnection factory = new DBConnection();
            var conn = factory.GetConnection();
            var cmdText = "sp_add_adoption_application_response";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AdoptionApplicationId", adoptionApplicationResponseVM.AdoptionApplicationId);
            cmd.Parameters.AddWithValue("@UsersId", adoptionApplicationResponseVM.ResponderUserId);
            cmd.Parameters.AddWithValue("@Approved", adoptionApplicationResponseVM.Approved);
            cmd.Parameters.AddWithValue("@AdoptionApplicationResponseDate", adoptionApplicationResponseVM.AdoptionApplicationResponseDate);
            cmd.Parameters.AddWithValue("@AdoptionApplicationResponseNotes", adoptionApplicationResponseVM.AdoptionApplicationResponseNotes);

            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
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

        public AdoptionApplicationResponseVM SelectAdoptionApplicationResponseByAdoptionApplicationId(int adoptionApplicationId)
        {
            throw new NotImplementedException();
        }

        public int UpdateAdoptionApplicationResponse(AdoptionApplicationResponse newAdoptionApplicationResponse, AdoptionApplicationResponse oldAdoptionApplicationResponse)
        {
            int rowsAffected = 0;

            var conn = new DBConnection().GetConnection();
            var cmdText = "sp_update_adoption_application_response";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AdoptionApplicationResponseId", oldAdoptionApplicationResponse.AdoptionApplicationResponseId);
            cmd.Parameters.AddWithValue("@AdoptionApplicationId", oldAdoptionApplicationResponse.AdoptionApplicationId);
            cmd.Parameters.AddWithValue("@UsersId", oldAdoptionApplicationResponse.ResponderUserId);

            cmd.Parameters.AddWithValue("@OldApproved", oldAdoptionApplicationResponse.Approved);
            //cmd.Parameters.AddWithValue("@OldAdoptionApplicationResponseNotes", oldAdoptionApplicationResponse.AdoptionApplicationResponseNotes);
            if (oldAdoptionApplicationResponse.AdoptionApplicationResponseNotes == null || oldAdoptionApplicationResponse.AdoptionApplicationResponseNotes.Length == 0)
            {
                cmd.Parameters.AddWithValue("@OldAdoptionApplicationResponseNotes", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OldAdoptionApplicationResponseNotes", oldAdoptionApplicationResponse.AdoptionApplicationResponseNotes);
            }

            cmd.Parameters.AddWithValue("@NewApproved", newAdoptionApplicationResponse.Approved);
            //cmd.Parameters.AddWithValue("@NewAdoptionApplicationResponseNotes", newAdoptionApplicationResponse.AdoptionApplicationResponseNotes);
            if (newAdoptionApplicationResponse.AdoptionApplicationResponseNotes == null || newAdoptionApplicationResponse.AdoptionApplicationResponseNotes.Length == 0)
            {
                cmd.Parameters.AddWithValue("@NewAdoptionApplicationResponseNotes", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NewAdoptionApplicationResponseNotes", newAdoptionApplicationResponse.AdoptionApplicationResponseNotes);
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
