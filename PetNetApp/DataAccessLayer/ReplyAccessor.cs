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
    public class ReplyAccessor : IReplyAccessor
    {
        public int InsertReply(Reply reply)
        {
            int newId = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_reply";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ReplyAuthor", SqlDbType.NVarChar, 25).Value = reply.ReplyAuthor;
            cmd.Parameters.Add("@ReplyContent", SqlDbType.NVarChar, 250).Value = reply.ReplyContent;
            cmd.Parameters.Add("@ReplyDate", SqlDbType.NVarChar, 25).Value = reply.ReplyDate;
            cmd.Parameters.Add("@PostId", SqlDbType.NVarChar, 25).Value = reply.PostId;


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

        public List<ReplyVM> SelectActiveRepliesByPostId(int postId)
        {
            List<ReplyVM> replies = new List<ReplyVM>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_active_replies_by_postid";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PostId", SqlDbType.NVarChar, 25).Value = postId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ReplyVM reply = new ReplyVM();

                        reply.ReplyId = reader.GetInt32(0);
                        reply.PostId = reader.GetInt32(1);
                        reply.ReplyAuthor = reader.GetInt32(2);
                        reply.ReplyContent = reader.GetString(3);
                        reply.ReplyDate = reader.GetDateTime(4);
                        reply.ReplyVisibility = reader.GetBoolean(5);
                        reply.ReplierGivenName = reader.GetString(6);
                        reply.ReplierFamilyName = reader.GetString(7);

                        replies.Add(reply);
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
            return replies;
        }

        public List<ReplyVM> SelectAllRepliesByPostId(int postId)
        {
            List<ReplyVM> replies = new List<ReplyVM>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_all_replies_by_postid";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PostId", SqlDbType.NVarChar, 25).Value = postId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ReplyVM reply = new ReplyVM();

                        reply.ReplyId = reader.GetInt32(0);
                        reply.PostId = reader.GetInt32(1);
                        reply.ReplyAuthor = reader.GetInt32(2);
                        reply.ReplyContent = reader.GetString(3);
                        reply.ReplyDate = reader.GetDateTime(4);
                        reply.ReplyVisibility = reader.GetBoolean(5);
                        reply.ReplierGivenName = reader.GetString(6);
                        reply.ReplierFamilyName = reader.GetString(7);

                        replies.Add(reply);
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
            return replies;
        }

        public int SelectCountActiveRepliesByPostId(int postId)
        {
            int repliesCount = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_count_active_replies_by_postId";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PostId", SqlDbType.NVarChar, 25).Value = postId;

            try
            {
                conn.Open();
                repliesCount = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return repliesCount;
        }

        public int SelectCountRepliesByPostId(int postId)
        {
            int repliesCount = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_count_replies_by_postId";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PostId", SqlDbType.NVarChar, 25).Value = postId;

            try
            {
                conn.Open();
                repliesCount = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return repliesCount;
        }

        public ReplyVM SelectReplyByReplyId(int replyId)
        {
            ReplyVM reply = null;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_reply_by_replyId";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ReplyId", replyId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        reply = new ReplyVM();

                        reply.ReplyId = reader.GetInt32(0);
                        reply.ReplyAuthor = reader.GetInt32(1);
                        reply.ReplyContent = reader.GetString(2);
                        reply.ReplyDate = reader.GetDateTime(3);
                        reply.ReplyVisibility = reader.GetBoolean(4);
                        reply.ReplierGivenName = reader.GetString(5);
                        reply.ReplierFamilyName = reader.GetString(6);
                        reply.PostId = reader.GetInt32(7);
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
            return reply;
        }

        public int UpdateReply(Reply reply, Reply newReply)
        {
            int rowsAffected = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_update_reply";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ReplyId", SqlDbType.NVarChar, 25).Value = reply.ReplyId;
            cmd.Parameters.Add("@ReplyContent", SqlDbType.NVarChar, 250).Value = newReply.ReplyContent;
            cmd.Parameters.Add("@ReplyDate", SqlDbType.NVarChar, 25).Value = newReply.ReplyDate;
            cmd.Parameters.Add("@OldReplyContent", SqlDbType.NVarChar, 250).Value = reply.ReplyContent;


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

        public int UpdateReplyVisibilityByReplyId(ReplyVM reply)
        {
            int rowsAffected = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_update_reply_visibility_by_replyid";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@replyid", SqlDbType.Int).Value = reply.ReplyId;
            cmd.Parameters.Add("@reply_content", SqlDbType.NVarChar, 250).Value = reply.ReplyContent;
            cmd.Parameters.Add("@reply_visiblility", SqlDbType.Bit).Value = reply.ReplyVisibility;

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


        public int InsertReplyReport(int replyId, int userId, int reportMessageId)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_reply_report";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ReplyReporter", SqlDbType.Int).Value = userId;
            cmd.Parameters.Add("@ReplyId", SqlDbType.Int).Value = replyId;
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


        public int SelectUserReplyReportedByReplyIdandUserId(int replyId, int userId)
        {
            int reportedCount = 0;

            var conn = new DBConnection().GetConnection();
            var cmd = new SqlCommand("sp_select_user_reply_reported_by_replyId_and_userId", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ReplyId", SqlDbType.Int).Value = replyId;
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

        public int DeleteReplyReport(int replyId, int userId)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_delete_reply_report";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ReplyReporter", SqlDbType.Int).Value = userId;
            cmd.Parameters.Add("@ReplyId", SqlDbType.Int).Value = replyId;


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
