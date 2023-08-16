using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class FosterPlacementRecord
    {
        public int FosterPlacementRecordId { get; set; }
        public int FosterPlacementId { get; set; }
        public string FosterPlacementRecordNotes { get; set; }
        public DateTime FosterPlacementRecordDate { get; set; }
    }
}
