using HR_MS.Utilities.Enums;

namespace HR_MS.Utilities
{
    public static class clsEnAbsenceType
    {
        public static string GetString(enAbsenceType AbsenceType)
        {
            return AbsenceType switch
            {
                enAbsenceType.SickLeave => "Sick Leave",
                enAbsenceType.CasualLeave => "Casual Leave",
                enAbsenceType.Vacation => "Vacation",
                enAbsenceType.MaternityLeave => "Maternity Leave",
                enAbsenceType.PaternityLeave => "Paternity Leave",
                enAbsenceType.UnpaidLeave => "Unpaid Leave",
                enAbsenceType.WorkFromHome => "Work From Home",
                enAbsenceType.EmergencyLeave => "Emergency Leave",
                enAbsenceType.BereavementLeave => "Bereavement Leave",
                _ => "Unknown"
            };
        }
    }
}
