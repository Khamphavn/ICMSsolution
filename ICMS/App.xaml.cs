using ICMS.Model.DataAccess;
using ICMS.View;
using ICMS.View.UC_Component;
using ICMS.ViewModel;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;

namespace ICMS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzA1NzE2QDMyMzAyZTMyMmUzMFNyMTNveWZnbkF6b0F2TFAwNzBVUVF1RThQakFHZnp0cW1MbjVNcWhYdkk9");
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            GlobalConfig.InitializeConnections();

            AppSettings.AppSettings.InitializeAppSettings();

    

            //Create [User]/Docuemnts/ICMS for database backup
            string myDocumentFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            string ICMSDefaultFolder = Path.Combine(myDocumentFolder, "ICMS");
           if (!Directory.Exists(ICMSDefaultFolder))
            {
                Directory.CreateDirectory(ICMSDefaultFolder);
               
            }

            MainWindow = new LoginFormWindow()
            {
                DataContext = new LoginFormViewModel()
            };

            MainWindow.ShowDialog();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);


            

            try
            {
                // remove temporary certificate file
                string tmpFolder = Path.GetTempPath();

                string tempoFilePath = Path.Combine(Path.GetTempPath(), "tmp_certificate.pdf");

                File.Delete(tempoFilePath);
                File.Delete(Path.Combine(Path.GetTempPath(), "tmp_certificate.docx"));
            }
            catch (Exception)
            {

                //throw;
            }
            
        }


    }
}
