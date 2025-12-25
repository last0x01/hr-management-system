using Back_End.Models;
using Business_Layer.Interfaces;
using Data_Access_Layer;

namespace Business_Layer
{
    public class DepartmentService : IDepartmentService
    {

        public clsDepartment? GetDepartmentByID(int DepartmentID)
        {

            return clsDepartmentData.GetDepartmentByID(DepartmentID);
        }

        public List<clsDepartment> GetAllDepartments()
        {
            return clsDepartmentData.GetAllDepartments();
        }



        public bool AddDepartment(clsDepartment Department)
        {
            return clsDepartmentData.AddNewDepartment(Department) != -1;
        }

        public bool UpdateDepartment(clsDepartment Department)
        {
            return clsDepartmentData.UpdateDepartment(Department);
        }

        public bool DeleteDepartment(int DepartmentID)
        {
            return clsDepartmentData.DeleteDepartment(DepartmentID);
        }

    }
}
