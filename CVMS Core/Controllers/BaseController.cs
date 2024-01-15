using CVMSCore.BAL.Models.HotelManagement;
using CVMSCore.BAL.Models.Master;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace CVMS_Core.Controllers
{
    public class BaseController : Controller
    {
        public LoginViewModel GetUserDetail()
        {
            LoginViewModel vMUserDetail = null;
            List<LoginViewModel> userDetailsList = new List<LoginViewModel>();
            var value = HttpContext.Session.GetString("LoggedUserInfo");
            if (value != null)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    vMUserDetail = new LoginViewModel();
                    vMUserDetail = JsonConvert.DeserializeObject<LoginViewModel>(value);

                }
            }

            return vMUserDetail;
        }

        public object GetUserDetail(string ParamName)
        {
            object returnValue = string.Empty;
            var value = HttpContext.Session.GetString("LoggedUserInfo");
            if (value != null)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    UserDetail vMUserDetail = new UserDetail();
                    vMUserDetail = JsonConvert.DeserializeObject<UserDetail>(value);
                    Type myType = vMUserDetail.GetType();
                    IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                    // var sss= propsx.GetType.)

                    foreach (PropertyInfo prop in props)
                    {
                        if (prop.Name.ToLower() == ParamName.ToLower())
                            returnValue = prop.GetValue(vMUserDetail, null);
                    }
                }
            }
            return returnValue;
        }
    }
}
