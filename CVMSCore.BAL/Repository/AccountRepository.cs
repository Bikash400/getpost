using CVMSCore.BAL.Common;
using CVMSCore.BAL.Extensions;
using CVMSCore.BAL.Models.Master;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CVMSCore.BAL.Repository
{
    public class AccountRepository
    {
        private SqlConnection _conn;
        private LogHandler log;
        private string errorMethodRoute = "OMMS.BAL.Repository.AccountRepository";
        private DapperConnection dapper = new DapperConnection(ConnectionFile.Connection_ANTSDB);

        public int ResetUserPassword(string email, string password, int userId, string strUserToken)
        {
            int num = 0;
            try
            {
                if (!string.IsNullOrEmpty(strUserToken))
                {
                    DynamicParameters dynamicParameters1 = new DynamicParameters();
                    dynamicParameters1.Add("UserName", (object)email, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                    dynamicParameters1.Add("Password", (object)password, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                    dynamicParameters1.Add("UserToken", (object)strUserToken.Replace(' ', '+'), new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                    dynamicParameters1.Add("UpdatedBy", (object)userId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                    this._conn = this.dapper.GetConnection();
                    DapperConnection.OpenConnection(this._conn);
                    SqlConnection conn = this._conn;
                    DynamicParameters dynamicParameters2 = dynamicParameters1;
                    CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                    int? nullable2 = new int?();
                    CommandType? nullable3 = nullable1;
                    string str = SqlMapper.Execute((IDbConnection)conn, "OMMS_ResetUserPassword", (object)dynamicParameters2, (IDbTransaction)null, nullable2, nullable3).ToString();
                    DapperConnection.CloseConnection(this._conn);
                    num = Convert.ToInt32(str);
                }
            }
            catch (Exception ex)
            {
                this.log = new LogHandler();
                this.log.WriteErrorLog(ex, this.errorMethodRoute, nameof(ResetUserPassword));
            }
            return num;
        }

        public UserDetail checkUserExist(
          string userName,
          string userPassword,
          out string Statustxt)
        {
            Statustxt = "";
            UserDetail userDetail = (UserDetail)null;
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add("UserName", (object)userName, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("Password", (object)userPassword, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                this._conn = this.dapper.GetConnection();
                DapperConnection.OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                SqlMapper.GridReader gridReader = SqlMapper.QueryMultiple((IDbConnection)conn, "OMMS_CheckUserExistBasedOnUserNamePassword", (object)dynamicParameters2, (IDbTransaction)null, nullable2, nullable3);
                string str = gridReader.Read<string>(true).FirstOrDefault<string>();
                Statustxt = str;
                if (str == "Success")
                    userDetail = gridReader.Read<UserDetail>(true).FirstOrDefault<UserDetail>();
                DapperConnection.CloseConnection(this._conn);
            }
            catch (Exception ex)
            {
                this.log = new LogHandler();
                this.log.WriteErrorLog(ex, this.errorMethodRoute, nameof(checkUserExist));
            }
            return userDetail;
        }

        public string ChangePasswordCode(string email, string userToken)
        {
            string empty = string.Empty;
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add("Email", (object)email, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("UserToken", (object)userToken, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                this._conn = this.dapper.GetConnection();
                DapperConnection.OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                empty = SqlMapper.QueryFirstOrDefault<string>((IDbConnection)conn, "OMMS_ChangePassword", (object)dynamicParameters2, (IDbTransaction)null, nullable2, nullable3).ToString();
                DapperConnection.CloseConnection(this._conn);
            }
            catch (Exception ex)
            {
                this.log = new LogHandler();
                this.log.WriteErrorLog(ex, this.errorMethodRoute, "checkUserExist");
            }
            return empty;
        }

        public List<Modules> GetAllModule(int Id)
        {
            List<Modules> lstmodule = null;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Id", Id);
                _conn = dapper.GetConnection();
                DapperConnection.OpenConnection(_conn);
                lstmodule = _conn.Query<Modules>("OMMS_GetAllModule", param, commandType: CommandType.StoredProcedure).ToList();
                DapperConnection.CloseConnection(_conn);
            }
            catch (Exception ex)
            {

                throw;
            }
            return lstmodule;
        }
        public UserDetail CheckUserValidWithToken(string email, string strUserToken)
        {
            UserDetail userDetail = (UserDetail)null;
            try
            {
                if (!string.IsNullOrEmpty(strUserToken))
                {
                    DynamicParameters dynamicParameters1 = new DynamicParameters();
                    dynamicParameters1.Add("UserName", (object)email, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                    dynamicParameters1.Add("UserToken", (object)strUserToken.Replace(' ', '+'), new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                    _conn = dapper.GetConnection();
                    DapperConnection.OpenConnection(_conn);
                    userDetail = _conn.Query<UserDetail>(StoredProcedures.OMMS_CheckUserUsingEmailAndToken.ToDescription(), dynamicParameters1, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    DapperConnection.CloseConnection(_conn);
                }
            }
            catch (Exception ex)
            {
                this.log = new LogHandler();
                this.log.WriteErrorLog(ex, this.errorMethodRoute, "CheckUserValidWithToken");
            }
            return userDetail;
        }
    }
}
