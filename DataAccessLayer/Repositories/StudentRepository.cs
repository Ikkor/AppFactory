using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using System.Data.SqlClient;
using Common;
using DataAccessLayer.Repositories.SqlQueries;

namespace Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        EnrollmentStatus GetEnrollmentStatus(int userId);
        bool IsUserEnrolled(int userId);
        Student Find(string email);

        int GetStudentId(int userId);
    }
    public class StudentRepository : ConnHelper, IStudentRepository
    {

        private readonly int GetAllUsers = -1;
       public Student Find(string email)
        {
            Student student = new Student();
            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(conn, StudentSql.findStudentByEmailSql))
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
                        student.EnrollmentStatus = (EnrollmentStatus)(int)(byte)reader["EnrollmentStatusId"];

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

        public EnrollmentStatus GetEnrollmentStatus(int userId)
        {

            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(conn, @"select top 1 EnrollmentStatusId from student where UserId = @UserId"))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        return (EnrollmentStatus)(int)(byte)reader["EnrollmentStatusId"];

                    }
                    conn.Close();
                }
            }
            throw new Exception("Student not found");
        }

        public bool Update(Student student)
        {
            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(conn, StudentSql.updateStudentSql))
                    {
                    cmd.Parameters.AddWithValue("@StudentId", student.StudentId);
                    cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", student.LastName);
                    cmd.Parameters.AddWithValue("@Address", student.Address);
                    cmd.Parameters.AddWithValue("@Phone", student.Phone);
                    cmd.Parameters.AddWithValue("@NationalIdentity", student.NationalIdentity);
                    cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                    cmd.Parameters.AddWithValue("@GuardianName", student.GuardianName);
                    int recordsAffected = cmd.ExecuteReader().RecordsAffected;
                    conn.Close();
                    return recordsAffected>0;
                }

            }

        }
        public bool DuplicateExist(Student student)
        {
            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(conn, StudentSql.duplicateStudentExistSql
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
                using (SqlCommand cmd = CreateCommand(conn, StudentSql.insertStudentSql
                   ))
                {
                    cmd.Parameters.AddWithValue("@UserId", student.UserId);
                    cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", student.LastName);
                    cmd.Parameters.AddWithValue("@Address", student.Address);
                    cmd.Parameters.AddWithValue("@Phone", student.Phone);
                    cmd.Parameters.AddWithValue("@NationalIdentity", student.NationalIdentity);
                    cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                    cmd.Parameters.AddWithValue("@GuardianName", student.GuardianName);
                    cmd.Parameters.AddWithValue("@EnrollmentStatusId", (int)EnrollmentStatus.Pending);

                    int insertedStudentId = Convert.ToInt32(cmd.ExecuteScalar());
                    return insertedStudentId;
                }

            }

        }

        public int GetStudentId(int userId)
        {
            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(conn, @"select StudentId from Student where UserId = @UserId;"))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    SqlDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        return (int)reader["StudentId"];

                    }

                    throw new Exception("Student not found");
                }
            }
        }

        public Student Find(int userId)
        {
            return GetListOfStudents(StudentSql.fetchStudentSql, userId)[0];
        }

        public List<Student> FetchAll()
        {
            return GetListOfStudents(StudentSql.fetchAllStudentsSql, GetAllUsers);
        }



        public List<Student> GetListOfStudents(string query, int UserId)
        {
            List<Student> studentList = new List<Student>();
            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(conn, query))
                {
                    if (UserId != GetAllUsers)
                    {
                     cmd.Parameters.AddWithValue("@UserId", UserId);
                    }

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        List<Result> resultList = new List<Result>();
                        Result result = new Result()
                        {
                            StudentId = (int)reader["StudentId"],
                            Marks = (int)reader["Marks"],
                            SubjectId = (int)(byte)reader["SubjectId"],
                            ResultId = (int)reader["ResultId"]

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
                                EnrollmentStatus = (EnrollmentStatus)(int)(byte)reader["EnrollmentStatusId"]
                            });
                        }
                    }
                }
                return studentList;
            }
        }
    }
}