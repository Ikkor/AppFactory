﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;

namespace UniRegistration.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            //debug
            return RedirectToAction("Register", "Student");

            if (Session["Email"] == null)
            {
                return RedirectToAction("Login", "User");
            }


            switch (Session["Role"])
            {
                case Role.Admin: return RedirectToAction("Index", "Admin");
                case Role.Enrolled: return RedirectToAction("Index", "Student");
                case Role.User: return RedirectToAction("Register", "Student");
            }

            return View();
        }

    }
}