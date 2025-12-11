using Back_End.Models;
using Data_Access_Layer;

namespace Business_Layer
{
    public class EmployeeService
    {
        public static List<clsEmployee> GetAllEmployees()
        {
            return clsEmployeeData.GetAllEmployees();
        }
    }
}
