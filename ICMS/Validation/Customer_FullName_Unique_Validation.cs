using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;

namespace ICMS.Validation
{

    public class Customer_FullName_Unique_Validation : ValidationRule 
    {
        //public Customer_FullName_ValidationWrapper Customer_FullName_ValidationWrapper { get; set; }
        public List<string>  ListOfValuesToCheck{ get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool isValid = true;

            string inputString = (string)value;

            // Check empty input
            isValid = String.IsNullOrWhiteSpace(inputString);
            if (isValid)
            {
                return new ValidationResult(false, $"Field is required");
            }

            // Check unique

            //TODO: can xem lai
            //ListOfValuesToCheck = GlobalConfig.Connection.Customer_GetAllFullName(GlobalConfig.CnnString("QLCMDatabase"));

            //foreach (string item in ListOfValuesToCheck)
            //{
            //    isEqualString = inputString.CompareWithString(item);

            //    if (isEqualString == true)
            //    {
            //        return new ValidationResult(false, $"Field is already existed");
            //    }
            //}


            return ValidationResult.ValidResult;
        }
    }
}
