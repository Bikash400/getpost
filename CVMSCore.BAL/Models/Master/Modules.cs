using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMSCore.BAL.Models.Master
{
    public class Modules
    {
        public int Id { get; set; }
        public string strModuleId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleUrl { get; set; }
        public string MImage { get; set; }
        public bool IsDisplay { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Createdby { get; set; }
        public DateTime ModifyDate { get; set; }
        public int ModifyBy { get; set; }
        public string BackgroundColor { get; set; }
    }
}
