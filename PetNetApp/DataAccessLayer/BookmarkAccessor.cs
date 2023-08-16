/// <summary>
/// Alexis Oetken
/// Created: 2023/04/20
/// </summary>
///
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/23
/// 
/// Final QA
/// </remarks>
using DataAccessLayerInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class BookmarkAccessor : IBookmarkAccessor
    {
        public int InsertBookmark(int UserId, int AnimalId)
        {
            int rows = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_bookmark_animal";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //set parameters
            cmd.Parameters.AddWithValue("@UsersId", UserId);
            cmd.Parameters.AddWithValue("@AnimalId", AnimalId);

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

        public int RemoveBookmark(int UserId, int AnimalId)
        {

            int rows = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_delete_animal_bookmark";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //set parameters
            cmd.Parameters.AddWithValue("@UsersId", UserId);
            cmd.Parameters.AddWithValue("@AnimalId", AnimalId);

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

        public List<Bookmark> RetrieveAllBookmarks(int UserId)
        {
            List<Bookmark> bookmarks = new List<Bookmark>();
            //connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            //command text
            var cmdText = "sp_select_all_user_bookmarks";

            //command
            var cmd = new SqlCommand(cmdText, conn);

            //command type
            cmd.CommandType = CommandType.StoredProcedure;
            //parameter
            cmd.Parameters.AddWithValue("@UsersId", UserId);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var bookmark = new Bookmark();
                        bookmark.AnimalId = reader.GetInt32(0);
                        bookmark.AnimalName = reader.GetString(1);
                        bookmarks.Add(bookmark);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw new ArgumentException("Could not receive Bookmark Information", ex);
            }
            finally
            {
                conn.Close();
            }
            return bookmarks;
        }
    }
}

