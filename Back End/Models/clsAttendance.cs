namespace Back_End.Models
{
    public class clsAttendance
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
            set { _AttendanceID = value; }
        }

        public int EmployeeID
        {
            get => _EmployeeID;
            set { _EmployeeID = value; }
        }

        public DateTime AttendanceDate
        {
            get => _AttendanceDate;
            set { _AttendanceDate = value; }
        }

        public TimeSpan? CheckIn
        {
            get => _CheckIn;
            set { _CheckIn = value; }
        }

        public TimeSpan? CheckOut
        {
            get => _CheckOut;
            set { _CheckOut = value; }
        }

        public int CreatedByUserID
        {
            get => _CreatedByUserID;
            set { _CreatedByUserID = value; }
        }

        public string? Status
        {
            get => _Status;
            set { _Status = value; }
        }


    }
}
