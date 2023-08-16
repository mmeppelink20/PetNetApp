using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Inspection
    {
        public int InspectionId { get; set; }
        public int ApplicantId { get; set; }
        public int InspectionInspectorId { get; set; }
        public string InspectionComments { get; set; }
        public DateTime InspectionDateSchduled { get; set; }
        public DateTime InspectionDateCompleted { get; set; }
        public int InspectionAnimalCountApproved { get; set; }
        public bool InspectionPassed { get; set; }
    }

    public class InspectionVM : Inspection
    {
        public Applicant InspectionApplicant { get; set; }
        public Users Inspector { get; set; }
    }
}
