using ICMS.Model.DataAccess;
using ICMS.View;
using ICMS.ViewModel;
using System.Globalization;
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


            MainWindow = new LoginFormWindow()
            {
                DataContext = new LoginFormViewModel()
            };
            MainWindow.ShowDialog();

            base.OnStartup(e);
        }


    }
}
