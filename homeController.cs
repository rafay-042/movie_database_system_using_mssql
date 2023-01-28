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

            return View();
        }

        public ActionResult authenticate(String Id, String password)
        {
            int result = CRUD.Login(Id, password);

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


            return RedirectToAction("homePage");

        }
        public ActionResult homePage()
        {

            List<Member> mems = CRUD.getAllUsers();

            return View(mems);
        }

       public ActionResult Signup()
        {

            return View();
        }

        public ActionResult authenticate1(String userId,String name, String password,String age, String gender, String iscritic, String firstchoice, String secondchoice )
        {
            int result = CRUD.Signup(userId,name, password,age,gender,iscritic,firstchoice,secondchoice );   // crus m fucntion bnao es name se

            if (result == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("Signup", (object)data);
            }
            else if (result == 0)
            {
                String data = "Incorrect Credentials";
                return View("Signup", (object)data);
            }

            return RedirectToAction("Login");

        }

    }
}