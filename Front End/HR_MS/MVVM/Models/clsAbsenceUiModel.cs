using HR_MS.Utilities;
using HR_MS.Utilities.Enums;
using Models;

namespace HR_MS.MVVM.Models
{
    public class clsAbsenceUiModel : clsNotifyObject
    {
        private int _AbsenceID;
        private int _EmployeeID;
        private string? _EmployeeName;
        private DateTime _AbsenceDate;
        private int _AbsenceTypeID;
        private string? _Reason;
        private int _CreatedByUserID;




        public clsAbsenceUiModel()
        {

            _AbsenceID = -1;
            _EmployeeID = -1;
            _EmployeeName = null;
            _AbsenceDate = DateTime.Now;
            _AbsenceTypeID = -1;
            _Reason = null;
            _CreatedByUserID = -1;
        }

        public clsAbsenceUiModel(clsAbsence absence)
        {
            _AbsenceID = absence.AbsenceID;
            _EmployeeID = absence.EmployeeID;
            _AbsenceDate = absence.AbsenceDate;
            _AbsenceTypeID = absence.AbsenceTypeID;
            _Reason = absence.Reason;
            _CreatedByUserID = absence.CreatedByUserID;
        }

        public clsAbsence ToAbsence()
        {
            return new clsAbsence
            {
                AbsenceID = this.AbsenceID,
                EmployeeID = this.EmployeeID,
                AbsenceDate = this.AbsenceDate,
                AbsenceTypeID = this.AbsenceTypeID,
                Reason = this.Reason,
                CreatedByUserID = this.CreatedByUserID
            };
        }

        public int AbsenceID
        {
            get => _AbsenceID;
            set { _AbsenceID = value; OnPropertyChanged(); }
        }

        public int EmployeeID
        {
            get => _EmployeeID;
            set { _EmployeeID = value; OnPropertyChanged(); }
        }


        public string? EmployeeName
        {
            get => _EmployeeName;
            set { _EmployeeName = value; OnPropertyChanged(); }
        }

        public DateTime AbsenceDate
        {
            get => _AbsenceDate;
            set { _AbsenceDate = value; OnPropertyChanged(); }
        }

        public int AbsenceTypeID
        {
            get => _AbsenceTypeID;
            set { _AbsenceTypeID = value; OnPropertyChanged(); }
        }

        public string AbsenceTypeName => clsEnAbsenceType.GetString((enAbsenceType)this.AbsenceTypeID);

        public string? Reason
        {
            get => _Reason;
            set { _Reason = value; OnPropertyChanged(); }
        }

        public int CreatedByUserID
        {
            get => _CreatedByUserID;
            set { _CreatedByUserID = value; OnPropertyChanged(); }
        }



        // Optional helper properties
        public bool HasReason => !string.IsNullOrWhiteSpace(Reason);
    }
}
