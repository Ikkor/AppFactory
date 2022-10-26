using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Common;
using System.Data;

namespace Repositories
{
    public interface IResultRepository<T>
    {
        int Insert(List<Result> resultList, int studentId);   
    }
    public class ResultRepository:SqlHelper,IResultRepository<Result>
    {

        
        public int Insert(List<Result> resultList, int studentId)
        {
            int rowsAffected = 0;
            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(conn, $"insert into [Result](SubjectId,StudentId,Marks) values(@SubjectId,@StudentId,@Marks)"))
                {
                    cmd.Parameters.AddWithValue("@SubjectId", SqlDbType.Int);
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.AddWithValue("@Marks", SqlDbType.Int);

                    foreach(Result result in resultList)
                    {
                        cmd.Parameters["@SubjectId"].Value = result.SubjectId;
                        cmd.Parameters["@Marks"].Value = result.Marks;
                       rowsAffected =  cmd.ExecuteNonQuery();
                    }



                }

            }
            return rowsAffected;

        }


    }
}