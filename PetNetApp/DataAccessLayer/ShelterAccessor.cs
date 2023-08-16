/// <summary>
/// Brian Collum
/// Created: 2023/02/23
/// ShelterAccessor governs access to the shelter entries in the database
/// </summary>
///
/// <remarks>
/// Brian Collum
/// Updated: 2023/03/30
/// 
/// Further cleanup from the AddressTwo => Address2 refactor
/// 
/// Brian Collum
/// Updated: 2023/04/27
/// 
/// Added a Trim() method to  data access methods that retrieve shelter zipcodes from the database.
/// Specifically RetrieveShelterList() and SelectShelterVMByShelterID(int shelterID)
/// This should prevent the display of trailing whitespace in shelter Zip Code fields.
/// 
/// Brian Collum
/// Updated: 2023/04/28
/// Fixed UpdateZipCodeByShelterID
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayerInterfaces;
using DataObjects;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    
    public class ShelterAccessor : IShelterAccessor
    {
        public List<Shelter> RetrieveShelterList()
        {
            // This list populates the Shelter UI
            List<Shelter> shelterList = new List<Shelter>();

            // ADO.NET needs a connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Text
            var cmdText = "sp_select_shelter_all";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // No Parameters
            try
            {
                // Open connection
                conn.Open();
                // Read in data
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var shelter = new ShelterVM();
                        try
                        {
                            shelter.ShelterId = Convert.ToInt32(reader["ShelterId"]);
                            shelter.ShelterName = Convert.ToString(reader["ShelterName"]);
                            shelter.Address = Convert.ToString(reader["Address"]);
                            shelter.Address2 = Convert.ToString(reader["AddressTwo"]);
                            shelter.ZipCode = Convert.ToString(reader["Zipcode"]).Trim();
                            shelter.Phone = Convert.ToString(reader["Phone"]);
                            shelter.Email = Convert.ToString(reader["Email"]);
                            shelter.AreasOfNeed = Convert.ToString(reader["Areasofneed"]);
                            shelter.ShelterActive = Convert.ToBoolean(reader["ShelterActive"]);
                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }
                        // Verify that required fields are populated
                        if (shelter.ShelterId != 0 && shelter.ShelterName != null && shelter.Address != null && shelter.ZipCode != null)
                        {
                            shelterList.Add(shelter);
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
            // Verify that shelter list built successfully
            if (shelterList.Count > 0)
            {
                return shelterList;
            }
            else
            {
                // Return a dummy shelter to indicate failure
                shelterList.Add(new ShelterVM { ShelterId = 0, ShelterName = "Failed to retrieve Shelter List", Address = "Failed to retrieve Shelter List", ZipCode = "Failed to retrieve Shelter List", ShelterActive = false });
                return shelterList;
            }
        }
        public bool InsertShelter(string shelterName, string address, string Address2, string zipCode, string phone, string email, string areasOfNeed, bool shelterActive)
        {
            // Initialize result to failure
            bool result = false;
            int rowsAffected = 0;
            // ADO.NET needs a connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Text
            var cmdText = "sp_add_shelter";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("ShelterName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("Address", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("AddressTwo", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("Zipcode", SqlDbType.Char, 9);
            cmd.Parameters.Add("Phone", SqlDbType.NVarChar, 13);
            cmd.Parameters.Add("Email", SqlDbType.NVarChar, 254);
            cmd.Parameters.Add("Areasofneed", SqlDbType.NVarChar);    // Max Value
            cmd.Parameters.Add("ShelterActive", SqlDbType.Bit);

            cmd.Parameters["ShelterName"].Value = shelterName;
            cmd.Parameters["Address"].Value = address;
            cmd.Parameters["AddressTwo"].Value = Address2;
            cmd.Parameters["Zipcode"].Value = zipCode;
            cmd.Parameters["Phone"].Value = phone;
            cmd.Parameters["Email"].Value = email;
            cmd.Parameters["Areasofneed"].Value = areasOfNeed;
            cmd.Parameters["ShelterActive"].Value = shelterActive;
            // Run the SP
            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            if (rowsAffected > 0)
            {
                result = true;
            }
            // Return confirmation of success
            return result;
        }
        // THIS WILL NEED MORE UPDATES AS ShelterVM objects are fleshed out.
        // SelectShelterVMByShelterID is for retrieving extra details on a selected shelter.
        public ShelterVM SelectShelterVMByShelterID(int shelterID)
        {
            // This list populates the Shelter UI
            ShelterVM shelter = new ShelterVM();
            bool hadException = false;

            // ADO.NET needs a connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Text
            var cmdText = "sp_select_shelter_by_id";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("ShelterId", SqlDbType.Int);
            cmd.Parameters["ShelterId"].Value = shelterID;
            try
            {
                // Open connection
                conn.Open();
                // Read in data
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            // Add more items as ShelterVM is fleshed out
                            shelter.ShelterId = Convert.ToInt32(reader["ShelterId"]);
                            shelter.ShelterName = Convert.ToString(reader["ShelterName"]);
                            shelter.Address = Convert.ToString(reader["Address"]);
                            shelter.Address2 = Convert.ToString(reader["AddressTwo"]);
                            shelter.ZipCode = Convert.ToString(reader["Zipcode"]).Trim();
                            shelter.Phone = Convert.ToString(reader["Phone"]);
                            shelter.Email = Convert.ToString(reader["Email"]);
                            shelter.AreasOfNeed = Convert.ToString(reader["Areasofneed"]);
                            shelter.ShelterActive = Convert.ToBoolean(reader["ShelterActive"]);
                        }
                        catch (Exception ex)
                        {
                            hadException = true;
                            throw ex;
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
            if (!hadException)
            {
                return shelter;
            }
            else
            {
                // Return a dummy shelter to indicate failure
                shelter.ShelterId = 0;
                shelter.ShelterName = "Failed to retrieve Shelter List";
                shelter.Address = "Failed to retrieve Shelter List";
                shelter.ZipCode = "Failed to retrieve Shelter List";
                shelter.ShelterActive = false;
                return shelter;
            }
        }

        public int UpdateActiveStatusByShelterID(int shelterID, bool oldActiveStatus, bool newActiveStatus)
        {
            // This should be 1 upon successful script execution
            int result = 0;
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Text
            var cmdText = "sp_edit_shelter_active_by_shelter_id";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("ShelterId", SqlDbType.Int);
            cmd.Parameters.Add("oldShelterActive", SqlDbType.Bit);
            cmd.Parameters.Add("newShelterActive", SqlDbType.Bit);
            cmd.Parameters["ShelterId"].Value = shelterID;
            cmd.Parameters["oldShelterActive"].Value = oldActiveStatus;
            cmd.Parameters["newShelterActive"].Value = newActiveStatus;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        public int UpdateShelterNameByShelterID(int shelterID, string oldShelterName, string newShelterName)
        {
            // This should be 1 upon successful script execution
            int result = 0;
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Text
            var cmdText = "sp_edit_shelter_name_by_shelter_id";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("ShelterId", SqlDbType.Int);
            cmd.Parameters.Add("oldName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("newName", SqlDbType.NVarChar, 50);
            cmd.Parameters["ShelterId"].Value = shelterID;
            cmd.Parameters["oldName"].Value = oldShelterName;
            cmd.Parameters["newName"].Value = newShelterName;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        public int UpdateAddressByShelterID(int shelterID, string oldAddress, string newAddress)
        {
            // This should be 1 upon successful script execution
            int result = 0;
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Text
            var cmdText = "sp_edit_address_by_shelter_id";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("ShelterId", SqlDbType.Int);
            cmd.Parameters.Add("oldAddress", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("newAddress", SqlDbType.NVarChar, 50);
            cmd.Parameters["ShelterId"].Value = shelterID;
            cmd.Parameters["oldAddress"].Value = oldAddress;
            cmd.Parameters["newAddress"].Value = newAddress;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public int UpdateAddress2ByShelterID(int shelterID, string newAddress2)
        {
            // This should be 1 upon successful script execution
            int result = 0;
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Text
            var cmdText = "sp_edit_addresstwo_by_shelter_id";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("ShelterId", SqlDbType.Int);
            cmd.Parameters.Add("newAddressTwo", SqlDbType.NVarChar, 50);
            cmd.Parameters["ShelterId"].Value = shelterID;
            cmd.Parameters["newAddressTwo"].Value = newAddress2;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public int UpdateAreasOfNeedByShelterID(int shelterID, string newAreasOfNeed)
        {
            // This should be 1 upon successful script execution
            int result = 0;
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Text
            var cmdText = "sp_edit_areas_of_need_by_shelter_id";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("ShelterId", SqlDbType.Int);
            cmd.Parameters.Add("newAreasofneed", SqlDbType.NVarChar, 50);
            cmd.Parameters["ShelterId"].Value = shelterID;
            cmd.Parameters["newAreasofneed"].Value = newAreasOfNeed;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public int UpdateEmailByShelterID(int shelterID, string newEmail)
        {
            // This should be 1 upon successful script execution
            int result = 0;
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Text
            var cmdText = "sp_edit_email_by_shelter_id";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("ShelterId", SqlDbType.Int);
            cmd.Parameters.Add("newEmail", SqlDbType.NVarChar, 254);
            cmd.Parameters["ShelterId"].Value = shelterID;
            cmd.Parameters["newEmail"].Value = newEmail;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public int UpdatePhoneByShelterID(int shelterID, string newPhone)
        {
            // This should be 1 upon successful script execution
            int result = 0;
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Text
            var cmdText = "sp_edit_phone_by_shelter_id";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("ShelterId", SqlDbType.Int);
            cmd.Parameters.Add("newPhone", SqlDbType.NVarChar, 13);
            cmd.Parameters["ShelterId"].Value = shelterID;
            cmd.Parameters["newPhone"].Value = newPhone;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public int UpdateZipCodeByShelterID(int shelterID, string oldZipCode, string newZipcode)
        {
            // This should be 1 upon successful script execution
            int result = 0;
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Text
            var cmdText = "sp_edit_zipcode_by_shelter_id";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("ShelterId", SqlDbType.Int);
            cmd.Parameters.Add("oldZipcode", SqlDbType.Char, 9);
            cmd.Parameters.Add("newZipcode", SqlDbType.Char, 9);
            cmd.Parameters["ShelterId"].Value = shelterID;
            cmd.Parameters["oldZipcode"].Value = oldZipCode;
            cmd.Parameters["newZipcode"].Value = newZipcode;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        public int DeactivateShelterByShelterID(int shelterID)
        {
            // This should be 1 upon successful script execution
            int result = 0;
            // Connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            // Command Text
            var cmdText = "sp_deactivate_shelter_by_shelter_id";
            // Command
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // Parameters
            cmd.Parameters.Add("ShelterId", SqlDbType.Int);
            cmd.Parameters["ShelterId"].Value = shelterID;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public List<HoursOfOperation> SelectHoursOfOperationByShelterID(int shelterID)
        {
            var hoursOfOperation = new List<HoursOfOperation>();

            var conn = new DBConnection().GetConnection();

            var cmdtext = "sp_select_hours_of_operation_by_shelter_id";

            var cmd = new SqlCommand(cmdtext, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ShelterId", SqlDbType.Int);

            cmd.Parameters["@ShelterId"].Value = shelterID;



            try
            {

                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var hours = new HoursOfOperation();
                        hours.OpenHour = reader.GetTimeSpan(0);
                        hours.CloseHour = reader.GetTimeSpan(1);
                        hoursOfOperation.Add(hours);
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return hoursOfOperation;
        }

        public int UpdateHoursOfOperationByShelterID(int shelterID, int dayOfWeek, HoursOfOperation hours)
        {
            int result = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_update_hours_of_operation_by_shelter_id";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("ShelterId", SqlDbType.Int);
            cmd.Parameters.Add("OpenTime", SqlDbType.Time);
            cmd.Parameters.Add("CloseTime", SqlDbType.Time);
            cmd.Parameters.Add("DayOfWeek", SqlDbType.TinyInt);
            cmd.Parameters["ShelterId"].Value = shelterID;
            cmd.Parameters["OpenTime"].Value = hours.OpenHour;
            cmd.Parameters["CloseTime"].Value = hours.CloseHour;
            cmd.Parameters["DayOfWeek"].Value = dayOfWeek;
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
}
