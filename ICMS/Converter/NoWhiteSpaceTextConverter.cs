using System;
using System.Globalization;
using System.Windows.Data;


namespace ICMS.Converter
{
    public class NoWhiteSpaceTextConverter : IValueConverter
    {
        // Data to UI
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty((string)value))
            {
                return ((string)value).Trim();
            }
            return string.Empty;
        }

        // UI to Data
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty((string)value))
            {
                return ((string)value).Trim();
            }
            return string.Empty;
        }
    }
}
