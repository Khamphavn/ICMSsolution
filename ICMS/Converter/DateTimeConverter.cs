using System;
using System.Globalization;
using System.Windows.Data;

namespace ICMS.Converter
{
    public class DateTimeConverter : IValueConverter
    {
        // Data to UI
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            return ((DateTime)value).ToString("dd-MMM-yyyy");
        }

        // UI to Data
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            return ((DateTime)value).ToString("dd-MMM-yyyy");
        }
    }
}
