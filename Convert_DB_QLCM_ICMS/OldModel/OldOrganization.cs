using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_DB_QLCM_ICMS.OldModel
{
    public class OldOrganization
    {
        public int OrganizationId { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public OldCity City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
