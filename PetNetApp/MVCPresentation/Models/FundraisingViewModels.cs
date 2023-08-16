using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;
using System.ComponentModel.DataAnnotations;

namespace MVCPresentation.Models
{
    public class CampaignsViewModel
    {
        public PagingInfo PagingInfo { get; private set; }
        [Display(Name = "Sort")]
        public SortingMethod? Sort { get; set; }
        [Display(Name = "Filter")]
        public FilterMethod? Filter { get; set; }
        public IEnumerable<FundraisingCampaignVM> Campaigns { get; set; }
        public IEnumerable<Shelter> Shelters { get; set; }
        public string Search { get; set; }
        public int? Shelter { get; set; }
        public CampaignsViewModel()
        {
            PagingInfo = new PagingInfo();
        }
    }
    public class FundraisingEventsViewModel
    {
        public PagingInfo PagingInfo { get; private set; }
        [Display(Name = "Sort")]
        public SortingMethod? Sort { get; set; }
        [Display(Name = "Filter")]
        public FilterMethod? Filter { get; set; }
        public IEnumerable<FundraisingEventVM> Events { get; set; }
        public IEnumerable<Shelter> Shelters { get; set; }
        public string Search { get; set; }
        public int? Shelter { get; set; }
        public FundraisingEventsViewModel()
        {
            PagingInfo = new PagingInfo();
        }
    }

    public class FundraisingCampaignForCampaignsModel
    {
        public FundraisingCampaignVM FundraisingCampaign { get; set; }
        public bool AlternateColor { get; set; }
    }

    public class FundraisingEventForEventsModel
    {
        public FundraisingEventVM FundraisingEvent { get; set; }
        public bool AlternateColor { get; set; }
    }

    public enum SortingMethod
    {
        [Display(Name = "Start Date")]
        StartDate,
        Title
    }
    public enum FilterMethod
    {
        Ongoing,
        Completed,
        Both
    }
}