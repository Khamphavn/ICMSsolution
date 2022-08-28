using System.Globalization;
using System.Windows.Controls;

namespace ICMS.Validation
{
    public class ComboBoxNotNull_Validation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return value == null
           ? new ValidationResult(false, "Please select one")
           : new ValidationResult(true, null);
        }
    }
}
