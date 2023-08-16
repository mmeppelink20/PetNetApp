/// <summary>
/// Zaid Rachman
/// Created: 2023/03/19
/// 
/// Item Accessor
/// </summary>
///
/// <remarks>
/// Nathan Zumsande
/// Updated: 2023/03/31
/// Added methods InsertItem, SelectAllCategories
/// InsertItemCategory, DeleteItemCategory, InsertCategory
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
    public class ItemAccessor : IItemAccessor
    {
        public int DeleteItemCategory(string itemId, string category)
        {
            int rows = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_delete_item_category";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //set parameters
            cmd.Parameters.AddWithValue("@ItemId", itemId);
            cmd.Parameters.AddWithValue("@CategoryId", category);

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

        public int InsertCategory(string categoryId)
        {
            int rows = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_category";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //set parameters
            cmd.Parameters.AddWithValue("@CategoryId", categoryId);

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

        public int InsertItem(string itemId)
        {
            int rows = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_item";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //set parameters
            cmd.Parameters.AddWithValue("@ItemId", itemId);

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

        public int InsertItemCategory(string itemId, string category)
        {
            int rows = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_item_category";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //set parameters
            cmd.Parameters.AddWithValue("@ItemId", itemId);
            cmd.Parameters.AddWithValue("@CategoryId", category);

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

        public List<string> SelectAllCategories()
        {
            List<string> categories = new List<string>();
            string category = "";

            //connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_all_categories";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                //open connection
                conn.Open();

                //execute
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    category = reader.GetString(0);
                    categories.Add(category);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //close connection
                conn.Close();
            }

            return categories;
        }

        public Item SelectItemByItemId(string ItemId)
        {
            Item item = new Item();
            List<string> categoryIds = new List<string>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            //command text
            var cmdText = "sp_select_categoryid_by_itemId";

            //command
            var cmd = new SqlCommand(cmdText, conn);

            //command type
            cmd.CommandType = CommandType.StoredProcedure;
            //parameter
            cmd.Parameters.Add("@ItemId", SqlDbType.NVarChar, 50);

            //Value
            cmd.Parameters["@ItemId"].Value = ItemId;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {


                    while (reader.Read())
                    {
                        item.ItemId = reader.GetString(0);
                        categoryIds.Add(reader.GetString(1));
                        item.CategoryId = categoryIds;
                    }


                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw new ArgumentException("Could not find Items", ex);
            }
            finally
            {
                conn.Close();
            }
            return item;

        }


    }
}
