using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace UniRegistration.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            if ((Role)Session["Role"] != Role.Admin)
                return RedirectToAction("Index", "User");
            return View();

        }

        public JsonResult IsAdmin()
        {
            if ((Role)Session["Role"] == Role.Admin)
                return Json(new { IsAdmin = "True" });
            return Json(new { IsAdmin = "" });


        }
    }
}