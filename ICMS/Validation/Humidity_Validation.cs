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
                return new ValidationResult(false, $"Không thể để trống");
            }

            if (((string)value).Length > 0)
            {
                isValid = double.TryParse((string)value, out tempValue);
            }

            if (!isValid)
            {
                return new ValidationResult(false, $"Phải là số");
            }

            if (tempValue < MinHumidity || tempValue > MaxHumidity)
            {
                return new ValidationResult(false, $"Phải nằm trong khoảng {MinHumidity} - {MaxHumidity}");
            }

            return ValidationResult.ValidResult;
        }
    }
}
