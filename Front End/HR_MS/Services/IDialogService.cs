using HR_MS.Utilities.Enums;

namespace HR_MS.Services
{
    public interface IDialogService
    {
        void ShowMessage(string Message, enMessageType MessageType);
    }
}
