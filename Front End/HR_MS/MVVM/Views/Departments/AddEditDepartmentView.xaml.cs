using HR_MS.MVVM.ViewModels.Departments;
using System.Windows;

namespace HR_MS.MVVM.Views.Departments
{
    /// <summary>
    /// Interaction logic for AddEditDepartmentView.xaml
    /// </summary>
    public partial class AddEditDepartmentView : Window
    {
        public AddEditDepartmentView()
        {
            InitializeComponent();

            Loaded += AddEditDepartmentView_Loaded;
        }

        public void AddEditDepartmentView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is AddEditDepartmentViewModel VM)
            {
                VM.RequestClose += () => this.Close();
            }
        }
    }
}
