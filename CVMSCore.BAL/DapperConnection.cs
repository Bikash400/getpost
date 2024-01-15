using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMSCore.BAL
{
    public class DapperConnection
    {
        private static string _sqlConnection;

        public DapperConnection(string connection)
        {
            _sqlConnection = connection; //ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        }
        public SqlConnection GetConnection()
        {
            SqlConnection _conn = new SqlConnection();
            string constr = _sqlConnection; //ConfigurationManager.ConnectionStrings["connectionString"].ToString();
            return _conn = new SqlConnection(constr);

        }

        public static void OpenConnection(SqlConnection _conn)
        {
            if (_conn != null)
            {
                if (_conn.State == ConnectionState.Closed)
                    _conn.Open();
            }

        }

        public static void CloseConnection(SqlConnection _conn)
        {
            if (_conn != null)
            {
                if (_conn.State == ConnectionState.Open)
                    _conn.Close();
            }

        }
    }
}
