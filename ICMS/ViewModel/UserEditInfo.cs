using ICMS.Command;
using ICMS.Model.DataAccess;
using ICMS.Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ICMS.ViewModel
{
    public class UserEditInfo : BaseViewModel
    {
        private User _CurrentUser;
        public User CurrentUser { get => _CurrentUser; 
            set 
            { 
                _CurrentUser = value; 
                
                if (CurrentUser != null)
                {
                    LoginName = CurrentUser.LoginName;
                    FullName = CurrentUser.FullName;
                }
                
                
                OnPropertyChanged(); 
            } 
        }


        private string _LoginName;
        public string LoginName { get => _LoginName; set { _LoginName = value; OnPropertyChanged(); } }

        private string _FullName;
        public string FullName { get => _FullName; set { _FullName = value; OnPropertyChanged(); } }



        #region Commands
        public ICommand SaveUserInfoCommand { get; set; }

        #endregion



        public UserEditInfo(MainViewModel mainViewModel)
        {
            CurrentUser = mainViewModel.CurrentUser;


            #region SaveUserInfoCommand
            SaveUserInfoCommand = new RelayCommand<object>(
                (p) =>
                {
                    return true;
                },
                (p) =>
                {
                    try
                    {
                        CurrentUser.LoginName = LoginName;
                        CurrentUser.FullName = FullName;
                        
                        GlobalConfig.Connection.User_Update_Infos(CurrentUser, GlobalConfig.CnnString("ICMSdatabase"));


                        mainViewModel.CurrentUser = CurrentUser;


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                                    messageBoxText: $"{ex.Message}\n\n{ex.StackTrace}",
                                    caption: "Error",
                                    button: MessageBoxButton.OK,
                                    icon: MessageBoxImage.Error
                                    );
                    }
                }
                );
            #endregion
        }


    }
}
