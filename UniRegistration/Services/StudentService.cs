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
        private readonly IRepository<User> _userRepo = new UserRepository();
        private readonly IResultRepository<Result> _resultRepo = new ResultRepository();


        public StudentService(IRepository<Student> students)
        {
            _repo = students;
        }
    



        public Student Register(Student student)
        {
            
            var email = _userRepo.Find(student.UserId).Email;
            student.Email = email;
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