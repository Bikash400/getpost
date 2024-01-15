using CVMSCore.BAL.Models.Employee;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace CVMSCore.BAL.Repository
{
    public class SatarupaRepo
    {
        private SqlConnection _conn;
        private DapperConnection dapper = new DapperConnection(ConnectionFile.Connection_ANTSDB);

        public List<EmpClass> GetData()
        {
            List<EmpClass> obj = new List<EmpClass>();
            this._conn = this.dapper.GetConnection();
            DapperConnection.OpenConnection(this._conn);
            SqlConnection conn = this._conn;
            obj = _conn.Query<EmpClass>("EmpTable", commandType: CommandType.StoredProcedure).ToList();
            DapperConnection.CloseConnection(this._conn);

            return obj;
        }
    }
}
