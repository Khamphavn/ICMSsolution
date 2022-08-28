using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace ICMS.Converter
{
    public class InverseBoolToVisibilityConverter : IValueConverter
    {
        // Data to UI
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool)value;
            return !boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        // UI to Data
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
