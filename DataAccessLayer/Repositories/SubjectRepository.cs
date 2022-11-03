using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using System.Web;
using Common;
using System.Data.SqlClient;

namespace Repositories
{
    public class SubjectRepository : ConnHelper, IRepository<Subject>
    {
        private string FetchAllSql = $"select {SqlHelper.GetColumnNames(typeof(Subject))} from Subject";
        public Subject Find(int id)
        {
            throw new NotImplementedException();
        }
        public Subject Find(string name)
        {
            throw new NotImplementedException();
        }
        public int Insert(Subject subject)
        {
            throw new NotImplementedException();
        }
        public bool Update(Subject subject)
        {
            throw new NotImplementedException();
        }
        public List<Subject> FetchAll()
        {
            List<Subject> subjectList = new List<Subject>();

            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(conn, FetchAllSql))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Subject subject = new Subject();
                        subject.SubjectId = (int)(byte)reader["SubjectId"];
                        subject.SubjectName = (string)reader["SubjectName"];
                        subjectList.Add(subject);
                    }
                }
                conn.Close();
            }
            return subjectList;
        }
    }
}
