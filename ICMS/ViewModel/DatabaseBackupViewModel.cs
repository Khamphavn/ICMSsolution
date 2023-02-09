using ICMS.Command;
using ICMS.HelperFunction;
using ICMS.Model;
using ICMS.Model.DataAccess;
using ICMS.Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ICMS.ViewModel
{
    public class DatabaseBackupViewModel : BaseViewModel
    {
        private DateTime _LastBackupDate;
        public DateTime LastBackupDate { get => _LastBackupDate; set { _LastBackupDate = value; OnPropertyChanged(); } }

        private string _BackupFileName;
        public string BackupFileName { get => _BackupFileName; set { _BackupFileName = value; OnPropertyChanged(); } }


        private string _BackupFolder1;
        public string BackupFolder1 { get => _BackupFolder1; set { _BackupFolder1 = value; OnPropertyChanged(); } }

        private string _backupFolder2;
        public string BackupFolder2 { get => _backupFolder2; set { _backupFolder2 = value; OnPropertyChanged(); } }


        private string _SelectedBackupDBInterval;
        public string SelectedBackupDBInterval { get => _SelectedBackupDBInterval; set { _SelectedBackupDBInterval = value; OnPropertyChanged(); } }


        private ObservableCollection<string> _backupDBOptions;
        public ObservableCollection<string> BackupDBOptions { get => _backupDBOptions; set { _backupDBOptions = value; OnPropertyChanged(); } }



        #region Commands
        public ICommand OpenSelectFoldderDialogCommand { get; set; }
        public ICommand SaveBackupSettingsCommand { get; set; }
        public ICommand BackupDatabaseCommand { get; set; }
        #endregion




        public DatabaseBackupViewModel()
        {
            #region init
            BackupDBOptions = new ObservableCollection<string>(new List<string> { "1", "2", "3", "6"});

            LastBackupDate = Properties.Settings.Default.LastBackupDate;

            //SetDefaultBackupFolder1();

            BackupFolder1 = Properties.Settings.Default.BackupFolder1;
            BackupFolder2 = Properties.Settings.Default.BackupFolder2;

            SelectedBackupDBInterval = Properties.Settings.Default.BackupDBMonths.ToString();
            #endregion


            #region Commands

            #region OpenSelectFoldderDialogCommand
            OpenSelectFoldderDialogCommand = new RelayCommand<object>(
                (p) =>
                {
                     return true; 
                },
                (p) =>
                {
                    using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
                    {
                        System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            BackupFolder2 = dialog.SelectedPath;
                        }

                        
                    }

                }
                );
            #endregion

            #region SaveBackupSettingsCommand
            SaveBackupSettingsCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (SelectedBackupDBInterval != null)
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
                    Properties.Settings.Default.BackupFolder1 = BackupFolder1;
                    Properties.Settings.Default.BackupFolder2 = BackupFolder2;
                    var test = SelectedBackupDBInterval;
                    Properties.Settings.Default.BackupDBMonths = Int32.Parse(SelectedBackupDBInterval);
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Reload();
                }
                );
            #endregion

            #region BackupDatabaseCommand
            BackupDatabaseCommand = new RelayCommand<object>(
                (p) =>
                {
                    return true;
                },
                (p) =>
                {
                    try
                    {
                        if (Directory.Exists(BackupFolder1))
                        {
                            BackupDB.BackupDatabase(BackupFolder1);
                        }

                        LastBackupDate = DateTime.Now;
                        Properties.Settings.Default.LastBackupDate = LastBackupDate;
                        Properties.Settings.Default.Save();
                        Properties.Settings.Default.Reload();

                        MessageBox.Show(
                           messageBoxText: $"Database backup successfully to {BackupFolder1}!",
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


                    try
                    {
                        if (Directory.Exists(BackupFolder2))
                        {
                            BackupDB.BackupDatabase(BackupFolder2);
                        }
                        LastBackupDate = DateTime.Now;
                        Properties.Settings.Default.LastBackupDate = LastBackupDate;
                        Properties.Settings.Default.Save();
                        Properties.Settings.Default.Reload();

                        MessageBox.Show(
                           messageBoxText: $"Database backup successfully to {BackupFolder2}!",
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
                );
            #endregion

            #endregion
        }


        private void SetDefaultBackupFolder1()
        {
            string BackupFolderDefault1 = Path.Combine(Directory.GetCurrentDirectory(), "BackupDB");

            if (!Directory.Exists(BackupFolderDefault1))
            {
                Directory.CreateDirectory(BackupFolderDefault1);
            }
            BackupFolder1 = BackupFolderDefault1;
        }


        //private int BackupDatabase(string backupFolderFullPath)
        //{
        //    using (var connection = new SqlConnection(GlobalConfig.CnnString("ICMSdatabase")))
        //    {
        //        string databaseName = connection.Database;

        //        string fileName = string.Format("{0}-backup-{1}.bak", databaseName, DateTime.Now.ToString("yyyy-MMM-dd_HH_mm"));

        //        string filePath = Path.Combine(backupFolderFullPath, fileName);

        //        var query = String.Format("BACKUP DATABASE [{0}] TO DISK='{1}'", databaseName, filePath);

        //        using (var command = new SqlCommand(query, connection))
        //        {
        //            connection.Open();
        //            command.ExecuteNonQuery();
        //        }
        //        return 1;
        //    }
        //}
    }
}
