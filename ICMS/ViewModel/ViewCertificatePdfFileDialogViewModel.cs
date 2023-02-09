using ICMS.Command;
using MaterialDesignThemes.Wpf;
using System.IO;
using System.Windows.Input;

namespace ICMS.ViewModel
{
    public class ViewCertificatePdfFileDialogViewModel : BaseViewModel
    {
        private string _CertificatePdfFileFullPath;
        public string CertificatePdfFileFullPath { get => _CertificatePdfFileFullPath; set { _CertificatePdfFileFullPath = value; OnPropertyChanged(); } }


        private Stream _DocumentStream;
        public Stream DocumentStream { get => _DocumentStream; set { _DocumentStream = value; OnPropertyChanged(); } }


        public ICommand CloseDocumentStream { get; set; }

        public ViewCertificatePdfFileDialogViewModel(Stream documentStream)
        {

            DocumentStream = documentStream;

            CloseDocumentStream = new RelayCommand<object>
                    (
                       (p) => {
                                 return true;
                           
                       },
                       (p) =>
                       {
                           DocumentStream.Close();

                           DialogHost.CloseDialogCommand.Execute(null, null);
                          
                       }
                   );

        }
    }
}
