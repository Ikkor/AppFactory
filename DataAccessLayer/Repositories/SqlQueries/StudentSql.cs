using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.SqlQueries
{

    public static class StudentSql
    {
        public static string duplicateStudentExistSql = @"SELECT 1 FROM Student s INNER JOIN Users u
                    ON s.UserId = u.UserId 
                    WHERE (s.NationalIdentity = @NationalIdentity 
                    OR s.Phone = @Phone
                    OR u.Email = @Email)";
        public static string fetchAllStudentsSql = @"SELECT s.StudentId,s.UserId,s.GuardianName,s.DateOfBirth,s.Phone,u.Email,s.Address,s.NationalIdentity,s.FirstName,s.LastName,s.EnrollmentStatusId, r.SubjectId,r.Marks,u.Email, r.ResultId
                                                            FROM Student s 
                                                            INNER JOIN Result r ON s.StudentId = r.StudentId 
                                                            INNER JOIN Users u ON u.UserId = s.UserId";
        public static string fetchStudentSql = @"
	SELECT s.StudentId,s.UserId,s.GuardianName,s.DateOfBirth,s.Phone,u.Email,s.Address,s.NationalIdentity,s.FirstName,s.LastName,s.EnrollmentStatusId, r.SubjectId,r.Marks,u.Email, r.ResultId
                                                            FROM Student s 
                                                            INNER JOIN Result r ON s.StudentId = r.StudentId 
                                                            INNER JOIN Users u ON u.UserId = s.UserId
															Where s.UserId = @UserId;";

        public static string updateStudentSql = @"update student
	                                        set Address = @Address,
	                                        LastName = @LastName,
	                                        FirstName = @FirstName,
	                                        Phone = @Phone,
	                                        NationalIdentity = @NationalIdentity,
	                                        DateOfBirth = @DateOfBirth,	
	                                        GuardianName = @GuardianName
	                                        where StudentId = @StudentId";

        public static string findStudentByEmailSql = @"select s.StudentId,s.UserId,s.GuardianName,s.Phone,s.NationalIdentity,s.DateOfBirth,s.FirstName,s.LastName,s.EnrollmentStatusId FROM Student s, Users u WHERE s.UserId = u.Id AND u.Email = @Email";



        public static string insertStudentSql = @"INSERT INTO [Student](UserId,FirstName,LastName,Address,Phone,NationalIdentity,DateOfBirth,GuardianName, EnrollmentStatusId) VALUES (
                    @UserId,@FirstName,@LastName,@Address,@Phone,@NationalIdentity,@DateOfBirth,@GuardianName,@EnrollmentStatusId); SELECT SCOPE_IDENTITY();";
    }
}
