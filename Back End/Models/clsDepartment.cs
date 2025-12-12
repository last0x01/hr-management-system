namespace Back_End.Models
{
    public class clsDepartment
    {

        public int DepartmentID { get; set; } = -1;

        public string DepartmentName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = false;

        public string Description { get; set; } = string.Empty;

        public clsDepartment()
        {
        }

        public clsDepartment(clsDepartment Department)
        {
            DepartmentID = Department.DepartmentID;
            DepartmentName = Department.DepartmentName;
            IsActive = Department.IsActive;
            Description = Department.Description;
        }
    }
}
