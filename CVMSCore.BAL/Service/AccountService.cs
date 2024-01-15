using CVMSCore.BAL.Common;
using CVMSCore.BAL.Models.Master;
using CVMSCore.BAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CVMSCore.BAL.Service
{
    public class AccountService
    {
        private AccountRepository repo = new AccountRepository();

        public int ResetUserPassword(string email, string password, string strUserToken)
        {
            int num = 102;
            try
            {
                num = this.repo.ResetUserPassword(email, password, 0, strUserToken);
            }
            catch
            {
            }
            return num;
        }

        public VMUserDetail checkUserExist(string userName, string userPassword, out string Rtxt)
        {
            string Statustxt = "";
            Rtxt = "";
            VMUserDetail vmUserDetail = (VMUserDetail)null;
            UserDetail userDetail1 = (UserDetail)null;
            try
            {
                userDetail1 = new UserDetail();
                UserDetail userDetail2 = this.repo.checkUserExist(userName, userPassword, out Statustxt);
                Rtxt = Statustxt;
                if (userDetail2 != null)
                {
                    vmUserDetail = new VMUserDetail();
                    vmUserDetail.Id = userDetail2.Id;
                    vmUserDetail.UserName = userDetail2.UserName;
                    vmUserDetail.UserType = userDetail2.UserType;
                    vmUserDetail.UserSubType = userDetail2.UserSubType;
                    vmUserDetail.CountryId = userDetail2.CountryId;
                    vmUserDetail.Country = userDetail2.Country;
                    vmUserDetail.CompanyId = userDetail2.CompanyId;
                    vmUserDetail.RegionId = userDetail2.RegionId;
                    vmUserDetail.LoggedInName = userDetail2.LoggedInName;
                }
            }
            catch
            {
            }
            return vmUserDetail;
        }

        public string ChangePasswordCode(string email, string userToken)
        {
            string str = string.Empty;
            try
            {
                str = this.repo.ChangePasswordCode(email, userToken);
            }
            catch
            {
            }
            return str;
        }

        public List<Modules> GetAllModule()
        {
            List<Modules> lstModule = (List<Modules>)null;
            List<Modules> lstUpdateModule = (List<Modules>)null;
            try
            {
                lstModule = new List<Modules>();
                lstModule = this.repo.GetAllModule(0);
                if (lstModule != null)
                {
                    lstModule.Select(x => { x.strModuleId = System.Convert.ToString(Security.GetEncryptString(System.Convert.ToString(x.Id).Trim())); return x; }).ToList();
                    
                }
            }
            catch
            {
            }
            return lstModule;
        }

        public int CheckUserValidWithToken(string email, string strUserToken)
        {
            int num = 102;
            UserDetail userDetail1 = new UserDetail();
            try
            {
                UserDetail userDetail2 = this.repo.CheckUserValidWithToken(email, strUserToken);
                if (userDetail2 != null)
                    num = !userDetail2.IsTokenActive ? 104 : ((DateTime.Now - userDetail2.TokenCreatedDate).TotalMinutes >= 2882.0 ? 107 : 101);
            }
            catch (Exception ex)
            {
            }
            return num;
        }

    }
}
