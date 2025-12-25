using Back_End.Models;
using Business_Layer;
using Business_Layer.Interfaces;
using HR_MS.MVVM.Commands;
using HR_MS.MVVM.Models;
using HR_MS.MVVM.ViewModels.Employees;
using HR_MS.MVVM.Views.Employees;
using HR_MS.Services;
using HR_MS.Utilities;
using HR_MS.Utilities.Enums;
using System.Collections.ObjectModel;

namespace Front_End.HR_MS.MVVM.ViewModels.Employees
{
    public class EmployeesViewModel : clsNotifyObject
    {
        private clsEmployeeUiModel? _SelectedEmployee;
        private readonly IEmployeeService _EmployeeService;
        private IDepartmentService _DepartmentService;


        private readonly IDialogService _DialogService;
        public ObservableCollection<clsEmployeeUiModel> Employees { get; } = new();


        public clsEmployeeUiModel? SelectedEmployee
        {
            get => _SelectedEmployee;
            set
            {
                _SelectedEmployee = value;
                OnPropertyChanged();


            }
        }

        public RelayCommand RefreshEmployeesCommand { get; }
        public RelayCommand AddEmployeeCommand { get; }
        public RelayCommand EditEmployeeCommand { get; }
        public RelayCommand DeleteEmployeeCommand { get; }


        public EmployeesViewModel()
        {
            _DialogService = new DialogService();
            _DepartmentService = new DepartmentService();
            _EmployeeService = new EmployeeService();
            _SelectedEmployee = null;

            RefreshEmployeesCommand = new RelayCommand(o => _LoadEmployees());
            EditEmployeeCommand = new RelayCommand(o => _UpdateEmployee());
            AddEmployeeCommand = new RelayCommand(o => _AddEmployee());

            DeleteEmployeeCommand = new RelayCommand(o => _DeleteEmployee());

            _LoadEmployees();
        }


        private void _AddEmployee()
        {
            AddEditEmployeeViewModel ViewModel = new AddEditEmployeeViewModel();
            AddEditEmployeeView window = new AddEditEmployeeView
            {
                DataContext = ViewModel
            };


            window.ShowDialog();
            _LoadEmployees();

        }
        private void _DeleteEmployee()
        {
            if (SelectedEmployee == null)
                return;

            if (_EmployeeService.DeleteEmployee(SelectedEmployee.EmployeeID))
            {
                _DialogService.ShowMessage("Deleted successfully", enMessageType.Success);
                _LoadEmployees();
                SelectedEmployee = null;
            }
            else
                _DialogService.ShowMessage("Failed to delete", enMessageType.Error);




        }
        private void _UpdateEmployee()
        {
            if (SelectedEmployee == null)
                return;

            AddEditEmployeeViewModel ViewModel = new AddEditEmployeeViewModel(SelectedEmployee);
            AddEditEmployeeView window = new AddEditEmployeeView
            {
                DataContext = ViewModel
            };

            window.ShowDialog();
            _LoadEmployees();
        }

        private void _LoadEmployees()
        {
            Employees.Clear();

            List<clsDepartment> DepartmentsList = _DepartmentService.GetAllDepartments();
            List<Back_End.Models.clsEmployee> EmployeesList = _EmployeeService.GetAllEmployees();

            var query = from e in EmployeesList
                        join d in DepartmentsList
                        on e.DepartmentID equals d.DepartmentID into deptGroup
                        from d in deptGroup.DefaultIfEmpty()  // Left join
                        select new clsEmployeeUiModel(e)
                        {
                            DepartmentName = d != null ? d.DepartmentName : " "
                        };


            foreach (var em in query)
            {
                Employees.Add(em);
            }
        }


    }
}
