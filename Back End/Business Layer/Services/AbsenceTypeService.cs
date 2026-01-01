using Back_End.Data_Access_Layer;
using Back_End.Models;
using Business_Layer.Interfaces.Services;

namespace Business_Layer.Services
{
    public class AbsenceTypeService : IAbsenceTypeService
    {
        public clsAbsenceType? GetAbsenceTypeByID(int typeID)
        {
            return clsAbsenceTypeData.GetAbsenceTypeByID(typeID);
        }

        public List<clsAbsenceType> GetAllAbsenceTypes()
        {
            return clsAbsenceTypeData.GetAllAbsenceTypes();
        }

        public bool AddAbsenceType(clsAbsenceType type)
        {
            return clsAbsenceTypeData.AddAbsenceType(type) != -1;
        }

        public bool UpdateAbsenceType(clsAbsenceType type)
        {
            return clsAbsenceTypeData.UpdateAbsenceType(type);
        }

        public bool DeleteAbsenceType(int typeID)
        {
            return clsAbsenceTypeData.DeleteAbsenceType(typeID);
        }


    }
}
