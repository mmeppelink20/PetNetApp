using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Kennel
    {
        public int KennelId { get; set; }
        public int ShelterId { get; set; }
        public string KennelName { get; set; }
        public int KennelSpace { get; set; }
        public bool KennelActive { get; set; }
        public string AnimalTypeId { get; set; }
    }

    public class KennelVM : Kennel
    {
        public string ShelterName { get; set; }
        public AnimalVM Animal { get; set; }
    }
}
