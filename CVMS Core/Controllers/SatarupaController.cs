using CVMSCore.BAL.Service;
using CVMSCore.BAL.Models.Employee;
using Microsoft.AspNetCore.Mvc;

namespace CVMS_Core.Controllers
{
    public class SatarupaController : Controller
    {
        SatarupaService _employeeService = new SatarupaService();
 

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetData()
        {
            return View();
        }

        //public  JsonResult GetEmployeeList()
        //{
        //    List<EmpClass> employees = new List<EmpClass>();
        //    employees = _employeeService.Getlist();
        //    return Json(new { employees = employees });
        //}sss

        public JsonResult GetEmployeeList()
        {
            List<EmpClass> empClasses = new List<EmpClass>();
            empClasses = _employeeService.Getlist();
            return Json(new { empClasses= empClasses});
        }
    }
}