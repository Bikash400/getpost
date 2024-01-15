using CVMSCore.BAL;
using CVMSCore.BAL.Common;
using CVMSCore.BAL.Models.Master;
using CVMSCore.BAL.Service;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace CVMS_Core.Controllers
{
    public class AccountController : Controller
    {
        AccountService accService = new AccountService();
        EmailService _IEmailService = new EmailService();

        [HttpGet]
        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            string strUserToken = Security.GetOneWayEncryptMD5("PASSWORD");
            VMUserDetail vMUserDetail = new VMUserDetail();
            var value = HttpContext.Session.GetString("LoggedUserInfo");
            if (value == null)
            {
                return View(vMUserDetail);
            }
            return View(vMUserDetail);
        }

        [HttpPost]
        public ActionResult Login(VMUserDetail vMUserDetail)
        {
            try
            {
                string Rststus = "";
                if (vMUserDetail != null)
                {
                    string encryptPassword = string.Empty;
                    if (!string.IsNullOrEmpty(vMUserDetail.Password))
                    {
                        //encryptPassword = Security.GetEncryptString(userDetail.Password.Trim());
                        encryptPassword = Security.GetOneWayEncryptMD5(vMUserDetail.Password.Trim());
                    }

                    if (!string.IsNullOrEmpty(vMUserDetail.UserName) && !string.IsNullOrEmpty(encryptPassword))
                    {
                        vMUserDetail = accService.checkUserExist(vMUserDetail.UserName, encryptPassword, out Rststus);
                        if (vMUserDetail != null)
                        {
                            //HttpContext.Session["LoggedUserInfo"] = vMUserDetail;
                            HttpContext.Session.SetString("LoggedUserInfo", JsonConvert.SerializeObject(vMUserDetail));
                            // return RedirectToAction("Home", "Account");

                            int Id = vMUserDetail.Id;
                            string _userDetail = Security.GetEncryptString(vMUserDetail.UserName.Trim() + "|" + encryptPassword.Trim());
                            // string pass = vMUserDetail.Password;
                            return RedirectToAction("Module", "Account", new { param = _userDetail });
                            // return RedirectToAction("home", "Account");

                            //ViewBag.ReturnMessage = "success";
                            //ModelState.Clear();
                        }
                        else
                            ViewBag.ReturnMessage = Rststus; // "Some error occured.please contact to admin.";
                    }
                    else
                    {
                        ViewBag.ReturnMessage = "You enter wrong Username or email";
                    }
                }
                else
                {
                    ViewBag.ReturnMessage = "1";
                }
            }
            catch (Exception)
            {


            }
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }

        [HttpGet]
        public ActionResult ResetPassword(string token)
        {
            TempData["ResetUrl"] = string.Empty;
            string currentUrl = HttpContext.Request.GetDisplayUrl();
            TempData["ResetUrl"] = currentUrl;
            UserDetail userDetail = new UserDetail();
            if (!string.IsNullOrEmpty(token))
                userDetail.UserToken = token;

            if (Convert.ToString(TempData["ResetPageLoad"]) == "2")
            { }
            else
                TempData["ResetReturnMessage"] = string.Empty;
            return View(userDetail);
        }

        public ActionResult ResetPassword(UserDetail userDetail)
        {
            TempData["ResetReturnMessage"] = string.Empty;
            string currentUrl = Convert.ToString(TempData["ResetUrl"]);
            try
            {
                string userName = string.Empty;
                string userEmail = string.Empty;
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(userDetail.UserToken))
                    {
                        string strToken = Security.GetOneWayEncryptMD5(userDetail.UserName.Trim().ToUpper());

                        #region check token valid  
                        if (strToken == userDetail.UserToken)
                        {
                            userEmail = userDetail.UserName.Trim();
                            if (!string.IsNullOrEmpty(userEmail))
                            {

                                int result = accService.CheckUserValidWithToken(userEmail, userDetail.UserToken);
                                if (result == (int)StatusMessageCode.StatusCode.Success)
                                {
                                    #region Reset password                                       
                                    string encryptPassword = string.Empty;
                                    if (!string.IsNullOrEmpty(userDetail.Password))
                                    {
                                        encryptPassword = Security.GetOneWayEncryptMD5(userDetail.Password.Trim());
                                    }

                                    if (userDetail.UserName == userEmail)
                                    {
                                        if (!string.IsNullOrEmpty(userEmail) && !string.IsNullOrEmpty(encryptPassword))
                                        {
                                            result = accService.ResetUserPassword(userEmail, encryptPassword, userDetail.UserToken);
                                            if (result == -1)
                                            {
                                                return RedirectToAction("Login", "Account");
                                            }
                                            else
                                            {
                                                TempData["ResetReturnMessage"] = "Some error occured.please contact to admin.";
                                                TempData["ResetPageLoad"] = "2";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        TempData["ResetReturnMessage"] = "You enter wrong Username or email";
                                        TempData["ResetPageLoad"] = "2";
                                    }

                                    #endregion
                                }
                                else if (result == (int)StatusMessageCode.StatusCode.Expired)
                                {
                                    TempData["ResetReturnMessage"] = "Your Token expired.please contact to admin.";
                                    TempData["ResetPageLoad"] = "2";
                                    return Redirect(currentUrl);
                                }
                                else if (result == (int)StatusMessageCode.StatusCode.NotExist)
                                {
                                    TempData["ResetReturnMessage"] = "Your Token not exist.please contact to admin.";
                                    TempData["ResetPageLoad"] = "2";
                                    return Redirect(currentUrl);
                                }
                                else
                                {
                                    TempData["ResetReturnMessage"] = "Some error occured.please contact to admin.";
                                    TempData["ResetPageLoad"] = "2";
                                    return Redirect(currentUrl);
                                }

                            }
                        }
                        else
                        {
                            TempData["ResetReturnMessage"] = "You enter wrong Username or email";
                            TempData["ResetPageLoad"] = "2";
                            return Redirect(currentUrl);
                        }

                        #endregion

                    }
                    else
                    {
                        TempData["ResetReturnMessage"] = "Your token not valid.";
                        return Redirect(currentUrl);
                    }
                }
                else
                {
                    
                }
            }
            catch (Exception)
            {

            }
            return Redirect(currentUrl);
        }

        [HttpPost]
        public ActionResult ResetPassword_old(UserDetail userDetail)
        {
            TempData["ResetReturnMessage"] = string.Empty;
            string currentUrl = Convert.ToString(TempData["ResetUrl"]);
            try
            {
                string userName = string.Empty;
                string userEmail = string.Empty;
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(userDetail.UserToken))
                    {
                        string strUserToken = Security.GetDecryptString(userDetail.UserToken);

                        if (!string.IsNullOrEmpty(strUserToken))
                        {
                            string[] arrToken = strUserToken.Split(new string[] { "|@|" }, StringSplitOptions.None);

                            #region check token valid  
                            // string[] arrToken = strUserToken.Split(new string[] { "|@|" }, StringSplitOptions.None);
                            if (arrToken.Length > 2)
                            {
                                userName = arrToken[0].Replace("+", " ");
                                userEmail = arrToken[1];

                                if (!string.IsNullOrEmpty(userEmail))
                                {
                                    userDetail.UserToken = userDetail.UserToken;

                                    int result = accService.CheckUserValidWithToken(userEmail, userDetail.UserToken);
                                    if (result == (int)StatusMessageCode.StatusCode.Success)
                                    {
                                        #region Reset password
                                        if (arrToken.Length > 1)
                                        {
                                            //companyName = arrToken[0].Replace("+", " ");
                                            userEmail = arrToken[1];
                                            string encryptPassword = string.Empty;
                                            if (!string.IsNullOrEmpty(userDetail.Password))
                                            {
                                                encryptPassword = Security.GetOneWayEncryptMD5(userDetail.Password.Trim());
                                            }

                                            if (userDetail.UserName == userEmail)
                                            {
                                                if (!string.IsNullOrEmpty(userEmail) && !string.IsNullOrEmpty(encryptPassword))
                                                {
                                                    result = this.accService.ResetUserPassword(userEmail, encryptPassword, userDetail.UserToken);
                                                    if (result == -1)
                                                    {
                                                        //return View("Login");
                                                        return RedirectToAction("Login", "Account");
                                                    }
                                                    else
                                                    {
                                                        TempData["ResetReturnMessage"] = "Some error occured.please contact to admin.";
                                                        TempData["ResetPageLoad"] = "2";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                TempData["ResetReturnMessage"] = "You enter wrong Username or email";
                                                TempData["ResetPageLoad"] = "2";
                                            }

                                        }
                                        #endregion
                                    }
                                    else if (result == (int)StatusMessageCode.StatusCode.Expired)
                                    {
                                        TempData["ResetReturnMessage"] = "Your Token expired.please contact to admin.";
                                        TempData["ResetPageLoad"] = "2";
                                        return Redirect(currentUrl);
                                    }
                                    else if (result == (int)StatusMessageCode.StatusCode.NotExist)
                                    {
                                        TempData["ResetReturnMessage"] = "Your Token not exist.please contact to admin.";
                                        TempData["ResetPageLoad"] = "2";
                                        return Redirect(currentUrl);
                                    }
                                    else
                                    {
                                        TempData["ResetReturnMessage"] = "Some error occured.please contact to admin.";
                                        TempData["ResetPageLoad"] = "2";
                                        return Redirect(currentUrl);
                                    }

                                }

                            }

                            #endregion


                        }
                    }
                    else
                    {
                        TempData["ResetReturnMessage"] = "Your token not valid.";
                        return Redirect(currentUrl);
                    }
                }
                else
                {
                    // ViewBag.ReturnMessage = "1";
                }
            }
            catch (Exception)
            {

            }
            return Redirect(currentUrl);
            //return View(userDetail);
            //  return RedirectToAction("ResetPassword?token=" + userDetail.UserToken);
        }

        [HttpGet]
        public ActionResult Home()
        {
            VMUserDetail vMUserDetail = new VMUserDetail();
            HttpContext.Session.GetString("LoggedUserInfo");

            if (vMUserDetail != null)
            {
                string usertype = vMUserDetail.UserType.ToLower().Trim();
                string userSubtype = vMUserDetail.UserSubType.ToLower().Trim();
                if (usertype.ToLower() == "superadmin" && userSubtype.ToLower() == "employee")//--6-12-2021
                    return RedirectToAction("Dashboard", "Vendor");//--6-12-2021
                else if (usertype.ToLower() == "superadmin" && userSubtype.ToLower() == "admin")
                    return RedirectToAction("Dashboard", "SuperAdmin");
                else if (usertype.ToLower() == "company" && userSubtype.ToLower() == "admin")
                    return RedirectToAction("ViewCompany", "Company");
                else if (usertype.ToLower() == "vendor" && userSubtype.ToLower() == "admin")
                    return RedirectToAction("Dashboard", "Vendor");
                else if (usertype.ToLower() == "vendor" && userSubtype.ToLower() == "employee")
                    return RedirectToAction("Dashboard", "Vendor");
                else if (usertype.ToLower() == "company" && userSubtype.ToLower() == "employee")
                    return RedirectToAction("ViewCompany", "Company");

                else
                    return RedirectToAction("Login", "Account");
            }
            else
                return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult ChangePassword(UserDetail userDetail)
        {
            return View();
        }

        [HttpPost]
        public string SendChangePasswordCode(string email)
        {
            string returnMessage = "Some error occured";
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    string userToken = string.Empty;
                    userToken = Security.GetOneWayEncryptMD5(email.Trim().ToUpper());
                    returnMessage = accService.ChangePasswordCode(email, userToken);
                    if (returnMessage == "success")
                    {
                        bool mailSend = _IEmailService.SendMailForChangePassword(email, userToken);
                        if (mailSend)
                            returnMessage = "success";
                        else
                            returnMessage = "Mail not send some technical issue.";
                    }
                }
                else
                {
                    ViewBag.ReturnMessage = "Email is required.";
                }
            }
            catch (Exception)
            {

            }
            return returnMessage;
        }

        public ActionResult Module()
        {

            List<Modules> lstModule = new List<Modules>();
            lstModule = accService.GetAllModule();
            ViewBag.Module = lstModule;
            return View();

        }

        [HttpGet]
        public ActionResult LoginModule(string param, string param2)
        {
            string _userDetail = string.Empty;
            string[] IdPass = null;
            string Rstatus = "";
            if (!string.IsNullOrEmpty(param) && !string.IsNullOrEmpty(param2))
            {
                VMUserDetail vMUserDetail = new VMUserDetail();
                HttpContext.Session.GetString("LoggedUserInfo");
                _userDetail = Security.GetDecryptString(param);
                int moduleId = Convert.ToInt32(Security.GetDecryptString(param2));
                // UserId = _userDetail.Split("|");
                IdPass = _userDetail.Split(new char[] { '|' });
                string _Id = IdPass[0];
                string encryptPassword = IdPass[1];
                if (!string.IsNullOrEmpty(_Id) && !string.IsNullOrEmpty(encryptPassword))
                {
                    vMUserDetail = accService.checkUserExist(_Id, encryptPassword, out Rstatus);
                    if (vMUserDetail != null)
                    {
                        vMUserDetail.ModuleId = moduleId;
                        vMUserDetail.urlQueryString = param;
                        HttpContext.Session.SetString("LoggedUserInfo", JsonConvert.SerializeObject(vMUserDetail));
                        return RedirectToAction("Login", "Account");
                    }
                    else
                        ViewBag.ReturnMessage = Rstatus; // "Some error occured.please contact to admin.";
                }
                else
                {
                    ViewBag.ReturnMessage = "You enter wrong Username or email";
                }
                //return RedirectToAction("Home", "Account");

            }
            else
            {
                // return RedirectToAction("Module", "Account",
                return RedirectToAction("Login", "Account");
            }

            return View();

        }

        public ActionResult BackToService()
        {
            VMUserDetail vMUserDetail = new VMUserDetail();
            var value = HttpContext.Session.GetString("LoggedUserInfo");
            vMUserDetail = JsonConvert.DeserializeObject<VMUserDetail>(value);
            //string returnUrl = "";
            try
            {
                if (vMUserDetail != null)
                {
                    string backUrlParameter = vMUserDetail.urlQueryString;
                    //returnUrl = "http://localhost:44378/Login/Module?param=" + backUrlParameter;
                    return RedirectToAction("Module", "Login", new { param = backUrlParameter });
                }
                else
                {
                    //returnUrl = "https://localhost:64741";
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Account");
            }

            //return returnUrl;
        }

    }
}
