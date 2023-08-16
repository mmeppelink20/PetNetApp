using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;

namespace DataAccessLayerFakes
{
    public class PostAccessorFake : IPostAccessor
    {
        List<PostVM> fakePostVMs = new List<PostVM>();
        List<Post> fakePosts = new List<Post>();
        List<Tuple<int, int>> reports = new List<Tuple<int, int>>();
        public PostAccessorFake()
        {

            reports.Add(new Tuple<int, int>(1, 1));
            fakePostVMs.Add(new PostVM
            {
                PostId = 1,
                PostAuthor = 1,
                PostContent = "Post Contents",
                PostDate = DateTime.Today,
                PostVisibility = true,
                PosterGivenName = "Gwen",
                PosterFamilyName = "Arman",
                
                Replies = new List<ReplyVM>
                {
                    new ReplyVM()
                    {
                        ReplyId = 1,
                        PostId = 1,
                        ReplyAuthor = 2,
                        ReplyContent = "This is supposed to test what a reply with a very long reply content message would look like. Hopefully this is displayed in a nice clean manner. Otherwise, Gwen will need to fix that because an ugly" +
                        "reply is no bueno. If it is displayed well, good job Gwen.. aka myself. Nice Nice Nice Nice" + "This is supposed to test what a reply with a very long reply content message would look like. Hopefully this is displayed in a nice clean manner. Otherwise, Gwen will need to fix that because an ugly" +
                        "reply is no bueno. If it is displayed well, good job Gwen.. aka myself. Nice Nice Nice Nice" + "This is supposed to test what a reply with a very long reply content message would look like. Hopefully this is displayed in a nice clean manner. Otherwise, Gwen will need to fix that because an ugly" +
                        "reply is no bueno. If it is displayed well, good job Gwen.. aka myself. Nice Nice Nice Nice",
                        ReplyDate = DateTime.Today,
                        ReplyVisibility = true,
                        ReplierGivenName = "Xander",
                        ReplierFamilyName = "Arman",
                        UserReplyReport = false
                    }
                },
                FavoriteCount = 5,
                UserPostReported = false,
                UserFavorited = true
            });
            fakePostVMs.Add(new PostVM
            {
                PostId = 2,
                PostAuthor = 2,
                PostContent = "This is supposed to test what a post with a very long post content message would look like. Hopefully this is displayed in a nice clean manner. Otherwise, Gwen will need to fix that because an ugly" +
                "post is no bueno. If it is displayed well, good job Gwen.. aka myself. Nice Nice Nice Nice" + "This is supposed to test what a reply with a very long reply content message would look like. Hopefully this is displayed in a nice clean manner. Otherwise, Gwen will need to fix that because an ugly" +
                "reply is no bueno. If it is displayed well, good job Gwen.. aka myself. Nice Nice Nice Nice" + "This is supposed to test what a reply with a very long reply content message would look like. Hopefully this is displayed in a nice clean manner. Otherwise, Gwen will need to fix that because an ugly" +
                "reply is no bueno. If it is displayed well, good job Gwen.. aka myself. Nice Nice Nice Nice" + "This is supposed to test what a reply with a very long reply content message would look like. Hopefully this is displayed in a nice clean manner. Otherwise, Gwen will need to fix that because an ugly" +
                "reply is no bueno. If it is displayed well, good job Gwen.. aka myself. Nice Nice Nice Nice",
                PostDate = DateTime.Today,
                PostVisibility = true,
                PosterGivenName = "Xander",
                PosterFamilyName = "Arman",
                UserPostReported = false
            });
            fakePostVMs.Add(new PostVM
            {
                PostId = 3,
                PostAuthor = 3,
                PostContent = "This is a post",
                PostDate = DateTime.Today,
                PostVisibility = true,
                PosterGivenName = "Nicholas",
                PosterFamilyName = "Arman",
                UserPostReported = false
            });
            fakePostVMs.Add(new PostVM
            {
                PostId = 3,
                PostAuthor = 3,
                PostContent = "This is a post",
                PostDate = DateTime.Today,
                PostVisibility = false,
                PosterGivenName = "Nicholas",
                PosterFamilyName = "Arman",
                UserPostReported = false
            });
            fakePosts.Add(new Post
            {
                PostId = 5,
                PostAuthor = 3,
                PostContent = "Post",
                PostDate = DateTime.Today,
                PostVisibility = true,
            });

        }

        public int DeletePostReport(int postId, int userId)
        {
            var report = reports.Find(tuple => tuple.Item1 == postId && tuple.Item2 == userId);
            if (report == null)
            {
                return 0;
            }
            reports.Remove(report);
            return 1;
        }

        public int InsertPost(Post post)
        {
            int result = 0;
            fakePosts.Add(post);
            foreach (var item in fakePosts)
            {
                if (item.PostId == post.PostId)
                {
                    result = 1;
                }
            }
            return result;
        }

        public int InsertPostReport(int postId, int userId, int reportMessageId)
        {
            int current = reports.Count;
            reports.Add(new Tuple<int, int>(postId, userId));
            return reports.Count - current;
        }

        public List<PostVM> SelectActivePosts()
        {
            return fakePostVMs.Where(p => p.PostVisibility == true).ToList();
        }

        public List<PostVM> SelectAllPosts()
        {
            return fakePostVMs;
        }

        public PostVM SelectPostByPostId(int postId)
        {
            return fakePostVMs.Find(p => p.PostId == postId);
        }

        public List<ReportMessage> SelectReportMessages()
        {
            List<ReportMessage> messages = new List<ReportMessage>();
            messages.Add(new ReportMessage() { ReportMessageDescription = "Foul Language", ReportMessageId = 1 });
            messages.Add(new ReportMessage() { ReportMessageDescription = "Harrassment", ReportMessageId = 2 });
            return messages;
        }

        public int SelectUserPostReportedByPostIdandUserId(int postId, int userId)
        {
            return reports.Count(join => join.Item1 == postId && join.Item2 == userId);
        }

        public int UpdatePost(Post post, Post newPost)
        {
            int result = 0;
            foreach (var item in fakePosts)
            {
                if (item.PostId == post.PostId)
                {
                    post.PostContent = newPost.PostContent;
                    post.PostDate = newPost.PostDate;
                    result = 1;
                }
            }
            return result;
        }
        public int UpdatePostVisibility(int postId, bool newVisibility, bool oldVisibility)
        {
            int result = 0;

            foreach (Post post in fakePosts)
            {
                if (post.PostId == postId)
                {
                    post.PostVisibility = newVisibility;
                    result = 1;
                }
            }

            return result;
        }
    }
}
