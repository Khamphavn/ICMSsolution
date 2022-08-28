using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_DB_QLCM_ICMS.OldModel
{
    public class OldMachine
    {
        public int MachineId { get; set; }
        public string Name { get; set; }
        public string Seri { get; set; }
        public string HeadSeri { get; set; }
        public OldMachineType MachineType { get; set; }
        public OldMachineModel MachineModel { get; set; }
        public OldMachineHeadType MachineHeadType { get; set; }
        public OldUnitType UnitType { get; set; }
        public int OrganizationId { get; set; }
        public DateTime CalibrationDate { get; set; }
        public int CertificateId { get; set; }

        // 

        public string MachineTypeCode { get; set; }
        public string MachineHeadTypeCode { get; set; }
        public string Manufacture { get; set; }
    }
}
