using System;
using System.Globalization;
using System.Windows.Data;


namespace ICMS.Converter
{
    /// <summary>
    /// Convert number string to 1 decimal point
    /// </summary>
    public class OneDecimalText_Converter : IValueConverter
    {
        // Data to UI
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string stringValue = (string)value;
                double number = double.Parse(stringValue);

                number = Math.Round(number, 1, MidpointRounding.AwayFromZero);

                return number;
                
            }
            
            return value;

        }

        // UI to Data
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string stringValue = (string)value;
                double number = double.Parse(stringValue);

                number = Math.Round(number, 1, MidpointRounding.AwayFromZero);

                return number;

            }

            return value;
        }
    }
}
