

namespace Back_End.Models
{
    public class clsDepartment
    {

        private int _DepartmentID;

        private string _DepartmentName;

        private bool _IsActive;

        private string _Description;



        public clsDepartment()
        {
            _DepartmentID = -1;
            _DepartmentName = "";
            _IsActive = false;
            _Description = "";
        }

        public int DepartmentID
        {
            get { return _DepartmentID; }
            set { _DepartmentID = value; }
        }
        public string DepartmentName
        {
            get { return _DepartmentName; }
            set { _DepartmentName = value; }
        }
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }



    }
}
