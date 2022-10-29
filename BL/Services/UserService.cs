using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;
using Models;
using Repositories;

namespace Services
{
    public class UserService
    {
        private readonly UserRepository _repo;
        private readonly StudentRepository _studentRepo;
        public UserService(UserRepository users)
        {
            _repo = users;
            _studentRepo = new StudentRepository();
        }
        public User Register(User user)
        {
            var hashedPassword = Crypto.HashPassword(user.Password);
            user.Password = hashedPassword;
            user.Role = Role.User;
            switch (IsEmailUsed(user.Email))
            {
                case true: throw new Exception("User already exist, please log in");
                case false: _repo.Insert(user); break;
            }
            return user;
        }
        public bool IsEmailUsed(string email) => _repo.Find(email).Email != null ? true : false;

        public bool IsUserEnrolled(int userId) => _studentRepo.IsUserEnrolled(userId);
      
    
        public User Login(User user)
        {
            if (user.Email == null && user.Password == null) throw new Exception("Please provide your login information");
            User found = _repo.Find(user.Email);
            if (user.Email == null) throw new Exception("Invalid credentials");
            if (Crypto.VerifyHashedPassword(found.Password, user.Password))
            {
                if (found.Role != Role.Admin) SetCookie(user);
                return found;
            }
            return null;
        }
        private void SetCookie(User user)
        {
            int timeout = user.RememberMe ? 300 : 1;
            var ticket = new FormsAuthenticationTicket(user.UserId.ToString(), user.RememberMe, timeout);
            string encrypted = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
            cookie.Expires = DateTime.Now.AddMinutes(timeout);
            cookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}

