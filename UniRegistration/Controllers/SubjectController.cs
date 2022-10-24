using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using System.Security.Policy;
using System.Collections;
using Newtonsoft.Json;

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

        public JsonResult GetSubjects()
        {
            var jsonString = JsonConvert.SerializeObject(_service.GetSubjects());
            return Json(jsonString);

        }
    }
}