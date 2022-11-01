using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using System.Data.SqlClient;
using Common;
namespace Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Status GetEnrollmentStatus(int userId);
        bool IsUserEnrolled(int userId);
        Student Find(string email);
    }
    public class StudentRepository : ConnHelper, IStudentRepository
    {
        private string duplicateExistSql = @"SELECT 1 FROM Student s INNER JOIN Users u
                    ON s.UserId = u.UserId 
                    WHERE (s.NationalIdentity = @NationalIdentity 
                    OR s.Phone = @Phone
                    OR u.Email = @Email)";
        private string fetchAllSql = @"SELECT s.StudentId,s.UserId,s.GuardianName,s.DateOfBirth,s.Phone,u.Email,s.Address,s.NationalIdentity,s.FirstName,s.LastName,s.Status, r.SubjectId,r.Marks,u.Email, r.Id
                                                            AS ResultId FROM Student s 
                                                            INNER JOIN Result r ON s.StudentId = r.StudentId 
                                                            INNER JOIN Users u ON u.UserId = s.UserId";


        public string testsql = $"{SqlHelper.GetColumnNames(typeof(Student))}";


        public Student Find(string email)
        {
            Student student = new Student();
            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(conn, @"select s.StudentId,s.UserId,s.GuardianName,s.Phone,s.NationalIdentity,s.DateOfBirth,s.FirstName,s.LastName,s.Status FROM Student s, Users u WHERE s.UserId = u.Id AND u.Email = @Email"))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        student.StudentId = (int)reader["StudentId"];
                        student.UserId = (int)reader["UserId"];
                        student.GuardianName = (string)reader["GuardianName"];
                        student.Phone = (string)reader["Phone"];
                        student.NationalIdentity = (string)reader["NationalIdentity"];
                        student.DateOfBirth = (DateTime)reader["DateOfBirth"];
                        student.FirstName = (string)reader["FirstName"];
                        student.LastName = (string)reader["LastName"];
                        student.Status = (Status)reader["Status"];

                    }
                }
                conn.Close();
            }
            return student;
        }


        public bool IsUserEnrolled(int userId)
        {
            using (SqlConnection conn = CreateConnection())
            {

                using (SqlCommand cmd = CreateCommand(conn,
                    @"select 1 from Student where UserId=@UserId"))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    return reader.HasRows;

                }

            }
        }
        public Student Find(int studentId)
        {
            throw new NotImplementedException();

        }
        public Status GetEnrollmentStatus(int userId)
        {

            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(conn, @"select top 1 status from student where UserId = @UserId"))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        return (Status)reader["Status"];

                    }
                    conn.Close();
                }
            }
            throw new Exception("Student not found");
        }

        public int Update(Student student)
        {
            throw new NotImplementedException();

        }
        public bool DuplicateExist(Student student)
        {
            using (SqlConnection conn = CreateConnection())
            {

                using (SqlCommand cmd = CreateCommand(conn, duplicateExistSql
                    ))
                {
                    cmd.Parameters.AddWithValue("@Phone", student.Phone);
                    cmd.Parameters.AddWithValue("@Email", student.Email);
                    cmd.Parameters.AddWithValue("@NationalIdentity", student.NationalIdentity);
                    SqlDataReader reader = cmd.ExecuteReader();

                    return reader.HasRows;
                }
            }
        }
        public int Insert(Student student)
        {
            if (DuplicateExist(student)) throw new Exception("Student already enrolled, duplicate violation");

            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(conn,
                    @"INSERT INTO [Student](UserId,FirstName,LastName,Address,Phone,NationalIdentity,DateOfBirth,GuardianName, Status) VALUES (
                    @UserId,@FirstName,@LastName,@Address,@Phone,@NationalIdentity,@DateOfBirth,@GuardianName,@Status); SELECT SCOPE_IDENTITY();"))
                {
                    cmd.Parameters.AddWithValue("@UserId", student.UserId);
                    cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", student.LastName);
                    cmd.Parameters.AddWithValue("@Address", student.Address);
                    cmd.Parameters.AddWithValue("@Phone", student.Phone);
                    cmd.Parameters.AddWithValue("@NationalIdentity", student.NationalIdentity);
                    cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                    cmd.Parameters.AddWithValue("@GuardianName", student.GuardianName);
                    cmd.Parameters.AddWithValue("@Status", (int)Status.Pending);

                    int insertedStudentId = Convert.ToInt32(cmd.ExecuteScalar());
                    /*                    conn.Close();
                    */
                    return insertedStudentId;
                }

            }

        }
        public List<Student> FetchAll()
        {

            List<Student> studentList = new List<Student>();

            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(conn, fetchAllSql))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        List<Result> resultList = new List<Result>();
                        Result result = new Result()
                        {
                            StudentId = (int)reader["StudentId"],
                            Marks = (int)reader["Marks"],
                            Id = (int)reader["ResultId"]

                        };
                        resultList.Add(result);

                        var foundStudent = studentList.FirstOrDefault(student => student.StudentId == (int)reader["StudentId"]);

                        if (foundStudent != null)
                        {
                            foundStudent.Results.Add(result);
                        }
                        else
                        {
                            studentList.Add(new Student()
                            {
                                Results = resultList,
                                StudentId = (int)reader["StudentId"],
                                UserId = (int)reader["UserId"],
                                GuardianName = (string)reader["GuardianName"],
                                Phone = (string)reader["Phone"],
                                Email = (string)reader["Email"],
                                Address = (string)reader["Address"],
                                NationalIdentity = (string)reader["NationalIdentity"],
                                DateOfBirth = (DateTime)reader["DateOfBirth"],
                                FirstName = (string)reader["FirstName"],
                                LastName = (string)reader["LastName"],
                                Status = (Status)reader["Status"]
                            });
                        }
                    }
                }
                return studentList;
            }
        }
    }
}