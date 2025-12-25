using HR_MS.MVVM.ViewModels.Message;
using System.Windows;

namespace HR_MS.MVVM.Views
{
    /// <summary>
    /// Interaction logic for MessageDialogView.xaml
    /// </summary>
    public partial class MessageDialogView : Window
    {


        public MessageDialogView(clsMessageViewModel VM)
        {
            InitializeComponent();

            DataContext = VM;

            VM.RequestClose += () => this.Close();
        }
    }
}
