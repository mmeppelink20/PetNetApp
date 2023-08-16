using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;
using DataObjects;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Diagnostics;
using MVCPresentation.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MVCPresentation.Controllers
{
    //[Authorize]
    public class CommunityController : Controller
    {
        private MasterManager masterManager = MasterManager.GetMasterManager();
        private List<PostVM> posts;
        private PostVM postVM;


        /// <summary>
        /// Author: Gwen Arman
        /// Date: 4/21/23
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        // GET: Community
        public ActionResult Index(Users user)
        {
            ViewBag.Tab = "Community";
            try
            {
                if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
                {
                    posts = masterManager.PostManager.RetrieveAllPosts();
                }
                else
                {
                    posts = masterManager.PostManager.RetrieveActivePosts();
                }
                if (User.Identity.IsAuthenticated)
                {
                    foreach (var post in posts)
                    {
                        post.UserPostReported = masterManager.PostManager.RetrieveUserPostReportedByPostIdAndUserId(post.PostId, user.UsersId);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message + "\n" + ex.InnerException;
                return View("Error");
            }
            ViewBag.UserId = GetLoggedInUser() == null ? -1 : GetLoggedInUser().UsersId;
            ViewBag.HasAdminRole = User.IsInRole("Admin");
            ViewBag.HasModeratorRole = User.IsInRole("Moderator");
            return View(posts);
        }

        /// <summary>
        /// Stephen Jaurigue
        /// 2023/04/13
        /// 
        /// Returns the form to select a report reason
        /// </summary>
        /// <param name="post">post to report</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult BeginReportPost(int? post, Users user)
        {
            ViewBag.User = user;
            try
            {
                if (post == null)
                {
                    throw new ApplicationException("No post to Report");
                }
                var postVM = masterManager.PostManager.RetrievePostByPostId(post.Value);
                postVM.UserPostReported = masterManager.PostManager.RetrieveUserPostReportedByPostIdAndUserId(postVM.PostId, user.UsersId);
                if (postVM.UserPostReported)
                {
                    throw new ApplicationException("You already reported this post");
                }
                if (postVM.PostAuthor == user.UsersId)
                {
                    throw new ApplicationException("You cannot report your own post");
                }
                // add logic here
                postVM.UserPostReported = !postVM.UserPostReported;
                var reportReasons = masterManager.PostManager.RetrieveReportMessages();
                ViewBag.ReportReasons = reportReasons;

                if (Request.IsAjaxRequest())
                {
                    return PartialView("BeginPostReport", postVM);
                }
                return View("BeginPostReport", postVM);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message + "\n" + ex.InnerException;
                if (Request.IsAjaxRequest())
                {
                    return Content("Error");
                }
                return View("Error");
            }
        }

        /// <summary>
        /// Stephen Jaurgiue
        /// 2023/04/13
        /// 
        /// removes the report from the current user for the post
        /// </summary>
        /// <param name="post">Post to unreport</param>
        /// <returns>Updated Report form or full page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult UnreportPost(int? post, Users user)
        {
            PostVM postVM = null;
            ViewBag.User = user;
            try
            {
                if (post == null)
                {
                    throw new ApplicationException("No post to unreport");
                }
                postVM = masterManager.PostManager.RetrievePostByPostId(post.Value);
                postVM.UserPostReported = masterManager.PostManager.RetrieveUserPostReportedByPostIdAndUserId(postVM.PostId, user.UsersId);
                if (!postVM.UserPostReported)
                {
                    throw new ApplicationException("You haven't reported this post");
                }

                if (!masterManager.PostManager.RemovePostReport(post.Value, user.UsersId))
                {
                    throw new ApplicationException("Something went wrong");
                }
                postVM.UserPostReported = masterManager.PostManager.RetrieveUserPostReportedByPostIdAndUserId(postVM.PostId, user.UsersId);
                if (Request.IsAjaxRequest())
                {
                    return PartialView("ReportPartial", postVM);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message + "\n" + ex.InnerException;
                ViewBag.ReportError = ex.Message;
                if (Request.IsAjaxRequest())
                {
                    if (postVM != null)
                    {
                        return PartialView("ReportPartial", postVM);
                    }
                    Content("Error");
                }
                return View("Error");
            }
        }

        /// <summary>
        /// Stephen Jaurigue
        /// 2023/04/13
        /// 
        /// Adds a report to the post for the currently logged in user
        /// </summary>
        /// <param name="post">post to report</param>
        /// <param name="reason">the reason for the report</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult ReportPost(int? post, int? reason, Users user)
        {
            ViewBag.User = user;
            Debug.WriteLine(post + " " + reason);
            PostVM postVM = null;
            try
            {
                if (post == null)
                {
                    throw new ApplicationException("No post to unreport");
                }
                postVM = masterManager.PostManager.RetrievePostByPostId(post.Value);
                postVM.UserPostReported = masterManager.PostManager.RetrieveUserPostReportedByPostIdAndUserId(postVM.PostId, user.UsersId);
                if (postVM.UserPostReported)
                {
                    throw new ApplicationException("You already reported this post");
                }

                if (reason == null)
                {
                    throw new ApplicationException("Select a reason");
                }

                if (postVM.PostAuthor == user.UsersId)
                {
                    throw new ApplicationException("You cannot report your own post");
                }

                if (!masterManager.PostManager.AddPostReport(post.Value, user.UsersId,reason.Value))
                {
                    throw new ApplicationException("Something went wrong");
                }
                postVM.UserPostReported = masterManager.PostManager.RetrieveUserPostReportedByPostIdAndUserId(postVM.PostId, user.UsersId);
                if (Request.IsAjaxRequest())
                {
                    return PartialView("ReportPartial", postVM);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message + "\n" + ex.InnerException;
                ViewBag.ReportError = ex.Message;
                if (Request.IsAjaxRequest())
                {
                    if (postVM == null)
                    {
                        return Content(ex.Message);
                    }
                    return PartialView("ReportPartial", postVM);
                }
                return View("Error");
            }
        }

        /// <summary>
        /// Stephen Jaurigue
        /// 2023/04/13
        /// 
        /// Returns the form to select a report reason
        /// </summary>
        /// <param name="reply">reply to report</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult BeginReportReply(int? reply, Users user)
        {
            ViewBag.User = user;
            try
            {
                Debug.WriteLine("Inside Try");
                if (reply == null)
                {
                    Debug.WriteLine("Reply Null");
                    throw new ApplicationException("No reply to Report");
                }
                var replyVM = masterManager.ReplyManager.RetrieveReplyByReplyId(reply.Value);
                replyVM.UserReplyReport = masterManager.ReplyManager.RetrieveUserReplyReportedByReplyIdAndUserId(replyVM.ReplyId, user.UsersId);
                if (replyVM.UserReplyReport)
                {
                    Debug.WriteLine("Already Reported");
                    throw new ApplicationException("You already reported this post");
                }
                if (replyVM.ReplyAuthor == user.UsersId)
                {
                    Debug.WriteLine("My Reported");
                    throw new ApplicationException("You cannot report your own post");
                }
                Debug.WriteLine("Past ifs");
                replyVM.UserReplyReport = !replyVM.UserReplyReport;
                var reportReasons = masterManager.PostManager.RetrieveReportMessages();
                ViewBag.ReportReasons = reportReasons;
                Debug.WriteLine("Past reportResons");

                if (Request.IsAjaxRequest())
                {
                    return PartialView("BeginReplyReport", replyVM);
                }
                return View("BeginReplyReport", replyVM);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                ViewBag.Message = ex.Message + "\n" + ex.InnerException;
                if (Request.IsAjaxRequest())
                {
                    return Content("Error");
                }
                return View("Error");
            }
        }

        /// <summary>
        /// Stephen Jaurgiue
        /// 2023/04/13
        /// 
        /// removes the report from the current user for the post
        /// </summary>
        /// <param name="reply">Post to unreport</param>
        /// <returns>Updated Report form or full page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult UnreportReply(int? reply, Users user)
        {
            ReplyVM replyVM = null;
            ViewBag.User = user;
            try
            {
                if (reply == null)
                {
                    throw new ApplicationException("No post to unreport");
                }
                replyVM = masterManager.ReplyManager.RetrieveReplyByReplyId(reply.Value);
                replyVM.UserReplyReport = masterManager.ReplyManager.RetrieveUserReplyReportedByReplyIdAndUserId(replyVM.ReplyId, user.UsersId);
                if (!replyVM.UserReplyReport)
                {
                    throw new ApplicationException("You haven't reported this post");
                }

                if (!masterManager.ReplyManager.RemoveReplyReport(reply.Value, user.UsersId))
                {
                    throw new ApplicationException("Something went wrong");
                }
                replyVM.UserReplyReport = masterManager.ReplyManager.RetrieveUserReplyReportedByReplyIdAndUserId(replyVM.ReplyId, user.UsersId);
                if (Request.IsAjaxRequest())
                {
                    return PartialView("ReportReplyPartial", replyVM);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message + "\n" + ex.InnerException;
                ViewBag.ReportError = ex.Message;
                if (Request.IsAjaxRequest())
                {
                    if (replyVM != null)
                    {
                        return PartialView("ReportReplyPartial", replyVM);
                    }
                    Content("Error");
                }
                return View("Error");
            }
        }

        /// <summary>
        /// Stephen Jaurigue
        /// 2023/04/13
        /// 
        /// Adds a report to the reply for the currently logged in user
        /// </summary>
        /// <param name="reply">post to report</param>
        /// <param name="reason">the reason for the report</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult ReportReply(int? reply, int? reason, Users user)
        {
            ViewBag.User = user;
            Debug.WriteLine(reply + " " + reason);
            ReplyVM replyVM = null;
            try
            {
                if (reply == null)
                {
                    throw new ApplicationException("No reply to unreport");
                }
                replyVM = masterManager.ReplyManager.RetrieveReplyByReplyId(reply.Value);
                replyVM.UserReplyReport = masterManager.ReplyManager.RetrieveUserReplyReportedByReplyIdAndUserId(replyVM.ReplyId, user.UsersId);
                if (replyVM.UserReplyReport)
                {
                    throw new ApplicationException("You already reported this reply");
                }

                if (reason == null)
                {
                    throw new ApplicationException("Select a reason");
                }

                if (replyVM.ReplyAuthor == user.UsersId)
                {
                    throw new ApplicationException("You cannot report your own post");
                }

                if (!masterManager.ReplyManager.AddReplyReport(reply.Value, user.UsersId, reason.Value))
                {
                    throw new ApplicationException("Something went wrong");
                }
                replyVM.UserReplyReport = masterManager.ReplyManager.RetrieveUserReplyReportedByReplyIdAndUserId(replyVM.ReplyId, user.UsersId);
                if (Request.IsAjaxRequest())
                {
                    return PartialView("ReportReplyPartial", replyVM);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message + "\n" + ex.InnerException;
                ViewBag.ReportError = ex.Message;
                if (Request.IsAjaxRequest())
                {
                    if (replyVM == null)
                    {
                        return Content(ex.Message);
                    }
                    return PartialView("ReportReplyPartial", replyVM);
                }
                return View("Error");
            }
        }

        /// <summary>
        /// Author: Gwen Arman
        /// Date: 4/21/23
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult ShowReplies(int? id, Users user)
        {
            if(id != null)
            {
                try
                {
                    postVM = masterManager.PostManager.RetrievePostByPostId(id.Value);

                    if (postVM.PostVisibility)
                    {
                        if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
                        {
                            postVM.Replies = masterManager.ReplyManager.RetrieveAllRepliesByPostId(postVM.PostId);
                        }
                        else
                        {
                            postVM.Replies = masterManager.ReplyManager.RetrieveActiveRepliesByPostId(postVM.PostId);
                        }
                        if (User.Identity.IsAuthenticated)
                        {
                            foreach (ReplyVM reply in postVM.Replies)
                            {
                                reply.UserReplyReport = masterManager.ReplyManager.RetrieveUserReplyReportedByReplyIdAndUserId(reply.ReplyId, user.UsersId);
                            }
                        }
                        
                    }
                    else
                    {
                        ViewBag.Message = "Invaild Request";
                        return View("Error");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message + "\n" + ex.InnerException;
                    return View("Error");
                }

                ViewBag.UserId = GetLoggedInUser() == null ? -1 : GetLoggedInUser().UsersId;
                ViewBag.HasAdminRole = User.IsInRole("Admin");
                ViewBag.HasModeratorRole = User.IsInRole("Moderator");
                Session["PostId"] = id;
                return View(postVM);
            }
            else
            {
                ViewBag.Message = "Please select a post";
                return View("Error");
            }
        }

        /// <summary>
        /// Author: Gwen Arman
        /// Date: 4/21/23
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        // POST: Community/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            post.PostAuthor = GetLoggedInUser().UsersId.Value;

            post.PostDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    masterManager.PostManager.AddPost(post);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message + "\n\n" + ex.InnerException.Message;
                    return View("Error");
                }
            }
            else
            {
                return View("Index");
            }
        }

        /// <summary>
        /// Author: Gwen Arman
        /// Date: 4/21/23
        /// </summary>
        /// <param name="reply"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateReply(Reply reply)
        {
            reply.ReplyAuthor = GetLoggedInUser().UsersId.Value;
            reply.ReplyDate = DateTime.Now;
            reply.PostId = (int)Session["PostId"];
            ViewBag.UserId = GetLoggedInUser() == null ? -1 : GetLoggedInUser().UsersId;
            ViewBag.HasAdminRole = User.IsInRole("Admin");
            ViewBag.HasModeratorRole = User.IsInRole("Moderator");

            try
            {
                postVM = masterManager.PostManager.RetrievePostByPostId(reply.PostId);

                if (postVM.PostVisibility)
                {
                    if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
                    {
                        postVM.Replies = masterManager.ReplyManager.RetrieveAllRepliesByPostId(postVM.PostId);
                    }
                    else
                    {
                        postVM.Replies = masterManager.ReplyManager.RetrieveActiveRepliesByPostId(postVM.PostId);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message + "\n\n" + ex.InnerException.Message;
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    masterManager.ReplyManager.AddReply(reply);
                    Session["PostId"] = null;
                    return RedirectToAction("ShowReplies", new { id = reply.PostId });
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message + "\n\n" + ex.InnerException.Message;
                    return View("Error");
                }
            }
            else
            {
                return View("ShowReplies", postVM);
            }
        }

        /// <summary>
        /// Author: Gwen Arman
        /// Date: 4/21/23
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Community/Edit/5
        public ActionResult Edit(int? id)
        {
            Post postToEdit = null;
            if(id != null)
            {
                try
                {
                    postToEdit = masterManager.PostManager.RetrievePostByPostId(id.Value);

                    if(postToEdit.PostAuthor != GetLoggedInUser().UsersId)
                    {
                        ViewBag.Message = "Invaild Request";
                        return View("Error");
                    }

                    Session["postToEdit"] = postToEdit;

                    if(postToEdit == null)
                    {
                        throw new Exception("Post not found");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View("Error");
                }
            } 
            else
            {
                ViewBag.Message = "Not a valid post";
                return View("Error");
            }
            return View(postToEdit);
        }

        /// <summary>
        /// Author: Gwen Arman
        /// Date: 4/21/23
        /// </summary>
        /// <param name="newPost"></param>
        /// <returns></returns>
        // POST: Community/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post newPost)
        {
            if (((Post)Session["postToEdit"]).PostAuthor != GetLoggedInUser().UsersId)
            {
                ViewBag.Message = "Invaild Request";
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    newPost.PostDate = DateTime.Now;
                    masterManager.PostManager.EditPost((Post)Session["postToEdit"], newPost);
                    Session["postToEdit"] = null;
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message + ex.InnerException;
                    return View("Error");
                }
            } 
            else
            {
                return View(newPost);
            }
        }

        /// <summary>
        /// Author: Gwen Arman
        /// Date: 4/21/23
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Community/EditReply/5
        public ActionResult EditReply(int? id)
        {
            Reply replyToEdit = null;
            if (id != null)
            {
                try
                {
                    replyToEdit = masterManager.ReplyManager.RetrieveReplyByReplyId(id.Value);

                    if (replyToEdit.ReplyAuthor != GetLoggedInUser().UsersId)
                    {
                        ViewBag.Message = "Invaild Request";
                        return View("Error");
                    }


                    Session["replyToEdit"] = replyToEdit;

                    if (replyToEdit == null)
                    {
                        throw new Exception("Reply not found");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View("Error");
                }
            }
            else
            {
                ViewBag.Message = "Not a valid reply";
                return View("Error");
            }
            return View(replyToEdit);
        }

        /// <summary>
        /// Author: Gwen Arman
        /// Date: 4/21/23
        /// </summary>
        /// <param name="newReply"></param>
        /// <returns></returns>
        // POST: Community/EditReply/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditReply(Reply newReply)
        {
            if (((Reply)Session["replyToEdit"]).ReplyAuthor != GetLoggedInUser().UsersId)
            {
                ViewBag.Message = "Invaild Request";
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    newReply.ReplyDate = DateTime.Now;
                    masterManager.ReplyManager.EditReply((Reply)Session["replyToEdit"], newReply);

                    int postId = ((Reply)Session["replyToEdit"]).PostId;
                    Session["replyToEdit"] = null;
                    return RedirectToAction("ShowReplies", new { id = postId });
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message + ex.InnerException;
                    return View("Error");
                }
            }
            else
            {
                return View(newReply);
            }
        }

        /// <summary>
        /// Matthew Meppelink
        /// 2023/04/13
        /// 
        /// Sets a posts visibility to false
        /// </summary>
        /// <param name="id">post id</param>
        /// <returns></returns>
        // GET: Community/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                Post post = new Post();
                try
                {
                    post = masterManager.PostManager.RetrieveActivePosts().Find(p => p.PostId == id);
                    if (post == null)
                    {
                        ViewBag.Message = "That post doesn't exist";
                        return View("Error");
                    }

                    if (post.PostVisibility == false)
                    {
                        ViewBag.Message = "That post has been deleted";
                    }

                    if (GetLoggedInUser().UsersId == post.PostAuthor || User.IsInRole("Admin") || User.IsInRole("Moderator"))
                    {
                        if (GetLoggedInUser().UsersId != post.PostAuthor && !User.IsInRole("Admin") && !User.IsInRole("Moderator"))
                        {
                            ViewBag.Message = "You can't delete a post you didn't make";
                            return View("Error");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Only admins can delete messages from others";
                        return View("Error");
                    }


                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message + ex.InnerException;
                    return View("Error");
                }
                return View(post);
            }
            else
            {
                ViewBag.Message = "You need to specify a post to view";
                return View("Error");
            }
        }

        /// <summary>
        /// Matthew Meppelink
        /// 2023/04/13
        /// 
        /// Sets a posts visibility to false
        /// </summary>
        /// <param name="id">post id</param>
        /// <returns></returns>
        // POST: Community/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            if (id != null)
                try
                {
                    Post oldPost = masterManager.PostManager.RetrieveActivePosts().
                            First(p => p.PostId == id);
                    Post newPost = masterManager.PostManager.RetrieveActivePosts().
                            First(p => p.PostId == id);

                    if (GetLoggedInUser().UsersId == oldPost.PostAuthor || User.IsInRole("Admin") || User.IsInRole("Moderator"))
                    {
                        if (GetLoggedInUser().UsersId != oldPost.PostAuthor && !User.IsInRole("Admin") && !User.IsInRole("Moderator"))
                        {
                            ViewBag.Message = "You can't delete a post you didn't make";
                            return View("Error");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Only admins can delete messages from others";
                        return View("Error");
                    }

                    newPost.PostVisibility = false;
                    masterManager.PostManager.EditPostVisibility(newPost.PostId, newPost.PostVisibility, oldPost.PostVisibility);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("Error");
                }
            else
            {
                ViewBag.Message = "You need to specify a post to delete";
                return View("Error");
            }
        }

        /// <summary>
        /// Matthew Meppelink
        /// 2023/04/13
        /// 
        /// Sets a posts visibility to false
        /// </summary>
        /// <param name="id">post id</param>
        /// <returns></returns>
        // GET: Community/Delete/5
        [Authorize]
        public ActionResult ReinstatePost(int? id)
        {
            if (id != null)
            {
                Post post = new Post();
                try
                {
                    post = masterManager.PostManager.RetrievePostByPostId((int)id);
                    if (post.PostVisibility == true)
                    {
                        ViewBag.Message = "This post is already active";
                        return View("Error");
                    }

                    if (GetLoggedInUser().UsersId == post.PostAuthor || User.IsInRole("Admin") || User.IsInRole("Moderator"))
                    {
                        if (GetLoggedInUser().UsersId != post.PostAuthor && !User.IsInRole("Admin") && User.IsInRole("Moderator"))
                        {
                            ViewBag.Message = "You can't reinstate a post you didn't make";
                            return View("Error");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Only admins can reinstate messages from others";
                        return View("Error");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message + ex.InnerException;
                    return View("Error");
                }
                return View(post);
            }
            else
            {
                ViewBag.Message = "You need to specify a post to view";
                return View("Error");
            }
        }

        /// <summary>
        /// Matthew Meppelink
        /// 2023/04/13
        /// 
        /// Sets a posts visibility to false
        /// </summary>
        /// <param name="id">post id</param>
        /// <returns></returns>
        // POST: Community/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReinstatePost(int? id, FormCollection collection)
        {
            if (id != null)
                try
                {
                    Post oldPost = masterManager.PostManager.RetrievePostByPostId((int)id);
                    Post newPost = masterManager.PostManager.RetrievePostByPostId((int)id);
                    newPost.PostVisibility = true;
                    if (GetLoggedInUser().UsersId == oldPost.PostAuthor || User.IsInRole("Admin") || User.IsInRole("Moderator"))
                    {
                        if (GetLoggedInUser().UsersId != oldPost.PostAuthor && !User.IsInRole("Admin") && User.IsInRole("Moderator"))
                        {
                            ViewBag.Message = "You can't reinstate a post you didn't make";
                            return View("Error");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Only admins can reinstate messages from others";
                        return View("Error");
                    }
                    masterManager.PostManager.EditPostVisibility(newPost.PostId, newPost.PostVisibility, oldPost.PostVisibility);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("Error");
                }
            else
            {
                ViewBag.Message = "You need to specify a post to delete";
                return View("Error");
            }
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/14
        /// 
        /// Shows a page where the user will verify if they actualy want to delete the reply.
        /// </summary>
        // GET: Community/DeleteReply/5
        public ActionResult DeleteReply(int? id)
        {
            if (id != null)
            {
                ReplyVM reply = new ReplyVM();
                try
                {
                    reply = masterManager.ReplyManager.RetrieveReplyByReplyId(id.Value);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message + ex.InnerException;
                    return View("Error");
                }
                return View(reply);
            }
            else
            {
                ViewBag.Message = "You need to specify a Reply to delete.";
                return View("Error");
            }
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/04/14
        /// 
        /// Causes the reply to no longer be shown and returns to index
        /// </summary>
        // POST: Community/DeleteReply/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteReply(int? id, FormCollection collection)
        {
            if (id != null)
                try
                {
                    ReplyVM reply = masterManager.ReplyManager.RetrieveReplyByReplyId(id.Value);
                    reply.ReplyVisibility = false;
                    masterManager.ReplyManager.EditReplyVisibilityByReplyId(reply);

                    return RedirectToAction("ShowReplies", new { id = reply.PostId });
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("Error");
                }
            else
            {
                ViewBag.Message = "You need to specify a reply to delete";
                return View("Error");
            }
        }

        /// <summary>
        /// Description: Helper method that gets the logged in Application User
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public ApplicationUser GetLoggedInUser()
        {
            var dbContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbContext));
            return userManager.FindById(User.Identity.GetUserId());
        }

        // MADS !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    }
}