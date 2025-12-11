using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Front_End.HR_MS.Utilities
{
    public class clsNotifyObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string PropertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
