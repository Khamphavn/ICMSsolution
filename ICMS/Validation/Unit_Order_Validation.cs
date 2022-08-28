using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ICMS.Validation
{
    public class Unit_Order_Validation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int number;

            bool isPositiveNumber = int.TryParse((string)value, out  number) && number >0;

            return isPositiveNumber ? ValidationResult.ValidResult : new ValidationResult(false, "Phải là số nguyên dương");

            //return inputString.All(c => Char.IsDigit(c) || Char.IsControl(c))
            //    ? ValidationResult.ValidResult    
            //    : new ValidationResult(false, "Not numeric value")

            //return string.IsNullOrWhiteSpace((value ?? "").ToString())
            //    ? new ValidationResult(false, "Field is required.")
            //    : ValidationResult.ValidResult;
        }
    }
}
