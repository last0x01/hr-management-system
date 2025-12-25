using Back_End.Models;
using HR_MS.Utilities;

namespace HR_MS.MVVM.Models
{
    public class clsPersonUiModel : clsNotifyObject
    {
        /*
         Phone allows null && Email allows null && Address allows null
         */
        private int _ID;
        private string _FirstName;
        private string _LastName;
        private int _Age;
        private string? _Phone;
        private string? _Email;
        private string _Gender;
        private string? _Address;

        public clsPersonUiModel()
        {
            _ID = -1;
            _FirstName = "";
            _LastName = "";
            _Age = default;
            _Phone = null;
            _Email = null;
            _Gender = "";
            _Address = null;
        }

        public clsPerson ToPerson()
        {
            return new clsPerson
            {
                PersonID = this.ID,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Age = this.Age,
                Gender = this.Gender,
                Phone = this.Phone,
                Email = this.Email,
                Address = this.Address
            };
        }


        public int ID
        {
            get => _ID;
            set { _ID = value; OnPropertyChanged(); }
        }

        public string FirstName
        {
            get => _FirstName;
            set { _FirstName = value; OnPropertyChanged();/* For updating FirstName in FullName */ OnPropertyChanged(nameof(FullName)); }
        }

        public string LastName
        {
            get => _LastName;
            set { _LastName = value; OnPropertyChanged();/* For updating LastName in FullName */ OnPropertyChanged(nameof(FullName)); }
        }

        public int Age
        {
            get => _Age;
            set { _Age = value; OnPropertyChanged(); }
        }

        public string? Phone
        {
            get => _Phone;
            set { _Phone = value; OnPropertyChanged(); }
        }

        public string? Email
        {
            get => _Email;
            set { _Email = value; OnPropertyChanged(); }
        }

        public string Gender
        {
            get => _Gender;
            set { _Gender = value; OnPropertyChanged(); }
        }

        public string? Address
        {
            get => _Address;
            set { _Address = value; OnPropertyChanged(); }
        }

        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
