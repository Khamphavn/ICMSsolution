using System;
using System.Globalization;
using System.Windows.Data;


namespace ICMS.Converter
{
    public class StringToNullableDecimalConverter : IValueConverter
    {
        // Data to UI
        public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture)
        {
            double? d = (double?)value;
            if (d.HasValue)
            {
                if (d.Value == 0)
                {
                    return String.Empty;
                }
                else
                {
                    return d.Value.ToString(culture);
                }
               
            }
                
            else
                return String.Empty;
        }

        // UI to Data
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            string s = (string)value;
            if (String.IsNullOrEmpty(s))
                return null;
            else
                return (double?)double.Parse(s, culture);
        }
    }
}
