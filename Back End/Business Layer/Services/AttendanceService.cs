using Back_End.Data_Access_Layer;
using Back_End.Models;
using Business_Layer.Interfaces;
using Business_Layer.Validations;

namespace Business_Layer.Services
{
    public class AttendanceService : IAttendanceService
    {

        public clsAttendance? GetAttendanceByID(int attendanceID)
        {

            return clsAttendanceData.GetAttendanceByID(attendanceID);
        }

        public List<clsAttendance> GetAllAttendances()
        {
            return clsAttendanceData.GetAllAttendances();
        }



        public bool AddAttendance(clsAttendance Attendance)
        {
            return clsAttendanceData.AddAttendance(Attendance) != -1;
        }

        public bool UpdateAttendance(clsAttendance Attendance)
        {
            return clsAttendanceData.UpdateAttendance(Attendance);
        }

        public bool DeleteAttendance(int AttendanceID)
        {
            return clsAttendanceData.DeleteAttendance(AttendanceID);
        }

        public int GetTodayPresentCount()
        {
            return clsAttendanceData.GetTodayPresentCount();
        }

        public int GetTodayLateCount(TimeOnly LateTime)
        {
            return clsAttendanceData.GetTodayLateCount(LateTime);
        }

        public bool IsEmployeePresentToday(int EmployeeID)
        {
            return clsAttendanceData.IsEmployeePresentToday(EmployeeID);
        }

        public AttendanceValidationResult CanAddAttendance(int EmployeeID)
        {
            if (IsEmployeePresentToday(EmployeeID))
            {
                return new(false, "Employee is already checked in today.");
            }

            if (clsAbsenceData.IsEmployeeAbsentToday(EmployeeID))
            {
                return new(false, "Employee is already marked absent today.");
            }

            return new(true, null);
        }


        public List<clsAttendance> GetTodayAttendances()
        {
            return clsAttendanceData.GetAllAttendances().Where(a => a.AttendanceDate.Date == DateTime.Now.Date).ToList();

        }

    }
}
