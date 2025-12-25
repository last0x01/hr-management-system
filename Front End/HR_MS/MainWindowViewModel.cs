using HR_MS.MVVM.Commands;
using HR_MS.MVVM.Views.Attendances;
using HR_MS.MVVM.Views.Departments;
using HR_MS.MVVM.Views.Employees;
using HR_MS.MVVM.Views.Home;
using HR_MS.MVVM.Views.Users;
using HR_MS.Utilities;
using System.Windows.Input;

namespace HR_MS
{
    public class MainWindowViewModel : clsNotifyObject
    {
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


        }

    }
}
