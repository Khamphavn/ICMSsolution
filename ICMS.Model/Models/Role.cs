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

        //public RoleAction User { get; set; }               0
        //public RoleAction Permission { get; set; }         1
        //public RoleAction BackupDB { get; set; }           2
        //public RoleAction RestoreDB { get; set; }          3
        //public RoleAction RadQuantity { get; set; }        4
        //public RoleAction DoseQuantity { get; set; }       5
        //public RoleAction Unit { get; set; }               6
        //public RoleAction TM { get; set; }                 7
        //public RoleAction Certificate { get; set; }        8
        //public RoleAction Customer { get; set; }           9
        //public RoleAction City { get; set; }              10
        //public RoleAction MachineName { get; set; }       11
        //public RoleAction MachineType { get; set; }       12
        //public RoleAction SensorType { get; set; }        13
    }
}
