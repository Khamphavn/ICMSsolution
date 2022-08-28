using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ICMS.Validation
{
    public class Temperature_Validation : ValidationRule
    {
        // giá trị tối thiểu của nhiệt độ
        public double MinTempValue { get; set; } = AppSettings.AppSettings.MINTEMPERATUE;
        public double MaxTempValue { get; set; } = AppSettings.AppSettings.MAXTEMPERATUE; // giá trị tối đa của nhiệt độ


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
                return new ValidationResult(false, $"Nhiệt độ phải là số");
            }

            if (tempValue < MinTempValue || tempValue > MaxTempValue)
            {
                return new ValidationResult(false, $"Nhiệt độ phải nằm trong khoảng {MinTempValue} - {MaxTempValue}");
            }

            return ValidationResult.ValidResult;
        }
    }
}
