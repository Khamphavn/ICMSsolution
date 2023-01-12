using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ICMS.Validation
{
    public class User_Role_Validation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(false, "Field is required.");
            }

            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                ? new ValidationResult(false, "Field is required.")
                : ValidationResult.ValidResult;
        }
    }
}
