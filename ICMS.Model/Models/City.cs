using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Model.Models
{
    public class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public string PhoneCode { get; set; }
        public bool IsActive { get; set; }
    }
}
