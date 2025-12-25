using System.Globalization;
using System.Windows.Data;

namespace HR_MS.MVVM.Converters
{
    public class EnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // value = the current value from the ViewModel (e.g., SelectedGender.Male or SelectedGender.Female)
            // targetType = the type expected by the UI element (usually bool for RadioButton's IsChecked)
            // parameter = the value provided in XAML (e.g., "Male" or "Female") to determine which RadioButton is linked

            if (value == null || parameter == null)
                return false;

            string? enumString = parameter.ToString();
            return value.ToString() == enumString?.ToString();
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || value == null || !(bool)value)
                return Binding.DoNothing;

            return Enum.Parse(targetType, parameter.ToString()!);
        }
    }
}
