using CVMSCore.BAL.Models.Master;
using CVMSCore.BAL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CVMSCore.BAL.Service;
using CVMSCore.BAL.Models.PostDataModel;
using CVMSCore.BAL.Models.Employee;

namespace CVMS_Core.Controllers
{
    public class EmployeeController : Controller
    {
        SatarupaService _employeeService = new SatarupaService();
        EmpService service = new EmpService();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EmpPost()
        {
            return View();
        }

        public int Savemployee(IFormCollection formcollection)
        {
            var result = 0;
            if (formcollection != null)
            {
                EmpModel obj = new EmpModel();

                obj.Name = Convert.ToString(formcollection["Name"]);
                obj.Age = Convert.ToInt32(formcollection["Age"]);
                obj.Gender = Convert.ToString(formcollection["Gender"]);
                obj.Address = Convert.ToString(formcollection["Address"]);

                result = service.AddEmpSer(obj);
            }
            return result;

        }

        public JsonResult GetEmployeeList()
        {
            List<EmpClass> empClasses = new List<EmpClass>();
            empClasses = _employeeService.Getlist();
            return Json(new { empClasses = empClasses });
        }
    }
}
