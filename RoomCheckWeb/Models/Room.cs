using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RoomCheck
{
    public class Room
    {
        public int ID { get; set; }
        public string RoomNo { get; set; }
        public int OccupiedStatusID { get; set; }
        public RoomType RoomType { get; set; }
        public RoomOccupiedStatus OccupiedStatus { get; set; }
        public RoomCleanStatus CleanStatus { get; set; }
        public int CleanStatusID { get; set; }
        public int RoomTypeID { get; set; }
        public string Note { get; set; }
        public string GuestRequest { get; set; }
        public int NoGuests { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public List<Event> Events { get; set; }
        public string CurrentEvent { get; set; }
        public DateTime Date { get; set; }

        public Room(int id,string roomNo, int occupiedStatus, int cleanStatus, int roomType, string note, int user, string request)
        {
            ID = id;
            RoomNo = roomNo;
            OccupiedStatusID = occupiedStatus;
            CleanStatusID = cleanStatus;
            RoomTypeID = roomType;
            Note = note;
            UserID = user;
            GuestRequest = request;
            //todo: add list of events here


        }

        public Room()
        {
                
        }
    }

    public class RoomOccupiedStatus
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
    }

    public class RoomCleanStatus
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
        public string BorderImage { get; set; }
    }

    public class RoomType
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
    }

    public class User
    {
        public int ID { get; set; }
        public byte[] Password { get; set; }
        public string Email { get; set; }
        public byte[] Salt { get; set; }
        public string FirstName { get; set; }
        public int UserTypeID { get; set; }
    }

    public class EventType
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
    }

    public class Event
    {
        public int ID { get; set; }
        public int EventTypeID { get; set; }
        public EventType EventType { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<Room> Rooms { get; set; }
    }
}