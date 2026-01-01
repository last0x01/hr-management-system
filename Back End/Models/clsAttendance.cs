namespace Back_End.Models
{
    public class clsAttendance
    {
        public int AttendanceID { get; set; } = -1;
        public int EmployeeID { get; set; } = -1;
        public clsEmployee Employee { get; set; } = new clsEmployee();

        public DateTime AttendanceDate { get; set; } = DateTime.Now;

        // Nullable: may not have checked in yet
        public TimeOnly? CheckIn { get; set; }

        // Nullable: may not have checked out yet
        public TimeOnly? CheckOut { get; set; }

        public int CreatedByUserID { get; set; } = -1;


        public clsAttendance()
        {
        }

        public clsAttendance(clsAttendance Attendance)
        {


            AttendanceID = Attendance.AttendanceID;
            EmployeeID = Attendance.EmployeeID;
            AttendanceDate = Attendance.AttendanceDate;
            CheckIn = Attendance.CheckIn;
            CheckOut = Attendance.CheckOut;
            CreatedByUserID = Attendance.CreatedByUserID;

            Employee = new clsEmployee(Attendance.Employee);
        }
    }
}
