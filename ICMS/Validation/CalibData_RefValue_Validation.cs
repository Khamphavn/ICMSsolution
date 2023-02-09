using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ICMS.Validation
{
    public class CalibData_RefValue_Validation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var stringNumber = value as String;

            if (string.IsNullOrEmpty(stringNumber)){
                return new ValidationResult(false, $"Must be a positive number");
            }

            stringNumber.Trim();

            var isNumeric = double.TryParse(stringNumber, out double number);

            if (!isNumeric)
            {
                return new ValidationResult(false, $"Must be a positive number");
            }
            else
            {
                if (number <= 0)
                {
                    return new ValidationResult(false, $"Must be a positive number");
                }
            }

            return ValidationResult.ValidResult;

        }
    }
}
