using HR_MS.MVVM.ViewModels.Absences;
using System.Windows;

namespace HR_MS.MVVM.Views.Absences
{
    /// <summary>
    /// Interaction logic for AddEditAbsencesView.xaml
    /// </summary>
    public partial class AddEditAbsencesView : Window
    {
        public AddEditAbsencesView()
        {
            InitializeComponent();

            Loaded += _AddEditAbsencesView_Loaded;
        }

        private void _AddEditAbsencesView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is AddEditAbsencesViewModel vm)
            {
                vm.RequestClose += () => this.Close();
            }
        }
    }
}
