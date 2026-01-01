using Back_End.Models;

namespace Models
{
    public class clsAbsence
    {
        public int AbsenceID { get; set; } = -1;
        public int EmployeeID { get; set; } = -1;
        public clsEmployee Employee { get; set; } = new clsEmployee();

        public DateTime AbsenceDate { get; set; } = DateTime.Now;

        public int AbsenceTypeID { get; set; } = -1;
        public clsAbsenceType AbsenceType { get; set; } = new clsAbsenceType();

        public string? Reason { get; set; }

        public int CreatedByUserID { get; set; } = -1;

        public clsAbsence()
        {
        }

        public clsAbsence(clsAbsence absence)
        {
            AbsenceID = absence.AbsenceID;
            EmployeeID = absence.EmployeeID;
            AbsenceDate = absence.AbsenceDate;
            AbsenceTypeID = absence.AbsenceTypeID;
            Reason = absence.Reason;
            CreatedByUserID = absence.CreatedByUserID;

            Employee = new clsEmployee(absence.Employee);
            AbsenceType = new clsAbsenceType(absence.AbsenceType);
        }
    }
}
