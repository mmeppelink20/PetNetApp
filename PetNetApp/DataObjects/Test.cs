using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Test
    {
        public int TestId { get; set; }
        public int MedicalRecordId { get; set; }
        public int UserId { get; set; }
        public string TestName { get; set; }
        public string TestAcceptableRange { get; set; }
        public string TestResult { get; set; }
        public string TestNotes { get; set; }
        public DateTime TestDate { get; set; }
    }

    public class TestVM : Test
    {
        public string TesterGivenName { get; set; }
        public string TestFamilyName { get; set; }
    }
}
