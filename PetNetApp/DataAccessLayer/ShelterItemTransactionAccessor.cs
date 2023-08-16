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
    public class ShelterItemTransactionAccessor : IShelterItemTransactionAccessor
    {
        public List<ShelterItemTransactionVM> SelcetShelterItemTransactionByShelterId(int shelterId)
        {
            List<ShelterItemTransactionVM> shelterItemTransactions = new List<ShelterItemTransactionVM>();

            var conn = new DBConnection().GetConnection();
            SqlCommand cmd = new SqlCommand("sp_select_shelteritemtransaction_by_shelter_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@shelterid", SqlDbType.Int).Value = shelterId;

            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            shelterItemTransactions.Add(new ShelterItemTransactionVM()
                            {
                                ShelterItemTransactionId = reader.GetInt32(0),
                                ShelterId = reader.GetInt32(1),
                                ItemId = reader.GetString(2),
                                ChangedByUsersId = reader.GetInt32(3),
                                InventoryChangeReasonId = reader.GetString(4),
                                QuantityIncrement = reader.GetInt32(5),
                                DateChanged = reader.GetDateTime(6),
                                ShelterName = reader.GetString(7),
                                GivenName = reader.GetString(8),
                                FamilyName = reader.GetString(9),
                                ReasonDescription = reader.GetString(10)
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

            return shelterItemTransactions;
        }

        public int InsertItemTransaction(ShelterItemTransaction transaction)
        {
            int rows = 0;

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_item_transaction";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            /*
            @ShelterId                  int,
            @ItemId                     nvarchar(50),
	        @ChangedByUsersId           int,
            @InventoryChangeReasonId    nvarchar(50),
	        @QuantityIncrement          int,
            @DateChanged                datetime
            */
            cmd.Parameters.AddWithValue("@ShelterId", transaction.ShelterId);
            cmd.Parameters.AddWithValue("@ItemId", transaction.ItemId);
            cmd.Parameters.AddWithValue("@ChangedByUsersId", transaction.ChangedByUsersId);
            cmd.Parameters.AddWithValue("@InventoryChangeReasonId", transaction.InventoryChangeReasonId);
            cmd.Parameters.AddWithValue("@QuantityIncrement", transaction.QuantityIncrement);
            cmd.Parameters.AddWithValue("@DateChanged", DateTime.Now);

            try
            {
                // open connection
                conn.Open();

                // execute
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // close connection
                conn.Close();
            }

            return rows;
        }

    }
}
