using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Welcome to Pet.Net!";
            return View();
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult TermsOfService()
        {
            return View();
        }
    }
}