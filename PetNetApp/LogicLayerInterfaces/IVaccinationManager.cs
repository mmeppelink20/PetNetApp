/// <summary>
/// Zaid Rachman
/// Created: 2023/02/09
/// 
/// Logic layer interface for the VaccinationManager
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd
/// </remarks>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IVaccinationManager
    {


        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/02/09
        /// 
        /// Inserts a new Vaccination. Takes in an animalId and a Vaccination object as parameters.
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/17
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="vaccine"></param>
        /// <param name="animalId"></param>
        /// <returns></returns>
        bool AddVaccination(Vaccination vaccine, int animalId);
        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/02/09
        /// 
        /// Retrieves a list of vaccination by Animal Id. Takes in an animal Id as a parameter
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/17
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="animalId"></param>
        /// <returns></returns>
        List<Vaccination> RetrieveVaccinationsByAnimalId(int animalId);
        /// <summary>
        /// Zaid Rachman
        /// Created: 2023/02/09
        /// 
        /// Updates Vaccination record. Takes in the oldVaccine and new Vaccine objects as parameters
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/17
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="oldVaccine"></param>
        /// <param name="newVaccine"></param>
        /// <returns></returns>
        bool EditVaccination(Vaccination oldVaccine, Vaccination newVaccine);

        /// <summary>
        /// Molly Meister
        /// Created: 2023/03/10
        /// 
        /// Calls the Accessor method to retrieve the vaccination associated with the specified medical record
        /// </summary>
        /// <param name="medicalRecordId">The ID of the medical record to get vaccination for</param>
        /// <exception cref="ApplicationException">If the retrieval fails</exception>
        /// <returns>A VaccinationVM object for the medical record</returns>
        VaccinationVM RetrieveVaccinationByMedicalRecordId(int medicalRecordId);
    }
}
