using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using System.Data.SqlClient;
using System.Web.Helpers;
using Common;
using ViewModels;



namespace Repositories
{
    public class StudentRepository : SqlHelper, IRepository<Student>
    {
        public Student Find(string email)
        {
            Student student = new Student();
            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(conn, "select s.* FROM Student s, Users u WHERE s.UserId = u.Id AND u.Email = @Email"))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        student.Id = (int)reader["Id"];
                        student.UserId = (int)reader["UserId"];
                        student.GuardianName = (string)reader["GuardianName"];
                        student.Phone = (string)reader["Phone"];
                        student.NID = (string)reader["NID"];
                        student.DoB = (DateTime)reader["DoB"];
                        student.FirstName = (string)reader["FirstName"];
                        student.LastName = (string)reader["LastName"];
                        student.Status = (Status)reader["Status"];

                    }
                }
            }
            return student;
        }

        public Student Find(int id)
        {
            throw new NotImplementedException();

        }

        public int Update(Student student)
        {
            throw new NotImplementedException();

        }

        public int Insert(Student student)
        { 
            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(conn, 
                    $"INSERT INTO [Student](UserId,FirstName,LastName,Address,Phone,NID,DoB,GuardianName,Registered) VALUES (" +
                    $"@UserId,@FirstName,@LastName,@Address,@Phone,@NID,@DoB,@GuardianName,@Status"))
                {
                    cmd.Parameters.AddWithValue("@UserId", student.UserId);
                    cmd.Parameters.AddWithValue("@Email", student.Email);
                    cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", student.LastName);
                    cmd.Parameters.AddWithValue("@Address", student.Address);
                    cmd.Parameters.AddWithValue("@NID", student.NID);
                    cmd.Parameters.AddWithValue("@DoB", student.DoB);
                    cmd.Parameters.AddWithValue("@GuardianName", student.GuardianName);
                    cmd.Parameters.AddWithValue("@Status", 1);


                    return cmd.ExecuteReader().RecordsAffected;
                }

            }
            throw new Exception("Could not register student, please try again.");
        }

  


        public List<Student> FetchAll()
        {

            List<Student> studentList = new List<Student>();
            
            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(conn, "SELECT s.*, r.SubjectId,r.Marks,u.Email, r.Id AS ResultId FROM Student s INNER JOIN Result r ON s.Id = r.StudentId INNER JOIN Users u ON u.Id = s.UserId"))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        List<Result> resultList = new List<Result>();
                        Result result = new Result()
                        {
                            StudentId = (int)reader["Id"],
                            Marks = (int)reader["Marks"],
                            Id = (int)reader["ResultId"]

                        };
                        resultList.Add(result);

                        var foundStudent = studentList.FirstOrDefault(student => student.Id == (int)reader["Id"]);

                        if (foundStudent!=null)
                        {
                            foundStudent.Results.Add(result);
                        }
                        else
                        {
                            studentList.Add(new Student()
                            {

                                Results = resultList,
                                Id = (int)reader["Id"],
                                UserId = (int)reader["UserId"],
                                GuardianName = (string)reader["GuardianName"],
                                Phone = (string)reader["Phone"],
                                Email = (string)reader["Email"],
                                Address = (string)reader["Address"],
                                NID = (string)reader["NID"],
                                DoB = (DateTime)reader["DoB"],
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