using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IPostManager
    {
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-03-26
        /// Description: Retrieves all posts that an admin or moderator could see
        /// </summary>
        /// <returns></returns>
        List<PostVM> RetrieveAllPosts();
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-03-26
        /// Description: Retrieves active posts
        /// </summary>
        /// <returns></returns>
        List<PostVM> RetrieveActivePosts();
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-03-26
        /// Description: Adds a post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        int AddPost(Post post);
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-03-26
        /// Description: Edits a post
        /// </summary>
        /// <param name="post"></param>
        /// <param name="newPost"></param>
        /// <returns></returns>
        bool EditPost(Post post, Post newPost);
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-03-26
        /// Description: Retrieves a post by post id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        PostVM RetrievePostByPostId(int postId);

        /// <summary>
        /// Stephen Jaurigue
        /// 2023/04/06
        /// 
        /// Gets whether the current user has reported the post
        /// </summary>
        /// <param name="postId">user to check</param>
        /// <param name="userId">post to check</param>
        /// <returns>Whether the user has reported the post</returns>
        bool RetrieveUserPostReportedByPostIdAndUserId(int postId, int userId);

        /// <summary>
        /// Author: Matthew Meppelink
        /// Date: 2023-03-30
        /// Description: Updates a post by postId
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="newVisibility"></param>
        /// <param name="oldVisibility"></param>
        /// <returns>true or false; whether or not the update was a success</returns>
        bool EditPostVisibility(int postId, bool newVisibility, bool oldVisibility);

        /// <summary>
        /// Stephen Jaurigue
        /// 2023/04/13
        /// 
        /// Returns a List of all the reasons for a report
        /// </summary>
        /// <returns>List of all the reasons for a report</returns>
        List<ReportMessage> RetrieveReportMessages();

        /// <summary>
        /// Stephen Jaurigue
        /// 2023/04/13
        /// 
        /// Adds a new report to the database for the selected post
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userId"></param>
        /// <param name="reportMessageId"></param>
        /// <returns></returns>
        bool AddPostReport(int postId, int userId, int reportMessageId);

        /// <summary>
        /// Stephen Jaurigue
        /// 2023/04/13
        /// 
        /// Removes a report from the database for the selected post
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userId"></param>
        /// <param name="reportMessageId"></param>
        /// <returns></returns>
        bool RemovePostReport(int postId, int userId);
    }
}
