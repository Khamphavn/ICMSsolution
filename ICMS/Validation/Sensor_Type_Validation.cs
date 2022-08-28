using System.Globalization;
using System.Windows.Controls;

namespace ICMS.Validation
{
    public class Sensor_Type_Validation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // Sensor type can be null
            return ValidationResult.ValidResult;





            //Sensor sensor = (value as BindingGroup).Items[0] as Sensor;

            //string sensorType_name = sensor.SensorType.Name;

            //if (String.IsNullOrEmpty(sensor.SensorType.Name)) {
            //    return new ValidationResult(false, "Please select one");
            //}
            //else
            //{
            //    return ValidationResult.ValidResult;
            //}


            //return String.IsNullOrEmpty(sensor.SensorType.Name)
            //    ? new ValidationResult(false, "Please select one")
            //    : ValidationResult.ValidResult;



            //return value == null
            //    ? new ValidationResult(false, "Please select one")
            //    : ValidationResult.ValidResult;
        }
    }
}
