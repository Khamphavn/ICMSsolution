using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.HelperFunction
{
    public static class UncertaintyEvaluation
    {
        public static double standardDeviation(List<double> sequence)
        {
            double result = 0;

            if (sequence.Count() >= 2)
            {
                double average = sequence.Average();
                double sum = sequence.Sum(d => Math.Pow(d - average, 2));
                result = Math.Sqrt((sum) / (sequence.Count() - 1));
            }
            return result;
        }
        public static double standardDeviationOfTheMean(List<double> sequence)
        {
            double result = 0;

            if (sequence.Count() >= 2)
            {
                double average = sequence.Average();
                double sum = sequence.Sum(d => Math.Pow(d - average, 2));
                double StdDev = Math.Sqrt((sum) / sequence.Count());

                result = StdDev / Math.Sqrt(sequence.Count());
            }
            return result;
        }
    }
}
