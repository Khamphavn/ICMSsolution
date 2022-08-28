using ICMS.HelperFunction;
using ICMS.Model.Models;
using ICMS.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;


namespace ICMS.View.UC_Component
{
    /// <summary>
    /// Interaction logic for UC_CertificateList.xaml
    /// </summary>
    public partial class UC_CertificateList : UserControl
    {
        public UC_CertificateList()
        {
            InitializeComponent();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = sender as TextBox;
            string searchString = txt.Text.Trim().ToLower();

            CertificateListViewModel viewModel = this.DataContext as CertificateListViewModel;

            // Customer Name
            if (Search_CustomerName_TextBox.Text.Length >= 3)
            {
                Search_CustomerName_Field(viewModel);
            }
            // CalibDate
            if (StringIsDate(Search_CalibDate_TextBox.Text))
            {
                Search_CalibDate_Field(viewModel);
            }

            // Certificate Number
            if (Search_CertificateNumber_TextBox.Text.Length >= 3)
            {
                Search_CertificateNumber_Field(viewModel);
            }

            // Machine Serial 
            if (Search_MachineSerial_TextBox.Text.Length >= 3)
            {
                Search_MachineSerial_Field(viewModel);
            }

            // Performer
            if (Search_Performer_TextBox.Text.Length >= 3)
            {
                Search_Performer_Field(viewModel);
            }

            // if all search box is empty -> restore certificate List
            if (string.IsNullOrWhiteSpace(Search_CustomerName_TextBox.Text) &
                string.IsNullOrWhiteSpace(Search_CalibDate_TextBox.Text) &
                string.IsNullOrWhiteSpace(Search_CertificateNumber_TextBox.Text) &
                string.IsNullOrWhiteSpace(Search_MachineModel_TextBox.Text) &
                string.IsNullOrWhiteSpace(Search_MachineSerial_TextBox.Text) &
                string.IsNullOrWhiteSpace(Search_Performer_TextBox.Text)
                )
            {
                viewModel.FilterCertificateList = viewModel.CertificateList;
            }

        }

        private void Search_CustomerName_Field(CertificateListViewModel viewModel)
        {
            bool isContain;
            string searchString = Search_CustomerName_TextBox.Text;
            ObservableCollection<Certificate> tempoFilterList = new ObservableCollection<Certificate>();

            foreach (Certificate certificate in viewModel.FilterCertificateList)
            {
                isContain = certificate.Customer.FullName.ContainsWord(searchString);

                if (isContain == true)
                {
                    tempoFilterList.Add(certificate);
                }
            }
            viewModel.FilterCertificateList = tempoFilterList;
        }

        private bool StringIsDate(string str)
        {
            return DateTime.TryParseExact(str, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }
        private void Search_CalibDate_Field(CertificateListViewModel viewModel)
        {
            string searchString = Search_CalibDate_TextBox.Text;
            DateTime.TryParseExact(searchString, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime searchDate);

            ObservableCollection<Certificate> tempoFilterList = new ObservableCollection<Certificate>
                    (
                    viewModel.FilterCertificateList.Where(s => s.CalibDate == searchDate)
                    );
            viewModel.FilterCertificateList = tempoFilterList;
        }

        private void Search_CertificateNumber_Field(CertificateListViewModel viewModel)
        {
            string searchString = Search_CertificateNumber_TextBox.Text;
            ObservableCollection<Certificate> tempoFilterList = new ObservableCollection<Certificate>
                    (
                        viewModel.FilterCertificateList.Where(s => s.CertificateNumber.Contains(searchString))
                        );
            viewModel.FilterCertificateList = tempoFilterList;
        }

        private void Search_MachineSerial_Field(CertificateListViewModel viewModel)
        {
            string searchString = Search_MachineSerial_TextBox.Text;
            StringComparison comp = StringComparison.OrdinalIgnoreCase;

            ObservableCollection<Certificate> tempoFilterList = new ObservableCollection<Certificate>
                (
                    viewModel.FilterCertificateList.Where(s => s.Machine.Serial.ContainsWithOptionStringComparison(searchString, comp))
                    );
            viewModel.FilterCertificateList = tempoFilterList;
        }

        private void Search_Performer_Field(CertificateListViewModel viewModel)
        {
            string searchString = Search_Performer_TextBox.Text;

            ObservableCollection<Certificate> tempoFilterList = new ObservableCollection<Certificate>
                (
                    viewModel.FilterCertificateList.Where(s => s.PerformedBy.ContainsWord(searchString))
                    );
            viewModel.FilterCertificateList = tempoFilterList;

        }

        private void SearchCustomer_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = sender as TextBox;
            string searchString = txt.Text.Trim().ToLower();

            CertificateListViewModel viewModel = this.DataContext as CertificateListViewModel;

            if (searchString.Length >= 3)
            {
                bool isContain;
                ObservableCollection<Certificate> tempoFilterList = new ObservableCollection<Certificate>();
                foreach (Certificate certificate in viewModel.FilterCertificateList)
                {
                    isContain = certificate.Customer.FullName.ContainsWord(searchString);

                    if (isContain == true)
                    {
                        tempoFilterList.Add(certificate);
                    }
                }
                viewModel.FilterCertificateList = tempoFilterList;
            }
            else
            {
                viewModel.FilterCertificateList = viewModel.CertificateList;
            }
        }

        private void SearchCalibDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = sender as TextBox;
            string searchString = txt.Text.Trim().ToLower();

            CertificateListViewModel viewModel = this.DataContext as CertificateListViewModel;

            if (searchString.Length >= 8 & searchString.Length <= 10)
            {
                bool isDate = DateTime.TryParseExact(searchString, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime searchDate);
                if (isDate)
                {
                    ObservableCollection<Certificate> tempoFilterList = new ObservableCollection<Certificate>
                        (
                        viewModel.FilterCertificateList.Where(s => s.CalibDate == searchDate)
                        );
                    viewModel.FilterCertificateList = tempoFilterList;
                }
            }
            else
            {
                viewModel.FilterCertificateList = viewModel.CertificateList; // refresh certificate list when search box is empty
            }
        }

        private void SearchCertificateNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = sender as TextBox;
            string searchString = txt.Text.Trim().ToLower();

            CertificateListViewModel viewModel = this.DataContext as CertificateListViewModel;

            if (searchString.Length >= 2 & searchString.Length <= 15)
            {
                //StringComparison comp = StringComparison.OrdinalIgnoreCase;

                ObservableCollection<Certificate> tempoFilterList = new ObservableCollection<Certificate>
                    (
                        viewModel.FilterCertificateList.Where(s => s.CertificateNumber.Contains(searchString))
                        );
                viewModel.FilterCertificateList = tempoFilterList;
            }
            else
            {
                viewModel.FilterCertificateList = viewModel.CertificateList;
            }
        }

        private void SearchMachineSerial_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = sender as TextBox;
            string searchString = txt.Text.Trim().ToLower();

            CertificateListViewModel viewModel = this.DataContext as CertificateListViewModel;

            if (searchString.Length >= 2 & searchString.Length <= 20)
            {
                StringComparison comp = StringComparison.OrdinalIgnoreCase;

                ObservableCollection<Certificate> tempoFilterList = new ObservableCollection<Certificate>
                    (
                        viewModel.FilterCertificateList.Where(s => s.Machine.Serial.ContainsWithOptionStringComparison(searchString, comp))
                        );
                viewModel.FilterCertificateList = tempoFilterList;
            }
            else
            {
                viewModel.FilterCertificateList = viewModel.CertificateList;
            }
        }

        private void SearchPerformer_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = sender as TextBox;
            string searchString = txt.Text.Trim().ToLower();

            CertificateListViewModel viewModel = this.DataContext as CertificateListViewModel;

            if (searchString.Length >= 3 & searchString.Length <= 30)
            {
                //StringComparison comp = StringComparison.InvariantCultureIgnoreCase;

                ObservableCollection<Certificate> tempoFilterList = new ObservableCollection<Certificate>
                    (
                        viewModel.FilterCertificateList.Where(s => s.PerformedBy.ContainsWord(searchString))
                        );
                viewModel.FilterCertificateList = tempoFilterList;
            }
            else
            {
                viewModel.FilterCertificateList = viewModel.CertificateList;
            }
        }
    }
}
