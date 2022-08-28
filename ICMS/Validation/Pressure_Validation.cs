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
        public double MaxPressure { get; set; } = AppSettings.AppSettings.MAXHUMIDITY;// giá trị tối đa của nhiệt độ


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

            if (tempValue < MinPressure || tempValue > MaxPressure)
            {
                return new ValidationResult(false, $"Phải nằm trong khoảng {MinPressure} - {MaxPressure}");
            }

            return ValidationResult.ValidResult;
        }
    }
}
