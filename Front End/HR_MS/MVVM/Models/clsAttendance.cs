using Front_End.HR_MS.Utilities;

namespace Front_End.HR_MS.MVVM.Models
{
    public class clsAttendance : clsNotifyObject
    {
        /*
         Check in allows null
         Check out allows null
         Status allows null
        */

        private int _AttendanceID;
        private int _EmployeeID;
        private DateTime _AttendanceDate;
        private TimeSpan? _CheckIn;
        private TimeSpan? _CheckOut;
        private int _CreatedByUserID;
        private string? _Status;

        public clsAttendance()
        {
            _AttendanceID = -1;
            _EmployeeID = -1;
            _AttendanceDate = DateTime.Now;
            _CheckIn = null;
            _CheckOut = null;
            _CreatedByUserID = -1;
            _Status = null;
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

        public DateTime AttendanceDate
        {
            get => _AttendanceDate;
            set { _AttendanceDate = value; OnPropertyChanged(); }
        }

        public TimeSpan? CheckIn
        {
            get => _CheckIn;
            set { _CheckIn = value; OnPropertyChanged(); }
        }

        public TimeSpan? CheckOut
        {
            get => _CheckOut;
            set { _CheckOut = value; OnPropertyChanged(); }
        }

        public int CreatedByUserID
        {
            get => _CreatedByUserID;
            set { _CreatedByUserID = value; OnPropertyChanged(); }
        }

        public string? Status
        {
            get => _Status;
            set { _Status = value; OnPropertyChanged(); }
        }

        public TimeSpan? TotalHours => CheckIn != null && CheckOut != null ? CheckOut - CheckIn : null;

        public bool HasCheckedIn => CheckIn != null;
        public bool HasCheckedOut => CheckOut != null;

        public bool IsAbsent => CheckIn == null;
    }
}
