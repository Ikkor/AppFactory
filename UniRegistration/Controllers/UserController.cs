using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repositories;
using Services;
using System.Web.Security;
using Models;
using ViewModels;
using System.Diagnostics;
using System.Security.Policy;

namespace UniRegistration.Controllers
{
    public class UserController : Controller
    {
        private UserService _service = new UserService(new UserRepository());


        public ActionResult Index()
        {
            if (Session["Enrolled"]!=null)
                return RedirectToAction("Index", "Student");
            return RedirectToAction("Register", "Student");

        }


        [HttpGet]
        public ActionResult Register()
        {

            return View();

        }

        [HttpPost]
        
        public JsonResult Register(User user)
        {

            try
            {
                _service.Register(user);
                
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }

            return Json(new { url = Url.Action("Login", "User") });
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login","User");
        }

        public ActionResult Login()
        {
            return View();
        }




        [HttpPost]
        public JsonResult Login(User user)
        {
            User loggedUser = _service.Login(user);

            string url = null;
            if (loggedUser != null)
            {
                SetUserSessions(loggedUser);
                url = RouteUserByRole(loggedUser.Role);
 
            }
            return Json(new { url = url });
        }

        private string RouteUserByRole(Role role)
        {
            switch (role)
            {
                case Role.Admin: return Url.Action("Index", "Admin");
                case Role.User: return Url.Action("Index", "User");                  
            }
            return null;
        }



        private bool SetUserSessions(User user)
        {
            this.Session["UserId"] = (int)user.UserId;
            this.Session["Role"] = user.Role;
            this.Session["Email"] = user.Email;
            if (_service.IsUserEnrolled((int)Session["UserId"]))
                this.Session["Enrolled"] = true;
            return true;
        }

    }
}
