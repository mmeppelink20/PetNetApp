using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class MedicalRecordAccessor : IMedicalRecordAccessor
    {
        public int SelectLastMedicalRecordIdByAnimalId(int animalId)
        {
            int medicalRecordId = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_last_medical_record_by_animal_id";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AnimalId", animalId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    medicalRecordId = reader.GetInt32(0);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return medicalRecordId;
        }
        
        public List<MedicalRecordVM> SelectMedicalRecordDiagnosisByAnimalId(int animalId)
        {
            List<MedicalRecordVM> medicalRecords = new List<MedicalRecordVM>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_medical_record_diagnosis_by_animalid";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AnimalId", SqlDbType.Int);

            cmd.Parameters["@AnimalId"].Value = animalId;

            try
            {
                // open a connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var medicalRecord = new MedicalRecordVM();
                        medicalRecord.MedicalRecordId = reader.GetInt32(0);
                        medicalRecord.Diagnosis = reader.GetString(1);
                        medicalRecord.QuarantineStatus = reader.GetBoolean(2);
                        medicalRecord.IsPrescription = reader.GetBoolean(3);
                        medicalRecord.MedicalNotes = reader.GetString(4);
                        medicalRecord.Date = reader.GetDateTime(5);
                        medicalRecords.Add(medicalRecord);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
            return medicalRecords;
        }

        
        public int UpdateMedicalTreatmentByMedicalrecordId(int medicalRecordId, string newDiagnosis, string newMedicalNotes, string oldDiagnosis, string oldMedicalNotes)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_update_medical_treatment";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@recordId", medicalRecordId);
            cmd.Parameters.AddWithValue("@newDiagnosis", newDiagnosis);
            cmd.Parameters.AddWithValue("@newMedicalNotes", newMedicalNotes);
            cmd.Parameters.AddWithValue("@oldDiagnosis", oldDiagnosis);
            cmd.Parameters.AddWithValue("@oldMedicalNotes", oldMedicalNotes);


            cmd.Parameters["@recordId"].Value = medicalRecordId;

            cmd.Parameters["@newDiagnosis"].Value = newDiagnosis;
            cmd.Parameters["@newMedicalNotes"].Value = newMedicalNotes;

            cmd.Parameters["@oldDiagnosis"].Value = oldDiagnosis;
            cmd.Parameters["@oldMedicalNotes"].Value = oldMedicalNotes;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public int InsertMedicalRecord(MedicalRecordVM medicalRecord)
        {
            int newRecordId;
            //connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            //Transaction
            SqlTransaction trans = null;
            //command texts
            var cmdTextAddMedicalRecord = "sp_insert_medical_record";
            var cmdTextAddVaccination = "sp_insert_vaccination";

            try
            {
                //Open the connection
                conn.Open();
                // begin the transaction
                trans = conn.BeginTransaction();

                //command
                SqlCommand cmdAddMedicalRecord = new SqlCommand(cmdTextAddMedicalRecord, conn, trans);
                cmdAddMedicalRecord.CommandType = CommandType.StoredProcedure;

                //parameters
                cmdAddMedicalRecord.Parameters.Add("@AnimalId", SqlDbType.Int);


                cmdAddMedicalRecord.Parameters["@AnimalId"].Value = medicalRecord.AnimalId;

                newRecordId = Convert.ToInt32(cmdAddMedicalRecord.ExecuteScalar());

                trans.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    //roll back changes
                    trans.Rollback();

                }
                catch (Exception ex2)
                {
                    throw ex2;
                }
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return newRecordId;
        }

        /// <summary>
        /// Ethan Kline
        /// Created: 2023/03/1
        /// 
        /// Selects all medical records for a specific animal
        /// </summary>
        ///
        /// <param name="animalId">animal's animalId number</param>
        /// <exception cref="Exception">Select Fails</exception>
        /// <returns>All medicalrecord rows where animalId equals the param</returns>
        public List<MedicalRecordVM> SelectMedicalRecordByAnimal(int animalId)
        {
            List<MedicalRecordVM> medicalRecords = new List<MedicalRecordVM>();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_medical_notes";

            // command 
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AnimalId", SqlDbType.Int);
            cmd.Parameters["@AnimalId"].Value = animalId;

            // try-catch-finally
            try
            {
                // open a connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        var medicalRecord = new MedicalRecordVM();

                        medicalRecord.MedicalRecordId = reader.GetInt32(0);
                        medicalRecord.AnimalId = reader.GetInt32(1);
                        medicalRecord.Date = reader.GetDateTime(2);//.ToShortDateString();
                        medicalRecord.MedicalNotes = reader.GetString(3);
                        medicalRecord.IsProcedure = reader.GetBoolean(4);
                        medicalRecord.IsTest = reader.GetBoolean(5);
                        medicalRecord.IsVaccination = reader.GetBoolean(6);
                        medicalRecord.IsPrescription = reader.GetBoolean(7);
                        medicalRecord.Images = reader.GetBoolean(8);
                        medicalRecord.QuarantineStatus = reader.GetBoolean(9);
                        medicalRecord.Diagnosis = reader.GetString(10);
                        medicalRecords.Add(medicalRecord);
                    }
                }
            }
            catch (Exception)
            {
                throw ;
            }
            finally
            {
                conn.Close();
            }
            return medicalRecords;
        }

        /// <summary>
        /// Ethan Kline
        /// Created: 2023/03/1
        /// 
        /// updates a medicalrecord by medicalrecordid
        /// </summary>
        ///
        /// <remarks>
        /// Updater Medical record
        /// </remarks>
        /// <param name="oldmedicalRecord">medical record to be replaced</param>
        /// <param name="medicalRecord">medical record to be updated to</param> 
        /// <exception cref="Exception">Update Fails</exception>
        /// <returns>Rows affected</returns>	
        public int UpdateMedicalRecord(MedicalRecord oldmedicalRecord, MedicalRecord medicalRecord)
        {
            int rows = 0;
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_edit_medical_notes_by_medical_record_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@AnimalId", medicalRecord.AnimalId);
            cmd.Parameters.AddWithValue("@MedicalRecords", medicalRecord.MedicalNotes);
            cmd.Parameters.AddWithValue("@IsProcedure", medicalRecord.IsProcedure);
            cmd.Parameters.AddWithValue("@Test", medicalRecord.IsTest);
            cmd.Parameters.AddWithValue("@Vaccination", medicalRecord.IsVaccination);
            cmd.Parameters.AddWithValue("@Prescription", medicalRecord.IsPrescription);
            cmd.Parameters.AddWithValue("@Images", medicalRecord.Images);
            cmd.Parameters.AddWithValue("@QuarantineStatus", medicalRecord.QuarantineStatus);
            cmd.Parameters.AddWithValue("@Diagnosis", medicalRecord.Diagnosis);

            cmd.Parameters.AddWithValue("@oldMedicalRecords", oldmedicalRecord.MedicalNotes);
            cmd.Parameters.AddWithValue("@oldIsProcedure", oldmedicalRecord.IsProcedure);
            cmd.Parameters.AddWithValue("@oldTest", oldmedicalRecord.IsTest);
            cmd.Parameters.AddWithValue("@oldVaccination", oldmedicalRecord.IsVaccination);
            cmd.Parameters.AddWithValue("@oldPrescription", oldmedicalRecord.IsPrescription);
            cmd.Parameters.AddWithValue("@oldImages", oldmedicalRecord.Images);
            cmd.Parameters.AddWithValue("@oldQuarantineStatus", oldmedicalRecord.QuarantineStatus);
            cmd.Parameters.AddWithValue("@oldDiagnosis", oldmedicalRecord.Diagnosis);
            cmd.Parameters.AddWithValue("@MedicalRecordId", oldmedicalRecord.MedicalRecordId);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rows;
        }
        public int AddMedicalNotes(MedicalRecord medicalRecord)
        {
            int rows = 0;
            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_add_medical_notes";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AnimalId",medicalRecord.AnimalId);
            cmd.Parameters.AddWithValue("@MedicalNotes",medicalRecord.MedicalNotes);
            cmd.Parameters.AddWithValue("@Diagnosis",medicalRecord.Diagnosis);

            try
            {
                conn.Open();

                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        public int InsertTestMedicalRecordByAnimalId(int animalId, string medicalNotes, bool test, string diagnosis)
        {
            int medicalRecordId = 0;

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_insert_test_medical_record_by_animal_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            /*
                @MedicalRecordID        int output,
                @AnimalID               int,
                @MedicalNotes           nvarchar(250),
                @Test                   bit,
                @Diagnosis              nvarchar(250)
            */
            // set parameters
            cmd.Parameters.AddWithValue("@AnimalID", animalId);
            cmd.Parameters.AddWithValue("@MedicalNotes", medicalNotes);
            cmd.Parameters.AddWithValue("@Test", test);
            cmd.Parameters.AddWithValue("@Diagnosis", diagnosis);
            try
            {
                // open connection
                conn.Open();

                // execute
                medicalRecordId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // close connection
                conn.Close();
            }

            return medicalRecordId;
        }

        public int UpdateQuarantineStatusByMedicalRecordId(int medicalRecordId, bool quarantineStatus, bool oldQuarantineStatus)
        {
            int rows = 0;

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_update_quarantine_status_by_medical_record_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            /*
                 @MedicalRecordID		int,
	             @QuarantineStatus		bit,
	             @OldQuarantineStatus	bit
            */
            // set parameters
            cmd.Parameters.AddWithValue("@MedicalRecordID", medicalRecordId);
            cmd.Parameters.AddWithValue("@QuarantineStatus", quarantineStatus);
            cmd.Parameters.AddWithValue("@OldQuarantineStatus", oldQuarantineStatus);
            try
            {
                // open connection
                conn.Open();

                // execute
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // close connection
                conn.Close();
            }
            return rows;
        }

        public List<MedicalRecordVM> SelectAllMedicalRecordsByAnimald(int animalId)
        {
            List<MedicalRecordVM> medicalRecords = new List<MedicalRecordVM>();
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_medical_records_by_animal_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AnimalId", SqlDbType.Int);
            cmd.Parameters["@AnimalId"].Value = animalId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var medicalRecord = new MedicalRecordVM();
                        medicalRecord.MedicalRecordId = reader.GetInt32(0);
                        medicalRecord.Date = reader.GetDateTime(1);
                        medicalRecord.MedicalNotes = reader.GetString(2);
                        medicalRecord.IsProcedure = reader.GetBoolean(3);
                        medicalRecord.IsTest = reader.GetBoolean(4);
                        medicalRecord.IsVaccination = reader.GetBoolean(5);
                        medicalRecord.IsPrescription = reader.GetBoolean(6);
                        medicalRecord.Images = reader.GetBoolean(7);
                        medicalRecord.QuarantineStatus = reader.GetBoolean(8);
                        medicalRecord.Diagnosis = reader.GetString(9);
                        medicalRecords.Add(medicalRecord);
                    }
                }
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }
            return medicalRecords;
        }
    }
}
