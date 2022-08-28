using ICMS.HelperFunction;
using ICMS.Model.Models;
using ICMS.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;


namespace ICMS.View.UC_Component
{
    /// <summary>
    /// Interaction logic for UC_Customer.xaml
    /// </summary>
    public partial class UC_Customer : UserControl
    {
        public UC_Customer()
        {
            InitializeComponent();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = sender as TextBox;
            string searchString = txt.Text.Trim().ToLower();

            CustomerViewModel viewModel = this.DataContext as CustomerViewModel;

            if (searchString.Length >= 3)
            {
                bool isContain;
                ObservableCollection<Customer> tempoFilterList = new ObservableCollection<Customer>();
                foreach (Customer customer in viewModel.CustomerList)
                {

                    isContain = customer.FullName.ContainsWord(searchString);

                    if (isContain == true)
                    {
                        tempoFilterList.Add(customer);
                    }
                }

                viewModel.FilterCustomerList = tempoFilterList;
            }
            else
            {
                viewModel.FilterCustomerList = viewModel.CustomerList;
            }
        }
    }
}
