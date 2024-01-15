using CVMSCore.BAL.Models.Employee;
using CVMSCore.BAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMSCore.BAL.Service
{
    public class SatarupaService
    {
        SatarupaRepo _Repo = new SatarupaRepo();

        public List<EmpClass> Getlist()
        {
            List<EmpClass> list = new List<EmpClass>();
            list = _Repo.GetData();
            return list;
        } 
    }
}
