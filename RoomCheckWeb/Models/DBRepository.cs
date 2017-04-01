using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.Helpers;
using Microsoft.Ajax.Utilities;
using RoomCheck;
using Crypto = RoomCheckWeb.Controllers.Crypto;

namespace RoomCheckWeb.Models
{
    public class DBRepository
    {
        List<Room> roomsForInput = new List<Room>();
        private MySqlConnection con =
                   new MySqlConnection(
"Server=s00142227db.cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=kmorris;Password=Lollipop12;charset=utf8");


        public List<Room> GetAllRooms()
        {
            List<Room> rooms = new List<Room>();

            //            MySqlConnection con =
            //                   new MySqlConnection(
            //"Server=roomcheckaurora.cluster-cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=s00142227;Password=Lollipop12;charset=utf8");
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    //TODO: Where date = today and user = the user that is currently signed in
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM RoomTbl where Date = '2016-10-31'", con);
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
                            Note = (string)reader["Note"];
                        var Request = "";
                        if (reader["GuestRequest"] is string)
                            Request = (string)reader["GuestRequest"];
                        var User = (int)reader["UserID"];
                        rooms.Add(new Room(ID, RoomNo, roomOcc, roomClean, roomType, Note, User, Request));
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
            //            MySqlConnection con =
            //                   new MySqlConnection(
            //"Server=roomcheckaurora.cluster-cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=s00142227;Password=Lollipop12;charset=utf8");
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
                                    note = (string)reader["Note"];
                                var request = "";
                                if (reader["GuestRequest"] is string)
                                    note = (string)reader["GuestRequest"];
                                room = new Room((int)reader["ID"], (string)reader["RoomNo"],
                                    (int)reader["RoomOccupiedStatusID"], (int)reader["RoomCleanStatusID"], (int)reader["RoomTypeID"], note, (int)reader["UserID"], request);
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

        public List<User> GetAllUsersInfo()
        {
            //MySqlConnection con =
            //       new MySqlConnection(
            //           "Server=roomcheckaurora.cluster-cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=s00142227;Password=Lollipop12;charset=utf8");
            List<User> users = new List<User>();

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("select * from UserTbl", con))
                    {

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User u = new User();
                                u.ID = (int)reader["ID"];
                                u.Email = (string)reader["Email"];
                                u.Password = (byte[])reader["Password"];
                                u.Salt = (byte[])reader["Salt"];
                                u.FirstName = (string)reader["FirstName"];
                                u.UserTypeID = (int)reader["UserTypeID"];
                                users.Add(u);
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

            return users;
        }

        public List<User> GetAllCleaners()
        {
            List<User> users = new List<User>();

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("select * from UserTbl where UserTypeID = 1", con))
                    {

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User u = new User();
                                u.ID = (int)reader["ID"];
                                u.Email = (string)reader["Email"];
                                u.Password = (byte[])reader["Password"];
                                u.Salt = (byte[])reader["Salt"];
                                u.FirstName = (string)reader["FirstName"];
                                u.UserTypeID = (int)reader["UserTypeID"];
                                users.Add(u);
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

            return users;
        }

        public List<EventType> GetAllEventTypes()
        {
            //MySqlConnection con =
            //       new MySqlConnection(
            //           "Server=roomcheckaurora.cluster-cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=s00142227;Password=Lollipop12;charset=utf8");
            List<EventType> types = new List<EventType>();

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("select * from EventTypeTbl", con))
                    {

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EventType e = new EventType();
                                e.ID = (int)reader["ID"];
                                e.Description = (string)reader["Description"];
                                e.IconPath = (string)reader["IconPath"];

                                types.Add(e);
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

            return types;
        }

        public List<int> GetRoomIDs(List<string> rooms, DateTime date)
        {
            List<int> ids = new List<int>();

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    foreach (string room in rooms)
                    {

                        using (MySqlCommand cmd = new MySqlCommand("select ID from RoomTbl where RoomNo = @roomNo and Date = @date", con))
                        {
                            cmd.Parameters.AddWithValue("@roomNo", room);
                            cmd.Parameters.AddWithValue("@date", date);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int id = 0;
                                    id = (int)reader["ID"];

                                    ids.Add(id);
                                }
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

            return ids;
        }

        public void CreateUser(string email, string firstName, string password, string hotelId)
        {

            var salt = Crypto.CreateSalt(16);
            var bytes = Crypto.EncryptAes(password, "roomcheckpassword", salt);

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (
                        MySqlCommand cmd =
                            new MySqlCommand(
                                "INSERT INTO UserTbl (`Email`,`Password`,`Salt`,`UserTypeID`,`FirstName`) VALUES (@email, @password, @salt, 2, @firstname);",
                                con))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@password", bytes);
                        cmd.Parameters.AddWithValue("@salt", salt);
                        cmd.Parameters.AddWithValue("@firstname", firstName);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

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

        public void AddRoomToList(string roomNo, int roomTypeId, string cleaners, string date, List<User> users)
        {
            Room room = new Room();
            room.RoomNo = roomNo;
            room.RoomTypeID = roomTypeId;
            room.RoomType = GetRoomTypeById(1);
            room.CleanStatusID = 1;
            room.CleanStatus = GetCleanStatusById(1);
            room.Date = Convert.ToDateTime(date);
            User cleaner = new User();
            var cleanerdata = Json.Decode(cleaners);
            foreach (var c in cleanerdata)
            {
                if (c.Key == roomNo)
                {
                    cleaner = users.Where(u => u.FirstName.ToUpper() == c.Value.ToUpper()).FirstOrDefault();
                }
            }
            room.User = cleaner;
            if (cleaner != null) room.UserID = cleaner.ID;
            else
            {
                room.UserID = 6;
            }

            roomsForInput.Add(room);
        }



        public void InputRoomData(string date, string stays, string deps, string empties, string cleaners)
        {

            List<User> users = GetAllUsersInfo();
            foreach (string stay in stays.Split(','))
            {
                AddRoomToList(stay, 1, cleaners, date, users);

            }
            foreach (string dep in deps.Split(','))
            {
                AddRoomToList(dep, 2, cleaners, date, users);
            }
            foreach (string empty in empties.Split(','))
            {
                AddRoomToList(empty, 3, cleaners, date, users);

            }

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                    foreach (Room room in roomsForInput)
                    {
                        if (room.UserID == 0) room.UserID = 6;

                        using (
                            MySqlCommand cmd =
                                new MySqlCommand(
                                    "INSERT INTO RoomTbl (`RoomNo`,`RoomOccupiedSTatusID`,`RoomCleanStatusID`,`RoomTypeID`,`UserID`, `Date`) VALUES (@roomNo, 1, @roomClean, @roomType, @user, @date);",
                                    con))
                        {
                            cmd.Parameters.AddWithValue("@roomNo", room.RoomNo);
                            cmd.Parameters.AddWithValue("@roomClean", 1);
                            cmd.Parameters.AddWithValue("@roomType", room.RoomTypeID);
                            cmd.Parameters.AddWithValue("@user", room.UserID);
                            cmd.Parameters.AddWithValue("@date", room.Date);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {

                                }
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

        public void InputEvent(string date, string rooms, string name, string type, string beginTime, string endTime)
        {

            List<string> roomNos = new List<string>();
            foreach (string room in rooms.Split(','))
            {
                roomNos.Add(room);
            }

            long eventId = 0;
            List<int> roomIDs = GetRoomIDs(roomNos, Convert.ToDateTime(date));
            int eventTypeId = GetEventTypeIDByName(type);
            //todo: convert times to datetimes with the date given
            DateTime timeFrom = Convert.ToDateTime(date + " " + beginTime);
            DateTime timeTo = Convert.ToDateTime(date + " " + endTime);

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();


                    using (
                        MySqlCommand cmd =
                            new MySqlCommand(
                                "INSERT INTO EventTbl (`EventTypeID`,`StartTime`,`EndTime`,`Description`) " +
                                "VALUES (@eventTypeID, @StartTime, @EndTime, @Description); Select LAST_INSERT_ID() as ID;",
                                con))
                    {
                        cmd.Parameters.AddWithValue("@eventTypeID", eventTypeId);
                        cmd.Parameters.AddWithValue("@StartTime", timeFrom);
                        cmd.Parameters.AddWithValue("@EndTime", timeTo);
                        cmd.Parameters.AddWithValue("@Description", name);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                              var result = reader["ID"];
                                eventId = Convert.ToInt64(result);
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
                //TODO: for each room insert into roomeventtbl
                InsertRoomEventInfo(roomIDs, eventId);
            }
        }
        public void InsertRoomEventInfo(List<int> rooms, long eventId)
        {
            try
            {
                

                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();

                    foreach (int id in rooms)
                    {
                        using (
                            MySqlCommand cmd =
                                new MySqlCommand(
                                    "INSERT INTO RoomEventTbl (`RoomID`,`EventID`) VALUES (@id, @eventId);",
                                    con))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.Parameters.AddWithValue("@eventId", eventId);
                             using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                }
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




        public bool CheckHotelID(string id)
        {
            //MySqlConnection con =
            //       new MySqlConnection(
            //           "Server=roomcheckaurora.cluster-cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=s00142227;Password=Lollipop12;charset=utf8");

            int counter = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Hotels WHERE HotelID = @id;", con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                counter++;
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

            return counter > 0;
        }

        public bool CheckEmailExists(string email)
        {
            //MySqlConnection con =
            //       new MySqlConnection(
            //           "Server=roomcheckaurora.cluster-cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=s00142227;Password=Lollipop12;charset=utf8");

            int counter = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM UserTbl WHERE Email = @email;", con))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                counter++;
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

            return counter > 0;
        }

        public RoomOccupiedStatus GetOccupiedStatusById(int id)
        {
            //            MySqlConnection con =
            //                   new MySqlConnection(
            //"Server=roomcheckaurora.cluster-cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=s00142227;Password=Lollipop12;charset=utf8");
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
                                occStatus.Description = (string)reader["Description"];
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
            //            MySqlConnection con =
            //                   new MySqlConnection(
            //"Server=roomcheckaurora.cluster-cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=s00142227;Password=Lollipop12;charset=utf8");
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
            //            MySqlConnection con =
            //                   new MySqlConnection(
            //"Server=roomcheckaurora.cluster-cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=s00142227;Password=Lollipop12;charset=utf8");
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
                                cleanStatus.ID = (int)reader["ID"];
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
            //            MySqlConnection con =
            //                   new MySqlConnection(
            //"Server=roomcheckaurora.cluster-cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=s00142227;Password=Lollipop12;charset=utf8"); 
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

        public void UpdateRoom(int id, string cleanStat, string note, string request)
        {
             try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                    using (MySqlCommand cmd = new MySqlCommand("UPDATE RoomTbl SET RoomCleanStatusID = (SELECT ID from RoomCleanStatusTbl WHERE Description LIKE @roomclean), Note = @note, GuestRequest = @request WHERE ID = @id;", con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@roomclean", cleanStat);
                        cmd.Parameters.AddWithValue("@note", note);
                        cmd.Parameters.AddWithValue("@request", note);
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

        public void UpdateRoomFull(int id, int roomType, int occStatus, int cleanStatus, string note, string request)
        {
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                    using (MySqlCommand cmd = new MySqlCommand("UPDATE RoomTbl SET RoomTypeID = @roomTypeID, RoomOccupiedStatusID = @OccStatusID, RoomCleanStatusID = @CleanStatusID, Note = @Note, GuestRequest = @GuestRequest WHERE ID = @id;", con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@roomTypeID", roomType);
                        cmd.Parameters.AddWithValue("@OccStatusID", occStatus);
                        cmd.Parameters.AddWithValue("@CleanStatusID", cleanStatus);
                        cmd.Parameters.AddWithValue("@Note", note);
                        cmd.Parameters.AddWithValue("@GuestRequest", request);
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
            //            MySqlConnection con =
            //                   new MySqlConnection(
            //"Server=roomcheckaurora.cluster-cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=s00142227;Password=Lollipop12;charset=utf8");
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
                                eventType.ID = (int)reader["ID"];
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
            //            MySqlConnection con =
            //                   new MySqlConnection(
            //"Server=roomcheckaurora.cluster-cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=s00142227;Password=Lollipop12;charset=utf8");
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
            //            MySqlConnection con =
            //                   new MySqlConnection(
            //"Server=roomcheckaurora.cluster-cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=s00142227;Password=Lollipop12;charset=utf8");
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
                                int roomID = (int)reader["RoomID"];
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
            //            MySqlConnection con =
            //                   new MySqlConnection(
            //"Server=roomcheckaurora.cluster-cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=s00142227;Password=Lollipop12;charset=utf8");
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
            //            MySqlConnection con =
            //                   new MySqlConnection(
            //"Server=roomcheckaurora.cluster-cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=s00142227;Password=Lollipop12;charset=utf8");
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
                                    ev.ID = (int)reader["ID"];
                                    ev.Description = (string)reader["Description"];
                                    ev.EventTypeID = (int)reader["EventTypeID"];
                                    ev.StartTime = (DateTime)reader["StartTime"];
                                    ev.EndTime = (DateTime)reader["EndTime"];
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
            //            MySqlConnection con =
            //                   new MySqlConnection(
            //"Server=roomcheckaurora.cluster-cshbhaowu4cu.eu-west-1.rds.amazonaws.com;Port=3306;database=RoomCheckDB;User Id=s00142227;Password=Lollipop12;charset=utf8");
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
                                et.IconPath = (string)reader["IconPath"];
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

        public int GetEventTypeIDByName(string name)
        {
            int id = 0;
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM EventTypeTbl WHERE Description = @name;", con))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                id = (int)reader["ID"];

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

            return id;
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
                if (room.Events.Count > 0)
                    room.CurrentEvent = room.Events[0].Description;
            }



        }


    }


}
