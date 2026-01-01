using Back_End.Models;

namespace Business_Layer.Interfaces.Services
{
    public interface IAbsenceTypeService
    {
        clsAbsenceType? GetAbsenceTypeByID(int typeID);
        List<clsAbsenceType> GetAllAbsenceTypes();
        bool AddAbsenceType(clsAbsenceType type);
        bool UpdateAbsenceType(clsAbsenceType type);
        bool DeleteAbsenceType(int typeID);


    }
}
