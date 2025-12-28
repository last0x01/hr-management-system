using Business_Layer;
using Business_Layer.Interfaces;
using HR_MS.MVVM.Commands;
using HR_MS.MVVM.Views.Attendances;
using HR_MS.MVVM.Views.Departments;
using HR_MS.MVVM.Views.Employees;
using HR_MS.MVVM.Views.Home;
using HR_MS.MVVM.Views.Logins;
using HR_MS.MVVM.Views.Users;
using HR_MS.Utilities;
using System.Windows.Input;

namespace HR_MS
{
    public class MainWindowViewModel : clsNotifyObject
    {
        private IUserSession _UserSession = UserSession.UserInstance;
        public event Action? RequestLogout;
        public ICommand LogoutCommand { get; }


        object? _CurrentView;

        public object? CurrentView
        {
            get => _CurrentView;

            set { _CurrentView = value; OnPropertyChanged(); }
        }

        public ICommand ShowEmployeesView { get; }
        public ICommand ShowUsersView { get; }
        public ICommand ShowHomeView { get; }
        public ICommand ShowDepartmentsView { get; }
        public ICommand ShowAttendancesView { get; }
        public MainWindowViewModel()
        {
            CurrentView = new HomeView();

            ShowHomeView = new RelayCommand(o => CurrentView = new HomeView());
            ShowEmployeesView = new RelayCommand(o => CurrentView = new EmployeesView());
            ShowAttendancesView = new RelayCommand(o => CurrentView = new AttendancesView());
            ShowUsersView = new RelayCommand(o => CurrentView = new UsersView());
            ShowDepartmentsView = new RelayCommand(o => CurrentView = new DepartmentsView());

            LogoutCommand = new RelayCommand(o => _Logout());


        }

        private void _Logout()
        {
            _UserSession.Logout();
            NavigateToLogIn();

        }

        public void NavigateToLogIn()
        {
            LoginView login = new LoginView();
            login.Show();
            RequestLogout?.Invoke();
        }

    }
}
