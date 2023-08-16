/// <summary>
/// Brian Collum
/// Created: 2023/03/23
/// 
/// Library Accessor class for building acessing Library information from the DB
/// Also for building and running CRUD functions on Library objects and data
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
    public class LibraryAccessor : ILibraryAccessor
    {
        public List<Item> RetrieveLibraryItemList()
        {
            {
                // Initialize the list
                List<Item> libraryItemList = new List<Item>();
                // ADO.NET needs a connection
                DBConnection connectionFactory = new DBConnection();
                var conn = connectionFactory.GetConnection();
                // Command Text
                var cmdText = "sp_select_all_library_items";
                // Command
                var cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                // No Parameters

                try
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = Convert.ToString(reader["ItemID"]);
                            string concatenatedTags = Convert.ToString(reader["Tags"]);
                            if (name.IsValidNVarcharX(50))
                            {
                                List<string> tagList = new List<string>();
                                string[] tags = concatenatedTags.Split('|');
                                foreach (string tag in tags)
                                {
                                    if (tag.IsValidNVarcharX(50))
                                    {
                                        tagList.Add(tag);
                                    }
                                }
                                libraryItemList.Add(new Item { ItemId = name, CategoryId = tagList });
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
                if (libraryItemList.Count == 0)
                {
                    libraryItemList.Add(new Item { ItemId = "Failed to load Library", CategoryId = new List<string>() });
                }
                return libraryItemList;
            }
        }
    }
}
