using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ICMS.Validation
{
    public class DoseQuantity_Name_Validation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string name = (string)value;

            if (string.IsNullOrWhiteSpace(name))
            {
                    return new ValidationResult(false, "Field is required");
            }

            return ValidationResult.ValidResult;
        }
    }
}
