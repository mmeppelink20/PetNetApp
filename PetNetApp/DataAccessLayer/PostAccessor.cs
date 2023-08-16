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
    public class PostAccessor : IPostAccessor
    {
        public int InsertPost(Post post)
        {
            int newId = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_post";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PostAuthor", SqlDbType.NVarChar, 25).Value = post.PostAuthor;
            cmd.Parameters.Add("@PostContent", SqlDbType.NVarChar, 250).Value = post.PostContent;
            cmd.Parameters.Add("@PostDate", SqlDbType.NVarChar, 25).Value = post.PostDate;
            

            try
            {
                conn.Open();
                newId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return newId;
        }

        public List<PostVM> SelectActivePosts()
        {
            List<PostVM> posts = new List<PostVM>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_active_posts";
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
                        PostVM post = new PostVM();

                        post.PostId = reader.GetInt32(0);
                        post.PostAuthor = reader.GetInt32(1);
                        post.PostContent = reader.GetString(2);
                        post.PostDate = reader.GetDateTime(3);
                        post.PostVisibility = reader.GetBoolean(4);
                        post.PosterGivenName = reader.GetString(5);
                        post.PosterFamilyName = reader.GetString(6);

                        posts.Add(post);
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
            return posts;
        }

        public List<PostVM> SelectAllPosts()
        {
            List<PostVM> posts = new List<PostVM>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_all_posts";
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
                        PostVM post = new PostVM();

                        post.PostId = reader.GetInt32(0);
                        post.PostAuthor = reader.GetInt32(1);
                        post.PostContent = reader.GetString(2);
                        post.PostDate = reader.GetDateTime(3);
                        post.PostVisibility = reader.GetBoolean(4);
                        post.PosterGivenName = reader.GetString(5);
                        post.PosterFamilyName = reader.GetString(6);

                        posts.Add(post);
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
            return posts;
        }

        public PostVM SelectPostByPostId(int postId)
        {
            PostVM post = null;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_post_by_postId";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PostId", postId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        post = new PostVM();

                        post.PostId = reader.GetInt32(0);
                        post.PostAuthor = reader.GetInt32(1);
                        post.PostContent = reader.GetString(2);
                        post.PostDate = reader.GetDateTime(3);
                        post.PostVisibility = reader.GetBoolean(4);
                        post.PosterGivenName = reader.GetString(5);
                        post.PosterFamilyName = reader.GetString(6);

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
            return post;
        }


        public int SelectUserPostReportedByPostIdandUserId(int postId, int userId)
        {
            int reportedCount = 0;

            var conn = new DBConnection().GetConnection();
            var cmd = new SqlCommand("sp_select_user_post_reported_by_postId_and_userId", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PostId", SqlDbType.Int).Value = postId;
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

            try
            {
                conn.Open();
                    reportedCount = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return reportedCount;
        }

        public int UpdatePost(Post post, Post newPost)
        {
            int rowsAffected = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_update_post";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PostId", SqlDbType.NVarChar, 25).Value = post.PostId;
            cmd.Parameters.Add("@PostContent", SqlDbType.NVarChar, 250).Value = newPost.PostContent;
            cmd.Parameters.Add("@PostDate", SqlDbType.NVarChar, 25).Value = newPost.PostDate;
            cmd.Parameters.Add("@OldPostContent", SqlDbType.NVarChar, 250).Value = post.PostContent;


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

        public int UpdatePostVisibility(int postId, bool newVisibility, bool oldVisibility)
        {
            int rowsAffected = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_update_post_visibility";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PostId", postId);
            cmd.Parameters.AddWithValue("@NewPostVisibility", newVisibility);
            cmd.Parameters.AddWithValue("@OldPostVisibility", oldVisibility);

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

        public List<ReportMessage> SelectReportMessages()
        {
            List<ReportMessage> messages = new List<ReportMessage>();

            var conn = new DBConnection().GetConnection();

            var cmdText = "sp_select_report_messages";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            messages.Add(new ReportMessage()
                            {
                                ReportMessageId = reader.GetInt32(0),
                                ReportMessageDescription = reader.GetString(1)
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

            return messages;
        }

        public int InsertPostReport(int postId, int userId, int reportMessageId)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_post_report";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PostReporter", SqlDbType.Int).Value = userId;
            cmd.Parameters.Add("@PostId", SqlDbType.Int).Value = postId;
            cmd.Parameters.Add("@ReportMessageId", SqlDbType.Int).Value = reportMessageId;


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

        public int DeletePostReport(int postId, int userId)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_delete_post_report";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PostReporter", SqlDbType.Int).Value = userId;
            cmd.Parameters.Add("@PostId", SqlDbType.Int).Value = postId;


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
