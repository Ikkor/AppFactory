using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Common;
using System.Data;
using System.Configuration;

namespace Repositories
{
    public interface IResultRepository<T>
    {
        bool Insert(List<Result> resultList, int studentId);

    }
    public class ResultRepository : ConnHelper, IResultRepository<Result>
    {


        public bool Insert(List<Result> resultList, int studentId)
        {

            DataTable resultTable = new DataTable();
            resultTable.Columns.Add(new DataColumn("SubjectId", typeof(Int32)));
            resultTable.Columns.Add(new DataColumn("StudentId", typeof(Int32)));
            resultTable.Columns.Add(new DataColumn("Marks", typeof(Int32)));
            foreach (Result result in resultList)
            {
                DataRow dr = resultTable.NewRow();
                dr["SubjectId"] = result.SubjectId;
                dr["StudentId"] = studentId;
                dr["Marks"] = result.Marks;
                resultTable.Rows.Add(dr);
            }

            if (resultTable.Rows.Count > 0)
            {
                using (SqlConnection conn = CreateConnection())
                {
                    SqlTransaction transaction = conn.BeginTransaction();
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(conn,SqlBulkCopyOptions.FireTriggers,transaction))
                    {
                        sqlBulkCopy.DestinationTableName = "dbo.Result";

                        sqlBulkCopy.ColumnMappings.Add("SubjectId", "SubjectId");
                        sqlBulkCopy.ColumnMappings.Add("StudentId", "StudentId");
                        sqlBulkCopy.ColumnMappings.Add("Marks", "Marks");
                        sqlBulkCopy.WriteToServer(resultTable);
                        transaction.Commit();
                        conn.Close();
                    }
                }
            }
            return true;

        }

    }
}