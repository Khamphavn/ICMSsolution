using ICMS.Model.Models;
using System.Windows.Controls;

namespace ICMS.Validation
{
    public class Customer_City_Validation : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            City city = (City)value;


            if ( city == null )
            {
                return new ValidationResult(false, "Please select one");
            }
            else if ( city.Name == null )
            {
                return new ValidationResult(false, "Please select one");
            }
            else
            {
                return ValidationResult.ValidResult;
            }

        }
    }
}
