using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Model.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public bool IsActive { get; set; }
    }
}
