using Back_End.Models;
using Data_Access_Layer;

namespace Business_Layer
{
    public class DepartmentService
    {
        private enum enMode { Add = 1, Update = 2 }
        private enMode _Mode = enMode.Add;

        private int _DepartmentID;
        private string _DepartmentName;
        private bool _IsActive;
        private string _Description;

        public DepartmentService()
        {
            _DepartmentID = -1;
            _DepartmentName = string.Empty;
            _IsActive = false;
            _Description = string.Empty;

            _Mode = enMode.Add;
        }

        public DepartmentService(clsDepartment department)
        {
            _DepartmentID = department.DepartmentID;
            _DepartmentName = department.DepartmentName;
            _IsActive = department.IsActive;
            _Description = department.Description;

            _Mode = enMode.Update;
        }


        public static clsDepartment? GetDepartmentByID(int departmentID)
        {
            return clsDepartmentData.GetDepartmentByID(departmentID);
        }

        public static List<clsDepartment> GetAllDepartments()
        {
            return clsDepartmentData.GetAllDepartments();
        }


        private clsDepartment _BuildDepartment()
        {
            return new clsDepartment
            {
                DepartmentID = _DepartmentID,
                DepartmentName = _DepartmentName,
                IsActive = _IsActive,
                Description = _Description
            };
        }

        private bool _AddDepartment()
        {
            return clsDepartmentData.AddNewDepartment(_BuildDepartment()) != -1;
        }

        private bool _UpdateDepartment()
        {
            return clsDepartmentData.UpdateDepartment(_BuildDepartment());
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Add:
                    if (_AddDepartment())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    break;

                case enMode.Update:
                    return _UpdateDepartment();
            }

            return false;
        }
    }
}
