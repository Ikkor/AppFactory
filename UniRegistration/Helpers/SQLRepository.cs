using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace UniRegistration.Helpers
{
    public class SQLRepository
    {
        public static DataTable Get(string query)
        {
            DBConnect DBConnect = new DBConnect();
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(query, DBConnect.connection))
            {
                cmd.CommandType = CommandType.Text;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }

            DBConnect.CloseConnection();

            return dt;
        }

        public static void InsertUpdate(string query, List<SqlParameter> parameters)
        {
            DBConnect dbConnect = new DBConnect();
            using (SqlCommand cmd = new SqlCommand(query, dbConnect.connection))
            {
                cmd.CommandType = CommandType.Text;
                if (parameters != null)
                {
                    parameters.ForEach(parameter => {
                        cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    });
                }
                cmd.ExecuteNonQuery();
            }
            dbConnect.CloseConnection();
        }

        public static DataTable GetDataWithConditions(string query, List<SqlParameter> parameters)
        {
            DBConnect dbConnect = new DBConnect();
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(query, dbConnect.connection))
            {
                cmd.CommandType = CommandType.Text;
                if (parameters != null)
                {
                    parameters.ForEach(parameter => {
                        cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    });
                }
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }

            dbConnect.CloseConnection();

            return dt;
        }
    }
}