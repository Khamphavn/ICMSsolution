using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ICMS.Command;
using ICMS.Model.DataAccess;
using ICMS.Model.Models;

namespace ICMS.ViewModel
{
    public class TMViewModel : BaseViewModel
    {
        private ObservableCollection<TM> _TMs;
        public ObservableCollection<TM> TMs { get => _TMs; set { _TMs = value; OnPropertyChanged(); } }


        private TM _SelectedTM;
        public TM SelectedTM
        {
            get => _SelectedTM;
            set
            {
                _SelectedTM = value;
                if (SelectedTM != null)
                {
                    TM_Name = SelectedTM.Name;
                }
                OnPropertyChanged();
            }
        }

        private string _TM_Name;
        public string TM_Name { get => _TM_Name; set { _TM_Name = value; OnPropertyChanged(); } }

        //mode operation
        private enum OperationMode { NormalMode, EditMode, AddMode };
        private string _TMCurrentOperationMode;
        public string TMCurrentOperationMode { get => _TMCurrentOperationMode; set { _TMCurrentOperationMode = value; OnPropertyChanged(); } }

        #region Command
        public ICommand TMEditButtonCommand { get; set; }
        public ICommand TMAddButtonCommand { get; set; }
        public ICommand TMCancelButtonCommand { get; set; }
        public ICommand TMDeleteButtonCommand { get; set; }
        #endregion


        public TMViewModel()
        {
            #region Init
            TMs = new ObservableCollection<TM>(GlobalConfig.Connection.TM_GetAll(GlobalConfig.CnnString("ICMSdatabase")));

            TMCurrentOperationMode = OperationMode.NormalMode.ToString();
            #endregion

            #region Commands

            #region TMAddButtonCommand
            TMAddButtonCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    return true;
                    
                },
                (p) =>
                {
                    if (TMCurrentOperationMode == OperationMode.NormalMode.ToString())
                    {
                        TM_Name = null;
                        SelectedTM = null;
                        TMCurrentOperationMode = OperationMode.AddMode.ToString();
                    }
                    else if (TMCurrentOperationMode == OperationMode.AddMode.ToString())
                    {
                        TM insertTM = new TM()
                        {
                            Name = TM_Name.Trim()
                        };

                        try
                        {
                            GlobalConfig.Connection.TM_Insert(insertTM, GlobalConfig.CnnString("ICMSdatabase"));
                            TMs = new ObservableCollection<TM>(GlobalConfig.Connection.TM_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
                            SelectedTM = (TM)TMs.FirstOrDefault(item => item.TMId == insertTM.TMId);
                            TMCurrentOperationMode = OperationMode.NormalMode.ToString();
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
                        TMCurrentOperationMode = OperationMode.NormalMode.ToString();
                    }
                }
                );
            #endregion

            #region TMEditButtonCommand
            TMEditButtonCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (SelectedTM != null) { return true; } else { return false; }
                },
                (p) =>
                {
                    if (TMCurrentOperationMode == OperationMode.NormalMode.ToString())
                    {
                        TMCurrentOperationMode = OperationMode.EditMode.ToString();
                    }
                    else if (TMCurrentOperationMode == OperationMode.EditMode.ToString())
                    {
                        TM updateTM = new TM()
                        {
                            TMId = SelectedTM.TMId,
                            Name = TM_Name.Trim()
                        };

                        int updateTMResult = GlobalConfig.Connection.TM_Update(updateTM, GlobalConfig.CnnString("ICMSdatabase"));

                        if (updateTMResult == 1)
                        {
                            TMs = new ObservableCollection<TM>(GlobalConfig.Connection.TM_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
                            SelectedTM = (TM)TMs.FirstOrDefault(item => item.TMId == updateTM.TMId);
                        }
                        else
                        {
                            MessageBox.Show(
                                messageBoxText: "Could not update Machine Name !",
                                caption: "Update Machine Name Failed",
                                button: MessageBoxButton.OK,
                                icon: MessageBoxImage.Error
                                );
                        }
                        TMCurrentOperationMode = OperationMode.NormalMode.ToString();
                    }
                    else
                    {
                        TMCurrentOperationMode = OperationMode.NormalMode.ToString();
                    }
                }
                );
            #endregion

            #region TMCancelButtonCommand
            TMCancelButtonCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) => { TMCurrentOperationMode = OperationMode.NormalMode.ToString(); }
                );
            #endregion

            #region TMDeleteButtonCommand
            TMDeleteButtonCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (SelectedTM == null | TMs.Count() <=1) 
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
                    if (TMCurrentOperationMode == OperationMode.NormalMode.ToString())
                    {
                        MessageBoxResult result = MessageBox.Show(
                            messageBoxText: "Bạn có chắc chắn muốn xóa ?",
                            caption: "YES/NO",
                            button: MessageBoxButton.YesNo,
                            icon : MessageBoxImage.Warning,
                            defaultResult: MessageBoxResult.No
                            );

                        if (result == MessageBoxResult.Yes)
                        {
                            try
                            {
                                GlobalConfig.Connection.TM_DeleteById(SelectedTM.TMId, GlobalConfig.CnnString("ICMSdatabase"));
                                TMs.Remove(SelectedTM);
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

            #endregion

        }
    }
}
