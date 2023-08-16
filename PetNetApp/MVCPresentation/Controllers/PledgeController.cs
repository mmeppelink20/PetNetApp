using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;
using DataObjects;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace MVCPresentation.Controllers
{
    /// <summary>
    /// William Rients
    /// Created: 2023/04/20
    /// 
    /// Pledge controller
    /// </summary>
    public class PledgeController : Controller
    {
        private MasterManager masterManager = MasterManager.GetMasterManager();
        private ApplicationUserManager applicationUserManager;
        private PledgeVM pledgeVM;

        // GET: Pledge
        public ActionResult Index()
        {
            return View();
        }

        // GET: Pledge/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/04/20
        /// 
        /// Get method for create pledge page
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Pledge/Create
        public ActionResult Create(int? fundrasingEventId)
        {
            ViewBag.Tab = "Fundraising";
            if (fundrasingEventId == null)
            {
                ViewBag.Message = "You must have an Event Id to create a pledge";

                return View("Error");
            }

            ViewBag.FundrasingEventId = fundrasingEventId;

            if (Session["pledgeStatus"] != null)
            {
                ViewBag.PledgeStatus = Session["pledgeStatus"].ToString();
                Session["pledgeStatus"] = null;
            }
            return View();
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/04/19
        /// 
        /// Post method for creating a pledge, takes
        /// in a pledge Id
        /// 
        /// </summary>
        /// <param name="fundrasingEventId"></param>
        /// <returns></returns>
        // POST: Pledge/Create
        [HttpPost]
        public ActionResult Create(PledgeVM pledgeVM)
        {
            applicationUserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = applicationUserManager.FindById(User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                try
                {
                    if (user != null)
                    {
                        // user has an account
                        pledgeVM.UserId = (int)user.UsersId;
                        masterManager.PledgeManager.CreatePledge(pledgeVM);
                    }
                    else
                    {
                        // user doesn't have an account & ID is null
                        masterManager.PledgeManager.CreatePledge(pledgeVM);
                    }
                    

                    Session["pledgeStatus"] = "Your Pledge has been Created!";
                    return RedirectToAction("Create");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;

                    return View("Error");
                }
            }
            return View(pledgeVM);
        }
    }
}
