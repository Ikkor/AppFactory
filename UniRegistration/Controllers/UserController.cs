using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repositories;
using Services;
using System.Web.Security;
using Models;
using System.Diagnostics;
using System.Security.Policy;

namespace UniRegistration.Controllers
{
    public class UserController : Controller
    {
        private UserService _service = new UserService(new UserRepository());


        public ActionResult Index()
        {
            return RedirectToAction("Login", "User");

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
            return RedirectToAction("Login","User");
        }


        [HttpPost]
        public JsonResult Login(User user)
        {
            User loggedUser = _service.Login(user);
            string url = null;
            if (loggedUser != null)
            {
                this.Session["Id"] = (int)loggedUser.Id;
                this.Session["Role"] = loggedUser.Role;
                this.Session["Email"] = loggedUser.Email;
                switch (loggedUser.Role)
                {
                    case Role.Admin: url = "/Admin"; break;
                    case Role.User: url = "/Home"; break;
                    case Role.Enrolled: url = Url.Action("Index", "Student"); break;

                }
            }

            return Json(new { url = url });


        }

        public ActionResult Login()
        {
            return View();
        }


    }
}
