using System;
using System.Globalization;
using System.Windows.Controls;

namespace ICMS.Validation
{
    public class Customer_FullName_Validation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return value == null
                ? new ValidationResult(false, "Please select one")
                : ValidationResult.ValidResult;
        }
    }
}
