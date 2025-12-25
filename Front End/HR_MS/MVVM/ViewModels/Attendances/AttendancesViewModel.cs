using Back_End.Models;
using Business_Layer;
using Business_Layer.Interfaces;
using HR_MS.MVVM.Commands;
using HR_MS.MVVM.Models;
using HR_MS.MVVM.Views.Attendances;
using HR_MS.Services;
using HR_MS.Utilities;
using HR_MS.Utilities.Enums;
using System.Collections.ObjectModel;

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

        public RelayCommand RefreshAttendancesCommand { get; }
        public RelayCommand AddAttendanceCommand { get; }
        public RelayCommand EditAttendanceCommand { get; }
        public RelayCommand DeleteAttendanceCommand { get; }

        public AttendancesViewModel()
        {
            _DialogService = new DialogService();
            _AttendanceService = new AttendanceService();

            RefreshAttendancesCommand = new RelayCommand(o => _LoadAttendances());
            AddAttendanceCommand = new RelayCommand(o => _AddAttendance());
            EditAttendanceCommand = new RelayCommand(o => _UpdateAttendance());
            DeleteAttendanceCommand = new RelayCommand(o => _DeleteAttendance());

            _LoadAttendances();
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
            Attendances.Clear();

            List<Back_End.Models.clsAttendance> list =
                _AttendanceService.GetAllAttendances();

            foreach (clsAttendance attendance in list)
            {
                Attendances.Add(new clsAttendanceUiModel(attendance));
            }
        }
    }
}
