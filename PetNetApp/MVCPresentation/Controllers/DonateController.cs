using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;
using DataObjects;
using MVCPresentation.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MVCPresentation.Controllers
{
    public class DonateController : Controller
    {
        /// <summary>
        /// Chris Dreismeier
        /// Created: 2023/04/27
        /// 
        /// Donate Controller
        /// </summary>
        private MasterManager _masterManager = MasterManager.GetMasterManager();

        
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Donations");
        }

        /// <summary>
        /// Chris Dreismeier
        /// Created: 2023/04/27
        /// 
        /// Lets user make donations
        /// </summary>
        // GET: Donate
        public ActionResult Donate()
        {
            try
            {

                ViewBag.Shelters = _masterManager.ShelterManager.GetShelterList();
            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }

        /// <summary>
        /// Chris Dreismeier
        /// Created: 2023/04/27
        /// 
        /// Lets user make donations Post method
        /// </summary>
        // GET: Donate
        // POST: Donation/Create
        [HttpPost]
        public ActionResult Donate(Donation donation)
        {
            var dbContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbContext));
            var user = userManager.FindById(User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                try
                {
                    // Logic to add donation to the database
                    try
                    {
                        if(user != null)
                        {
                            donation.UserId = user.UsersId;
                            _masterManager.DonationManager.AddDonation(donation);
                        }
                        else
                        {
                            _masterManager.DonationManager.AddDonation(donation);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = ex.Message + "\n\n" + ex.InnerException.Message;
                        return View("Error");
                    }

                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ViewBag.Message = ex.Message + "\n \n" + ex.InnerException.Message;
                    return View("Error");
                }
            }
            else // model was not valid
            {
                try
                {
                    ViewBag.Shelters = _masterManager.ShelterManager.GetShelterList();
                }
                catch(Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("Error");
                }
            }
            return View();
        }


    }
}

