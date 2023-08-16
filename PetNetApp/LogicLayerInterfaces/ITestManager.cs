using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface ITestManager
    {
        /// <summary>
        /// Stephen Jaurigue
        /// Created: 2023/02/23
        /// 
        /// Calls the Accessor method to retrieve all tests for an animal
        /// </summary>
        /// <param name="animalId">The ID of the animal to get tests for</param>
        /// <exception cref="ApplicationException">Thrown if something goes wrong running the stored procedure</exception>
        /// <returns>A list of tests for the animal</returns>
        List<Test> RetrieveTestsByAnimalId(int animalId);

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/02/09
        /// 
        /// Creates a new Test
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
        /// <exception cref="ApplicationException">If there is a concurrency issue and no rows where inserted</exception>
        /// <returns>True or false if row was edited</returns>
        bool AddTestByMedicalRecordId(Test test, int medicalRecordId);

        /// <summary>
        /// Molly Meister
        /// Created: 2023/03/10
        /// 
        /// Calls the Accessor method to retrieve the test associated with the specified medical record
        /// </summary>
        /// <param name="medicalRecordId">The ID of the medical record to get test for</param>
        /// <exception cref="ApplicationException">If the retrieval fails</exception>
        /// <returns>A TestVM object for the medical record</returns>
        TestVM RetrieveTestByMedicalRecordId(int medicalRecordId);
    }
}
