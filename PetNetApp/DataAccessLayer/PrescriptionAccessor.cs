using DataAccessLayerInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    // Created by Tyler Hand
    // Created on 2023/03/25
    public class PrescriptionAccessor : IPrescriptionAccessor
    {
        
        
        public List<PrescriptionVM> SelectAllPrescriptions(int animalId)
        {
            List<PrescriptionVM> prescriptionVMs = new List<PrescriptionVM>();

            var conn = new DBConnection().GetConnection();
            SqlCommand cmd = new SqlCommand("sp_select_prescriptions_by_AnimalId", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AnimalId", SqlDbType.Int).Value = animalId;
            


            try
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            prescriptionVMs.Add(new PrescriptionVM()
                            {
                                MedicalRecordId = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                PrescriptionName = reader.GetString(2),
                                PrescriptionDosage = reader.GetString(3),
                                PrescriptionFrequency = reader.GetString(4),
                                PrescriptionNotes = reader.IsDBNull(5) ? "": reader.GetString(5),
                                PrescriptionDuration = reader.GetInt32(6)
  
                            }); 
                        }
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
            
            return prescriptionVMs;
        }




        public int InsetPrescriptionByMedicalRecordId(Prescription prescription, int medicalRecordId)
        {
            int rows = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_insert_prescription";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MedicalRecordId", medicalRecordId);
            cmd.Parameters.AddWithValue("@UsersId", prescription.UserId);
            cmd.Parameters.AddWithValue("@PrescriptionName", prescription.PrescriptionName);
            cmd.Parameters.AddWithValue("@PrescriptionDosage", prescription.PrescriptionDosage);
            cmd.Parameters.AddWithValue("@PrescriptionFrequency", prescription.PrescriptionFrequency);
            cmd.Parameters.AddWithValue("@PrescriptionDuration", prescription.PrescriptionDuration);
            cmd.Parameters.AddWithValue("@PrescriptionTypeId", prescription.PrescriptionTypeId);
            cmd.Parameters.AddWithValue("@DatePrescribed", prescription.DatePrescribed);
            cmd.Parameters.AddWithValue("@EndDate", prescription.EndDate);
            if (prescription.PrescriptionNotes == null ||  prescription.PrescriptionNotes == "")
            {
                cmd.Parameters.AddWithValue("@PrescriptionNotes", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@PrescriptionNotes", prescription.PrescriptionNotes);
            }
            


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

