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
    public  class UserManagementViewModel : BaseViewModel
    {
        private ObservableCollection<User> _ActiveUserList;
        public ObservableCollection<User> ActiveUserList { get => _ActiveUserList; set { _ActiveUserList = value; OnPropertyChanged(); } }

        private ObservableCollection<User> _AllUserList;
        public ObservableCollection<User> AllUserList
        {
            get => _AllUserList; set
            {
                _AllUserList = value;
                if (AllUserList != null)
                {

                }
                OnPropertyChanged();
            }
        }


        #region  Field Properties for Add and Adit
        private ObservableCollection<Role> _RoleList;
        private string _User_LoginName;
        private string _User_FullName;
        private string _User_Password;
        private string _User_RoleName;
        private bool _User_IsActive;

        public string User_LoginName { get => _User_LoginName; set { _User_LoginName = value; OnPropertyChanged(); } }
        public string User_FullName { get => _User_FullName; set { _User_FullName = value; OnPropertyChanged(); } }
        public string User_Password { get => _User_Password; set { _User_Password = value; OnPropertyChanged(); } }
        public string User_RoleName { get => _User_RoleName; set { _User_RoleName = value; OnPropertyChanged(); } }
        public bool User_IsActive { get => _User_IsActive; set { _User_IsActive = value; OnPropertyChanged(); } }
        public ObservableCollection<Role> RoleList { get => _RoleList; set { _RoleList = value; OnPropertyChanged(); } }
        #endregion

        private User _SelectedUser;
        public User SelectedUser
        {
            get => _SelectedUser;
            set
            {
                _SelectedUser = value;

                if (SelectedUser != null)
                {
                    User_LoginName = SelectedUser.LoginName;
                    User_FullName = SelectedUser.FullName;
                    User_RoleName = SelectedUser.Role.Name;
                    User_IsActive = SelectedUser.IsActive;

                    if (RoleList.Any(p => p.Name == SelectedUser.Role.Name))
                    {
                        foreach (Role role in RoleList)
                        {
                            if (role.Name == SelectedUser.Role.Name)
                            {
                                SelectedRole = role;
                            }
                        }
                    }
                    else
                    {
                        SelectedRole = null;
                    }
                }
                OnPropertyChanged();
            }
        }

        private Role _SelectedRole;
        public Role SelectedRole
        {
            get => _SelectedRole;
            set
            {
                _SelectedRole = value;

                if (SelectedRole != null)
                {
                    User_RoleName = SelectedRole.Name;
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

        private enum OperationMode { NormalMode, EditMode, AddMode };


        #region Commands
        public ICommand UserEditButtonCommand { get; set; }
        public ICommand UserAddButtonCommand { get; set; }
        public ICommand UserCancelButtonCommand { get; set; }
        #endregion

        #region Constructor
        public UserManagementViewModel()
        {
            AllUserList = new ObservableCollection<User>(GlobalConfig.Connection.User_GetAll(GlobalConfig.CnnString("ICMSdatabase")));

            RoleList = new ObservableCollection<Role>(GlobalConfig.Connection.Role_GetAll(GlobalConfig.CnnString("ICMSdatabase")));

            SelectedUser = null;
            SelectedRole = null;

            CurrentOperationMode = OperationMode.NormalMode.ToString();

            #region Commands

            #region UserEditButtonCommand
            UserEditButtonCommand = new RelayCommand<object>
                (
                (p) =>
                {
                    if (SelectedUser != null)
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
                        // Update User to Database.
                        User updateUser = new User()
                        {
                            UserId = SelectedUser.UserId,
                            LoginName = User_LoginName.Trim(),
                            FullName = User_FullName.Trim(),
                            Password = User_Password.Trim(),
                            Role = new Role() { Name = User_RoleName.Trim() },
                            IsActive = User_IsActive
                        };

                        int updateUserResult = GlobalConfig.Connection.User_Update_Infos(updateUser, GlobalConfig.CnnString("ICMSdatabase"));

                        if (updateUserResult == 1)
                        {
                            AllUserList = new ObservableCollection<User>(GlobalConfig.Connection.User_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
                        }
                        else
                        {
                            MessageBox.Show(
                                messageBoxText: "Could not update User !",
                                caption: "Update User Failed",
                                button: MessageBoxButton.OK,
                                icon: MessageBoxImage.Error
                                );
                        }
                        CurrentOperationMode = OperationMode.NormalMode.ToString();
                    }
                    else
                    {
                        CurrentOperationMode = OperationMode.NormalMode.ToString();
                    }

                }
                );
            #endregion

            #region UserAddButtonCommand
            UserAddButtonCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) =>
                {
                    if (CurrentOperationMode == OperationMode.NormalMode.ToString())
                    {
                        CurrentOperationMode = OperationMode.AddMode.ToString();
                        User_LoginName = "";
                        User_FullName = "";
                        User_RoleName = "";
                        User_IsActive = false;
                        User_Password = "";

                        SelectedRole = null;
                        SelectedUser = null;
                    }
                    else
                    {
                        CurrentOperationMode = OperationMode.NormalMode.ToString();
                    }

                }
                );
            #endregion

            #region UserCancelButtonCommand
            UserCancelButtonCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) => { CurrentOperationMode = OperationMode.NormalMode.ToString(); }
                );
            #endregion

            #endregion
        }


        #endregion
    }
}
