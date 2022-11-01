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
        List<Student> FetchStudentsResults();

    }


    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;
        private readonly IUserRepository _userRepo;
        private readonly IResultRepository<Result> _resultRepo;
        private const int _minimumMarks = 10;

        public StudentService(IStudentRepository students, IResultRepository<Result> result, IUserRepository user)
        {
            _repo = students;
            _resultRepo = result;
            _userRepo = user;
        }
        public Status GetEnrollmentStatus(int userId)
        {
            return _repo.GetEnrollmentStatus(userId);
        }
        public Student Register(Student student)
        {

            if (student.TotalMarks < _minimumMarks)
                throw new Exception("Needs minimum of 10 marks");

            int studentId = _repo.Insert(student);
            _resultRepo.Insert(student.Results, studentId);
            return student;
        }
        public List<Student> FetchStudentsResults()
        {
            List<Student> studentList = _repo.FetchAll();

            return studentList;
        }
    }
}