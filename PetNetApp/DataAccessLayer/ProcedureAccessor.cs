/// <summary>
/// Andrew Cromwell
/// Created: 2023/02/08
/// 
/// Class that retrieves Procedure records from the data base.
/// </summary>
/// 
///< remarks >
/// Andrew Cromwell
/// Updated: 2023/02/16
/// Added UpdateProcedureByMedicalRecordIdAndProcedureId and SelectProceduresByAnimalId
/// </remarks>

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
    public class ProcedureAccessor : IProcedureAccessor
    {

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/08
        /// 
        /// Inserts a new procedure record into the database
        /// </summary>
        /// <param name="procedure">the procedure to be inserted</param>
        /// <exception cref="SQLException">Insert Fails</exception>
        /// <returns>Rows affected</returns>
        public int InsetProcedureByMedicalRecordId(Procedure procedure, int medicalRecordId)
        {
            int rows = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_insert_procedure_by_medical_record_id";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MedicalRecordId", medicalRecordId);
            cmd.Parameters.AddWithValue("@UserId", procedure.UserId);
            cmd.Parameters.AddWithValue("@ProcedureName", procedure.ProcedureName);
            cmd.Parameters.AddWithValue("@MedicationsAdministered", procedure.MedicationsAdministered);
            cmd.Parameters.AddWithValue("@ProcedureNotes", procedure.ProcedureNotes);
            cmd.Parameters.AddWithValue("@ProcedureDate", procedure.ProcedureDate.Date);
            cmd.Parameters.AddWithValue("@ProcedureTime", procedure.ProcedureDate.TimeOfDay);


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

        public ProcedureVM SelectProcedureByMedicalRecordId(int medicalRecordId)
        {
            ProcedureVM procedure = null;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_procedure_by_medical_record_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MedicalRecordId", SqlDbType.Int).Value = medicalRecordId;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        procedure = new ProcedureVM()
                        {
                            ProcedureId = reader.GetInt32(0),
                            MedicalRecordId = reader.GetInt32(1),
                            UserId = reader.GetInt32(2),
                            ProcedureName = reader.GetString(3),
                            MedicationsAdministered = reader.IsDBNull(4) ? null : reader.GetString(4),
                            ProcedureNotes = reader.IsDBNull(5) ? null : reader.GetString(5),
                            ProcedureDate = reader.GetDateTime(6)
                        };
                    }
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
            return procedure;
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/16
        /// </summary>
        /// <param name="animalId">animalId to find the procedures associated with</param>
        /// <exception cref="SQLException">Select Fails</exception>
        /// <returns>procedureVMs that have a medicalRecordId that is associated with the animalId</returns>
        public List<ProcedureVM> SelectProceduresByAnimalId(int animalId)
        {
            List<ProcedureVM> procedures = new List<ProcedureVM>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_procedures_by_animal_id";

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
                        ProcedureVM procedure = new ProcedureVM();
                        procedure.ProcedureId = reader.GetInt32(0);
                        procedure.MedicalRecordId = reader.GetInt32(1);
                        procedure.UserId = reader.GetInt32(2);
                        procedure.SurgeonGivenName = reader.GetString(3);
                        procedure.SurgeonFamilyName = reader.GetString(4);
                        procedure.ProcedureName = reader.GetString(5);
                        if (reader.IsDBNull(6)) 
                        {
                            procedure.MedicationsAdministered = null;
                        }
                        else
                        {
                            procedure.MedicationsAdministered = reader.GetString(6);
                        }
                        if (reader.IsDBNull(7))
                        {
                            procedure.ProcedureNotes = null;
                        }
                        else
                        {
                            procedure.ProcedureNotes = reader.GetString(7);
                        }
                        DateTime procedureDate = reader.GetDateTime(8);
                        TimeSpan procedureTime = reader.GetTimeSpan(9);
                        procedure.ProcedureDate = new DateTime(procedureDate.Year, procedureDate.Month, procedureDate.Day,
                            procedureTime.Hours, procedureTime.Minutes, procedureTime.Seconds);
                        procedures.Add(procedure);
                    }
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

            return procedures;
        }

        /// <summary>
        /// Andrew Cromwell
        /// Created: 2023/02/16
        /// </summary>
        /// <param name="procedure">procedure that will overwrite an existing procedure</param>
        /// <param name="oldProcedure">procedure that will be overwriten</param>
        /// <param name="medicalRecordId">id of the medical record that the procedure belongs to</param>
        /// <exception cref="SQLException">Update Fails</exception>
        /// <returns>rows efected</returns>
        public int UpdateProcedureByProcedureId(Procedure procedure, Procedure oldProcedure)
        {
            int rows = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_update_procedure_by_procedure_id";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProcedureId", procedure.ProcedureId);
            cmd.Parameters.AddWithValue("@OldUserID", oldProcedure.UserId);
            cmd.Parameters.AddWithValue("@ProcedureName", procedure.ProcedureName);
            cmd.Parameters.AddWithValue("@OldProcedureName", oldProcedure.ProcedureName);
            cmd.Parameters.AddWithValue("@MedicationsAdministered", procedure.MedicationsAdministered);
            cmd.Parameters.AddWithValue("@OldMedicationsAdministered", oldProcedure.MedicationsAdministered);
            cmd.Parameters.AddWithValue("@ProcedureNotes", procedure.ProcedureNotes);
            cmd.Parameters.AddWithValue("@OldProcedureNotes", oldProcedure.ProcedureNotes);
            cmd.Parameters.AddWithValue("@ProcedureDate", procedure.ProcedureDate.Date);
            cmd.Parameters.AddWithValue("@OldProcedureDate", oldProcedure.ProcedureDate.Date);
            cmd.Parameters.AddWithValue("@ProcedureTime", procedure.ProcedureDate.TimeOfDay);
            cmd.Parameters.AddWithValue("@OldProcedureTime", oldProcedure.ProcedureDate.TimeOfDay);

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
