using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class ZipcodeAccessor : IZipcodeAccessor
    {
        public Zipcode SelectCityStateLatLongByZipcode(string zipcode)
        {
            //throw new NotImplementedException();

            Zipcode zipcodeObj = new Zipcode();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_city_state_lat_long_by_zipcode";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Zipcode", SqlDbType.Char).Value = zipcode;

            try
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //TODO: complete reading data in to object
                            zipcodeObj.ZipcodeId = reader.GetString(0);
                            zipcodeObj.City = reader.GetString(1);
                            zipcodeObj.State = reader.GetString(2);
                            zipcodeObj.Latitude = reader.GetDecimal(3);
                            zipcodeObj.Longitude = reader.GetDecimal(4);
                        }
                    }
                    else
                    {
                        throw new ApplicationException("Zipcode not found.");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return zipcodeObj;
        }
    }
}
