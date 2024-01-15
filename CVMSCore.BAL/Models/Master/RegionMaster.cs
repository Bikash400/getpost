using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMSCore.BAL.Models.Master
{
    public class RegionMaster
    {
        public int RegionID { get; set; }
        public string ID { get; set; } = string.Empty;
        public string RegionCode { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public bool Status { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public int SpaceID { get; set; }
        public int VersionNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedId { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
    }

    public class VMRegionMaster
    {
        public string RegionID { get; set; } = string.Empty;
        public string RegionCode { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
        public int SpaceID { get; set; }
    }
}
