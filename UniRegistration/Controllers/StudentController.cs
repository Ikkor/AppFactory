using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Repositories;
using Services;

namespace UniRegistration.Controllers
{
    public class StudentController : Controller
    {

        private StudentService _service = new StudentService(new StudentRepository());

        public ActionResult Index()
        {
            return RedirectToAction("Register", "Student");
        }

        [HttpGet]
        public ActionResult Register()
        {


            return View();

        }


        [HttpPost]
        public ActionResult Register(Student student)
        {
            student.UserId = (int)Session["Id"];
           _service.Register(student);
            return View();
        }
        
    }
}