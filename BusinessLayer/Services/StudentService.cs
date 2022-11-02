using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Repositories;
using ViewModels;
using Services;
using Common;

namespace Services
{

    interface IStudentService
    {
        EnrollmentStatus GetEnrollmentStatus(int studentId);
        bool Register(Student student);
        List<Student> FetchStudentsResults();

        Student GetStudent(int userId);

        int GetStudentId(int UserId);
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
        public EnrollmentStatus GetEnrollmentStatus(int userId)
        {
            return _repo.GetEnrollmentStatus(userId);
        }
        public bool Register(Student student)
        {
            if (student.TotalMarks < _minimumMarks)
                throw new Exception("Needs minimum of 10 marks");

            int studentId = _repo.Insert(student);
            _resultRepo.Insert(student.Results, studentId);
            return true;
        }
        public bool Update(Student student)
        {
            return _repo.Update(student);
        }

        public List<Student> FetchStudentsResults()
        {
            List<Student> studentList = _repo.FetchAll();
            studentList = studentList.OrderByDescending(student => student.TotalMarks).ToList();

            return studentList;
        }
        public Student GetStudent(int userId)
        {
            return _repo.Find(userId);
        }

        public int GetStudentId(int UserId)
        {
            return _repo.GetStudentId(UserId);
        }
    }
}