
using System.Globalization;
using System.Windows.Controls;

namespace ICMS.Validation 
{
    public class Customer_ShortName_Validation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string shortName = (string)value;

            if (shortName != null)
            {
                if (shortName.Length > 20)
                {
                    return new ValidationResult(false, "Maximum 30 charaters");
                }
            }

            return ValidationResult.ValidResult;
        }
    }
}
