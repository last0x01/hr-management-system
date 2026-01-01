using Back_End.Models;
using Business_Layer;
using Business_Layer.Interfaces;
using Business_Layer.Interfaces.Services;
using Business_Layer.Services;
using Business_Layer.Validations;
using HR_MS.MVVM.Commands;
using HR_MS.MVVM.Models;
using HR_MS.Services;
using HR_MS.Utilities;
using HR_MS.Utilities.Enums;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HR_MS.MVVM.ViewModels.Absences
{
    public class AddEditAbsencesViewModel : clsNotifyObject
    {
        private clsAbsenceUiModel _Absence;
        private readonly IAbsenceService _AbsenceService;

        public clsAbsenceUiModel Absence
        {
            get => _Absence;

            set
            {
                _Absence = value;
                OnPropertyChanged();
            }
        }

        private clsEmployeeUiModel? _SelectedEmployee;
        private readonly IEmployeeService _EmployeeService;

        public clsEmployeeUiModel? SelectedEmployee
        {
            get => _SelectedEmployee;

            set
            {
                _SelectedEmployee = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<clsEmployeeUiModel> Employees { get; } = new();

        private clsAbsenceTypeUiModel? _SelectedAbsenceType;
        private readonly IAbsenceTypeService _AbsenceTypeService;

        public clsAbsenceTypeUiModel? SelectedAbsenceType
        {
            get => _SelectedAbsenceType;

            set
            {
                _SelectedAbsenceType = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<clsAbsenceTypeUiModel> AbsenceTypes { get; } = new();

        private string _Username = UserSession.UserInstance.CurrentUser!.Username;
        private int _UserID = UserSession.UserInstance.CurrentUser!.UserID;


        private string? _Title;
        public string? Title
        {
            get => _Title;
            set
            {
                _Title = value;
                OnPropertyChanged();
            }
        }


        private enum enMode { Add = 1, Update = 2 }
        private enMode _Mode = enMode.Add;
        private enMode Mode
        {
            get => _Mode;

            set
            {
                _Mode = value;
                OnPropertyChanged(nameof(Mode));
                // This will make IsNewAbsence change depending on the Mode
                OnPropertyChanged(nameof(IsNewAbsence));
            }
        }

        public bool IsNewAbsence => _Mode == enMode.Add;

        public string Username
        {
            get => _Username;
        }

        public event Action? RequestClose;

        private readonly IDialogService _DialogService;

        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand RefreshEmployeesCommand { get; }
        public ICommand RefreshAbsenceTypesCommand { get; }


        public AddEditAbsencesViewModel()
        {

            _DialogService = new DialogService();
            _AbsenceService = new AbsenceService();
            _AbsenceTypeService = new AbsenceTypeService();
            _EmployeeService = new EmployeeService();

            SaveCommand = new RelayCommand(o => _Save());
            CloseCommand = new RelayCommand(o => _Close());
            RefreshEmployeesCommand = new RelayCommand(o => _LoadEmployeesToComboBox());
            RefreshAbsenceTypesCommand = new RelayCommand(o => _LoadAbsenceTypesToComboBox());

            this._Absence = new clsAbsenceUiModel();

            _Mode = enMode.Add;
            _Title = "Add Absence";

            _LoadAbsenceTypesToComboBox();
            _LoadEmployeesToComboBox();

            Absence.AbsenceDate = DateTime.Now;

        }

        public AddEditAbsencesViewModel(clsAbsenceUiModel Absence)
        {
            _DialogService = new DialogService();
            _AbsenceService = new AbsenceService();
            _AbsenceTypeService = new AbsenceTypeService();
            _EmployeeService = new EmployeeService();

            SaveCommand = new RelayCommand(o => _Save());
            CloseCommand = new RelayCommand(o => _Close());
            RefreshEmployeesCommand = new RelayCommand(o => _LoadEmployeesToComboBox());
            RefreshAbsenceTypesCommand = new RelayCommand(o => _LoadAbsenceTypesToComboBox());

            this._Absence = Absence;

            _Mode = enMode.Update;
            _Title = "Update Absence";

            _LoadAbsenceTypesToComboBox();
            _LoadEmployeesToComboBox();

            SelectedEmployee = Employees.FirstOrDefault(emp => emp.EmployeeID == Absence.EmployeeID);
            SelectedAbsenceType = AbsenceTypes.FirstOrDefault(absType => absType.AbsenceTypeID == Absence.AbsenceTypeID);
        }

        private bool _IsEmployeeSelected()
        {
            if (SelectedEmployee == null)
            {
                _DialogService.ShowMessage("Please Select an Employee", enMessageType.Warning);
                return false;
            }
            return true;
        }
        private bool _IsAbsenceTypeSelected()
        {
            if (SelectedAbsenceType == null)
            {
                _DialogService.ShowMessage("Please Select an Absence Type", enMessageType.Warning);
                return false;
            }
            return true;
        }

        private void _Close()
        {
            RequestClose?.Invoke();
        }

        private void _Save()
        {


            switch (_Mode)
            {
                case enMode.Add:
                    _AddAbsence();
                    break;

                case enMode.Update:
                    _UpdateAbsence();
                    break;
            }
        }

        private void _AddAbsence()
        {

            if (!_IsEmployeeSelected() || !_IsAbsenceTypeSelected())
                return;

            AbsenceValidationResult result = _AbsenceService.CanAddAbsence(SelectedEmployee!.EmployeeID);

            if (!result.CanAdd)
            {
                _DialogService.ShowMessage(result.Message!, enMessageType.Warning);
                return;
            }


            Absence.AbsenceTypeID = SelectedAbsenceType!.AbsenceTypeID;
            Absence.EmployeeID = SelectedEmployee!.EmployeeID;
            Absence.CreatedByUserID = _UserID;

            int AbsenceID = _AbsenceService.AddAndGetAbsenceID(Absence.ToAbsence());
            Absence.AbsenceID = AbsenceID;

            if (AbsenceID != -1)
            {
                _DialogService.ShowMessage("Absence added successfully", enMessageType.Success);
                Mode = enMode.Update;
                Title = "Update Absence";
            }
            else
                _DialogService.ShowMessage("Failed to add", enMessageType.Error);
        }

        private void _UpdateAbsence()
        {

            if (_AbsenceService.UpdateAbsence(Absence.ToAbsence()))
            {
                _DialogService.ShowMessage("Absence updated successfully", enMessageType.Success);
            }
            else
                _DialogService.ShowMessage("Failed to update", enMessageType.Error);
        }


        private void _LoadEmployeesToComboBox()
        {
            Employees.Clear();
            List<clsEmployee> EmployeesList = _EmployeeService.GetAllEmployees();

            foreach (clsEmployee emp in EmployeesList)
            {
                Employees.Add(new clsEmployeeUiModel(emp));
            }
        }

        private void _LoadAbsenceTypesToComboBox()
        {
            AbsenceTypes.Clear();
            List<clsAbsenceType> List = _AbsenceTypeService.GetAllAbsenceTypes();

            foreach (clsAbsenceType absType in List)
            {
                AbsenceTypes.Add(new clsAbsenceTypeUiModel(absType));
            }
        }
    }
}
