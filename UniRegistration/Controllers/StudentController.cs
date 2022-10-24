using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using ViewModels;
using Repositories;
using Services;

namespace UniRegistration.Controllers
{
    public class StudentController : Controller
    {

        private StudentService _studentService = new StudentService(new StudentRepository());
        private SubjectService _subjectService = new SubjectService(new SubjectRepository());

        // GET: Student
        public ActionResult Index()
        {
            return RedirectToAction("Register", "Student");
        }

        [HttpGet]
        public ActionResult Register()
        {

            StudentRegisterVm student = new StudentRegisterVm();
            student.SubjectList = _subjectService.GetSubjects();

            //System.Diagnostics.Debug.WriteLine(_service.StudentTotalMarks()[0].TotalMarks);
            return View(student);

        }


        [HttpPost]
        public ActionResult Register(Student student)
        {

           _studentService.Register(student);
            return View();
        }
        
    }
}