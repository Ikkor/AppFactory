using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;


namespace Controllers
{
    public class SubjectController : Controller
    {
        private SubjectService _service = new SubjectService(new SubjectRepository());

        // GET: Subject
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult GetSubjects()
        {
            return PartialView("GetSubjects",_service.GetSubjects());

        }
    }
}