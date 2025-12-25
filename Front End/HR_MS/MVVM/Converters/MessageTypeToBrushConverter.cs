using HR_MS.Utilities.Enums;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace HR_MS.MVVM.Converters
{
    public class MessageTypeToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not enMessageType type)
                return Brushes.LightGray;

            return type switch
            {
                enMessageType.Success => CreateBrush(
                    Color.FromRgb(210, 244, 203),
                    Color.FromRgb(122, 237, 101)),

                enMessageType.Warning => CreateBrush(
                    Color.FromRgb(255, 230, 207),
                    Color.FromRgb(255, 184, 56)),

                enMessageType.Error => CreateBrush(
                    Color.FromRgb(255, 200, 200),
                    Color.FromRgb(220, 60, 60)),

                _ => Brushes.LightGray
            };
        }

        private static LinearGradientBrush CreateBrush(Color c1, Color c2)
        {
            return new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0, 1),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(c1, 0),
                    new GradientStop(c2, 1)
                }
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
