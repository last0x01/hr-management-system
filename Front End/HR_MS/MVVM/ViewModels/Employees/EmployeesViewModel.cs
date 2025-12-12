using Business_Layer;
using HR_MS.MVVM.Commands;
using HR_MS.MVVM.Models;
using HR_MS.Utilities;
using System.Collections.ObjectModel;

namespace Front_End.HR_MS.MVVM.ViewModels.Employees
{
    public class EmployeesViewModel : clsNotifyObject
    {
        private clsEmployeeUiModel _SelectedEmployee;
        public ObservableCollection<clsEmployeeUiModel> Employees { get; set; } = new ObservableCollection<clsEmployeeUiModel>();

        public clsEmployeeUiModel SelectedEmployee
        {
            get => _SelectedEmployee;
            set { _SelectedEmployee = value; OnPropertyChanged(); }
        }

        public RelayCommand RefreshCommand { get; }
        public RelayCommand AddEditCommand { get; }

        public EmployeesViewModel()
        {

            SelectedEmployee = new clsEmployeeUiModel();

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
                Employees.Add(new clsEmployeeUiModel(em));
            }

        }

    }
}
