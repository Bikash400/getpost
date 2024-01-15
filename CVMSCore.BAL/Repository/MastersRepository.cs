using CVMSCore.BAL.Common;
using CVMSCore.BAL.Models.Master;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVMSCore.BAL.Extensions;
using System.Drawing;

namespace CVMSCore.BAL.Repository
{
    public class MastersRepository
    {
        private SqlConnection _conn;
        private LogHandler log;
        private string errorMethodRoute = "OMMS.BAL.Repository.AccountRepository";
        private DapperConnection dapper = new DapperConnection(ConnectionFile.Connection_ANTSDB);
        public List<CountryMaster> GetAllCountry(int? regionId,string usertype, string usersubtype, int UserId,int CompanyId = 0)
        {
            List<CountryMaster> allCountry = new List<CountryMaster> ();
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add("RegionId", (object)regionId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("CountryID", (object)0, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                this._conn = this.dapper.GetConnection();
                DapperConnection.OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                allCountry = SqlMapper.Query<CountryMaster>((IDbConnection)conn, "OMMS_GetAllCountry", (object)dynamicParameters2, (IDbTransaction)null, true, nullable2, nullable3).ToList<CountryMaster>();
                DapperConnection.CloseConnection(this._conn);
            }
            catch (Exception ex)
            {
                this.log = new LogHandler();
                this.log.WriteErrorLog(ex, this.errorMethodRoute, nameof(GetAllCountry));
            }
            return allCountry;
        }

        public List<RegionMaster> GetAllRegion()
        {
            List<RegionMaster> allRegion = (List<RegionMaster>)null;
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add("Id", (object)0, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                this._conn = this.dapper.GetConnection();
                DapperConnection.OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                allRegion = SqlMapper.Query<RegionMaster>((IDbConnection)conn, "OMMS_GetRegionDetails", (object)dynamicParameters2, (IDbTransaction)null, true, nullable2, nullable3).ToList<RegionMaster>();
                DapperConnection.CloseConnection(this._conn);
            }
            catch (Exception ex)
            {
                this.log = new LogHandler();
                this.log.WriteErrorLog(ex, this.errorMethodRoute, nameof(GetAllRegion));
            }
            return allRegion;
        }

        public List<CVMSCore.BAL.Models.Master.Currency> GetAllCurrency()
        {
            List<CVMSCore.BAL.Models.Master.Currency> allCurrency = new List<Currency>();
            try
            {
                this._conn = this.dapper.GetConnection();
                DapperConnection.OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                allCurrency = SqlMapper.Query<CVMSCore.BAL.Models.Master.Currency>((IDbConnection)conn, "OMMS_GetAllCurrency", (object)null, (IDbTransaction)null, true, nullable2, nullable3).ToList<CVMSCore.BAL.Models.Master.Currency>();
                DapperConnection.CloseConnection(this._conn);
            }
            catch (Exception ex)
            {
                this.log = new LogHandler();
                this.log.WriteErrorLog(ex, this.errorMethodRoute, nameof(GetAllCurrency));
            }
            return allCurrency;
        }

        public List<TimeZones> GetAllTimeZone()
        {
            List<TimeZones> allTimeZone = (List<TimeZones>)null;
            try
            {
                this._conn = this.dapper.GetConnection();
                DapperConnection.OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                allTimeZone = SqlMapper.Query<TimeZones>((IDbConnection)conn, "OMMS_GetTimeZoneDetail", (object)null, (IDbTransaction)null, true, nullable2, nullable3).ToList<TimeZones>();
                DapperConnection.CloseConnection(this._conn);
            }
            catch (Exception ex)
            {
                this.log = new LogHandler();
                this.log.WriteErrorLog(ex, this.errorMethodRoute, nameof(GetAllTimeZone));
            }
            return allTimeZone;
        }

        public int SaveCountry(CountryMaster objCountry, int userId)
        {
            int num = 0;
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add("CountryId", (object)0, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("CountryCode", (object)objCountry.CountryCode, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("Country", (object)objCountry.Country, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("RegionID", (object)objCountry.RegionID, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("Remarks", (object)objCountry.Remarks, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("Currency", (object)objCountry.CurrencyId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("TimeZoneId", (object)objCountry.TimeZoneId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("UserId", (object)userId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                this._conn = this.dapper.GetConnection();
                DapperConnection.OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                string str = SqlMapper.ExecuteScalar((IDbConnection)conn, "OMMS_SaveUpdateCountreyDetail", (object)dynamicParameters2, (IDbTransaction)null, nullable2, nullable3).ToString();
                DapperConnection.CloseConnection(this._conn);
                num = Convert.ToInt32(str);
            }
            catch (Exception ex)
            {
                this.log = new LogHandler();
                this.log.WriteErrorLog(ex, this.errorMethodRoute, nameof(SaveCountry));
            }
            return num;
        }

        public int UpdateCountry(CountryMaster objCountry, int userId)
        {
            int num = 0;
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add("CountryId", (object)objCountry.CountryID, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("CountryCode", (object)objCountry.CountryCode, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("Country", (object)objCountry.Country, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("RegionID", (object)objCountry.RegionID, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("Remarks", (object)objCountry.Remarks, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("Currency", (object)objCountry.CurrencyId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("TimeZoneId", (object)objCountry.TimeZoneId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("UserId", (object)userId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                this._conn = this.dapper.GetConnection();
                DapperConnection.OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                string str = SqlMapper.ExecuteScalar((IDbConnection)conn, "OMMS_SaveUpdateCountreyDetail", (object)dynamicParameters2, (IDbTransaction)null, nullable2, nullable3).ToString();
                DapperConnection.CloseConnection(this._conn);
                num = Convert.ToInt32(str);
            }
            catch (Exception ex)
            {
                this.log = new LogHandler();
                this.log.WriteErrorLog(ex, this.errorMethodRoute, nameof(UpdateCountry));
            }
            return num;
        }


        public List<CountryMaster> GetCountryDetail(int CountryId)
        {
            List<CountryMaster> countryDetail = new List<CountryMaster>();
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add("CountryId", (object)CountryId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("RegionId", (object)"", new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                this._conn = this.dapper.GetConnection();
                DapperConnection.OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                countryDetail = SqlMapper.Query<CountryMaster>((IDbConnection)conn, "OMMS_GetCountryBaseOnId", (object)dynamicParameters2, (IDbTransaction)null, true, nullable2, nullable3).ToList<CountryMaster>();
                DapperConnection.CloseConnection(this._conn);
            }
            catch (Exception ex)
            {
                this.log = new LogHandler();
                this.log.WriteErrorLog(ex, this.errorMethodRoute, nameof(GetCountryDetail));
            }
            return countryDetail;
        }

        public int DeleteCountry(int countryId, int userId)
        {
            int num = 0;
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add("CountryId", (object)countryId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("UserId", (object)userId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                this._conn = this.dapper.GetConnection();
                DapperConnection.OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                string str = SqlMapper.ExecuteScalar((IDbConnection)conn, "OMMS_DeleteCountryDetail", (object)dynamicParameters2, (IDbTransaction)null, nullable2, nullable3).ToString();
                DapperConnection.CloseConnection(this._conn);
                num = Convert.ToInt32(str);
            }
            catch (Exception ex)
            {
                this.log = new LogHandler();
                this.log.WriteErrorLog(ex, this.errorMethodRoute, nameof(DeleteCountry));
            }
            return num;
        }

        public int SaveCurrency(Currency objModal, int userId)
        {
            int num = 0;
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add("CurrencyId", (object)0, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("CurrencyCode", (object)objModal.CurrencyCode, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("CurrencyName", (object)objModal.CurrencyName, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("Remarks", (object)objModal.Remarks, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("UserId", (object)userId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                this._conn = this.dapper.GetConnection();
                DapperConnection.OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                string str = SqlMapper.ExecuteScalar((IDbConnection)conn, "OMMS_SaveUpdateCurrencyDetail", (object)dynamicParameters2, (IDbTransaction)null, nullable2, nullable3).ToString();
                DapperConnection.CloseConnection(this._conn);
                num = Convert.ToInt32(str);
            }
            catch (Exception ex)
            {
                this.log = new LogHandler();
                this.log.WriteErrorLog(ex, this.errorMethodRoute, nameof(SaveCurrency));
            }
            return num;
        }

        public int UpdateCurrency(Currency objModal, int userId)
        {
            int num = 0;
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add("CurrencyId", (object)objModal.Id, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("CurrencyCode", (object)objModal.CurrencyCode, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("CurrencyName", (object)objModal.CurrencyName, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("Remarks", (object)objModal.Remarks, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("UserId", (object)userId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                this._conn = this.dapper.GetConnection();
                DapperConnection.OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                string str = SqlMapper.ExecuteScalar((IDbConnection)conn, "OMMS_SaveUpdateCurrencyDetail", (object)dynamicParameters2, (IDbTransaction)null, nullable2, nullable3).ToString();
                DapperConnection.CloseConnection(this._conn);
                num = Convert.ToInt32(str);
            }
            catch (Exception ex)
            {
                this.log = new LogHandler();
                this.log.WriteErrorLog(ex, this.errorMethodRoute, nameof(UpdateCurrency));
            }
            return num;
        }

        public List<Currency> GetCurrencyDetail(int Id)
        {
            List<Currency> currencyDetail = new List<Currency>();
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add(nameof(Id), (object)Id, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                this._conn = this.dapper.GetConnection();
                DapperConnection.OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                currencyDetail = SqlMapper.Query<Currency>((IDbConnection)conn, "OMMS_GetCurrencyDetail", (object)dynamicParameters2, (IDbTransaction)null, true, nullable2, nullable3).ToList<Currency>();
                DapperConnection.CloseConnection(this._conn);
            }
            catch (Exception ex)
            {
                this.log = new LogHandler();
                this.log.WriteErrorLog(ex, this.errorMethodRoute, "GetAllCurrency");
            }
            return currencyDetail;
        }

        public int DeleteCurrency(int id, int userId)
        {
            int num = 0;
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add("CurrencyId", (object)id, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("UserId", (object)userId, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                this._conn = this.dapper.GetConnection();
                DapperConnection.OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                string str = SqlMapper.ExecuteScalar((IDbConnection)conn, "OMMS_DeleteCurrencyDetail", (object)dynamicParameters2, (IDbTransaction)null, nullable2, nullable3).ToString();
                DapperConnection.CloseConnection(this._conn);
                num = Convert.ToInt32(str);
            }
            catch (Exception ex)
            {
                this.log = new LogHandler();
                this.log.WriteErrorLog(ex, this.errorMethodRoute, nameof(DeleteCurrency));
            }
            return num;
        }

    }
}
