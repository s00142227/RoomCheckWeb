using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RoomCheck;

namespace RoomCheckWeb.ViewModels
{
    public class RoomEventType
    {
        public List<Room> Rooms { get; set; }
        public List<EventType> EventTypes { get; set; }
    }
}