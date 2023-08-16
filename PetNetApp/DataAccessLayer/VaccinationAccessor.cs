/// <summary>
/// Zaid Rachman
/// Created: 2023/02/09
/// 
/// Data access layer for Vaccination
/// Methods:
/// InsertVaccination
/// SelectVaccinationByAnimalId
/// UpdateVaccination
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
using DataAccessLayerInterfaces;
using DataObjects;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class VaccinationAccessor : IVaccinationAccessor
    {
        public int InsertVaccination(Vaccination vaccination, int animalId)
        {
            int ID;
            int rows;
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
                

                cmdAddMedicalRecord.Parameters["@AnimalId"].Value = animalId;

                ID = Convert.ToInt32(cmdAddMedicalRecord.ExecuteScalar());

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
            SqlCommand cmdAddVaccination = new SqlCommand(cmdTextAddVaccination, conn);
            cmdAddVaccination.CommandType = CommandType.StoredProcedure;
            cmdAddVaccination.Parameters.Add("@MedicalRecordId", SqlDbType.Int);
            cmdAddVaccination.Parameters.Add("@UsersId", SqlDbType.Int);
            cmdAddVaccination.Parameters.Add("@VaccineName", SqlDbType.NVarChar, 50);
            cmdAddVaccination.Parameters.Add("@VaccineAdminsterDate", SqlDbType.DateTime);
            //Values
            cmdAddVaccination.Parameters["@MedicalRecordId"].Value = ID; //set to the ID value of the previously created medical record.
            cmdAddVaccination.Parameters["@UsersId"].Value = vaccination.UserId;
            cmdAddVaccination.Parameters["@VaccineName"].Value = vaccination.VaccineName;
            cmdAddVaccination.Parameters["@VaccineAdminsterDate"].Value = vaccination.VaccineAdminsterDate;
            try
            {
                conn.Open();
                rows = cmdAddVaccination.ExecuteNonQuery();

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

        public VaccinationVM SelectVaccinationByMedicalRecordId(int medicalRecordId)
        {
            VaccinationVM vaccination = null;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_vaccination_by_medical_record_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MedicalRecordId", SqlDbType.Int).Value = medicalRecordId;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    vaccination = new VaccinationVM()
                    {
                        VaccineId = reader.GetInt32(0),
                        MedicalRecordId = reader.GetInt32(1),
                        UserId = reader.GetInt32(2),
                        VaccineName = reader.GetString(3),
                        VaccineAdminsterDate = reader.GetDateTime(4)
                    };
                }
                reader.Close();
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }
            return vaccination;
        }

        public List<Vaccination> SelectVaccinationsByAnimalId(int animalId)
        {
            List<Vaccination> vaccinations = new List<Vaccination>();
            //connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            //command text
            var cmdText = "sp_select_vaccinations_by_animal_id";

            //command
            var cmd = new SqlCommand(cmdText, conn);

            //command type
            cmd.CommandType = CommandType.StoredProcedure;
            //parameter
            cmd.Parameters.Add("@AnimalId", SqlDbType.Int);

            //Value
            cmd.Parameters["@AnimalId"].Value = animalId;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var vaccine = new Vaccination();
                        vaccine.VaccineId = reader.GetInt32(0);
                        vaccine.MedicalRecordId = reader.GetInt32(1);
                        vaccine.UserId = reader.GetInt32(2);
                        vaccine.VaccineName = reader.GetString(3);
                        vaccine.VaccineAdminsterDate = reader.GetDateTime(4);

                        vaccinations.Add(vaccine);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw new ArgumentException("Could not receive animal's vaccine information", ex);
            }
            finally
            {
                conn.Close();
            }
            return vaccinations;
        }

        public int UpdateVaccination(Vaccination oldVaccination, Vaccination newVaccination)
        {
            int rows; //rows returned
            //connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            //command text
            var cmdText = "sp_update_vaccination";
            //command
            var cmd = new SqlCommand(cmdText, conn);
            //Command Type
            cmd.CommandType = CommandType.StoredProcedure;
            //parameter

            cmd.Parameters.AddWithValue("@VaccineId", oldVaccination.VaccineId);
            cmd.Parameters.AddWithValue("@MedicalRecordId", oldVaccination.MedicalRecordId);
            cmd.Parameters.AddWithValue("@OldUsersId", oldVaccination.UserId);
            cmd.Parameters.AddWithValue("@OldVaccineName", oldVaccination.VaccineName);
            cmd.Parameters.AddWithValue("@OldVaccineAdminsterDate", oldVaccination.VaccineAdminsterDate);

            cmd.Parameters.AddWithValue("@UsersId", newVaccination.UserId);
            cmd.Parameters.AddWithValue("@VaccineName", newVaccination.VaccineName);
            cmd.Parameters.AddWithValue("@VaccineAdminsterDate", newVaccination.VaccineAdminsterDate);

           

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
    }
}
