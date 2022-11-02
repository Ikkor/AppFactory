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
using ViewModels;

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
            string url = null;
           
            
          if(_service.Register(student))
                url = Url.Action("Index", "Student");
            
   
            return Json(new { url = url });
        }

        public ActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Update(Student student)
        {
            student.UserId = (int)Session["UserId"];
            student.Email = (string)Session["Email"];
            student.StudentId = _service.GetStudentId(student.UserId);
            string url = null;

            if (_service.Update(student))
                url = Url.Action("Index", "Student");
            return Json(new { url = url });
        }

        public JsonResult GetStudent()
        {

            var student = _service.GetStudent((int)Session["UserId"]);
            var studentJson = JsonConvert.SerializeObject(student);
            return Json(studentJson);
        }
        [HttpPost]
        public JsonResult GetEnrollmentStatus()
        {
            EnrollmentStatus _status = (EnrollmentStatus)_service.GetEnrollmentStatus((int)Session["UserId"]);
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