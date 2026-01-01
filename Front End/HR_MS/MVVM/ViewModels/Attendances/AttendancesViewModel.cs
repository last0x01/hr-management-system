using Back_End.Models;
using Business_Layer.Interfaces;
using Business_Layer.Interfaces.Services;
using Business_Layer.Services;
using HR_MS.MVVM.Commands;
using HR_MS.MVVM.Models;
using HR_MS.MVVM.Views.Attendances;
using HR_MS.Services;
using HR_MS.Utilities;
using HR_MS.Utilities.Enums;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HR_MS.MVVM.ViewModels.Attendances
{
    public class AttendancesViewModel : clsNotifyObject
    {
        private clsAttendanceUiModel? _SelectedAttendance;

        private readonly IAttendanceService _AttendanceService;
        private readonly IDialogService _DialogService;

        public ObservableCollection<clsAttendanceUiModel> Attendances { get; } = new();

        public clsAttendanceUiModel? SelectedAttendance
        {
            get => _SelectedAttendance;
            set
            {
                _SelectedAttendance = value;
                OnPropertyChanged();
            }
        }

        private readonly IEmployeeService _EmployeeService;



        public ICommand RefreshAttendancesCommand { get; }
        public ICommand AddAttendanceCommand { get; }
        public ICommand EditAttendanceCommand { get; }
        public ICommand DeleteAttendanceCommand { get; }

        public ICommand ExportTodayCommand { get; }

        public AttendancesViewModel()
        {
            _DialogService = new DialogService();
            _AttendanceService = new AttendanceService();
            _EmployeeService = new EmployeeService();

            RefreshAttendancesCommand = new RelayCommand(o => _LoadAttendances());
            AddAttendanceCommand = new RelayCommand(o => _AddAttendance());
            EditAttendanceCommand = new RelayCommand(o => _UpdateAttendance());
            DeleteAttendanceCommand = new RelayCommand(o => _DeleteAttendance());
            ExportTodayCommand = new RelayCommand(o => _ExportToday());

            _LoadAttendances();
        }

        private void _ExportToday()
        {

            _DialogService.ShowMessage("Not implemented yet.", enMessageType.Info);

        }

        private void _AddAttendance()
        {
            AddEditAttendanceViewModel viewModel = new AddEditAttendanceViewModel();
            AddEditAttendanceView window = new AddEditAttendanceView
            {
                DataContext = viewModel
            };

            window.ShowDialog();
            _LoadAttendances();
        }

        private void _UpdateAttendance()
        {
            if (SelectedAttendance == null)
                return;

            AddEditAttendanceViewModel viewModel =
                new AddEditAttendanceViewModel(SelectedAttendance);

            AddEditAttendanceView window = new AddEditAttendanceView
            {
                DataContext = viewModel
            };

            window.ShowDialog();
            _LoadAttendances();
        }

        private void _DeleteAttendance()
        {
            if (SelectedAttendance == null)
                return;

            if (_AttendanceService.DeleteAttendance(SelectedAttendance.AttendanceID))
            {
                _DialogService.ShowMessage("Deleted successfully", enMessageType.Success);
                _LoadAttendances();
                SelectedAttendance = null;
            }
            else
            {
                _DialogService.ShowMessage("Failed to delete", enMessageType.Error);
            }
        }

        private void _LoadAttendances()
        {
            //This is Temp Function Because it is very slow
            Attendances.Clear();

            List<clsEmployee> EmpList = _EmployeeService.GetAllEmployees();

            List<Back_End.Models.clsAttendance> AttList = _AttendanceService.GetAllAttendances();

            var query = from A in AttList
                        join E in EmpList
                        on A.EmployeeID equals E.EmployeeID into AttGroup
                        from E in AttGroup.DefaultIfEmpty()
                        select new clsAttendanceUiModel(A)
                        {
                            EmployeeName = (E != null) ? $"{E.Person.FirstName} {E.Person.LastName}" : ""
                        };

            foreach (var attendance in query)
            {
                Attendances.Add(attendance);
            }
        }

        public void OnNavigatedTo()
        {
            SelectedAttendance = null;
        }

    }
}
