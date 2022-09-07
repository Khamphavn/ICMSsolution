using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using ICMS.Command;
using ICMS.Model.DataAccess;
using ICMS.Model.Models;

namespace ICMS.ViewModel
{
    public class MachineViewModel : BaseViewModel
    {
        private User _CurrentUser;
        public User CurrentUser { get => _CurrentUser; private set { _CurrentUser = value; OnPropertyChanged(); } }

        #region Machine Name
        private ObservableCollection<MachineName> _MachineNames;
        public ObservableCollection<MachineName> MachineNames { get => _MachineNames; set { _MachineNames = value; OnPropertyChanged(); }}

        private string _MachineName_Name;
        public string MachineName_Name { get => _MachineName_Name; set { _MachineName_Name = value; OnPropertyChanged(); } }

        private MachineName _SelectedMachineName;
        public MachineName SelectedMachineName
        {
            get => _SelectedMachineName;
            set
            {
                _SelectedMachineName = value;
                if (SelectedMachineName != null)
                {
                    MachineName_Name = SelectedMachineName.Name;
                }
                OnPropertyChanged();
            }
        }

        //mode operation
        private string _MachineNameCurrentOperationMode;
        public string MachineNameCurrentOperationMode { get => _MachineNameCurrentOperationMode;  set { _MachineNameCurrentOperationMode = value; OnPropertyChanged(); }}
        private enum OperationMode { NormalMode, EditMode, AddMode };

        public ICommand MachineNameEditButtonCommand { get; set; }
        public ICommand MachineNameAddButtonCommand { get; set; }
        public ICommand MachineNameDeleteButtonCommand { get; set; }
        public ICommand MachineNameCancelButtonCommand { get; set; }
        //public ICommand MachineClearFieldButtonCommand { get; set; }
        #endregion

        #region Machine Type
        private ObservableCollection<MachineType> _MachineTypes;
        public ObservableCollection<MachineType> MachineTypes { get => _MachineTypes; set { _MachineTypes = value; OnPropertyChanged(); } }

        private string _MachineType_Name;
        public string MachineType_Name { get => _MachineType_Name; set { _MachineType_Name = value; OnPropertyChanged(); } }

        private bool _MachineType_IsActive;
        public bool MachineType_IsActive { get => _MachineType_IsActive; set { _MachineType_IsActive = value; OnPropertyChanged(); } }

        private MachineType _SelectedMachineType;
        public MachineType SelectedMachineType
        {
            get => _SelectedMachineType;
            set
            {
                _SelectedMachineType = value;
                if (SelectedMachineType != null)
                {
                    MachineType_Name = SelectedMachineType.Name;
                    MachineType_IsActive = SelectedMachineType.IsActive;
                }
                OnPropertyChanged();
            }
        }

        //mode operation
        private string _MachineTypeCurrentOperationMode;
        public string MachineTypeCurrentOperationMode { get => _MachineTypeCurrentOperationMode; set { _MachineTypeCurrentOperationMode = value; OnPropertyChanged(); } }

        public ICommand MachineTypeEditButtonCommand { get; set; }
        public ICommand MachineTypeAddButtonCommand { get; set; }
        public ICommand MachineTypeCancelButtonCommand { get; set; }
        public ICommand MachineTypeDeleteButtonCommand { get; set; }
        public ICommand MachineTypeClearFieldButtonCommand { get; set; }

        #endregion

        #region Sensor Type
        private ObservableCollection<SensorType> _SensorTypes;
        public ObservableCollection<SensorType> SensorTypes { get => _SensorTypes; set { _SensorTypes = value; OnPropertyChanged(); } }

        private string _SensorType_Name;
        public string SensorType_Name { get => _SensorType_Name; set { _SensorType_Name = value; OnPropertyChanged(); } }

        private bool _SensorType_IsActive;
        public bool SensorType_IsActive { get => _SensorType_IsActive; set { _SensorType_IsActive = value; OnPropertyChanged(); } }

        private SensorType _SelectedSensorType;
        public SensorType SelectedSensorType
        {
            get => _SelectedSensorType;
            set
            {
                _SelectedSensorType = value;
                if (SelectedSensorType != null)
                {
                    SensorType_Name = SelectedSensorType.Name;
                    SensorType_IsActive = SelectedSensorType.IsActive;
                }
                OnPropertyChanged();
            }
        }

        //mode operation
        private string _SensorTypeCurrentOperationMode;
        public string SensorTypeCurrentOperationMode { get => _SensorTypeCurrentOperationMode; set { _SensorTypeCurrentOperationMode = value; OnPropertyChanged(); } }

        public ICommand SensorTypeEditButtonCommand { get; set; }
        public ICommand SensorTypeAddButtonCommand { get; set; }
        public ICommand SensorTypeDeleteButtonCommand { get; set; }
        public ICommand SensorTypeCancelButtonCommand { get; set; }
        public ICommand SensorTypeClearFieldButtonCommand { get; set; }

        #endregion


        public MachineViewModel(User currentUser)
        {
            #region Init
            CurrentUser = currentUser;

            MachineNames = new ObservableCollection<MachineName>(GlobalConfig.Connection.MachineName_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
            MachineTypes = new ObservableCollection<MachineType>(GlobalConfig.Connection.MachineType_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
            SensorTypes = new ObservableCollection<SensorType>(GlobalConfig.Connection.SensorType_GetAll(GlobalConfig.CnnString("ICMSdatabase")));

            MachineNameCurrentOperationMode = OperationMode.NormalMode.ToString();
            MachineTypeCurrentOperationMode = OperationMode.NormalMode.ToString();
            SensorTypeCurrentOperationMode = OperationMode.NormalMode.ToString();
            #endregion



            #region Commands

            #region MachineNameAddButtonCommand
            MachineNameAddButtonCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    return true; 
                },
                (p) =>
                {
                    if (MachineNameCurrentOperationMode == OperationMode.NormalMode.ToString())
                    {
                        MachineName_Name = null;
                        SelectedMachineName = null;
                        MachineNameCurrentOperationMode = OperationMode.AddMode.ToString();
                    }
                    else if (MachineNameCurrentOperationMode == OperationMode.AddMode.ToString())
                    {
                        MachineName insertMachineName= new MachineName()
                        {
                            Name = MachineName_Name.Trim()
                        };

                        // check unique MachineName.Name 
                        bool isUniqueMachineName = IsUniqueMachineName(insertMachineName.Name, MachineNames.ToList());

                        if (isUniqueMachineName)
                        {
                            try
                            {
                                GlobalConfig.Connection.MachineName_Insert(insertMachineName, GlobalConfig.CnnString("ICMSdatabase"));
                                //TMs = new ObservableCollection<TM>(GlobalConfig.Connection.TM_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
                                MachineNames.Add(insertMachineName);
                                SelectedMachineName = (MachineName)MachineNames.FirstOrDefault(item => item.MachineNameId == insertMachineName.MachineNameId);
                                MachineNameCurrentOperationMode = OperationMode.NormalMode.ToString();
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
                        else
                        {
                            MessageBox.Show(
                               messageBoxText: $"Tên thiết bị '{MachineName_Name}' đã có !",
                               caption: "Error",
                               button: MessageBoxButton.OK,
                               icon: MessageBoxImage.Error
                               );
                        }


                    }
                    else
                    {
                        MachineNameCurrentOperationMode = OperationMode.NormalMode.ToString();
                    }
                }
                );
            #endregion

            #region MachineNameEditButtonCommand
            MachineNameEditButtonCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (SelectedMachineName != null)  { return true; }  else  { return false; }
                },
                (p) =>
                {
                    if (MachineNameCurrentOperationMode == OperationMode.NormalMode.ToString())
                    {
                        MachineNameCurrentOperationMode = OperationMode.EditMode.ToString();
                    }
                    else if (MachineNameCurrentOperationMode == OperationMode.EditMode.ToString())
                    {
                        MachineName updateMachineName = new MachineName()
                        {
                            MachineNameId = SelectedMachineName.MachineNameId,
                            Name = MachineName_Name.Trim()
                        };

                        // check unique MachineName.Name 
                        List<MachineName> tempoMachineNameList = MachineNames.Where(s => s.MachineNameId != updateMachineName.MachineNameId).ToList();

                        bool isUniqueMachineName = IsUniqueMachineName(updateMachineName.Name, tempoMachineNameList.ToList());

                        if (isUniqueMachineName)
                        {
                            try
                            {
                                GlobalConfig.Connection.MachineName_Update(updateMachineName, GlobalConfig.CnnString("ICMSdatabase"));
                                MachineNames = new ObservableCollection<MachineName>(GlobalConfig.Connection.MachineName_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
                                SelectedMachineName = (MachineName)MachineNames.FirstOrDefault(item => item.MachineNameId == updateMachineName.MachineNameId);
                                MachineNameCurrentOperationMode = OperationMode.NormalMode.ToString();
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
                        else
                        {
                            MessageBox.Show(
                               messageBoxText: $"Tên thiết bị '{MachineName_Name}' đã có !",
                               caption: "Error",
                               button: MessageBoxButton.OK,
                               icon: MessageBoxImage.Error
                               );
                        }

                    }
                    else
                    {
                        MachineNameCurrentOperationMode = OperationMode.NormalMode.ToString();
                    }
                }
                );
            #endregion

            #region MachineNameCancelButtonCommand
            MachineNameCancelButtonCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) => { MachineNameCurrentOperationMode = OperationMode.NormalMode.ToString(); }
                );
            #endregion

            #region MachineNameDeleteButtonCommand
            MachineNameDeleteButtonCommand = new RelayCommand<object>
                (
                (p) => 
                {
                    if (SelectedMachineName == null | MachineNames.Count() <= 1)
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
                    if (MachineNameCurrentOperationMode == OperationMode.NormalMode.ToString())
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
                                GlobalConfig.Connection.MachineName_DeleteById(SelectedMachineName.MachineNameId, GlobalConfig.CnnString("ICMSdatabase"));
                                MachineNames.Remove(SelectedMachineName);
                                //TMs = new ObservableCollection<TM>(GlobalConfig.Connection.TM_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
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
                );
            #endregion



            #region MachineTypeAddButtonCommand
            MachineTypeAddButtonCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    return true; 
                },
                (p) =>
                {
                    if (MachineTypeCurrentOperationMode == OperationMode.NormalMode.ToString())
                    {
                        MachineType_Name = null;
                        SelectedMachineType = null;
                        MachineTypeCurrentOperationMode = OperationMode.AddMode.ToString();
                    }
                    else if (MachineTypeCurrentOperationMode == OperationMode.AddMode.ToString())
                    {
                        MachineType insertMachineType = new MachineType()
                        {
                            Name = Regex.Replace(MachineType_Name.Trim(), @"\s+", " "),  
                            IsActive = MachineType_IsActive
                        };

                        // check unique MachineType.Name 
                        bool isUniqueMachineTypeName = IsUniqueMachineTypeName(insertMachineType.Name, MachineTypes.ToList());

                        if (isUniqueMachineTypeName)
                        {
                            try
                            {
                                GlobalConfig.Connection.MachineType_Insert(insertMachineType, GlobalConfig.CnnString("ICMSdatabase"));
                                MachineTypes.Add(insertMachineType);
                                SelectedMachineType = (MachineType)MachineTypes.FirstOrDefault(item => item.MachineTypeId == insertMachineType.MachineTypeId);
                                MachineTypeCurrentOperationMode = OperationMode.NormalMode.ToString();
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
                        else
                        {
                            MessageBox.Show(
                               messageBoxText: $"Loại thiết bị '{insertMachineType.Name}' đã có !",
                               caption: "Error",
                               button: MessageBoxButton.OK,
                               icon: MessageBoxImage.Error
                               );
                        }
                    }
                    else
                    {
                        MachineTypeCurrentOperationMode = OperationMode.NormalMode.ToString();
                    }
                }
                );
            #endregion

            #region MachineTypeEditButtonCommand
            MachineTypeEditButtonCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (SelectedMachineType != null) { return true; } else { return false; }
                },
                (p) =>
                {
                    if (MachineTypeCurrentOperationMode == OperationMode.NormalMode.ToString())
                    {
                        MachineTypeCurrentOperationMode = OperationMode.EditMode.ToString();
                    }
                    else if (MachineTypeCurrentOperationMode == OperationMode.EditMode.ToString())
                    {
                        MachineType updateMachineType = new MachineType()
                        {
                            MachineTypeId = SelectedMachineType.MachineTypeId,
                            Name = MachineType_Name.Trim(),
                            IsActive = MachineType_IsActive
                        };
                        // check unique MachineName.Name 
                        List<MachineType> tempoMachineTypeList = MachineTypes.Where(s => s.MachineTypeId != updateMachineType.MachineTypeId).ToList();

                        bool isUniqueMachineType = IsUniqueMachineTypeName(updateMachineType.Name, tempoMachineTypeList.ToList());

                        if (isUniqueMachineType)
                        {
                            try
                            {
                                GlobalConfig.Connection.MachineType_Update(updateMachineType, GlobalConfig.CnnString("ICMSdatabase"));
                                MachineTypes = new ObservableCollection<MachineType>(GlobalConfig.Connection.MachineType_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
                                SelectedMachineType = (MachineType)MachineTypes.FirstOrDefault(item => item.MachineTypeId == updateMachineType.MachineTypeId);
                                MachineTypeCurrentOperationMode = OperationMode.NormalMode.ToString();
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
                        else
                        {
                            MessageBox.Show(
                               messageBoxText: "Loại thiết bị đã tồn tại !",
                               caption: "Error",
                               button: MessageBoxButton.OK,
                               icon: MessageBoxImage.Error
                               );
                        }
                    }
                    else
                    {
                        MachineTypeCurrentOperationMode = OperationMode.NormalMode.ToString();
                    }
                }
                );
            #endregion

            #region MachineTypeDeleteButtonCommand
            MachineTypeDeleteButtonCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (SelectedMachineType == null | MachineTypes.Count() <= 1)
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
                    if (MachineTypeCurrentOperationMode == OperationMode.NormalMode.ToString())
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
                                GlobalConfig.Connection.MachineType_DeleteById(SelectedMachineType.MachineTypeId, GlobalConfig.CnnString("ICMSdatabase"));
                                MachineTypes.Remove(SelectedMachineType);
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

            #region MachineTypeCancelButtonCommand
            MachineTypeCancelButtonCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) => { MachineTypeCurrentOperationMode = OperationMode.NormalMode.ToString(); }
                );
            #endregion




            #region SensorTypeAddButtonCommand
            SensorTypeAddButtonCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    return true;
                },
                (p) =>
                {
                    if (SensorTypeCurrentOperationMode == OperationMode.NormalMode.ToString())
                    {
                        SensorType_Name = null;
                        SelectedSensorType = null;
                        SensorTypeCurrentOperationMode = OperationMode.AddMode.ToString();
                    }
                    else if (SensorTypeCurrentOperationMode == OperationMode.AddMode.ToString())
                    {
                        SensorType insertSensorType = new SensorType()
                        {
                            Name = Regex.Replace(SensorType_Name.Trim(), @"\s+", " "),
                            IsActive = SensorType_IsActive
                        };

                        // check unique SensorType.Name 
                        bool isUniqueSensorTypeName = IsUniqueSensorTypeName(insertSensorType.Name, SensorTypes.ToList());

                        if (isUniqueSensorTypeName)
                        {
                            try
                            {
                                GlobalConfig.Connection.SensorType_Insert(insertSensorType, GlobalConfig.CnnString("ICMSdatabase"));
                                SensorTypes.Add(insertSensorType);
                                SelectedSensorType = (SensorType)SensorTypes.FirstOrDefault(item => item.Name == insertSensorType.Name);
                                SensorTypeCurrentOperationMode = OperationMode.NormalMode.ToString();
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
                        else
                        {
                            MessageBox.Show(
                               messageBoxText: $"Loại đầu đo '{insertSensorType.Name}' đã có !",
                               caption: "Error",
                               button: MessageBoxButton.OK,
                               icon: MessageBoxImage.Error
                               );
                        }
                    }
                    else
                    {
                        SensorTypeCurrentOperationMode = OperationMode.NormalMode.ToString();
                    }
                }
                );
            #endregion

            #region SensorTypeEditButtonCommand
            SensorTypeEditButtonCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (SelectedSensorType != null) { return true; } else { return false; }
                },
                (p) =>
                {
                    if (SensorTypeCurrentOperationMode == OperationMode.NormalMode.ToString())
                    {
                        SensorTypeCurrentOperationMode = OperationMode.EditMode.ToString();
                    }
                    else if (SensorTypeCurrentOperationMode == OperationMode.EditMode.ToString())
                    {
                        SensorType updateSensorType = new SensorType()
                        {
                            SensorTypeId = SelectedSensorType.SensorTypeId,
                            Name = SensorType_Name.Trim(),
                            IsActive = SensorType_IsActive
                        };

                        // check unique SensorType.Name 
                        List<SensorType> tempoSensorTypeList = SensorTypes.Where(s => s.SensorTypeId != updateSensorType.SensorTypeId).ToList();

                        bool isUniqueSensorType = IsUniqueSensorTypeName(updateSensorType.Name, tempoSensorTypeList.ToList());

                        if (isUniqueSensorType)
                        {
                            try
                            {
                                GlobalConfig.Connection.SensorType_Update(updateSensorType, GlobalConfig.CnnString("ICMSdatabase"));
                                SensorTypes = new ObservableCollection<SensorType>(GlobalConfig.Connection.SensorType_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
                                SelectedSensorType = (SensorType)SensorTypes.FirstOrDefault(item => item.SensorTypeId == updateSensorType.SensorTypeId);
                                SensorTypeCurrentOperationMode = OperationMode.NormalMode.ToString();
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
                        else
                        {
                            MessageBox.Show(
                               messageBoxText: $"Loại đầu đo '{updateSensorType.Name}' đã có !",
                               caption: "Error",
                               button: MessageBoxButton.OK,
                               icon: MessageBoxImage.Error
                               );
                        }
                    }
                    else
                    {
                        SensorTypeCurrentOperationMode = OperationMode.NormalMode.ToString();
                    }
                }
                );
            #endregion

            #region SensorTypeDeleteButtonCommand
            SensorTypeDeleteButtonCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (SelectedSensorType == null | SensorTypes.Count() <= 1)
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
                    if (SensorTypeCurrentOperationMode == OperationMode.NormalMode.ToString())
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
                                GlobalConfig.Connection.SensorType_DeleteById(SelectedSensorType.SensorTypeId, GlobalConfig.CnnString("ICMSdatabase"));
                                SensorTypes.Remove(SelectedSensorType);
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
                );
            #endregion

            #region SensorTypeCancelButtonCommand
            SensorTypeCancelButtonCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) => { SensorTypeCurrentOperationMode = OperationMode.NormalMode.ToString(); }
                );
            #endregion

            #endregion
        }

        #region private function
        private bool IsUniqueMachineName(string machineName_Name, List<MachineName> MachineNames)
        {
            MachineName machine  = MachineNames.FirstOrDefault(s => s.Name.ToLower() == machineName_Name.ToLower());
            return machine != null ? false : true;
        }

        private bool IsUniqueMachineTypeName(string machineType_Name, List<MachineType> MachineNames)
        {
            MachineType machineTypeName = MachineTypes.FirstOrDefault(s => s.Name.ToLower() == machineType_Name.ToLower());
            return machineTypeName != null ? false : true;
        }

        private bool IsUniqueSensorTypeName(string sensorType_Name, List<SensorType> MachineNames)
        {
            SensorType sensorTypeName = SensorTypes.FirstOrDefault(s => s.Name.ToLower() == sensorType_Name.ToLower());
            return sensorTypeName != null ? false : true;
        }


        #endregion
    }
}
