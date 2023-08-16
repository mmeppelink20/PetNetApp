using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class AdoptionApplication
    {
        public int AdoptionApplicationId { get; set; }
        public int ApplicantId { get; set; }
        public int AnimalId { get; set; }
        public string ApplicationStatusId { get; set; }
        public DateTime AdoptionApplicationDate { get; set; }
    }

    public class AdoptionApplicationVM : AdoptionApplication
    {
        public Animal AdoptionAnimal { get; set; }
        public AdoptionApplicationResponse Response { get; set; }
        public Applicant AdoptionApplicant { get; set; }
        public ApplicationStatus Status { get; set; }
    }
}
