using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class AdoptionPlacement
    {
        public int AdoptionPlacementId { get; set; }
        public int AnimalId { get; set; }
        public int ApplicantId { get; set; }
        public DateTime AdoptionPlacementDate { get; set; }
    }

    public class AdoptionPlacementVM : AdoptionPlacement
    {
        public Animal AdoptionPlacementAnimal { get; set; }
        public Applicant AdoptionPlacementApplicant { get; set; }
    }
}
