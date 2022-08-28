using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Model.Models
{
    public class Sensor
    {
        public int SensorId { get; set; }
        public int MachineId { get; set; }
        public SensorType SensorType { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }
    }
}
