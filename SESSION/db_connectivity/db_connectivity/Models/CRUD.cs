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
        public static string connectionString = "data source=localhost; Initial Catalog=connectivity; integrated security=true";



        public static User getUser(string userId)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("ViewUser", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@userId", SqlDbType.NVarChar, 50).Value = userId;

                SqlDataReader rdr = cmd.ExecuteReader();

                     
                 if (rdr.Read())
                 {
                
                    User user = new User();
                    user.userId = rdr["userId"].ToString();
                    user.password = rdr["password"].ToString();
                    user.dateOfBirth = rdr["dateOfBirth"].ToString();
                    rdr.Close();
                    con.Close();

                    return user;

                 }


                 rdr.Close();
                 con.Close();
                 return null;

            }

            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;

            }


        }


        public static List<User> getAllUsers()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("ViewUsers", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader rdr = cmd.ExecuteReader();

                List<User> list = new List<User>();
                while (rdr.Read())
                {
                    User user = new User();

                    user.userId = rdr["userId"].ToString();
                    user.password = rdr["password"].ToString();
                    user.dateOfBirth = rdr["dateOfBirth"].ToString();
                    list.Add(user);
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



        public static int Login(string userId, string password)
        {

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd;
            int result = 0;

            try
            {
               
                cmd = new SqlCommand("UserLoginProc", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@userId", SqlDbType.NVarChar, 50).Value = userId;
                cmd.Parameters.Add("@password", SqlDbType.NVarChar, 50).Value = password;


                cmd.Parameters.Add("@output", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@output"].Value);



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