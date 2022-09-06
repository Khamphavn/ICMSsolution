using ICMS.Command;
using ICMS.Model.DataAccess;
using ICMS.Model.Models;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Role = ICMS.Model.Models.Role;

namespace ICMS.ViewModel
{
    public class RoleManagementViewModel : BaseViewModel
    {
        private ObservableCollection<Role> _Roles;
        public ObservableCollection<Role> Roles { get => _Roles; set { _Roles = value; OnPropertyChanged(); } }


        private RoleAction _SelectedRoleAction;
        public RoleAction SelectedRoleAction { get => _SelectedRoleAction; set { _SelectedRoleAction = value; OnPropertyChanged(); } }


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


        #region Commands
        public ICommand SaveRolePermissionCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand RestoreDefaultRolePermissionCommand { get; set; }


        #endregion



        public RoleManagementViewModel()
        {
            #region Init
            Roles = new ObservableCollection<Role>(GlobalConfig.Connection.Role_GetAll(GlobalConfig.CnnString("ICMSdatabase")));

            Roles.Remove(Roles.Where(s => s.Name.ToLower() == "admin").Single());



            SelectedRole = Roles[1];


            #endregion


            #region Commands

            #region SaveRolePermissionCommand
            SaveRolePermissionCommand = new RelayCommand<object>
                (
                (p) => 
                {
                    foreach (Role role in Roles)
                    {
                        foreach (RoleAction roleAction in role.RoleActions)
                        {
                            if (roleAction.View == false)
                            {
                                if (roleAction.Add == true | roleAction.Edit == true | roleAction.Delete == true | roleAction.Print == true)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    return true; 
                },
                (p) =>
                {
                    Roles.Count();
                    foreach (Role role in Roles)
                    {
                        GlobalConfig.Connection.Role_Update(role, GlobalConfig.CnnString("ICMSdatabase"));
                    }
                    
        }
                );
            #endregion



            #region CancelCommand
            CancelCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) =>
                {
                    Roles = new ObservableCollection<Role>(GlobalConfig.Connection.Role_GetAll(GlobalConfig.CnnString("ICMSdatabase")));

                    SelectedRole = Roles[0];
                }
                );
            #endregion


            #endregion


        }




        #region private function

        //private void GetDefaultRolePermission()
        //{
        //    Role viewer = Roles.FirstOrDefault(s => s.Name == "Viewer");
            
        //    List<RoleAction> viewerRoleActions = new List<RoleAction>() 
        //    { 
        //        new RoleAction()
        //        {
        //            ActionCode = "User",

        //        }
        //    }



        //}


        #endregion
    }
}
