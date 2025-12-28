using HR_MS.MVVM.Views.Logins;
using System.Windows;

namespace HR_MS.Controllers
{
    public class ApplicationController
    {
        private static ApplicationController? _ControllerInstance;

        private Window? _CurrentWindow;

        public static ApplicationController ControllerInstance => _ControllerInstance ??= new ApplicationController();


        public void SetCurrentWindow(Window window)
        {
            _CurrentWindow = window;
        }

        public void NavigateToMain()
        {
            MainWindow main = new MainWindow();
            main.Show();
            _CurrentWindow?.Close();
            SetCurrentWindow(main);
        }

        public void NavigateToLogIn()
        {
            LoginView login = new LoginView();
            login.Show();
            _CurrentWindow?.Close();
            SetCurrentWindow(login);
        }

    }
}
