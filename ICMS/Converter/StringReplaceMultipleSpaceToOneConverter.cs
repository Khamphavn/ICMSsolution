using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;


namespace ICMS.Converter
{
    public class StringReplaceMultipleSpaceToOneConverter : IValueConverter
    {
        // Data to UI
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = (string)value;

            if (string.IsNullOrWhiteSpace(str))
            {
                return "";
            }
            else
            {
                return Regex.Replace(str, @"\s+", " ");
            }
        }

        // UI to Data
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = (string)value;

            if (string.IsNullOrWhiteSpace(str))
            {
                return "";
            }
            else
            {
                return Regex.Replace(str, @"\s+", " ");
            }
        }
    }
}
