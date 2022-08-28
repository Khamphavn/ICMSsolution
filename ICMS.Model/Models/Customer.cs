using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Model.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public City City { get; set; }


        /// <summary>
        /// To use with searchtable combo Box
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return FullName;
        }
    }
}
