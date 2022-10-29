using Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Services
{
    interface ISubjectService
    {
        List<Subject> GetSubjects();
    }
    public class SubjectService:ISubjectService
    {
        private readonly IRepository<Subject> _repo;
        public SubjectService(IRepository<Subject> subject)
        {
            _repo = subject;
        }
        public List<Subject> GetSubjects()
        {
            return _repo.FetchAll();
        }
    }
}