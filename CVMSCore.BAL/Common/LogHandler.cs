using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CVMSCore.BAL.Common
{
    public class LogHandler
    {
        private SqlConnection _conn;
        LogHandler log = null;
        string errorMethodRoute = "OMMS.BAL.Common.LogHandler";
        DapperConnection dapper = new DapperConnection(ConnectionFile.Connection_ANTSDB);
        private string sPathName = Path.Combine("~/ErrorLogs");

        public void WriteErrorLog(Exception ex, string ControllerName = null, string ActionName = null)
        {
            try
            {
                string fileName = sPathName + "/" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                // Check if file already exists. If yes, append it error.     
                if (File.Exists(fileName))
                {
                    File.AppendAllText(fileName, "New Error:" + DateTime.Now.ToString() + " On " + ControllerName + "/" + ActionName +
                        Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine);
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(fileName))
                    {
                        sw.WriteLine("New Error:" + DateTime.Now.ToString() + " On " + ControllerName + "/" + ActionName);
                        sw.WriteLine(ex.Message);
                        sw.WriteLine("");
                        sw.WriteLine("");
                        sw.Flush();
                        sw.Close();
                    }
                }

                if (ControllerName != null && ActionName != null)
                    SaveErrorInDB("CODE", ex.Message, ex.StackTrace, ControllerName, ActionName, DateTime.Now);

            }
            catch (Exception exx)
            {
                if (ControllerName != null && ActionName != null)
                    SaveErrorInDB("CODE", exx.Message, exx.StackTrace, ControllerName, ActionName, DateTime.Now);
            }
        }

        public void SaveErrorInDB(string ErrorFrom, string ExceptionMessage, string ExceptionStackTrack, string ControllerName, string ActionName, DateTime ExceptionLogTime)
        {
            int result = 0;
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("ErrorFrom", ErrorFrom);
                param.Add("ExceptionMessage", ExceptionMessage);
                param.Add("ExceptionStackTrack", ExceptionStackTrack);
                param.Add("ControllerName", ControllerName);
                param.Add("ActionName", ActionName);
                param.Add("ExceptionLogTime", ExceptionLogTime);

                _conn = dapper.GetConnection();
                DapperConnection.OpenConnection(_conn);
                string strResult = _conn.Execute("sp_SaveExceptionLog", param, commandType: CommandType.StoredProcedure).ToString();
                DapperConnection.CloseConnection(_conn);
                result = Convert.ToInt32(strResult);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex);
            }
        }
    }
}
