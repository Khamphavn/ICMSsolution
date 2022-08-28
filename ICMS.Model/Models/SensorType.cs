using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Model.Models
{
    public class SensorType
    {
        public int SensorTypeId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
