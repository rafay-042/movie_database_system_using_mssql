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
        public static string connectionString = "data source=DESKTOP-K3N2DJ3\\SHEIKH; Initial Catalog=IMDB10;Integrated Security=true";

        public static List<Member> getAllUsers()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("ViewMember", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader rdr = cmd.ExecuteReader();

                List<Member> list = new List<Member>();
                while (rdr.Read())
                {
                    Member mem = new Member();
                    mem.Id   = rdr["Id"].ToString();
                    mem.name = rdr["name"].ToString();
                    mem.password = rdr["password"].ToString();
                    mem.age = rdr["age"].ToString();
                    mem.gender=rdr["gender"].ToString();
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



        public static int Login(string Id, string password)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
                cmd = new SqlCommand("login", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.NVarChar,50).Value = Id;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar, 50).Value = password;


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


        public static int Signup(string Id,string name, string password, string age ,string gender , string iscritic, string firstchoice, string secondchoice)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            { 
                cmd = new SqlCommand("SIGNUP", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@userId", SqlDbType.NVarChar, 50).Value = Id;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = name;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar, 50).Value = password;
                cmd.Parameters.Add("@age", SqlDbType.NVarChar, 50).Value = age;
                cmd.Parameters.Add("@gender", SqlDbType.NVarChar, 50).Value = gender;
                cmd.Parameters.Add("@iscritic", SqlDbType.NVarChar, 50).Value = iscritic;
                cmd.Parameters.Add("@firstchoice", SqlDbType.NVarChar, 50).Value = firstchoice;
                cmd.Parameters.Add("@secondchoice", SqlDbType.NVarChar, 50).Value = secondchoice;
              
                
              

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