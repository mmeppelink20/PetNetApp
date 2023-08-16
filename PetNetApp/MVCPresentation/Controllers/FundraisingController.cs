using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;
using LogicLayerInterfaces;
using DataObjects;
using MVCPresentation.Models;
using System.Threading.Tasks;

namespace MVCPresentation.Controllers
{
    public class FundraisingController : Controller
    {
        private MasterManager _masterManger = MasterManager.GetMasterManager();
        // GET: Fundraising
        public ActionResult Index()
        {
            @ViewBag.Tab = "Fundraising";
            return View();
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/04/08
        /// 
        /// Controller method for /Fundraising/Campaign to view a single campaign
        /// </summary>
        /// <param name="campaign">the id of the campaign to view</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Campaign(int? campaign)
        {
            FundraisingCampaignVM fundraisingCampaign = null;
            try
            {

                if (campaign == null
                    || (fundraisingCampaign = _masterManger.FundraisingCampaignManager.RetrieveFundraisingCampaignByFundraisingCampaignId(campaign.Value)) == null
                    || !fundraisingCampaign.Active)
                {
                    return RedirectToAction("Campaigns");
                }
                fundraisingCampaign.Sponsors = _masterManger.InstitutionalEntityManager.RetrieveFundraisingSponsorsByCampaignId(campaign.Value);
                fundraisingCampaign.FundraisingEventList = _masterManger.FundraisingEventManager.RetrieveAllFundraisingEventsByCampaignId(campaign.Value).Cast<FundraisingEvent>().ToList();

                return View(fundraisingCampaign);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message + ex.InnerException.Message;
                return View("Error");
            }
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/04/08
        /// 
        /// Controller method for /Fundraising/Campaign to get a partial view for a single campaign
        /// </summary>
        /// <param name="campaign">the id of the campaign to view</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Campaign(FormCollection collection)
        {
            int? campaign;
            try
            {
                campaign = Convert.ToInt32(collection.Get("campaign"));
            }
            catch
            {
                campaign = null;
            }
            FundraisingCampaignVM fundraisingCampaign = null;
            try
            {
                if (campaign == null
                    || (fundraisingCampaign = _masterManger.FundraisingCampaignManager.RetrieveFundraisingCampaignByFundraisingCampaignId(campaign.Value)) == null
                    || !fundraisingCampaign.Active)
                {
                    ViewBag.Message = "Could not find the selected campaign";
                    return PartialView("Error");
                }
                fundraisingCampaign.Sponsors = _masterManger.InstitutionalEntityManager.RetrieveFundraisingSponsorsByCampaignId(campaign.Value);
                fundraisingCampaign.FundraisingEventList = _masterManger.FundraisingEventManager.RetrieveAllFundraisingEventsByCampaignId(campaign.Value).Cast<FundraisingEvent>().ToList();

                return PartialView(fundraisingCampaign);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message + ex.InnerException.Message;

                return PartialView("Error");
            }
        }

        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/04/08
        /// 
        /// Controller method for /Fundraising/Campaigns to view all public campaigns
        /// </summary>
        /// <param name="campaignsViewModel">The model holding the different parameters for searching and campaign data</param>
        /// <param name="Page">The current page were on</param>
        /// <returns></returns>
        public ActionResult Campaigns(CampaignsViewModel campaignsViewModel, int Page = 1)
        {
            @ViewBag.Tab = "Fundraising";
            PagingInfo pagingInfo = campaignsViewModel.PagingInfo;
            pagingInfo.CurrentPage = Page;
            pagingInfo.ItemsPerPage = 10;

            List<FundraisingCampaignVM> campaigns = null;
            try
            {
                
                campaignsViewModel.Shelters = _masterManger.ShelterManager.GetShelterList().Where(shelter => shelter.ShelterActive).OrderBy(sh => sh.ShelterName);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }

            try
            {
                if (campaignsViewModel.Shelter != null)
                {
                    campaigns = _masterManger.FundraisingCampaignManager.RetrieveAllActiveFundraisingCampaignsByShelterId(campaignsViewModel.Shelter.Value);
                }
                else
                {
                    campaigns = _masterManger.FundraisingCampaignManager.RetrieveAllActiveFundraisingCampaigns();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message + "\n\n" + ex.InnerException.Message;
                return View("Error");
            }

            Func<FundraisingCampaignVM, string> sortMethod = null;
            switch (campaignsViewModel.Sort)
            {
                case null:
                case SortingMethod.StartDate:
                    sortMethod = new Func<FundraisingCampaign, string>(fc => fc.StartDate != null ? fc.StartDate.Value.Ticks.ToString() : DateTime.MaxValue.Ticks.ToString());
                    break;
                case SortingMethod.Title:
                    sortMethod = new Func<FundraisingCampaign, string>(fc => fc.Title);
                    break;
            }

            Func<FundraisingCampaignVM, bool> filterMethod = null;
            switch (campaignsViewModel.Filter)
            {
                case null:
                case FilterMethod.Ongoing:
                    filterMethod = new Func<FundraisingCampaign, bool>(fc => !fc.Complete && fc.Active);
                    break;
                case FilterMethod.Completed:
                    filterMethod = new Func<FundraisingCampaign, bool>(fc => fc.Complete && fc.Active);
                    break;
                case FilterMethod.Both:
                    filterMethod = new Func<FundraisingCampaign, bool>(fc => fc.Active);
                    break;
            }

            Func<FundraisingCampaignVM, bool> searchForTextInFundraisingCampaign = (campaign) => true;

            if (campaignsViewModel.Search != null && campaignsViewModel.Search.Trim() != "")
            {
                campaignsViewModel.Search = campaignsViewModel.Search.Trim();
                searchForTextInFundraisingCampaign = (fundraisingCampaign) =>
                {
                    return fundraisingCampaign.Title?.IndexOf(campaignsViewModel.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                            fundraisingCampaign.Description?.IndexOf(campaignsViewModel.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                            (fundraisingCampaign.StartDate != null ? fundraisingCampaign.StartDate.Value.ToString("MM/dd/yyyy").Contains(campaignsViewModel.Search) : false) ||
                            (fundraisingCampaign.EndDate != null ? fundraisingCampaign.EndDate.Value.ToString("MM/dd/yyyy").Contains(campaignsViewModel.Search) : false) ||
                            (fundraisingCampaign.StartDate != null ? fundraisingCampaign.StartDate.Value.ToString("M/d/yyyy").Contains(campaignsViewModel.Search) : false) ||
                            (fundraisingCampaign.EndDate != null ? fundraisingCampaign.EndDate.Value.ToString("M/d/yyyy").Contains(campaignsViewModel.Search) : false);
                };
            }


            campaigns = campaigns.OrderBy(sortMethod).Where(filterMethod).Where(searchForTextInFundraisingCampaign).ToList();
            pagingInfo.TotalItems = campaigns.Count;

            if (pagingInfo.CurrentPage > pagingInfo.TotalPages)
            {
                pagingInfo.CurrentPage = pagingInfo.TotalPages;
            }
            if (pagingInfo.CurrentPage < 1)
            {
                pagingInfo.CurrentPage = 1;
            }

            campaignsViewModel.Campaigns = campaigns.Skip(pagingInfo.ItemsPerPage * (pagingInfo.CurrentPage - 1)).Take(pagingInfo.ItemsPerPage);
            return View(campaignsViewModel);
        }

        /// <summary>
        /// Barry Mikulas
        /// Created 04/18/2023
        /// 
        /// Controller method for \Fundraising\event to view a single event
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// 2023/04/26
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="frEvent">the id of the event to view</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Event(int? frEvent)
        {
            FundraisingEventVM fundraisingEvent = null;
            try
            {

                if (frEvent == null
                    || (fundraisingEvent = _masterManger.FundraisingEventManager.RetrieveFundraisingEventByFundraisingEventId(frEvent.Value)) == null
                    || fundraisingEvent.Hidden)
                {
                    return RedirectToAction("Events");
                }
                fundraisingEvent.Sponsors = _masterManger.InstitutionalEntityManager.RetrieveFundraisingInstitutionalEntitiesByEventIdAndContactType(frEvent.Value, "Sponsor");
                fundraisingEvent.Contacts = _masterManger.InstitutionalEntityManager.RetrieveFundraisingInstitutionalEntitiesByEventIdAndContactType(frEvent.Value, "Contact");
                fundraisingEvent.Host = _masterManger.InstitutionalEntityManager.RetrieveInstitutionalEntityByEventIdAndContactType(frEvent.Value, "Host");
                if (fundraisingEvent.Host == null || fundraisingEvent.Host.InstitutionalEntityId == 0)
                {
                    fundraisingEvent.Host = new InstitutionalEntity()
                    {
                        Address = "No address on file.",
                        CompanyName = "No host on file."
                    };
                }
                // get campaign
                if (fundraisingEvent.CampaignId != null)
                {
                    fundraisingEvent.Campaign = _masterManger.FundraisingCampaignManager.RetrieveFundraisingCampaignByFundraisingCampaignId((int)fundraisingEvent.CampaignId);
                }
                else
                {
                    fundraisingEvent.Campaign = new FundraisingCampaignVM();
                    fundraisingEvent.Campaign.Title = "Not part of a campaign.";
                }
                // get animals for the event
                fundraisingEvent.Animals = MasterManager.GetMasterManager().AnimalManager.RetrieveAnimalsByFundrasingEventId((int)fundraisingEvent.FundraisingEventId);

                // get donations for the event
                //_amtRaised = MasterManager.GetMasterManager().DonationManager.RetrieveSumDonationsByEventId((int)fundraisingEvent.FundraisingEventId);
                return View(fundraisingEvent);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message + ex.InnerException.Message;
                return View("Error");
            }
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/04/16
        /// 
        /// Controller method for /Fundraising/Event to get a partial view for a single event
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// 2023/04/26
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="collection">The event from the view button click</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Event(FormCollection collection)
        {
            int? frEvent;
            try
            {
                frEvent = Convert.ToInt32(collection.Get("event"));
            }
            catch
            {
                frEvent = null;
            }
            FundraisingEventVM fundraisingEvent = null;
            try
            {
                if (frEvent == null
                    || (fundraisingEvent = _masterManger.FundraisingEventManager.RetrieveFundraisingEventByFundraisingEventId(frEvent.Value)) == null
                    || fundraisingEvent.Hidden)
                {
                    ViewBag.Message = "Could not find the selected event";
                    return PartialView("Error");
                }
                fundraisingEvent.Sponsors = _masterManger.InstitutionalEntityManager.RetrieveFundraisingInstitutionalEntitiesByEventIdAndContactType(frEvent.Value,"Sponsor");
                fundraisingEvent.Contacts = _masterManger.InstitutionalEntityManager.RetrieveFundraisingInstitutionalEntitiesByEventIdAndContactType(frEvent.Value, "Contact");
                fundraisingEvent.Host = _masterManger.InstitutionalEntityManager.RetrieveInstitutionalEntityByEventIdAndContactType(frEvent.Value, "Host");
                if (fundraisingEvent.Host == null || fundraisingEvent.Host.InstitutionalEntityId == 0)
                {
                    fundraisingEvent.Host = new InstitutionalEntity()
                    {
                        Address = "Stay tuned!",
                        CompanyName = "Coming Soon!"
                    };
                }
                // get campaign
                if (fundraisingEvent.CampaignId != null)
                {
                    fundraisingEvent.Campaign = _masterManger.FundraisingCampaignManager.RetrieveFundraisingCampaignByFundraisingCampaignId((int)fundraisingEvent.CampaignId);
                }
                else
                {
                    fundraisingEvent.Campaign = new FundraisingCampaignVM();
                    fundraisingEvent.Campaign.Title = "Not part of a campaign.";
                }
                // get animals for the event
                fundraisingEvent.Animals = MasterManager.GetMasterManager().AnimalManager.RetrieveAnimalsByFundrasingEventId((int)fundraisingEvent.FundraisingEventId);

                // get donations for the event
                //_amtRaised = MasterManager.GetMasterManager().DonationManager.RetrieveSumDonationsByEventId((int)fundraisingEvent.FundraisingEventId);

                return PartialView(fundraisingEvent);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message + ex.InnerException.Message;

                return PartialView("Error");
            }
        }

        /// <summary>
        /// Barry Mikulas
        /// Created: 2023/04/15
        /// 
        /// Controller method for /Fundraising/Events to view all public events
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// 2023/04/26
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="fundraisingEventsViewModel">The model holding the different parameters for searching and event data</param>
        /// <param name="Page">The current page of event we are on</param>
        /// <returns></returns>
        public ActionResult Events(FundraisingEventsViewModel fundraisingEventsViewModel, int Page = 1)
        {
            @ViewBag.Tab = "Fundraising";
            PagingInfo pagingInfo = fundraisingEventsViewModel.PagingInfo;
            pagingInfo.CurrentPage = Page;
            pagingInfo.ItemsPerPage = 10;

            List<FundraisingEventVM> fundraisingEvents = null;

            //retrieve list of shelters
            try
            {
                fundraisingEventsViewModel.Shelters = _masterManger.ShelterManager.GetShelterList()
                    .Where(shelter => shelter.ShelterActive).OrderBy(sh => sh.ShelterName);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }


            try
            {
                // check to see if shelter was chosen on view page
                if (fundraisingEventsViewModel.Shelter != null)
                {
                    fundraisingEvents = _masterManger.FundraisingEventManager.RetrieveAllActiveFundraisingEventsByShelterId(fundraisingEventsViewModel.Shelter.Value);
                }
                else
                {
                    fundraisingEvents = _masterManger.FundraisingEventManager.RetrieveAllActiveFundraisingEvents();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message + "\n\n" + ex.InnerException.Message;
                return View("Error");
            }

            Func<FundraisingEventVM, string> sortMethod = null;
            switch (fundraisingEventsViewModel.Sort)
            {
                case null:
                case SortingMethod.StartDate:
                    sortMethod = new Func<FundraisingEvent, string>(fc => fc.StartTime != null ? fc.StartTime.Value.Ticks.ToString() : DateTime.MaxValue.Ticks.ToString());
                    break;
                case SortingMethod.Title:
                    sortMethod = new Func<FundraisingEvent, string>(fc => fc.Title);
                    break;
            }

            Func<FundraisingEventVM, bool> filterMethod = null;
            switch (fundraisingEventsViewModel.Filter)
            {
                case null:
                case FilterMethod.Ongoing:
                    filterMethod = new Func<FundraisingEvent, bool>(fc => !fc.Complete && !fc.Hidden);
                    break;
                case FilterMethod.Completed:
                    filterMethod = new Func<FundraisingEvent, bool>(fc => fc.Complete && !fc.Hidden);
                    break;
                case FilterMethod.Both:
                    filterMethod = new Func<FundraisingEvent, bool>(fc => !fc.Hidden);
                    break;
            }

            Func<FundraisingEventVM, bool> searchForTextInFundraisingEvent = (frEvent) => true;

            if (fundraisingEventsViewModel.Search != null && fundraisingEventsViewModel.Search.Trim() != "")
            {
                fundraisingEventsViewModel.Search = fundraisingEventsViewModel.Search.Trim();
                searchForTextInFundraisingEvent = (fundraisingEvent) =>
                {
                    return fundraisingEvent.Title?.IndexOf(fundraisingEventsViewModel.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                            fundraisingEvent.Description?.IndexOf(fundraisingEventsViewModel.Search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                            (fundraisingEvent.StartTime != null ? fundraisingEvent.StartTime.Value.ToString("MM/dd/yyyy").Contains(fundraisingEventsViewModel.Search) : false) ||
                            (fundraisingEvent.EndTime != null ? fundraisingEvent.EndTime.Value.ToString("MM/dd/yyyy").Contains(fundraisingEventsViewModel.Search) : false) ||
                            (fundraisingEvent.StartTime != null ? fundraisingEvent.StartTime.Value.ToString("M/d/yyyy").Contains(fundraisingEventsViewModel.Search) : false) ||
                            (fundraisingEvent.EndTime != null ? fundraisingEvent.EndTime.Value.ToString("M/d/yyyy").Contains(fundraisingEventsViewModel.Search) : false);
                };
            }


            fundraisingEvents = fundraisingEvents.OrderBy(sortMethod).Where(filterMethod).Where(searchForTextInFundraisingEvent).ToList();
            pagingInfo.TotalItems = fundraisingEvents.Count;

            if (pagingInfo.CurrentPage > pagingInfo.TotalPages)
            {
                pagingInfo.CurrentPage = pagingInfo.TotalPages;
            }
            if (pagingInfo.CurrentPage < 1)
            {
                pagingInfo.CurrentPage = 1;
            }

            fundraisingEventsViewModel.Events = fundraisingEvents.Skip(pagingInfo.ItemsPerPage * (pagingInfo.CurrentPage - 1)).Take(pagingInfo.ItemsPerPage);
            return View(fundraisingEventsViewModel);
        }
    }
}