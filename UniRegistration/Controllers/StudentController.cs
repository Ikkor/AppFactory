using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
        private readonly StudentService _service;

        public StudentController(StudentService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "User");

            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "User");
            

            return View();
        }
        [HttpPost]
        public JsonResult Register(Student student)
        {
            student.UserId = (int)Session["UserId"];
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
        public JsonResult GetEnrollmentStatus()
        {
            Status _status = (Status)_service.GetEnrollmentStatus((int)Session["UserId"]);
            string _statusStr = JsonConvert.SerializeObject(_status.ToString()); 

            return Json(new { status = _statusStr });
        }

        [HttpPost]
        public JsonResult FetchStudentsResults()
        {
            List<Student> studentsList = _service.FetchStudentsResults();
            string studentsListStr = JsonConvert.SerializeObject(studentsList);
            return Json(studentsListStr);

        }
    }
}