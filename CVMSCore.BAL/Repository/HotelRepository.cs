using CVMSCore.BAL.Common;
using CVMSCore.BAL.Models.HotelManagement;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CVMSCore.BAL.Repository
{
    public class HotelRepository
    {
        private SqlConnection _conn;  //for sql connection
        private DapperConnection dapper = new DapperConnection(ConnectionFile.Connection_ANTSDB);

        public LoginViewModel UserLoginRepo(string UserName, string Password)
        {
            LoginViewModel userDetail = null;

            try
            {

                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserName", UserName);
                dynamicParameters.Add("@Password", Password);

                using (this._conn = this.dapper.GetConnection())
                {
                    DapperConnection.OpenConnection(this._conn);


                    userDetail = _conn.Query<LoginViewModel>("ValidateUserLogin", dynamicParameters, commandType: CommandType.StoredProcedure)
                              .FirstOrDefault();
                    //userDetail = _conn.Query<LoginViewModel>("ValidateUserLogin", dynamicParameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, log, or rethrow if needed
            }
            finally
            {
                DapperConnection.CloseConnection(this._conn);
            }

            return userDetail;
        }
        public int AdminLoginRepo(LoginViewModel obj)
            {

                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@UserName", obj.UserName);
                dynamicParameters.Add("@Password", obj.Password);
                this._conn = this.dapper.GetConnection();
                DapperConnection.OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                var result = conn.QueryFirstOrDefault<int>("AdminLoginproc", dynamicParameters,commandType: CommandType.StoredProcedure);
                DapperConnection.CloseConnection(this._conn);

                return result;

            }

        public int employeeLoginRepo(LoginemployeeModel obj)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Username", obj.Username);
            dynamicParameters.Add("@Password", obj.Password);
            this._conn = this.dapper.GetConnection();
            DapperConnection.OpenConnection(this._conn);
            SqlConnection conn = this._conn;
            var result = conn.QueryFirstOrDefault<int>("EmployeeLoginproc", dynamicParameters, commandType: CommandType.StoredProcedure);
            DapperConnection.CloseConnection(this._conn);

            return result;
        }

        public int customerLoginRepo(LogincustomerModel obj)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Name", obj.Name);
            dynamicParameters.Add("@Password", obj.Password);
            this._conn = this.dapper.GetConnection();
            DapperConnection.OpenConnection(this._conn);
            SqlConnection conn = this._conn;
            var result = conn.QueryFirstOrDefault<int>("CustomerLoginproc", dynamicParameters, commandType: CommandType.StoredProcedure);
            DapperConnection.CloseConnection(this._conn);

            return result;
        }

        public int Savecustomerrepo(CustomerSigninModel obj)
        {
            DynamicParameters dynamicParameters1 = new DynamicParameters();
            //  dynamicParameters1.Add("EmpIdd", (object)0, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("Name", (object)obj.Name, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("Address", (object)obj.Address, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("Email", (object)obj.Email, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("Phone", (object)obj.Phone, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("Password", (object)obj.Password, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("ConfirmPassword", (object)obj.ConfirmPassword, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            this._conn = this.dapper.GetConnection();
            DapperConnection.OpenConnection(this._conn);
            SqlConnection conn = this._conn;
            DynamicParameters dynamicParameters2 = dynamicParameters1;
            CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
            int? nullable2 = new int?();
            CommandType? nullable3 = nullable1;
            string? str = SqlMapper.ExecuteScalar((IDbConnection)conn, "saf_custproc", (object)dynamicParameters2, (IDbTransaction)null, nullable2, nullable3).ToString();
            DapperConnection.CloseConnection(this._conn);
            int num = Convert.ToInt32(str);

            return num;
        }

        public int Saveemprepo(EmployeeDetails obj)
        {
            DynamicParameters dynamicParameters1 = new DynamicParameters();
            //  dynamicParameters1.Add("EmpIdd", (object)0, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("Name", (object)obj.Name, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("Address", (object)obj.Address, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("Email", (object)obj.Email, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("Phone", (object)obj.Phone, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("shiftfrom", (object)obj.shiftfrom, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("shiftto", (object)obj.shiftto, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("Password", (object)obj.Password, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            this._conn = this.dapper.GetConnection();
            DapperConnection.OpenConnection(this._conn);
            SqlConnection conn = this._conn;
            DynamicParameters dynamicParameters2 = dynamicParameters1;
            CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
            int? nullable2 = new int?();
            CommandType? nullable3 = nullable1;
            string? str = SqlMapper.ExecuteScalar((IDbConnection)conn, "saf_GetEmpDetails", (object)dynamicParameters2, (IDbTransaction)null, nullable2, nullable3).ToString();
            DapperConnection.CloseConnection(this._conn);
            int num = Convert.ToInt32(str);

            return num;
        }

        public List<EmployeeDetails> GetData()
        {
            List<EmployeeDetails> obj = new List<EmployeeDetails>();
            this._conn = this.dapper.GetConnection();
            DapperConnection.OpenConnection(this._conn);
            SqlConnection conn = this._conn;
            obj = _conn.Query<EmployeeDetails>("getEmployee", commandType: CommandType.StoredProcedure).ToList();
            DapperConnection.CloseConnection(this._conn);
            return obj;
        }

        public int DeleteEmployeeById(object Id)
        {
            try
            {
                using (this._conn = dapper.GetConnection())
                {
                    DapperConnection.OpenConnection(this._conn);

                    // Implement your delete logic using Dapper and a stored procedure here
                    // Example:
                    var parameters = new DynamicParameters();
                    parameters.Add("@Empid", Id);

                    var result = _conn.Execute("deleteEmployee", parameters, commandType: CommandType.StoredProcedure);

                    DapperConnection.CloseConnection(this._conn);
                    return result; // The stored procedure should return the number of affected rows
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and log them
                throw ex;
            }
        }

        public List<EmployeeDetails> GetEmployeeById(object Id)
        {
            List<EmployeeDetails> list = new List<EmployeeDetails>();
            try
            {
                this._conn = this.dapper.GetConnection();
                DapperConnection.OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Empid", Id, DbType.Int32, ParameterDirection.Input);
                list = conn.Query<EmployeeDetails>("GetSAF_EmployeeEdits", dynamicParameters, commandType: CommandType.StoredProcedure).ToList();
                DapperConnection.CloseConnection(this._conn);

            }
            catch (Exception)
            {


            }

            return list;
        }

        public int updateEmployee(EmployeeDetails obj, int Id)
        {
            int num = 0;
            try
            {
                

                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add("Name", (object)obj.Name, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("Address", (object)obj.Address, new DbType?(), new ParameterDirection?(), new char?(), new byte?(), new byte?());
                dynamicParameters1.Add("Email", (object)obj.Email, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("Phone", (object)obj.Phone, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("shiftfrom", (object)obj.shiftfrom, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("shiftto", (object)obj.shiftto, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("Password", (object)obj.Password, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("@Empid", (object)Id, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());



                //dynamicParameters1.Add("USERTYPE", (object)"", new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                //dynamicParameters1.Add("SubUserType", (object)"", new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                //dynamicParameters1.Add("UserID", (object)"", new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                this._conn = this.dapper.GetConnection();
                DapperConnection.OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                string? str = SqlMapper.ExecuteScalar((IDbConnection)conn, "SAF_UpdateEmployee", (object)dynamicParameters2, (IDbTransaction)null, nullable2, nullable3).ToString();
                DapperConnection.CloseConnection(this._conn);
                num = Convert.ToInt32(str);

            }
            catch (Exception ex)
            {

            }

            return num;

        }

        public int Savehotelrepo(AddHotelDetails obj, string filepath1)
        {
            DynamicParameters dynamicParameters1 = new DynamicParameters();
            //  dynamicParameters1.Add("EmpIdd", (object)0, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            //dynamicParameters1.Add("Hotelid", (object)obj.Hotelid, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("HotelName", (object)obj.HotelName, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("HotelAddress", (object)obj.HotelAddress, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("HotelPhoneno", (object)obj.HotelPhoneno, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("HotelEmailid", (object)obj.HotelEmailid, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("Numberofrooms", (object)obj.Numberofrooms, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("hotelImages", (object)obj.hotelImages, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            this._conn = this.dapper.GetConnection();
            DapperConnection.OpenConnection(this._conn);
            SqlConnection conn = this._conn;
            DynamicParameters dynamicParameters2 = dynamicParameters1;
            CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
            int? nullable2 = new int?();
            CommandType? nullable3 = nullable1;
            string? str = SqlMapper.ExecuteScalar((IDbConnection)conn, "saf_GethotelDetails", (object)dynamicParameters2, (IDbTransaction)null, nullable2, nullable3).ToString();
            DapperConnection.CloseConnection(this._conn);
            int num = Convert.ToInt32(str);
            return num;
        }

        public List<AddHotelDetails> GetHotelData()
        {
            List<AddHotelDetails> obj = new List<AddHotelDetails>();
            this._conn = this.dapper.GetConnection();
            DapperConnection.OpenConnection(this._conn);
            SqlConnection conn = this._conn;
            obj = _conn.Query<AddHotelDetails>("getHotel", commandType: CommandType.StoredProcedure).ToList();
            DapperConnection.CloseConnection(this._conn);
            return obj;
        }

        public int DeleteHotelById(int Id)
        {
            try
            {
                using (this._conn = dapper.GetConnection())
                {
                    DapperConnection.OpenConnection(this._conn);

                    // Implement your delete logic using Dapper and a stored procedure here
                    // Example:
                    var parameters = new DynamicParameters();
                    parameters.Add("@Hotelid", Id);

                    var result = _conn.Execute("deleteHotel", parameters, commandType: CommandType.StoredProcedure);

                    DapperConnection.CloseConnection(this._conn);
                    return result; // The stored procedure should return the number of affected rows
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and log them
                throw ex;
            }
        }

        public List<AddHotelDetails> EditHotelById(int Hotelid)
        {
            List<AddHotelDetails> list = new List<AddHotelDetails>();
            try
            {
                this._conn = this.dapper.GetConnection();
                DapperConnection.OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@Hotelid", Hotelid, DbType.Int32, ParameterDirection.Input);
                list = conn.Query<AddHotelDetails>("GetSAF_HotelEdits", dynamicParameters, commandType: CommandType.StoredProcedure).ToList();
                DapperConnection.CloseConnection(this._conn);

            }
            catch (Exception)
            {


            }

            return list;
        }

        internal int updateHotel(AddHotelDetails obj, int Id)
        {
            int num = 0;
            try
            {


                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add("HotelName", (object)obj.HotelName, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("HotelAddress", (object)obj.HotelAddress, new DbType?(), new ParameterDirection?(), new char?(), new byte?(), new byte?());
                dynamicParameters1.Add("HotelPhoneno", (object)obj.HotelPhoneno, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("HotelEmailid", (object)obj.HotelEmailid, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("Numberofrooms", (object)obj.Numberofrooms, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("@Hotelid", (object)Id, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());



                //dynamicParameters1.Add("USERTYPE", (object)"", new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                //dynamicParameters1.Add("SubUserType", (object)"", new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                //dynamicParameters1.Add("UserID", (object)"", new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                this._conn = this.dapper.GetConnection();
                DapperConnection.OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                string? str = SqlMapper.ExecuteScalar((IDbConnection)conn, "SAF_UpdateHotel", (object)dynamicParameters2, (IDbTransaction)null, nullable2, nullable3).ToString();
                DapperConnection.CloseConnection(this._conn);
                num = Convert.ToInt32(str);

            }
            catch (Exception ex)
            {

            }

            return num;
        }

        public int Savecustomerhotelbookingrepo(CustomerBookingForm obj)
        {
            DynamicParameters dynamicParameters1 = new DynamicParameters();
            //  dynamicParameters1.Add("EmpIdd", (object)0, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("FirstName", (object)obj.FirstName, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("LastName", (object)obj.LastName, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("AadharNumber", (object)obj.AadharNumber, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("Address", (object)obj.Address, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("City", (object)obj.City, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("Hotels", (object)obj.Hotels, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("phonenumber", (object)obj.phonenumber, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("Emailid", (object)obj.Emailid, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("RoomPreference", (object)obj.RoomPreference, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("NumberofAdults", (object)obj.NumberofAdults, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("Payment", (object)obj.Payment, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("Checkin", (object)obj.Checkin, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("checkout", (object)obj.checkout, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            dynamicParameters1.Add("Userid", (object)obj.Userid, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
            this._conn = this.dapper.GetConnection();
            DapperConnection.OpenConnection(this._conn);
            SqlConnection conn = this._conn;
            DynamicParameters dynamicParameters2 = dynamicParameters1;
            CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
            int? nullable2 = new int?();
            CommandType? nullable3 = nullable1;
            string? str = SqlMapper.ExecuteScalar((IDbConnection)conn, "Savecustomerhotelbookingproc", (object)dynamicParameters2, (IDbTransaction)null, nullable2, nullable3).ToString();
            DapperConnection.CloseConnection(this._conn);
            int num = Convert.ToInt32(str);
            return num;
        }

        public List<GetCity> GetCity()
        {
            DynamicParameters dynamicParameters1 = new DynamicParameters();
            List<GetCity> city = new List<GetCity>();
            this._conn = this.dapper.GetConnection();
            DapperConnection.OpenConnection(this._conn);
            SqlConnection conn = this._conn;
            DynamicParameters dynamicParameters2 = dynamicParameters1;
            CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
            // int? nullable2 = new int?();
            CommandType? nullable3 = nullable1;
            city = _conn.Query<GetCity>("city_get", commandType: CommandType.StoredProcedure).ToList();
            DapperConnection.CloseConnection(this._conn);
            //int num = Convert.ToInt32(str);
            return city;
        }


        public List<GetHotel> Gethotel(int cid)
        {
            List<GetHotel> HotelList = new List<GetHotel>();

            using (_conn = dapper.GetConnection())
            {
                DapperConnection.OpenConnection(_conn);

                HotelList = _conn.Query<GetHotel>("hotel_get", new { CId = cid }, commandType: CommandType.StoredProcedure).ToList();

                DapperConnection.CloseConnection(_conn);
            }

            return HotelList;
        }

         public List<RoomType> Getroom()
        {
            DynamicParameters dynamicParameters1 = new DynamicParameters();
            List<RoomType> room = new List<RoomType>();
            this._conn = this.dapper.GetConnection();
            DapperConnection.OpenConnection(this._conn);
            SqlConnection conn = this._conn;
            DynamicParameters dynamicParameters2 = dynamicParameters1;
            CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
            // int? nullable2 = new int?();
            CommandType? nullable3 = nullable1;
            room = _conn.Query<RoomType>("getroom", commandType: CommandType.StoredProcedure).ToList();
            DapperConnection.CloseConnection(this._conn);
            //int num = Convert.ToInt32(str);
            return room;
        }

        public List<CustomerBookingForm> Getadmincustomerbookingdetailsrepo()
        {
            List<CustomerBookingForm> obj = new List<CustomerBookingForm>();
            this._conn = this.dapper.GetConnection();
            DapperConnection.OpenConnection(this._conn);
            SqlConnection conn = this._conn;
            obj = _conn.Query<CustomerBookingForm>("Getadmincustomerbookingdetailsprocedure", commandType: CommandType.StoredProcedure).ToList();
            DapperConnection.CloseConnection(this._conn);
            return obj;

        }
        
        public List<GetHotel> GetadminHotelData(int cid)
        {
            List<GetHotel> HotelList = new List<GetHotel>();

            using (_conn = dapper.GetConnection())
            {
                DapperConnection.OpenConnection(_conn);

                HotelList = _conn.Query<GetHotel>("hotel_get", new { CId = cid }, commandType: CommandType.StoredProcedure).ToList();

                DapperConnection.CloseConnection(_conn);
            }

            return HotelList;
        }


        public List<GetCity> GetadminCityData()
        {
            List<GetCity> obj = new List<GetCity>();
            this._conn = this.dapper.GetConnection();
            DapperConnection.OpenConnection(this._conn);
            SqlConnection conn = this._conn;
            obj = _conn.Query<GetCity>("admincitybindingproc", commandType: CommandType.StoredProcedure).ToList();
            DapperConnection.CloseConnection(this._conn);
            return obj;
        }

        public List<CustomerBookingForm> Getemployeecustomerbookingdetailsrepo()
        {

            List<CustomerBookingForm> obj = new List<CustomerBookingForm>();
            this._conn = this.dapper.GetConnection();
            DapperConnection.OpenConnection(this._conn);
            SqlConnection conn = this._conn;
            obj = _conn.Query<CustomerBookingForm>("Getadmincustomerbookingdetailsprocedure", commandType: CommandType.StoredProcedure).ToList();
            DapperConnection.CloseConnection(this._conn);
            return obj;
        }

        public List<GetCity> GetemployeeCityData()
        {
            List<GetCity> obj = new List<GetCity>();
            this._conn = this.dapper.GetConnection();
            DapperConnection.OpenConnection(this._conn);
            SqlConnection conn = this._conn;
            obj = _conn.Query<GetCity>("admincitybindingproc", commandType: CommandType.StoredProcedure).ToList();
            DapperConnection.CloseConnection(this._conn);
            return obj;
        }

        public List<GetHotel> GetemployeeHotelData(int cid)
        {
            List<GetHotel> HotelList = new List<GetHotel>();

            using (_conn = dapper.GetConnection())
            {
                DapperConnection.OpenConnection(_conn);

                HotelList = _conn.Query<GetHotel>("hotel_get", new { CId = cid }, commandType: CommandType.StoredProcedure).ToList();

                DapperConnection.CloseConnection(_conn);
            }

            return HotelList;
        }

        public List<EmployeeDetails> EmployeeDashRepo(string Name, string Password)
        {
            List<EmployeeDetails> obj = new List<EmployeeDetails>();

            this._conn = this.dapper.GetConnection();
            DapperConnection.OpenConnection(this._conn);
            SqlConnection conn = this._conn;

            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Name", Name);
            dynamicParameters.Add("@Password", Password);

            obj = conn.Query<EmployeeDetails>("EmployeeCredentialsDetails", dynamicParameters, commandType: CommandType.StoredProcedure).ToList();

            DapperConnection.CloseConnection(this._conn);
            return obj;
        }

        public List<CustomerBookingForm> GetHotelimage()
        {
            List<CustomerBookingForm> obj = new List<CustomerBookingForm>();
            this._conn = this.dapper.GetConnection();
            DapperConnection.OpenConnection(this._conn);
            SqlConnection conn = this._conn;
            obj = _conn.Query<CustomerBookingForm>("hotelimage", commandType: CommandType.StoredProcedure).ToList();
            DapperConnection.CloseConnection(this._conn);
            return obj; throw new NotImplementedException();
        }

        //************************************************************
        public List<AddHotelDetails> Hotelimagerepo(int Hotelid)
        {
            List<AddHotelDetails> getlist = new List<AddHotelDetails>();
            try
            {
                using (this._conn = dapper.GetConnection())
                {
                    DapperConnection.OpenConnection(this._conn);
                    // Implement your retrieval logic using Dapper and a stored procedure here
                    // Example:
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Hotelid", Hotelid);
                    getlist = _conn.Query<AddHotelDetails>("GetSAF_HotelEdits", parameters, commandType: CommandType.StoredProcedure).ToList();
                    DapperConnection.CloseConnection(this._conn);
                    //return employee;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and log them

            }
            return getlist;
        }

        public List<CustomerBookingForm> Getcustomerstatusrepo(int Userid)
        {
            List<CustomerBookingForm> obj = new List<CustomerBookingForm>();
            this._conn = this.dapper.GetConnection();
            DapperConnection.OpenConnection(this._conn);
            SqlConnection conn = this._conn;
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Userid", Userid, DbType.Int32, ParameterDirection.Input);
            obj = conn.Query<CustomerBookingForm>("GetCustomerStatus", dynamicParameters, commandType: CommandType.StoredProcedure).ToList();
            DapperConnection.CloseConnection(this._conn);

            return obj;
        }

        public List<AddHotelDetails> gethotels()
        {
            List<AddHotelDetails> hotels = new List<AddHotelDetails>();
            string Statustxt = null;
           
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                //dynamicParameters1.Add("userName", (object)obj.username, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                // dynamicParameters1.Add("password", (object)password, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                this._conn = this.dapper.GetConnection();
                DapperConnection.OpenConnection(this._conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;

                hotels = _conn.Query<AddHotelDetails>("gethotelimage", dynamicParameters2, commandType: CommandType.StoredProcedure).ToList();

            
            //catch (Exception ex)
            //{
            //    this.log = new LogHandler();
            //    this.log.WriteErrorLog(ex, this.errorMethodRoute, nameof(CheckUserExist));
            //}
            return hotels;
        }
    }
}



