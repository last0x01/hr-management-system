using Back_End.Models;
using Business_Layer.Interfaces;
using Business_Layer.Interfaces.Services;
using Business_Layer.Services;
using HR_MS.MVVM.Commands;
using HR_MS.MVVM.Models;
using HR_MS.MVVM.Views.Absences;
using HR_MS.Services;
using HR_MS.Utilities;
using HR_MS.Utilities.Enums;
using Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HR_MS.MVVM.ViewModels.Absences
{
    public class AbsencesViewModel : clsNotifyObject
    {
        private clsAbsenceUiModel? _SelectedAbsence;

        private readonly IAbsenceService _AbsenceService;
        private readonly IDialogService _DialogService;
        private readonly IEmployeeService _EmployeeService;

        public ObservableCollection<clsAbsenceUiModel> Absences { get; } = new();

        public clsAbsenceUiModel? SelectedAbsence
        {
            get => _SelectedAbsence;
            set
            {
                _SelectedAbsence = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshAbsencesCommand { get; }
        public ICommand AddAbsenceCommand { get; }
        public ICommand EditAbsenceCommand { get; }
        public ICommand DeleteAbsenceCommand { get; }
        public ICommand ExportTodayCommand { get; }

        public AbsencesViewModel()
        {
            _AbsenceService = new AbsenceService();
            _DialogService = new DialogService();
            _EmployeeService = new EmployeeService();

            RefreshAbsencesCommand = new RelayCommand(o => _LoadAbsences());
            AddAbsenceCommand = new RelayCommand(o => _AddAbsence());
            EditAbsenceCommand = new RelayCommand(o => _UpdateAbsence());
            DeleteAbsenceCommand = new RelayCommand(o => _DeleteAbsence());
            ExportTodayCommand = new RelayCommand(o => _ExportToday());


            _LoadAbsences();
        }

        private void _ExportToday()
        {
            _DialogService.ShowMessage("Not implemented yet.", enMessageType.Info);
        }

        private void _AddAbsence()
        {
            AddEditAbsencesViewModel vm = new AddEditAbsencesViewModel();
            AddEditAbsencesView View = new AddEditAbsencesView
            {
                DataContext = vm
            };

            View.ShowDialog();
            _LoadAbsences();
        }
        private void _UpdateAbsence()
        {
            if (SelectedAbsence == null)
            {
                return;
            }

            AddEditAbsencesViewModel vm = new AddEditAbsencesViewModel(SelectedAbsence);
            AddEditAbsencesView View = new AddEditAbsencesView
            {
                DataContext = vm
            };

            View.ShowDialog();
            _LoadAbsences();
        }
        private void _LoadAbsences()
        {
            //This is Temp Function Because it is very slow

            Absences.Clear();

            List<clsAbsence> AbsList = _AbsenceService.GetAllAbsences();
            List<clsEmployee> EmpList = _EmployeeService.GetAllEmployees();

            var query = from A in AbsList
                        join E in EmpList
                        on A.EmployeeID equals E.EmployeeID into AbsGroup
                        from E in AbsGroup.DefaultIfEmpty()
                        select new clsAbsenceUiModel(A)
                        {
                            EmployeeName = (E != null) ? $"{E.Person.FirstName} {E.Person.LastName}" : ""
                        };

            foreach (var abs in query)
            {
                Absences.Add(abs);
            }


        }
        private void _DeleteAbsence()
        {
            _DialogService.ShowMessage("Not implement it yet.", Utilities.Enums.enMessageType.Info);
        }

        public void OnNavigatedTo()
        {
            SelectedAbsence = null;
        }

    }
}
