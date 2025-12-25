using Business_Layer;
using Business_Layer.Interfaces;
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
            }
            else
                _DialogService.ShowMessage("Failed to Add", enMessageType.Error);
        }
        public void _Save()
        {
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
