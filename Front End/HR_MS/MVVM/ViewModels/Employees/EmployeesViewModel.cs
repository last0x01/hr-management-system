using Business_Layer;
using Front_End.HR_MS.MVVM.Commands;
using Front_End.HR_MS.MVVM.Models;
using Front_End.HR_MS.Utilities;
using System.Collections.ObjectModel;
namespace Front_End.HR_MS.MVVM.ViewModels.Employees
{
    public class EmployeesViewModel : clsNotifyObject
    {
        private clsEmployee _SelectedEmployee;
        public ObservableCollection<clsEmployee> Employees { get; set; } = new ObservableCollection<clsEmployee>();

        public clsEmployee SelectedEmployee
        {
            get => _SelectedEmployee;
            set { _SelectedEmployee = value; OnPropertyChanged(); }
        }

        public RelayCommand RefreshCommand { get; }
        public RelayCommand AddEditCommand { get; }

        public EmployeesViewModel()
        {

            SelectedEmployee = new clsEmployee();

            RefreshCommand = new RelayCommand(o => _LoadEmployees());
            AddEditCommand = new RelayCommand(o => _AddEditEmployee());

            _LoadEmployees();
        }


        private void _AddEditEmployee()
        {

        }

        private void _LoadEmployees()
        {
            Employees.Clear();

            List<Back_End.Models.clsEmployee> EmployeesList = EmployeeService.GetAllEmployees();

            foreach (Back_End.Models.clsEmployee em in EmployeesList)
            {
                Employees.Add(new clsEmployee(em));
            }

        }

    }
}
