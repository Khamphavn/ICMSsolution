using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Model.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }

        public List<RoleAction> RoleActions { get; set; }

        //public RoleAction User { get; set; }
        //public RoleAction Permission { get; set; }
        //public RoleAction BackupDB { get; set; }
        //public RoleAction RestoreDB { get; set; }
        //public RoleAction RadQuantity { get; set; }
        //public RoleAction DoseQuantity { get; set; }
        //public RoleAction Unit { get; set; }
        //public RoleAction TM { get; set; }
        //public RoleAction Certificate { get; set; }
        //public RoleAction Customer { get; set; }
        //public RoleAction City { get; set; }
        //public RoleAction MachineName { get; set; }
        //public RoleAction MachineType { get; set; }
        //public RoleAction SensorType { get; set; }
    }
}
