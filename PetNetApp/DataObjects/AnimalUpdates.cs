using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class AnimalUpdates
    {
        public int AnimalRecordId { get; set; }
        public int AnimalId { get; set; }
        public string AnimalRecordNotes { get; set; }
        public DateTime AnimalRecordDate { get; set; }
    }
    public class AnimalUpdatesVM : AnimalUpdates
    {
        public string AnimalName { get; set; }
    }
}
