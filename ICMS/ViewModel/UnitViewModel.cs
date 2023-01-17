using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ICMS.Command;
using ICMS.Model.DataAccess;
using ICMS.Model.Models;
using ICMS.ViewModel;

namespace ICMS.ViewModel
{
    public class UnitViewModel : BaseViewModel
    {
        private User _CurrentUser;
        public User CurrentUser { get => _CurrentUser; private set { _CurrentUser = value; OnPropertyChanged(); } }



        private ObservableCollection<Unit> _AllUnitList;
        public ObservableCollection<Unit> AllUnitList
        {
            get => _AllUnitList; set
            {
                _AllUnitList = value;
                OnPropertyChanged();
            }
        }

        #region  Field Properties for Add and Edit
        private string _Unit_Name;
        private bool _Unit_IsActive;
        private int _Unit_Order;  


        public string Unit_Name { get => _Unit_Name; set { _Unit_Name = value; OnPropertyChanged(); } }
        public bool Unit_IsActive { get => _Unit_IsActive; set { _Unit_IsActive = value; OnPropertyChanged(); } }
        public int Unit_Order { get => _Unit_Order; set { _Unit_Order = value; OnPropertyChanged(); } }


        private Unit _SelectedUnit;
        public Unit SelectedUnit
        {
            get => _SelectedUnit;
            set
            {
                _SelectedUnit = value;

                if (SelectedUnit != null)
                {
                    Unit_Name = SelectedUnit.Name;
                    Unit_IsActive = SelectedUnit.IsActive;
                    Unit_Order = SelectedUnit.Order;
                }
                OnPropertyChanged();
            }
        }
        #endregion


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

        private enum OperationMode { NormalMode, EditMode, AddMode };

        #region Commands
        public ICommand UnitEditButtonCommand { get; set; }
        public ICommand UnitAddButtonCommand { get; set; }
        public ICommand UnitCancelButtonCommand { get; set; }
        public ICommand UnitDeleteButtonCommand { get; set; }
        #endregion




        #region Constructor
        public UnitViewModel(User currentUser)
        {
            CurrentUser = currentUser;

            AllUnitList = new ObservableCollection<Unit>(GlobalConfig.Connection.Unit_GetAll(GlobalConfig.CnnString("ICMSdatabase")));

            SelectedUnit = null;

            CurrentOperationMode = OperationMode.NormalMode.ToString();

            #region Commands

            #region UnitEditButtonCommand
            UnitEditButtonCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (SelectedUnit != null)
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

                        Unit updateUnit = new Unit()
                        {
                            UnitId = SelectedUnit.UnitId,
                            Name = Unit_Name.Trim(),
                            Order = Unit_Order,
                            IsActive = Unit_IsActive
                        };

                        //Check Unique Unit name
                        bool isUniqueUnitName = true;
                        foreach (Unit unit in AllUnitList)
                        {
                            if (unit.UnitId != updateUnit.UnitId & unit.Name.ToLower() == updateUnit.Name.ToLower())
                            {
                                isUniqueUnitName = false;
                            }
                        }
                        if (isUniqueUnitName)
                        {
                            try
                            {
                                GlobalConfig.Connection.Unit_Update(updateUnit, GlobalConfig.CnnString("ICMSdatabase"));

                                // Update AllUnitList
                                AllUnitList = new ObservableCollection<Unit>(GlobalConfig.Connection.Unit_GetAll(GlobalConfig.CnnString("ICMSdatabase")));

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
                                   messageBoxText: "Tên đơn vị đo đã tồn tại!",
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

            #region UnitAddButtonCommand
            UnitAddButtonCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) =>
                {
                    if (CurrentOperationMode == OperationMode.NormalMode.ToString())
                    {
                        CurrentOperationMode = OperationMode.AddMode.ToString();
                        Unit_Name = null;
                        Unit_IsActive = false;
                        Unit_Order = 1;

                        SelectedUnit = null;
                    }
                    else
                    {
                        Unit insertUnit = new Unit()
                        {
                            Name = Unit_Name.Trim(),
                            Order = Unit_Order,
                            IsActive = Unit_IsActive
                        };

                        //Check Unique Unit name
                        bool isUniqueUnitName = true;
                        foreach (Unit unit in AllUnitList)
                        {
                            if (unit.Name.ToLower() == insertUnit.Name.ToLower())
                            {
                                isUniqueUnitName = false;
                            }
                        }

                        if (isUniqueUnitName)
                        {
                            try
                            {
                                GlobalConfig.Connection.Unit_Insert(insertUnit, GlobalConfig.CnnString("ICMSdatabase"));

                                AllUnitList.Add(insertUnit);

                                SelectedUnit = AllUnitList.FirstOrDefault(s => s.Name == insertUnit.Name);
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
                                caption: "",
                                button: MessageBoxButton.OK,
                                icon: MessageBoxImage.Error
                                );
                        }
                    }
                }
                );
            #endregion

            #region UnitCancelButtonCommand
            UnitCancelButtonCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) => { CurrentOperationMode = OperationMode.NormalMode.ToString(); }
                );
            #endregion

            #region UnitDeleteButtonCommand
            UnitDeleteButtonCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (SelectedUnit == null | AllUnitList.Count() <= 1)
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
                            messageBoxText: $"Hãy tạm khóa đơn vị \"{SelectedUnit.Name}\" nếu không muốn sử dụng.\n\n Bạn có muốn xóa đơn vị này ?",
                            caption: "YES/NO",
                            button: MessageBoxButton.YesNo,
                            icon: MessageBoxImage.Warning,
                            defaultResult: MessageBoxResult.No
                            );

                        if (result == MessageBoxResult.Yes)
                        {
                            MessageBoxResult result2 = MessageBox.Show(
                            messageBoxText: "Bạn có chắc chắn muốn xóa ?",
                            caption: "YES/NO",
                            button: MessageBoxButton.YesNo,
                            icon: MessageBoxImage.Warning,
                            defaultResult: MessageBoxResult.No
                            );

                            if (result2 == MessageBoxResult.Yes)
                            {
                                try
                                {
                                    GlobalConfig.Connection.Unit_DeleteById(SelectedUnit.UnitId, GlobalConfig.CnnString("ICMSdatabase"));
                                    AllUnitList.Remove(SelectedUnit);
                                }
                                catch (Exception ex)
                                {
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
                }
                );
            #endregion

            #endregion
        }


        #endregion
    }
}
