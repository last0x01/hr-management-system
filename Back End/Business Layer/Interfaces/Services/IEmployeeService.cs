using Back_End.Models;

namespace Business_Layer.Interfaces.Services
{
    public interface IEmployeeService
    {

        clsEmployee? GetEmployeeByID(int EmployeeId);
        List<clsEmployee> GetAllEmployees();
        bool AddEmployee(clsEmployee Employee);
        bool UpdateEmployee(clsEmployee Employee);
        bool DeleteEmployee(int EmployeeId);
        int GetEmployeeCount();


    }
}
