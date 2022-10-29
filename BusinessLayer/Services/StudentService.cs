using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Repositories;
using Services;

namespace Services
{

    interface IStudentService
    {
        Status GetEnrollmentStatus(int studentId);
        Student Register(Student student);
        List<Student> AllStudentMarks();

    }


    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _repo;
        private readonly IUserRepository _userRepo;
        private readonly IResultRepository<Result> _resultRepo;
        private const int _minimumMarks = 10;

        public StudentService(IStudentRepository students)
        {
            _repo = students;
            _resultRepo = new ResultRepository();
            _userRepo = new UserRepository(); 
        }
        public Status GetEnrollmentStatus(int studentId)
        {
            return _repo.GetEnrollmentStatus(studentId); 
        }
        public Student Register(Student student)
        {
            student.TotalMarks = student.Results.Sum(result => result.Marks);

            if (student.TotalMarks < _minimumMarks)
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