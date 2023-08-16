/// <summary>
/// Author: Stephen Jaurigue
/// Date: 2023/04/21
/// 
/// </summary>
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/27
/// 
/// Final QA
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class FundraisingCampaign
    {
        public int FundraisingCampaignId { get; set; }
        public string Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public bool Complete { get; set; }
        public int UsersId { get; set; }
        public int ShelterId { get; set; }
        public bool Active { get; set; }
        public decimal AmountRaised { get; set; }
        public int NumOfAttendees { get; set; }
        public int NumAnimalsAdopted { get; set; }
    }

    public class FundraisingCampaignVM : FundraisingCampaign
    {
        public List<InstitutionalEntity> Sponsors { get; set; }
        public List<FundraisingEvent> FundraisingEventList { get; set; }
    }

    public static class FundraisingCampaignExtensions
    {
        public static FundraisingCampaignVM Copy(this FundraisingCampaignVM fundraisingCampaignVM)
        {
            FundraisingCampaignVM copy = new FundraisingCampaignVM()
            {
                FundraisingCampaignId = fundraisingCampaignVM.FundraisingCampaignId,
                ShelterId = fundraisingCampaignVM.ShelterId,
                UsersId = fundraisingCampaignVM.UsersId,
                Title = fundraisingCampaignVM.Title,
                Description = fundraisingCampaignVM.Description,
                Complete = fundraisingCampaignVM.Complete,
                StartDate = fundraisingCampaignVM.StartDate,
                EndDate = fundraisingCampaignVM.EndDate,
                Sponsors = fundraisingCampaignVM.Sponsors.ToList(),
                Active = fundraisingCampaignVM.Active
            };

            return copy;
        }
    }
}
