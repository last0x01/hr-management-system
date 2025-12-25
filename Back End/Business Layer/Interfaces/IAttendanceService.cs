using Back_End.Models;

namespace Business_Layer.Interfaces
{
    public interface IAttendanceService
    {

        clsAttendance? GetAttendanceByID(int AttendanceId);
        List<clsAttendance> GetAllAttendances();
        bool AddAttendance(clsAttendance Attendance);
        bool UpdateAttendance(clsAttendance Attendance);
        bool DeleteAttendance(int AttendanceId);

    }
}
