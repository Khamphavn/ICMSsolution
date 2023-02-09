using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ICMS.Validation
{
    public class RadQuantity_Energy_Validation :  ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string strNumber = (string)value;
            //var allowedChars = "1234567890.,";

            if (string.IsNullOrWhiteSpace(strNumber))
            {
                return new ValidationResult(false, "Field is required");
            }

            double number;
            if (double.TryParse(strNumber, out number) && number > 0.0 )
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Must be a positive number");
            }
        }
}
}
