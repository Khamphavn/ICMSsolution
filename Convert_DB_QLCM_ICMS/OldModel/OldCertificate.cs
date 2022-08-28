using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_DB_QLCM_ICMS.OldModel
{
    public class OldCertificate
    {
        public int CertificateId { get; set; }
        public OldMachine Machine { get; set; }
        public string CertificateNumber { get; set; }
        public DateTime CalibrationDate { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
        public DateTime Created { get; set; }

        // Bo qua : Created, CreatedBy, Updated, UpdatedBy

        public string Staff1 { get; set; }  // performed by
        public string Staff2 { get; set; }   // TM
        public string U { get; set; }


        public List<OldCertificateRadiation> CertificateRadiations { get; set; }

        public OldOrganization Organization { get; set; }
    }
}
