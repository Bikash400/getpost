using CVMSCore.BAL;
using CVMSCore.BAL.Models.Master;
using CVMSCore.BAL.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;


namespace CVMS_Core.Controllers
{
    public class MasterController : Controller
    {
        MastersService service = new MastersService();
        public IActionResult Index()
        {
            return View();
        }

        #region Country Master
        [HttpGet]   
        public IActionResult Country()
        {
            List<CountryMaster> lstCountry = new List<CountryMaster>();
            VMUserDetail vMUserDetail = new VMUserDetail();
            var value = HttpContext.Session.GetString("LoggedUserInfo");
            vMUserDetail = JsonConvert.DeserializeObject<VMUserDetail>(value);

            if (vMUserDetail != null )
            {
                int id = vMUserDetail.Id;
                int countryId = vMUserDetail.CountryId;
                string usertype = vMUserDetail.UserType;
                string usersubtype = vMUserDetail.UserSubType;

                lstCountry = service.GetCountryList(id, countryId, usertype, usersubtype);
                ViewBag.CountryList = lstCountry;
                ViewBag.RegionList = service.GetRegionList();
                ViewBag.CurrencyList = service.GetCurrencyList();
                ViewBag.TimeZoneList = service.GetTimeZoneList();

            }


            return View();
        }

        [HttpPost]
        public string SaveCountry(IFormCollection formcollection, int UserId)
        {
            string strReturn = "Some error occured.";
            List<CountryMaster> lstCountry = new List<CountryMaster>();
            VMUserDetail vMUserDetail = new VMUserDetail();
            var value = HttpContext.Session.GetString("LoggedUserInfo");
            vMUserDetail = JsonConvert.DeserializeObject<VMUserDetail>(value);

            if (vMUserDetail != null)
            {
                int id = vMUserDetail.Id;
                int countryId = vMUserDetail.CountryId;
                string usertype = vMUserDetail.UserType;
                string usersubtype = vMUserDetail.UserSubType;
                lstCountry = service.GetCountryList(id, countryId, usertype, usersubtype);
                ViewBag.CountryList = lstCountry;
                ViewBag.RegionList = service.GetRegionList();
                ViewBag.CurrencyList = service.GetCurrencyList();
                ViewBag.TimeZoneList = service.GetTimeZoneList();
            }

            if (formcollection != null)
            {
                CountryMaster obj = new CountryMaster();
                obj.CountryID = Convert.ToInt32(formcollection["CountryID"]);
                obj.Country = Convert.ToString(formcollection["Country"]);
                obj.CountryCode = !string.IsNullOrEmpty(formcollection["CountryCode"]) ? formcollection["CountryCode"] : formcollection["CountryCode"];
                obj.RegionID = Convert.ToInt32(formcollection["RegionID"]);
                obj.Remarks = Convert.ToString(formcollection["Remarks"]);
                obj.CurrencyId = Convert.ToInt32(formcollection["CurrencyId"]);
                obj.TimeZoneId = Convert.ToInt32(formcollection["TimeZoneId"]);

                int result = (int)StatusMessageCode.StatusCode.Error;
                if (Convert.ToInt32(formcollection["CountryID"]) == 0)
                    result = service.SaveCountry(obj, UserId);
                else
                    result = service.UpdateCountry(obj, UserId);

                if (result == (int)StatusMessageCode.StatusCode.Success)
                    strReturn = "success";
                else if (result == (int)StatusMessageCode.StatusCode.AlreadyExist)
                    strReturn = "This country  already exist.";
                else
                    strReturn = "Some error occured.";
            }

            return strReturn;

        }

        [HttpGet]
        public JsonResult GetCountryDetail(int countryId)
        {
            List<CountryMaster> lstCountry = new List<CountryMaster>();
            VMUserDetail vMUserDetail = new VMUserDetail();
            var value = HttpContext.Session.GetString("LoggedUserInfo");
            vMUserDetail = JsonConvert.DeserializeObject<VMUserDetail>(value);

            if (vMUserDetail != null)
            {
                int id = vMUserDetail.Id;
               //int countryId = vMUserDetail.CountryId;
                string usertype = vMUserDetail.UserType;
                string usersubtype = vMUserDetail.UserSubType;

                lstCountry = service.GetCountryList(id, countryId, usertype, usersubtype);
                ViewBag.CountryList = lstCountry;
                ViewBag.RegionList = service.GetRegionList();
                ViewBag.CurrencyList = service.GetCurrencyList();
                ViewBag.TimeZoneList = service.GetTimeZoneList();

            }

            List<CountryMaster> country = new List<CountryMaster>();
            country = service.GetCountryDetail(countryId);
            //var data = Json(country, JsonRequestBehavior.AllowGet);
            //return Json(country, JsonRequestBehavior.AllowGet);
            return Json(new { country = country });
        }

        [HttpGet]
        public string DeleteCountry(int Id, int userId)
        {
            string strReturn = "Some error occured.";

            if (Id > 0)
            {
                int result = service.DeleteCountry(Id, userId);

                if (result == (int)StatusMessageCode.StatusCode.Success)
                    strReturn = "Your record deleted.";
                else if (result == (int)StatusMessageCode.StatusCode.NotAccess)
                    strReturn = "you have not access to delete this record.";
                else
                    strReturn = "Some error occured.";
            }
            return strReturn;

        }

        #endregion


        #region Currency Master
        [HttpGet]
        public IActionResult Currency()
        {
            VMUserDetail vMUserDetail = new VMUserDetail();
            var value = HttpContext.Session.GetString("LoggedUserInfo");
            vMUserDetail = JsonConvert.DeserializeObject<VMUserDetail>(value);
            if (vMUserDetail != null)
            {
                int id = vMUserDetail.Id;
                int countryId = vMUserDetail.CountryId;
                string usertype = vMUserDetail.UserType;
                string usersubtype = vMUserDetail.UserSubType;

                ViewBag.CountryList = service.GetCountryList(id, countryId, usertype, usersubtype);
                ViewBag.CurrencyList = service.GetCurrencyList();

            }


            return View();
        }

        [HttpPost]
        public string SaveCurrency(IFormCollection formcollection, int userId)
        {
            string strReturn = "Some error occured.";
            List<CountryMaster> lstCountry = new List<CountryMaster>();
            VMUserDetail vMUserDetail = new VMUserDetail();
            var value = HttpContext.Session.GetString("LoggedUserInfo");
            vMUserDetail = JsonConvert.DeserializeObject<VMUserDetail>(value);

            int id = vMUserDetail.Id;
            int countryId = vMUserDetail.CountryId;
            string usertype = vMUserDetail.UserType;
            string usersubtype = vMUserDetail.UserSubType;
            lstCountry = service.GetCountryList(id, countryId, usertype, usersubtype);
            ViewBag.CountryList = lstCountry;

            if (formcollection != null)
            {
                if (ModelState.IsValid)
                {
                    Currency obj = new Currency();
                    obj.Id = Convert.ToInt32(formcollection["Id"]);
                    obj.CurrencyName = Convert.ToString(formcollection["CurrencyName"]);
                    obj.CurrencyCode = !string.IsNullOrEmpty(formcollection["CurrencyCode"]) ? formcollection["CurrencyCode"] : formcollection["CurrencyCode"];
                    obj.CountryId = countryId;
                    obj.Remarks = Convert.ToString(formcollection["Remarks"]);

                    int result = (int)StatusMessageCode.StatusCode.Error;
                    if (Convert.ToInt32(formcollection["Id"]) == 0)
                            result = service.SaveCurrency(obj, userId);
                    else
                        result = service.UpdateCurrency(obj, userId);

                    if (result == (int)StatusMessageCode.StatusCode.Success)
                        strReturn = "success";
                    else if (result == (int)StatusMessageCode.StatusCode.AlreadyExist)
                        strReturn = "This currency  already exist.";
                    else
                        strReturn = "Some error occured.";
                }
            }

            return strReturn;

        }
        [HttpGet]
        public JsonResult GetCurrencyDetail(int currencyId)
        {
            VMUserDetail vMUserDetail = new VMUserDetail();
            var value = HttpContext.Session.GetString("LoggedUserInfo");
            vMUserDetail = JsonConvert.DeserializeObject<VMUserDetail>(value);
            if (vMUserDetail != null)
            {
                int id = vMUserDetail.Id;
                int countryId = vMUserDetail.CountryId;
                string usertype = vMUserDetail.UserType;
                string usersubtype = vMUserDetail.UserSubType;

                ViewBag.CountryList = service.GetCountryList(id, countryId, usertype, usersubtype);

            }
            List<Currency> currency = new List<Currency>();
            currency = service.GetCurrencyDetail(currencyId);
            return Json(new { currency = currency});
        }

        [HttpGet]
        public string DeleteCurrency(int Id, int userId)
        {
            string strReturn = "Some error occured.";

            if (Id > 0)
            {   
                int result = service.DeleteCurrency(Id, userId);

                if (result == (int)StatusMessageCode.StatusCode.Success)
                    strReturn = "Your record deleted.";
                else if (result == (int)StatusMessageCode.StatusCode.NotAccess)
                    strReturn = "you have not access to delete this record.";
                else
                    strReturn = "Some error occured.";
            }
            return strReturn;

        }

        #endregion

    }
}
