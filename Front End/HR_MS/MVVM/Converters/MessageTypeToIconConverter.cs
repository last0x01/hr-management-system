using HR_MS.Utilities.Enums;
using MahApps.Metro.IconPacks;
using System.Globalization;
using System.Windows.Data;

namespace HR_MS.MVVM.Converters
{
    public class MessageTypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (enMessageType)value switch
            {
                enMessageType.Success => PackIconMaterialKind.CheckCircle,
                enMessageType.Error => PackIconMaterialKind.CloseCircle,
                enMessageType.Warning => PackIconMaterialKind.Alert,
                enMessageType.Info => PackIconMaterialKind.Information,
                _ => PackIconMaterialKind.Information

            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
