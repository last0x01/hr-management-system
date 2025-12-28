using HR_MS.MVVM.ViewModels.Logins;
using System.Windows;

namespace HR_MS.MVVM.Views.Logins
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {

        public LoginView()
        {
            InitializeComponent();

            Loaded += LoginView_Loaded;


        }


        private void LoginView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
            {
                vm.RequestLogIn += () => this.Close();
            }
        }
    }
}
