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
            return value;

        }

        // UI to Data
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //string stringNumber = (string)value;
            //if (!string.IsNullOrEmpty(stringNumber))
            //{
            //    stringNumber = String.Concat(stringNumber.Where(c => !Char.IsWhiteSpace(c))); // remove all white space

            //    double.TryParse(stringNumber, out double number);

            //    stringNumber = String.Format("{0:0.0}", number); // format

            //    return stringNumber;
            //}
            //return Binding.DoNothing;
            return value;
        }
    }
}
