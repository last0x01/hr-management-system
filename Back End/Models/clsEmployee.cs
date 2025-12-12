namespace Back_End.Models
{
    public class clsEmployee
    {
        public int EmployeeID { get; set; } = -1;
        public int PersonID { get; set; } = -1;

        //Deprtment Allows Null if the employee is CEO
        public int? DepartmentID { get; set; }
        public decimal Salary { get; set; }
        public string JobPosition { get; set; } = string.Empty;
        public clsPerson Person { get; set; } = new clsPerson();

        public clsEmployee()
        {

        }

        public clsEmployee(clsEmployee Employee)
        {
            EmployeeID = Employee.EmployeeID;
            PersonID = Employee.PersonID;
            DepartmentID = Employee.DepartmentID;
            Salary = Employee.Salary;
            JobPosition = Employee.JobPosition;

            Person = new clsPerson(Employee.Person);
        }


    }
}
