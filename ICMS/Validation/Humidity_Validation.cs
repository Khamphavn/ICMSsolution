using System;
using System.Globalization;
using System.Windows.Controls;

namespace ICMS.Validation
{
    public class Humidity_Validation : ValidationRule
    {
        public double MinHumidity { get; set; } = AppSettings.AppSettings.MINHUMIDITY; // giá trị tối thiểu của nhiệt độ
        public double MaxHumidity { get; set; } = AppSettings.AppSettings.MAXHUMIDITY; // giá trị tối đa của nhiệt độ


        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double tempValue = 0;
            bool isValid = true;


            if (String.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, $"Field is required");
            }

            if (((string)value).Length > 0)
            {
                isValid = double.TryParse((string)value, out tempValue);
            }

            if (!isValid)
            {
                return new ValidationResult(false, $"Must be a number");
            }

            if (tempValue < MinHumidity || tempValue > MaxHumidity)
            {
                return new ValidationResult(false, $"Must be a number between {MinHumidity} and {MaxHumidity}");
            }

            return ValidationResult.ValidResult;
        }
    }
}
