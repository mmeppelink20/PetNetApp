using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class FosterApplication
    {
        public int FosterApplicationId { get; set; }
        public int ApplicantId { get; set; }
        public string ApplicationStatusId { get; set; }
        public DateTime FosterApplicationDate { get; set; }
        public DateTime FosterApplicationStartDate { get; set; }
        public int FosterApplicationMaxAnimals { get; set; }
    }

    public class FosterApplicationVM : FosterApplication
    {
        public FosterApplicationResponseVM Response { get; set; }
        public Applicant FosterApplicationApplicant { get; set; }
        public ApplicationStatus FosterApplicationStatus { get; set; }
        public List<AnimalType> AcceptedAnimalTypes { get; set; }
    }
}
