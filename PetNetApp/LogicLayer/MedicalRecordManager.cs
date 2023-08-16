using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayerInterfaces;
using DataAccessLayerInterfaces;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class MedicalRecordManager : IMedicalRecordManager
    {
        private IMedicalRecordAccessor _medicalRecordAccessor = null;

        public MedicalRecordManager()
        {
            _medicalRecordAccessor = new MedicalRecordAccessor();
        }

        public MedicalRecordManager(IMedicalRecordAccessor medicalRecordAccessor)
        {
            _medicalRecordAccessor = medicalRecordAccessor;
        }

        public int AddMedicalRecord(MedicalRecordVM medicalRecord)
        {
            int medicalRecordId = 0;
            try
            {
                medicalRecordId = _medicalRecordAccessor.InsertMedicalRecord(medicalRecord);

            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occored. The medical record could not be created.", ex);
            }
            return medicalRecordId;
        }

        public List<MedicalRecordVM>  RetrieveAllMedicalRecordsByAnimalId(int animalId)
        {
            List<MedicalRecordVM> medicalRecords = null;
            try
            {
                medicalRecords = _medicalRecordAccessor.SelectAllMedicalRecordsByAnimald(animalId);
            }
            catch (Exception up)
            {
                throw new ApplicationException("There was an error retrieving data.");
            }
            return medicalRecords;
        }

        public int RetrieveLastMedicalRecordIdByAnimalId(int animalId)
        {
            int medicalRecordId = 0;
            try
            {
                medicalRecordId = _medicalRecordAccessor.SelectLastMedicalRecordIdByAnimalId(animalId);

            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occored. The medical record could not be retreived.", ex);
            }
            return medicalRecordId;
        }

        public List<MedicalRecordVM> RetrieveMedicalRecordDiagnosisByAnimalId(int animalId)
        {
            List<MedicalRecordVM> medicalRecords = null;
            try
            {
                medicalRecords = _medicalRecordAccessor.SelectMedicalRecordDiagnosisByAnimalId(animalId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an error retrieving data", ex);
            }
            return medicalRecords;
        }

        public bool EditTreatmentByMedicalRecordId(int medicalRecordId, string newDiagnosis, string newMedicalNotes, string oldDiagnosis, string oldMedicalNotes)
        {
            bool result = false;
            try
            {
                result = 1 == _medicalRecordAccessor.UpdateMedicalTreatmentByMedicalrecordId(medicalRecordId, newDiagnosis, newMedicalNotes, oldDiagnosis, oldMedicalNotes);
                if (!result)
                {
                    throw new ApplicationException("\na data concurrency error occured, refreshing page; try again.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool EditQuarantineStatusByMedicalRecordId(int medicalRecordId, bool quarantineStatus, bool oldQuarantineStatus)
        {
            bool result = false;
            try
            {
                result = 1 == _medicalRecordAccessor.UpdateQuarantineStatusByMedicalRecordId(medicalRecordId, quarantineStatus, oldQuarantineStatus);
                if (!result)
                {
                    throw new ApplicationException("Concurrency Conflict");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// Ethan Kline
        /// Created: 2023/03/1
        /// </summary>
        /// <param name="animalId">the id of the animal to return the medical records of</param>
        /// <exception cref="ApplicationException">Faild to retrieve records</exception>
        /// <returns>List of the records associated with the animal</returns>
        public List<MedicalRecordVM> SelectMedicalRecordByAnimal(int animalId)
        {
            List<MedicalRecordVM> MedicalRecords;

            try
            {
                MedicalRecords = _medicalRecordAccessor.SelectMedicalRecordByAnimal(animalId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.", ex);
            }

            return MedicalRecords;
        }

        /// Ethan Kline
        ///  /// Created: 2023/03/1
        /// </summary>
        /// <param name="oldmedicalRecord">the record to replace</param>
        /// <param name="medicalRecord">the new record to update</param>
        /// <exception cref="ApplicationException">Faild to update record </exception>
        /// <returns>true if the record updated</returns>
        /// </summary>
        
        /// <returns></returns>
        public bool EditMedicalRecord(MedicalRecord oldmedicalRecord, MedicalRecord medicalRecord)
        {
            bool result = false;
            try
            {
                result = (1 == _medicalRecordAccessor.UpdateMedicalRecord(oldmedicalRecord, medicalRecord));

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update failed.", ex);
            }
            return result;
        }
        /// Ethan Kline
        ///  /// Created: 2023/03/10
        /// </summary>
        /// <param name="medicalRecord">the record to add</param>
        /// <exception cref="ApplicationException">Faild to add record </exception>
        /// <returns>true if the record added</returns>
        /// </summary>

        public bool AddMedicalNote(MedicalRecord medicalRecord)
        {
            bool result = false;
            try
            {
                result = (1 == _medicalRecordAccessor.AddMedicalNotes(medicalRecord));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Add failed.", ex); ;
            }
            return result;
        }

        public int AddTestMedicalRecordByAnimalId(int animalId, string medicalNotes, bool test, string diagnosis)
        {
            int medicalRecordId;
            try
            {
                medicalRecordId = _medicalRecordAccessor.InsertTestMedicalRecordByAnimalId(animalId, medicalNotes, test, diagnosis);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return medicalRecordId;
        }
    }
}
