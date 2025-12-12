using Back_End.Data_Access_Layer;
using Back_End.Models;

namespace Business_Layer
{
    public class AttendanceService
    {
        private enum enMode { Add = 1, Update = 2 }
        private enMode _Mode = enMode.Add;

        private int _AttendanceID;
        private int _EmployeeID;
        private clsEmployee _Employee;

        private DateTime _AttendanceDate;
        private TimeSpan? _CheckIn;
        private TimeSpan? _CheckOut;
        private int _CreatedByUserID;
        private string? _Status;

        public AttendanceService()
        {
            _AttendanceID = -1;
            _EmployeeID = -1;
            _Employee = new clsEmployee();

            _AttendanceDate = DateTime.Now;
            _CheckIn = null;
            _CheckOut = null;
            _CreatedByUserID = -1;
            _Status = null;

            _Mode = enMode.Add;
        }

        public AttendanceService(clsAttendance attendance)
        {
            _AttendanceID = attendance.AttendanceID;
            _EmployeeID = attendance.EmployeeID;
            _Employee = new clsEmployee(attendance.Employee);

            _AttendanceDate = attendance.AttendanceDate;
            _CheckIn = attendance.CheckIn;
            _CheckOut = attendance.CheckOut;
            _CreatedByUserID = attendance.CreatedByUserID;
            _Status = attendance.Status;

            _Mode = enMode.Update;
        }


        public static clsAttendance? GetAttendanceByID(int attendanceID)
        {

            return clsAttendanceData.GetAttendanceByID(attendanceID);
        }

        public static List<clsAttendance> GetAllAttendances()
        {
            return clsAttendanceData.GetAllAttendances();
        }


        private clsAttendance _BuildAttendance()
        {
            return new clsAttendance
            {
                AttendanceID = _AttendanceID,
                EmployeeID = _EmployeeID,
                Employee = _Employee,

                AttendanceDate = _AttendanceDate,
                CheckIn = _CheckIn,
                CheckOut = _CheckOut,
                CreatedByUserID = _CreatedByUserID,
                Status = _Status
            };
        }

        private bool _AddAttendance()
        {
            return clsAttendanceData.AddAttendance(_BuildAttendance()) != -1;
        }

        private bool _UpdateAttendance()
        {
            return clsAttendanceData.UpdateAttendance(_BuildAttendance());
        }


        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Add:
                    if (_AddAttendance())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    break;

                case enMode.Update:
                    return _UpdateAttendance();
            }

            return false;
        }
    }
}
