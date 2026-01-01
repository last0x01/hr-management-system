using Business_Layer.Interfaces;
using Business_Layer.Interfaces.Services;
using Business_Layer.Services;
using HR_MS.MVVM.Commands;
using HR_MS.MVVM.Models;
using HR_MS.Services;
using HR_MS.Utilities;
using HR_MS.Utilities.Enums;
using System.Windows.Input;

namespace HR_MS.MVVM.ViewModels.Departments
{
    public class AddEditDepartmentViewModel : clsNotifyObject
    {
        private IDialogService _DialogService;
        private IDepartmentService _DepartmentService;

        private clsDepartmentUiModel _Department;
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

        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }

        public clsDepartmentUiModel Department
        {
            get => _Department;
            set
            {
                _Department = value; OnPropertyChanged();
            }
        }
        public AddEditDepartmentViewModel()
        {
            this._Department = new clsDepartmentUiModel();

            _DialogService = new DialogService();
            _DepartmentService = new DepartmentService();

            SaveCommand = new RelayCommand(o => _Save());
            CloseCommand = new RelayCommand(o => _Close());

            _Title = "Add Department";
            _Mode = enMode.Add;
        }
        public AddEditDepartmentViewModel(clsDepartmentUiModel Department)
        {
            this._Department = Department;

            _DialogService = new DialogService();
            _DepartmentService = new DepartmentService();

            SaveCommand = new RelayCommand(o => _Save());
            CloseCommand = new RelayCommand(o => _Close());


            _Title = "Update Department";
            _Mode = enMode.Update;
        }


        private void _Save()
        {
            if (!_IsValidDepartmentName())
            {
                return;
            }

            switch (_Mode)
            {
                case enMode.Add:
                    _AddDepartment();
                    break;
                case enMode.Update:
                    _UpdateDepartment();
                    break;

            }
        }

        private bool _IsValidDepartmentName()
        {
            if (Department == null || string.IsNullOrWhiteSpace(Department.DepartmentName))
            {
                _DialogService.ShowMessage("Department name is required.", enMessageType.Warning);
                return false;
            }

            return true;
        }


        private void _UpdateDepartment()
        {
            if (_DepartmentService.UpdateDepartment(Department.ToDepartment()))
            {
                _DialogService.ShowMessage("Department Updated successfully", enMessageType.Success);
            }
            else
                _DialogService.ShowMessage("Failed to Update", enMessageType.Error);
        }
        private void _AddDepartment()
        {
            if (_DepartmentService.AddDepartment(Department.ToDepartment()))
            {
                _DialogService.ShowMessage("Department added successfully", enMessageType.Success);
                _Mode = enMode.Update;

                _Title = "Update Department";
            }
            else
                _DialogService.ShowMessage("Failed to add", enMessageType.Error);
        }
        private void _Close()
        {
            RequestClose?.Invoke();

        }
    }
}
