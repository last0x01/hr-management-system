using Back_End.Data_Access_Layer;
using Back_End.Models;
using Business_Layer.Interfaces;

namespace Business_Layer
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


    }
}
