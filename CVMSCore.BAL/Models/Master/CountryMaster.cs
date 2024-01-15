using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMSCore.BAL.Models.Master
{
    public class CountryMaster
    {
        public int CountryID { get; set; }
        public string CountryCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int RegionID { get; set; }
        public bool Status { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public int SpaceID { get; set; }
        public int VersionNumber { get; set; }
        public decimal offset { get; set; }
        public string Region { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public int CurrencyId { get; set; }
        public string TimeZone { get; set; } = string.Empty;
        public int TimeZoneId { get; set; }
        public string EncryptCountryId { get; set; } = string.Empty;

    }

    public class VMCountryMaster
    {
        public int CountryID { get; set; }
        public string CountryCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int RegionID { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public int CurrencyId { get; set; }
        public int TimeZoneId { get; set; }
    }
}
