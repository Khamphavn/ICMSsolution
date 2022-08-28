using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using ICMS.HelperFunction;


namespace ICMS.Converter
{
    public class StringToFormatttedStringConverter : IValueConverter
    {
        // Data to UI
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringNumbers = (string)value;
            if (!string.IsNullOrEmpty(stringNumbers))
            {
                List<double> numbers = ConvertStringNumberHelper.ConvertStringToListDouble(stringNumbers); // convert string to list of double 

                //numbers = ConvertStringNumberHelper.RoundListDouble(numbers);   // round all double values

                string result = ConvertStringNumberHelper.ConvertListDoubleToFormattedString(numbers);

                return result;

            }

            return string.Empty;

        }

        // UI to Data
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringNumbers = (string)value;
            if (!string.IsNullOrEmpty(stringNumbers))
            {
                List<double> numbers = ConvertStringNumberHelper.ConvertStringToListDouble(stringNumbers); // convert string to list of double 

                //numbers = ConvertStringNumberHelper.RoundListDouble(numbers);   // round all double values

                string result = ConvertStringNumberHelper.ConvertListDoubleToFormattedString(numbers);

                return result;

            }

            return string.Empty;
        }
    }
}
