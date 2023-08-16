using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterfaces
{
    public interface IMedicalRecordManager
    {
        int RetrieveLastMedicalRecordIdByAnimalId(int animalId);

        List<MedicalRecordVM> RetrieveMedicalRecordDiagnosisByAnimalId(int animalId);

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/03/22
        /// 
        /// 
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="medicalRecordId"></param>
        /// /// <param name="newDiagnosis"></param>
        /// /// <param name="newMedicalNotes"></param>
        /// /// <param name="oldDiagnosis"></param>
        /// /// <param name="oldMedicalNotes"></param>
        /// <exception cref="ApplicationException">Insert Fails</exception>
        /// <returns>returns bool of the result of the update</returns>
        bool EditTreatmentByMedicalRecordId(int medicalRecordId, string newDiagnosis, string newMedicalNotes, string oldDiagnosis, string oldMedicalNotes);

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/25
        /// 
        /// Passes a MedicalRecord to the MedicalRecordAccessor to add to the database. Returns
        /// the Id of the record that was just added
        /// </summary>
        /// <param name="medicalRecord">the MedicalRecord to be added</param>
        /// <exception cref="ApplicationException">Insert Fails</exception>
        /// <returns>MeidicalRecordId of the inserted record</returns>
        int AddMedicalRecord(MedicalRecordVM medicalRecord);

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/02/01
        /// 
        /// Updates the Quarantine Status of a medical record
        /// </summary>
        /// 
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="medicalRecordId"></param>
        /// <param name="quarantineStatus"></param>
        /// <param name="oldQuarantineStatus"></param>
        /// <returns>True or false if row was edited</returns>
        bool EditQuarantineStatusByMedicalRecordId(int medicalRecordId, bool quarantineStatus, bool oldQuarantineStatus);

        /// <summary>
        /// Nathan Zumsande
        /// Created: 2023/09/02
        /// 
        /// Creates a new medical record and returns the Id
        /// </summary>
        /// 
        ///
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="animalId"></param>
        /// <param name="medicalNotes"></param>
        /// <param name="test"></param>
        /// <param name="diagnosis"></param>
        /// <returns>Id of the created medical record</returns>
        int AddTestMedicalRecordByAnimalId(int animalId, string medicalNotes, bool test, string diagnosis);

        /// <summary>
        /// Molly Meister
        /// Created: 2023/03/09
        /// 
        /// Passes an animalId to the MedicalRecordAccessor to retrieve medical records from the database.
        /// Returns a list of MedicalRecordVMs 
        /// </summary>
        /// <param name="animalId">the animalId of the animal retrieving medical records for</param>
        /// <exception cref="ApplicationException">Retrieval Fails</exception>
        /// <returns>A list of all MedicalRecordVMs for the passed animalId</returns>
        List<MedicalRecordVM> RetrieveAllMedicalRecordsByAnimalId(int animalId);

        /// <summary>
        /// Ethan Kline 
        /// Created: 2023/02/18
        /// </summary>
        /// <param name="animalId">the id of the animal to return the medical record of</param>
        /// <exception cref="ApplicationException">Faild to retrieve medical records</exception>
        /// <returns>List of the medical record associated with the animal</returns>
        List<MedicalRecordVM> SelectMedicalRecordByAnimal(int animalId);

        /// <summary>
        /// Ethan Kline 
        /// Created: 2023/02/18
        /// 
        /// Takes two medical notes, passes them to the MedicalRecordAccessor, receives a response 
        /// from the accessor, and returns a respnonse about whether the old medical record was
        /// updated to be the new medical record.
        /// </summary>
        /// <param name="medicalRecord">the medical record to replace the old medical record in the db</param>
        /// <param name="oldmedicalRecord">the medical record to be overwriten</param>
        /// <exception cref="ApplicationException">Update Fails</exception>
        /// <returns>Rows affected</returns>
        bool EditMedicalRecord(MedicalRecord oldmedicalRecord, MedicalRecord medicalRecord);

        /// <summary>
        /// Ethan Kline 
        /// Created: 2023/03/10
        /// 
        /// Takes a medical record, passes it to the MedicalRecordAccessor, receives a response 
        /// from the accessor, and returns a respnonse about whether the  medical record was added
        /// </summary>
        /// <remarks>
        /// Zaid Rachman
        /// Updated: 2023/04/21
        /// Final QA
        /// </remarks>
        /// <param name="medicalRecord">the medical record to be added</param>
        /// <exception cref="ApplicationException">add Failed</exception>
        /// <returns>Rows affected</returns>
        bool AddMedicalNote(MedicalRecord medicalRecord);
    }
}
