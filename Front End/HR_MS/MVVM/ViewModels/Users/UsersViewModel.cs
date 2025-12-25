using Back_End.Models;
using Business_Layer;
using Business_Layer.Interfaces;
using HR_MS.MVVM.Commands;
using HR_MS.MVVM.Models;
using HR_MS.MVVM.Views.Users;
using HR_MS.Services;
using HR_MS.Utilities;
using HR_MS.Utilities.Enums;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HR_MS.MVVM.ViewModels.Users
{
    public class UsersViewModel : clsNotifyObject
    {
        private clsUserUiModel? _SelectedUser;
        private IDialogService _DialogService;
        private IUserService _UserService;
        public ObservableCollection<clsUserUiModel> Users { get; } = new();



        public clsUserUiModel? SelectedUser
        {
            get => _SelectedUser;
            set
            {
                _SelectedUser = value; OnPropertyChanged();
            }
        }

        public ICommand AddUserCommand { get; }
        public ICommand EditUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand RefreshUserCommand { get; }
        public UsersViewModel()
        {
            _DialogService = new DialogService();
            _UserService = new UserService();

            AddUserCommand = new RelayCommand(_ => _AddUser());
            EditUserCommand = new RelayCommand(_ => _UpdateUser());
            DeleteUserCommand = new RelayCommand(_ => _DeleteUser());
            RefreshUserCommand = new RelayCommand(_ => _LoadUsers());

            _LoadUsers();
        }

        private void _AddUser()
        {
            AddEditUserViewModel VM = new AddEditUserViewModel();

            AddEditUserView View = new AddEditUserView
            {
                DataContext = VM
            };

            View.ShowDialog();
            _LoadUsers();

        }
        private void _UpdateUser()
        {
            if (SelectedUser == null)
                return;

            AddEditUserViewModel VM = new AddEditUserViewModel(SelectedUser);

            AddEditUserView View = new AddEditUserView
            {
                DataContext = VM
            };

            View.ShowDialog();
            _LoadUsers();
        }
        private void _DeleteUser()
        {
            if (SelectedUser == null)
                return;

            if (_UserService.DeleteUser(SelectedUser.UserID))
            {
                _DialogService.ShowMessage("Deleted successfully", enMessageType.Success);
                _LoadUsers();
                SelectedUser = null;
            }
            else
                _DialogService.ShowMessage("Failed to delete", enMessageType.Error);
        }
        private void _LoadUsers()
        {
            Users.Clear();

            List<clsUser> List = _UserService.GetAllUsers();


            foreach (clsUser user in List)
            {
                Users.Add(new clsUserUiModel(user));
            }

        }
    }
}
