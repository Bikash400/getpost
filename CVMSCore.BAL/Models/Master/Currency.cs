using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMSCore.BAL.Models.Master
{
    public class Currency
    {
        public int Id { get; set; }
        public string CurrencyCode { get; set; } = string.Empty;
        public string CurrencyName { get; set; } = string.Empty;
        public bool Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public string CountryName { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
    }

    public class VMCurrency
    {
        public int Id { get; set; }
        [Required]
        public string CurrencyCode { get; set; } 
        [Required]
        public string CurrencyName { get; set; }
        [Required]
        public int CountryId { get; set; }
        public string Remarks { get; set; }
    }
}
