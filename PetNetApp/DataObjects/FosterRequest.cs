using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class FosterRequest
    {
        public int FosterRequestId { get; set; }
        public int FosterRequestShelterId { get; set; }
        public int UserId { get; set; }
        public string FosterRequestMessage { get; set; }
    }

    public class FosterRequestVM : FosterRequest
    {
        public Shelter FosterRequestShelter { get; set; }
        public Users FosterRequestEmployee { get; set; }
        public List<Animal> FosterRequestAnimals { get; set; }
        public List<Applicant> ApprovedFosterApplicants { get; set; }
        public List<FosterRequestResponseVM> FosterRequestResponses { get; set; }
    }
}
