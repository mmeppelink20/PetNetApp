// Created by Asa Armstrong
// Created on 2023/02/02

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class DeathAccessor : IDeathAccessor
    {
        public int InsertAnimalDeath(Death death)
        {
            int rowsAffected = 0;

            var conn = new DBConnection().GetConnection();
            var cmdText = "sp_insert_animal_death";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UsersId", death.UsersId);
            cmd.Parameters.AddWithValue("@AnimalId", death.AnimalId);
            cmd.Parameters.AddWithValue("@DeathDate", death.DeathDate);
            cmd.Parameters.AddWithValue("@DeathCause", death.DeathCause);
            cmd.Parameters.AddWithValue("@DeathDisposal", death.DeathDisposal);
            cmd.Parameters.AddWithValue("@DeathDisposalDate", death.DeathDisposalDate);

            //cmd.Parameters.AddWithValue("@DeathNotes", death.DeathNotes); //accounting for null or empty string
            //cmd.Parameters.AddWithValue("@DeathNotes", (death.DeathNotes.Length == 0 || death.DeathNotes.Equals(null) ? DBNull.Value : death.DeathNotes));
            if (death.DeathNotes == null || death.DeathNotes.Length == 0)
            {
                cmd.Parameters.AddWithValue("@DeathNotes", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DeathNotes", death.DeathNotes);
            }

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

        public DeathVM SelectAnimalDeath(Animal animal)
        {
            DeathVM deathVM = new DeathVM();

            var conn = new DBConnection().GetConnection();
            var cmdText = "sp_select_animal_death";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AnimalId", animal.AnimalId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        deathVM.DeathId = reader.GetInt32(0);
                        deathVM.UsersId = reader.GetInt32(1);
                        deathVM.AnimalId = reader.GetInt32(2);
                        deathVM.DeathDate = reader.GetDateTime(3);
                        deathVM.DeathCause = reader.GetString(4);
                        deathVM.DeathDisposal = reader.GetString(5);
                        deathVM.DeathDisposalDate = reader.GetDateTime(6);
                        deathVM.DeathNotes = reader.IsDBNull(7) ? null : reader.GetString(7);
                        deathVM.AnimalName = reader.GetString(8);
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

            return deathVM;
        }

        public int UpdateAnimalDeath(Death newDeath, Death oldDeath)
        {
            int rowsAffected = 0;

            var conn = new DBConnection().GetConnection();
            var cmdText = "sp_update_animal_death";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DeathId", oldDeath.DeathId);
            cmd.Parameters.AddWithValue("@AnimalId", oldDeath.AnimalId);
            cmd.Parameters.AddWithValue("@UsersId", oldDeath.UsersId);

            cmd.Parameters.AddWithValue("@NewDeathDate", newDeath.DeathDate);
            cmd.Parameters.AddWithValue("@NewDeathCause", newDeath.DeathCause);
            cmd.Parameters.AddWithValue("@NewDeathDisposal", newDeath.DeathDisposal);
            cmd.Parameters.AddWithValue("@NewDeathDisposalDate", newDeath.DeathDisposalDate);
            if (newDeath.DeathNotes == null || newDeath.DeathNotes.Length == 0)
            {
                cmd.Parameters.AddWithValue("@NewDeathNotes", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NewDeathNotes", newDeath.DeathNotes);
            }

            cmd.Parameters.AddWithValue("@OldDeathDate", oldDeath.DeathDate);
            cmd.Parameters.AddWithValue("@OldDeathCause", oldDeath.DeathCause);
            cmd.Parameters.AddWithValue("@OldDeathDisposal", oldDeath.DeathDisposal);
            cmd.Parameters.AddWithValue("@OldDeathDisposalDate", oldDeath.DeathDisposalDate);
            if (oldDeath.DeathNotes == null || oldDeath.DeathNotes.Length == 0)
            {
                cmd.Parameters.AddWithValue("@OldDeathNotes", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OldDeathNotes", oldDeath.DeathNotes);
            }

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rowsAffected;
        }
    }
}
