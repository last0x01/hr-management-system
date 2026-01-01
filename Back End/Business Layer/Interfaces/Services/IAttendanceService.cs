using Back_End.Models;
using Business_Layer.Validations;

namespace Business_Layer.Interfaces
{
    public interface IAttendanceService
    {

        clsAttendance? GetAttendanceByID(int AttendanceId);
        List<clsAttendance> GetAllAttendances();
        bool AddAttendance(clsAttendance Attendance);
        bool UpdateAttendance(clsAttendance Attendance);
        bool DeleteAttendance(int AttendanceId);

        int GetTodayPresentCount();

        bool IsEmployeePresentToday(int EmployeeID);

        int GetTodayLateCount(TimeOnly LateTime);

        AttendanceValidationResult CanAddAttendance(int EmployeeID);

        List<clsAttendance> GetTodayAttendances();

    }
}
