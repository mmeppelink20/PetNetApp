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
    /// Created: 2023/04/19
    /// 
    /// Ticket controller
    /// </summary>
    public class TicketController : Controller
    {
        private MasterManager masterManager = MasterManager.GetMasterManager();
        private ApplicationUserManager applicationUserManager;
        private List<TicketVM> ticketVMs;

        /// <summary>
        /// William Rients
        /// Created: 2023/04/19
        /// 
        /// Retrieves a list of tickets
        /// </summary>
        /// <returns></returns>
        // GET: Ticket
        [Authorize(Roles = "Admin, Administration")]
        public ActionResult Index()
        {
            try
            {
                ticketVMs = masterManager.TicketManager.RetrieveAllTickets();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message + "<br /><br />" + ex.InnerException.Message;

                return View("Error");
            }
            return View(ticketVMs);
        }

        // GET: Ticket/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/04/19
        /// 
        /// Get method for create ticket page
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Ticket/Create       
        [Authorize]
        public ActionResult Create()
        {
            if (Session["ticketStatus"] != null)
            {
                ViewBag.TicketStatus = Session["ticketStatus"].ToString();
                Session["ticketStatus"] = null;
            }
            
            return View();
        }

        /// <summary>
        /// William Rients
        /// Created: 2023/04/19
        /// 
        /// Post method for creating a ticket, gets
        /// the current user logged in and checks if model state
        /// is valid
        /// 
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        // POST: Ticket/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ticket ticket)
        {
            applicationUserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = applicationUserManager.FindById(User.Identity.GetUserId());
            

            if (ModelState.IsValid)
            {
                try
                {
                    ticket.UserId = (int)user.UsersId;
                    masterManager.TicketManager.CreateNewTicket(ticket.UserId, ticket.TicketStatusId, ticket.TicketTitle, ticket.TicketContext);
                    Session["ticketStatus"] = "Your Ticket has been Created!";
                    return RedirectToAction("Create");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;

                    return View("Error");
                }
            }
            return View(ticket);
        }

        // GET: Ticket/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ticket/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ticket/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ticket/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}