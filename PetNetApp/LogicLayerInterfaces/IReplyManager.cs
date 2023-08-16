using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IReplyManager
    {
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-03-26
        /// Description: Retrieves all replies that an admin or moderator could see by post id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        List<ReplyVM> RetrieveAllRepliesByPostId(int postId);
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-03-26
        /// Description: Retrieves active replies 
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        List<ReplyVM> RetrieveActiveRepliesByPostId(int postId);
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-03-26
        /// Description: Retrieves count of replies for an admin or moderator
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        int RetrieveCountRepliesByPostId(int postId);
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-03-26
        /// Description: Retrieves count of active replies
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        int RetrieveCountActiveRepliesByPostId(int postId);
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-04-01
        /// Description: Adds a reply
        /// </summary>
        /// <param name="reply"></param>
        /// <returns></returns>
        int AddReply(Reply reply);
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-04-01
        /// Description: Edits a reply
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="newReply"></param>
        /// <returns></returns>
        bool EditReply(Reply reply, Reply newReply);
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-04-01
        /// Description: Retrieves reply by reply id
        /// </summary>
        /// <param name="replyId"></param>
        /// <returns></returns>
        ReplyVM RetrieveReplyByReplyId(int replyId);

        /// <summary>
        /// Author: Andrew Cromwell
        /// Date: 2023-04-14
        /// Description: Retrieves reply by reply id
        /// </summary>
        /// <param name="reply"></param>
        /// <returns></returns>
        bool EditReplyVisibilityByReplyId(ReplyVM reply);

        /// <summary>
        /// Stephen Jaurigue
        /// 2023/04/13
        /// 
        /// Adds a new report to the database for the selected post
        /// </summary>
        /// <param name="replyId"></param>
        /// <param name="userId"></param>
        /// <param name="reportMessageId"></param>
        /// <returns></returns>
        bool AddReplyReport(int replyId, int userId, int reportMessageId);

        /// <summary>
        /// Stephen Jaurigue
        /// 2023/04/13
        /// 
        /// Removes a report from the database for the selected post
        /// </summary>
        /// <param name="replyId"></param>
        /// <param name="userId"></param>
        /// <param name="reportMessageId"></param>
        /// <returns></returns>
        bool RemoveReplyReport(int replyId, int userId);


        /// <summary>
        /// Stephen Jaurigue
        /// 2023/04/06
        /// 
        /// Gets whether the current user has reported the post
        /// </summary>
        /// <param name="replyId">user to check</param>
        /// <param name="userId">post to check</param>
        /// <returns>Whether the user has reported the post</returns>
        bool RetrieveUserReplyReportedByReplyIdAndUserId(int replyId, int userId);
    }
}
