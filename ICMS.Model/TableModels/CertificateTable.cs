using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Model.TableModels
{
    public class CertificateTable
    {
        public int CertificateId { get; set; }
        public string CertificateNumber { get; set; }
        public int DoseQuantityId { get; set; }
        public DateTime CalibDate { get; set; }
        public int MachineId { get; set; }
        public int CustomerId { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float Pressure { get; set; }
        public string PerformedBy { get; set; }
        public string TM { get; set; }
        public string Note { get; set; }
    }
}
