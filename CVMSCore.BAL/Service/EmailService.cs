using CVMSCore.BAL.Models.Master;
using CVMSCore.BAL.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMSCore.BAL.Service
{
    public class EmailService
    {
        private string errorMethodRoute = "CVMS.Service.SuperAdminEmailService";
        private string hostAddress;
        EmailRepository _IEmailRepository = new EmailRepository();


        #region Email send when company create vendor
        //public bool SendMailForCompanyVendorCreated(VendorMaster vendorMaster, string strUserToken, string httpcontent)
        //{
        //    bool isSend = false;
        //    try
        //    {
        //        string strMailBody = string.Empty;
        //        strMailBody = MailBodyForCompanyVendorCreated(vendorMaster, strUserToken, httpcontent);
        //        if (!string.IsNullOrEmpty(strMailBody))
        //            isSend = _IEmailRepository.SendEmail(vendorMaster.EmailAddress, string.Empty, string.Empty, "Company Vendor Created", strMailBody);
        //    }
        //    catch (Exception ex)
        //    {
        //        //WriteErrorLog(ex, errorMethodRoute, nameof(SendMailForCompanyVendorCreated));
        //    }

        //    return isSend;
        //}


        //public string MailBodyForCompanyVendorCreated(VendorMaster vendorMaster, string strUserToken, string httpcontent)
        //{
        //    string str = string.Empty;
        //    try
        //    {

        //        using (StreamReader streamReader = new StreamReader(httpcontent)) /*HttpContext.Current.Server.MapPath("~/EmailTemplates/CompanyCreated.html"*/
        //            str = streamReader.ReadToEnd();
        //        string newValue = hostAddress + "/Account/ResetPassword?token=" + httpcontent;
        //        str = str.Replace("[companyname]", vendorMaster.Vendor);
        //        str = str.Replace("[email]", vendorMaster.EmailAddress);
        //        str = str.Replace("[setpasswordlink]", newValue);
        //        str = str.Replace("[LogoImage]", Convert.ToString(hostAddress + "LogoImageForMailBody"));

        //    }
        //    catch (Exception ex)
        //    {
        //        //WriteErrorLog(ex, errorMethodRoute, nameof(MailBodyForCompanyVendorCreated));   
        //    }

        //    return str;
        //}

        #endregion

        #region Change password email

        public bool SendMailForChangePassword(string email, string strUserToken)
        {
            bool isSend = false;
            try
            {
                string strMailBody = string.Empty;
                strMailBody = MailBodyForChangePassword(email, strUserToken);
                if (!string.IsNullOrEmpty(strMailBody))
                    isSend = _IEmailRepository.SendEmail(email, string.Empty, string.Empty, "Change password", strMailBody);
            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendMailForChangePassword));
            }

            return isSend;
        }

        public string MailBodyForChangePassword(string email, string strUserToken)
        {
            string strMailBody = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(Path.Combine("~/EmailTemplates/ChangePasswordTemplate.html")))
                {
                    strMailBody = reader.ReadToEnd();
                }

                string setPasswordLink = ConfigurationManager.AppSettings["hostAddress"].ToString() + "/Account/ResetPassword?token=" + strUserToken;
                // strMailBody = strMailBody.Replace("[email]", email);
                strMailBody = strMailBody.Replace("[setpasswordlink]", setPasswordLink);
                strMailBody = strMailBody.Replace("[LogoImage]", Convert.ToString(ConfigurationManager.AppSettings["LogoImageForMailBody"]));

            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(MailBodyForChangePassword));
            }

            return strMailBody;
        }

        #endregion
        public string convertColors_HTML_KML(string htmlColor)
        {
            List<string> result = new List<string>(System.Text.RegularExpressions.Regex.Split(htmlColor.Replace("#", ""), @"(?<=\G.{2})", System.Text.RegularExpressions.RegexOptions.Singleline));

            return "FF" + result[2] + result[1] + result[0];
        }
        //---------------------usercreation cr
        public bool SendMailForUserCreation(string email, string strUserToken)
        {


            bool isSend = false;
            try
            {
                VMUserDetail vMUserDetail = new VMUserDetail();
                //vMUserDetail = (VMUserDetail)HttpContext.Session.GetString["LoggedUserInfo"];
                //var value = HttpContext.Session.GetString("loginSession");
                //vMUserDetail = JsonConvert.DeserializeObject<VMUserDetail>(value);
                if (vMUserDetail.Id > 0)
                {
                    string strMailBody = string.Empty;
                    strMailBody = MailBodyForUserCreation(vMUserDetail.UserType, vMUserDetail.UserSubType, email, strUserToken);
                    if (!string.IsNullOrEmpty(strMailBody))
                        isSend = _IEmailRepository.SendEmail(email, string.Empty, string.Empty, "UserCreation", strMailBody);

                    if (isSend == true)
                        strMailBody = _IEmailRepository.TracEmailHtmlSend(0, strMailBody, email, "", "", vMUserDetail.Id, vMUserDetail.UserType, vMUserDetail.UserSubType, vMUserDetail.Id);
                }
            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendMailForUserCreation));
            }

            return isSend;
        }

        public string MailBodyForUserCreation(string UserType, string UserSubType, string email, string strUserToken)
        {
            string strMailBody = string.Empty;
            try
            {

                strMailBody = _IEmailRepository.GetEmailHtml(UserType, UserSubType, "UserCreation");
                string setPasswordLink = ConfigurationManager.AppSettings["hostAddress"].ToString() + "/Account/ResetPassword?token=" + strUserToken;
                strMailBody = strMailBody.Replace("[email]", email);
                strMailBody = strMailBody.Replace("[setpasswordlink]", setPasswordLink);
                strMailBody = strMailBody.Replace("[LogoImage]", Convert.ToString(ConfigurationManager.AppSettings["LogoImageForMailBody"]));

            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(MailBodyForUserCreation));
            }

            return strMailBody;
        }

        public bool SendMailForProjectDelete(string EmailId, string Project, string Vertical)
        {
            bool isSend = false;
            try
            {
                string strMailBody = string.Empty;
                VMUserDetail vMUserDetail = new VMUserDetail();
                //vMUserDetail = (VMUserDetail)HttpContext.Current.Session["LoggedUserInfo"];
                if (vMUserDetail.Id > 0)
                {
                    strMailBody = MailBodyForProjectDelete(EmailId, Project, Vertical);
                    if (!string.IsNullOrEmpty(strMailBody))
                        isSend = _IEmailRepository.SendEmail(EmailId, string.Empty, string.Empty, "Project Delete", strMailBody);

                    if (isSend == true)
                        strMailBody = _IEmailRepository.TracEmailHtmlSend(0, strMailBody, EmailId, "", "", vMUserDetail.Id, vMUserDetail.UserType, vMUserDetail.UserSubType, vMUserDetail.Id);
                }
            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendMailForProjectDelete));
            }

            return isSend;
        }

        public bool SendMailForProjectAddEdit(string EmailId, string Project)
        {
            bool isSend = false;
            try
            {
                string strMailBody = string.Empty;
                VMUserDetail vMUserDetail = new VMUserDetail();
                //vMUserDetail = (VMUserDetail)HttpContext.Current.Session["LoggedUserInfo"];
                if (vMUserDetail.Id > 0)
                {
                    strMailBody = MailBodyForProjectAddEdit(EmailId, Project);
                    if (!string.IsNullOrEmpty(strMailBody))
                        isSend = _IEmailRepository.SendEmail(EmailId, string.Empty, string.Empty, "Project Details Add/Edit", strMailBody);

                    if (isSend == true)
                        strMailBody = _IEmailRepository.TracEmailHtmlSend(0, strMailBody, EmailId, "", "", vMUserDetail.Id, vMUserDetail.UserType, vMUserDetail.UserSubType, vMUserDetail.Id);
                }
            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendMailForProjectAddEdit));
            }

            return isSend;
        }

        public string MailBodyForProjectDelete(string EmailId, string Project, string Vertical)
        {
            string strMailBody = string.Empty;
            try
            {
                VMUserDetail vMUserDetail = new VMUserDetail();
                // vMUserDetail = (VMUserDetail)HttpContext.Current.Session["LoggedUserInfo"];
                if (vMUserDetail.Id > 0)
                {
                    strMailBody = _IEmailRepository.GetEmailHtml(vMUserDetail.UserType, vMUserDetail.UserSubType, "ProjectDelete");
                    strMailBody = strMailBody.Replace("[ProjectName]", Project);
                    strMailBody = strMailBody.Replace("[email]", Vertical);
                    strMailBody = strMailBody.Replace("[LogoImage]", Convert.ToString(ConfigurationManager.AppSettings["LogoImageForMailBody"]));
                }
            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(MailBodyForProjectDelete));
            }

            return strMailBody;
        }

        public string MailBodyForProjectAddEdit(string EmailId, string Project)
        {
            string strMailBody = string.Empty;
            try
            {
                VMUserDetail vMUserDetail = new VMUserDetail();
                //vMUserDetail = (VMUserDetail)HttpContext.Current.Session["LoggedUserInfo"];
                if (vMUserDetail.Id > 0)
                {
                    strMailBody = _IEmailRepository.GetEmailHtml(vMUserDetail.UserType, vMUserDetail.UserSubType, "ProjectAdd");
                    strMailBody = strMailBody.Replace("[ProjectName]", Project);
                    strMailBody = strMailBody.Replace("[email]", EmailId);
                    strMailBody = strMailBody.Replace("[LogoImage]", Convert.ToString(ConfigurationManager.AppSettings["LogoImageForMailBody"]));
                }
            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(MailBodyForProjectAddEdit));
            }

            return strMailBody;
        }

        #region Employee Create Project To Parent

        public bool SendMailForEmployeeCreateProjectToParent(string email, string employeeName, string projectName)
        {
            bool isSend = false;
            try
            {
                string strMailBody = string.Empty;
                strMailBody = MailBodyForEmployeeCreateProjectToParent(employeeName, projectName);
                if (!string.IsNullOrEmpty(strMailBody))
                    isSend = _IEmailRepository.SendEmail(email, string.Empty, string.Empty, "Project Created By Employee", strMailBody);
            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendMailForEmployeeCreateProjectToParent));
            }

            return isSend;
        }

        public string MailBodyForEmployeeCreateProjectToParent(string employeeName, string projectName)
        {
            string strMailBody = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(Path.Combine("~/EmailTemplates/ForEmployeeCreateProjectToParent.html")))
                {
                    strMailBody = reader.ReadToEnd();
                }
                strMailBody = strMailBody.Replace("[projectname]", projectName);
                strMailBody = strMailBody.Replace("[employeename]", employeeName);
                strMailBody = strMailBody.Replace("[LogoImage]", Convert.ToString(ConfigurationManager.AppSettings["LogoImageForMailBody"]));

            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(MailBodyForEmployeeCreateProjectToParent));
            }

            return strMailBody;
        }

        #endregion

        #region Change password email

        public bool SendlinkBasedOnProject(string EmployeeEmail, string EmployeeName, string ProjectCoordEmial, string ProjectManagerEmail)
        {
            string toMails = string.Empty;
            string[] arrCCmails = new string[3];
            if (!string.IsNullOrEmpty(ProjectCoordEmial))
                arrCCmails[0] = ProjectCoordEmial;
            if (!string.IsNullOrEmpty(ProjectManagerEmail))
                arrCCmails[1] = ProjectManagerEmail;
            if (!string.IsNullOrEmpty(EmployeeEmail))
                arrCCmails[2] = EmployeeEmail;

            if (arrCCmails != null)
                toMails = string.Join(";", arrCCmails);

            bool isSend = false;
            try
            {
                if (arrCCmails != null)
                {
                    string strMailBody = string.Empty;
                    strMailBody = SendlinkBasedOnProject(EmployeeEmail, "");
                    if (!string.IsNullOrEmpty(strMailBody))
                        isSend = _IEmailRepository.SendEmailWithFromMail(EmployeeEmail, EmployeeName, toMails, string.Empty, string.Empty, "Drive Status Report Link", strMailBody);
                }
            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendlinkBasedOnProject));
            }

            return isSend;
        }

        public string SendlinkBasedOnProject(string email, string strUserToken)
        {
            string strMailBody = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(Path.Combine("~/EmailTemplates/SendDriveStatusReportLink.html")))
                {
                    strMailBody = reader.ReadToEnd();
                }

                string SendLink = ConfigurationManager.AppSettings["SendDriveStatusReportLink"].ToString();
                // strMailBody = strMailBody.Replace("[email]", email);
                strMailBody = strMailBody.Replace("[SendLink]", SendLink);
                strMailBody = strMailBody.Replace("[LogoImage]", Convert.ToString(ConfigurationManager.AppSettings["LogoImageForMailBody"]));

            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendlinkBasedOnProject));
            }

            return strMailBody;
        }

        #endregion

        #region send report link to project customer

        public bool SendReportlinkToCustomer(string[] CustomerEmail, string ReportName, string ReportLink, IFormFile[] uploadedFile)
        {
            bool isSend = false;
            try
            {
                string toMails = string.Empty;

                if (CustomerEmail != null)
                    toMails = string.Join(";", CustomerEmail);

                if (!string.IsNullOrEmpty(toMails))
                {
                    string strMailBody = string.Empty;
                    strMailBody = SendReportlinkToCustomerMailBody(ReportName, ReportLink);
                    if (!string.IsNullOrEmpty(strMailBody))
                        isSend = _IEmailRepository.SendEmailMultipleToEmails(toMails, string.Empty, string.Empty, ReportName, strMailBody, uploadedFile);
                    //isSend = MailSystem.SendEmail(toMails, string.Empty, string.Empty, ReportName, strMailBody);
                }
            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendReportlinkToCustomer));
            }

            return isSend;
        }

        public string SendReportlinkToCustomerMailBody(string ReportName, string ReportLink)
        {
            string strMailBody = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(Path.Combine("~/EmailTemplates/ReportLinkSendToCustomer.html")))
                {
                    strMailBody = reader.ReadToEnd();
                }

                //string SendLink = ConfigurationManager.AppSettings["SendDriveStatusReportLink"].ToString();
                //// strMailBody = strMailBody.Replace("[email]", email);
                strMailBody = strMailBody.Replace("[ReportName]", ReportName);
                strMailBody = strMailBody.Replace("[ReportLink]", ReportLink);
                strMailBody = strMailBody.Replace("[LogoImage]", Convert.ToString(ConfigurationManager.AppSettings["LogoImageForMailBody"]));

            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendReportlinkToCustomerMailBody));
            }

            return strMailBody;
        }

        #endregion

        #region send report link to project coordinater and manager

        public bool SendReportlinkToInternal(string[] internalEmails, string ReportName, string ReportLink, IFormFile[] uploadedFile)
        {
            string toMails = string.Empty;

            if (internalEmails != null)
                toMails = string.Join(";", internalEmails);

            bool isSend = false;
            try
            {
                if (!string.IsNullOrEmpty(toMails))
                {
                    string strMailBody = string.Empty;
                    strMailBody = SendReportlinkToInternalMailBody(ReportName, ReportLink);
                    if (!string.IsNullOrEmpty(strMailBody))
                        isSend = _IEmailRepository.SendEmailMultipleToEmails(toMails, string.Empty, string.Empty, ReportName, strMailBody, uploadedFile);
                }
            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendReportlinkToInternal));
            }

            return isSend;
        }

        public bool SendReportlinkToRC(string[] internalEmails, string ReportName, string ReportLink, IFormFile[] uploadedFile)
        {
            string toMails = string.Empty;

            if (internalEmails != null)
                toMails = string.Join(";", internalEmails);

            bool isSend = false;
            try
            {
                if (!string.IsNullOrEmpty(toMails))
                {
                    string strMailBody = string.Empty;
                    strMailBody = SendReportlinkRctoRcMailBody(ReportName, ReportLink);
                    if (!string.IsNullOrEmpty(strMailBody))
                        isSend = _IEmailRepository.SendEmailMultipleToEmails(toMails, string.Empty, string.Empty, ReportName, strMailBody, uploadedFile);
                }
            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendReportlinkToRC));
            }

            return isSend;
        }

        public string SendReportlinkToInternalMailBody(string ReportName, string ReportLink)
        {
            string strMailBody = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(Path.Combine("~/EmailTemplates/ReportLinkSendToInternal.html")))
                {
                    strMailBody = reader.ReadToEnd();
                }

                strMailBody = strMailBody.Replace("[ReportName]", ReportName);
                strMailBody = strMailBody.Replace("[ReportLink]", ReportLink);
                strMailBody = strMailBody.Replace("[LogoImage]", Convert.ToString(ConfigurationManager.AppSettings["LogoImageForMailBody"]));

            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendReportlinkToInternalMailBody));
            }

            return strMailBody;
        }

        public string SendReportlinkRctoRcMailBody(string ReportName, string ReportLink)
        {
            string strMailBody = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(Path.Combine("~/EmailTemplates/rctorc.html")))
                {
                    strMailBody = reader.ReadToEnd();
                }

                strMailBody = strMailBody.Replace("[ReportName]", ReportName);
                strMailBody = strMailBody.Replace("[ReportLink]", ReportLink);
                strMailBody = strMailBody.Replace("[LogoImage]", Convert.ToString(ConfigurationManager.AppSettings["LogoImageForMailBody"]));

            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendReportlinkRctoRcMailBody));
            }

            return strMailBody;
        }
        #endregion

        #region send mail when client add any remark to project coordinater and manager

        public bool SendUpdateStatusInfoToInternalApp(string[] internalEmails, string ReportName, string ReportLink, string Remark, string StatusChangedBy, IFormFile[] uploadedFile)
        {
            string toMails = string.Empty;

            if (internalEmails != null)
                toMails = string.Join(";", internalEmails);

            bool isSend = false;
            try
            {
                if (!string.IsNullOrEmpty(toMails))
                {
                    string strMailBody = string.Empty;
                    strMailBody = AppSendUpdateStatusInfoToInternalMailBody(ReportName, ReportLink, Remark, StatusChangedBy);
                    if (!string.IsNullOrEmpty(strMailBody))
                        isSend = _IEmailRepository.SendEmailMultipleToEmails(toMails, string.Empty, string.Empty, ReportName, strMailBody, uploadedFile);
                }
            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendUpdateStatusInfoToInternalApp));
            }

            return isSend;
        }

        public bool SendUpdateStatusInfoToInternalReject(string[] internalEmails, string ReportName, string ReportLink, string Remark, string StatusChangedBy, IFormFile[] uploadedFile)
        {
            string toMails = string.Empty;

            if (internalEmails != null)
                toMails = string.Join(";", internalEmails);

            bool isSend = false;
            try
            {
                if (!string.IsNullOrEmpty(toMails))
                {
                    string strMailBody = string.Empty;
                    strMailBody = RejAppSendUpdateStatusInfoToInternalMailBody(ReportName, ReportLink, Remark, StatusChangedBy);
                    if (!string.IsNullOrEmpty(strMailBody))
                        isSend = _IEmailRepository.SendEmailMultipleToEmails(toMails, string.Empty, string.Empty, ReportName, strMailBody, uploadedFile);
                }
            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendUpdateStatusInfoToInternalReject));
            }

            return isSend;
        }

        public string AppSendUpdateStatusInfoToInternalMailBody(string ReportName, string ReportLink, string Remark = null, string StatusChangedBy = null)
        {
            string strMailBody = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(Path.Combine("~/EmailTemplates/internalapp.html")))
                {
                    strMailBody = reader.ReadToEnd();
                }

                strMailBody = strMailBody.Replace("[ReportName]", ReportName);
                strMailBody = strMailBody.Replace("[ReportLink]", ReportLink);
                strMailBody = strMailBody.Replace("[Remark]", Remark);
                strMailBody = strMailBody.Replace("[ChangedBy]", StatusChangedBy);
                strMailBody = strMailBody.Replace("[LogoImage]", Convert.ToString(ConfigurationManager.AppSettings["LogoImageForMailBody"]));

            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(AppSendUpdateStatusInfoToInternalMailBody));
            }

            return strMailBody;
        }

        public string RejAppSendUpdateStatusInfoToInternalMailBody(string ReportName, string ReportLink, string Remark = null, string StatusChangedBy = null)
        {
            string strMailBody = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(Path.Combine("~/EmailTemplates/internalreject.html")))
                {
                    strMailBody = reader.ReadToEnd();
                }

                strMailBody = strMailBody.Replace("[ReportName]", ReportName);
                strMailBody = strMailBody.Replace("[ReportLink]", ReportLink);
                strMailBody = strMailBody.Replace("[Remark]", Remark);
                strMailBody = strMailBody.Replace("[ChangedBy]", StatusChangedBy);
                strMailBody = strMailBody.Replace("[LogoImage]", Convert.ToString(ConfigurationManager.AppSettings["LogoImageForMailBody"]));

            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(RejAppSendUpdateStatusInfoToInternalMailBody));
            }

            return strMailBody;
        }

        #endregion

        #region send mail when client add any remark to project coordinater and manager

        public bool SendUpdateStatusInfoToUser(string[] internalEmails, string ReportName, string ReportLink, IFormFile[] uploadedFile)
        {
            string toMails = string.Empty;

            if (internalEmails != null)
                toMails = string.Join(";", internalEmails);

            bool isSend = false;
            try
            {
                if (!string.IsNullOrEmpty(toMails))
                {
                    string strMailBody = string.Empty;
                    strMailBody = SendUpdateStatusInfoToUserMailBody(ReportName, ReportLink);
                    if (!string.IsNullOrEmpty(strMailBody))
                        isSend = _IEmailRepository.SendEmailMultipleToEmails(toMails, string.Empty, string.Empty, ReportName, strMailBody, uploadedFile);
                }
            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendUpdateStatusInfoToUser));
            }

            return isSend;
        }

        public string SendUpdateStatusInfoToUserMailBody(string ReportName, string ReportLink)
        {
            string strMailBody = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(Path.Combine("~/EmailTemplates/ReportStatusUpdateMailToUser.html")))
                {
                    strMailBody = reader.ReadToEnd();
                }

                strMailBody = strMailBody.Replace("[ReportName]", ReportName);
                strMailBody = strMailBody.Replace("[ReportLink]", ReportLink);
                strMailBody = strMailBody.Replace("[LogoImage]", Convert.ToString(ConfigurationManager.AppSettings["LogoImageForMailBody"]));

            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendUpdateStatusInfoToUserMailBody));
            }

            return strMailBody;
        }

        #endregion

        public string RctoRc(string ReportName, string ReportLink, string Remark = null, string StatusChangedBy = null)
        {
            string strMailBody = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(Path.Combine("~/EmailTemplates/rctorc.html")))
                {
                    strMailBody = reader.ReadToEnd();
                }

                strMailBody = strMailBody.Replace("[ReportName]", ReportName);
                strMailBody = strMailBody.Replace("[ReportLink]", ReportLink);
                strMailBody = strMailBody.Replace("[Remark]", Remark);
                strMailBody = strMailBody.Replace("[ChangedBy]", StatusChangedBy);
                strMailBody = strMailBody.Replace("[LogoImage]", Convert.ToString(ConfigurationManager.AppSettings["LogoImageForMailBody"]));

            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendUpdateStatusInfoToUserMailBody));
            }

            return strMailBody;
        }

        public bool SendUpdateStatusInfoToClientApp(string[] internalEmails, string ReportName, string ReportLink, string Remark, string StatusChangedBy, IFormFile[] uploadedFile)
        {
            string toMails = string.Empty;

            if (internalEmails != null)
                toMails = string.Join(";", internalEmails);

            bool isSend = false;
            try
            {
                if (!string.IsNullOrEmpty(toMails))
                {
                    string strMailBody = string.Empty;
                    strMailBody = AppSendUpdateStatusInfoToClientMailBody(ReportName, ReportLink, Remark, StatusChangedBy);
                    if (!string.IsNullOrEmpty(strMailBody))
                        isSend = _IEmailRepository.SendEmailMultipleToEmails(toMails, string.Empty, string.Empty, ReportName, strMailBody, uploadedFile);
                }
            }
            catch (Exception ex)
            {

                //WriteErrorLog(ex, errorMethodRoute, nameof(SendReportlinkToInternal));
            }

            return isSend;
        }

        public bool SendUpdateStatusInfoToClientRejectRC(string[] internalEmails, string ReportName, string ReportLink, string Remark, string StatusChangedBy, IFormFile[] uploadedFile)
        {
            string toMails = string.Empty;

            if (internalEmails != null)
                toMails = string.Join(";", internalEmails);

            bool isSend = false;
            try
            {
                if (!string.IsNullOrEmpty(toMails))
                {
                    string strMailBody = string.Empty;
                    strMailBody = RejectSendUpdateStatusInfoToClientMailBodyRC(ReportName, ReportLink, Remark, StatusChangedBy);
                    if (!string.IsNullOrEmpty(strMailBody))
                        isSend = _IEmailRepository.SendEmailMultipleToEmails(toMails, string.Empty, string.Empty, ReportName, strMailBody, uploadedFile);
                }
            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendReportlinkToInternal));
            }

            return isSend;
        }


        public bool SendUpdateStatusInfoToClientRejectNONRC(string[] internalEmails, string ReportName, string ReportLink, string Remark, string StatusChangedBy, IFormFile[] uploadedFile)
        {
            string toMails = string.Empty;

            if (internalEmails != null)
                toMails = string.Join(";", internalEmails);

            bool isSend = false;
            try
            {
                if (!string.IsNullOrEmpty(toMails))
                {
                    string strMailBody = string.Empty;
                    strMailBody = RejectSendUpdateStatusInfoToClientMailBodyNONRC(ReportName, ReportLink, Remark, StatusChangedBy);
                    if (!string.IsNullOrEmpty(strMailBody))
                        isSend = _IEmailRepository.SendEmailMultipleToEmails(toMails, string.Empty, string.Empty, ReportName, strMailBody, uploadedFile);
                }
            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendReportlinkToInternal));
            }

            return isSend;
        }

        public string AppSendUpdateStatusInfoToClientMailBody(string ReportName, string ReportLink, string Remark = null, string StatusChangedBy = null)
        {
            string strMailBody = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(Path.Combine("~/EmailTemplates/clientapp.html")))
                {
                    strMailBody = reader.ReadToEnd();
                }

                strMailBody = strMailBody.Replace("[ReportName]", ReportName);
                strMailBody = strMailBody.Replace("[ReportLink]", ReportLink);
                strMailBody = strMailBody.Replace("[Remark]", Remark);
                strMailBody = strMailBody.Replace("[ChangedBy]", StatusChangedBy);
                strMailBody = strMailBody.Replace("[LogoImage]", Convert.ToString(ConfigurationManager.AppSettings["LogoImageForMailBody"]));

            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendReportlinkToCustomerMailBody));
            }

            return strMailBody;
        }


        public string RejectSendUpdateStatusInfoToClientMailBodyRC(string ReportName, string ReportLink, string Remark = null, string StatusChangedBy = null)
        {
            string strMailBody = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(Path.Combine("~/EmailTemplates/clientreject.html")))
                {
                    strMailBody = reader.ReadToEnd();
                }

                strMailBody = strMailBody.Replace("[ReportName]", ReportName);
                strMailBody = strMailBody.Replace("[ReportLink]", ReportLink);
                strMailBody = strMailBody.Replace("[Remark]", Remark);
                strMailBody = strMailBody.Replace("[ChangedBy]", StatusChangedBy);
                strMailBody = strMailBody.Replace("[LogoImage]", Convert.ToString(ConfigurationManager.AppSettings["LogoImageForMailBody"]));

            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendReportlinkToCustomerMailBody));
            }

            return strMailBody;
        }

        public string RejectSendUpdateStatusInfoToClientMailBodyNONRC(string ReportName, string ReportLink, string Remark = null, string StatusChangedBy = null)
        {
            string strMailBody = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(Path.Combine("~/EmailTemplates/clrejectmanager.html")))
                {
                    strMailBody = reader.ReadToEnd();
                }

                strMailBody = strMailBody.Replace("[ReportName]", ReportName);
                strMailBody = strMailBody.Replace("[ReportLink]", ReportLink);
                strMailBody = strMailBody.Replace("[Remark]", Remark);
                strMailBody = strMailBody.Replace("[ChangedBy]", StatusChangedBy);
                strMailBody = strMailBody.Replace("[LogoImage]", Convert.ToString(ConfigurationManager.AppSettings["LogoImageForMailBody"]));

            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, nameof(SendReportlinkToCustomerMailBody));
            }

            return strMailBody;
        }


    }
}
