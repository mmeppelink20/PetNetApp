using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;

namespace DataAccessLayerInterfaces
{
    /// <summary>
    /// Brian Collum
    /// Created: 2023/02/23
    /// IShelterAccessor interface governing access to the ShelterAccessor class in DataAccessLayer
    /// </summary>
    /// <remarks>
    /// Zaid Rachman
    /// Updated: 2023/04/27
    /// 
    /// Final QA
    /// </remarks>
    public interface IShelterAccessor
    {
        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// This returns the list of all Shelters so that the list of shelter objects can be populated
        /// </summary>
        /// 
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/14
        /// 
        /// FinalQA
        /// </remarks>
        /// <exception cref="SQLException">Can throw an SQL Exception if retrieval fails</exception>
        /// <returns>A list of Shelter objects</returns>
        List<Shelter> RetrieveShelterList();

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Adds a new shelter to the database
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="shelterName">The name of the shelter</param>
        /// <param name="address">the address of the shelter</param>
        /// <param name="Address2">the extended address information of the shelter</param>
        /// <param name="zipCode">the zipcode of the shelter</param>
        /// <exception cref="SQLException">Zipcode will throw SQLExceptions when the user enters a zipcode that is not present in the database</exception>
        /// <param name="phone">the phone number of the shelter</param>
        /// <param name="email">the email address of the shelter</param>
        /// <param name="areasOfNeed">the areas of need of the shelter</param>
        /// <param name="shelterActive">Whether the shelter is active or not</param>
        /// <exception cref="SQLException">All can throw SQLExceptions</exception>
        /// <returns>Boolean containing whether the operation succeeded or failed</returns>
        bool InsertShelter(
            string shelterName
            , string address
            , string Address2
            , string zipCode
            , string phone
            , string email
            , string areasOfNeed
            , bool shelterActive);

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Updates the name of a shelter
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="shelterID">The ID of the shelter</param>
        /// <param name="oldShelterName">The old name of the shelter</param>
        /// <param name="newShelterName">The new name of the shelter</param>
        /// <exception cref="SQLException">All can throw SQLExceptions</exception>
        /// <returns>Integer number of rows affected</returns>
        int UpdateShelterNameByShelterID(int shelterID, string oldShelterName, string newShelterName);

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Updates the address of a shelter
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="shelterID">The ID of the shelter</param>
        /// <param name="oldAddress">The old address of the shelter</param>
        /// <param name="newAddress">The new address of the shelter</param>
        /// <exception cref="SQLException">All can throw SQLExceptions</exception>
        /// <returns>Integer number of rows affected</returns>
        int UpdateAddressByShelterID(int shelterID, string oldAddress, string newAddress);

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Updates the Address2 of a shelter
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="shelterID">The ID of the shelter</param>
        /// <param name="newAddress2">The new Address2 of the shelter</param>
        /// <exception cref="SQLException">All can throw SQLExceptions</exception>
        /// <returns>Integer number of rows affected</returns>
        int UpdateAddress2ByShelterID(int shelterID, string newAddress2);

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Updates the zipcode of a shelter
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="shelterID">The ID of the shelter</param>
        /// <param name="oldZipCode">The old zipcode of the shelter</param>
        /// <param name="newZipCode">The new zipcode of the shelter</param>
        /// <exception cref="SQLException">All can throw SQLExceptions</exception>
        /// <returns>Integer number of rows affected</returns>
        int UpdateZipCodeByShelterID(int shelterID, string oldZipCode, string newZipCode);

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Updates the phone number of a shelter
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="shelterID">The ID of the shelter</param>
        /// <param name="newPhone">The new phone number of the shelter</param>
        /// <exception cref="SQLException">All can throw SQLExceptions</exception>
        /// <returns>Integer number of rows affected</returns>
        int UpdatePhoneByShelterID(int shelterID, string newPhone);

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Updates the email of a shelter
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="shelterID">The ID of the shelter</param>
        /// <param name="newEmail">The new email address of the shelter</param>
        /// <exception cref="SQLException">All can throw SQLExceptions</exception>
        /// <returns>Integer number of rows affected</returns>
        int UpdateEmailByShelterID(int shelterID, string newEmail);

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Updates the areas of need of a shelter
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="shelterID">The ID of the shelter</param>
        /// <param name="newAreasOfNeed">The new areas of need of the shelter</param>
        /// <exception cref="SQLException">All can throw SQLExceptions</exception>
        /// <returns>Integer number of rows affected</returns>
        int UpdateAreasOfNeedByShelterID(int shelterID, string newAreasOfNeed);

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Updates the active status of a shelter
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="shelterID">The ID of the shelter</param>
        /// <param name="oldActiveStatus">The old active status of the shelter</param>
        /// <param name="newActiveStatus">The new active status of the shelter</param>
        /// <exception cref="SQLException">All can throw SQLExceptions</exception>
        /// <returns>Integer number of rows affected</returns>
        int UpdateActiveStatusByShelterID(int shelterID, bool oldActiveStatus, bool newActiveStatus);

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Deactivates a shelter
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/27
        /// 
        /// Final QA
        /// </remarks>
        /// <param name="shelterID">The ID of the shelter</param>
        /// <exception cref="SQLException">All can throw SQLExceptions</exception>
        /// <returns>Integer number of rows affected</returns>
        int DeactivateShelterByShelterID(int shelterID);

        /// <summary>
        /// Brian Collum
        /// Created: 2023/02/23
        /// Selects a ShelterVM object to pass on
        /// </summary>
        /// <remarks>
        /// Oleksiy Fedchuk
        /// Updated: 2023/04/13
        /// 
        /// FinalQA
        /// </remarks>
        /// <param name="shelterID">The ID of the shelter</param>
        /// <exception cref="SQLException">All can throw SQLExceptions</exception>
        /// <returns>A ShelterVM object</returns>
        ShelterVM SelectShelterVMByShelterID(int shelterID);

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/02/23
        /// Gets the hours of operation for a shelter on a specific day.
        /// </summary>
        /// <param name="shelterID">The ID of the shelter</param>
        /// <exception cref="SQLException">All can throw SQLExceptions</exception>
        /// <returns>A ShelterVM object</returns>
        List<HoursOfOperation> SelectHoursOfOperationByShelterID(int shelterID);

        /// <summary>
        /// Teft Francisco
        /// Created: 2023/02/23
        /// Sets the hours of operation for a shelter on a specific day.
        /// </summary>
        /// <param name="shelterID">The ID of the shelter</param>
        /// <param name="dayOfWeek">The day of the week as 1 for sunday through 7 as saturday</param>
        /// <param name="hours">Hours to update</param>
        /// <exception cref="SQLException">All can throw SQLExceptions</exception>
        /// <returns>A ShelterVM object</returns>
        int UpdateHoursOfOperationByShelterID(int shelterID, int dayOfWeek, HoursOfOperation hours);
    }
}
