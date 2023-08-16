using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayer
{
    public class AnimalUpdatesAccessor : IAnimalUpdatesAccessor
    {
        public int InsertAnimalUpdatesByAnimalId(int animalId, string animalRecordNotes)
        {
            int rowAffected = 0;

            var conn = new DBConnection().GetConnection();

            var cmdText = "sp_insert_animalupdates_by_animalid";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AnimalId", animalId);
            cmd.Parameters.AddWithValue("@AnimalRecordNotes", animalRecordNotes);

            try
            {
                conn.Open();
                rowAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rowAffected;
        }

        public List<AnimalUpdates> SelectAllAnimalUpdateByAnimalId(int animalId)
        {
            List<AnimalUpdates> animalUpdates = new List<AnimalUpdates>();

            var conn = new DBConnection().GetConnection();

            var cmdText = "sp_select_all_animal_updates_by_animalid";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AnimalId", animalId);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        animalUpdates.Add(new AnimalUpdates
                        {
                            AnimalId = animalId,
                            AnimalRecordNotes = reader.GetString(0),
                            AnimalRecordId = reader.GetInt32(1),
                            AnimalRecordDate = reader.GetDateTime(2)
                        });
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

            return animalUpdates;
        }

        public string SelectAnimalUpdatesByAnimal(int animalId)
        {
            string result = "";

            var conn = new DBConnection().GetConnection();

            var cmdText = "sp_select_animalupdates_by_animalid";

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
                    if (reader.IsDBNull(0))
                    {
                        result = "";
                    }
                    else
                    {
                        result = reader.GetString(0);
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

            return result;
        }
    }
}
