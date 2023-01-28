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

            if (Session["id_"] == null )
                return View();

            else
            {

                return RedirectToAction("homePage");
            }
        }

        public ActionResult authenticate(string Id, String password)
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

            
            Session["id_"] = Id;
            return RedirectToAction("homePage");

        }
        






        public ActionResult redi()
        {
            return RedirectToAction("Signup");
        }

        public ActionResult homere()
        {
            return RedirectToAction("homepage");
        }


        // Home Page
        public ActionResult homePage()
        {
            if (Session["id_"] == null)
                return View("Login");
            else
            {
                ViewBag.User= CRUD.userDetail(Session["id_"].ToString());
                ViewBag.Movie1 = CRUD.MovieName1(Session["id_"].ToString());
                ViewBag.Movie2 = CRUD.MovieName2(Session["id_"].ToString());
                ViewBag.Movie= CRUD.MovieName(Session["id_"].ToString());
              
                    return View("homePage");
            }
        }






        public ActionResult addtowatched(string mid)
        {
            if (Session["id_"] == null)
                return View("Login");
            else
            {
                ViewBag.User = CRUD.userDetail(Session["id_"].ToString());
                ViewBag.Movie1 = CRUD.MovieName1(Session["id_"].ToString());
                ViewBag.Movie2 = CRUD.MovieName2(Session["id_"].ToString());
                ViewBag.Movie = CRUD.MovieName(Session["id_"].ToString());
                CRUD.addtowatchedlist(Session["id_"].ToString(), mid);
                return RedirectToAction("MovieDetail",new {id=mid});
            }
        }


        public ActionResult addtowatchlater(string mid)
        {
            if (Session["id_"] == null)
                return View("Login");
            else
            {
                ViewBag.User = CRUD.userDetail(Session["id_"].ToString());
                ViewBag.Movie1 = CRUD.MovieName1(Session["id_"].ToString());
                ViewBag.Movie2 = CRUD.MovieName2(Session["id_"].ToString());
                ViewBag.Movie = CRUD.MovieName(Session["id_"].ToString());
                CRUD.addtowatchlater(Session["id_"].ToString(), mid);
                return RedirectToAction("MovieDetail", new { id = mid });
            }
        }



        public ActionResult deletewatched(string mid)
        {
            if (Session["id_"] == null)
                return View("Login");
            else
            {
                ViewBag.User = CRUD.userDetail(Session["id_"].ToString());
                ViewBag.Movie1 = CRUD.MovieName1(Session["id_"].ToString());
                ViewBag.Movie2 = CRUD.MovieName2(Session["id_"].ToString());
                ViewBag.Movie = CRUD.MovieName(Session["id_"].ToString());
                CRUD.deletewatched(Session["id_"].ToString(), mid);
                return RedirectToAction("MovieDetail", new { id = mid });
            }
        }


        public ActionResult deletewatchlater(string mid)
        {
            if (Session["id_"] == null)
                return View("Login");
            else
            {
                ViewBag.User = CRUD.userDetail(Session["id_"].ToString());
                ViewBag.Movie1 = CRUD.MovieName1(Session["id_"].ToString());
                ViewBag.Movie2 = CRUD.MovieName2(Session["id_"].ToString());
                ViewBag.Movie = CRUD.MovieName(Session["id_"].ToString());
                CRUD.deletewatchlater(Session["id_"].ToString(), mid);
                return RedirectToAction("MovieDetail", new { id = mid });
            }
        }


        //Checking Search

        public ActionResult authenticate2(String name)
        {
            int result = CRUD.CheckSearch(name);

            Session["mov"] = name;
            Session["result"] = result;
            if (result == 1) //Director
            {
                return RedirectToAction("ListofMovies2");
            }
            else if (result == 2) // Actor
            {
                return RedirectToAction("ListofMovies2");
            }
            else if (result == 3) //Genre
            {
                return RedirectToAction("ListofMovies2");
            }
            else if (result == 4) //Movie
            {
                return RedirectToAction("ListofMovies2");
            }
            else if (result == 0)
                return View("Error");
            else
                return RedirectToAction("homepage");
        }

        

        //Giving Searched movie by movie name

        public ActionResult ListofMovies2()
        {
            if (Session["mov"] == null)
                return RedirectToAction("homepage");
            else
            {
                List<Movie> Movie=null;
                if (Session["result"].ToString() == "1")
                {
                     Movie = CRUD.SearchByDirector(Session["mov"].ToString());
                }
                if (Session["result"].ToString() == "2")
                {
                    Movie = CRUD.SearchByActor(Session["mov"].ToString());
                }
                if (Session["result"].ToString() == "3")
                {
                    Movie = CRUD.SearchByGenre(Session["mov"].ToString());
                }
                if (Session["result"].ToString() == "4")
                {
                    Movie = CRUD.SearchByMovie(Session["mov"].ToString());
                }
                ViewBag.User = CRUD.userDetail(Session["id_"].ToString());

                return View(Movie);
            }
        }

        

        public ActionResult userDetail()
        {
            if (Session["id_"] == null)
                return View("Login");
            else
            {
                ViewBag.User = CRUD.userDetail(Session["id_"].ToString());

                return View("userDetail");
            }
        }


        public ActionResult MovieDetail(string id)
        {
            if (Session["id_"] == null)
                return View("Login");
            else
            {
                ViewBag.Movie = CRUD.getAllMovies(id);
                ViewBag.User = CRUD.userDetail(Session["id_"].ToString());
                ViewBag.review = CRUD.GetReview1(id);

                return View("MovieDetail");
            }
        }

        public ActionResult reviews(string mid)
        {
            if (Session["id_"] == null)
                return View("Login");
            else
            {
                ViewBag.Movie = CRUD.getAllMovies(mid);
                ViewBag.User = CRUD.userDetail(Session["id_"].ToString());
                ViewBag.review = CRUD.GetReview(mid);

                return View("reviews");
            }
        }


        public ActionResult reviewadd(string rev, string rat, string mid)
        {
            if (Session["id_"] == null)
                return View("Login");
            else
            {
                ViewBag.Movie = CRUD.getAllMovies(mid);
                ViewBag.User = CRUD.userDetail(Session["id_"].ToString());
                ViewBag.review = CRUD.GetReview(mid);

                CRUD.AddReview(Session["id_"].ToString(), mid, rev, rat);

                return View("reviews");
            }
        }

        public ActionResult reviewdel(string mid)
        {
            if (Session["id_"] == null)
                return View("Login");
            else
            {
                ViewBag.Movie = CRUD.getAllMovies(mid);
                ViewBag.User = CRUD.userDetail(Session["id_"].ToString());
                ViewBag.review = CRUD.GetReview(mid);

                CRUD.DeleteRev(Session["id_"].ToString(), mid);

                return View("reviews");
            }
        }


        public ActionResult Logout()
        {

                Session["id_"] = null;
                return RedirectToAction("Login");
            
        }

        public ActionResult ActorDetail(string id)
        {
            if (Session["id_"] == null)
                return View("Login");
            else
            {
                ViewBag.Actor = CRUD.getActor(id);
                ViewBag.User = CRUD.userDetail(Session["id_"].ToString());

                return View("ActorDetail");
            }
        }




        public ActionResult GetCast(string id)
        {
            if (Session["id_"] == null)
                return View("Login");
            else
            {
                
                string Did = CRUD.GetCastB(id);

                ViewBag.User = CRUD.userDetail(Session["id_"].ToString());
                ViewBag.Actor = CRUD.GetCastA(id);
                ViewBag.Director = CRUD.getDirector(Did);

                return View("GetCast");
            }
        }

        public ActionResult Signup()
        {

            return View("signup");
        }

        public ActionResult authenticate1(String pic, String id, String name, String password, String age, String gender, String iscritic, String firstchoice, String secondchoice)
        {
            int result = CRUD.Signup(id, name, password, age, gender, iscritic, firstchoice, secondchoice,pic);   // crus m fucntion bnao es name se

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