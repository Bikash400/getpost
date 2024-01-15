using CVMSCore.BAL.Models.HotelManagement;
using CVMSCore.BAL.Models.PostDataModel;
using CVMSCore.BAL.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json;

namespace CVMS_Core.Controllers
{
    public class HotelManagementController : BaseController
    {
        HotelService Service = new HotelService();
        private HotelService officeShiftService;
       

        public IActionResult login(string UserName, string Password)
        {
            LoginViewModel userdt = new LoginViewModel();
            List<LoginViewModel> userdtt = new List<LoginViewModel>();
            if (UserName != null && Password != null)
            {

                userdt = Service.UserLoginSer(UserName, Password);
                if (userdt != null)
                {
                    HttpContext.Session.SetString("LoggedUserInfo", JsonConvert.SerializeObject(userdt));
                    var value = HttpContext.Session.GetString("LoggedUserInfo");
                    ViewBag.Userid=userdt.Userid;
                    //string _userDetail = Security.GetEncryptString(userdt.UserName.Trim() + "|" + encryptPassword.Trim());
                    userdt = GetUserDetail();
                }

                if (userdt != null)
                {
                    if (userdt.UserSubType == "Admin")
                    {

                        return RedirectToAction("adminhomepage", "HotelManagement");
                    }
                    else if (userdt.UserSubType == "Employee")
                    {

                        return RedirectToAction("Employeehomepage", "HotelManagement");
                    }
                    else if (userdt.UserSubType == "Customer")
                    {

                        return RedirectToAction("Customerhomepage", "HotelManagement", new { Userid = userdt.Userid });
                    }
                 
                }
                //if(userdt != null)
                //{
                //    HttpContext.Session.SetString("LoggedUserInfo", JsonConvert.SerializeObject(userdt));
                //    var value = HttpContext.Session.GetString("LoggedUserInfo");
                //    //string _userDetail = Security.GetEncryptString(userdt.UserName.Trim() + "|" + encryptPassword.Trim());
                //    userdt = GetUserDetail();
                //}


                ModelState.AddModelError("", "Invalid username or password");
                return View(admin);
            }
            else
            {

                return View();
            }
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Home()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AdminLoginPage(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = Service.AdminLoginSer(model);

                if (result == 1)
                {

                    return RedirectToAction("adminhomepage", "HotelManagement");
                }
                else
                {

                    ViewBag.ReturnMessage = "Invalid credentials";
                    return View("admin", model);
                }
            }


            return View("admin", model);
        }


        public IActionResult admin()
        {
            return View();
        }
        public IActionResult adminhomepage()
        {
            return View();
        }

        public IActionResult Adminseecustomerbookingdetails()
        {
            return View();
        }

        public JsonResult Getadmincustomerbookingdetails()
        {
            List<CustomerBookingForm> customer = new List<CustomerBookingForm>();
            customer = Service.Getadmincustomerbookingdetailsser();
            return Json(new { customer = customer });

        }



        public JsonResult GetadminHotelDetails(int cid)
        {
            List<GetHotel> hotel = new List<GetHotel>();
            hotel = Service.GetadminHoteldata(cid);
            return Json(new { hotel = hotel });

        }

        public JsonResult GetadminCityDetails()
        {
            List<GetCity> city = new List<GetCity>();
            city = Service.GetadminCitydata();
            return Json(new { city = city });

        }

        //----------------------------------------------------Employee--------------------------------------------------------
        public int Saveemployeedetails(IFormCollection formcollection)
        {
            var result = 0;
            if (formcollection != null)
            {
                EmployeeDetails obj = new EmployeeDetails();

                obj.Name = Convert.ToString(formcollection["Name"]);
                obj.Address = Convert.ToString(formcollection["Address"]);
                obj.Email = Convert.ToString(formcollection["Email"]);
                obj.Phone = Convert.ToString(formcollection["Phone"]);
                obj.shiftfrom = Convert.ToString(formcollection["shiftfrom"]);
                obj.shiftto = Convert.ToString(formcollection["shiftto"]);
                obj.Password = Convert.ToString(formcollection["Password"]);

                result = Service.AddEmpSer(obj);
            }
            return result;

        }
            

        public IActionResult GetEmpDetails()
        {
            return View();
        }

        public JsonResult GetMethod()
        {
            List<EmployeeDetails> employees = new List<EmployeeDetails>();
            employees = Service.Getdata();
            return Json(new { employees = employees });

        }

        public JsonResult Delete(int Id)
        {
            try
            {
                int result = Service.DeleteEmployeeById(Id)
;

                if (result > 0)
                {
                    //Successfully deleted
                    return Json(new { success = true, message = "Employee deleted successfully" });
                }
                else
                {
                    //Employee with the specified ID was not found
                    return Json(new { success = false, message = "Employee not found" });
                }
            }
            catch (Exception ex)
            {
                //Handle exceptions, log them, and return an error response
                return Json(new { success = false, message = "An error occurred while deleting the employee" });
            }
        }

        
        public JsonResult EditEmployee(int Empid)
        {

            List<EmployeeDetails> list = new List<EmployeeDetails>();
            
            list = Service.GetEmployeeById(Empid);

            return Json(new { list = list });
        }
        
        [HttpPost]
        public JsonResult UpdateEmployee(IFormCollection formcollection)
        {
            int result = 0;

            EmployeeDetails obj = new EmployeeDetails();

            obj.Name = Convert.ToString(formcollection["Name"]);
            obj.Address = Convert.ToString(formcollection["Address"]);
            obj.Email = Convert.ToString(formcollection["Email"]);
            obj.Phone = Convert.ToString(formcollection["Phone"]);
            obj.shiftfrom = Convert.ToString(formcollection["shiftfrom"]);
            obj.shiftto = Convert.ToString(formcollection["shiftto"]);
            obj.Password = Convert.ToString(formcollection["Password"]);
            var Id = Convert.ToInt32(formcollection["Empid"]);
            result = Service.updateEmployee(obj, Id);

            //return Json(new { list = list });
            // result = PostService.updatefault(int Id);
            return Json(new { success = false, message = "Employee not found" });
        }

        public IActionResult Employee()
        {
            return View();
        }


        public IActionResult Employeehomepage()
        {
            return View();
        }

        public IActionResult Customerseebookingdetails()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EmployeeLoginPage(LoginemployeeModel model)
        {

            if (ModelState.IsValid)
            {
                var result = Service.EmployeeLoginSer(model);

                if (result == 1)
                {

                    return RedirectToAction("Employeehomepage", "HotelManagement");
                }
                else
                {

                    ViewBag.ReturnMessage = "Invalid credentials";
                    return View("Employee", model);
                }

            }
            return View("Employee", model);
        }
//----------------------------------------------------------------------------------------------------------------------------
        //public IActionResult Employee()
        //{
        //    return View();
        //}
        //-----------------------------------------------------New Hotel----------------------------------------------------------------------------

        public IActionResult AddHotel()
        {
            return View();
        }

        public JsonResult Savehoteldetails(IFormCollection formcollection)
        {
            var result = 0;
           // IFormFile file = Request.Form.Files[0];
          //  string filename = "";
            string filepath1 = "";
           // string savefile = "";
            if (formcollection != null )
            {
                AddHotelDetails obj = new AddHotelDetails();

                obj.HotelName = Convert.ToString(formcollection["HotelName"]);
                obj.HotelAddress = Convert.ToString(formcollection["HotelAddress"]);
                obj.HotelPhoneno = Convert.ToString(formcollection["HotelPhoneno"]);
                obj.HotelEmailid = Convert.ToString(formcollection["HotelEmailid"]);
                obj.Numberofrooms= Convert.ToInt32(formcollection["Numberofrooms"]);
                obj.hotelImages = Convert.ToString(formcollection["hotelImages"]);
                result = Service.AddhotelService(obj, filepath1);


                //filename = file.FileName;
                //filepath = Path.Combine("~/myfile/", filename);
                //result = Service.AddhotelService(obj, filepath);
            }
            return Json(new {result=result});

        }

        public IActionResult showimage()
        {
            List<AddHotelDetails> Hotel = Service.gethotels();
            var viewModel = new AddHotelDetails
            {
                HotelList = Hotel,

            };
            return View(viewModel);

        }
        //public JsonResult Hotelimage()
        //{
        //    List<CustomerBookingForm> image = new List<CustomerBookingForm>();
        //    image = Service.Gethotelimage();
        //    return Json(new { image = image });

        //}
        public JsonResult GetHotelDetails()
        {
            List<AddHotelDetails> employees = new List<AddHotelDetails>();
            employees = Service.GetHoteldata();
            return Json(new { employees = employees });

        }

        public JsonResult DeleteHotel(int Id)
        {
            try
            {
                int result = Service.DeleteHotelById(Id)

;

                if (result > 0)
                {
                    //Successfully deleted
                    return Json(new { success = true, message = "Hotel deleted successfully" });
                }
                else
                {
                    //Employee with the specified ID was not found
                    return Json(new { success = false, message = "Hotel not found" });
                }
            }
            catch (Exception ex)
            {
                //Handle exceptions, log them, and return an error response
                return Json(new { success = false, message = "An error occurred while deleting the Hotel" });
            }
        }

        public JsonResult EditHotels(int Hotelid)
        {

            List<AddHotelDetails> list = new List<AddHotelDetails>();

            list = Service.EditHotelById(Hotelid);

            return Json(new { list = list });
        }


        [HttpPost]
        public JsonResult UpdateHotel(IFormCollection formcollection)
        {
            int result = 0;

            AddHotelDetails obj = new AddHotelDetails();

            obj.HotelName = Convert.ToString(formcollection["HotelName"]);
            obj.HotelAddress = Convert.ToString(formcollection["HotelAddress"]);
            obj.HotelPhoneno = Convert.ToString(formcollection["HotelPhoneno"]);
            obj.HotelEmailid = Convert.ToString(formcollection["HotelEmailid"]);
            obj.Numberofrooms = Convert.ToInt32(formcollection["Numberofrooms"]);
            var Id = Convert.ToInt32(formcollection["Hotelid"]);
            result = Service.updateHotel(obj, Id);

            //return Json(new { list = list });



            // result = PostService.updatefault(int Id);
            return Json(new { success = false, message = "Employee not found" });
        }
        //----------------------------------------------------------------------------------------------------------------------------------------


        //----------------------------------------------------------Employee------------------------------------------------------------------------------

        public IActionResult Empseedetails()
        {
            return View();
        }

        public JsonResult Getemployeecustomerbookingdetails()
        {
            List<CustomerBookingForm> customer = new List<CustomerBookingForm>();
            customer = Service.Getemployeecustomerbookingdetailsser();
            return Json(new { customer = customer });

        }

        public JsonResult GetemployeeCityDetails()
        {
            List<GetCity> city = new List<GetCity>();
            city = Service.GetemployeeCitydata();
            return Json(new { city = city });

        }

        public JsonResult GetemployeeHotelDetails(int cid)
        {
            List<GetHotel> hotel = new List<GetHotel>();
            hotel = Service.GetemployeeHoteldata(cid);
            return Json(new { hotel = hotel });

        }
        //-----------------------------------------------------------------------------------------------------------


       

       
        //----------------------------------------------------------------Customer------------------------------------------------------------------------

        public IActionResult Customer()
        {
            return View();
        }

        public IActionResult Customerhomepage(int Userid)
        {
            ViewBag.Userid = Userid;
            return View();
        }

        public IActionResult Customerloginpage(LogincustomerModel model)
        {
            if (ModelState.IsValid)
            {
                var result = Service.CustomerLoginSer(model);

                if (result == 1)
                {

                    return RedirectToAction("Customerhomepage", "HotelManagement");
                }
                else
                {

                    ViewBag.ReturnMessage = "Invalid credentials";

                }

            }
            return View("Customer", model);
        }

        public IActionResult SignIn()
        {
            return View();


        }

        public IActionResult cutomerformbooking(int Userid)
        {
            ViewBag.Userid = Userid;
            return View();

        }

        public int Savecustomerhotelbooking(IFormCollection formcollection)
        {
            var result = 0;
            if (formcollection != null)
            {
                CustomerBookingForm obj = new CustomerBookingForm();

                obj.FirstName = Convert.ToString(formcollection["FirstName"]);
                obj.LastName = Convert.ToString(formcollection["LastName"]);
                obj.AadharNumber = Convert.ToString(formcollection["AadharNumber"]);
                obj.Address = Convert.ToString(formcollection["Address"]);
                obj.City = Convert.ToString(formcollection["City"]);
                obj.Hotels = Convert.ToString(formcollection["Hotels"]);
                obj.phonenumber = Convert.ToString(formcollection["phonenumber"]);
                obj.Emailid = Convert.ToString(formcollection["Emailid"]);
                obj.RoomPreference = Convert.ToString(formcollection["RoomPreference"]);
                obj.NumberofAdults = Convert.ToString(formcollection["NumberofAdults"]);
                obj.Payment = Convert.ToString(formcollection["Payment"]);
                obj.Checkin = Convert.ToString(formcollection["Checkin"]);
                obj.checkout = Convert.ToString(formcollection["checkout"]);
                obj.Userid = Convert.ToInt32(formcollection["Userid"]);

                result = Service.Savecustomerhotelbookingservice(obj);
            }
            return result;
            
        }

        public JsonResult GetCityList()
        {
            List<GetCity> city = new List<GetCity>();
            city = Service.GetCity();
            return Json(new { City = city });
            //return json(output, jsonrequestbehavior.allowget);
        }

        public JsonResult BindHotel(int cid)
        {
            List<GetHotel> hotelbinding = new List<GetHotel>();
            hotelbinding = Service.Gethotel(cid);
            return Json(new { hotelbinding = hotelbinding });
        }

        public JsonResult GetRoomtype()
        {
            List<RoomType> room = new List<RoomType>();
            room = Service.Getroom();
            return Json(new { room = room });
            //return json(output, jsonrequestbehavior.allowget);
        }

      
        //-----------------------------------------------------------------------------------------------------------------------------------------
        public int Savecustomerdetails(IFormCollection formcollection)
        {
            var result = 0;
            if (formcollection != null)
            {
                CustomerSigninModel obj = new CustomerSigninModel();

                obj.Name = Convert.ToString(formcollection["Name"]);
                obj.Address= Convert.ToString(formcollection["Address"]);
                obj.Email = Convert.ToString(formcollection["Email"]);
                obj.Phone = Convert.ToString(formcollection["Phone"]);
                obj.Password = Convert.ToString(formcollection["Password"]);
                obj.ConfirmPassword = Convert.ToString(formcollection["ConfirmPassword"]);

                result = Service.AddcustomerSer(obj);
            }
            return result;

        }

        public IActionResult EmployeeDashBoardPage()
        {
            return View();
        }
        public JsonResult EmployeeDashLogin(string Name, string Password)
        {
            if (Name != null && Password != null)
            {
                var result = Service.EmpDashSer(Name, Password);

                if (result != null && result.Count > 0)
                {
                    var shiftTiming = $"{result[0].shiftfrom} - {result[0].shiftto}";
                    var employeeName = $"{result[0].Name}";                 

                    return Json(new { success = true, shiftTiming, employeeName, });
                }
                else
                {
                    return Json(new { success = false, message = "Invalid credentials" });
                }
            }
            else
            {
                return Json(new { success = false, message = "Invalid input" });
            }
        }


        //********************************************************************
        public JsonResult Hotelimage(int Hotelid)
        {
            List<AddHotelDetails> company = new List<AddHotelDetails>();
            //Newcity company = new Newcity();
            try
            {
                company = Service.Hotelimageser(Hotelid)
;
            }
            catch (Exception ex)
            {

            }
            return Json(new { company = company });


        }
        public JsonResult Getcustomerstatus(int Userid)
        {
            List<CustomerBookingForm> customer = new List<CustomerBookingForm>();
            customer = Service.Getcustomerstatusser(Userid);
            return Json(new { customer = customer });

        }

    }

  
}

