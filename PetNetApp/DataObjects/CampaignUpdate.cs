using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class CampaignUpdate
    {
        public int CampaignUpdateId { get; set; }
        public int CampaignId { get; set; }
        public string UpdateTitle { get; set; }
        public string UpdateDescription { get; set; }
    }
}
