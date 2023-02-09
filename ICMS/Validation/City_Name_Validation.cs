using System.Globalization;
using System.Windows.Controls;

namespace ICMS.Validation
{
    public class City_Name_Validation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string str = (string)value;

            return string.IsNullOrWhiteSpace(str)
           ? new ValidationResult(false, "Field is required")
           : new ValidationResult(true, null);
        }
    }
}
