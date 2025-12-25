using Back_End.Models;
using Business_Layer;
using Business_Layer.Interfaces;
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



        private void _Save()
        {


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
            if (_EmployeeService.AddEmployee(Employee.ToEmployee()))
            {
                _DialogService.ShowMessage("Employee added successfully", enMessageType.Success);
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
