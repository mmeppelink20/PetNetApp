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
    public class KennelAccessor : IKennelAccessor
    {
        public int InsertKennel(Kennel kennel)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_kennel";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ShelterId", SqlDbType.Int);
            cmd.Parameters.Add("@KennelName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@AnimalTypeId", SqlDbType.NVarChar, 50);
            cmd.Parameters["@ShelterId"].Value = kennel.ShelterId;
            cmd.Parameters["@KennelName"].Value = kennel.KennelName;
            cmd.Parameters["@AnimalTypeId"].Value = kennel.AnimalTypeId;

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

        public List<string> SelectAnimalTypes()
        {
            List<string> animalTypes = new List<string>();
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_animal_types";
            var cmd = new SqlCommand(cmdText, conn);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string animalType = null;

                        animalType = reader.GetString(0);

                        animalTypes.Add(animalType);
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
            return animalTypes;
        }

        public List<KennelVM> SelectKennels(int ShelterId)
        {
            List<KennelVM> kennelVMs = new List<KennelVM>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_kennels";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ShelterId", ShelterId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        KennelVM kennelVM = new KennelVM();
                        AnimalVM animal = new AnimalVM();
                        //List<Images> animalImages = new List<Images>();
                        //Images image = new Images();
                        //animal.AnimalImages = animalImages;

                        kennelVM.KennelId = reader.GetInt32(0);
                        kennelVM.ShelterId = reader.GetInt32(1);
                        kennelVM.KennelName = reader.GetString(2);
                        kennelVM.AnimalTypeId = reader.GetString(3);
                        kennelVM.KennelActive = reader.GetBoolean(4);
                        
                        if(reader.IsDBNull(8))
                        {
                            animal = null;
                        } else
                        {
                            animal.AnimalName = reader.GetString(5);
                            animal.BroughtIn = reader.GetDateTime(6);
                            animal.Notes = reader.GetString(7);
                            animal.AnimalId = reader.GetInt32(8);
                        }
                        
                        //if(reader.IsDBNull(9))
                        //{
                        //    image = null;
                        //} else
                        //{
                        //    image.ImageId = reader.GetString(9);
                        //    image.ImageFileName = reader.GetString(10);
                        //    animal.AnimalImages.Add(image);
                        //}

                        kennelVM.Animal = animal;
                        kennelVMs.Add(kennelVM);
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
            return kennelVMs;
        }

        public Kennel SelectKennelIdByAnimalId(int AnimalId)
        {
            Kennel _kennel = new Kennel();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_kennelId_by_animal_Id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AnimalId", SqlDbType.Int);
            cmd.Parameters["@AnimalId"].Value = AnimalId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //[KennelId]
                        _kennel.KennelId = reader.GetInt32(0);
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

            return _kennel;
        }

        
        public int InsertAnimalIntoKennelByAnimalId(int KennelId, int AnimalId)
        {
            int result = 0;
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_animal_into_kennel_by_animalId_and_kennelId";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AnimalId", SqlDbType.Int);
            cmd.Parameters.Add("@KennelId", SqlDbType.Int);
            cmd.Parameters["@AnimalId"].Value = AnimalId;
            cmd.Parameters["@KennelId"].Value = KennelId;

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

        public List<Animal> SelectAllAnimalsForKennel(int ShelterId)
        {
            List<Animal> _animalList = new List<Animal>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_all_animals_without_kennel";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ShelterId", ShelterId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var _animal = new Animal();
                        _animal.AnimalId = reader.GetInt32(0);
                        _animal.AnimalName = reader.GetString(1);
                        _animal.AnimalTypeId = reader.GetString(2);
                        _animal.AnimalBreedId = reader.GetString(3);
                        _animal.MicrochipNumber = reader.IsDBNull(4) ? null : reader.GetString(4);
                        _animal.AnimalShelterId = reader.GetInt32(5);
                        _animalList.Add(_animal);
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

            return _animalList;
        }

        public int UpdateKennelStatusByKennelId(int KennelId)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_update_kennel_status_by_kennelid";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@KennelId", SqlDbType.Int);
            cmd.Parameters["@KennelId"].Value = KennelId;

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

        public int DeleteAnimalKennelingByKennelId(int KennelId)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_delete_animal_kenneling_by_kennelid";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@KennelId", SqlDbType.Int);
            cmd.Parameters["@KennelId"].Value = KennelId;

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

        // Created By: Asa Armstrong
        public int DeleteAnimalKennelingByKennelIdAndAnimalId(int kennelId, int animalId)
        {
            int rowsAffected = 0;

            var conn = new DBConnection().GetConnection();
            var cmdText = "sp_remove_animal_from_animalkenneling_by_kennelId_and_animalId";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@KennelId", kennelId);
            cmd.Parameters.AddWithValue("@AnimalId", animalId);

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

            return rowsAffected;
        }

        public List<Kennel> SelectAllEmptyKennels(int shelterId)
        {
            List<Kennel> _kennelList = new List<Kennel>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_all_empty_kennels";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ShelterId", shelterId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var kennel = new Kennel();
                        kennel.KennelId = reader.GetInt32(0);
                        kennel.KennelName = reader.GetString(1);
                        kennel.AnimalTypeId = reader.GetString(2);
                        _kennelList.Add(kennel);
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

            return _kennelList;
        }

        public Images SelectImageByAnimalId(int animalId)
        {
            Images image = new Images();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_image_by_animalid";
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
                        image.ImageId = reader.GetString(0);
                        image.ImageFileName = reader.GetString(1);
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
            return image;
        }
    }
}
