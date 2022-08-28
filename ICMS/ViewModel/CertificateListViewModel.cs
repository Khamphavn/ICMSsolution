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

namespace ICMS.ViewModel
{
    public class CertificateListViewModel : BaseViewModel
    {
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

        #region Commands
        
        public ICommand ViewAndPrintCertificateCommand { get; set; }
        public ICommand ExportCertificateToWordCommand { get; set; }
        public ICommand ViewDetailsOnlyCertificateCommand { get; set; }
        public ICommand EditCertificateCommand { get; set; }
        public ICommand AddNewCertificateBaseOnThisMachineCommand { get; set; }
        public ICommand DeleteCertificateCommand { get; set; }
        public ICommand GetCertificateFromDateToDateCommand { get; set; }
        #endregion


        public CertificateListViewModel(MainViewModel mainViewModel)
        {
            #region Init

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
                        OpenViewCertificatePdfDialog(certificatePdfFileFullPath);
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
                    mainViewModel.CurrentControl = new CertificateFormViewModel(mainViewModel, SelectedCertificate, "ViewMode");
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
                    mainViewModel.CurrentControl = new CertificateFormViewModel(mainViewModel, SelectedCertificate, "EditMode");
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

                    foreach (CalibData calibdata in SelectedCertificate.CalibDatas)
                    {
                        calibdata.MachineReading = null;
                    }

                    mainViewModel.CurrentControl = new CertificateFormViewModel(mainViewModel, SelectedCertificate, "AddMode");
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
                    //CertificateFormViewModel certificate = p as CertificateFormViewModel;
                    System.Windows.MessageBox.Show($"TODO");
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

            DialogContent = new UC_ViewCertificatePdfFile_Dialog()
            {
                DataContext = new ViewCertificatePdfFileDialogViewModel(certificatePdfFileFullPath)
            };
            IsDialogOpen = true;
        }

        #endregion
    }
}
