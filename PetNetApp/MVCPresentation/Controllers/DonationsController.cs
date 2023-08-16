using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using DataObjects;
using LogicLayer;
using MVCPresentation.Models;

namespace MVCPresentation.Controllers
{
    public class DonationsController : Controller
    {
        private MasterManager masterManager = MasterManager.GetMasterManager();
        private List<DonationVM> donationVMs;
        private DonationsViewModel _donationsViewModel = new DonationsViewModel();
        private DonationVM donationVM;
        private List<Shelter> _shelters;

        /// <summary>
        /// Author: Gwen Arman
        /// Date: 4/21/23
        /// </summary>
        /// 
        /// <remarks>
        /// Updater: Andrew Schneider
        /// Updated: 2023/04/26
        /// Added db calls and switch staments for the sort/filter functionality.
        /// 
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// Final QA
        /// </remarks>
        /// <param name="donationsViewModel">The view model that will be sorted, filtered, and displayed</param>
        /// <returns>Donations/Index view</returns>
        // GET: Donations
        public ActionResult Index(DonationsViewModel donationsViewModel)
        {
            ViewBag.Tab = "Donate";
            List<DonationVM> anonymousVMs = new List<DonationVM>();

            try
            {
                donationVMs = masterManager.DonationManager.RetrieveAllDonations();
            }
            catch (Exception)
            {
                ViewBag.Message = "Could not retrieve donations";
                View("Error");
            }

            try
            {
                _shelters = masterManager.ShelterManager.GetShelterList().
                    Where(shelter => shelter.ShelterActive).OrderBy(shelter => shelter.ShelterName).ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                View("Error");
            }

            try
            {
                if (donationsViewModel.Shelter != null)
                {
                    donationVMs = masterManager.DonationManager.RetrieveDonationsByShelterId(donationsViewModel.Shelter.Value);
                }
                else
                {
                    donationVMs = donationVMs = masterManager.DonationManager.RetrieveAllDonations();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message + "\n\n" + ex.Message;
                return View("Error");
            }

            int dateRange = 0;

            switch (donationsViewModel.DateFilterOptions)
            {
                case null:
                    break;
                case DateFilterOptions.LastDay:
                    dateRange = 1;
                    break;
                case DateFilterOptions.LastWeek:
                    dateRange = 7;
                    break;
                case DateFilterOptions.LastMonth:
                    dateRange = 30;
                    break;
                case DateFilterOptions.LastSixMonths:
                    dateRange = 180;
                    break;
                case DateFilterOptions.LastYear:
                    dateRange = 365;
                    break;
            }
            
            if(dateRange != 0)
            {
                donationVMs = donationVMs.Where(d => d.DateDonated >= (DateTime.Today - TimeSpan.FromDays(dateRange))).ToList();
            }

            switch (donationsViewModel.AmountFilterOptions)
            {
                case null:
                    break;
                case AmountFilterOptions.OneToTwentyFive:
                    donationVMs = donationVMs.Where(d => d.Amount > 0 && d.Amount <= 25).ToList();
                    break;
                case AmountFilterOptions.TwentyFiveToFifty:
                    donationVMs = donationVMs.Where(d => d.Amount > 25 && d.Amount <= 50).ToList();
                    break;
                case AmountFilterOptions.FiftyToOneHundred:
                    donationVMs = donationVMs.Where(d => d.Amount > 50 && d.Amount <= 100).ToList();
                    break;
                case AmountFilterOptions.OneHundredToFiveHundred:
                    donationVMs = donationVMs.Where(d => d.Amount > 100 && d.Amount <= 500).ToList();
                    break;
                case AmountFilterOptions.OverFiveHundred:
                    donationVMs = donationVMs.Where(d => d.Amount > 500).ToList();
                    break;
            }

            switch (donationsViewModel.MiscellaneousFilterOptions)
            {
                case null:
                    break;
                case MiscellaneousFilterOptions.HasMessage:
                    donationVMs = donationVMs.Where(d => d.Message != null && d.Message != "").ToList();
                    break;
                case MiscellaneousFilterOptions.HasNoMessage:
                    donationVMs = donationVMs.Where(d => d.Message == null || d.Message == "").ToList();
                    break;
            }

            switch (donationsViewModel.Sort)
            {
                case null:
                case DonationSortOptions.Date:
                    donationVMs = donationVMs.OrderByDescending(d => d.DateDonated != null ?
                                    d.DateDonated.Value.Ticks.ToString() : DateTime.MaxValue.Ticks.ToString()).ToList();
                    break;
                case DonationSortOptions.FamilyName:
                    anonymousVMs = donationVMs.Where(d => d.Anonymous).ToList();
                    donationVMs = donationVMs.Where(d => !d.Anonymous).OrderBy(d => d.FamilyName).ToList();
                    donationVMs.AddRange(anonymousVMs);
                    break;
                case DonationSortOptions.Amount:
                    donationVMs = donationVMs.OrderBy(d => d.Amount == null ? 0 : d.Amount).ToList();
                    break;
            }

            donationsViewModel.Count = donationVMs.Count();
            donationsViewModel.DonationVMs = donationVMs;
            donationsViewModel.Shelters = _shelters;
            return View(donationsViewModel);
        }

        /// <summary>
        /// Author: Gwen Arman
        /// Date: 4/21/23
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Donations/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Tab = "Donate";
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
                        ViewBag.Message = ex.Message;
                        return View("Error");
                    }
                }

                return View(donationVM);
            }
            else
            {
                ViewBag.Message = "No donation with that ID";
                return View("Error");
            }
        }

        // GET: Donations/Filtered
        public ActionResult Filtered(DonationsViewModel donationsViewModel)
        {
            return View(donationsViewModel);
        }



        /// <summary>
        /// Author: Gwen Arman
        /// Date: 4/21/23
        /// </summary>
        /// <returns></returns>
        // GET: Donations/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Author: Gwen Arman
        /// Date: 4/21/23
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        // POST: Donations/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
