using System.Windows;

namespace HR_MS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {


            if (DataContext is MainWindowViewModel Vm)
            {
                Vm.RequestLogout += () => this.Close();
            }
        }
    }
}