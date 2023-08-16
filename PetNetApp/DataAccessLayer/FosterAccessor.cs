using DataAccessLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class FosterAccessor : IFosterAccessor
    {
        public bool SelectCurrentlyAcceptingAnimalsByUsersId(int usersId)
        {
            bool isAccepting = false;

            var conn = new DBConnection().GetConnection();

            var cmdtext = "sp_select_currentlyacceptinganimals_by_usersId";

            var cmd = new SqlCommand(cmdtext, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UsersId", SqlDbType.Int);

            cmd.Parameters["@UsersId"].Value = usersId;

            try
            {
                conn.Open();

                isAccepting = Convert.ToBoolean(cmd.ExecuteScalar());
            }
            catch (Exception up)
            {

                throw up;
            }
            finally
            {
                conn.Close();
            }

            return isAccepting;

        }

        public int SelectNumberOfAnimalsApprovedByUsersId(int usersId)
        {
            int animalsApproved = 0;

            var conn = new DBConnection().GetConnection();

            var cmdtext = "sp_select_number_of_animals_approved_by_usersId";

            var cmd = new SqlCommand(cmdtext, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UsersId", SqlDbType.Int);

            cmd.Parameters["@UsersId"].Value = usersId;

            try
            {
                conn.Open();

                animalsApproved = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception up)
            {

                throw up;
            }
            finally
            {
                conn.Close();
            }

            return animalsApproved;

        }

        public int SelectNumberOfAnimalsFostererHasByUsersId(int usersId)
        {
            int animalsFostering = 0;

            var conn = new DBConnection().GetConnection();

            var cmdtext = "sp_select_number_of_animals_fosterer_has_by_usersId";

            var cmd = new SqlCommand(cmdtext, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UsersId", SqlDbType.Int);

            cmd.Parameters["@UsersId"].Value = usersId;

            try
            {
                conn.Open();

                animalsFostering = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception up)
            {

                throw up;
            }
            finally
            {
                conn.Close();
            }

            return animalsFostering;
        }

        public int UpdateCurrentlyAcceptingAnimalsByUsersId(int usersId, bool onOff)
        {

            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_update_currentlyacceptinganimals_by_usersId";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UsersId", usersId);
            cmd.Parameters.Add("@CurrentlyAcceptingAnimals", SqlDbType.Bit);
            cmd.Parameters["@CurrentlyAcceptingAnimals"].Value = onOff;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }
    }
}
