/// <summary>
/// Andrew Cromwell
/// Created: 2023/02/08
/// 
/// Class for the creation of Procedure Objects with set data fields
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Procedure
    {
        public int ProcedureId { get; set; }
        public int MedicalRecordId { get; set; }
        public int UserId { get; set; }
        public string ProcedureName { get; set; }
        public string MedicationsAdministered { get; set; }
        public string ProcedureNotes { get; set; }
        public DateTime ProcedureDate { get; set; }
    }

    public class ProcedureVM : Procedure
    {
        public string SurgeonGivenName { get; set; }
        public string SurgeonFamilyName { get; set; }
    }
}
