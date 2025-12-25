using Back_End.Models;
using HR_MS.Utilities;

namespace HR_MS.MVVM.Models
{
    public class clsEmployeeUiModel : clsNotifyObject
    {
        private int _EmployeeID;
        private int _PersonID;
        private clsPersonUiModel _Person;

        //Deprtment Allows Null if the employee is CEO
        private int? _DepartmentID;
        private decimal _Salary; //Allow Null For Good UI for Removing 0 
        private string _JobPosition;
        private string? _DepartmentName; // جديد

        public clsEmployeeUiModel()
        {
            _EmployeeID = -1;
            _PersonID = -1;
            _DepartmentID = null;
            _Salary = default;
            _JobPosition = "";
            _Person = new clsPersonUiModel();
        }

        public clsEmployee ToEmployee()
        {
            return new clsEmployee
            {
                EmployeeID = this.EmployeeID,
                PersonID = this.PersonID,
                Salary = this.Salary,
                JobPosition = this.JobPosition,
                DepartmentID = this.DepartmentID,
                Person = this.Person.ToPerson()
            };
        }



        public clsEmployeeUiModel(Back_End.Models.clsEmployee Employee)
        {

            _EmployeeID = Employee.EmployeeID;
            _PersonID = Employee.PersonID;
            _DepartmentID = Employee.DepartmentID;
            _Salary = Employee.Salary;
            _JobPosition = Employee.JobPosition;

            if (Employee.Person != null)
            {
                _Person = new clsPersonUiModel
                {
                    ID = Employee.Person.PersonID,
                    FirstName = Employee.Person.FirstName,
                    LastName = Employee.Person.LastName,
                    Age = Employee.Person.Age,
                    Gender = Employee.Person.Gender,
                    Phone = Employee.Person.Phone,
                    Email = Employee.Person.Email,
                    Address = Employee.Person.Address
                };
            }
            else
            {

                _Person = new clsPersonUiModel();
            }
        }

        public string? DepartmentName
        {
            get => _DepartmentName;
            set { _DepartmentName = value; OnPropertyChanged(); }
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

        public string JobPosition
        {
            get => _JobPosition;
            set { _JobPosition = value; OnPropertyChanged(); }
        }

        public clsPersonUiModel Person
        {
            get => _Person;
            set { _Person = value; OnPropertyChanged(); }
        }
    }
}
