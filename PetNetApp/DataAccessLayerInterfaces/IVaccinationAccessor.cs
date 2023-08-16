/// <summary>
/// Zaid Rachman
/// Created: 2023/02/09
/// 
/// Vaccination Accessor Interface for the VaccinationAccessor
/// 
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

namespace DataAccessLayerInterfaces
{
    public interface IVaccinationAccessor
    {
        /// <summary>
        /// Zaid Rachman
        /// 2023/02/09
        /// 
        /// Inserts new vaccination. Takes in a Vaccination object and animalId as input
        /// return the number of rows updated
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/17
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="vaccination"></param>
        /// <param name="animalId"></param>
        /// <returns></returns>
        int InsertVaccination(Vaccination vaccination, int animalId);

        /// <summary>
        /// Zaid Rachman
        /// 2023/02/09
        /// 
        /// Retrieves a list of vaccinations by animal Id. Takes in an animal Id as a parameter
        /// returns list of Vaccination objects
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
        List<Vaccination> SelectVaccinationsByAnimalId(int animalId);

        /// <summary>
        /// Zaid Rachman
        /// 2023/02/09
        /// Updates Vaccination object. Takes in oldVaccination and newVaccination objects as parameters.
        /// returns the number of rows updated
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/17
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="oldVaccination"></param>
        /// <param name="newVaccination"></param>
        /// <returns></returns>
        int UpdateVaccination(Vaccination oldVaccination, Vaccination newVaccination);

        /// <summary>
        /// Molly Meister
        /// Created: 2023/03/10
        /// 
        /// Gets the vaccine for a specified medical record
        /// </summary>
        /// <param name="medicalRecordId">The medical record ID to get the vaccine for</param>
        /// <returns>A VaccineVM object</returns>
        VaccinationVM SelectVaccinationByMedicalRecordId(int medicalRecordId);
    }
}
