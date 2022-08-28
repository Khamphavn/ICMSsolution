using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Model.Models
{
    public class DoseQuantity
    {
        public int DoseQuantityId { get; set; }
        public string NameVN { get; set; }
        public string NameEN { get; set; }
        public string Notation { get; set; }
        public bool IsActive { get; set; }
    }
}
