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

            return Json(new { message=user.Email });
        }





            /*        [HttpPost]
                    [ValidateAntiForgeryToken]
                    public ActionResult Register(UserRegisterVm user)
                    {

                        try
                        {
                            _service.Register(user.Email, user.Password);
                        }
                        catch(Exception e)
                        {
                            ModelState.AddModelError("ErrorMsg", e.Message);
                        }
                        return View();
                    }*/



        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login","User");
        }

/*        [HttpPost]
        public ActionResult LoginOLD(User user)
        {
            try
            {
                _service.Login(user.Email, user.Password,user.RememberMe);
                return RedirectToAction("About", "Home");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErrorMsg", e.Message);
            }

            return View();
        }*/


        [HttpPost]
        public JsonResult Login(User user)
        {



            User loggedUser = _service.Login(user);
            string url = null;
            if(loggedUser != null)
            {
                this.Session["Id"] = (int)loggedUser.Id;
                this.Session["Role"] = loggedUser.Role;
                this.Session["Email"] = loggedUser.Email;
                switch (loggedUser.Role)
                {
                    case Role.Admin: url = "/Admin"; break;
                    case Role.User: url = "/Home"; break;

                }
            }
           
            return Json(new {url = url });
            
            
            
            
            /*            try
            {
               User loggedUser = _service.Login(user);

                this.Session["Role"] = loggedUser.Role;
                this.Session["Email"] = loggedUser.Email;
                
            }
            catch(Exception e)
            {
                return Json(new { error = e.Message });
            }

                return Json(new {url: Url.Action("Index", "Employee")) });*/

        }

        public ActionResult Login()
        {
            return View();
        }


    }
}
