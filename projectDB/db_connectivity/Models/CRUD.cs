using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;

namespace db_connectivity.Models
{
    public class CRUD
    {
        public static string connectionString = "data source=BATMAN; Initial Catalog=IMDB18;Integrated Security=true";

        public static List<User> getAllUsers()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("ViewMember", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader rdr = cmd.ExecuteReader();

                List<User> list = new List<User>();
                while (rdr.Read())
                {
                    User mem = new User();
                    mem.id = rdr["id"].ToString();
                    mem.name = rdr["name"].ToString();
                    mem.password = rdr["password_"].ToString();
                    mem.age = rdr["age"].ToString();
                    mem.gender = rdr["gender"].ToString();
                    mem.iscritic = rdr["iscritic"].ToString();
                    mem.firstchoice = rdr["firstchoice"].ToString();
                    mem.secondchoice = rdr["secondchoice"].ToString();

                    list.Add(mem);

                }
                rdr.Close();
                con.Close();

                return list;


            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }

        }








        public static List<User> userDetail(string id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("ViewMember1", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                SqlDataReader rdr = cmd.ExecuteReader();

                List<User> list = new List<User>();
                while (rdr.Read())
                {
                    User mem = new User();
                    mem.id = rdr["id"].ToString();
                    mem.name = rdr["name"].ToString();
                    mem.password = rdr["password_"].ToString();
                    mem.age = rdr["age"].ToString();
                    mem.gender = rdr["gender"].ToString();
                    mem.iscritic = rdr["iscritic"].ToString();
                    mem.firstchoice = rdr["firstchoice"].ToString();
                    mem.secondchoice = rdr["secondchoice"].ToString();
                    mem.pic = rdr["pic"].ToString();
                    

                    list.Add(mem);

                }

                rdr.Close();
                con.Close();
                return list;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                
            }

            return null;

        }



        public static List<Movie> MovieName(string id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("Sugesstions", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                SqlDataReader rdr = cmd.ExecuteReader();

                List<Movie> list = new List<Movie>();
                while (rdr.Read())
                {
                    Movie mem = new Movie();
                    mem.name = rdr["name"].ToString();
                    mem.id = rdr["id"].ToString();
                    mem.picture = rdr["picture"].ToString();
                    
                    list.Add(mem);

                }

                rdr.Close();
                con.Close();
                return list;

            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }

            return null;

        }




        //WATCH LATER MOVIE

        public static List<Movie1> MovieName1(string id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("getWatchedList", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                SqlDataReader rdr = cmd.ExecuteReader();

                List<Movie1> list = new List<Movie1>();
                while (rdr.Read())
                {
                    Movie1 mem = new Movie1();
                    mem.name = rdr["name"].ToString();
                    mem.id = rdr["id"].ToString();
                    mem.picture = rdr["picture"].ToString();

                    list.Add(mem);

                }

                rdr.Close();
                con.Close();
                return list;

            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }

            return null;

        }



        //WATCHED MOVIES

        public static List<Movie2> MovieName2(string id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("getWatchLater", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                SqlDataReader rdr = cmd.ExecuteReader();

                List<Movie2> list = new List<Movie2>();
                while (rdr.Read())
                {
                    Movie2 mem = new Movie2();
                    mem.name = rdr["name"].ToString();
                    mem.id = rdr["id"].ToString();
                    mem.picture = rdr["picture"].ToString();

                    list.Add(mem);

                }

                rdr.Close();
                con.Close();
                return list;

            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());

            }

            return null;

        }




        public static int Login(string id, string password_)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("login", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.NVarChar, 10).Value = id;
                cmd.Parameters.Add("@password_", SqlDbType.NVarChar, 20).Value = password_;


                cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@status"].Value);



            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;

        }






        public static List<Movie> getAllMovies(string id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("ViewMovie", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                SqlDataReader rdr = cmd.ExecuteReader();

                List<Movie> list = new List<Movie>();
                while (rdr.Read())
                {
                    Movie mov = new Movie();

                    mov.id = rdr["id"].ToString();
                    mov.age_rating = rdr["age_rating"].ToString();
                    mov.name = rdr["name"].ToString();
                    mov.description = rdr["description"].ToString();
                    mov.release_date = rdr["release_date"].ToString();
                    mov.rating = rdr["rating"].ToString();
                    mov.genre = rdr["genre"].ToString();
                    mov.duration = rdr["duration"].ToString();
                    mov.Language = rdr["Language"].ToString();
                    mov.budget = rdr["budget"].ToString();
                    mov.box_office = rdr["box_office"].ToString();
                    mov.producer = rdr["producer"].ToString();
                    mov.picture = rdr["picture"].ToString();

                    list.Add(mov);

                }
                rdr.Close();
                con.Close();

                return list;


            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }

        }




        //GET ACTOR

        public static List<Actor> getActor(string id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("DisplayActor", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                SqlDataReader rdr = cmd.ExecuteReader();

                List<Actor> list = new List<Actor>();
                while (rdr.Read())
                {
                    Actor mov = new Actor();

                    mov.id = rdr["id"].ToString();
                    mov.name = rdr["name"].ToString();
                    mov.age = rdr["age"].ToString();
                    mov.gender = rdr["gender"].ToString();
                    mov.about = rdr["about"].ToString();
                    mov.awards = rdr["awards"].ToString();
                    mov.picture = rdr["picture"].ToString();
                    

                    list.Add(mov);

                }
                rdr.Close();
                con.Close();

                return list;


            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }

        }



        //GET Director


        public static List<Director> getDirector(string id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("DisplayDirector", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                SqlDataReader rdr = cmd.ExecuteReader();

                List<Director> list = new List<Director>();
                while (rdr.Read())
                {
                    Director mov = new Director();

                    mov.id = rdr["id"].ToString();
                    mov.name = rdr["name"].ToString();
                    mov.age = rdr["age"].ToString();
                    mov.gender = rdr["gender"].ToString();
                    mov.about = rdr["about"].ToString();
                    mov.awards = rdr["awards"].ToString();
                   
                    list.Add(mov);

                }
                rdr.Close();
                con.Close();

                return list;


            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }

        }




        //GET CAST




        public static List<Actor> GetCastA(string id)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("getCast", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                SqlDataReader rdr = cmd.ExecuteReader();

                string a = null;
                List<Actor> list = new List<Actor>();
                while (rdr.Read())
                {
                    Actor ac = new Actor();
                    ac.id = rdr["id"].ToString();
                    a = rdr["did"].ToString();
                    ac.name = rdr["name"].ToString();
                    ac.picture = rdr["picture"].ToString();

                    list.Add(ac);
                }
                rdr.Close();
                con.Close();

                return list;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
        }


        public static string GetCastB(string id)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("getCast", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                SqlDataReader rdr = cmd.ExecuteReader();

                string a = null;
                List<Actor> list = new List<Actor>();
                while (rdr.Read())
                {
                    Actor ac = new Actor();
                    ac.id = rdr["id"].ToString();
                    a = rdr["did"].ToString();
                    ac.name = rdr["name"].ToString();
                    ac.picture = rdr["picture"].ToString();

                }
                rdr.Close();
                con.Close();

                return a;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
        }



        //add to watched list
        public static string addtowatchedlist(string id,string movid)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("Watchedadd", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@movid", SqlDbType.Int).Value = movid;

                SqlDataReader rdr = cmd.ExecuteReader();

                string a = null;
                
                rdr.Close();
                con.Close();

                return a;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
        }

        //add to watch later
        public static string addtowatchlater(string id, string movid)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("WatchLateradd", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@movid", SqlDbType.Int).Value = movid;

                SqlDataReader rdr = cmd.ExecuteReader();

                string a = null;

                rdr.Close();
                con.Close();

                return a;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
        }


        //delete to watched 
        public static string deletewatched(string id, string movid)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("Watchedremove", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@movid", SqlDbType.Int).Value = movid;

                SqlDataReader rdr = cmd.ExecuteReader();

                string a = null;

                rdr.Close();
                con.Close();

                return a;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
        }



        //delete to watch later
        public static string deletewatchlater(string id, string movid)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("WatchLaterremove", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@movid", SqlDbType.Int).Value = movid;

                SqlDataReader rdr = cmd.ExecuteReader();

                string a = null;

                rdr.Close();
                con.Close();

                return a;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
        }


        //Checking Search

        public static int CheckSearch(string name)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("CheckSearch", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = name;

                cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@status"].Value);

            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return -1;
            }
            finally
            {
                con.Close();
            }
            return result;

        }

        //Movie Searching

        public static List<Movie> SearchByMovie(string name)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("SearchByMovie", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@input", SqlDbType.NVarChar, 50).Value = name;

                SqlDataReader rdr = cmd.ExecuteReader();

                List<Movie> list = new List<Movie>();
                while (rdr.Read())
                {
                    Movie mov = new Movie();

                    mov.id = rdr["id"].ToString();
                    mov.age_rating = rdr["age_rating"].ToString();
                    mov.name = rdr["name"].ToString();
                    mov.description = rdr["description"].ToString();
                    mov.release_date = rdr["release_date"].ToString();
                    mov.rating = rdr["rating"].ToString();
                    mov.genre = rdr["genre"].ToString();
                    mov.duration = rdr["duration"].ToString();
                    mov.Language = rdr["Language"].ToString();
                    mov.budget = rdr["budget"].ToString();
                    mov.box_office = rdr["box_office"].ToString();
                    mov.producer = rdr["producer"].ToString();
                    mov.picture = rdr["picture"].ToString();

                    list.Add(mov);

                }
                rdr.Close();
                con.Close();

                return list;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
        }


        //GetReview


        public static List<review> GetReview(string movid)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("GetReview2", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@movid", SqlDbType.Int).Value = movid;
          

                SqlDataReader rdr = cmd.ExecuteReader();

                List<review> list = new List<review>();
                while (rdr.Read())
                {
                    review mov = new review();

                    mov.rating = rdr["rating"].ToString();
                    mov.comments = rdr["comments"].ToString();
                    mov.name = rdr["name"].ToString();

                    list.Add(mov);

                }
                rdr.Close();
                con.Close();

                return list;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
        }



        //GetReview1


        public static List<review> GetReview1(string movid)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("GetReview", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@movid", SqlDbType.Int).Value = movid;


                SqlDataReader rdr = cmd.ExecuteReader();

                List<review> list = new List<review>();
                while (rdr.Read())
                {
                    review mov = new review();

                    mov.rating = rdr["rat"].ToString();
                   
                    list.Add(mov);

                }
                rdr.Close();
                con.Close();

                return list;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
        }





        //GiveReview


        public static string AddReview(string id,string movid,string review , string rating)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("GiveReview", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@review", SqlDbType.VarChar, 500).Value = review;
                cmd.Parameters.Add("@memid", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@ratin", SqlDbType.Float).Value = rating;
                cmd.Parameters.Add("@movid", SqlDbType.Int).Value = movid;


                SqlDataReader rdr = cmd.ExecuteReader();

                string list = null;
                while (rdr.Read())
                {
                    
                    
                }
                rdr.Close();
                con.Close();

                return list;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
        }









        //GiveReview


        public static string DeleteRev(string id, string movid)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("DeleteReview", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@memid", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@movid", SqlDbType.Int).Value = movid;


                SqlDataReader rdr = cmd.ExecuteReader();

                string list = null;
                while (rdr.Read())
                {


                }
                rdr.Close();
                con.Close();

                return list;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
        }








        //Actor Searching

        public static List<Movie> SearchByActor(string name)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("SearchByActor", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@input", SqlDbType.NVarChar, 50).Value = name;

                SqlDataReader rdr = cmd.ExecuteReader();

                List<Movie> list = new List<Movie>();
                while (rdr.Read())
                {
                    Movie mov = new Movie();

                    mov.id = rdr["id"].ToString();
                    mov.age_rating = rdr["age_rating"].ToString();
                    mov.name = rdr["name"].ToString();
                    mov.description = rdr["description"].ToString();
                    mov.release_date = rdr["release_date"].ToString();
                    mov.rating = rdr["rating"].ToString();
                    mov.genre = rdr["genre"].ToString();
                    mov.duration = rdr["duration"].ToString();
                    mov.Language = rdr["Language"].ToString();
                    mov.budget = rdr["budget"].ToString();
                    mov.box_office = rdr["box_office"].ToString();
                    mov.producer = rdr["producer"].ToString();
                    mov.picture = rdr["picture"].ToString();

                    list.Add(mov);

                }
                rdr.Close();
                con.Close();

                return list;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
        }

        //Director Searching

        public static List<Movie> SearchByDirector(string name)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("SearchByDirector", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@input", SqlDbType.NVarChar, 50).Value = name;

                SqlDataReader rdr = cmd.ExecuteReader();

                List<Movie> list = new List<Movie>();
                while (rdr.Read())
                {
                    Movie mov = new Movie();

                    mov.id = rdr["id"].ToString();
                    mov.age_rating = rdr["age_rating"].ToString();
                    mov.name = rdr["name"].ToString();
                    mov.description = rdr["description"].ToString();
                    mov.release_date = rdr["release_date"].ToString();
                    mov.rating = rdr["rating"].ToString();
                    mov.genre = rdr["genre"].ToString();
                    mov.duration = rdr["duration"].ToString();
                    mov.Language = rdr["Language"].ToString();
                    mov.budget = rdr["budget"].ToString();
                    mov.box_office = rdr["box_office"].ToString();
                    mov.producer = rdr["producer"].ToString();
                    mov.picture = rdr["picture"].ToString();

                    list.Add(mov);

                }
                rdr.Close();
                con.Close();

                return list;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
        }

        //Search by Genre

        public static List<Movie> SearchByGenre(string name)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("SearchByGenre", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@input", SqlDbType.NVarChar, 50).Value = name;

                SqlDataReader rdr = cmd.ExecuteReader();

                List<Movie> list = new List<Movie>();
                while (rdr.Read())
                {
                    Movie mov = new Movie();

                    mov.id = rdr["id"].ToString();
                    mov.age_rating = rdr["age_rating"].ToString();
                    mov.name = rdr["name"].ToString();
                    mov.description = rdr["description"].ToString();
                    mov.release_date = rdr["release_date"].ToString();
                    mov.rating = rdr["rating"].ToString();
                    mov.genre = rdr["genre"].ToString();
                    mov.duration = rdr["duration"].ToString();
                    mov.Language = rdr["Language"].ToString();
                    mov.budget = rdr["budget"].ToString();
                    mov.box_office = rdr["box_office"].ToString();
                    mov.producer = rdr["producer"].ToString();
                    mov.picture = rdr["picture"].ToString();

                    list.Add(mov);

                }
                rdr.Close();
                con.Close();

                return list;
            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
        }








        public static int Signup(string id, string name, string password_, string age, string gender, string iscritic, string firstchoice, string secondchoice,string pics)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("SIGNUP", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@pic", SqlDbType.Text).Value = pics;
                cmd.Parameters.Add("@id", SqlDbType.NVarChar, 10).Value = id;
                cmd.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = name;
                cmd.Parameters.Add("@password_", SqlDbType.NVarChar, 20).Value = password_;
                cmd.Parameters.Add("@age", SqlDbType.NVarChar, 10).Value = age;
                cmd.Parameters.Add("@gender", SqlDbType.VarChar, 50).Value = gender;
                cmd.Parameters.Add("@iscritic", SqlDbType.Char, 1).Value = iscritic;
                cmd.Parameters.Add("@firstchoice", SqlDbType.VarChar, 50).Value = firstchoice;
                cmd.Parameters.Add("@secondchoice", SqlDbType.VarChar, 50).Value = secondchoice;


                cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@status"].Value);

            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally
            {
                con.Close();
            }
            return result;

        }
    }
}




