/// <summary>
/// Andrew Cromwell
/// Created: 2023/02/08
/// 
/// Interface that lists methods that ProcedureAccessor and ProcedureAccessorInterfaces must implament.
/// </summary>
/// 
/// < remarks >
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

namespace DataAccessLayerInterfaces
{
    public interface IProcedureAccessor
    {/// <summary>
     ///  Andrew Cromwell
     /// 2023/02/24
     /// 
     /// </summary>
     /// <remarks>
     /// Zaid Rachman
     /// Updated: 2023/04/27
     /// 
     /// Final QA
     /// </remarks>
     /// <param name="procedure"></param>
     /// <param name="medicalRecordId"></param>
     /// <returns></returns>
        int InsetProcedureByMedicalRecordId(Procedure procedure, int medicalRecordId);
        /// <summary>
        /// Andrew Cromwell
        /// 2023/02/24
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="procedure"></param>
        /// <param name="oldProcedure"></param>
        /// <returns></returns>
        int UpdateProcedureByProcedureId(Procedure procedure, Procedure oldProcedure);
        List<ProcedureVM> SelectProceduresByAnimalId(int animalId);

        /// <summary>
        /// Molly Meister
        /// Created: 2023/03/10
        /// 
        /// Gets the procedure for a specified medical record
        /// </summary>
        /// <param name="medicalRecordId">The medical record ID to get the procedure for</param>
        /// <returns>A ProcedureVM object</returns>
        ProcedureVM SelectProcedureByMedicalRecordId(int medicalRecordId);
    }
}
