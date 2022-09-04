using ICMS.Model.DataAccess;
using ICMS.Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.ViewModel
{
    public class RolePermissionViewModel : BaseViewModel
    {
        private ObservableCollection<Role> _Roles;
        public ObservableCollection<Role> Roles { get => _Roles; set { _Roles = value; OnPropertyChanged(); } }


        private Role _SelectedRole;
        public Role SelectedRole { get => _SelectedRole; set 
            { 
                _SelectedRole = value; 
                
                if (SelectedRole != null)
                {
                    RoleActions = SelectedRole.RoleActions;
                }
                OnPropertyChanged(); 
            
            } }


        private List<RoleAction> _RoleActions;
        public List<RoleAction> RoleActions { get => _RoleActions; set { _RoleActions = value; OnPropertyChanged(); } }

        public RolePermissionViewModel()
        {
            #region Init
            Roles = new ObservableCollection<Role>(GlobalConfig.Connection.Role_GetAll(GlobalConfig.CnnString("ICMSdatabase")));

            SelectedRole = Roles[0];


            #endregion
        }
    }
}
