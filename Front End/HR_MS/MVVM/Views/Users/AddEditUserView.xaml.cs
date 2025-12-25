using HR_MS.MVVM.ViewModels.Users;
using System.Windows;

namespace HR_MS.MVVM.Views.Users
{
    /// <summary>
    /// Interaction logic for AddEditUserView.xaml
    /// </summary>
    public partial class AddEditUserView : Window
    {
        public AddEditUserView()
        {
            InitializeComponent();

            Loaded += AddEditUserView_Loaded;
        }


        public void AddEditUserView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is AddEditUserViewModel VM)
            {
                VM.RequestClose += () => this.Close();
            }
        }
    }
}
