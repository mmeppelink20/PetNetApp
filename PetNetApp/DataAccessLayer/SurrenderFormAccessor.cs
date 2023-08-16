/// <summary>
/// Alexis Oetken
/// Created: 2023/04/20
/// </summary>
///
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/23
/// 
/// Final QA
/// </remarks>
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

    // Code by Alex Oetken

    public class SurrenderFormAccessor : ISurrenderFormAccessor
    {
        public int InsertSurrenderForm(string AnimalType, string ReasonForSurrender, bool SpayOrNeuterStatus, string ContactPhone, string ContactEmail)
        {
            int rows = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_new_surrender_form";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AnimalType", AnimalType);
            cmd.Parameters.AddWithValue("@ReasonForSurrender", ReasonForSurrender);
            cmd.Parameters.AddWithValue("@SpayOrNeuterStatus", SpayOrNeuterStatus);
            cmd.Parameters.AddWithValue("@ContactPhone", ContactPhone);
            cmd.Parameters.AddWithValue("@ContactEmail", ContactEmail);

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

        public int RemoveSurrenderForm(int SurrenderFormID)
        {
            int rows = 0;

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_delete_surrender_form";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SurrenderFormID", SurrenderFormID);

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

        public List<SurrenderForm> RetrieveAllSurrenderForms()
        {
            List<SurrenderForm> surrenderFormsList = new List<SurrenderForm>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_all_surrender_forms";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader(); 
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        var surrenderForm = new SurrenderForm(); 

                        try
                        {
                            
                            surrenderForm.SurrenderFormID = reader.GetInt32(0);
                            surrenderForm.AnimalType = reader.GetString(1);
                            surrenderForm.ReasonForSurrender = reader.GetString(2);
                            surrenderForm.SpayOrNeuterStatus = reader.GetBoolean(3);
                            surrenderForm.ContactPhone = reader.GetString(4);
                            surrenderForm.ContactEmail = reader.GetString(5);

                            surrenderFormsList.Add(surrenderForm);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        
                    }
                }
                reader.Close(); 

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Could not receive Surrender Records", ex);
            }
            finally
            {
                conn.Close(); 
            }
            return surrenderFormsList;

        }
    }
}