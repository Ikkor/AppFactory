using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repositories;
using ViewModels;
using Services;
using System.Web.Security;
using Models;

namespace UniRegistration.Controllers
{
    public class UserController : Controller
    {
        private UserService _service = new UserService(new UserRepository());


        public ActionResult Home()
        {
            return View();
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



        [HttpGet]
        public ActionResult LoginOLD()
        {
            return View();
        }
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
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
            try
            {
                _service.Login(user);
            }
            catch(Exception e)
            {
                return Json(new { error = e.Message });
            }

                return Json(new {em = user.Email, pas=user.Password });
        }

        public ActionResult Login()
        {
            return View();
        }


    }
}
