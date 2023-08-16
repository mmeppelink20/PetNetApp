using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class FosterPlacement
    {
        public int FosterPlacementId { get; set; }
        public int AnimalId { get; set; }
        public int ApplicantId { get; set; }
        public DateTime FosterStartDate { get; set; }
        public DateTime FosterEndDate { get; set; }
        public bool FosterAnimalReturned { get; set; }
    }

    public class FosterPlacementVM : FosterPlacement
    {
        public Animal FosterPlacementAnimal { get; set; }
        public Applicant FosterPacementApplicant { get; set; }
        public List<FosterPlacementRecord> FosterPlacementNotes { get; set; }
    }
}
