﻿using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace ICMS.Validation
{
    public class City_PhoneCode_Validation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string str = (string)value;
            

            var allowedChars = "1234567890";



            if (string.IsNullOrWhiteSpace(str))
            {
                return new ValidationResult(false, "Không thể để trống");
            }

            str = str.Trim();
            if (str.Length != 3)
            {
                return new ValidationResult(false, "Phải có 3 chữ số");
            }

            if (!str.All(x => allowedChars.Contains(x)))
            {
                return new ValidationResult(false, "Phải có 3 chữ số");
            }

            return new ValidationResult(true, null);
        }
    }
}
