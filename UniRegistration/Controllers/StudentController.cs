using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Newtonsoft.Json;
using Repositories;
using Services;

namespace UniRegistration.Controllers
{
    public class StudentController : Controller
    {
        private StudentService _service = new StudentService(new StudentRepository());

        public ActionResult Index()
        {
            //to do: check if user enrolled else redirect
            
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Register(Student student)
        {
            student.UserId = (int)Session["Id"];
            student.Email = (string)Session["Email"];
            try
            {
                _service.Register(student);
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }
            return Json(new { url = Url.Action("Index", "Student") });
        }
        [HttpPost]
        public JsonResult GetStatus()
        {
            Status _status = (Status)_service.GetEnrollmentStatus((int)Session["Id"]);
            string _statusStr = JsonConvert.SerializeObject(_status);
            return Json(new { status = _status });
        }
    }
}