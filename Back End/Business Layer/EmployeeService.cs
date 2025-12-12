using Back_End.Models;
using Data_Access_Layer;

namespace Business_Layer
{
    public class EmployeeService
    {
        private enum enMode { Add = 1, Update = 2 }
        private enMode _Mode = enMode.Add;
        private int _EmployeeID;
        private int _PersonID;
        private clsPerson _Person;

        private int? _DepartmentID;
        private decimal _Salary;
        private string _JobPosition;

        public EmployeeService()
        {
            _EmployeeID = -1;
            _PersonID = -1;
            _DepartmentID = null;
            _Salary = 0;
            _JobPosition = "";
            _Person = new clsPerson();

            _Mode = enMode.Add;
        }

        public EmployeeService(clsEmployee Employee)
        {
            _EmployeeID = Employee.EmployeeID;
            _PersonID = Employee.PersonID;
            _DepartmentID = Employee.DepartmentID;
            _Salary = Employee.Salary;
            _JobPosition = Employee.JobPosition;
            _Person = Employee.Person;

            _Mode = enMode.Update;
        }

        public static clsEmployee? GetEmployeeByID(int EmployeeID)
        {
            return clsEmployeeData.GetEmployeeByID(EmployeeID);


        }
        public static List<clsEmployee> GetAllEmployees()
        {
            return clsEmployeeData.GetAllEmployees();
        }

        private clsEmployee _BulidAndGetEmployeeObject()
        {
            return new clsEmployee
            {
                EmployeeID = _EmployeeID,
                PersonID = _PersonID,
                JobPosition = _JobPosition,
                Salary = _Salary,
                DepartmentID = _DepartmentID,
                Person = _Person

            };
        }

        private bool _UpdateEmployee()
        {


            return clsEmployeeData.UpdateEmployee(_BulidAndGetEmployeeObject());
        }


        private bool _AddEmployee()
        {

            return clsEmployeeData.AddNewEmployee(_BulidAndGetEmployeeObject()) != -1;
        }


        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Add:
                    if (_AddEmployee())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    break;


                case enMode.Update:
                    return _UpdateEmployee();
            }

            return false;

        }
    }
}
