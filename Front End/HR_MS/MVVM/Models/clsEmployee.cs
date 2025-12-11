using Front_End.HR_MS.Utilities;

namespace Front_End.HR_MS.MVVM.Models
{
    public class clsEmployee : clsNotifyObject
    {
        private int _EmployeeID;
        private int _PersonID;
        private clsPerson _Person;

        //Deprtment Allows Null if the employee is CEO
        private int? _DepartmentID;
        private decimal _Salary;
        private string _Job_Position;

        public clsEmployee()
        {
            _EmployeeID = -1;
            _PersonID = -1;
            _DepartmentID = null;
            _Salary = 0;
            _Job_Position = "";
            _Person = new clsPerson();
        }

        public clsEmployee(Back_End.Models.clsEmployee Employee)
        {

            _EmployeeID = Employee.EmployeeID;
            _PersonID = Employee.PersonID;
            _DepartmentID = Employee.DepartmentID;
            _Salary = Employee.Salary;
            _Job_Position = Employee.Job_Position;
            _Person = new clsPerson();

            _Person.ID = Employee.Person.ID;
            _Person.FirstName = Employee.Person.FirstName;
            _Person.LastName = Employee.Person.LastName;
            _Person.Age = Employee.Person.Age;
            _Person.Gender = Employee.Person.Gender;
            _Person.Phone = Employee.Person.Phone;
            _Person.Email = Employee.Person.Email;
            _Person.Address = Employee.Person.Address;
        }

        public int EmployeeID
        {
            get => _EmployeeID;
            set { _EmployeeID = value; OnPropertyChanged(); }
        }
        public int PersonID
        {
            get => _PersonID;
            set { _PersonID = value; OnPropertyChanged(); }
        }

        public int? DepartmentID
        {
            get => _DepartmentID;
            set { _DepartmentID = value; OnPropertyChanged(); }
        }

        public decimal Salary
        {
            get => _Salary;
            set { _Salary = value; OnPropertyChanged(); }
        }

        public string Job_Position
        {
            get => _Job_Position;
            set { _Job_Position = value; OnPropertyChanged(); }
        }


        public clsPerson Person
        {
            get => _Person;
            set { _Person = value; OnPropertyChanged(); }
        }
    }
}
