using DataAccessLayerInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DataAccessLayer
{
    public class AnimalAccessor : IAnimalAccessor
    {
        public int InsertAnimal(AnimalVM animal)
        {
            int id = 0;

            DBConnection factory = new DBConnection();
            var conn = factory.GetConnection();
            var cmdText = "sp_insert_animal";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AnimalShelterId", animal.AnimalShelterId);
            cmd.Parameters.AddWithValue("@AnimalName", animal.AnimalName);
            cmd.Parameters.AddWithValue("@AnimalGender", animal.AnimalGender);
            cmd.Parameters.AddWithValue("@AnimalTypeId", animal.AnimalTypeId);
            cmd.Parameters.AddWithValue("@AnimalBreedId", animal.AnimalBreedId);
            cmd.Parameters.AddWithValue("@AnimalStatusId", animal.AnimalStatusId);
            cmd.Parameters.AddWithValue("@Aggressive", animal.Aggressive);
            cmd.Parameters.AddWithValue("@ChildFriendly", animal.ChildFriendly);
            cmd.Parameters.AddWithValue("@NeuterStatus", animal.NeuterStatus);

            if (animal.Personality == null)
            {
                cmd.Parameters.AddWithValue("@Personality", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Personality", animal.Personality);
            }

            if (animal.Description == null)
            {
                cmd.Parameters.AddWithValue("@Description", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Description", animal.Description);
            }

            if (animal.MicrochipNumber == null)
            {
                cmd.Parameters.AddWithValue("@MicrochipSerialNumber", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@MicrochipSerialNumber", animal.MicrochipNumber);
            }

            if (animal.AggressiveDescription == null)
            {
                cmd.Parameters.AddWithValue("@AggressiveDescription", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@AggressiveDescription", animal.AggressiveDescription);
            }

            if (animal.Notes == null)
            {
                cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Notes", animal.Notes);
            }

            try
            {
                conn.Open();
                id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return id;
        }

        /// <summary>
        /// Matthew Meppelink
        /// Created: 2023/02/16
        /// 
        /// Selects all animals with a like supplied string
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        /// <param name="animalName">the animal name to search for</param>
        /// <exception cref="Exception">Update Fails</exception>
        /// <returns>all animals with like name</returns>
        public List<Animal> SelectAllAnimals(string animalName)
        {
            List<Animal> animals = new List<Animal>();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_all_animals";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AnimalName", SqlDbType.NVarChar, 50);

            cmd.Parameters["@AnimalName"].Value = animalName;

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
                        var animal = new Animal();
                        animal.AnimalId = reader.GetInt32(0);
                        animal.AnimalName = reader.GetString(1);
                        animals.Add(animal);
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

            return animals;
        }

        public AnimalVM SelectAnimalByAnimalId(int animalId, int shelterId)
        {
            AnimalVM animal = new AnimalVM();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_animal_by_animalId";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@AnimalId", animalId);
            cmd.Parameters.AddWithValue("@AnimalShelterId", shelterId);

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
                        /*
                         *[AnimalId], [AnimalName], [AnimalGender], [AnimalTypeId], [AnimalBreedId], [KennelName], [Personality],
                         *[Description], [Animal].[AnimalStatusId], [AnimalStatus].[AnimalStatusDescription], [RecievedDate],
                         *[MicrochipSerialNumber], [Aggressive], [AggressiveDescription], [ChildFriendly], [NeuterStatus], [Notes], [ShelterId]
                        */

                        animal.AnimalId = reader.GetInt32(0);
                        animal.AnimalName = reader.GetString(1);
                        animal.AnimalGender = reader.GetString(2);
                        animal.AnimalTypeId = reader.GetString(3);
                        animal.AnimalBreedId = reader.GetString(4);
                        animal.KennelName = reader.IsDBNull(5) ? null : reader.GetString(5);
                        animal.Personality = reader.IsDBNull(6) ? null : reader.GetString(6);
                        animal.Description = reader.IsDBNull(7) ? null : reader.GetString(7);
                        animal.AnimalStatusId = reader.GetString(8);
                        animal.AnimalStatusDescription = reader.IsDBNull(9) ? null : reader.GetString(9);
                        animal.BroughtIn = reader.GetDateTime(10);
                        animal.MicrochipNumber = reader.IsDBNull(11) ? null : reader.GetString(11);
                        animal.Aggressive = reader.GetBoolean(12);
                        animal.AggressiveDescription = reader.IsDBNull(13) ? null : reader.GetString(13);
                        animal.ChildFriendly = reader.GetBoolean(14);
                        animal.NeuterStatus = reader.GetBoolean(15);
                        animal.Notes = reader.IsDBNull(16) ? null : reader.GetString(16);
                        animal.AnimalShelterId = reader.GetInt32(17);
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

            return animal;
        }
       
        public Dictionary<string, List<string>> SelectAllAnimalBreeds()
        {
            Dictionary<string, List<string>> breeds = new Dictionary<string, List<string>>();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_all_animal_breeds";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

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
                        string breed = reader.GetString(0);
                        string animalType = reader.GetString(1);
                        if (breeds.ContainsKey(animalType))
                        {
                            breeds[animalType].Add(breed);
                        }
                        else
                        {
                            breeds.Add(animalType, new List<string> { breed });
                        }
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
            return breeds;
        }

        public List<string> SelectAllAnimalGenders()
        {
            List<string> genders = new List<string>();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_all_animal_genders";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

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
                        string gender = "";
                        gender = reader.GetString(0);
                        genders.Add(gender);

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
            return genders;
        }
       
        public List<string> SelectAllAnimalStatuses()
        {
            List<string> statuses = new List<string>();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_all_animal_statuses";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

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
                        string status = "";
                        status = reader.GetString(0);
                        statuses.Add(status);

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
            return statuses;
        }
       
        public List<string> SelectAllAnimalTypes()
        {
            List<string> types = new List<string>();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_all_animal_types";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

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
                        string type = "";
                        type = reader.GetString(0);
                        types.Add(type);

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
            return types;
        }

        public int UpdateAnimal(AnimalVM oldAnimal, AnimalVM newAnimal)
        {
            int rows = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // cmdText
            var cmdText = "sp_update_animal";

            //command
            var cmd = new SqlCommand(cmdText, conn);

            // type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@AnimalId", oldAnimal.AnimalId);
            cmd.Parameters.AddWithValue("@AnimalShelterId", oldAnimal.AnimalShelterId);
            cmd.Parameters.AddWithValue("@OldAnimalName", oldAnimal.AnimalName);
            cmd.Parameters.AddWithValue("@OldAnimalGender", oldAnimal.AnimalGender);
            cmd.Parameters.AddWithValue("@OldAnimalTypeId", oldAnimal.AnimalTypeId);
            cmd.Parameters.AddWithValue("@OldAnimalBreedId", oldAnimal.AnimalBreedId);
            cmd.Parameters.AddWithValue("@OldAnimalStatusId", oldAnimal.AnimalStatusId);
            cmd.Parameters.AddWithValue("@OldReceivedDate", oldAnimal.BroughtIn); // "ReceivedDate" in the DB
            cmd.Parameters.AddWithValue("@OldAggressive", oldAnimal.Aggressive);
            cmd.Parameters.AddWithValue("@OldChildFriendly", oldAnimal.ChildFriendly);
            cmd.Parameters.AddWithValue("@OldNeuterStatus", oldAnimal.NeuterStatus);

            if (oldAnimal.Personality == null)
            {
                cmd.Parameters.AddWithValue("@OldPersonality", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OldPersonality", oldAnimal.Personality);
            }

            if (oldAnimal.Description == null)
            {
                cmd.Parameters.AddWithValue("@OldDescription", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OldDescription", oldAnimal.Description);
            }
            
            if (oldAnimal.MicrochipNumber == null)
            {
                cmd.Parameters.AddWithValue("@OldMicrochipSerialNumber", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OldMicrochipSerialNumber", oldAnimal.MicrochipNumber);
            }
            
            if (oldAnimal.AggressiveDescription == null)
            {
                cmd.Parameters.AddWithValue("@OldAggressiveDescription", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OldAggressiveDescription", oldAnimal.AggressiveDescription);
            }
           
            if (oldAnimal.Notes == null)
            {
                cmd.Parameters.AddWithValue("@OldNotes", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OldNotes", oldAnimal.Notes);
            }

            cmd.Parameters.AddWithValue("@NewAnimalName", newAnimal.AnimalName);
            cmd.Parameters.AddWithValue("@NewAnimalGender", newAnimal.AnimalGender);
            cmd.Parameters.AddWithValue("@NewAnimalTypeId", newAnimal.AnimalTypeId);
            cmd.Parameters.AddWithValue("@NewAnimalBreedId", newAnimal.AnimalBreedId);
            cmd.Parameters.AddWithValue("@NewPersonality", newAnimal.Personality);
            cmd.Parameters.AddWithValue("@NewDescription", newAnimal.Description);
            cmd.Parameters.AddWithValue("@NewAnimalStatusId", newAnimal.AnimalStatusId);
            cmd.Parameters.AddWithValue("@NewReceivedDate", newAnimal.BroughtIn); // "ReceivedDate" in the DB
            cmd.Parameters.AddWithValue("@NewMicrochipSerialNumber", newAnimal.MicrochipNumber);
            cmd.Parameters.AddWithValue("@NewAggressive", newAnimal.Aggressive);
            cmd.Parameters.AddWithValue("@NewAggressiveDescription", newAnimal.AggressiveDescription);
            cmd.Parameters.AddWithValue("@NewChildFriendly", newAnimal.ChildFriendly);
            cmd.Parameters.AddWithValue("@NewNeuterStatus", newAnimal.NeuterStatus);
            cmd.Parameters.AddWithValue("@NewNotes", newAnimal.Notes);

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

        public List<Animal> SelectAllAnimals(int shelterId)  // add shelterId
        {
            List<Animal> animals = new List<Animal>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_all_animals_by_shelter_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AnimalShelterId", shelterId);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var animal = new Animal();

                        animal.AnimalId = reader.GetInt32(0);
                        animal.AnimalName = reader.GetString(1);
                        animal.AnimalTypeId = reader.GetString(2);
                        animal.AnimalBreedId = reader.GetString(3);
                        animal.Personality = reader.IsDBNull(4) ? null : reader.GetString(4);
                        animal.Description = reader.IsDBNull(5) ? null : reader.GetString(5);
                        animal.AnimalStatusId = reader.GetString(6);
                        animal.BroughtIn = reader.GetDateTime(7);
                        animal.MicrochipNumber = reader.GetString(8);
                        animal.Aggressive = reader.GetBoolean(9);
                        animal.AggressiveDescription = reader.IsDBNull(10) ? null : reader.GetString(10);
                        animal.ChildFriendly = reader.GetBoolean(11);
                        animal.NeuterStatus = reader.GetBoolean(12);
                        animal.Notes = reader.IsDBNull(13) ? null : reader.GetString(13);
                        animal.AnimalShelterId = reader.GetInt32(14);
                        animals.Add(animal);
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

            return animals;
        }

        public List<Animal> SelectAllAnimalsNotInKennel()
        {
            throw new NotImplementedException();
        }

        public AnimalVM SelectAnimalMedicalProfileByAnimalId(int animalId)
        {
            AnimalVM animalVM = new AnimalVM();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_animal_record_by_animal_id";
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
                        //[AnimalId], [AnimalName], [AnimalGender], [AnimalTypeId], [AnimalBreedId], [Personality], [Description],
                        //[AnimalStatusId],[RecievedDate], [MicrochipSerialNumber], [Aggressive], [AggressiveDescription],
                        //[ChildFriendly], [NeuterStatus],[Notes]

                        animalVM.AnimalId = reader.GetInt32(0);
                        animalVM.AnimalName = reader.GetString(1);
                        animalVM.AnimalGender = reader.GetString(2);
                        animalVM.AnimalTypeId = reader.GetString(3);
                        animalVM.AnimalBreedId = reader.GetString(4);
                        animalVM.Personality = reader.IsDBNull(5) ? null : reader.GetString(5);
                        animalVM.Description = reader.IsDBNull(6) ? null : reader.GetString(6);
                        animalVM.AnimalStatusId = reader.GetString(7);
                        animalVM.BroughtIn = reader.GetDateTime(8);
                        animalVM.MicrochipNumber = reader.IsDBNull(9) ? null : reader.GetString(9);
                        animalVM.Aggressive = reader.GetBoolean(10);
                        animalVM.AggressiveDescription = reader.IsDBNull(11) ? null : reader.GetString(11);
                        animalVM.ChildFriendly = reader.GetBoolean(12);
                        animalVM.NeuterStatus = reader.GetBoolean(13);
                        animalVM.Notes = reader.IsDBNull(14) ? null : reader.GetString(14);
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

            return animalVM;
        }

        public AnimalVM SelectAnimalAdoptableProfile(int animalId)
        {
            AnimalVM animal = new AnimalVM();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_animal_profile_if_adoptable";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@AnimalId", animalId);

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
                        /*
                           [AnimalId], [AnimalName], [AnimalGender], [AnimalTypeId], [AnimalBreedId],		
                           [Personality], [Description], [AnimalStatusId], [RecievedDate], [MicrochipSerialNumber], 	
                           [Aggressive], [AggressiveDescription], [ChildFriendly], [NeuterStatus], [Notes], [AnimalShelterId]
                        */

                        animal.AnimalId = reader.GetInt32(0);
                        animal.AnimalName = reader.GetString(1);
                        animal.AnimalGender = reader.GetString(2);
                        animal.AnimalTypeId = reader.GetString(3);
                        animal.AnimalBreedId = reader.GetString(4);
                        animal.Personality = reader.IsDBNull(5) ? null : reader.GetString(5);
                        animal.Description = reader.IsDBNull(6) ? null : reader.GetString(6);
                        animal.AnimalStatusId = reader.GetString(7);
                        animal.BroughtIn = reader.GetDateTime(8);
                        animal.MicrochipNumber = reader.IsDBNull(9) ? null : reader.GetString(9);
                        animal.Aggressive = reader.GetBoolean(10);
                        animal.AggressiveDescription = reader.IsDBNull(11) ? null : reader.GetString(11);
                        animal.ChildFriendly = reader.GetBoolean(12);
                        animal.NeuterStatus = reader.GetBoolean(13);
                        animal.Notes = reader.IsDBNull(14) ? null : reader.GetString(14);
                        animal.AnimalShelterId = reader.GetInt32(15);
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

            return animal;
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// </summary>
        /// <param name="usersId"></param>
        /// <returns></returns>
        public List<AnimalVM> SelectAdoptedAnimalByUserId(int usersId)
        {
            List<AnimalVM> animals = new List<AnimalVM>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_animal_adopted_by_usersId";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UsersId", usersId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int animalId = reader.GetInt32(0);
                        AnimalVM animal = SelectAnimalAdoptableProfile(animalId);
                        animals.Add(animal);
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

            return animals;
        }

        public FosterPlacementRecord SelectFosterPlacementRecordNotes(int animalId)
        {
            FosterPlacementRecord fosterPlacementRecord = new FosterPlacementRecord();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_foster_placement_record_notes";
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
                        fosterPlacementRecord.FosterPlacementRecordId = reader.GetInt32(0);
                        fosterPlacementRecord.FosterPlacementId = reader.GetInt32(1);
                        fosterPlacementRecord.FosterPlacementRecordNotes = reader.GetString(2);
                        fosterPlacementRecord.FosterPlacementRecordDate = reader.GetDateTime(3);
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

            return fosterPlacementRecord;
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// </summary>
        /// <param name="fundraisingEventId"></param>
        /// <returns></returns>
        public List<AnimalVM> SelectAnimalsByFundraisingEventId(int fundraisingEventId)
        {
            //throw new NotImplementedException();
            List<AnimalVM> animals = new List<AnimalVM>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_all_animals_by_fundraising_event_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FundraisingEventId", fundraisingEventId);

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var animal = new AnimalVM();

                        animal.AnimalId = reader.GetInt32(0);
                        animal.AnimalName = reader.GetString(1);
                        animal.AnimalTypeId = reader.GetString(2);
                        animal.AnimalBreedId = reader.GetString(3);
                        animal.Personality = reader.IsDBNull(4) ? null : reader.GetString(4);
                        animal.Description = reader.IsDBNull(5) ? null : reader.GetString(5);
                        animal.AnimalStatusId = reader.GetString(6);
                        animal.BroughtIn = reader.GetDateTime(7);
                        animal.MicrochipNumber = reader.GetString(8);
                        animal.Aggressive = reader.GetBoolean(9);
                        animal.AggressiveDescription = reader.IsDBNull(10) ? null : reader.GetString(10);
                        animal.ChildFriendly = reader.GetBoolean(11);
                        animal.NeuterStatus = reader.GetBoolean(12);
                        animal.Notes = reader.IsDBNull(13) ? null : reader.GetString(13);
                        animal.AnimalShelterId = reader.GetInt32(14);
                        animal.AnimalStatusDescription = "";
                        animal.KennelName = "";
                        animal.AnimalGender = "";
                        animal.AnimalTypeDescription = "";
                        animal.AnimalBreedDescription = "";
                        animal.MedicalNotes = new List<MedicalRecord>();
                        animal.AnimalDeath = new DeathVM();
                        animal.AnimalImages = new List<Images>();

                        animals.Add(animal);
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

            return animals;
        }

        /// <summary>
        /// Author: Hoang Chu
        /// 04/27/2023
        /// </summary>
        /// <returns></returns>
        public List<AnimalVM> SelectAllAdoptableAnimals()
        {
            List<AnimalVM> adoptableAnimals = new List<AnimalVM>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_all_adoptable_animals";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var animal = new AnimalVM();

                        animal.AnimalId = reader.GetInt32(0);
                        animal.AnimalShelterId = reader.GetInt32(1);
                        animal.AnimalName = reader.GetString(2);
                        animal.AnimalTypeId = reader.GetString(3);
                        animal.AnimalBreedId = reader.GetString(4);
                        adoptableAnimals.Add(animal);
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
            return adoptableAnimals;
        }
    }
}
