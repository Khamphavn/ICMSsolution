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
    public class DoseQuantityViewModel : BaseViewModel
    {
        private User _CurrentUser;
        public User CurrentUser { get => _CurrentUser; private set { _CurrentUser = value; OnPropertyChanged(); } }

        private ObservableCollection<DoseQuantity> _DoseQuantities;
        public ObservableCollection<DoseQuantity> DoseQuantities { get => _DoseQuantities; set { _DoseQuantities = value; OnPropertyChanged(); } }


        #region  Field Properties for Add and Adit
        private string _DoseQuantity_NameVN;
        public string DoseQuantity_NameVN { get => _DoseQuantity_NameVN; set { _DoseQuantity_NameVN = value; OnPropertyChanged(); } }

        private string _DoseQuantity_NameEN;
        public string DoseQuantity_NameEN { get => _DoseQuantity_NameEN; set { _DoseQuantity_NameEN = value; OnPropertyChanged(); } }

        private string _DoseQuantity_Notation;
        public string DoseQuantity_Notation { get => _DoseQuantity_Notation; set { _DoseQuantity_Notation = value; OnPropertyChanged(); } }

        private bool _DoseQuantity_IsActive;
        public bool DoseQuantity_IsActive { get => _DoseQuantity_IsActive; set { _DoseQuantity_IsActive = value; OnPropertyChanged(); } }
        #endregion

        private DoseQuantity _SelectedDoseQuantity;
        public DoseQuantity SelectedDoseQuantity
        {
            get => _SelectedDoseQuantity;
            set
            {
                _SelectedDoseQuantity = value;
                if (_SelectedDoseQuantity != null)
                {
                    DoseQuantity_NameVN = _SelectedDoseQuantity.NameVN;
                    DoseQuantity_NameEN = _SelectedDoseQuantity.NameEN;
                    DoseQuantity_Notation = _SelectedDoseQuantity.Notation;
                    DoseQuantity_IsActive = _SelectedDoseQuantity.IsActive;
                }
                OnPropertyChanged();
            }
        }

        //mode operation
        private string _CurrentOperationMode;
        public string CurrentOperationMode { get => _CurrentOperationMode; set { _CurrentOperationMode = value; OnPropertyChanged(); } }
        private enum OperationMode { NormalMode, EditMode, AddMode };


        #region Commands
        public ICommand DoseQuantityEditButtonCommand { get; set; }
        public ICommand DoseQuantityAddButtonCommand { get; set; }
        public ICommand DoseQuantityDeleteButtonCommand { get; set; }
        public ICommand DoseQuantityCancelButtonCommand { get; set; }
        public ICommand DoseQuantityClearFieldButtonCommand { get; set; }
        #endregion


        public DoseQuantityViewModel(User currentUser)
        {
            #region Init
            CurrentUser = currentUser;

            DoseQuantities = new ObservableCollection<DoseQuantity>(GlobalConfig.Connection.DoseQuantity_GetAll(GlobalConfig.CnnString("ICMSdatabase")));

            CurrentOperationMode = OperationMode.NormalMode.ToString();

            #endregion


            #region Commands

            #region DoseQuantityEditButtonCommand
            DoseQuantityEditButtonCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (SelectedDoseQuantity != null) { return true; } else { return false; }
                },
                (p) =>
                {
                    if (CurrentOperationMode == OperationMode.NormalMode.ToString())
                    {
                        CurrentOperationMode = OperationMode.EditMode.ToString();
                    }
                    else if (CurrentOperationMode == OperationMode.EditMode.ToString())
                    {
                        DoseQuantity updateDoseQuantity = new DoseQuantity()
                        {
                            DoseQuantityId = SelectedDoseQuantity.DoseQuantityId,
                            NameVN = DoseQuantity_NameVN.Trim(),
                            NameEN = DoseQuantity_NameEN.Trim(),
                            Notation = DoseQuantity_Notation,
                            IsActive = DoseQuantity_IsActive
                        };

                        // check unique NameVN and Name EN
                        List<DoseQuantity> tempoCityList = DoseQuantities.Where(s => s.DoseQuantityId != updateDoseQuantity.DoseQuantityId).ToList();

                        bool isUniqueDoseQuantityNameVN = IsUniqueDoseQuantityNameVN(updateDoseQuantity.NameVN, tempoCityList);
                        bool isUniqueDoseQuantityNameEN = IsUniqueDoseQuantityNameEN(updateDoseQuantity.NameEN, tempoCityList);

                        if (isUniqueDoseQuantityNameVN & isUniqueDoseQuantityNameEN)
                        {
                            try
                            {
                                GlobalConfig.Connection.DoseQuantity_Update(updateDoseQuantity, GlobalConfig.CnnString("ICMSdatabase"));
                                DoseQuantities = new ObservableCollection<DoseQuantity>(GlobalConfig.Connection.DoseQuantity_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
                                SelectedDoseQuantity = DoseQuantities.FirstOrDefault(s => s.DoseQuantityId == updateDoseQuantity.DoseQuantityId);
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
                                messageBoxText: "Tên tiếng Việt hoặc tiếng Anh đã tồn tại !",
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

            #region DoseQuantityAddButtonCommand
            DoseQuantityAddButtonCommand = new RelayCommand<object>(
                (p) =>
                {
                    return true;
                },
                (p) =>
                {
                    if (CurrentOperationMode == OperationMode.NormalMode.ToString())
                    {
                        CurrentOperationMode = OperationMode.AddMode.ToString();
                        DoseQuantity_NameVN = null;
                        DoseQuantity_NameEN = null;
                        DoseQuantity_Notation = null;
                        DoseQuantity_IsActive = true;

                        //SelectedCity = null;
                    }
                    else
                    {
                        // check unique NameVN and Name EN
                        bool isUniqueDoseQuantityNameVN = IsUniqueDoseQuantityNameVN(DoseQuantity_NameVN, DoseQuantities.ToList());
                        bool isUniqueDoseQuantityNameEN = IsUniqueDoseQuantityNameEN(DoseQuantity_NameEN, DoseQuantities.ToList());

                        if (isUniqueDoseQuantityNameVN & isUniqueDoseQuantityNameEN)
                        {
                            try
                            {
                                DoseQuantity insertDoseQuantity = new DoseQuantity()
                                {
                                    NameVN = DoseQuantity_NameVN.Trim(),
                                    NameEN = DoseQuantity_NameEN.Trim(),
                                    Notation = DoseQuantity_Notation,
                                    IsActive = DoseQuantity_IsActive
                                };

                                GlobalConfig.Connection.DoseQuantity_Insert(insertDoseQuantity, GlobalConfig.CnnString("ICMSdatabase"));

                                DoseQuantities = new ObservableCollection<DoseQuantity>(GlobalConfig.Connection.DoseQuantity_GetAll(GlobalConfig.CnnString("ICMSdatabase")));

                                SelectedDoseQuantity = DoseQuantities.FirstOrDefault(s => s.DoseQuantityId == insertDoseQuantity.DoseQuantityId);

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
                                messageBoxText: "Tên tiếng Việt hoặc tiếng Anh đã tồn tại !",
                                caption: "Error",
                                button: MessageBoxButton.OK,
                                icon: MessageBoxImage.Error
                                );
                        }
                    }
                }
                );
            #endregion

            #region DoseQuantityDeleteButtonCommand
            DoseQuantityDeleteButtonCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (SelectedDoseQuantity == null | DoseQuantities.Count() < 1)
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
                            messageBoxText: $"Hãy tạm khóa đại lượng \"{SelectedDoseQuantity.NameVN}\" nếu không muốn sử dụng.\n\n Bạn có muốn xóa đại lượng này ?",
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
                                    GlobalConfig.Connection.DoseQuantity_DeleteById(SelectedDoseQuantity.DoseQuantityId, GlobalConfig.CnnString("ICMSdatabase"));
                                    DoseQuantities.Remove(SelectedDoseQuantity);
                                    //TMs = new ObservableCollection<TM>(GlobalConfig.Connection.TM_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
                                }
                                catch (SqlException ex)
                                {
                                    if (ex.Number == 547)
                                    {
                                        MessageBox.Show(messageBoxText: $"Không thể xóa đại lượng \"{SelectedDoseQuantity.NameVN}\"\n\nHãy tạm khóa đại lượng này này.",
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

            #region DoseQuantityCancelButtonCommand
            DoseQuantityCancelButtonCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) => { CurrentOperationMode = OperationMode.NormalMode.ToString(); }
                );
            #endregion

            #region DoseQuantityClearFieldButtonCommand
            DoseQuantityClearFieldButtonCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) =>
                {
                    DoseQuantity_NameVN = null;
                    DoseQuantity_NameEN = null;
                    DoseQuantity_Notation = null;
                    DoseQuantity_IsActive = true;

                    SelectedDoseQuantity = null;

                    if (CurrentOperationMode == OperationMode.AddMode.ToString())
                    {
                        SelectedDoseQuantity = null;
                    }
                }
                );
            #endregion


            #endregion
        }

        #region private function
        private bool IsUniqueDoseQuantityNameVN(string name, List<DoseQuantity> doseQuantities)
        {
            DoseQuantity doseQuantity = doseQuantities.FirstOrDefault(s => s.NameVN.ToLower() == name.ToLower());
            return doseQuantity != null ? false : true;
        }

        private bool IsUniqueDoseQuantityNameEN(string name, List<DoseQuantity> doseQuantities)
        {
            DoseQuantity doseQuantity = doseQuantities.FirstOrDefault(s => s.NameEN.ToLower() == name.ToLower());
            return doseQuantity != null ? false : true;
        }

        #endregion
    }
}
