using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;
using Models;
using ViewModels;
using Repositories;

namespace Services
{
    public interface IUserService
    {
        bool Register(string email, string passworrd);
        bool IsEmailUsed(string email);
        bool IsUserEnrolled(int userId);
        User Authenticate(string email, string password);
    }
    public class UserService:IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IStudentRepository _studentRepo;
        public UserService(IUserRepository user, IStudentRepository student)
        {
            _repo = user;
            _studentRepo = student;
        }
        public bool Register(string email, string password)
        {
            User user = new User();

            user.Password = Crypto.HashPassword(password);
            user.Role = Role.User;
            user.Email = email;
            user.IsActive = true;
            switch (IsEmailUsed(user.Email))
            {
                case true: return false;
                case false: _repo.Insert(user); break;
            }
            return true;
        }


        public bool IsEmailUsed(string email) => _repo.Find(email).Email != null ? true : false;

        public bool IsUserEnrolled(int userId) => _studentRepo.IsUserEnrolled(userId);
      
    
        public User Authenticate(string email, string password)
        {

            if (email == null && password == null) throw new Exception("Please provide your login information");
            User found = _repo.Find(email);
            if (email == null) throw new Exception("Invalid credentials");
            if (Crypto.VerifyHashedPassword(found.Password, password))
            {
                return found;
            }
            return null;
        }

    }
}

