using System.Data.SqlClient;

namespace Common
{
    public class SqlHelper
    {

        private string _connString = "Data Source=l-r913xdng;Initial Catalog=university;User ID=sa;Password=Regards1200!";
        protected SqlConnection CreateConnection()
        {
            SqlConnection result = new SqlConnection(_connString);
            result.Open();
            return result; //add close
        }

        protected SqlCommand CreateCommand(SqlConnection conn, string sql)
        {
            SqlCommand command = conn.CreateCommand();
            command.CommandText = sql;
            return command;
        }
    }
}
