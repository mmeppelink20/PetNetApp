using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class FosterRequestResponse
    {
        public int FosterRequestResponseId { get; set; }
        public int FosterApplicantId { get; set; }
        public int FosterRequestId { get; set; }
        public bool FosterRequestResponseAccepted { get; set; }
        public string FosterRequestResponseNotes { get; set; }
    }

    public class FosterRequestResponseVM : FosterRequestResponse
    {
        public Applicant FosterRequestApplicant { get; set; }
        public FosterRequest FosterRequestResponseRequest { get; set; }
    }
}
