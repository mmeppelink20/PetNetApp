/// <summary>
/// Zaid Rachman
/// Created: 2023/02/11
/// 
/// </summary>
///
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/17
/// 
/// Final QA
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Vaccination
    {
        public int VaccineId { get; set; }
        public int MedicalRecordId { get; set; }
        public int UserId { get; set; }
        public string VaccineName { get; set; }
        public DateTime VaccineAdminsterDate { get; set; }
    }

    public class VaccinationVM : Vaccination
    {
        public string VaccinatorGivenName { get; set; }
        public string VaccinatorLastName { get; set; }
    }
}
