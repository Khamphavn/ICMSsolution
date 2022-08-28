using ICMS.HelperFunction;
using ICMS.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;


namespace ICMS.Model
{
    public class CalibDataDTO : INotifyPropertyChanged
    {
        #region Properties
        public int STT { get; set; }

        private RadQuantity _RadQuantity;
        public RadQuantity RadQuantity
        {
            get
            {
                return _RadQuantity;
            }
            set
            {
                _RadQuantity = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(CF));
                NotifyPropertyChanged(nameof(CF_reUnc));
                NotifyPropertyChanged(nameof(CF_Full_2sigma));
            }
        }
        private double _RefValue;
        public double RefValue
        {
            get
            {
                return _RefValue;

            }
            set
            {
                _RefValue = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(CF));
                NotifyPropertyChanged(nameof(CF_reUnc));
                NotifyPropertyChanged(nameof(CF_Full_2sigma));
            }
        }
        public Unit RefUnit { get; set; }

        private string _MachineReading;
        public string MachineReading
        {
            get { return _MachineReading; }
            set
            {
                _MachineReading = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(ReadingValues));
                NotifyPropertyChanged(nameof(AvgReading));
                NotifyPropertyChanged(nameof(AvgReading_absUnc));
                NotifyPropertyChanged(nameof(AvgReading_reUnc));

                NotifyPropertyChanged(nameof(AvgReadingWithUnc));

                NotifyPropertyChanged(nameof(CF));
                NotifyPropertyChanged(nameof(CF_reUnc));
                NotifyPropertyChanged(nameof(CF_Full_2sigma));
            }
        }
        public Unit MachineUnit { get; set; }


        public List<double> ReadingValues
        {
            get
            {
                try
                {
                    List<double> numbers = ConvertStringNumberHelper.ConvertFormattedStringToListDouble(MachineReading);

                    return numbers;
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {

                }

            }
        }

        public double AvgReading
        {
            get
            {
                if (ReadingValues != null)
                {
                    double result = ReadingValues.Count() > 0 ? ReadingValues.Average() : 0.0;
                    return ConvertStringNumberHelper.RoundDouble(result);
                }
                else
                {
                    return double.NaN;
                }
            }
        }

        public double AvgReading_absUnc
        {
            get
            {
                double AvgReading_absUnc_value = AvgReading_absUnc_evaluation();
                return AvgReading_absUnc_value;
            }
        }

        public double AvgReading_reUnc
        {
            get
            {
                double AvgRedaing_reUnc_value = AvgReading_reUnc_evaluation();
                return AvgRedaing_reUnc_value;
            }
        }

        public string AvgReadingWithUnc
        {
            get
            {
                string AvgReadingString = AvgReading != double.NaN ? string.Format("{0:0.00}", AvgReading) : "NaN";
                string AvgReading_absUncString = AvgReading_absUnc != double.NaN ? string.Format("{0:0.00}", AvgReading_absUnc) : "NaN";

                string AvgReading_reUncString = "NaN";
                if (!double.IsNaN(AvgReading_reUnc))
                {
                    AvgReading_reUncString = string.Format("{0:0.00}%", AvgReading_reUnc * 100);
                }

                string output = $"{AvgReadingString} ± {AvgReading_absUncString} ({AvgReading_reUncString})";

                return output;
            }
        }


        public double CF
        {
            get
            {
                double CFvalue = AvgReading != 0 ? RefValue / AvgReading : Double.NaN;
                return CFvalue;
            }
        }


        public double CF_reUnc
        {
            get
            {
                double CF_reUnc_value = CF_reUnc_evaluation();
                return CF_reUnc_value;
            }
        }

        public string CF_Full_2sigma
        {
            get
            {
                string CF_Full_2sigma_string = CF_Full_2sigma_presentation();
                return CF_Full_2sigma_string;
            }
        }

        #endregion

        #region private method
        private double AvgReading_absUnc_evaluation()
        {
            try
            {
                double StdDevOfMean = UncertaintyEvaluation.standardDeviationOfTheMean(ReadingValues);
                return StdDevOfMean;
            }
            catch (Exception)
            {
                return double.NaN;
            }
        }

        private double AvgReading_reUnc_evaluation()
        {
            try
            {
                double result = AvgReading_absUnc / AvgReading;
                return result;
            }
            catch (Exception)
            {
                return double.NaN;
            }
        }


        private double CF_reUnc_evaluation()
        {
            double result = 0;

            double RefValue_reUnce = RadQuantity != null ? RadQuantity.ReUnc : 0.0;
            result = Math.Sqrt(RefValue_reUnce * RefValue_reUnce + AvgReading_reUnc * AvgReading_reUnc);

            return result;
        }
        private string CF_Full_2sigma_presentation()
        {
            string output = "";
            output = String.Format("{0:F2} ± {1:F2} ({2:P})", CF, CF * CF_reUnc * 2.0, CF_reUnc * 2.0);
            return output;
        }


        #endregion

        #region INotifyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
