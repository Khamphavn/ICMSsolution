using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Model.Models
{
    public class Certificate
    {
        public int CertificateId { get; set; }
        public string CertificateNumber { get; set; }
        public DoseQuantity DoseQuantity { get; set; }
        public DateTime CalibDate { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
        public string PerformedBy { get; set; }
        public string TM { get; set; }
        public string Note { get; set; }


        public Machine Machine { get; set; }
        public Customer Customer { get; set; }

        //public CalibProcedure CalibProcedure { get; set; }

        public List<CalibData> CalibDatas { get; set; }
    }
}
