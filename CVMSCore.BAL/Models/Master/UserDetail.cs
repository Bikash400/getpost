using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMSCore.BAL.Models.Master
{
    public class UserDetail
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "UserName/Email Is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password Is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password Is required")]
        [Compare("Password", ErrorMessage = "Password name and Confirm Password doesn't match")]
        public string ConfirmPassword { get; set; }
        public string UserType { get; set; }
        public string UserSubType { get; set; }
        public string UserToken { get; set; }
        public DateTime TokenCreatedDate { get; set; }
        public bool IsTokenActive { get; set; }
        public string fkUserId { get; set; }
        public string MasterId { get; set; }
        public string Remark { get; set; }
        public string IpAddress { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public int CountryId { get; set; }

        public int RegionId { get; set; }


        public string Country { get; set; }
        public int CompanyId { get; set; }
        public string LoggedInName { get; set; }
    }

    public class VMUserDetail
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        [Required(ErrorMessage = "UserName/Email Is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password Is required")]
        public string Password { get; set; }
        public string UserToken { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public int CompanyId { get; set; }

        public int RegionId { get; set; }
        public string UserType { get; set; }
        public string UserSubType { get; set; }
        public string LoggedInName { get; set; }
        public string urlQueryString { get; set; }
    }
}
