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
    public class FosterApplicationAccessor : IFosterApplicationAccessor
    {

        public List<FosterApplicationVM> SelectAllFosterApplicationsByUsersId(int usersId)
        {
            List<AnimalType> animalTypes = new List<AnimalType>();
            List<FosterApplicationVM> applications = new List<FosterApplicationVM>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_all_foster_applications_by_users_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@usersId  ", usersId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        FosterApplicationVM application = new FosterApplicationVM()
                        {
                            FosterApplicationId = reader.GetInt32(0),
                            ApplicantId = reader.GetInt32(1),
                            ApplicationStatusId = reader.GetString(2),
                            FosterApplicationDate = reader.GetDateTime(3),
                            FosterApplicationStartDate = reader.GetDateTime(4),
                            FosterApplicationMaxAnimals = reader.GetInt32(5),
                            AcceptedAnimalTypes = animalTypes
                        };

                        //String[] animalTypeArr = reader.GetString(20).Split(',');
                        foreach (string type in reader.GetString(20).Split(','))
                        {
                            AnimalType _type = new AnimalType()
                            {
                                AnimalTypeId = type
                            };
                            animalTypes.Add(_type);
                        }

                        Applicant applicant = new Applicant()
                        {
                            ApplicantId = reader.GetInt32(1),
                            UserId = reader.IsDBNull(7) ? null : (int?)reader.GetInt32(7),
                            ApplicantGivenName = reader.GetString(8),
                            ApplicantFamilyName = reader.GetString(9),
                            ApplicantAddress = reader.GetString(10),
                            ApplicantAddress2 = reader.IsDBNull(11) ? "" : reader.GetString(11),
                            ApplicantZipCode = reader.GetString(12),
                            ApplicantPhoneNumber = reader.GetString(13),
                            ApplicantEmail = reader.GetString(14),
                            HomeTypeId = reader.GetString(15),
                            HomeOwnershipId = reader.GetString(16),
                            NumberOfChildren = reader.GetInt32(17),
                            NumberOfPets = reader.GetInt32(18),
                            CurrentlyAcceptingAnimals = reader.GetBoolean(19)
                        };

                        application.FosterApplicationApplicant = applicant;
                        applications.Add(application);
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

            return applications;
        }
    }
}
