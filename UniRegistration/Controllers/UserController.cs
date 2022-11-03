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
using System.ComponentModel.DataAnnotations;

namespace UniRegistration.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _service;
        public UserController(UserService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            if ((bool)Session["Enrolled"])
                return RedirectToAction("Index", "Student");
            return RedirectToAction("Register", "Student");
        }


        [HttpGet]
        public ActionResult Register()
        {

            return View();

        }

        [HttpPost]

        public JsonResult Register(UserRegisterViewModel user)
        {
            string url = null;

            if (_service.Register(user.Email, user.Password))
            {
                url = Url.Action("Login", "User");
            };

            return Json(new { url = url });
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "User");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(UserLoginViewModel user)
        {
            string url = null;
            User loggedUser;
            try
            {
                loggedUser = _service.Authenticate(user.Email, user.Password);
            }
            catch (Exception e)
            {
                return Json(new { url = url, error = e.Message });
            }

            if (loggedUser != null)
            {
                SetUserSessions(loggedUser);

                url = Url.Action("Index", "Home");
            }
            return Json(new { url = url });
        }

        public JsonResult CurrentEmailSession()
        {
            return Json(new { email = Session["Email"] });
        }


        private bool SetUserSessions(User user)
        {
            this.Session["UserId"] = (int)user.UserId;
            this.Session["Role"] = user.Role;
            this.Session["Email"] = user.Email;
            if (_service.IsUserEnrolled((int)Session["UserId"]))
                this.Session["Enrolled"] = true;
            else
                this.Session["Enrolled"] = false;
            return true;
        }

    }
}
