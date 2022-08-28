using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Model.Models
{
    public class Unit
    {
        public int UnitId { get; set; }
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public int Order { get; set; }
    }
}
