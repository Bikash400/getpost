using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMSCore.BAL.Models.HotelManagement
{
    public class LoginViewModel
    {
        public int Userid { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string UserSubType { get; set; }
        public int IsAuthenticated { get; set; }
        public string LoggedInName { get; set; }

    }

    public class LoginemployeeModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class LogincustomerModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class CustomerSigninModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }

    public class EmployeeDetails
    {
        public int Empid { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string shiftfrom { get; set; }
        public string shiftto { get; set; }


    }

    //-----------------------------------------------------Inventory--------------------------------------

    public class AddHotelDetails
    {
        public List<AddHotelDetails> HotelList { get; set; }
        public int Hotelid { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set;}
        public string HotelPhoneno { get; set;}
        public string HotelEmailid { get; set;}
        public int Numberofrooms { get; set; }
        public string hotelImages { get; set; }


    }

    public class CustomerBookingForm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AadharNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Hotels { get; set; }
        public string Cityname { get; set; }
        public string Hotelname { get; set; }
        public string phonenumber { get; set; }
        public string Emailid { get; set; }
        public string RoomPreference { get; set; }
        public string roomtype { get; set; }
        public string NumberofAdults { get; set; }
        public string Checkin { get; set; }
        public string checkout { get; set; }
        public string Payment { get; set; }
        public string Filepath { get; set; }

        public int Userid { get; set; }
    }

    public class GetCity
    {
        public int Cid { get; set; }
        public string Cityname { get; set; }
    }


    public class GetHotel
    {
        public int Hotelid { get; set; }
        public string Hotelname { get; set; }
        public int Cid { get; set; }

    }

    public class RoomType
    {
        public int roomid { get; set; }
        public string roomtype { get; set; }
    }

    public class RoomType2
    {
        public int Roomstypeid { get; set; }
        public string RoomName { get; set; }
    }


}
    