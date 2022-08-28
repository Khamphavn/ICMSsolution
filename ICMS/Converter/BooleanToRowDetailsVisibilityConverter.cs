using System;
using System.Globalization;
using System.Windows.Data;

namespace ICMS.Converter
{
    public class BooleanToRowDetailsVisibilityConverter : IValueConverter
    {
        // Data to UI
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool ShowRowDetails = (bool)value;

            return ShowRowDetails ? "VisibleWhenSelected" : "Collapsed";
        }

        // UI to Data
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string RowDetailsVisibilityMode = (string)value;

            if (RowDetailsVisibilityMode == "VisibleWhenSelected")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
