using MaterialDesignThemes.Wpf;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ICMS.Command;
using ICMS.Model.Models;
using ICMS.Model.DataAccess;
using ICMS.View.UC_Dialog;
using ICMS.Bussiness.CertificateProcessing;
using System.Diagnostics;
using Microsoft.Win32;
using System.Windows.Forms;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;
using System.IO;
using System.Data.SqlClient;
using MessageBox = System.Windows.MessageBox;
using System.Collections.Generic;
using System.ComponentModel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ICMS.ViewModel
{
    public class CertificateListViewModel : BaseViewModel
    {
        private User _CurrentUser;
        public User CurrentUser { get => _CurrentUser; private set { _CurrentUser = value; OnPropertyChanged(); } }

        private ObservableCollection<Certificate> _CertificateList;
        public ObservableCollection<Certificate> CertificateList
        {
            get => _CertificateList; 
            set
            {
                _CertificateList = value;
                if (CertificateList != null)
                {
                    FilterCertificateList = CertificateList;
                }
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Certificate> _FilterCertificateList;
        public ObservableCollection<Certificate> FilterCertificateList { get => _FilterCertificateList; set { _FilterCertificateList = value; OnPropertyChanged(); } }

        private bool _ShowRowDeltails;
        public bool ShowRowDeltails { get => _ShowRowDeltails; set { _ShowRowDeltails = value; OnPropertyChanged(); } }

        private Certificate _SelectedCertificate;
        public Certificate SelectedCertificate { get => _SelectedCertificate; set { _SelectedCertificate = value; OnPropertyChanged(); }}

        private DateTime _FromDate;
        public DateTime FromDate { get => _FromDate; set { _FromDate = value; OnPropertyChanged(); } }

        private DateTime _ToDate;
        public DateTime ToDate { get => _ToDate; set { _ToDate = value; OnPropertyChanged(); } }


        private string _TempoCertificatePdfFilePath;
        public string TempoCertificatePdfFilePath { get => _TempoCertificatePdfFilePath; set { _TempoCertificatePdfFilePath = value; OnPropertyChanged(); } }

        private bool _IsDialogOpen;
        public bool IsDialogOpen { get => _IsDialogOpen; set { _IsDialogOpen = value; OnPropertyChanged(); } }

        private object _DialogContent;
        public object DialogContent { get => _DialogContent; set { _DialogContent = value; OnPropertyChanged(); } }

        private Stream _documentStream;
        public Stream DocumentStream { get => _documentStream; set { _documentStream = value; OnPropertyChanged(); } }

        #region Commands
        public ICommand ViewAndPrintCertificateCommand { get; set; }
        public ICommand ExportCertificateToWordCommand { get; set; }
        public ICommand ExportFilterCertificatesToCSVCommand { get; set; }
        public ICommand ViewDetailsOnlyCertificateCommand { get; set; }
        public ICommand EditCertificateCommand { get; set; }
        public ICommand AddNewCertificateBaseOnThisMachineCommand { get; set; }
        public ICommand DeleteCertificateCommand { get; set; }
        public ICommand GetCertificateFromDateToDateCommand { get; set; }
        #endregion


        public CertificateListViewModel(MainViewModel mainViewModel)
        {
            #region Init
            CurrentUser = mainViewModel.CurrentUser;


            FromDate = DateTime.Now.AddMonths(-AppSettings.AppSettings.MONTHBEFORETODAY);
            ToDate = DateTime.Today;

            CertificateList = new ObservableCollection<Certificate>(GlobalConfig.Connection.Certificate_GetFromDateToDate(FromDate, ToDate, GlobalConfig.CnnString("ICMSdatabase")));
            FilterCertificateList = CertificateList;
            ShowRowDeltails = true;
            SelectedCertificate = null;



            //IsDialogOpen = false;

            #endregion

            #region Commands

            #region ViewAndPrintCertificateCommand
            ViewAndPrintCertificateCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (SelectedCertificate != null)
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
                    int createTempoCertificatePdfSuccess;
                    string certificatePdfFileFullPath;
                    try
                    {
                        ProcessingCertificate processingCertificate = new ProcessingCertificate();


                        //Stopwatch stopWatch = new Stopwatch();
                        //stopWatch.Start();
                        if (DocumentStream != null)
                        {
                            DocumentStream.Close();
                            DocumentStream.Dispose();
                            DocumentStream = null;
                        }
                        createTempoCertificatePdfSuccess = processingCertificate.CreateTemporaryCertificatePdf(SelectedCertificate);

                        //stopWatch.Stop();
                        //Debug.WriteLine("\n\n Execution time: " + stopWatch.ElapsedMilliseconds + "\n\n");

                    }
                    catch (Exception ex)
                    {
                        MessageBoxResult result = System.Windows.MessageBox.Show(
                           messageBoxText: "Bị lỗi trong quá trình tạo chứng chỉ chuẩn\n\n" + ex.Message,
                           caption: "Error",
                           button: MessageBoxButton.OK,
                           icon: MessageBoxImage.Error,
                           defaultResult: MessageBoxResult.OK
                           );

                        createTempoCertificatePdfSuccess = 0;
                    }
                    finally
                    {
                        Mouse.OverrideCursor = null;
                    }


                    if (createTempoCertificatePdfSuccess == 1)
                    {
                        certificatePdfFileFullPath = CertificateHelper.GetTempoFilePdfFullPath(SelectedCertificate);

                        if (File.Exists(certificatePdfFileFullPath))
                        {
                            OpenViewCertificatePdfDialog(certificatePdfFileFullPath);
                        }
                        else
                        {
                            MessageBoxResult result = System.Windows.MessageBox.Show(
                           messageBoxText: "File chứng chỉ pdf không tồn tại\n\n",
                           caption: "Error",
                           button: MessageBoxButton.OK,
                           icon: MessageBoxImage.Error,
                           defaultResult: MessageBoxResult.OK
                           );
                        }
                    }
                    else
                    {
                        MessageBoxResult result = System.Windows.MessageBox.Show(
                           messageBoxText: "Bị lỗi trong quá trình tạo chứng chỉ chuẩn\n\n",
                           caption: "Error",
                           button: MessageBoxButton.OK,
                           icon: MessageBoxImage.Error,
                           defaultResult: MessageBoxResult.OK
                           );
                    }
                }
                );
            #endregion

            #region ExportCertificateToWordCommand
            ExportCertificateToWordCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (SelectedCertificate != null)
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
                    SaveFileDialog saveFileDialog = new SaveFileDialog();

                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);



                    saveFileDialog.Title = "Save Certificate File";
                    saveFileDialog.CheckFileExists = false;
                    saveFileDialog.CheckPathExists = true;
                    saveFileDialog.DefaultExt = "docx";
                    saveFileDialog.Filter = "Word Document (*.docx)|*.docx";
                    saveFileDialog.FilterIndex = 2;
                    saveFileDialog.RestoreDirectory = true;
                    saveFileDialog.FileName = "No." + SelectedCertificate.CertificateNumber + "_"
                        + SelectedCertificate.Customer.FullName + "_"
                        + SelectedCertificate.Machine.Name + "_"
                        + SelectedCertificate.Machine.Serial;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

                        int createCertificateWordSuccess;
                        string certificateWordFileFullPath = saveFileDialog.FileName;

                        try
                        {
                            ProcessingCertificate processingCertificate = new ProcessingCertificate();

                            createCertificateWordSuccess = processingCertificate.ExportCertificateWordFile(SelectedCertificate, certificateWordFileFullPath);


                           System.Windows.MessageBox.Show(
                               messageBoxText: "Đã tạo thành công file chứng chỉ !",
                               caption: "",
                               button: MessageBoxButton.OK,
                               icon: MessageBoxImage.Information,
                               defaultResult: MessageBoxResult.OK
                               );

                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show(
                                                      messageBoxText: "Bị lỗi trong quá trình tạo chứng chỉ chuẩn\n\n" + ex.Message,
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
                }
                );
            #endregion

            #region ExportFilterCertificatesToCSVCommand
            ExportFilterCertificatesToCSVCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (FilterCertificateList.Count() >=1)
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
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                    saveFileDialog.Title = "Export Certificate List to CSV File";
                    saveFileDialog.CheckFileExists = false;
                    saveFileDialog.CheckPathExists = true;
                    saveFileDialog.DefaultExt = "csv";
                    saveFileDialog.Filter = "CSV file (*.csv)|*.csv";
                    saveFileDialog.FilterIndex = 2;
                    saveFileDialog.RestoreDirectory = true;
                    saveFileDialog.FileName = "CertificateList";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                        string CSVFileFullPath = saveFileDialog.FileName;
                        
                        try
                        {
                            ExportFilterCertificatesToCSV(FilterCertificateList, CSVFileFullPath);
                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show(
                                                      messageBoxText: "Bị lỗi trong quá trình xuất file\n\n" + ex.Message,
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

                    
                }
                );
            #endregion

            #region ViewDetailsOnlyCertificateCommand
            ViewDetailsOnlyCertificateCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (SelectedCertificate != null)
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
                    mainViewModel.CurrentControl = new CertificateFormViewModel(mainViewModel, SelectedCertificate, "ViewMode", CurrentUser);
                }
                );
            #endregion

            #region EditCertificateCommand
            EditCertificateCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (SelectedCertificate != null)
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
                    mainViewModel.CurrentControl = new CertificateFormViewModel(mainViewModel, SelectedCertificate, "EditMode", CurrentUser);
                }
                );
            #endregion

            #region AddNewCertificateBaseOnThisMachineCommand
            AddNewCertificateBaseOnThisMachineCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (SelectedCertificate != null)
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
                    SelectedCertificate.Temperature = 0;
                    SelectedCertificate.Pressure = 0;
                    SelectedCertificate.Humidity = 0;

                    SelectedCertificate.CalibDate = DateTime.Today;
                    SelectedCertificate.CertificateNumber = null;
                    SelectedCertificate.PerformedBy = null;
                    SelectedCertificate.TM = null;

                    SelectedCertificate.PerformedBy = CurrentUser.FullName;

                    foreach (CalibData calibdata in SelectedCertificate.CalibDatas)
                    {
                        calibdata.MachineReading = null;
                    }

                    mainViewModel.CurrentControl = new CertificateFormViewModel(mainViewModel, SelectedCertificate, "AddMode", CurrentUser);
                }
                );
            #endregion

            #region DeleteCertificateCommand
            DeleteCertificateCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (SelectedCertificate != null)
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
                    MessageBoxResult result1 = MessageBox.Show(
                            messageBoxText: "Bạn có chắc chắn muốn xóa chứng chỉ này ?",
                            caption: "YES/NO",
                            button: MessageBoxButton.YesNo,
                            icon: MessageBoxImage.Warning,
                            defaultResult: MessageBoxResult.No
                            );

                    if (result1 == MessageBoxResult.Yes)
                    {
                        MessageBoxResult result2 = MessageBox.Show(
                            messageBoxText: "Bạn vẫn muốn xóa chứng chỉ này ?",
                            caption: "YES/NO",
                            button: MessageBoxButton.YesNo,
                            icon: MessageBoxImage.Warning,
                            defaultResult: MessageBoxResult.No
                            );

                        if (result2 == MessageBoxResult.Yes)
                        {
                            try
                            {
                                GlobalConfig.Connection.Certificate_Delete(SelectedCertificate, GlobalConfig.CnnString("ICMSdatabase"));
                                CertificateList.Remove(SelectedCertificate);
                            }
                            catch (SqlException ex)
                            {
                                    MessageBox.Show(messageBoxText: $"{ex.Number}\n{ex.Message}\n{ex.StackTrace}",
                                                                        caption: "SQL Error",
                                                                        button: MessageBoxButton.OK,
                                                                        icon: MessageBoxImage.Error
                                                                        );
                            }
                        }
                    }
                }
                );
            #endregion

            #region GetCertificateFromDateToDateCommand
            GetCertificateFromDateToDateCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (FromDate == null | ToDate == null) { return false; } 
                    else
                    {
                        if (DateTime.Compare(FromDate, ToDate) > 0)
                        {
                            return false;
                        }
                    }
                    return true;
                },
                (p) =>
                {
                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                    try
                    {
                        CertificateList = new ObservableCollection<Certificate>(GlobalConfig.Connection.Certificate_GetFromDateToDate(FromDate, ToDate, GlobalConfig.CnnString("ICMSdatabase")));
                    }
                    catch (Exception ex)
                    {
                        MessageBoxResult result = System.Windows.MessageBox.Show(
                                                  messageBoxText: "Bị lỗi trong quá đọc dữ liệu\n\n" + ex.Message,
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

        #region private
        private void OpenViewCertificatePdfDialog(string certificatePdfFileFullPath)
        {
            //System.Windows.MessageBox.Show(
            //   messageBoxText: certificatePdfFileFullPath,
            //   caption: "Error",
            //   button: MessageBoxButton.OK,
            //   icon: MessageBoxImage.Error,
            //   defaultResult: MessageBoxResult.OK
            //   );

            try
            {
                DocumentStream = null;
                DocumentStream = new FileStream(certificatePdfFileFullPath, FileMode.Open, FileAccess.Read);


                //System.Windows.MessageBox.Show(
                //    messageBoxText: "Open file " + certificatePdfFileFullPath,
                //    caption: "Error",
                //    button: MessageBoxButton.OK,
                //    icon: MessageBoxImage.Error,
                //    defaultResult: MessageBoxResult.OK
                //    );



                DialogContent = new UC_ViewCertificatePdfFile_Dialog()
                {
                    DataContext = new ViewCertificatePdfFileDialogViewModel(DocumentStream)
                };
                IsDialogOpen = true;
               

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(
                   messageBoxText: ex.Message,
                   caption: "Error",
                   button: MessageBoxButton.OK,
                   icon: MessageBoxImage.Error,
                   defaultResult: MessageBoxResult.OK
                   );
            }

        }

        private void ExportFilterCertificatesToCSV(ObservableCollection<Certificate> FilterCertificates, string path)
        {
            string separator = ";";
            var lines = new List<string>();
 
            var header = string.Join(separator, "Calib. Date", "Customer", "Certificate No.", "Machine Name", "Model", "Serial",
                                                "Dose Quantity","Performed By", "Reviewed By");
            lines.Add(header);

            string line;
            for (int i = 0; i < FilterCertificates.Count(); i++)
            {
                line = string.Join(separator, FilterCertificates[i].CalibDate.ToString("dd-MMM-yyyy"), FilterCertificates[i].Customer.FullName, FilterCertificates[i].CertificateNumber,
                                              FilterCertificates[i].Machine.Name, FilterCertificates[i].Machine.Model, FilterCertificates[i].Machine.Serial,
                                              FilterCertificates[i].DoseQuantity.Notation, FilterCertificates[i].PerformedBy, FilterCertificates[i].TM);
                lines.Add(line);
            }

            File.WriteAllLines(path, lines.ToArray());
        }

        #endregion
    }
}
