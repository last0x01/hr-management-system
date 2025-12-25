using Back_End.Models;
using HR_MS.Utilities;

namespace HR_MS.MVVM.Models
{
    public class clsDepartmentUiModel : clsNotifyObject
    {

        private int _DepartmentID;

        private string _DepartmentName;

        private bool _IsActive;

        private string _Description;



        public clsDepartmentUiModel()
        {
            _DepartmentID = -1;
            _DepartmentName = "";
            _IsActive = false;
            _Description = "";
        }

        public clsDepartmentUiModel(clsDepartment department)
        {
            _DepartmentID = department.DepartmentID;
            _DepartmentName = department.DepartmentName;
            _IsActive = department.IsActive;
            _Description = department.Description;
        }

        public clsDepartment ToDepartment()
        {
            return new clsDepartment
            {
                DepartmentID = this.DepartmentID,
                DepartmentName = this.DepartmentName,
                IsActive = this.IsActive,
                Description = this.Description,
            };
        }


        public int DepartmentID
        {
            get { return _DepartmentID; }
            set { _DepartmentID = value; OnPropertyChanged(); }
        }
        public string DepartmentName
        {
            get { return _DepartmentName; }
            set { _DepartmentName = value; OnPropertyChanged(); }
        }
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; OnPropertyChanged(); }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; OnPropertyChanged(); }
        }



    }
}
