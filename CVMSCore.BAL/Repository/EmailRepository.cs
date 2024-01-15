using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CVMSCore.BAL.Common;

namespace CVMSCore.BAL.Repository
{
    public class EmailRepository
    {
        private SqlConnection _conn;
        private LogHandler log;
        private string errorMethodRoute = "OMMS.BAL.Repository.EmailRepository";
        private DapperConnection dapper = new DapperConnection(ConnectionFile.Connection_ANTSDB);

        private static string fromMailAddress;
        private static string fromSenderName;
        private static string fromMailPwd;
        private static string hostType;
        public string GetEmailHtml(object UserType, object UserSubType, string EmailType)
        {
            string emailHtml = "";
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add(nameof(UserType), (object)UserType, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add(nameof(UserSubType), (object)UserSubType, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add(nameof(EmailType), (object)EmailType, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                _conn = dapper.GetConnection();
                DapperConnection.OpenConnection(_conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                emailHtml = SqlMapper.ExecuteScalar((IDbConnection)conn, "OMMS_GetMstEmailSend", (object)dynamicParameters2, (IDbTransaction)null, nullable2, nullable3).ToString();
                DapperConnection.CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                this.log = new LogHandler();
                this.log.WriteErrorLog(ex, this.errorMethodRoute, "GetEmailHtml");
            }
            return emailHtml;
        }

        public bool SendEmail(string ToMails, string CCs, string BCCs, string mailSubject, string mailMessage)
        {
            bool IsSend = false;
            try
            {
                if (!string.IsNullOrEmpty(ToMails))
                {
                    MailMessage msg = new MailMessage();
                    SmtpClient smtp = new SmtpClient();

                    msg.From = new MailAddress(fromMailAddress, fromSenderName);
                    msg.To.Add(new MailAddress(ToMails));
                    if (!string.IsNullOrEmpty(CCs))
                        msg.CC.Add(CCs);
                    if (!string.IsNullOrEmpty(CCs))
                        msg.Bcc.Add(BCCs);
                    msg.Subject = mailSubject;
                    msg.IsBodyHtml = true; //to make message body as html  
                    msg.Body = mailMessage;

                    smtp.Port = 587;
                    smtp.Host = hostType;//"smtp.gmail.com"; //for gmail host  
                    smtp.EnableSsl = false;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(fromMailAddress, fromMailPwd);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(msg);

                    IsSend = true;
                }
            }
            catch (Exception ex)
            {
                this.log = new LogHandler();
                this.log.WriteErrorLog(ex, this.errorMethodRoute, "SendEmail");
            }

            return IsSend;
        }

        public bool SendEmailMultipleToEmails(string ToMails, string CCs, string BCCs, string mailSubject, string mailMessage, IFormFile[] uploadedFile)
        {
            bool IsSend = false;
            try
            {
                if (!string.IsNullOrEmpty(ToMails))
                {
                    MailMessage msg = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    msg.From = new MailAddress(fromMailAddress, fromSenderName);
                    foreach (var address in ToMails.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        msg.To.Add(new MailAddress(address));
                    }
                    //msg.To.Add(new MailAddress(ToMails));
                    if (!string.IsNullOrEmpty(CCs))
                        msg.CC.Add(CCs);
                    if (!string.IsNullOrEmpty(CCs))
                        msg.Bcc.Add(BCCs);
                    msg.Subject = mailSubject;
                    msg.IsBodyHtml = true; //to make message body as html  
                    msg.Body = mailMessage;

                    //System.Net.Mail.Attachment attachment;
                    if (uploadedFile != null)
                    {
                        for (int i = 0; i < uploadedFile.Length; i++)
                        {
                            string fileName = Path.GetFileName(uploadedFile[i].FileName);
                            msg.Attachments.Add(new Attachment(uploadedFile[i].OpenReadStream(), fileName));
                        }
                    }

                    smtp.Port = 587;
                    smtp.Host = hostType;//"smtp.gmail.com"; //for gmail host  
                    smtp.EnableSsl = false;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(fromMailAddress, fromMailPwd);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(msg);

                    IsSend = true;
                }
            }
            catch (Exception ex)
            {
                this.log = new LogHandler();
                this.log.WriteErrorLog(ex, this.errorMethodRoute, "SendEmailMultipleToEmails");
            }

            return IsSend;
        }

        public bool SendEmailWithFromMail(string FromMail, string FromName, string ToMails, string CCs, string BCCs, string mailSubject, string mailMessage)
        {
            bool IsSend = false;
            try
            {
                if (!string.IsNullOrEmpty(ToMails))
                {
                    MailMessage msg = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    msg.From = new MailAddress(fromMailAddress, fromSenderName);
                    foreach (var address in ToMails.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        msg.To.Add(new MailAddress(address));
                    }
                    //msg.To.Add(new MailAddress(ToMails));
                    if (!string.IsNullOrEmpty(CCs))
                        msg.CC.Add(CCs);
                    if (!string.IsNullOrEmpty(CCs))
                        msg.Bcc.Add(BCCs);
                    msg.Subject = mailSubject;
                    msg.IsBodyHtml = true; //to make message body as html  
                    msg.Body = mailMessage;

                    smtp.Port = 587;
                    smtp.Host = hostType;//"smtp.gmail.com"; //for gmail host  
                    smtp.EnableSsl = false;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(fromMailAddress, fromMailPwd);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(msg);

                    IsSend = true;
                }
            }
            catch (Exception ex)
            {
                this.log = new LogHandler();
                this.log.WriteErrorLog(ex, this.errorMethodRoute, "SendEmail");
            }

            return IsSend;
        }

        public string TracEmailHtmlSend(int ID, string EmailHTml, string SendTo, string SendCC, string SendBCC, int SendBy, string UserType, string UserSubType, int CreatedBy)
        {
            string str = "";
            try
            {
                DynamicParameters dynamicParameters1 = new DynamicParameters();
                dynamicParameters1.Add(nameof(ID), (object)ID, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add(nameof(EmailHTml), (object)EmailHTml, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add(nameof(SendTo), (object)SendTo, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add(nameof(SendCC), (object)SendCC, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add(nameof(SendBCC), (object)SendBCC, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add(nameof(SendBy), (object)SendBy, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("SendUserType", (object)UserType, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add("SendUserSubType", (object)UserSubType, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                dynamicParameters1.Add(nameof(CreatedBy), (object)CreatedBy, new DbType?(), new ParameterDirection?(), new int?(), new byte?(), new byte?());
                _conn = dapper.GetConnection();
                DapperConnection.OpenConnection(_conn);
                SqlConnection conn = this._conn;
                DynamicParameters dynamicParameters2 = dynamicParameters1;
                CommandType? nullable1 = new CommandType?(CommandType.StoredProcedure);
                int? nullable2 = new int?();
                CommandType? nullable3 = nullable1;
                str = SqlMapper.ExecuteScalar((IDbConnection)conn, "OMMS_SaveUpdateDtlEmailSend", (object)dynamicParameters2, (IDbTransaction)null, nullable2, nullable3).ToString();
                DapperConnection.CloseConnection(_conn);
            }
            catch (Exception ex)
            {
                this.log = new LogHandler();
                this.log.WriteErrorLog(ex, this.errorMethodRoute, "TracEmailHtmlSend");
            }
            return str;
        }
    }
}
