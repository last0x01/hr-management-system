using Back_End.Models;
using Business_Layer;
using Business_Layer.Interfaces;
using Front_End.HR_MS.MVVM.ViewModels.Employees;
using HR_MS.MVVM.Commands;
using HR_MS.MVVM.ViewModels.Absences;
using HR_MS.MVVM.ViewModels.Attendances;
using HR_MS.MVVM.ViewModels.Departments;
using HR_MS.MVVM.ViewModels.Home;
using HR_MS.MVVM.ViewModels.Users;
using HR_MS.Utilities;
using System.Windows.Input;

namespace HR_MS.MVVM.ViewModels
{
    public class MainWindowViewModel : clsNotifyObject
    {

        public IUserSession _UserSession = UserSession.UserInstance;
        public event Action? RequestLogout;

        public clsUser? User
        {
            get => _UserSession.CurrentUser;
        }

        private object? _CurrentView;

        public object? CurrentView
        {
            get => _CurrentView;

            set
            {
                //null
                //This is use for delete the old content of the view before load the new one 
                //prevent showing the old content on the new view
                _CurrentView = null;
                OnPropertyChanged();


                _CurrentView = value;
                OnPropertyChanged();
            }
        }
        private HomeViewModel _HomeVM = new HomeViewModel();
        private EmployeesViewModel _EmployeesVM = new EmployeesViewModel();
        private UsersViewModel _UsersVM = new UsersViewModel();
        private DepartmentsViewModel _DepartmentsVM = new DepartmentsViewModel();
        private AttendancesViewModel _AttendancesVM = new AttendancesViewModel();
        private AbsencesViewModel _AbsencesVM = new AbsencesViewModel();

        private void _Home(object obj)
        {
            _HomeVM.RefreshDashboard();
            CurrentView = _HomeVM;

        }

        private void _Employees(object obj)
        {
            _EmployeesVM.OnNavigatedTo();
            CurrentView = _EmployeesVM;
        }
        private void _Users(object obj)
        {
            _UsersVM.OnNavigatedTo();
            CurrentView = _UsersVM;
        }
        private void _Departments(object obj)
        {
            _DepartmentsVM.OnNavigatedTo();
            CurrentView = _DepartmentsVM;
        }

        private void _Attendances(object obj)
        {

            _AttendancesVM.OnNavigatedTo();
            CurrentView = _AttendancesVM;
        }

        private void _Absences(object obj)
        {
            _AbsencesVM.OnNavigatedTo();
            CurrentView = _AbsencesVM;
        }

        public ICommand ShowEmployeesViewCommand { get; }
        public ICommand ShowUsersViewCommand { get; }
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowDepartmentsViewCommand { get; }
        public ICommand ShowAttendancesViewCommand { get; }
        public ICommand ShowAbsencesViewCommand { get; }

        public ICommand LogoutCommand { get; }

        public MainWindowViewModel()
        {

            ShowHomeViewCommand = new RelayCommand(_Home);
            ShowEmployeesViewCommand = new RelayCommand(_Employees);
            ShowAttendancesViewCommand = new RelayCommand(_Attendances);
            ShowUsersViewCommand = new RelayCommand(_Users);
            ShowDepartmentsViewCommand = new RelayCommand(_Departments);
            ShowAbsencesViewCommand = new RelayCommand(_Absences);

            LogoutCommand = new RelayCommand(o => _Logout());

            CurrentView = _HomeVM;

        }

        private void _Logout()
        {
            _UserSession.Logout();
            RequestLogout?.Invoke();

        }



    }
}
