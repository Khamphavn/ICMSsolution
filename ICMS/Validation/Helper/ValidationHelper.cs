using ICMS.HelperFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMS.Validation.Helper
{
    public static class ValidationHelper
    {
        public static bool IsMachineReadingStringValid(string machineReadingString)
        {
            bool isValid = true;


            List<double> machineReadings = ConvertStringNumberHelper.ConvertStringToListDouble(machineReadingString);

            if (machineReadings != null)
            {
                bool containsNegative = machineReadings.Any(i => i < 0);

                if (containsNegative)
                {
                    isValid = false;
                }
            }
            else
            {
                isValid = false;
            }


            return isValid;
        }

        public static bool IsRefValueValid(string refValueString)
        {
            bool isValid = true;

            var isNumeric = double.TryParse("123", out double refValue);

            if (isNumeric)
            {
                if (refValue <= 0) { isValid = false; } else { isValid = false; }
            }
            else
            {
                isValid = false;
            }

            return isValid;
        }

    }
}
