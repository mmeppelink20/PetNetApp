using DataAccessLayer;
using DataAccessLayerInterfaces;
using DataObjects;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class ReplyManager : IReplyManager
    {
        private IReplyAccessor replyAccessor = null;
        public ReplyManager()
        {
            replyAccessor = new ReplyAccessor();
        }
        public ReplyManager(IReplyAccessor ra)
        {
            replyAccessor = ra;
        }

        public int AddReply(Reply reply)
        {
            int newId = 0;
            try
            {
                newId = replyAccessor.InsertReply(reply);
                if (newId == 0)
                {
                    throw new Exception("Accesor method returned 0");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add a reply", ex);
            }
            return newId;
        }

        public bool EditReply(Reply reply, Reply newReply)
        {
            int rowAffected = 0;
            try
            {
                rowAffected = replyAccessor.UpdateReply(reply, newReply);
                if (rowAffected == 0)
                {
                    throw new Exception("Update failed");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Reply failed to update", ex);
            }
            return rowAffected == 1;
        }

        public bool EditReplyVisibilityByReplyId(ReplyVM reply)
        {
            bool result = false;
            try
            {
                int rows = replyAccessor.UpdateReplyVisibilityByReplyId(reply);
                if(rows == 1)
                {
                    result = true;
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Faild to delete the reply.", ex);
            }
            return result;
        }

        public List<ReplyVM> RetrieveActiveRepliesByPostId(int postId)
        {
            List<ReplyVM> replies = new List<ReplyVM>();

            try
            {
                replies = replyAccessor.SelectActiveRepliesByPostId(postId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Replies not found", ex);
            }

            return replies;
        }

        public List<ReplyVM> RetrieveAllRepliesByPostId(int postId)
        {
            List<ReplyVM> replies = new List<ReplyVM>();

            try
            {
                replies = replyAccessor.SelectAllRepliesByPostId(postId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Replies not found", ex);
            }

            return replies;
        }

        public int RetrieveCountActiveRepliesByPostId(int postId)
        {
            int count = 0;

            try
            {
                count = replyAccessor.SelectCountActiveRepliesByPostId(postId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not retrieve a count", ex);
            }
            return count;
        }

        public int RetrieveCountRepliesByPostId(int postId)
        {
            int count = 0;

            try
            {
                count = replyAccessor.SelectCountActiveRepliesByPostId(postId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not retrieve a count", ex);
            }
            return count;
        }

        public ReplyVM RetrieveReplyByReplyId(int replyId)
        {
            ReplyVM reply = new ReplyVM();

            try
            {
                reply = replyAccessor.SelectReplyByReplyId(replyId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Post not found", ex);
            }

            return reply;
        }

        public bool AddReplyReport(int replyId, int userId, int reportMessageId)
        {
            try
            {
                return replyAccessor.InsertReplyReport(replyId, userId, reportMessageId) == 1;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to report the reply", ex);
            }
        }

        public bool RemoveReplyReport(int replyId, int userId)
        {
            try
            {
                return replyAccessor.DeleteReplyReport(replyId, userId) == 1;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to unreport the reply", ex);
            }
        }

        public bool RetrieveUserReplyReportedByReplyIdAndUserId(int replyId, int userId)
        {
            bool reported = false;

            try
            {
                reported = replyAccessor.SelectUserReplyReportedByReplyIdandUserId(replyId, userId) != 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to check if the post has been reported", ex);
            }

            return reported;
        }
    }
}
