using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataObjects
{
    public class FundraisingEvent
    {
        public int FundraisingEventId { get; set; }
        public int UsersId { get; set; }
        public int? CampaignId { get; set; }
        public int ShelterId { get; set; }
        public string ImageId { get; set; }
        public bool Hidden { get; set; }
        public string Title { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool Complete { get; set; }
        public string Description { get; set; }
        public string AdditionalInfo { get; set; }
        public decimal? Cost { get; set; }
        public int? NumOfAttendees { get; set; }
        public int? NumAnimalsAdopted { get; set; }
        public string UpdateNotes { get; set; }

    }

    public class FundraisingEventVM : FundraisingEvent
    {
        public List<InstitutionalEntity> Contacts { get; set; }
        public List<InstitutionalEntity> Sponsors { get; set; }
        public InstitutionalEntity Host { get; set; }
        public FundraisingCampaignVM Campaign { get; set; }
        public List<AnimalVM> Animals { get; set; }
    }

    public static class FundraisingEventExtensions
    {
        public static FundraisingEventVM Copy(this FundraisingEventVM fundraisingEventVM)
        {
            FundraisingEventVM copy = new FundraisingEventVM()
            {
                FundraisingEventId = fundraisingEventVM.FundraisingEventId,
                UsersId = fundraisingEventVM.UsersId,
                CampaignId = fundraisingEventVM.CampaignId,
                ShelterId = fundraisingEventVM.ShelterId,
                ImageId = fundraisingEventVM.ImageId,
                Complete = fundraisingEventVM.Complete,
                Hidden = fundraisingEventVM.Hidden,
                Title = fundraisingEventVM.Title,
                StartTime = fundraisingEventVM.StartTime,
                EndTime = fundraisingEventVM.EndTime,
                Description = fundraisingEventVM.Description,
                AdditionalInfo = fundraisingEventVM.AdditionalInfo,
                Cost = fundraisingEventVM.Cost,
                NumOfAttendees = fundraisingEventVM.NumOfAttendees,
                NumAnimalsAdopted = fundraisingEventVM.NumAnimalsAdopted,
                UpdateNotes = fundraisingEventVM.UpdateNotes,
                Sponsors = fundraisingEventVM.Sponsors.ToList(),
                Contacts = fundraisingEventVM.Contacts.ToList(),
                Host = fundraisingEventVM.Host,
                Campaign = fundraisingEventVM.Campaign,
                Animals = fundraisingEventVM.Animals.ToList()
            };

            return copy;
        }
    }

}
