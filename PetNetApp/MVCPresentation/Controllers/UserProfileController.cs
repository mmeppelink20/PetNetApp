using DataObjects;
using LogicLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCPresentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentation.Controllers
{
    /// <summary>
    /// Chris Dreismeier
    /// Created: 2023/04/27
    /// 
    /// User Profile Controller
    /// </summary>
    public class UserProfileController : Controller
    {
        MasterManager _manager = MasterManager.GetMasterManager();

        /// <summary>
        /// Chris Dreismeier
        /// Created: 2023/04/27
        /// 
        /// Displays the users profile and the users information
        /// </summary>
        // GET: UserProfile
        [Authorize]
        public ActionResult Index()
        {
            var dbContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbContext));
            var user = userManager.FindById(User.Identity.GetUserId());

            try
            {
                if(user.ShelterId != null)
                {
                    var userShelter = _manager.ShelterManager.RetrieveShelterVMByShelterID((int)user.ShelterId);
                    ViewBag.UserShelterName = userShelter.ShelterName;
                }
                else
                {
                    ViewBag.NoShelter = "You do not have a shelter selected!";
                }
                
            }
            catch (Exception ex)
            {

                throw;
            }
            return View(); 
        }

        // Code by Alexis Oetken 
        public ActionResult AddBookmark(int? animalID, Users user)
        {
            var bookmarkManager = new LogicLayer.BookmarkManager();
            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.User = user; 

                    if (animalID == null)
                    {
                        ViewBag.Message = "Something went wrong! Please try again.";
                        return View("Error");
                    }

                    if (user == null)
                    {
                        ViewBag.Message = "Please sign in to use this feature.";
                        return View("Error");
                    }

                    else
                    {
                        bookmarkManager.AddBookmark((int)user.UsersId, (int)animalID);
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View("Error");
                }
            }
            return View();
        }

        //Code by Alexis Oetken 
        public ActionResult DeleteBookmark(int? animalID, Users user)
        {
            try
            {
                var bookmarkManager = new LogicLayer.BookmarkManager();

            ViewBag.User = user;

            bookmarkManager.DeleteBookmark((int)user.UsersId, (int)animalID);
            return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
            

        }

        // Code by Alexis Oetken 
        public ActionResult ViewAllBookmarks(int userID)
        {
            try
            {
                var bookmarkManager = new LogicLayer.BookmarkManager();

                List<DataObjects.Bookmark> bookmarkList = bookmarkManager.RetrieveAllBookmarks(userID);

                return View("BookmarksView", bookmarkList);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }
        }

        /// <summary>
        /// Molly Meister
        /// Created: 2023/04/23
        /// 
        /// Displays the adoption applications for the user.
        /// <paramref name="userId"/>
        /// </summary>
        //GET: UserProfile/MyApplications/6
        [ChildActionOnly]
        public ActionResult AdoptionApplicationsPartial(int userId)
        {
            try
            {
                List<AdoptionApplicationVM> applications = _manager.AdoptionApplicationManager.RetrieveAllAdoptionApplicationsByUsersId(userId);
                return PartialView(applications);
            }
            catch (Exception up)
            {
                ViewBag.Message = up.Message;
                return View("Error");
                //return View("Error");
            }
        }
    }
}