using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using System.Web;
using Common;
using System.Data.SqlClient;

namespace Repositories
{
    public class SubjectRepository : SqlHelper, IRepository<Subject>
    {
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
        public int Update(Subject subject)
        {
            throw new NotImplementedException();
        }
        public List<Subject> FetchAll()
        {
            List<Subject> subjectList = new List<Subject>();

            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = CreateCommand(conn, "select * from Subject"))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Subject subject = new Subject();
                        subject.Id = (int)reader["Id"];
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
