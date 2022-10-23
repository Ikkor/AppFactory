using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Common;
using System.Data.SqlClient;
using System.Web.Mvc.Ajax;
using Repositories;
using System.Web.Helpers;
using System.Runtime.Remoting.Messaging;


namespace Repositories
{

    public class UserRepository : SqlHelper, IUserRepository
    {

        public User Find(string email)
        {
            User userModel = new User();
            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(conn, "select * from Users where Email = @Email"))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        userModel.Id = (int)reader["Id"];
                        userModel.Email = (string)reader["Email"];
                        userModel.Password = (string)reader["Password"];
                        userModel.Role = (Role)reader["Role"];

                    }
                }
            }
           
            return userModel;
        }




        public int Insert(User user)
        {
            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(conn, $"insert into [Users](Email,Password,Role) values(@Email,@Password,@Role)"))
                {
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@Role", user.Role);
                    return cmd.ExecuteReader().RecordsAffected;
                }

            }
            throw new Exception("Could not register user, please try again.");

        }



        public User Find(int id)
        {
            throw new NotImplementedException();
        }


        public List<User> FetchAll()
        {

            throw new NotImplementedException();

        }


        public int Update(User user)
        {
            throw new NotImplementedException();
        }




    }
}


