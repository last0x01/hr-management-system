using Back_End.Models;
using Business_Layer;
using Business_Layer.Interfaces;
using HR_MS.MVVM.Commands;
using HR_MS.Services;
using HR_MS.Utilities;
using System.Windows.Controls;
using System.Windows.Input;

namespace HR_MS.MVVM.ViewModels.Logins
{
    public class LoginViewModel : clsNotifyObject
    {
        public event Action? RequestLogIn;
        private IUserSession _UserSession = UserSession.UserInstance;
        private IUserService _UserService;
        private IDialogService _DialogService;
        private bool _IsLoading;

        private string? _Username;

        public bool IsLoading
        {
            get => _IsLoading;

            set
            {
                _IsLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        public ICommand SignInCommand { get; }
        public string? Username
        {
            get => _Username;

            set
            {
                _Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public LoginViewModel()
        {

            _UserService = new UserService();
            _DialogService = new DialogService();

            SignInCommand = new RelayCommand(async param => await _Login(param));
        }


        private async Task _Login(object Parameter)
        {
            if (Parameter is not PasswordBox passBox)
                return;
            string Password = passBox.Password;



            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                _DialogService.ShowMessage("Please enter username and password.", Utilities.Enums.enMessageType.Warning);
                return;
            }

            IsLoading = true;

            clsUser? User = await Task.Run(() => _UserService.GetUserByUsernameAndPassword(Username, Password));

            await Task.Delay(1000);

            if (User == null)
            {
                _DialogService.ShowMessage("Login Failed!", Utilities.Enums.enMessageType.Error);
                return;
            }

            _UserSession.Login(User);

            IsLoading = false;

            NavigateToMain();

        }


        public void NavigateToMain()
        {
            MainWindow main = new MainWindow();
            main.Show();
            RequestLogIn?.Invoke();
        }



    }
}
