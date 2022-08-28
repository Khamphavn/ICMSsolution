using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;


namespace ICMS.Converter
{
    public class StringRemoveWhiteSpaceConverter : IValueConverter
    {
        // Data to UI
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return ""; }

            if (value.GetType() == typeof(string))
            {
                string str = (string)value;

                if (string.IsNullOrWhiteSpace(str))
                {
                    return "";
                }
                else
                {
                    return Regex.Replace(str, @"\s+", "");
                }
            }
            else if (value.GetType() == typeof(double))
            {
                return value.ToString();
            }
            else
            {
                throw new Exception();
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
                string stringReturn = Regex.Replace(str, @"\s+", "");
                return Regex.Replace(str, @"\s+", "");
            }
        }
    }
}
