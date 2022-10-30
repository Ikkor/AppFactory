using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Common;
using System.Data.SqlClient;
using Repositories;
using System.Runtime.Remoting.Messaging;


namespace Repositories
{
    public interface IUserRepository:IRepository<User>
    {
       User Find (string email);

       
    }

    public class UserRepository : SqlHelper, IUserRepository
    {

      

        public User Find(string email) 
        {
            User userModel = new User();
            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(conn, "select top 1 UserId,Email,Password,Role from Users where Email = @Email"))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        userModel.UserId = (int)reader["UserId"];
                        userModel.Email = (string)reader["Email"];
                        userModel.Password = (string)reader["Password"];
                        userModel.Role = (Role)reader["Role"];
                    }
                }
                conn.Close();
            }
           
            return userModel;
        }
        public int Insert(User user)
        {
            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(conn, @"insert into [Users](Email,Password,Role) values(@Email,@Password,@Role)"))
                {
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@Role", user.Role);
                    conn.Close();
                    return cmd.ExecuteReader().RecordsAffected;
                }
                

            }
            throw new Exception("Could not register user, please try again.");

        }
        public User Find(int userId)
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


