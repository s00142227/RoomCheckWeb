using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RoomCheck;

namespace RoomCheckWeb.ViewModels
{
    public class RoomCleaner
    {
        public List<User> Cleaners { get; set; }
        public List<Room> Rooms { get; set; }
    }
}