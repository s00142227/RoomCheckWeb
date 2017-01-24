using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using RoomCheck;

namespace RoomCheckWeb.Models
{
    public class DBRepository
    {
        public List<Room> GetAllRooms()
        {
            List<Room> rooms = new List<Room>();

            MySqlConnection con =
                   new MySqlConnection(
                       "Server=s00142227db.cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=kmorris;Password=s00142227;charset=utf8");

            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    //TODO: Where date = today and user = the user that is currently signed in
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM RoomTbl", con);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var ID = (int)reader["ID"];
                        var RoomNo = (string)reader["RoomNo"];
                        var roomOcc = (int)reader["RoomOccupiedStatusID"];
                        var roomClean = (int)reader["RoomCleanStatusID"];
                        var roomType = (int)reader["RoomTypeID"];
                        //need to handle null notes
                        var Note = "";
                        if (reader["Note"] is string)
                            Note = (string) reader["Note"];
                        var User = (int) reader["UserID"];
                        rooms.Add(new Room(ID, RoomNo, roomOcc, roomClean, roomType, Note, User));
                    }

                    

                }
            }
            catch (MySqlException ex)
            {
                //Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
            finally
            {
                con.Close();
            }

            return rooms;
        }

        public Room GetRoomById(int id)
        {
            Room room = new Room();
            MySqlConnection con =
                   new MySqlConnection(
                       "Server=s00142227db.cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=kmorris;Password=s00142227;charset=utf8");

            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM RoomTbl where ID = @id;", con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //need to handle null note
                                var note = "";
                                if (reader["Note"] is string)
                                    note = (string) reader["Note"];
                                room = new Room((int)reader["ID"], (string)reader["RoomNo"],
                                    (int)reader["RoomOccupiedStatusID"], (int)reader["RoomCleanStatusID"], (int)reader["RoomTypeID"], note, (int)reader["UserID"]);
                            }
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                //Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
            finally
            {
                con.Close();
            }

            return room;
        }

        public RoomOccupiedStatus GetOccupiedStatusById(int id)
        {
            MySqlConnection con =
                   new MySqlConnection(
                       "Server=s00142227db.cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=kmorris;Password=s00142227;charset=utf8");
            RoomOccupiedStatus occStatus = new RoomOccupiedStatus();
            try
            {
                
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM RoomOccupiedStatusTbl where ID = @id;", con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                occStatus.Description = (string) reader["Description"];
                                occStatus.IconPath = (string)reader["IconPath"];
                            }
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                //Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
            finally
            {
                con.Close();
            }

            return occStatus;
        }

        public RoomCleanStatus GetCleanStatusById(int id)
        {
            MySqlConnection con =
                   new MySqlConnection(
                       "Server=s00142227db.cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=kmorris;Password=s00142227;charset=utf8");
            RoomCleanStatus cleanStatus = new RoomCleanStatus();
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM RoomCleanStatusTbl where ID = @id;", con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cleanStatus.Description = (string)reader["Description"];
                                cleanStatus.IconPath = (string)reader["IconPath"];
                                cleanStatus.BorderImage = (string)reader["BorderImage"];
                            }
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                //Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
            finally
            {
                con.Close();
            }

            return cleanStatus;
        }
        //Replaced 'JavaList' here, so if it's not working check on that
        public List<RoomCleanStatus> GetAllCleaningStatuses()
        {
            MySqlConnection con =
                   new MySqlConnection(
                       "Server=s00142227db.cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=kmorris;Password=s00142227;charset=utf8");
            List<RoomCleanStatus> cleanStatuses = new List<RoomCleanStatus>();
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM RoomCleanStatusTbl;", con))
                    {
                        
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RoomCleanStatus cleanStatus = new RoomCleanStatus();
                                cleanStatus.ID = (int) reader["ID"];
                                cleanStatus.Description = (string)reader["Description"];
                                cleanStatus.IconPath = (string)reader["IconPath"];
                                cleanStatus.BorderImage = (string)reader["BorderImage"];
                                cleanStatuses.Add(cleanStatus);
                            }
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                //Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
            finally
            {
                con.Close();
            }

            return cleanStatuses;
        }

        public RoomType GetRoomTypeById(int id)
        {
            MySqlConnection con =
                   new MySqlConnection(
                       "Server=s00142227db.cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=kmorris;Password=s00142227;charset=utf8");
            RoomType roomType = new RoomType();
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM RoomTypeTbl where ID = @id;", con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                roomType.Description = (string)reader["Description"];
                                roomType.IconPath = (string)reader["IconPath"];
                            }
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                //Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
            finally
            {
                con.Close();
            }

            return roomType;
        }

        public void UpdateRoom(int id, string cleanStat, string note)
        {
            MySqlConnection con =
                   new MySqlConnection(
                       "Server=s00142227db.cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=kmorris;Password=s00142227;charset=utf8");

            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                    using (MySqlCommand cmd = new MySqlCommand("UPDATE RoomTbl SET RoomCleanStatusID = (SELECT ID from RoomCleanStatusTbl WHERE Description LIKE @roomclean), Note = @note WHERE ID = @id;", con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@roomclean", cleanStat);
                        cmd.Parameters.AddWithValue("@note", note);
                        cmd.ExecuteNonQuery();
                    }
                }


            }
            catch (MySqlException ex)
            {
                //Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
            finally
            {
                con.Close();
            }

        }

        public void UpdateRoomFull(int id, int roomType, int occStatus,int cleanStatus, string note)
        {
            //TODO: add more fields that might be edited
            MySqlConnection con =
                   new MySqlConnection(
                       "Server=s00142227db.cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=kmorris;Password=s00142227;charset=utf8");

            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                    using (MySqlCommand cmd = new MySqlCommand("UPDATE RoomTbl SET RoomTypeID = @roomTypeID, RoomOccupiedStatusID = @OccStatusID, RoomCleanStatusID = @CleanStatusID, Note = @Note WHERE ID = @id;", con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@roomTypeID", roomType);
                        cmd.Parameters.AddWithValue("@OccStatusID", occStatus);
                        cmd.Parameters.AddWithValue("@CleanStatusID", cleanStatus);
                        cmd.Parameters.AddWithValue("@Note", note);
                        cmd.ExecuteNonQuery();
                    }
                }


            }
            catch (MySqlException ex)
            {
                //Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
            finally
            {
                con.Close();
            }

        }

        public EventType GetEventTypeById(int id)
        {
            MySqlConnection con =
                   new MySqlConnection(
                       "Server=s00142227db.cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=kmorris;Password=s00142227;charset=utf8");
            EventType eventType = new EventType();
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM EventTypeTbl where ID = @id;", con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                eventType.ID = (int) reader["ID"];
                                eventType.Description = (string)reader["Description"];
                                eventType.IconPath = (string)reader["IconPath"];
                            }
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                //Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
            finally
            {
                con.Close();
            }

            return eventType;
        }

        public Event GetEventById(int id)
        {
            MySqlConnection con =
                   new MySqlConnection(
                       "Server=s00142227db.cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=kmorris;Password=s00142227;charset=utf8");
            Event eventE = new Event();
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM EventTbl where ID = @id;", con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                eventE.ID = (int)reader["ID"];
                                eventE.Description = (string)reader["Description"];
                                eventE.StartTime = (DateTime)reader["StartTime"];
                                eventE.EndTime = (DateTime)reader["EndTime"];
                            }
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                //Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
            finally
            {
                con.Close();
            }

            return eventE;
        }

        public List<Room> GetRoomsForEvent(int id)
        {
            MySqlConnection con =
                   new MySqlConnection(
                       "Server=s00142227db.cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=kmorris;Password=s00142227;charset=utf8");
            List<Room> rooms = new List<Room>();
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                   
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM RoomEvent where EventID = @id;", con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int roomID = (int) reader["RoomID"];
                                Room room = GetRoomById(roomID);
                                //todo: only add to list if the date is today's date
                                rooms.Add(room);
                            }
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                //Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
            finally
            {
                con.Close();
            }

            return rooms;
        }

        public bool CheckEvents(int id)
        {
            bool result = false;
            MySqlConnection con =
                   new MySqlConnection(
                       "Server=s00142227db.cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=kmorris;Password=s00142227;charset=utf8");

            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    //TODO: add in join to the table and check if the event date matches the room date (leaving this out for testing purposes)
                    using (MySqlCommand cmd = new MySqlCommand("SELECT count(*) as noEvents FROM RoomEventTbl where RoomID = @id;", con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        int mysqlint = int.Parse(cmd.ExecuteScalar().ToString());

                        result = mysqlint > 0;
                    }
                }

            }
            catch (MySqlException ex)
            {
                //Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
            finally
            {
                con.Close();
            }

            return result;
        }

        public List<Event> EventsForRoom(int id)
        {
            List<Event> events = new List<Event>();
            MySqlConnection con =
                   new MySqlConnection(
                       "Server=s00142227db.cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=kmorris;Password=s00142227;charset=utf8");
            if (CheckEvents(id))
            {
                try
                {

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        //TODO: add in join to the table and check if the event date matches the room date (leaving this out for testing purposes)
                        //TODO: populate event type for the event straight away here using joins, to prevent need for connecting again
                        using (
                            MySqlCommand cmd =
                                new MySqlCommand(
                                    "SELECT ev.* FROM EventTbl ev JOIN RoomEventTbl rev ON ev.ID = rev.EventID WHERE rev.RoomID = @id;",
                                    con))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Event ev = new Event();
                                    ev.ID = (int) reader["ID"];
                                    ev.Description = (string) reader["Description"];
                                    ev.EventTypeID = (int) reader["EventTypeID"];
                                    ev.StartTime = (DateTime) reader["StartTime"];
                                    ev.EndTime = (DateTime) reader["EndTime"];
                                    events.Add(ev);
                                }
                            }
                        }
                    }

                }
                catch (MySqlException ex)
                {
                    //Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
                }
                finally
                {
                    con.Close();
                }
            }
            

            return events;
        }
        public EventType GetEventTypeByID(int id)
        {
            EventType et = new EventType();
            MySqlConnection con =
                   new MySqlConnection(
                       "Server=s00142227db.cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=kmorris;Password=s00142227;charset=utf8");

            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM EventTypeTbl WHERE ID = @id;", con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                et.ID = (int)reader["ID"];
                                et.Description = (string)reader["Description"];
                                et.IconPath = (string) reader["IconPath"];
                            }
                        }
                    }
                }

            }
            catch (MySqlException ex)
            {
                //Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
            finally
            {
                con.Close();
            }

            return et;
        }

        public void GetAllRoomInfo(ref List<Room> rooms)
        {
            //TODO add event info here somehow
            foreach (Room room in rooms)
            {
                room.Events = EventsForRoom(room.ID);
                room.RoomType = GetRoomTypeById(Convert.ToInt32(room.RoomTypeID));
                room.CleanStatus = GetCleanStatusById(room.CleanStatusID);
                room.OccupiedStatus = GetOccupiedStatusById(Convert.ToInt32(room.OccupiedStatusID));
                //Todo: implement users here if needed
                //room.User = GetUserByID()
                foreach (Event e in room.Events)
                {
                    e.EventType = GetEventTypeByID(e.EventTypeID);
                }
                //TOdo: change this to check if any of the events are on currently and then add id
                if(room.Events.Count > 0)
                room.CurrentEvent = room.Events[0].Description;
            }

            
            
        }


    }

    
}
