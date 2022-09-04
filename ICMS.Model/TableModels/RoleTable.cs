using ICMS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Model.TableModels
{
    public  class RoleTable
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public int User { get; set; }
        public int Permission { get; set; }
        public int BackupDB { get; set; }
        public int RestoreDB { get; set; }
        public int RadQuantity { get; set; }
        public int DoseQuantity { get; set; }
        public int Unit { get; set; }
        public int TM { get; set; }
        public int Certificate { get; set; }
        public int Customer { get; set; }
        public int City { get; set; }
        public int MachineName { get; set; }
        public int MachineType { get; set; }
        public int SensorType { get; set; }
    }
}
