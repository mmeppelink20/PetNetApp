using DataAccessLayer;
using DataAccessLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayerInterfaces;
using DataObjects;

namespace LogicLayer
{
    public class PostManager : IPostManager
    {
        private IPostAccessor postAccessor = null;
        public PostManager()
        {
            postAccessor = new PostAccessor();
        }
        public PostManager(IPostAccessor pa)
        {
            postAccessor = pa;
        }

        public int AddPost(Post post)
        {
            int newId = 0;
            try
            {
                newId = postAccessor.InsertPost(post);
                if (newId == 0)
                {
                    throw new Exception("Accesor method returned 0");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add a post", ex);
            }
            return newId;
        }

        public bool EditPost(Post post, Post newPost)
        {
            int rowAffected = 0;
            try
            {
                rowAffected = postAccessor.UpdatePost(post, newPost);
                if (rowAffected == 0)
                {
                    throw new Exception("Update failed");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Post failed to update", ex);
            }
            return rowAffected == 1;
        }

        public List<PostVM> RetrieveActivePosts()
        {
            List<PostVM> posts = new List<PostVM>();

            try
            {
                posts = postAccessor.SelectActivePosts();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Posts not found", ex);
            }

            return posts;
        }

        public List<PostVM> RetrieveAllPosts()
        {
            List<PostVM> posts = new List<PostVM>();

            try
            {
                posts = postAccessor.SelectAllPosts();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Posts not found", ex);
            }

            return posts;
        }

        public PostVM RetrievePostByPostId(int postId)
        {
            PostVM post = new PostVM();

            try
            {
                post = postAccessor.SelectPostByPostId(postId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Post not found", ex);
            }

            return post;
        }

        public bool RetrieveUserPostReportedByPostIdAndUserId(int postId, int userId)
        {
            bool reported = false;

            try
            {
                reported = postAccessor.SelectUserPostReportedByPostIdandUserId(postId, userId) != 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to check if the post has been reported", ex);
            }

            return reported;
        }

        public bool EditPostVisibility(int postId, bool newVisibility, bool oldVisibility)
        {
            bool result = false;
            try
            {
                result = 1 == postAccessor.UpdatePostVisibility(postId, newVisibility, oldVisibility);
                if (!result)
                {
                    throw new ApplicationException("deletion of post failed");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("deletion of post failed", ex);
            }
            return result;
        }

        public List<ReportMessage> RetrieveReportMessages()
        {
            List<ReportMessage> messages = null;
            try
            {
                messages = postAccessor.SelectReportMessages();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to load report reasons", ex);
            }
            return messages;
        }

        public bool AddPostReport(int postId, int userId, int reportMessageId)
        {
            try
            {
                return postAccessor.InsertPostReport(postId, userId, reportMessageId) == 1;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to report the post", ex);
            }
        }

        public bool RemovePostReport(int postId, int userId)
        {
            try
            {
                return postAccessor.DeletePostReport(postId, userId) == 1;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to unreport the post", ex);
            }
        }
    }
}
