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

        private readonly IStudentRepository _repo;
        private readonly IUserRepository _userRepo;
        private readonly IResultRepository<Result> _resultRepo;



        public StudentService(IStudentRepository students)
        {
            _repo = students;
            _resultRepo = new ResultRepository();
            _userRepo = new UserRepository();
        }
    

        public Status GetStatus(int id)
        {
            return _repo.GetStatus(id); 
        }

        public Student Register(Student student)
        {

            student.TotalMarks = student.Results.Sum(result => result.Marks);

            if (student.TotalMarks < 10)
                throw new Exception("Needs minimum of 10 marks");
            
            int studentId = _repo.Insert(student);
            
            _resultRepo.Insert(student.Results, studentId);
            _userRepo.EnrollUser(student.UserId);

            

            return student;
        }

        
   

        public List<Student> AllStudentMarks()
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