using CVMSCore.BAL.Models.PostDataModel;
using CVMSCore.BAL.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMSCore.BAL.Service
{
    public class EmpService
    {
       
        EmpRepo _repo = new EmpRepo();
        //public int AddEmpSer(EmpModel emp)
        //{
        //    return _repo.SaveEmp(emp);
        //}

        public int AddEmpSer(EmpModel empModel)
        {
            int num = 102;
            try
            {
                return _repo.SaveEmp(empModel);


            }
            catch
            {

            }
            return num;
        }
    }
}
