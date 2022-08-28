using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Model.Models
{
    public class Machine
    {
        public int MachineId { get; set; }
        public string Name { get; set; }
        public MachineType MachineType { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }
        public string Manufacturer { get; set; }
        public string MadeIn { get; set; }

        public List<Sensor> Sensors { get; set; }
    }
}
