using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoomCheck;
using RoomCheckWeb.Models;

namespace RoomCheckWeb.Controllers
{
    public class HomeController : Controller
    {
        public static DBRepository dbr = new DBRepository();

        public ActionResult Index()
        {
            //List<Room> rooms = dbr.GetAllRooms();
            //dbr.GetAllRoomInfo(ref rooms);

            return View();
        }

        public JsonResult GetRooms(string sidx, string sord, int page, int rows)
        {
            List<Room> rooms = dbr.GetAllRooms();
            dbr.GetAllRoomInfo(ref rooms);

            int pageindex = Convert.ToInt32(page) - 1;
            int pagesize = rows;
            int totalRecords = rooms.Count();
            var totalPages = (int) Math.Ceiling((float) totalRecords/(float) rows);

            if (sord.ToUpper() == "DESC")
            {
                rooms = rooms.OrderByDescending(room => room.RoomNo).ToList();
                rooms = rooms.Skip(pageindex*pagesize).Take(pagesize).ToList();
            }
            else
            {
                rooms = rooms.OrderBy(room => room.RoomNo).ToList();
                rooms = rooms.Skip(pageindex * pagesize).Take(pagesize).ToList();
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = rooms
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);


        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public string Create([Bind(Exclude = "ID")] Room objRoom)
        {
            string msg = "";
            //TODO: implement the rest of the code here
            return msg;
        }

        public string Edit(Room objRoom)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    dbr.UpdateRoomFull(objRoom.ID, Convert.ToInt32(objRoom.RoomTypeID),
                        Convert.ToInt32(objRoom.OccupiedStatusID));
                    msg = "Saved Successfully";
                }
                else
                {
                    msg = "Validation data not successful";
                }
            }
            catch (Exception ex)
            {
                msg = "Error Occured: " + ex.Message;
            }
            return msg;
        }

        public string Delete(int id)
        {
            Room room = dbr.GetRoomById(id);
            //TODO: dbr.DeleteRoom...
            return "Deleted SUccessfully";
        }
    }
}