using System.Configuration;
using System.Data.SqlClient;

namespace Common
{
    public class SqlHelper
    {

        private string _connString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        protected SqlConnection CreateConnection()

        {
            SqlConnection result = new SqlConnection(_connString);
            result.Open();
            return result;
            
        }

        protected SqlCommand CreateCommand(SqlConnection conn, string sql)
        {
            SqlCommand command = conn.CreateCommand();
            
            command.CommandText = sql;
            return command;
        }
    }
}
