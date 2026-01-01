using Back_End.Models;
using HR_MS.Utilities;

namespace HR_MS.MVVM.Models
{
    public class clsAbsenceTypeUiModel : clsNotifyObject
    {
        private int _AbsenceTypeID;
        private string _Name;

        public clsAbsenceTypeUiModel()
        {
            _AbsenceTypeID = -1;
            _Name = string.Empty;
        }

        public clsAbsenceTypeUiModel(clsAbsenceType type)
        {
            _AbsenceTypeID = type.AbsenceTypeID;
            _Name = type.Name;
        }

        public clsAbsenceType ToAbsenceType()
        {
            return new clsAbsenceType
            {
                AbsenceTypeID = this.AbsenceTypeID,
                Name = this.Name
            };
        }

        public int AbsenceTypeID
        {
            get => _AbsenceTypeID;
            set { _AbsenceTypeID = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get => _Name;
            set { _Name = value; OnPropertyChanged(); }
        }
    }
}
