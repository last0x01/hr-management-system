using HR_MS.MVVM.ViewModels.Employees;
using System.Windows;

namespace HR_MS.MVVM.Views.Employees
{
    /// <summary>
    /// Interaction logic for AddEditEmployeeView.xaml
    /// </summary>
    public partial class AddEditEmployeeView : Window
    {
        public AddEditEmployeeView()
        {
            InitializeComponent();


            Loaded += AddEditEmployeeView_Loaded;

        }

        private void AddEditEmployeeView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is AddEditEmployeeViewModel vm)
            {
                vm.RequestClose += () => this.Close();
            }
        }

    }
}
