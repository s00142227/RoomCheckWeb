using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using RoomCheck;
using RoomCheckWeb.Models;

namespace RoomCheckWeb.Controllers
{
    public class HomeController : Controller
    {
        public static DBRepository dbr = new DBRepository();
        private List<User> users;
        public ActionResult Index()
        {
            if (Session["UserEmail"] != null)
            {
                return View();
            }
            return RedirectToAction("Login");
        }

        public JsonResult GetRooms(string sidx, string sord, int page, int rows)
        {
            List<Room> rooms = dbr.GetAllRooms();
            dbr.GetAllRoomInfo(ref rooms);

            int pageindex = Convert.ToInt32(page) - 1;
            int pagesize = rows;
            int totalRecords = rooms.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            if (sord.ToUpper() == "DESC")
            {
                rooms = rooms.OrderByDescending(room => room.RoomNo).ToList();
                rooms = rooms.Skip(pageindex * pagesize).Take(pagesize).ToList();
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
                        Convert.ToInt32(objRoom.OccupiedStatusID), objRoom.CleanStatusID, objRoom.Note);
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

        public ActionResult Login()
        {
            //if (Session["UserEmail"] != null)
            //{
            //    return RedirectToAction("Index");
            //}
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                users = dbr.GetAllUsersInfo();

                if (CheckUserCredentials(email, password))
                {

                    Session["UserEmail"] = email;
                    Session["UserName"] = users.Where(u => u.Email == email).FirstOrDefault().FirstName;
                    return RedirectToAction("Index");

                }


            }
            return View(email, password);
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");

        }

        [HttpPost]
        public JsonResult Signup(string email, string password, string firstName, string hotelId)
        {
            if (ModelState.IsValid)
            {
                users = dbr.GetAllUsersInfo();

                if (!dbr.CheckEmailExists(email))
                {
                    if (dbr.CheckHotelID(hotelId))
                    {
                        //save user to database
                        dbr.CreateUser(email, firstName, password, hotelId);

                        Session["UserEmail"] = email;
                        Session["UserName"] = firstName;
                        return Json(new { Success = true, Result = "sucess" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //show error message
                        return Json(new { Success = false, Result = "Not a valid hotel id" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    //show error message
                    return Json(new { Success = false, Result = "There is already an account with this email address" }, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(new { Success = false, Result = "Some Error" }, JsonRequestBehavior.AllowGet);
        }

        private bool CheckUserCredentials(string email, string password)
        {

            if (users.Any(u => u.Email == email))
            {
                User user = users.FirstOrDefault(u => u.Email == email);
                var salt = user.Salt;
                var passAttempt = Crypto.EncryptAes(password, "roomcheckpassword", salt);
                //these are the same when i look when debugging, do i need to convert to strings to compare?
                if (passAttempt.ToString() == user.Password.ToString())
                {
                    if (user.UserTypeID == 2)
                    {
                        return true;
                    }
                    //not the right type of user
                    return false;
                }
                //passwords don't match
                return false;
            }
            //email doesnt exist
            return false;
        }


    }
}