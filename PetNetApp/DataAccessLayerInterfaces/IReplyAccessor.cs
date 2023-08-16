using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IReplyAccessor
    {
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-03-26
        /// Description: Selects all replies that an admin or moderator could see
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        List<ReplyVM> SelectAllRepliesByPostId(int postId);
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-03-26
        /// Description: Selects active replies
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        List<ReplyVM> SelectActiveRepliesByPostId(int postId);
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-03-26
        /// Description: Selects a count of replies for an admin or moderator
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        int SelectCountRepliesByPostId(int postId);
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-03-26
        /// Description: Selects active replies
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        int SelectCountActiveRepliesByPostId(int postId);
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-04-01
        /// Description: Inserts a reply
        /// </summary>
        /// <param name="reply"></param>
        /// <returns></returns>
        int InsertReply(Reply reply);
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-04-01
        /// Description: Updates a reply
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="newReply"></param>
        /// <returns></returns>
        int UpdateReply(Reply reply, Reply newReply);
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-04-01
        /// Description: Selects reply by replyId
        /// </summary>
        /// <param name="replyid"></param>
        /// <returns></returns>
        ReplyVM SelectReplyByReplyId(int replyId);
        /// <summary>
        /// Author: Andrew Cromwell
        /// Date: 2023-04-14
        /// 
        /// Description: Makes it so that a reply will no longer be visible
        /// </summary>
        /// <param name="reply">The reply to update</param>
        /// <returns>int rows affected</returns>
        int UpdateReplyVisibilityByReplyId(ReplyVM reply);

        /// <summary>
        /// Stephen Jaurigue
        /// 2023/04/13
        /// 
        /// Adds a new report to the database for the selected reply
        /// </summary>
        /// <param name="replyId"></param>
        /// <param name="userId"></param>
        /// <param name="reportMessageId"></param>
        /// <returns></returns>
        int InsertReplyReport(int replyId, int userId, int reportMessageId);

        /// <summary>
        /// Stephen Jaurigue
        /// 2023/04/13
        /// 
        /// Removes a report from the database for the selected reply
        /// </summary>
        /// <param name="replyId"></param>
        /// <param name="userId"></param>
        /// <param name="reportMessageId"></param>
        /// <returns></returns>
        int DeleteReplyReport(int replyId, int userId);

        /// <summary>
        /// Stephen Jaurigue
        /// 2023/04/13
        /// 
        /// Selects the count of reports this user has on this post
        /// </summary>
        /// <param name="replyId">the post</param>
        /// <param name="userId"> the user</param>
        /// <returns>count of reports user has on post</returns>
        int SelectUserReplyReportedByReplyIdandUserId(int replyId, int userId);
    }
}
