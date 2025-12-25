using Back_End.Models;
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

        public clsUserUiModel(clsUser User)
        {
            _UserID = User.UserID;
            _PersonID = User.PersonID;
            _Username = User.Username;
            _Passwrod = User.Password;

            if (User.Person != null)
            {
                _Person = new clsPersonUiModel
                {

                    ID = User.Person.PersonID,
                    FirstName = User.Person.FirstName,
                    LastName = User.Person.LastName,
                    Age = User.Person.Age,
                    Gender = User.Person.Gender,
                    Phone = User.Person.Phone,
                    Email = User.Person.Email,
                    Address = User.Person.Address

                };
            }
            else
            {
                _Person = new clsPersonUiModel();
            }
        }

        public clsUser ToUser()
        {
            return new clsUser
            {
                UserID = this.UserID,
                PersonID = this.PersonID,
                Password = this.Password,
                Username = this.Username,
                Person = this.Person.ToPerson()
            };
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
