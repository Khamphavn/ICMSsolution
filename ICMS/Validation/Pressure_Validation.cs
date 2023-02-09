using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ICMS.Validation
{
    public class Pressure_Validation : ValidationRule
    {
        public double MinPressure { get; set; } = AppSettings.AppSettings.MINPRESSURE; // giá trị tối thiểu của nhiệt độ
        public double MaxPressure { get; set; } = AppSettings.AppSettings.MAXPRESSURE;// giá trị tối đa của nhiệt độ


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

            if (tempValue < MinPressure || tempValue > MaxPressure)
            {
                return new ValidationResult(false, $"Must be between {MinPressure} - {MaxPressure}");
            }

            return ValidationResult.ValidResult;
        }
    }
}
