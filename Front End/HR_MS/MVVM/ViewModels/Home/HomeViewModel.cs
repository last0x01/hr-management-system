using Business_Layer.Interfaces;
using Business_Layer.Interfaces.Services;
using Business_Layer.Services;
using HR_MS.Utilities;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace HR_MS.MVVM.ViewModels.Home
{
    public class HomeViewModel : clsNotifyObject
    {
        private readonly IEmployeeService _EmployeeService;
        private readonly IAttendanceService _AttendanceService;
        private readonly IAbsenceService _AbsenceService;

        private int _EmployeeCount;
        private int _PresentCount;
        private int _LateCount;
        private int _AbsentCount;


        public int EmployeeCount
        {
            get => _EmployeeCount;
            set
            {
                _EmployeeCount = value;
                OnPropertyChanged();
            }

        }
        public int PresentCount
        {
            get => _PresentCount;
            set
            {
                _PresentCount = value;
                OnPropertyChanged();
            }

        }
        public int LateCount
        {
            get => _LateCount;
            set
            {
                _LateCount = value;
                OnPropertyChanged();
            }

        }
        public int AbsentCount
        {
            get => _AbsentCount;
            set
            {
                _AbsentCount = value;
                OnPropertyChanged();
            }

        }

        private PlotModel _AttendancePlotModel;
        public PlotModel AttendancePlotModel
        {
            get => _AttendancePlotModel;
            set
            {
                _AttendancePlotModel = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel()
        {
            _EmployeeService = new EmployeeService();
            _AttendanceService = new AttendanceService();
            _AbsenceService = new AbsenceService();

            _AttendancePlotModel = new();
            _LoadDashboard();
            _BuildAttendanceChart();
        }

        private void _LoadDashboard()
        {
            EmployeeCount = _EmployeeService.GetEmployeeCount();
            PresentCount = _AttendanceService.GetTodayPresentCount();
            LateCount = _AttendanceService.GetTodayLateCount(new TimeOnly(9, 0, 0));// It is mean the clock in 9 AM
            AbsentCount = _AbsenceService.GetTodayAbsenceCount();
        }

        private void _BuildAttendanceChart()
        {
            PlotModel model = new PlotModel { Title = "Today Attendance" };



            BarSeries series = new BarSeries
            {
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0}",
                StrokeThickness = 1
            };


            series.Items.Add(new BarItem { Value = PresentCount, Color = OxyColor.FromRgb(0, 150, 0) });
            series.Items.Add(new BarItem { Value = LateCount, Color = OxyColor.FromRgb(255, 215, 0) });
            series.Items.Add(new BarItem { Value = AbsentCount, Color = OxyColor.FromRgb(200, 0, 0) });


            model.Series.Add(series);


            model.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Left,
                ItemsSource = new[] { "Present", "Late", "Absent" },
                FontSize = 16,
                TextColor = OxyColors.DarkSlateGray,
                GapWidth = 0.5
            });


            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Title = "Count",
                TitleFontSize = 14,
                FontSize = 14,
                TextColor = OxyColors.DarkSlateGray
            });


            AttendancePlotModel = model;
        }

        public void RefreshDashboard()
        {
            _LoadDashboard();
            _BuildAttendanceChart();
        }
    }

}
