using Back_End.Models;
using Business_Layer;
using Business_Layer.Interfaces;
using HR_MS.MVVM.Commands;
using HR_MS.MVVM.Models;
using HR_MS.MVVM.Views.Departments;
using HR_MS.Services;
using HR_MS.Utilities;
using HR_MS.Utilities.Enums;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HR_MS.MVVM.ViewModels.Departments
{
    public class DepartmentsViewModel : clsNotifyObject
    {
        private clsDepartmentUiModel? _SelectedDepartment;
        private IDepartmentService _DepartmentService;
        private IDialogService _DialogService;
        public clsDepartmentUiModel? SelectedDepartment
        {
            get => _SelectedDepartment;
            set { _SelectedDepartment = value; OnPropertyChanged(); }
        }
        public ObservableCollection<clsDepartmentUiModel> Departments { get; } = new();

        public ICommand EditDepartmentCommand { get; }
        public ICommand AddDepartmentCommand { get; }
        public ICommand DeleteDepartmentCommand { get; }
        public ICommand RefreshDepartmentCommand { get; }
        public DepartmentsViewModel()
        {
            _DepartmentService = new DepartmentService();
            _DialogService = new DialogService();

            AddDepartmentCommand = new RelayCommand(_ => _AddDepartment());
            EditDepartmentCommand = new RelayCommand(_ => _UpdateDepartment());
            DeleteDepartmentCommand = new RelayCommand(_ => _DeleteDepartment());
            RefreshDepartmentCommand = new RelayCommand(_ => _LoadDepartments());

            _LoadDepartments();
        }


        private void _AddDepartment()
        {

            AddEditDepartmentViewModel VM = new AddEditDepartmentViewModel();
            AddEditDepartmentView View = new AddEditDepartmentView
            {
                DataContext = VM
            };

            View.ShowDialog();
            _LoadDepartments();
        }
        private void _UpdateDepartment()
        {
            if (SelectedDepartment == null)
                return;

            AddEditDepartmentViewModel VM = new AddEditDepartmentViewModel(SelectedDepartment);
            AddEditDepartmentView View = new AddEditDepartmentView()
            {
                DataContext = VM
            };

            View.ShowDialog();
            _LoadDepartments();
        }
        private void _DeleteDepartment()
        {
            if (SelectedDepartment == null)
                return;

            if (_DepartmentService.DeleteDepartment(SelectedDepartment.DepartmentID))
            {
                _DialogService.ShowMessage("Deleted successfully", enMessageType.Success);
                _LoadDepartments();
                SelectedDepartment = null;
            }
            else
                _DialogService.ShowMessage("Failed to delete", enMessageType.Error);


        }
        private void _LoadDepartments()
        {
            Departments.Clear();

            List<clsDepartment> List = _DepartmentService.GetAllDepartments();

            foreach (clsDepartment Dep in List)
            {
                Departments.Add(new clsDepartmentUiModel(Dep));
            }

        }
    }
}
