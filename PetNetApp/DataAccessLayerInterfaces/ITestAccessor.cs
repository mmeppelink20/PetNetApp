using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerInterfaces
{
    public interface ITestAccessor
    {
        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/23
        /// 
        /// Gets a list of tests for the specified animal
        /// </summary>
        /// <param name="animalId">The animals ID to get tests for</param>
        /// <returns>Returns a list of tests</returns>
        List<Test> SelectTestsByAnimalId(int animalId);

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/02/09
        /// 
        /// Inserts new Test into database with the passed test and medicalRecordId
        /// </summary>
        /// 
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="test"></param>
        /// <param name="medicalRecordId"></param>
        /// <exception cref="SQLException">Insert Fails</exception>
        /// <returns>Rows edited</returns>
        int InsertTestByMedicalRecordId(Test test, int medicalRecordId);

        /// <summary>
        /// Molly Meister
        /// Created: 2023/03/10
        /// 
        /// Gets the test for a specified medical record
        /// </summary>
        /// <param name="medicalRecordId">The medical record ID to get the test for</param>
        /// <returns>A TestVM object</returns>
        TestVM SelectTestByMedicalRecordId(int medicalRecordId);
    }
}
