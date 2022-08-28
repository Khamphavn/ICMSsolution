using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Model.TableModels
{
    public class CalibDataTable
    {
        public int CalibDataId { get; set; }
        public int CertificateId { get; set; }
        public int RadQuantityId { get; set; }
        public string MachineReading { get; set; }
        public double AvgReading { get; set; }
        public string MachineUnit { get; set; }
        public double RefValue { get; set; }
        public string RefUnit { get; set; }
        public double CF { get; set; }
        public double CF_reUnc { get; set; }
    }
}
