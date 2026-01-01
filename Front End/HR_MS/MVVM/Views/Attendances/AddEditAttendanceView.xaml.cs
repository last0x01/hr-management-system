using HR_MS.MVVM.ViewModels.Attendances;
using System.Windows;

namespace HR_MS.MVVM.Views.Attendances
{
    /// <summary>
    /// Interaction logic for AddEditAttendanceView.xaml
    /// </summary>
    public partial class AddEditAttendanceView : Window
    {
        public AddEditAttendanceView()
        {
            InitializeComponent();

            Loaded += AddEditAttendanceView_Loaded;
        }

        private void AddEditAttendanceView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is AddEditAttendanceViewModel vm)
            {
                vm.RequestClose += () => this.Close();
            }
        }
    }
}
