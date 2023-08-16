using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

// Based on FundraisingViewModels.cs
namespace MVCPresentation.Models
{
    public class DonationsViewModel
    {
        public DonationSortOptions? Sort { get; set; }
        public MiscellaneousFilterOptions? MiscellaneousFilterOptions { get; set; }
        public AmountFilterOptions? AmountFilterOptions { get; set; }
        public DateFilterOptions? DateFilterOptions { get; set; }
        public IEnumerable<DonationVM> DonationVMs { get; set; }
        public IEnumerable<Shelter> Shelters { get; set; }
        public int? Shelter { get; set; }
        
        public int Count { get; set; }
    }

    public enum DonationSortOptions
    {
        [Display(Name = "Sort by Amount")]
        Amount,
        [Display(Name = "Sort by Date")]
        Date,
        [Display(Name = "Sort by Name")]
        FamilyName
    }


    public enum MiscellaneousFilterOptions
    {
        [Display(Name = "Has Message")]
        HasMessage,
        [Display(Name = "Has No Message")]
        HasNoMessage
    }

    public enum AmountFilterOptions
    {
        [Display(Name = "$1 to $25")]
        OneToTwentyFive,
        [Display(Name = "$26 to $50")]
        TwentyFiveToFifty,
        [Display(Name = "$51 to $100")]
        FiftyToOneHundred,
        [Display(Name = "$101 to $500")]
        OneHundredToFiveHundred,
        [Display(Name = "$501 or More")]
        OverFiveHundred
    }

    public enum DateFilterOptions
    {
        [Display(Name = "Last Day")]
        LastDay,
        [Display(Name = "Last Week")]
        LastWeek,
        [Display(Name = "Last Month")]
        LastMonth,
        [Display(Name = "Last 6 Months")]
        LastSixMonths,
        [Display(Name = "Last Year")]
        LastYear
    }
}