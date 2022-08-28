using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_DB_QLCM_ICMS.OldModel
{
    public class OldCertificateRadiation
    {
        public int Id { get; set; }
        public int CertificateId { get; set; }
        public OldRadiation Radiation { get; set; }
        public string ReferenceDoseRate { get; set; }
        public string SurveyMeterReading { get; set; }  // avg reading

        public string Input1 { get; set; }
        public string Input2 { get; set; }    // sring of reading, ";" separator
    }
}
