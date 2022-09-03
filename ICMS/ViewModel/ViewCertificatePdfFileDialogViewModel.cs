using System.IO;

namespace ICMS.ViewModel
{
    public class ViewCertificatePdfFileDialogViewModel : BaseViewModel
    {
        private string _CertificatePdfFileFullPath;
        public string CertificatePdfFileFullPath { get => _CertificatePdfFileFullPath; set { _CertificatePdfFileFullPath = value; OnPropertyChanged(); } }


        private Stream _DocumentStream;
        public Stream DocumentStream { get => _DocumentStream; set { _DocumentStream = value; OnPropertyChanged(); } }




        public ViewCertificatePdfFileDialogViewModel(Stream documentStream)
        {

            DocumentStream = documentStream;

        }
    }
}
