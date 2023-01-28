using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using db_connectivity.Models;

namespace db_connectivity.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            if (Session["user_id"]==null)
            return View();

            else
            {
                return RedirectToAction("homePage");
            }
        }

        public ActionResult authenticate(String userId, String password)
        {
            int result = CRUD.Login(userId, password);

            if (result == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("Login", (object)data);
            }
            else if (result == 0)
            {

                String data = "Incorrect Credentials";
                return View("Login", (object)data);
            }

            Session["user_id"] = userId;
            return RedirectToAction("homePage");

        }
        public ActionResult homePage()
        {
            if (Session["user_id"] == null)
                return View("login");

            else
            {
                User user = CRUD.getUser(Session["user_id"].ToString());

                return View(user);
            }
        }

    }
}
