using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Helpers;
using System.Web.ModelBinding;
using System.Web.Security;
using Antlr.Runtime.Tree;
using Models;
using Repositories;

namespace Services
{
    public class UserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository users)
        {
            _repo = users;
        }



        public User Register(User user)
        {

            var hashedPassword = Crypto.HashPassword(user.Password);

 
            user.Password = hashedPassword;
            user.Role = Role.User;

            switch (Exist(user.Email))
            {
                case true: throw new Exception("User already exist, please log in");
                case false: _repo.Insert(user); break;
            }
            return user;

        }

        public bool Exist(string email) => _repo.Find(email).Email != null ? true : false;

     


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
            var ticket = new FormsAuthenticationTicket(user.Id.ToString(), user.RememberMe, timeout);
            string encrypted = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
            cookie.Expires = DateTime.Now.AddMinutes(timeout);
            cookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Add(cookie);
           

        }


    }
}

