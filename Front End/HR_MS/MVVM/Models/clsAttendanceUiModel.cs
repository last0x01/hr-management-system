using Back_End.Models;
using HR_MS.Utilities;

namespace HR_MS.MVVM.Models
{
    public class clsAttendanceUiModel : clsNotifyObject
    {
        /*
         Check in allows null
         Check out allows null
         Status allows null
        */

        private int _AttendanceID;
        private int _EmployeeID;
        private string? _EmployeeName;
        private DateTime _AttendanceDate;
        private TimeOnly? _CheckIn;
        private TimeOnly? _CheckOut;
        private int _CreatedByUserID;

        public clsAttendanceUiModel()
        {
            _AttendanceID = -1;
            _EmployeeID = -1;
            _EmployeeName = null;
            _AttendanceDate = DateTime.Now;
            _CheckIn = null;
            _CheckOut = null;
            _CreatedByUserID = -1;

        }

        public clsAttendanceUiModel(clsAttendance attendance)
        {
            _AttendanceID = attendance.AttendanceID;
            _EmployeeID = attendance.EmployeeID;
            _AttendanceDate = attendance.AttendanceDate;
            _CheckIn = attendance.CheckIn;
            _CheckOut = attendance.CheckOut;
            _CreatedByUserID = attendance.CreatedByUserID;

        }

        public clsAttendance ToAttendance()
        {
            return new clsAttendance
            {
                AttendanceID = this.AttendanceID,
                EmployeeID = this.EmployeeID,
                AttendanceDate = this.AttendanceDate,
                CheckIn = this.CheckIn,
                CheckOut = this.CheckOut,
                CreatedByUserID = this.CreatedByUserID,

            };
        }

        public int AttendanceID
        {
            get => _AttendanceID;
            set { _AttendanceID = value; OnPropertyChanged(); }
        }

        public int EmployeeID
        {
            get => _EmployeeID;
            set { _EmployeeID = value; OnPropertyChanged(); }
        }

        public string? EmployeeName
        {
            get => _EmployeeName;
            set { _EmployeeName = value; OnPropertyChanged(); }
        }

        public DateTime AttendanceDate
        {
            get => _AttendanceDate;
            set { _AttendanceDate = value; OnPropertyChanged(); }
        }

        public TimeOnly? CheckIn
        {
            get => _CheckIn;
            set { _CheckIn = value; OnPropertyChanged(); }
        }

        public TimeOnly? CheckOut
        {
            get => _CheckOut;
            set { _CheckOut = value; OnPropertyChanged(); }
        }

        public int CreatedByUserID
        {
            get => _CreatedByUserID;
            set { _CreatedByUserID = value; OnPropertyChanged(); }
        }


        public TimeSpan? TotalHours => CheckIn != null && CheckOut != null ? CheckOut - CheckIn : null;



        public string? TotalHoursFormateed => TotalHours.HasValue ?
            $"{(int)TotalHours.Value.TotalHours:D2}:{TotalHours.Value.Minutes:D2}" : "--:--";


        public bool HasCheckedIn => CheckIn != null;
        public bool HasCheckedOut => CheckOut != null;

        public bool IsAbsent => CheckIn == null;
    }
}
