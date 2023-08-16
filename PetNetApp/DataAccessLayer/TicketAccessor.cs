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
    public class TicketAccessor : ITicketAccessor
    {
        public int InsertTicket(int UserId, string TicketStatusId, string TicketTitle, string TicketContext)
        {
            int result = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_insert_ticket";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@TicketStatusId", TicketStatusId);
            cmd.Parameters.AddWithValue("@TicketTitle", TicketTitle);
            cmd.Parameters.AddWithValue("@TicketContext", TicketContext);

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

        public List<TicketVM> SelectAllTickets()
        {
            List<TicketVM> _tickets = new List<TicketVM>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_select_all_tickets";
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
                        //[TicketTitle], [TicketContext], [UserId], [TicketStatusId], [TicketId], [TicketDate], [TicketActive], [Email]
                        var ticket = new TicketVM();
                        ticket.TicketTitle = reader.GetString(0);
                        ticket.TicketContext = reader.GetString(1);
                        ticket.UserId = reader.GetInt32(2);
                        ticket.TicketStatusId = reader.GetString(3);
                        ticket.TicketId = reader.GetInt32(4);
                        ticket.TicketDate = reader.GetDateTime(5);
                        ticket.TicketActive = reader.GetBoolean(6);
                        ticket.Email = reader.GetString(7);
                        _tickets.Add(ticket);
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

            return _tickets;
        }

        public int UpdateTicketStatus(Ticket newTicket, Ticket oldTicket)
        {
            int rows = 0;

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_update_ticket";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TicketId", oldTicket.TicketId);
            cmd.Parameters.AddWithValue("@OldTicketStatusId", oldTicket.TicketStatusId);
            cmd.Parameters.AddWithValue("@NewTicketStatusId", newTicket.TicketStatusId);

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

        public List<string> SelectAllTicketStatusId()
        {
            List<string> ticketStatuses = new List<string>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_all_ticketstatusid";

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
                        ticketStatuses.Add(reader.GetString(0));
                    }
                }
                else
                {
                    throw new ArgumentException("Cannot retrieve ticket statuses.");
                }

                reader.Close();
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }


            return ticketStatuses;
        }

        public List<string> SelectEmailsByTickets()
        {
            List<string> emails = new List<string>();

            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_emails_by_tickets";

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
                        emails.Add(reader.GetString(0));
                    }
                }
                else
                {
                    throw new ArgumentException("Cannot retrieve emails.");
                }

                reader.Close();
            }
            catch (Exception up)
            {
                throw up;
            }
            finally
            {
                conn.Close();
            }

            return emails;
        }

        public List<TicketVM> SelectTicketsByTicketStatusId(string ticketStatus)
        {
            List<TicketVM> tickets = new List<TicketVM>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_tickets_by_ticketstatusid";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@TicketStatusId", SqlDbType.NVarChar, 50);

            cmd.Parameters["@TicketStatusId"].Value = ticketStatus;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var ticket = new TicketVM();

                        ticket.TicketId = reader.GetInt32(0);
                        ticket.UserId = reader.GetInt32(1);
                        ticket.TicketStatusId = reader.GetString(2);
                        ticket.TicketTitle = reader.GetString(3);
                        ticket.TicketContext = reader.GetString(4);
                        ticket.TicketDate = reader.GetDateTime(5);
                        ticket.TicketActive = reader.GetBoolean(6);
                        ticket.Email = reader.GetString(7);

                        tickets.Add(ticket);
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

            return tickets;
        }

        public List<TicketVM> SelectTicketsByEmail(string email)
        {
            List<TicketVM> tickets = new List<TicketVM>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_tickets_by_email";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 254);

            cmd.Parameters["@Email"].Value = email;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var ticket = new TicketVM();

                        ticket.TicketId = reader.GetInt32(0);
                        ticket.UserId = reader.GetInt32(1);
                        ticket.TicketStatusId = reader.GetString(2);
                        ticket.TicketTitle = reader.GetString(3);
                        ticket.TicketContext = reader.GetString(4);
                        ticket.TicketDate = reader.GetDateTime(5);
                        ticket.TicketActive = reader.GetBoolean(6);
                        ticket.Email = reader.GetString(7);

                        tickets.Add(ticket);
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

            return tickets;
        }

        public List<TicketVM> SelectTicketsByDate(string startDate, string endDate = null)
        {
            List<TicketVM> tickets = new List<TicketVM>();

            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_select_tickets_by_date";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StartDate", SqlDbType.Date);
            cmd.Parameters["@StartDate"].Value = startDate;
            if (endDate != null)
            {
                cmd.Parameters.Add("@EndDate", SqlDbType.Date);
                cmd.Parameters["@EndDate"].Value = endDate;
            }

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var ticket = new TicketVM();

                        ticket.TicketId = reader.GetInt32(0);
                        ticket.UserId = reader.GetInt32(1);
                        ticket.TicketStatusId = reader.GetString(2);
                        ticket.TicketTitle = reader.GetString(3);
                        ticket.TicketContext = reader.GetString(4);
                        ticket.TicketDate = reader.GetDateTime(5);
                        ticket.TicketActive = reader.GetBoolean(6);
                        ticket.Email = reader.GetString(7);

                        tickets.Add(ticket);
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

            return tickets;
        }
    }
}
