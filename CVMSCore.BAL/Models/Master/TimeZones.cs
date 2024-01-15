using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMSCore.BAL.Models.Master
{
    public class TimeZones
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string StandardName { get; set; }
        public string DisplayName { get; set; }
        public string DaylightName { get; set; }
        public string SupportsDaylightSavingTime { get; set; }
        public string BaseUtcOffsetSec { get; set; }
    }
}
