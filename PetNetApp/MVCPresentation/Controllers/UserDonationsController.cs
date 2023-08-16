using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using MVCPresentation.Models;

namespace MVCPresentation.Controllers
{
    public class UserDonationsController : Controller
    {
        /// <summary>
        /// Chris Dreismeier
        /// Created: 2023/04/27
        /// 
        /// User Donations Controller
        /// </summary>
        private MasterManager masterManager = MasterManager.GetMasterManager();
        private List<DonationVM> donationVMs;
        private DonationVM donationVM;
        private ApplicationUserManager userManager;


        /// <summary>
        /// Chris Dreismeier
        /// Created: 2023/04/27
        /// 
        /// Gives user a list of all of there donations
        /// </summary>
        // GET: Donate
        [Authorize]
        // GET: UserDonations
        public ActionResult Index()
        {
            var dbContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbContext));
            var user = userManager.FindById(User.Identity.GetUserId());

            try
            {
                if(user.UsersId != null)
                {
                    donationVMs = masterManager.DonationManager.RetrieveDonationsByUserId((int)user.UsersId);
                    ViewBag.User = user;
                }
                else
                {
                    ViewBag.Error = "There was an error retrieving your data please try again later";
                }
                
            }
            catch (Exception)
            {
                ViewBag.Error = "Could not retrieve donations";
                View("Error");
            }
            return View(donationVMs);
        }

        /// <summary>
        /// Chris Dreismeier
        /// Created: 2023/04/27
        /// 
        /// Gives a user more details on a specific donation
        /// </summary>
        // GET: Donate
        [Authorize]
        // GET: UserDonations/Details/5
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                try
                {
                    donationVM = masterManager.DonationManager.RetrieveDonationByDonationId(id.Value);
                }
                catch (Exception)
                {
                    ViewBag.Message = "Could not retrieve donation";
                    View("Error");
                }

                if (donationVM == null)
                {
                    ViewBag.Message = "No donation found";
                    return View("Error");
                }
                else
                {
                    try
                    {
                        donationVM.InKindList = masterManager.DonationManager.RetrieveInKindsByDonationId(donationVM.DonationId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                return View(donationVM);
            }
            else
            {
                ViewBag.Error = "No donation with that ID";
                return View("Error");
            }
        } 
    }
}
