/// <summary>
/// Andrew Cromwell
/// Created: 2023/02/08
/// 
/// Class that includes fake Procedure records and methods for 
/// using them.
/// </summary>
/// 
///< remarks >
/// Andrew Cromwell
/// Updated: 2023/02/15
/// Added UpdateProcedureByMedicalRecordIdAndProcedureId and SelectProceduresByAnimalId
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;

namespace DataAccessLayerFakes
{
    public class ProcedureAccessorFakes : IProcedureAccessor
    {
        private int expectedProcedureIdForTest = 0;
        private int expectedMedicalRecordIdForTest = 0;
        private int expectedUserIdForTest = 0;

        private List<ProcedureVM> fakeProcedures = new List<ProcedureVM>()
        {
            new ProcedureVM()
            {
                ProcedureId = 111,
                MedicalRecordId = 111,
                UserId = 111,
                ProcedureName = "procedure name",
                MedicationsAdministered = "some meds were used.",
                ProcedureNotes = "notes",
                ProcedureDate = DateTime.Parse("2021-01-22"),
                SurgeonGivenName = "fred",
                SurgeonFamilyName = "flintstone"
            },
            new ProcedureVM()
            {
                ProcedureId = 666,
                MedicalRecordId = 666,
                UserId = 999,
                ProcedureName = "procedure name",
                MedicationsAdministered = "some meds were used.",
                ProcedureNotes = "notes to be overriden",
                ProcedureDate = DateTime.Parse("2021-01-22"),
                SurgeonGivenName = "Barny",
                SurgeonFamilyName = "fife"
            },
            new ProcedureVM()
            {
                ProcedureId = 333,
                MedicalRecordId = 333,
                UserId = 444,
                ProcedureName = "procedure name",
                MedicationsAdministered = "some meds were used.",
                ProcedureNotes = "notes",
                ProcedureDate = DateTime.Parse("2011-11-22")
            },
            new ProcedureVM()
            {
                ProcedureId = 555,
                MedicalRecordId = 555,
                UserId = 555,
                ProcedureName = "procedure name",
                MedicationsAdministered = "some meds were used.",
                ProcedureNotes = "notes to be overriden",
                ProcedureDate = DateTime.Parse("2021-01-22"),
                SurgeonGivenName = "oRiely",
                SurgeonFamilyName = "Auto"
            },
            new ProcedureVM()
            {
                ProcedureId = 777,
                MedicalRecordId = 777,
                UserId = 111,
                ProcedureName = "procedure name",
                MedicationsAdministered = "some meds were used.",
                ProcedureNotes = "notes",
                ProcedureDate = DateTime.Parse("2021-01-22")
            },
            new ProcedureVM()
            {
                ProcedureId = 222,
                MedicalRecordId = 222,
                UserId = 999,
                ProcedureName = "procedure name",
                MedicationsAdministered = "some meds were used.",
                ProcedureNotes = "notes to be overriden",
                ProcedureDate = DateTime.Parse("2021-01-22")
            }
        };

        private Dictionary<int, List<int>> AnimalIdToMedicalRecordIdRepresentation = new Dictionary<int, List<int>>()
        {
            {5, new List<int>() { 111, 666, 555 }},
            {8, new List<int>() { 333, 777, 222 }}


        };

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/08
        /// 
        /// Returns 2 if the procedure passed in has the expected values for the test
        /// </summary>
        /// <param name="procedure">the procedure that would be inserted in the non-fake class</param>
        /// <returns>Rows edited that would be edited in the real insert procedure</returns>
        public int InsetProcedureByMedicalRecordId(Procedure procedure, int medicalRecordId)
        {
            if(procedure.ProcedureId == expectedProcedureIdForTest || medicalRecordId == expectedMedicalRecordIdForTest
                || procedure.UserId == expectedUserIdForTest)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        public ProcedureVM SelectProcedureByMedicalRecordId(int medicalRecordId)
        {
            ProcedureVM procedure = null;
            foreach(var vm in fakeProcedures)
            {
                if(vm.MedicalRecordId == medicalRecordId)
                {
                    procedure = vm;
                }
            }
            return procedure;
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/15
        /// </summary>
        /// <param name="animalId">animalId to find the procedures associated with</param>
        /// <returns>procedureVMs that have a medicalRecordId that is associated with the animalId</returns>
        public List<ProcedureVM> SelectProceduresByAnimalId(int animalId)
        {
            List<ProcedureVM> returnList = new List<ProcedureVM>();
            List<int> medicalRecordIds = AnimalIdToMedicalRecordIdRepresentation[animalId];             
            var proceduresToReturn = fakeProcedures.Where(p => medicalRecordIds.Contains(p.MedicalRecordId));
            foreach(ProcedureVM procedure in proceduresToReturn)
            {
                returnList.Add(procedure);
            }
            return returnList;
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/15
        /// </summary>
        /// <param name="procedure">procedure that will overwrite an existing procedure</param>
        /// <param name="oldProcedure">procedure that will be overwriten</param>
        /// <param name="medicalRecordId">id of the medical record that the procedure belongs to</param>
        /// <returns>rows efected</returns>
        public int UpdateProcedureByProcedureId(Procedure procedure, Procedure oldProcedure)
        {
            int result = 0;
            foreach(ProcedureVM p in fakeProcedures)
            {
                if(p.ProcedureId == procedure.ProcedureId)
                {
                    p.UserId = procedure.UserId;
                    p.ProcedureName = procedure.ProcedureName;
                    p.MedicationsAdministered = procedure.MedicationsAdministered;
                    p.ProcedureNotes = procedure.ProcedureNotes;
                    p.ProcedureDate = procedure.ProcedureDate;
                    result = 1;
                }
            }

            return result;
        }
    }
}
