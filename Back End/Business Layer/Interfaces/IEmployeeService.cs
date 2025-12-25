using Back_End.Models;

namespace Business_Layer.Interfaces
{
    public interface IEmployeeService
    {

        clsEmployee? GetEmployeeByID(int EmployeeId);
        List<clsEmployee> GetAllEmployees();
        bool AddEmployee(clsEmployee Employee);
        bool UpdateEmployee(clsEmployee Employee);
        bool DeleteEmployee(int EmployeeId);


    }
}
