
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ICMS.Command;
using ICMS.Model.DataAccess;
using ICMS.Model.Models;

namespace ICMS.ViewModel
{
    public class UserRoleManagementViewModel : BaseViewModel
    {
        private readonly User CurrentUser;



        private BaseViewModel _UserManagementControl;
        public BaseViewModel UserManagementControl { get => _UserManagementControl; set { _UserManagementControl = value; OnPropertyChanged(); } }

        private BaseViewModel _RoleManagementControl;
        public BaseViewModel RoleManagementControl { get => _RoleManagementControl; set { _RoleManagementControl = value; OnPropertyChanged(); } }

        #region Constructor
        public UserRoleManagementViewModel(User currentUser)
        {
            CurrentUser = currentUser;

            UserManagementControl = new UserManagementViewModel(currentUser);

            RoleManagementControl = new RoleManagementViewModel(currentUser);
            CurrentUser = currentUser;
        }


        #endregion

    }
}
