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
    public class TestAccessor : ITestAccessor
    {
        public TestVM SelectTestByMedicalRecordId(int medicalRecordId)
        {
            TestVM test = null;
            var conn = new DBConnection().GetConnection();
            SqlCommand cmd = new SqlCommand("sp_select_test_by_medical_record_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MedicalRecordId", SqlDbType.Int).Value = medicalRecordId;

            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        test = new TestVM()
                        {
                            TestId = reader.GetInt32(0),
                            MedicalRecordId = reader.GetInt32(1),
                            UserId = reader.GetInt32(2),
                            TestName = reader.GetString(3),
                            TestAcceptableRange = reader.GetString(4),
                            TestResult = reader.GetString(5),
                            TestNotes = reader.GetString(6),
                            TestDate = reader.GetDateTime(7)
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
            return test;
        }

        public List<Test> SelectTestsByAnimalId(int animalId)
        {
            List<Test> tests = new List<Test>();

            var conn = new DBConnection().GetConnection();
            SqlCommand cmd = new SqlCommand("sp_select_tests_by_animal_id", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AnimalID", SqlDbType.Int).Value = animalId;

            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tests.Add(new Test()
                            {
                                TestId = reader.GetInt32(0),
                                MedicalRecordId = reader.GetInt32(1),
                                UserId = reader.GetInt32(2),
                                TestName = reader.GetString(3),
                                TestAcceptableRange = reader.GetString(4),
                                TestResult = reader.GetString(5),
                                TestNotes = reader.GetString(6),
                                TestDate = reader.GetDateTime(7)
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

            return tests;
        }

        public int InsertTestByMedicalRecordId(Test test, int medicalRecordId)
        {
            int rows = 0;

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_test_by_medical_record_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            /*
                 @MedicalRecordID	
                 @UserID				
                 @TestName			
                 @TestAcceptableRange
                 @TestResult         
                 @TestNotes			
                 @TestDate			
            */
            // set parameters
            cmd.Parameters.AddWithValue("@MedicalRecordID", medicalRecordId);
            cmd.Parameters.AddWithValue("@UserID", test.UserId);
            cmd.Parameters.AddWithValue("@TestName", test.TestName);
            cmd.Parameters.AddWithValue("@TestAcceptableRange", test.TestAcceptableRange);
            cmd.Parameters.AddWithValue("@TestResult", test.TestResult);
            cmd.Parameters.AddWithValue("@TestNotes", test.TestNotes);
            cmd.Parameters.AddWithValue("@TestDate", test.TestDate);
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
    }
}
