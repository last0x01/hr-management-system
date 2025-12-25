using HR_MS.MVVM.ViewModels.Message;
using HR_MS.MVVM.Views;
using HR_MS.Services;
using HR_MS.Utilities.Enums;


namespace Business_Layer.Interfaces
{
    public class DialogService : IDialogService
    {

        public void ShowMessage(string Message, enMessageType MessageType)
        {
            clsMessageViewModel VM = new clsMessageViewModel(Message, MessageType);

            MessageDialogView View = new MessageDialogView(VM);

            View.ShowDialog();

        }
    }
}
