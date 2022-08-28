using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Model.Models
{
    public class RadQuantity
    {
        public int RadQuantityId { get; set; }
        public string NameVN { get; set; }
        public string NameEN { get; set; }
        public double Energy { get; set; }
        public double ReUnc { get; set; }
        public bool IsActive { get; set; }
    }
}
