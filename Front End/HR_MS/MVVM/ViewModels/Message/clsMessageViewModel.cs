using HR_MS.MVVM.Commands;
using HR_MS.Utilities.Enums;
using System.Windows.Input;

namespace HR_MS.MVVM.ViewModels.Message
{
    public class clsMessageViewModel
    {
        public string Message { get; }
        public enMessageType MessageType { get; }

        public ICommand OKCommand { get; }

        public event Action? RequestClose;

        public clsMessageViewModel(string Message, enMessageType MessageType)
        {
            this.Message = Message;
            this.MessageType = MessageType;

            OKCommand = new RelayCommand(o => RequestClose?.Invoke());
        }


    }
}
