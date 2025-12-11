using Front_End.HR_MS.MVVM.ViewModels.Employees;
using System.Windows;

namespace HR_MS.MVVM.Views.Employees
{
    /// <summary>
    /// Interaction logic for EmployeesView.xaml
    /// </summary>
    public partial class EmployeesView : Window
    {
        public EmployeesView()
        {
            InitializeComponent();

            DataContext = new EmployeesViewModel();
        }
    }
}
