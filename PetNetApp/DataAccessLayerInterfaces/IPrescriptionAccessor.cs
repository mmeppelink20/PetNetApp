using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerInterfaces
{
   public  interface IPrescriptionAccessor
    {

        /// <summary>
        /// Tyler Hand 
        /// Created: 2023/25/02
        /// Methods selects Prescriptions from the database
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated : 2023/04/28
        ///  Final QA
        /// </remarks>
        /// <param name="animalId"></param>
        /// <returns></returns>
        List<PrescriptionVM> SelectAllPrescriptions(int animalId);



        /// <summary>
        /// Tyler Hand 
        /// Created: 2023/25/02
        /// Method inserting prescriptions to the database
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated : 2023/04/28
        ///  Final QA
        /// </remarks>
        /// <param name="prescription"></param>
        /// <param name="medicalRecordId"></param>
        /// <returns></returns>
        int InsetPrescriptionByMedicalRecordId(Prescription prescription, int medicalRecordId);


    }
}
