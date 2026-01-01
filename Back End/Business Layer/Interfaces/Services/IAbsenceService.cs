using Business_Layer.Validations;
using Models;

namespace Business_Layer.Interfaces.Services
{
    public interface IAbsenceService
    {
        clsAbsence? GetAbsenceByID(int absenceID);
        List<clsAbsence> GetAllAbsences();
        bool AddAbsence(clsAbsence absence);
        int AddAndGetAbsenceID(clsAbsence absence);
        bool UpdateAbsence(clsAbsence absence);
        bool DeleteAbsence(int absenceID);

        int GetTodayAbsenceCount();

        bool IsEmployeeAbsentToday(int EmployeeID);

        AbsenceValidationResult CanAddAbsence(int Employee);

        List<clsAbsence> GetTodayAbsences();
    }
}
