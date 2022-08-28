using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ICMS.Command;
using ICMS.Model.DataAccess;
using ICMS.Model.Models;

namespace ICMS.ViewModel
{
    public class CityViewModel: BaseViewModel
    {
        private ObservableCollection<City> _Cities;
        public ObservableCollection<City> Cities { get => _Cities; set { _Cities = value;   OnPropertyChanged(); }}

        #region  Field Properties for Add and Adit

        private string _City_Name;
        public string City_Name { get => _City_Name; set { _City_Name = value; OnPropertyChanged(); } }

        private string _City_PhoneCode;
        public string City_PhoneCode { get => _City_PhoneCode; set { _City_PhoneCode = value; OnPropertyChanged(); } }

        private bool _City_IsActive;
        public bool City_IsActive { get => _City_IsActive; set { _City_IsActive = value; OnPropertyChanged(); } }
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
                    City_Name = SelectedCity.Name;
                    City_PhoneCode = SelectedCity.PhoneCode;
                    City_IsActive = SelectedCity.IsActive;
                }
                OnPropertyChanged(); 
            } 
        }

        //mode operation
        private string _CurrentOperationMode;
        public string CurrentOperationMode {get => _CurrentOperationMode; set { _CurrentOperationMode = value; OnPropertyChanged();}}
        private enum OperationMode { NormalMode, EditMode, AddMode };

        #region Commands
        public ICommand CityEditButtonCommand { get; set; }
        public ICommand CityAddButtonCommand { get; set; }
        public ICommand CityCancelButtonCommand { get; set; }
        public ICommand CityDeleteButtonCommand { get; set; }
        public ICommand CityClearFieldButtonCommand { get; set; }
        #endregion

        public CityViewModel()
        {
            #region Init
            Cities =  new ObservableCollection<City>(GlobalConfig.Connection.City_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
            CurrentOperationMode = OperationMode.NormalMode.ToString();
            #endregion

            #region Commands

            #region CityEditButtonCommand
            CityEditButtonCommand = new RelayCommand<object>  (
                (p) =>
                {
                    if (SelectedCity != null)  {  return true; } else { return false;  }
                },
                (p) =>
                {
                    if (CurrentOperationMode == OperationMode.NormalMode.ToString())
                    {
                        CurrentOperationMode = OperationMode.EditMode.ToString();
                    }
                    else if (CurrentOperationMode == OperationMode.EditMode.ToString())
                    {
                        City updateCity = new City()
                        {
                            CityId = SelectedCity.CityId,
                            Name = City_Name.Trim(),
                            PhoneCode = City_PhoneCode.Trim(),
                            IsActive = City_IsActive
                        };

                        // check unique City Name and PhoneCode

                        List<City> tempoCityList = Cities.Where(s => s.CityId != updateCity.CityId).ToList();

                        bool isUniqueName = isUniqueCityName(updateCity.Name, tempoCityList);
                        bool isUniquePhoneCode = isUniqueCityPhoneCode(updateCity.PhoneCode, tempoCityList);

                        int updateCityResult;

                        if (isUniqueName & isUniquePhoneCode)
                        {
                            MessageBox.Show("Update city infos into database !");

                            try
                            {
                                updateCityResult = GlobalConfig.Connection.City_Update(updateCity, GlobalConfig.CnnString("ICMSdatabase"));
                                Cities = new ObservableCollection<City>(GlobalConfig.Connection.City_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
                                SelectedCity = Cities.FirstOrDefault(s => s.CityId == updateCity.CityId);
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
                                messageBoxText: "Tên thành phố hoặc mã điện thoại đã tồn tại !",
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

            #region CityAddButtonCommand
            CityAddButtonCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) =>
                {
                    if (CurrentOperationMode == OperationMode.NormalMode.ToString())
                    {
                        CurrentOperationMode = OperationMode.AddMode.ToString();
                        City_Name = null;
                        City_PhoneCode = null;
                        City_IsActive = true;

                        SelectedCity = null;
                    }
                    else
                    {
                        // check unique City Name and PhoneCode
                        bool isUniqueName   = isUniqueCityName(City_Name, Cities.ToList());
                        bool isUniquePhoneCode = isUniqueCityPhoneCode(City_PhoneCode, Cities.ToList());

                        if (isUniqueName & isUniquePhoneCode)
                        {
                            MessageBox.Show("Insert a new city into database !");

                            try
                            {
                                City insertCity = new City() 
                                {
                                    Name = City_Name,
                                    PhoneCode = City_PhoneCode,
                                    IsActive = City_IsActive
                                };

                                GlobalConfig.Connection.City_Insert(insertCity, GlobalConfig.CnnString("ICMSdatabase"));

                                Cities = null;
                                Cities = new ObservableCollection<City>(GlobalConfig.Connection.City_GetAll(GlobalConfig.CnnString("ICMSdatabase")));

                                SelectedCity = Cities.FirstOrDefault(s => s.CityId == insertCity.CityId);

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
                            CurrentOperationMode = OperationMode.NormalMode.ToString();
                        }
                        else
                        {
                            MessageBox.Show(
                                messageBoxText: "Tên thành phố hoặc mã điện thoại đã tồn tại !",
                                caption: "Error",
                                button: MessageBoxButton.OK,
                                icon: MessageBoxImage.Error
                                );
                        }


                        
                    }
                }
                );
            #endregion

            #region CityCancelButtonCommand
            CityCancelButtonCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) => { CurrentOperationMode = OperationMode.NormalMode.ToString(); }
                );
            #endregion

            #region CityDeleteButtonCommand
            CityDeleteButtonCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (SelectedCity == null | Cities.Count() < 1)
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
                            try
                            {
                                GlobalConfig.Connection.City_DeleteById(SelectedCity.CityId, GlobalConfig.CnnString("ICMSdatabase"));
                                Cities.Remove(SelectedCity);
                                //TMs = new ObservableCollection<TM>(GlobalConfig.Connection.TM_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
                            }
                            catch (Exception ex)
                            {
                            //messageBoxText: "Không thể xóa thông tin về tỉnh/thành này !",
                                
                                MessageBox.Show(
                                    messageBoxText: $"{ex.Message}\n{ex.StackTrace}",
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

            #region CityClearFieldButtonCommand
            CityClearFieldButtonCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) =>
                {
                    City_Name = null;
                    City_PhoneCode = null;
                    City_IsActive = false;
                    SelectedCity = null;

                    if (CurrentOperationMode == OperationMode.AddMode.ToString())
                    {
                        SelectedCity = null;
                    }
                }
                );
            #endregion

            #endregion
        }

        #region private function
        private bool isUniqueCityName(string cityName, List<City> cities)
        {
            City city = cities.FirstOrDefault(s => s.Name.ToLower() == cityName.ToLower());
            return city != null ? false : true;
        }

        private bool isUniqueCityPhoneCode(string cityPhoneCode, List<City> cities)
        {
            City city = cities.FirstOrDefault(s => s.PhoneCode == cityPhoneCode);
            return city != null ? false : true;
        }

        #endregion
    }
}
