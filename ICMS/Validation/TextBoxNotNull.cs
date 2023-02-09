using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ICMS.Validation
{
    public class TextBoxNotNull : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            //bool success = String.IsNullOrWhiteSpace((string)value);

            return String.IsNullOrWhiteSpace((string)value)
           ? new ValidationResult(false, "Field is required")
           : new ValidationResult(true, null);
        }
    }
}
