
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
        private BaseViewModel _UserManagementControl;
        public BaseViewModel UserManagementControl { get => _UserManagementControl; set { _UserManagementControl = value; OnPropertyChanged(); } }

        private BaseViewModel _RoleManagementControl;
        public BaseViewModel RoleManagementControl { get => _RoleManagementControl; set { _RoleManagementControl = value; OnPropertyChanged(); } }

        #region Constructor
        public UserRoleManagementViewModel()
        {
            UserManagementControl = new UserManagementViewModel();

            RoleManagementControl = new RoleManagementViewModel();
        }


        #endregion

    }
}
