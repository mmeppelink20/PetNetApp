using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class EventAccessor:IEventAccessor
    {
        public List<Event> SelectAllEvent()
        {
            List<Event> events = new List<Event>();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_all_events_by_EventVisible";

            // command 
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@EventVisible", EventVisible);

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
                        var ivent = new EventVM();

                        ivent.Eventid = reader.GetInt32(0);
                        ivent.EventTypeid = reader.GetString(1);
                        ivent.Shelterid = reader.GetInt32(2);
                        ivent.EventTitle = reader.GetString(3);
                        ivent.EventDescription = reader.GetString(4);
                        ivent.EventStart = reader.GetDateTime(5);
                        ivent.EventEnd = reader.GetDateTime(6);
                        ivent.EventAddress = reader.GetString(7);
                        ivent.EventZipcode = reader.GetString(8);
                        ivent.EventVisible = reader.GetBoolean(9);
                        ivent.Zipcode = reader.GetString(10);
                        
                        events.Add(ivent);
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
            return events;
        }
        public int DeleteEventByEventId(int eventid)
        {
            int rowsAffected = 0;

            var conn = new DBConnection().GetConnection();
            var cmdText = "sp_update_event_visibility_by_eventid";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Eventid", eventid);

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
        public int AddEvents(Event ivent)
        {
            int rows = 0;
            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            var cmdText = "sp_insert_event";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@EventId", ivent.Eventid);
            cmd.Parameters.AddWithValue("@EventTypeId", ivent.EventTypeid);
            cmd.Parameters.AddWithValue("@ShelterId", ivent.Shelterid);
            cmd.Parameters.AddWithValue("@EventTitle", ivent.EventTitle);
            cmd.Parameters.AddWithValue("@EventDescription", ivent.EventDescription);
            cmd.Parameters.AddWithValue("@EventStart", ivent.EventStart);
            cmd.Parameters.AddWithValue("@EventEnd", ivent.EventEnd);
            cmd.Parameters.AddWithValue("@EventAddress", ivent.EventAddress);
            cmd.Parameters.AddWithValue("@EventZipcode", ivent.EventZipcode);
            cmd.Parameters.AddWithValue("@Zipcode", ivent.Zipcode);

            Console.WriteLine(ivent.EventZipcode);
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
        public List<EventType> SelectAllEventType()
        {
            List<EventType> eventTypes = new List<EventType>();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_all_event_types";

            // command 
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@EventVisible", EventVisible);

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
                        var eventtype = new EventType();

                        eventtype.EventTypeId = reader.GetString(0);
                        eventtype.EventTypeDescription = reader.GetString(1);
             

                        eventTypes.Add(eventtype);
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
            return eventTypes;
        }
        public List<Shelter> SelectAllShelter()
        {
            List<Shelter> shelters = new List<Shelter>();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_all_shelter_id";

            // command 
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@EventVisible", EventVisible);

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
                        var shelter = new Shelter();

                        shelter.ShelterId = reader.GetInt32(0);
                        shelter.ShelterName = reader.GetString(1);
                        shelter.Address = reader.GetString(2);
                        shelter.Address2 = Convert.ToString(reader["AddressTwo"]);
                        shelter.ZipCode = reader.GetString(4);
                        shelter.Phone = reader.GetString(5);
                        shelter.Email = reader.GetString(6);
                        shelter.AreasOfNeed = reader.GetString(7);
                        shelter.ShelterActive = reader.GetBoolean(8);
                        shelters.Add(shelter);
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
            return shelters;
        }
        public int UpdateEvent(Event oldevent, Event newevent)
        {
            int rows = 0;
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();
            var cmdText = "sp_update_event_by_eventid";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@EventTypeId", newevent.EventTypeid);
            cmd.Parameters.AddWithValue("@ShelterId", newevent.Shelterid);
            cmd.Parameters.AddWithValue("@EventTitle", newevent.EventTitle);
            cmd.Parameters.AddWithValue("@EventDescription", newevent.EventDescription);
            cmd.Parameters.AddWithValue("@EventStart", newevent.EventStart);
            cmd.Parameters.AddWithValue("@EventEnd", newevent.EventEnd);
            cmd.Parameters.AddWithValue("@EventAddress", newevent.EventAddress);
            cmd.Parameters.AddWithValue("@EventZipcode", newevent.EventZipcode);
            cmd.Parameters.AddWithValue("@EventVisible", newevent.EventVisible);
            cmd.Parameters.AddWithValue("@zipcode", newevent.Zipcode);

            cmd.Parameters.AddWithValue("@oldEventId", oldevent.Eventid);
            cmd.Parameters.AddWithValue("@oldEventTypeId", oldevent.EventTypeid);
            cmd.Parameters.AddWithValue("@oldEventShelterId", oldevent.Shelterid);
            cmd.Parameters.AddWithValue("@oldEventTitle", oldevent.EventTitle);
            cmd.Parameters.AddWithValue("@oldEventDescription", oldevent.EventDescription);
            cmd.Parameters.AddWithValue("@oldEventStart", oldevent.EventStart);
            cmd.Parameters.AddWithValue("@oldEventEnd", oldevent.EventEnd);
            cmd.Parameters.AddWithValue("@oldEventAddress", oldevent.EventAddress);
            cmd.Parameters.AddWithValue("@oldEventZipcode", oldevent.EventZipcode);
            cmd.Parameters.AddWithValue("@oldEventVisible", oldevent.EventVisible);
            cmd.Parameters.AddWithValue("@oldzipcode", oldevent.Zipcode);
            //cmd.Parameters.AddWithValue("@oldIsProcedure", oldmedicalRecord.IsProcedure);
            //cmd.Parameters.AddWithValue("@oldTest", oldmedicalRecord.IsTest);
            //cmd.Parameters.AddWithValue("@oldVaccination", oldmedicalRecord.IsVaccination);
            //cmd.Parameters.AddWithValue("@oldPrescription", oldmedicalRecord.IsPrescription);
            //cmd.Parameters.AddWithValue("@oldImages", oldmedicalRecord.Images);
            //cmd.Parameters.AddWithValue("@oldQuarantineStatus", oldmedicalRecord.QuarantineStatus);
            //cmd.Parameters.AddWithValue("@oldDiagnosis", oldmedicalRecord.Diagnosis);
            //cmd.Parameters.AddWithValue("@MedicalRecordId", oldmedicalRecord.MedicalRecordId);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rows;
        }
    }
}
