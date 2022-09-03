using ICMS.Command;
using ICMS.Model.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using MessageBox = System.Windows.MessageBox;

namespace ICMS.ViewModel
{
    public class DatabaseRestoreViewModel : BaseViewModel
    {
        private string _databaseFilePath;
        public string databaseFilePath { get => _databaseFilePath; set { _databaseFilePath = value; OnPropertyChanged(); } }


        #region Commands
        public ICommand SelectDatabaseCommand { get; set; }
        public ICommand RestoreDatabaseCommand { get; set; }
        #endregion


        public DatabaseRestoreViewModel()
        {
            #region Commands

            #region SelectDatabaseCommand
            SelectDatabaseCommand = new RelayCommand<object>(
                (p) =>
                {
                    return true;
                },
                (p) =>
                {
                    OpenFileDialog  selectecDatabaseDialog = new OpenFileDialog()
                    {
                        InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), Properties.Settings.Default.BackupFolder1),
                        Filter = "database backup file (*.bak)|*.bak",
                        Title = "Select a database to restore"
                    };

                    DialogResult result = selectecDatabaseDialog.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        databaseFilePath = selectecDatabaseDialog.FileName;
                    }

                }
                );
            #endregion

            #region RestoreDatabaseCommand
            RestoreDatabaseCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (File.Exists(databaseFilePath))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                },
                (p) =>
                {
                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                    try
                    {
                        RestoreDatabase(databaseFilePath);

                        MessageBox.Show(
                           messageBoxText: "Database restore successfully !",
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
                    finally
                    {
                        Mouse.OverrideCursor = null;    
                    }
                }
                );
            #endregion

            #endregion
        }


        private void RestoreDatabase(string databaseFilePath)
        {
            using (SqlConnection connection = new SqlConnection(GlobalConfig.CnnString("ICMSdatabase")))
            {
                connection.Open();

                string ICMSdatabase = connection.Database;


                string UseMaster = "USE master";
                SqlCommand UseMasterCommand = new SqlCommand(UseMaster, connection);
                UseMasterCommand.ExecuteNonQuery();

                string Alter1 = $"ALTER DATABASE [{ICMSdatabase}] SET Single_User WITH Rollback Immediate";
                SqlCommand Alter1Cmd = new SqlCommand(Alter1, connection);
                Alter1Cmd.ExecuteNonQuery();

                var restoreQuery = String.Format("RESTORE DATABASE {0} FROM DISK='{1}' WITH NOUNLOAD, REPLACE, STATS = 5", ICMSdatabase, databaseFilePath);
                SqlCommand RestoreCmd = new SqlCommand(restoreQuery, connection);
                RestoreCmd.ExecuteNonQuery();


                string Alter2 = $"ALTER DATABASE [{ICMSdatabase}] SET Multi_User";
                SqlCommand Alter2Cmd = new SqlCommand(Alter2, connection);
                Alter2Cmd.ExecuteNonQuery();


                //string UserICMSdatabase = "USER " + connection.Database;
                //SqlCommand UseICMSCommand = new SqlCommand(UserICMSdatabase, connection);
                //UseICMSCommand.ExecuteNonQuery();
            }









        }

    }
}
