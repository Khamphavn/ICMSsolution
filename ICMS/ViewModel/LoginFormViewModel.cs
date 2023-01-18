using DocumentFormat.OpenXml.Spreadsheet;
using ICMS.Command;
using ICMS.HelperFunction;
using ICMS.Model.DataAccess;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace ICMS.ViewModel
{
    public class LoginFormViewModel : BaseViewModel
    {
        private string _LoginName;
        public string LoginName { get => _LoginName; set { _LoginName = value; OnPropertyChanged(); } }

        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }


        private bool _IsAllCheckPass;
        public bool IsAllCheckPass { get => _IsAllCheckPass; set { _IsAllCheckPass = value; OnPropertyChanged(); } }


        private bool _IsFinishChecking;
        public bool IsFinishChecking { get => _IsFinishChecking; set { _IsFinishChecking = value; OnPropertyChanged(); } }



        private string _CheckMessageText;
        public string CheckMessageText { get => _CheckMessageText; set { _CheckMessageText = value; OnPropertyChanged(); } }

        public ICommand NavigateAppShutdownCommand { get; set; }


        public LoginFormViewModel()
        {
            DateTime LastBackupDate = Properties.Settings.Default.LastBackupDate;

            string BackupFolder1 = Properties.Settings.Default.BackupFolder1;
            string BackupFolder2 = Properties.Settings.Default.BackupFolder2;

            int BackupDBMonths = Properties.Settings.Default.BackupDBMonths;

            if (!Directory.Exists(BackupFolder1))
            {
                MessageBox.Show(
                       messageBoxText: $"Thư mục \"{BackupFolder1}\" không tồn tại ! \n\n Không thể tự động sao lưu cơ sở dữ liệu !",
                       caption: "Error",
                       button: MessageBoxButton.OK,
                       icon: MessageBoxImage.Warning,
                       defaultResult: MessageBoxResult.OK
                       );
            }

            if (!Directory.Exists(BackupFolder2))
            {
                MessageBox.Show(
                       messageBoxText: $"Thư mục \"{BackupFolder2}\" không tồn tại ! \n\n Không thể tự động sao lưu cơ sở dữ liệu !",
                       caption: "Error",
                       button: MessageBoxButton.OK,
                       icon: MessageBoxImage.Warning,
                       defaultResult: MessageBoxResult.OK
                       );
            }
            
            var diffOfDates = DateTime.Now - LastBackupDate;
            var diffInDays = diffOfDates.TotalDays;
            var diffInMonths = diffInDays / 30.4375;   //362.25/12=30.4375

            if(diffInMonths >= BackupDBMonths)
            {
                try
                {
                    if (Directory.Exists(BackupFolder1))
                    {
                        BackupDB.BackupDatabase(BackupFolder1);
                    }

                    if (Directory.Exists(BackupFolder2))
                    {
                        BackupDB.BackupDatabase(BackupFolder2);
                    }
                    Properties.Settings.Default.LastBackupDate = DateTime.Now;
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Reload();

                    MessageBox.Show(
                       messageBoxText: "The database has been automatically backed up successfully !",
                       caption: "",
                       button: MessageBoxButton.OK,
                       icon: MessageBoxImage.Information,
                       defaultResult: MessageBoxResult.OK
                       );

                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                     messageBoxText: ex.Message,
                     caption: "Error",
                     button: MessageBoxButton.OK,
                     icon: MessageBoxImage.Error,
                     defaultResult: MessageBoxResult.OK
                     );
                }
            }




            if (!CanConnectDatabase())
            {
                MessageBox.Show(
                       messageBoxText: $"Không thể kết nối cơ sở dữ liệu \"{GlobalConfig.CnnString("ICMSdatabase")}\"",
                       caption: "Error",
                       button: MessageBoxButton.OK,
                       icon: MessageBoxImage.Error,
                       defaultResult: MessageBoxResult.OK
                       );

                Application.Current.Shutdown();
            }

            IsAllCheckPass = true;


            



            #region NavigateAppShutdownCommand
            NavigateAppShutdownCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) =>
                {
                    Application.Current.Shutdown();
                }
                );
            #endregion

        }


        #region private check

        private bool CanConnectDatabase()
        {
            using (SqlConnection connection = new SqlConnection(GlobalConfig.CnnString("ICMSdatabase")))
            {
                try
                {
                    connection.Open();
                   
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
                finally
                {
                    // not really necessary
                    connection.Close();
                }
            }
        }

        #endregion

    }
}
