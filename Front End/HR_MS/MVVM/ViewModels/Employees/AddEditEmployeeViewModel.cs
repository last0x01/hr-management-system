using Back_End.Models;
using Business_Layer.Interfaces;
using Business_Layer.Interfaces.Services;
using Business_Layer.Services;
using HR_MS.MVVM.Commands;
using HR_MS.MVVM.Models;
using HR_MS.Services;
using HR_MS.Utilities;
using HR_MS.Utilities.Enums;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace HR_MS.MVVM.ViewModels.Employees

{
    public class AddEditEmployeeViewModel : clsNotifyObject
    {


        private enGenderType _SelectedGender;



        private clsEmployeeUiModel _Employee;
        private IEmployeeService _EmployeeService;
        private readonly IDepartmentService _DepartmentService;

        private readonly IDialogService _DialogService;
        private enum enMode { Add = 1, Update = 2 }
        private enMode _Mode = enMode.Add;

        private string _Title;

        public string Title
        {
            get => _Title;
            set
            {
                _Title = value;
                OnPropertyChanged();
            }
        }

        public event Action? RequestClose;
        public ObservableCollection<clsDepartmentUiModel> Departments { get; } = new();
        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }

        public enGenderType SelectedGender
        {
            get => _SelectedGender;

            set
            {
                _SelectedGender = value;
                if (Employee?.Person != null)
                    Employee.Person.Gender = GenderString;

                OnPropertyChanged();

            }
        }

        public string GenderString => (SelectedGender == enGenderType.Male) ? "Male" : "Female";


        public clsEmployeeUiModel Employee
        {
            get => _Employee;

            set { _Employee = value; OnPropertyChanged(); }
        }

        private void _UpdateEmployeeGenderToUI()
        {
            SelectedGender = (Employee.Person.Gender == "Male") ? enGenderType.Male : enGenderType.Female;
        }

        public AddEditEmployeeViewModel(clsEmployeeUiModel Employee)
        {
            this._Employee = Employee;
            _DialogService = new DialogService();
            _EmployeeService = new EmployeeService();
            _DepartmentService = new DepartmentService();
            SaveCommand = new RelayCommand(o => _Save());
            CloseCommand = new RelayCommand(o => _Close());

            _Mode = enMode.Update;
            _Title = "Update Employee";
            _UpdateEmployeeGenderToUI();
            _LoadDepartments();
        }
        public AddEditEmployeeViewModel()
        {
            this._Employee = new clsEmployeeUiModel();
            _DialogService = new DialogService();
            _EmployeeService = new EmployeeService();
            _DepartmentService = new DepartmentService();
            SaveCommand = new RelayCommand(o => _Save());
            CloseCommand = new RelayCommand(o => _Close());

            _Mode = enMode.Add;
            _Title = "Add Employee";

            SelectedGender = enGenderType.Male;

            _LoadDepartments();
        }

        private void _Close()
        {
            RequestClose?.Invoke();
        }

        private void _LoadDepartments()
        {
            Departments.Clear();

            List<clsDepartment> List = _DepartmentService.GetAllDepartments();

            foreach (clsDepartment item in List)
            {
                Departments.Add(new clsDepartmentUiModel(item));
            }
        }

        private bool _IsValidSalary()
        {
            if (Employee == null)
            {
                _DialogService.ShowMessage("Employee information is missing.", enMessageType.Warning);
                return false;
            }

            if (Employee.Salary == null)
            {
                _DialogService.ShowMessage("Salary is not provided.", enMessageType.Warning);
                return false;
            }
            else if (Employee.Salary <= 0)
            {
                _DialogService.ShowMessage("Salary must be greater than 0.", enMessageType.Warning);
                return false;
            }

            return true;
        }


        private bool _IsValidAge()
        {
            if (Employee?.Person == null)
            {
                _DialogService.ShowMessage("Employee information is missing.", enMessageType.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Employee.Person.Age.ToString()))
            {
                _DialogService.ShowMessage("Age is not provided.", enMessageType.Warning);
                return false;
            }

            if (Employee.Person.Age < 18)
            {
                _DialogService.ShowMessage("Age is below the minimum allowed (18 years).", enMessageType.Warning);
                return false;
            }
            else if (Employee.Person.Age > 65)
            {
                _DialogService.ShowMessage("Age exceeds the maximum allowed (65 years).", enMessageType.Warning);
                return false;
            }

            return true;
        }

        private bool _IsValidName(string Name, string Message)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                _DialogService.ShowMessage(Message, enMessageType.Warning);
                return false;
            }

            return true;
        }
        private bool _IsValidJobPosition()
        {
            if (string.IsNullOrWhiteSpace(Employee.JobPosition))
            {
                _DialogService.ShowMessage("Job Position is required.", enMessageType.Warning);
                return false;
            }


            return true;
        }

        private bool _IsValidEmployeeNames()
        {
            if (!_IsValidName(Employee.Person.FirstName, "First name is required."))
                return false;
            if (!_IsValidName(Employee.Person.LastName, "Last name is required."))
                return false;

            return true;
        }


        private void _Save()
        {
            if (!_IsValidAge() || !_IsValidSalary() || !_IsValidEmployeeNames() || !_IsValidJobPosition())
            {
                return;
            }




            switch (_Mode)
            {
                case enMode.Add:
                    AddEmployee();
                    break;

                case enMode.Update:
                    UpdateEmployee();
                    break;
            }
        }

        private void AddEmployee()
        {
            int EmployeeID = _EmployeeService.AddAndGetEmployeeID(Employee.ToEmployee());

            if (EmployeeID != -1)
            {
                _DialogService.ShowMessage("Employee added successfully", enMessageType.Success);
                _Mode = enMode.Update;
                Title = "Update Employee";
                Employee.EmployeeID = EmployeeID;
                Employee.PersonID = _EmployeeService.GetPersonIDByEmployeeID(EmployeeID);
            }
            else
                _DialogService.ShowMessage("Failed to add", enMessageType.Error);
        }

        private void UpdateEmployee()
        {
            if (_EmployeeService.UpdateEmployee(Employee.ToEmployee()))
            {
                _DialogService.ShowMessage("Employee updated successfully", enMessageType.Success);
            }
            else
                _DialogService.ShowMessage("Failed to update", enMessageType.Error);
        }


    }
}
