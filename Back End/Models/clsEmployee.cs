namespace Back_End.Models
{
    public class clsEmployee
    {
        private int _EmployeeID;
        private int _PersonID;
        private clsPerson _Person;

        //Deprtment Allows Null if the employee is CEO
        private int? _DepartmentID;
        private decimal _Salary;
        private string _Job_Position;

        public clsEmployee()
        {
            _EmployeeID = -1;
            _PersonID = -1;
            _DepartmentID = null;
            _Salary = 0;
            _Job_Position = "";
            _Person = new clsPerson();
        }

        public int EmployeeID
        {
            get => _EmployeeID;
            set { _EmployeeID = value; }
        }
        public int PersonID
        {
            get => _PersonID;
            set { _PersonID = value; }
        }

        public int? DepartmentID
        {
            get => _DepartmentID;
            set { _DepartmentID = value; }
        }

        public decimal Salary
        {
            get => _Salary;
            set { _Salary = value; }
        }

        public string Job_Position
        {
            get => _Job_Position;
            set { _Job_Position = value; }
        }


        public clsPerson Person
        {
            get => _Person;
            set { _Person = value; }
        }
    }
}
