using Back_End.Models;
using Business_Layer;
using Business_Layer.Interfaces;
using Business_Layer.Interfaces.Services;
using Business_Layer.Services;
using Business_Layer.Validations;
using HR_MS.MVVM.Commands;
using HR_MS.MVVM.Models;
using HR_MS.Services;
using HR_MS.Utilities;
using HR_MS.Utilities.Enums;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HR_MS.MVVM.ViewModels.Attendances
{
    public class AddEditAttendanceViewModel : clsNotifyObject
    {
        private clsAttendanceUiModel _Attendance;
        private readonly IAttendanceService _AttendanceService;
        private readonly IDialogService _DialogService;
        private readonly IEmployeeService _EmployeeService;
        private clsEmployeeUiModel? _SelectedEmployee;

        private int _UserID = UserSession.UserInstance.CurrentUser!.UserID;

        public bool IsNewAttendance => _Mode == enMode.Add;


        public clsEmployeeUiModel? SelectedEmployee
        {
            get => _SelectedEmployee;

            set
            {
                _SelectedEmployee = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<clsEmployeeUiModel> Employees { get; } = new();


        private string? _Title;
        public string? Title
        {
            get => _Title;
            set
            {
                _Title = value;
                OnPropertyChanged();
            }
        }


        private enum enMode { Add = 1, Update = 2 }
        private enMode _Mode = enMode.Add;
        private enMode Mode
        {
            get => _Mode;

            set
            {
                _Mode = value;
                OnPropertyChanged(nameof(Mode));
                // This will make IsNewAttendance change depending on the Mode
                OnPropertyChanged(nameof(IsNewAttendance));
            }
        }



        public event Action? RequestClose;

        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand RefreshEmployeesCommand { get; }

        public clsAttendanceUiModel Attendance
        {
            get => _Attendance;
            set
            {
                _Attendance = value;
                OnPropertyChanged();
            }
        }

        // 🔹 Update
        public AddEditAttendanceViewModel(clsAttendanceUiModel attendance)
        {
            _Attendance = attendance;

            _DialogService = new DialogService();
            _AttendanceService = new AttendanceService();
            _EmployeeService = new EmployeeService();

            SaveCommand = new RelayCommand(o => _Save());
            CloseCommand = new RelayCommand(o => _Close());
            RefreshEmployeesCommand = new RelayCommand(o => _LoadEmployeesToComboBox());

            Mode = enMode.Update;
            Title = "Update Attendance";
            _LoadEmployeesToComboBox();

            SelectedEmployee = Employees.FirstOrDefault(emp => emp.EmployeeID == attendance.EmployeeID);
        }

        // 🔹 Add
        public AddEditAttendanceViewModel()
        {
            _Attendance = new clsAttendanceUiModel();

            _DialogService = new DialogService();
            _AttendanceService = new AttendanceService();
            _EmployeeService = new EmployeeService();

            SaveCommand = new RelayCommand(o => _Save());
            CloseCommand = new RelayCommand(o => _Close());
            RefreshEmployeesCommand = new RelayCommand(o => _LoadEmployeesToComboBox());
            Mode = enMode.Add;
            Title = "Add Attendance";
            Attendance.CheckIn = TimeOnly.FromDateTime(DateTime.Now);
            _LoadEmployeesToComboBox();
        }

        private void _Close()
        {
            RequestClose?.Invoke();
        }

        private void _Save()
        {


            switch (Mode)
            {
                case enMode.Add:
                    AddAttendance();
                    break;

                case enMode.Update:
                    UpdateAttendance();
                    break;
            }
        }

        private bool _IsEmployeeSelected()
        {
            if (SelectedEmployee == null)
            {
                _DialogService.ShowMessage("Please Select an Employee", enMessageType.Warning);
                return false;
            }
            return true;
        }


        private void AddAttendance()
        {


            Attendance.CheckIn = TimeOnly.FromDateTime(DateTime.Now);
            Attendance.CreatedByUserID = _UserID;

            if (!_IsEmployeeSelected())
            {
                return;
            }
            else if (SelectedEmployee != null)
            {
                Attendance.EmployeeID = SelectedEmployee.EmployeeID;
            }

            AttendanceValidationResult result = _AttendanceService.CanAddAttendance(SelectedEmployee!.EmployeeID);


            if (!result.CanAdd)
            {
                _DialogService.ShowMessage(result.Message!, enMessageType.Warning);
                return;
            }

            if (_AttendanceService.AddAttendance(Attendance.ToAttendance()))
            {
                _DialogService.ShowMessage(
                    "Attendance added successfully",
                    enMessageType.Success);
                Title = "Update Attendance";
                Mode = enMode.Update;
            }
            else
            {
                _DialogService.ShowMessage(
                    "Failed to add attendance",
                    enMessageType.Error);
            }
        }

        private void UpdateAttendance()
        {
            Attendance.CheckOut = TimeOnly.FromDateTime(DateTime.Now);


            if (_AttendanceService.UpdateAttendance(Attendance.ToAttendance()))
            {
                _DialogService.ShowMessage(
                    "Attendance updated successfully",
                    enMessageType.Success);
            }
            else
            {
                _DialogService.ShowMessage(
                    "Failed to update attendance",
                    enMessageType.Error);
            }
        }

        private void _LoadEmployeesToComboBox()
        {
            Employees.Clear();
            List<clsEmployee> EmployeesList = _EmployeeService.GetAllEmployees();


            foreach (clsEmployee emp in EmployeesList)
            {
                Employees.Add(new clsEmployeeUiModel(emp));
            }
        }
    }
}
