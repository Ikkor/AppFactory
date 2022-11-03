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
    public class UserRepository : ConnHelper, IUserRepository
    {
        public User Find(string email) 
        {
            User userModel = new User();
            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(conn, "select top 1 UserId,Email,Password,RoleId from Users where Email = @Email"))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        userModel.UserId = (int)reader["UserId"];
                        userModel.Email = (string)reader["Email"];
                        userModel.Password = (string)reader["Password"];
                        userModel.Role= (Role)(int)(byte)reader["RoleId"];
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
                using (SqlCommand cmd = CreateCommand(conn, @"insert into [Users](Email,Password,RoleId,IsActive) values(@Email,@Password,@RoleId,@IsActive)"))
                {
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@RoleId", user.Role);
                    cmd.Parameters.AddWithValue("@IsActive", user.IsActive);
                    int rowsAffected =  cmd.ExecuteReader().RecordsAffected;
                    conn.Close();
                    return rowsAffected;
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

        public bool Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}


