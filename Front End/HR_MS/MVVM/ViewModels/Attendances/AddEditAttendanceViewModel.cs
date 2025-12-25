using Business_Layer;
using Business_Layer.Interfaces;
using HR_MS.MVVM.Commands;
using HR_MS.MVVM.Models;
using HR_MS.Services;
using HR_MS.Utilities;
using HR_MS.Utilities.Enums;
using System.Windows.Input;

namespace HR_MS.MVVM.ViewModels.Attendances
{
    public class AddEditAttendanceViewModel : clsNotifyObject
    {
        private clsAttendanceUiModel _Attendance;
        private readonly IAttendanceService _AttendanceService;
        private readonly IDialogService _DialogService;

        private enum enMode { Add = 1, Update = 2 }
        private enMode _Mode = enMode.Add;

        public event Action? RequestClose;

        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }

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

            SaveCommand = new RelayCommand(o => _Save());
            CloseCommand = new RelayCommand(o => _Close());

            _Mode = enMode.Update;
        }

        // 🔹 Add
        public AddEditAttendanceViewModel()
        {
            _Attendance = new clsAttendanceUiModel();

            _DialogService = new DialogService();
            _AttendanceService = new AttendanceService();

            SaveCommand = new RelayCommand(o => _Save());
            CloseCommand = new RelayCommand(o => _Close());

            _Mode = enMode.Add;
        }

        private void _Close()
        {
            RequestClose?.Invoke();
        }

        private void _Save()
        {
            switch (_Mode)
            {
                case enMode.Add:
                    AddAttendance();
                    break;

                case enMode.Update:
                    UpdateAttendance();
                    break;
            }
        }

        private void AddAttendance()
        {
            if (_AttendanceService.AddAttendance(Attendance.ToAttendance()))
            {
                _DialogService.ShowMessage(
                    "Attendance added successfully",
                    enMessageType.Success);
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
    }
}
