using ICMS.Command;
using ICMS.Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ICMS.ViewModel
{
    public class UserEditDialogViewModel : BaseViewModel
    {
        private User _updateUser;

        public User UpdateUser
        {
            get => _updateUser;
            set
            {
                _updateUser = value;

                if (UpdateUser != null)
                {
                    User_LoginName = UpdateUser.LoginName;
                    User_FullName = UpdateUser.FullName;
                    User_RoleName = UpdateUser.Role.Name;
                    User_IsActive = UpdateUser.IsActive;

                    if (RoleList.Any(p => p.Name == UpdateUser.Role.Name))
                    {
                        foreach (Role role in RoleList)
                        {
                            if (role.Name == UpdateUser.Role.Name)
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

        #region Commands
        public ICommand CancelButtonCommand { get; set; }
        #endregion

        public UserEditDialogViewModel(User user, ObservableCollection<Role> roleList)
        {
            RoleList = roleList;
            UpdateUser = user;

            #region Commands

        

          

            #endregion
        }
    }
}
