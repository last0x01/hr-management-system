using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HR_MS.MVVM.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        // Converts a boolean value from the ViewModel to a Visibility value for the UI
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If the value from the ViewModel is a boolean and true, return Visible
            // Otherwise, return Collapsed
            if (value is bool b && b)
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        // Converts a Visibility value back to a boolean (not commonly used in one-way bindings)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If the value is Visibility, return true if it is Visible, otherwise false
            if (value is Visibility v)
                return v == Visibility.Visible;

            return false;
        }
    }
}