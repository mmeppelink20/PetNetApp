using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IPostAccessor
    {
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-03-26
        /// Description: Selects all posts that an admin or moderator could see
        /// </summary>
        /// <returns></returns>
        List<PostVM> SelectAllPosts();
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-03-26
        /// Description: Selects all actives post
        /// </summary>
        /// <returns></returns>
        List<PostVM> SelectActivePosts();
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-03-26
        /// Description: Inserts a post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        int InsertPost(Post post);
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-03-26
        /// Description: Updates a post
        /// </summary>
        /// <param name="post"></param>
        /// <param name="newPost"></param>
        /// <returns></returns>
        int UpdatePost(Post post, Post newPost);
        /// <summary>
        /// Author: Gwen Arman
        /// Date: 2023-03-26
        /// Description: Selects a post by post id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        PostVM SelectPostByPostId(int postId);

        /// <summary>
        /// Stephen Jaurigue
        /// 2023/04/13
        /// 
        /// Selects the count of reports this user has on this post
        /// </summary>
        /// <param name="postId">the post</param>
        /// <param name="userId"> the user</param>
        /// <returns>count of reports user has on post</returns>
        int SelectUserPostReportedByPostIdandUserId(int postId, int userId);

        /// <summary>
        /// Author: Matthew Meppelink
        /// Date: 2023-03-30
        /// Description: Updates a post visibility to true or false
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="newVisibility"></param>
        /// <param name="oldVisibility"></param>
        /// <returns></returns>
        int UpdatePostVisibility(int postId, bool newVisibility, bool oldVisibility);

        /// <summary>
        /// Stephen Jaurigue
        /// 2023/04/13
        /// 
        /// Gets a List of all the different messages someone can report for
        /// </summary>
        /// <returns></returns>
        List<ReportMessage> SelectReportMessages();



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
        int InsertPostReport(int postId, int userId, int reportMessageId);

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
        int DeletePostReport(int postId, int userId);
    }
}
