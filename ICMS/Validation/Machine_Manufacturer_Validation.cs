using System.Globalization;
using System.Windows.Controls;


namespace ICMS.Validation
{
    public class Machine_Manufacturer_Validation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                 ? new ValidationResult(false, "Field is required.")
                 : ValidationResult.ValidResult;
        }
    }
}
