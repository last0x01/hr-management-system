using Back_End.Data_Access_Layer;
using Business_Layer.Interfaces.Services;
using Business_Layer.Validations;
using Models;

namespace Business_Layer.Services
{
    public class AbsenceService : IAbsenceService
    {



        public clsAbsence? GetAbsenceByID(int absenceID)
        {
            return clsAbsenceData.GetAbsenceByID(absenceID);
        }

        public List<clsAbsence> GetAllAbsences()
        {
            return clsAbsenceData.GetAllAbsences();
        }

        public bool AddAbsence(clsAbsence absence)
        {
            return clsAbsenceData.AddAbsence(absence) != 0;
        }
        public int AddAndGetAbsenceID(clsAbsence absence)
        {
            return clsAbsenceData.AddAbsence(absence);
        }

        public bool UpdateAbsence(clsAbsence absence)
        {
            return clsAbsenceData.UpdateAbsence(absence);
        }

        public bool DeleteAbsence(int absenceID)
        {
            return clsAbsenceData.DeleteAbsence(absenceID);
        }

        public bool IsEmployeeAbsentToday(int EmployeeID)
        {
            return clsAbsenceData.IsEmployeeAbsentToday(EmployeeID);
        }

        public int GetTodayAbsenceCount()
        {
            return clsAbsenceData.GetTodayAbsenceCount();
        }

        public AbsenceValidationResult CanAddAbsence(int EmployeeID)
        {
            if (IsEmployeeAbsentToday(EmployeeID))
            {
                return new(false, "Employee is already marked absent today.");
            }


            if (clsAttendanceData.IsEmployeePresentToday(EmployeeID))
            {
                return new(false, "Employee is already checked in today.");
            }

            return new(true, null);
        }

        public List<clsAbsence> GetTodayAbsences()
        {
            return clsAbsenceData.GetAllAbsences().Where(a => a.AbsenceDate.Date == DateTime.Now.Date).ToList();

        }


    }
}
