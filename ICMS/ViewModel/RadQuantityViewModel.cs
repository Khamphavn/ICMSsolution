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
    public class RadQuantityViewModel : BaseViewModel
    {
        private User _CurrentUser;
        public User CurrentUser { get => _CurrentUser; private set { _CurrentUser = value; OnPropertyChanged(); } }

        private ObservableCollection<RadQuantity> _RadQuantities;
        public ObservableCollection<RadQuantity> RadQuantities { get => _RadQuantities; set { _RadQuantities = value; OnPropertyChanged(); } }


        private RadQuantity _SelectedRadQuantity;
        public RadQuantity SelectedRadQuantity
        {
            get => _SelectedRadQuantity;
            set
            {
                _SelectedRadQuantity = value;
                if (SelectedRadQuantity != null)
                {
                    RadQuantity_NameVN = SelectedRadQuantity.NameVN;
                    RadQuantity_NameEN = SelectedRadQuantity.NameEN;
                    RadQuantity_Energy = SelectedRadQuantity.Energy;
                    RadQuantity_ReUnc = SelectedRadQuantity.ReUnc;
                    RadQuantity_IsActive = SelectedRadQuantity.IsActive;
                }
                OnPropertyChanged();
            }
        }

        private string _RadQuantity_NameVN;
        public string RadQuantity_NameVN { get => _RadQuantity_NameVN; set { _RadQuantity_NameVN = value; OnPropertyChanged(); } }

        private string _RadQuantity_NameEN;
        public string RadQuantity_NameEN { get => _RadQuantity_NameEN; set { _RadQuantity_NameEN = value; OnPropertyChanged(); } }

        private double _RadQuantity_Energy;
        public double RadQuantity_Energy { get => _RadQuantity_Energy; set { _RadQuantity_Energy = value; OnPropertyChanged(); } }

        private double _RadQuantity_ReUnc;
        public double RadQuantity_ReUnc { get => _RadQuantity_ReUnc; set { _RadQuantity_ReUnc = value; OnPropertyChanged(); } }

        private bool _RadQuantity_IsActive;
        public bool RadQuantity_IsActive { get => _RadQuantity_IsActive; set { _RadQuantity_IsActive = value; OnPropertyChanged(); } }

        //mode operation
        private enum OperationMode { NormalMode, EditMode, AddMode };
        private string _CurrentOperationMode;
        public string CurrentOperationMode { get => _CurrentOperationMode; set { _CurrentOperationMode = value; OnPropertyChanged(); } }

        #region Command
        public ICommand RadQuantityEditButtonCommand { get; set; }
        public ICommand RadQuantityAddButtonCommand { get; set; }
        public ICommand RadQuantityDeleteButtonCommand { get; set; }
        public ICommand RadQuantityCancelButtonCommand { get; set; }
        #endregion


        public RadQuantityViewModel(User currentUser)
        {
            #region Init
            CurrentUser = currentUser;
            RadQuantities = new ObservableCollection<RadQuantity>(GlobalConfig.Connection.RadQuantity_GetAll(GlobalConfig.CnnString("ICMSdatabase")));

            CurrentOperationMode = OperationMode.NormalMode.ToString();
            #endregion

            #region Commands

            #region RadQuantityAddButtonCommand
            RadQuantityAddButtonCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    return true;
                },
                (p) =>
                {
                    if (CurrentOperationMode == OperationMode.NormalMode.ToString())
                    {
                        RadQuantity_NameVN = null;
                        RadQuantity_NameEN = null;
                        RadQuantity_Energy = 0;
                        RadQuantity_ReUnc = 0;
                        SelectedRadQuantity = null;
                        CurrentOperationMode = OperationMode.AddMode.ToString();
                    }
                    else if (CurrentOperationMode == OperationMode.AddMode.ToString())
                    {
                        RadQuantity insertRadQuantity = new RadQuantity()
                        {
                            NameVN = RadQuantity_NameVN.Trim(),
                            NameEN = RadQuantity_NameEN.Trim(),
                            Energy = RadQuantity_Energy,
                            ReUnc = RadQuantity_ReUnc,
                            IsActive = RadQuantity_IsActive
                        };

                        // check unique RadQuantity.Name VN, EN
                        bool isUniqueNameVN = isUniqueRadQuantity_NameVN(insertRadQuantity.NameVN, RadQuantities.ToList());
                        bool isUniqueNameEN = isUniqueRadQuantity_NameEN(insertRadQuantity.NameEN, RadQuantities.ToList());

                        if (isUniqueNameVN & isUniqueNameVN)
                        {
                            try
                            {
                                GlobalConfig.Connection.RadQuantity_Insert(insertRadQuantity, GlobalConfig.CnnString("ICMSdatabase"));

                                RadQuantities.Add(insertRadQuantity);

                                SelectedRadQuantity = RadQuantities.FirstOrDefault(s => s.RadQuantityId == insertRadQuantity.RadQuantityId);

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
                                messageBoxText: "Tên bức xạ đã tồn tại !",
                                caption: "",
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

            #region RadQuantityEditButtonCommand
            RadQuantityEditButtonCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (SelectedRadQuantity != null) { return true; } else { return false; }
                },
                (p) =>
                {
                    if (CurrentOperationMode == OperationMode.NormalMode.ToString())
                    {
                        CurrentOperationMode = OperationMode.EditMode.ToString();
                    }
                    else if (CurrentOperationMode == OperationMode.EditMode.ToString())
                    {
                        RadQuantity updateRadQuantity = new RadQuantity()
                        {
                            RadQuantityId = SelectedRadQuantity.RadQuantityId,
                            NameVN = RadQuantity_NameVN.Trim(),
                            NameEN = RadQuantity_NameEN.Trim(),
                            Energy = RadQuantity_Energy,
                            ReUnc = RadQuantity_ReUnc,
                            IsActive = RadQuantity_IsActive
                        };

                        List<RadQuantity> tempoRadQuantityList = RadQuantities.Where(s => s.RadQuantityId != updateRadQuantity.RadQuantityId).ToList();



                        // check unique RadQuantity.Name VN, EN
                        bool isUniqueNameVN = isUniqueRadQuantity_NameVN(updateRadQuantity.NameVN, tempoRadQuantityList);
                        bool isUniqueNameEN = isUniqueRadQuantity_NameEN(updateRadQuantity.NameEN, tempoRadQuantityList);

                        if (isUniqueNameVN & isUniqueNameEN)
                        {
                            try
                            {
                                GlobalConfig.Connection.RadQuantity_Update(updateRadQuantity, GlobalConfig.CnnString("ICMSdatabase"));

                                RadQuantities = new ObservableCollection<RadQuantity>(GlobalConfig.Connection.RadQuantity_GetAll(GlobalConfig.CnnString("ICMSdatabase")));

                                SelectedRadQuantity = RadQuantities.FirstOrDefault(s => s.RadQuantityId == updateRadQuantity.RadQuantityId);

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
                                messageBoxText: "Tên bức xạ đã tồn tại !",
                                caption: "",
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

            #region RadQuantityDeleteButtonCommand
            RadQuantityDeleteButtonCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (SelectedRadQuantity == null | RadQuantities.Count() <= 1)
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
                            messageBoxText: $"Thay vì xóa phẩm chất bức xạ này, hãy tạm khóa nó lại.\n\n Bạn có muốn xóa bức xạ \"{SelectedRadQuantity.NameVN}\" ?",
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
                                    GlobalConfig.Connection.RadQuantity_DeleteById(SelectedRadQuantity.RadQuantityId, GlobalConfig.CnnString("ICMSdatabase"));
                                    RadQuantities.Remove(SelectedRadQuantity);
                                    //TMs = new ObservableCollection<TM>(GlobalConfig.Connection.TM_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
                                }
                                catch (SqlException ex)
                                {
                                    if (ex.Number == 547)
                                    {
                                        MessageBox.Show(messageBoxText: $"Không thể xóa phẩm chất bức xạ \"{SelectedRadQuantity.NameVN}\"\n\nHãy tạm khóa phẩm chất bức xạ này.",
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

            #region RadQuantityCancelButtonCommand
            RadQuantityCancelButtonCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) => { CurrentOperationMode = OperationMode.NormalMode.ToString(); }
                );
            #endregion

            #endregion
        }

        #region private function
        private bool isUniqueRadQuantity_NameVN(string radName, List<RadQuantity> RadQuantities)
        {
            RadQuantity radQuantity = RadQuantities.FirstOrDefault(s => s.NameVN.ToLower() == radName.ToLower());
            return radQuantity != null ? false : true;
        }

        private bool isUniqueRadQuantity_NameEN(string radName, List<RadQuantity> RadQuantities)
        {
            RadQuantity radQuantity = RadQuantities.FirstOrDefault(s => s.NameEN.ToLower() == radName.ToLower());
            return radQuantity != null ? false : true;
        }

        #endregion
    }
}
