using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMSCore.BAL
{
    public static class ConnectionFile  
    {
        // ANTS_PreProd_new ANTSDBTemp ANTS_PreProd
        public static string Connection_ANTSDB = "Data Source=DESKTOP-JOP2CD4\\SQLEXPRESS;Initial Catalog=Ram;Integrated Security=True";
        //// for server publish
        //   public static string db_ANTSDBTemp = "Data Source=203.122.13.198;Initial Catalog=ANTS_PreProd;User Id=SA;Password=G25@P201301;Integrated Security=True;Trusted_Connection=False;";
        ///// for local
        //public static string db_ANTSDBTemp = "Data Source=203.122.13.198;Initial Catalog=ANTSDBTemp;User Id=SA;Password=G25@P201301;Integrated Security=True;Trusted_Connection=False;";
        // public static string db_ANTSDBTemp = "Data Source=203.122.13.198;Initial Catalog=ANTS_PreProd;User Id=SA;Password=G25@P201301;Integrated Security=True;Trusted_Connection=False;";
        //public static string db_ANTSDBTemp = ConfigurationManager.AppSettings["Connection_ANTSDB"].ToString();

        public static string db_Oracal = "Data Source = (description = (address = (protocol = tcp)(host = 192.168.1.203)(port = 1521))(connect_data = (service_name = orcl))); User ID = lqomms; Password=lqomms;";
    }
}
