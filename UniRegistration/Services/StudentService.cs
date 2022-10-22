using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Repositories;
using Services;

namespace Services
{
    public class StudentService
    {

        private readonly IRepository<Student> _repo;
        public StudentService(IRepository<Student> students)
        {
            _repo = students;
        }




        public Student Register(Student student)
        {

            int currentUserId = int.Parse(HttpContext.Current.User.Identity.Name);
            student.UserId = currentUserId;
            _repo.Insert(student);
            return student;
        }




        public List<Student> StudentTotalMarks()
        {
           List<Student>studentList = _repo.FetchAll();
            foreach(Student student in studentList)
            {
                student.TotalMarks = student.Results.Sum(result => result.Marks);
            }
            return studentList;
        }



    }
}