using Back_End.Models;
using Business_Layer.Interfaces;
using Data_Access_Layer;

namespace Business_Layer
{
    public class EmployeeService : IEmployeeService
    {


        public clsEmployee? GetEmployeeByID(int EmployeeID)
        {
            return clsEmployeeData.GetEmployeeByID(EmployeeID);


        }
        public List<clsEmployee> GetAllEmployees()
        {
            return clsEmployeeData.GetAllEmployees();
        }

        public bool UpdateEmployee(clsEmployee Employee)
        {


            return clsEmployeeData.UpdateEmployee(Employee);
        }


        public bool AddEmployee(clsEmployee Employee)
        {

            return clsEmployeeData.AddNewEmployee(Employee) != -1;
        }

        public bool DeleteEmployee(int EmployeeID)
        {

            return clsEmployeeData.DeleteEmployee(EmployeeID);
        }


    }
}
