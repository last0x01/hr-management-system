using Business_Layer.Interfaces;
using Business_Layer.Interfaces.Services;
using Business_Layer.Services;
using HR_MS.MVVM.Commands;
using HR_MS.MVVM.Models;
using HR_MS.Services;
using HR_MS.Utilities;
using HR_MS.Utilities.Enums;
using System.Windows.Input;

namespace HR_MS.MVVM.ViewModels.Users
{
    public class AddEditUserViewModel : clsNotifyObject
    {
        private enGenderType _SelectedGender;
        private IDialogService _DialogService;
        private IUserService _UserService;
        private clsUserUiModel _User;
        private enum enMode { Add = 1, Update = 2 }
        private enMode _Mode = enMode.Add;
        private string _Title;

        public string Title
        {
            get => _Title;
            set
            {
                _Title = value;
                OnPropertyChanged();
            }
        }
        public clsUserUiModel User
        {
            get => _User;
            set
            {
                _User = value; OnPropertyChanged();
            }
        }

        public enGenderType SelectedGender
        {
            get => _SelectedGender;

            set
            {
                _SelectedGender = value;
                User.Person.Gender = GenderString;
                OnPropertyChanged();
            }
        }

        public string GenderString => (SelectedGender == enGenderType.Male) ? "Male" : "Female";
        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }

        public event Action? RequestClose;

        private void _UpdateUserGenderToUI()
        {
            SelectedGender = (User.Person.Gender == "Male") ? enGenderType.Male : enGenderType.Female;
        }

        public AddEditUserViewModel()
        {
            _User = new clsUserUiModel();

            _DialogService = new DialogService();
            _UserService = new UserService();

            SaveCommand = new RelayCommand(_ => _Save());
            CloseCommand = new RelayCommand(_ => _Close());

            SelectedGender = enGenderType.Male;

            _Title = "Add User";
            _Mode = enMode.Add;
        }
        public AddEditUserViewModel(clsUserUiModel User)
        {
            this._User = User;

            _DialogService = new DialogService();
            _UserService = new UserService();

            SaveCommand = new RelayCommand(_ => _Save());
            CloseCommand = new RelayCommand(_ => _Close());

            _UpdateUserGenderToUI();

            _Title = "Update User";
            _Mode = enMode.Update;
        }


        public void _UpdateUser()
        {
            if (_UserService.UpdateUser(User.ToUser()))
            {
                _DialogService.ShowMessage("User Updated successfully", enMessageType.Success);
            }
            else
                _DialogService.ShowMessage("Failed to Update", enMessageType.Error);
        }
        public void _AddUser()
        {
            if (_UserService.AddUser(User.ToUser()))
            {
                _DialogService.ShowMessage("User Added successfully", enMessageType.Success);
                _Mode = enMode.Update;
                _Title = "Update User";
            }
            else
                _DialogService.ShowMessage("Failed to Add", enMessageType.Error);
        }





        private bool _IsValidAge()
        {
            if (User?.Person == null)
            {
                _DialogService.ShowMessage("User information is missing.", enMessageType.Error);
                return false;
            }

            if (User.Person.Age == null)
            {
                _DialogService.ShowMessage("Age is not provided.", enMessageType.Error);
                return false;
            }

            if (User.Person.Age < 18)
            {
                _DialogService.ShowMessage("Age is below the minimum allowed (18 years).", enMessageType.Error);
                return false;
            }
            else if (User.Person.Age > 65)
            {
                _DialogService.ShowMessage("Age exceeds the maximum allowed (65 years).", enMessageType.Error);
                return false;
            }

            return true;
        }

        private bool _IsValidName(string Name, string Message)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                _DialogService.ShowMessage(Message, enMessageType.Error);
                return false;
            }

            return true;
        }

        private bool _IsValidUserNames()
        {
            if (!_IsValidName(User.Person.FirstName, "First name is required."))
                return false;
            if (!_IsValidName(User.Person.LastName, "Last name is required."))
                return false;

            return true;
        }
        public void _Save()
        {
            if (!_IsValidAge() || !_IsValidUserNames())
            {
                return;
            }


            switch (_Mode)
            {
                case enMode.Add:
                    _AddUser();
                    break;

                case enMode.Update:
                    _UpdateUser();
                    break;

            }
        }
        public void _Close()
        {
            RequestClose?.Invoke();
        }
    }
}
