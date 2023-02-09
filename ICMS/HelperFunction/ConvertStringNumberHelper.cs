using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ICMS.HelperFunction
{
    public static class ConvertStringNumberHelper
    {
        public static string ConvertListDoubleToFormattedString(List<double> numbers)
        {
            List<string> stringNumbers = ListDoubleToListFormattedString(numbers);

            string result = String.Join(" ; ", stringNumbers);


            return result;
        }
        public static List<double> ConvertStringToListDouble(string stringNumbers)
        {
            List<double> output = new List<double>();

            stringNumbers = stringNumbers.Trim();   // remove white space at the begin and the end

            Regex rgx2 = new Regex("\t|\\s+");         // replace tab by whitespace
            stringNumbers = rgx2.Replace(stringNumbers, " ");

            stringNumbers = stringNumbers.Replace(',', '.');           //  replace ',' by '.'

            string[] dataString = stringNumbers.Split(new char[] { ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in dataString)
            {
                double dat;
                bool isValid = true;
                isValid = double.TryParse(item, out dat);

                if (isValid == false)
                {
                    return null;
                }
                output.Add(dat);
            }

            return output;
        }



        /// <summary>
        /// stringNumbers format: 2.30;100.2;1000;56
        /// </summary>
        /// <param name="stringNumber"></param>
        /// <returns></returns>
        public static List<double> ConvertFormattedStringToListDouble(string stringNumbers)
        {

            if (String.IsNullOrEmpty(stringNumbers))
            {
                return null;
            }

            try
            {
                List<double> numbers = stringNumbers
                .Split(';')
                .Where(x => double.TryParse(x, out _))
                .Select(double.Parse)
                .ToList();

                numbers = RoundListDouble(numbers);  // round all double values 

                return numbers;
            }
            catch (Exception)
            {
                return new List<double>();
            }

        }


        public static double RoundDouble(double number)
        {
            double result;
            if (number >= 1000)
            {
                result = Math.Round(number, MidpointRounding.AwayFromZero);
                return result;
            }
            else if (number < 1000 & number >= 100)
            {
                result = Math.Round(number, 1, MidpointRounding.AwayFromZero);   // 1 decimal number
                return result;
            }
            else if (number < 100 & number >= 10)
            {
                result = Math.Round(number, 2, MidpointRounding.AwayFromZero);   // 1 decimal number
                return result;
            }
            else if (number < 10)
            {
                result = Math.Round(number, 3, MidpointRounding.AwayFromZero);   // 1 decimal number
                return result;
            }
            else
            {
                return -1;  // error
            }
        }


        public static double RoundDoubleToDecimalPoints(double number, int decimalPoints)
        {
            double result;

            result = Math.Round(number, decimalPoints, MidpointRounding.AwayFromZero);
            return result;

        }


        public static List<double> RoundListDouble(List<double> numbers)
        {
            for (int i = 0; i < numbers.Count(); i++)
            {
                numbers[i] = RoundDouble(numbers[i]);
            }
            return numbers;
        }

        public static string DoubleToFormattedString(double number)
        {
            string result = "";
            if (number >= 1000)
            {
                result = number.ToString();
                return result;
            }
            else if (number < 1000 & number >= 100)
            {
                result = String.Format("{0:0.0}", number);  // 1 decimal number
                return result;
            }
            else if (number < 100)
            {
                result = String.Format("{0:0.0}", number);
                return result;
            }
            else
            {
                return result;  // error
            }
        }

        public static List<string> ListDoubleToListFormattedString(List<double> numbers)
        {
            List<string> stringNumbers = new List<string>();
            string formattedStringNumber = "";

            foreach (double number in numbers)
            {
                //formattedStringNumber = DoubleToFormattedString(number);
                formattedStringNumber = number.ToString();
                stringNumbers.Add(formattedStringNumber);
            }

            return stringNumbers;
        }
    }
}
