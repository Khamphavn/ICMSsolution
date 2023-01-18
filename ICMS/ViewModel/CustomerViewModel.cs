using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ICMS.Command;
using ICMS.Model.DataAccess;
using ICMS.Model.Models;

namespace ICMS.ViewModel
{
    public class CustomerViewModel : BaseViewModel
    {
        private User _CurrentUser;
        public User CurrentUser { get => _CurrentUser; private set { _CurrentUser = value; OnPropertyChanged(); } }

        private ObservableCollection<Customer> _CustomerList;
        public ObservableCollection<Customer> CustomerList { get => _CustomerList; set { 
                _CustomerList = value; 
                if (CustomerList != null)
                {
                    FilterCustomerList = CustomerList;
                    CustomerFullNameList = CustomerList.Select(p => p.FullName).ToList();
                }
                OnPropertyChanged(); 
            } 
        }

        private ObservableCollection<Customer> _FilterCustomerList;
        public ObservableCollection<Customer> FilterCustomerList { get => _FilterCustomerList; set { _FilterCustomerList = value; OnPropertyChanged(); }}

        public List<City> CityList { get; }

        #region  Field Properties for Add and Adit
        private List<string> _CustomerFullNameList;
        private string _Customer_ShortName;
        private string _Customer_FullName;
        private string _Customer_Address;
        private string _Customer_CityName;
        private string _Customer_Phone;
        public string Customer_ShortName { get => _Customer_ShortName; set { _Customer_ShortName = value; OnPropertyChanged(); } }
        public string Customer_FullName { get => _Customer_FullName; set { _Customer_FullName = value; OnPropertyChanged(); } }
        public string Customer_Address { get => _Customer_Address; set { _Customer_Address = value; OnPropertyChanged(); } }
        public string Customer_CityName { get => _Customer_CityName; set { _Customer_CityName = value; OnPropertyChanged(); } }
        public string Customer_Phone { get => _Customer_Phone; set { _Customer_Phone = value; OnPropertyChanged(); } }
        public List<string> CustomerFullNameList { get => _CustomerFullNameList; set { _CustomerFullNameList = value; OnPropertyChanged(); } }
        #endregion


        private City _SelectedCity;
        public City SelectedCity
        {
            get => _SelectedCity;
            set
            {
                _SelectedCity = value;

                if (SelectedCity != null)
                {
                    Customer_CityName = SelectedCity.Name;
                }
                OnPropertyChanged();
            }
        }


        private Customer _SelectedCustomer;
        public Customer SelectedCustomer
        {
            get => _SelectedCustomer;
            set
            {
                _SelectedCustomer = value;

                if (SelectedCustomer != null)
                {
                    Customer_ShortName = SelectedCustomer.ShortName;
                    Customer_FullName = SelectedCustomer.FullName;
                    Customer_Address = SelectedCustomer.Address;
                    //Customer_CityName = SelectedCustomer.City.Name;

                    //SelectedCity = (City)CityList.Select(p => p.Name == SelectedCustomer.City.Name);

                    if (SelectedCustomer.City != null)
                    {
                        SelectedCity = CityList.FirstOrDefault(s => s.CityId == SelectedCustomer.City.CityId);
                    }
                    else
                    {
                        SelectedCity = null;
                    }
                }
                OnPropertyChanged();
            }
        }


        //mode operation
        private string _CurrentOperationMode;
        public string CurrentOperationMode
        {
            get => _CurrentOperationMode;
            set
            {
                _CurrentOperationMode = value;

                OnPropertyChanged();
            }
        }

        private enum OperationMode{ NormalMode, EditMode, AddMode };


        #region Commands
        public ICommand CustomerEditButtonCommand { get; set; }
        public ICommand CustomerAddButtonCommand { get; set; }
        public ICommand CustomerCancelButtonCommand { get; set; }
        public ICommand CustomerDeleteButtonCommand { get; set; }
        public ICommand CustomerClearFieldButtonCommand { get; set; }
        #endregion

          
        public CustomerViewModel(User currentUser)
        {
            #region Init
            CurrentUser = currentUser;

            CustomerList = new ObservableCollection<Customer>(GlobalConfig.Connection.Customer_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
            FilterCustomerList = CustomerList;
            CityList = GlobalConfig.Connection.City_GetActive(GlobalConfig.CnnString("ICMSdatabase"));

            SelectedCustomer = null;
            SelectedCity = null;
            //CustomerList.Insert(0, new Customer());

            CurrentOperationMode = OperationMode.NormalMode.ToString();
            #endregion


            #region Commands

            #region CustomerEditButtonCommand
            CustomerEditButtonCommand = new RelayCommand<object>
                (
                (p) => 
                    { 
                        if (SelectedCustomer != null)
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
                        if (CurrentOperationMode == OperationMode.NormalMode.ToString())
                        {
                            CurrentOperationMode = OperationMode.EditMode.ToString();
                        }
                        else if (CurrentOperationMode == OperationMode.EditMode.ToString())
                        {
                            Customer updateCustomer = new Customer()
                            {
                                CustomerId = SelectedCustomer.CustomerId,
                                ShortName = Customer_ShortName,
                                FullName = Customer_FullName.Trim(),
                                Address = Customer_Address.Trim(),
                                City = new City() { Name = Customer_CityName.Trim() },
                            };

                            // check unique Customer.FullName
                            List<Customer> tempoCustomerList = CustomerList.Where(s => s.CustomerId != updateCustomer.CustomerId).ToList();

                            bool isUniqueCustomerFullName = IsUniqueCustomerFullName(updateCustomer.FullName, tempoCustomerList);

                            if (isUniqueCustomerFullName)
                            {
                                try
                                {
                                    GlobalConfig.Connection.Customer_Update(updateCustomer, GlobalConfig.CnnString("ICMSdatabase"));
                                    CustomerList = new ObservableCollection<Customer>(GlobalConfig.Connection.Customer_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
                                    SelectedCustomer = CustomerList.FirstOrDefault(s => s.CustomerId == updateCustomer.CustomerId);
                                    CurrentOperationMode = OperationMode.NormalMode.ToString();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(
                                                messageBoxText: $"{ex.Message}\n\n{ex.StackTrace}" ,
                                                caption: "SQL Error",
                                                button: MessageBoxButton.OK,
                                                icon: MessageBoxImage.Error
                                                );
                                }
                            }
                            else
                            {
                                MessageBox.Show(
                                    messageBoxText: "Tên đơn vị đã tồn tại !",
                                    caption: "Error",
                                    button: MessageBoxButton.OK,
                                    icon: MessageBoxImage.Error
                                    );
                            }
                        }
                        else
                        {
                            CurrentOperationMode = OperationMode.NormalMode.ToString();
                        }
                        
                    }
                );
            #endregion

            #region CustomerAddButtonCommand
            CustomerAddButtonCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) =>
                {
                    if (CurrentOperationMode == OperationMode.NormalMode.ToString())
                    {
                        CurrentOperationMode = OperationMode.AddMode.ToString();
                        Customer_ShortName = null;
                        Customer_FullName = null;
                        Customer_Address = null;
                        Customer_CityName = null;
                        Customer_Phone = null;

                        //SelectedCity = null;
                    }
                    else
                    {
                        Customer newCustomer = new Customer()
                        {
                            ShortName = Customer_ShortName,
                            FullName = Customer_FullName.Trim(),
                            Address = Customer_Address.Trim(),
                            City = SelectedCity
                        };

                        // check unique Customer.FullName
                        bool isUniqueCustomerFullName = IsUniqueCustomerFullName(newCustomer.FullName, CustomerList.ToList());

                        if (isUniqueCustomerFullName)
                        {
                            try
                            {
                                GlobalConfig.Connection.Customer_Insert(newCustomer, GlobalConfig.CnnString("ICMSdatabase"));
                                CustomerList.Add(newCustomer);
                                SelectedCustomer = (Customer)CustomerList.FirstOrDefault(s => s.CustomerId == newCustomer.CustomerId);
                                CurrentOperationMode = OperationMode.NormalMode.ToString();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(
                                            messageBoxText: $"{ex.Message}\n\n{ex.StackTrace}",
                                            caption: "SQL Error",
                                            button: MessageBoxButton.OK,
                                            icon: MessageBoxImage.Error
                                            );
                            }
                        }
                        else
                        {
                            MessageBox.Show(
                                messageBoxText: "Tên đơn vị đã tồn tại !",
                                caption: "Error",
                                button: MessageBoxButton.OK,
                                icon: MessageBoxImage.Error
                                );
                        }
                    }

                }
                );
            #endregion

            #region CustomerCancelButtonCommand
            CustomerCancelButtonCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) => { CurrentOperationMode = OperationMode.NormalMode.ToString(); }
                );
            #endregion

            #region CustomerDeleteButtonCommand
            CustomerDeleteButtonCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (SelectedCustomer == null | CustomerList.Count() < 1)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                },
                (p) =>
                {
                    if (CurrentOperationMode == OperationMode.NormalMode.ToString())
                    {
                        MessageBoxResult result = MessageBox.Show(
                            messageBoxText: "Bạn có chắc chắn muốn xóa ?",
                            caption: "YES/NO",
                            button: MessageBoxButton.YesNo,
                            icon: MessageBoxImage.Warning,
                            defaultResult: MessageBoxResult.No
                            );

                        if (result == MessageBoxResult.Yes)
                        {
                            MessageBoxResult result2 = MessageBox.Show(
                            messageBoxText: "Bạn vẫn muốn xóa ?",
                            caption: "YES/NO",
                            button: MessageBoxButton.YesNo,
                            icon: MessageBoxImage.Warning,
                            defaultResult: MessageBoxResult.No
                            );

                            if (result2 == MessageBoxResult.Yes)
                            {
                                try
                                {
                                    GlobalConfig.Connection.Customer_DeleteById(SelectedCustomer.CustomerId, GlobalConfig.CnnString("ICMSdatabase"));
                                    CustomerList.Remove(SelectedCustomer);
                                    FilterCustomerList.Remove(SelectedCustomer);
                                }
                                catch (SqlException ex)
                                {
                                    if (ex.Number == 547)
                                    {
                                        MessageBox.Show(messageBoxText: $"Không thể xóa đơn vị \"{SelectedCustomer.FullName}\"\n\nHãy đổi tên hoặc sửa các thông tin chưa chính xác.",
                                                        caption: "SQL Error",
                                                        button: MessageBoxButton.OK,
                                                        icon: MessageBoxImage.Error
                                                        );
                                    }
                                    else
                                    {
                                        MessageBox.Show(
                                                                            messageBoxText: $"{ex.Number}\n{ex.Message}\n{ex.StackTrace}",
                                                                            caption: "SQL Error",
                                                                            button: MessageBoxButton.OK,
                                                                            icon: MessageBoxImage.Error
                                                                            );
                                    }
                                }
                            }    
                        }
                    }
                }
                );
            #endregion

            #region CustomerClearFieldButtonCommand
            CustomerClearFieldButtonCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) =>
                {
                    Customer_ShortName = null;
                    Customer_FullName = null;
                    Customer_Address = null;
                    Customer_CityName = null;
                    Customer_Phone = null;

                    SelectedCity = null;

                    if (CurrentOperationMode == OperationMode.AddMode.ToString())
                    {
                        SelectedCustomer = null;
                    }

                }
                );
            #endregion

            #endregion
        }

        #region private function
        private bool IsUniqueCustomerFullName(string customerName, List<Customer> customerList)
        {
            Customer customer = customerList.FirstOrDefault(s => s.FullName.ToLower() == customerName.ToLower());
            return customer != null ? false : true;
        }

        #endregion
    }
}
