using HR_MS.Utilities;

namespace HR_MS.MVVM.Models
{
    public class clsUserUiModel : clsNotifyObject
    {
        private int _UserID;
        private int _PersonID;
        private clsPersonUiModel _Person;
        private string _Username;
        private string _Passwrod;

        public clsUserUiModel()
        {
            _UserID = -1;
            _PersonID = -1;
            _Username = "";
            _Passwrod = "";
            _Person = new clsPersonUiModel();
        }

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; OnPropertyChanged(); }
        }
        public int PersonID
        {
            get { return _PersonID; }
            set { _PersonID = value; OnPropertyChanged(); }
        }
        public string Username
        {
            get { return _Username; }
            set { _Username = value; OnPropertyChanged(); }
        }
        public string Password
        {
            get { return _Passwrod; }
            set { _Passwrod = value; OnPropertyChanged(); }
        }

        public clsPersonUiModel Person
        {
            get => _Person;
            set { _Person = value; OnPropertyChanged(); }
        }
    }
}
