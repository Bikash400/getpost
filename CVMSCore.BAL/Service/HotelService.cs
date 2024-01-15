using CVMSCore.BAL.Models.HotelManagement;
using CVMSCore.BAL.Models.PostDataModel;
using CVMSCore.BAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMSCore.BAL.Service
{
    public class HotelService
    {
        HotelRepository Repository = new HotelRepository();


        public LoginViewModel UserLoginSer(string UserName, string Password)
        {
            return Repository.UserLoginRepo(UserName, Password);
        }
        public int AdminLoginSer(LoginViewModel obj)
        { 
                return Repository.AdminLoginRepo(obj);
        }

       

        public int EmployeeLoginSer(LoginemployeeModel obj)
        {
            return Repository.employeeLoginRepo(obj);
        }

        public int CustomerLoginSer(LogincustomerModel obj)
        {
            return Repository.customerLoginRepo(obj);
        }

        public int AddcustomerSer(CustomerSigninModel obj)
        {
            int num = 102;
            try
            {
                return Repository.Savecustomerrepo(obj);


            }
            catch
            {

            }
            return num;
        }

        public int AddEmpSer(EmployeeDetails obj)
        {
            int num = 102;
            try
            {
                return Repository.Saveemprepo(obj);


            }
            catch
            {

            }
            return num;
        }

        public List<EmployeeDetails> Getdata()
        {
            List<EmployeeDetails> list = new List<EmployeeDetails>();
            list = Repository.GetData();
            return list;
        }

        public int DeleteEmployeeById(int Id)
        {
            try
            {
                return Repository.DeleteEmployeeById(Id)
;
            }
            catch (Exception ex)
            {
                //Handle exceptions and log them
                throw ex;
            }
        }

        public List<EmployeeDetails> GetEmployeeById(int Id)
        {
            List<EmployeeDetails> list = new List<EmployeeDetails>();

            list = Repository.GetEmployeeById(Id);
            return list;
        }

        public int updateEmployee(EmployeeDetails obj, int Id)
            {
            return Repository.updateEmployee(obj, Id);
        }

        //public int AddhotelService(AddHotelDetails obj, string filepath1)
        //{
        //    int num = 102;
        //    try
        //    {
        //        return Repository.Savehotelrepo(obj, filepath1);


        //    }
        //    catch
        //    {

        //    }
        //    return num;
        //}

        public int AddhotelService(AddHotelDetails obj, string filepath1)
        {
            return Repository.Savehotelrepo(obj, filepath1);
        }
      

        public List<AddHotelDetails> GetHoteldata()
        {
            List<AddHotelDetails> list = new List<AddHotelDetails>();
            list = Repository.GetHotelData();
            return list;
        }

        public int DeleteHotelById(int Id)
        {
            try
            {
                return Repository.DeleteHotelById(Id)

;
            }
            catch (Exception ex)
            {
                //Handle exceptions and log them
                throw ex;
            }
        }

        public List<AddHotelDetails> EditHotelById(int Hotelid)
        {
            List<AddHotelDetails> list = new List<AddHotelDetails>();

            list = Repository.EditHotelById(Hotelid);
            return list;
        }

        public int updateHotel(AddHotelDetails obj, int Id)
        {
            return Repository.updateHotel(obj, Id);
        }

        public int Savecustomerhotelbookingservice(CustomerBookingForm obj)
        {
            int num = 102;
            try
            {
                return Repository.Savecustomerhotelbookingrepo(obj);


            }
            catch
            {

            }
            return num;
        }

        public List<GetCity> GetCity()
        {
            List<GetCity> city = new List<GetCity>();
            city = Repository.GetCity();
            return city;
        }

        public List<GetHotel> Gethotel(int cid)
        {
            return Repository.Gethotel(cid);
        }

        public List<RoomType> Getroom()
        {
            List<RoomType> room = new List<RoomType>();
            room = Repository.Getroom();
            return room;
        }

        public List<CustomerBookingForm> Getadmincustomerbookingdetailsser()
        {
            List<CustomerBookingForm> list = new List<CustomerBookingForm>();
            list = Repository.Getadmincustomerbookingdetailsrepo();
            return list;
        }

        public List<GetHotel> GetadminHoteldata(int cid)
        {
            //List<GetHotel> list = new List<GetHotel>();
            //list = Repository.GetadminHotelData();
            //return list;
            return Repository.GetadminHotelData(cid);


        }


        public List<GetCity> GetadminCitydata()
        {
            List<GetCity> list = new List<GetCity>();
            list = Repository.GetadminCityData();
            return list;
        }

        public List<CustomerBookingForm> Getemployeecustomerbookingdetailsser()
        {
            List<CustomerBookingForm> list = new List<CustomerBookingForm>();
            list = Repository.Getemployeecustomerbookingdetailsrepo();
            return list;
        }

        public List<GetCity> GetemployeeCitydata()
        {
            List<GetCity> list = new List<GetCity>();
            list = Repository.GetemployeeCityData();
            return list;
        }

        public List<GetHotel> GetemployeeHoteldata(int cid)
        {
            return Repository.GetemployeeHotelData(cid);

        }

        public List<EmployeeDetails> EmpDashSer(string name, string password)
        {
            List<EmployeeDetails> list = new List<EmployeeDetails>();
            list = Repository.EmployeeDashRepo(name, password);
            return list;
        }

        //public List<CustomerBookingForm> Gethotelimage()
        //{
        //    List<CustomerBookingForm> image = new List<CustomerBookingForm>();
        //    image = Repository.GetHotelimage();
        //    return image;
        //}

        //***************************************************

        public List<AddHotelDetails> Hotelimageser(int Hotelid)
        {
            List<AddHotelDetails> company = new List<AddHotelDetails>();
            try
            {
                return Repository.Hotelimagerepo(Hotelid)
;
            }
            catch (Exception ex)
            {
                // Handle exceptions and log them
                throw ex;
            }
        }


        //public int CustomeroginSer(LoginemployeeModel model)
        //{
        //    return Repository.customerLoginRepo(obj);
        //}

        public List<CustomerBookingForm> Getcustomerstatusser(int Userid)
        {
            List<CustomerBookingForm> company = new List<CustomerBookingForm>();
            try
            {
                return Repository.Getcustomerstatusrepo(Userid)
;
            }
            catch (Exception ex)
            {
                // Handle exceptions and log them
                throw ex;
            }
        }

        public List<AddHotelDetails> gethotels()
        {

            List<AddHotelDetails> hotel = new List<AddHotelDetails>();
            hotel = Repository.gethotels();
            return hotel;
        }
    }
    
}
