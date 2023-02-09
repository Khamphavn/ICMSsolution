using ICMS.HelperFunction;
using System;
using System.Collections.Generic;
using System.Globalization;

using System.Windows.Controls;

namespace ICMS.Validation
{
    public class CalibData_MachineReading_Validation : ValidationRule
    {
        public int MinDataNumber { get; set; } // giá trí tối thiểu của số lượng số đọc

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            List<double> expData = new List<double>();
            //bool isValid = true;

            var stringNumber = value as String;

            if (String.IsNullOrWhiteSpace(stringNumber))
            {
                return new ValidationResult(false, $"Field is required");
            }


            if (stringNumber.Length > 0)
            {
                expData = ConvertStringNumberHelper.ConvertStringToListDouble(stringNumber);

                if (expData == null)
                {
                    return new ValidationResult(false, $"Can not convert to number");
                }
            }

            if (expData.Count < MinDataNumber)
            {
                return new ValidationResult(false, $"Must have at least {MinDataNumber} reading !");
            }

            return ValidationResult.ValidResult;
        }

        //private List<decimal> ConvertToNumbers(string s)
        //{
        //    List<decimal> output = new List<decimal>();

        //    string line = s.Trim();   // remove white space at the begin and the end

        //    Regex rgx2 = new Regex("\t|\\s+");         // replace tab by whitespace
        //    line = rgx2.Replace(line, " ");

        //    line = line.Replace(',', '.');           //  replace ',' by '.'

        //    string[] dataString = line.Split(new char[] { ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);

        //    foreach (var item in dataString)
        //    {
        //        decimal dat;
        //        bool isValid = true;
        //        isValid = decimal.TryParse(item, out dat);

        //        if (isValid == false)
        //        {
        //            return null;
        //        }
        //        output.Add(dat);
        //    }
        //    return output;
        //}



    }
}
