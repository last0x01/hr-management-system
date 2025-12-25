using Back_End.Models;

namespace Business_Layer.Interfaces
{
    public interface IDepartmentService
    {

        clsDepartment? GetDepartmentByID(int DepartmentId);
        List<clsDepartment> GetAllDepartments();
        bool AddDepartment(clsDepartment Department);
        bool UpdateDepartment(clsDepartment Department);
        bool DeleteDepartment(int DepartmentId);

    }
}
