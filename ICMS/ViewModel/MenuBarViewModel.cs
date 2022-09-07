using System.IO;
using System.Windows;
using System.Windows.Input;
using ICMS.Command;
using ICMS.Model.Models;
using ICMS.View;
using ICMS.View.UC_Dialog;

namespace ICMS.ViewModel
{
    public class MenuBarViewModel : BaseViewModel
    {
        public User CurrentUser;


        private bool _UserRoleViewPermission;
        public bool UserRoleViewPermission { get => _UserRoleViewPermission; set { _UserRoleViewPermission = value; OnPropertyChanged(); } }



        private bool _MachineViewPermission;
        public bool MachineViewPermission { get => _MachineViewPermission; set { _MachineViewPermission = value; OnPropertyChanged(); } }

        #region System
        public ICommand NavigateUserConfigurationCommand { get; set; }
        public ICommand NavigateDatabaseBackupCommand { get; set; }
        public ICommand NavigateDatabaseRestoreCommand { get; set; }
        public ICommand NavigateAppShutdownCommand { get; set; }
        #endregion

        #region Configuration
        public ICommand NavigateRadQuantityCommand { get; set; }
        public ICommand NavigateDoseQuantityCommand { get; set; }
        public ICommand NavigateUnitCommand { get; set; }
        public ICommand NavigateTMCommand { get; set; }
        #endregion

        #region List
        public ICommand NavigateCertificateListCommand { get; set; }
        public ICommand NavigateCustomerCommand { get; set; }
        public ICommand NavigateCityCommand { get; set; }
        public ICommand NavigateMachineCommand { get; set; }
        #endregion

        #region Help
        public ICommand OpenUserManualCommand { get; set; }
        public ICommand AboutMeCommand { get; set; }
        #endregion

        #region Shortcut button
        public ICommand NavigateCertificateFormCommand { get; set; }
        #endregion

        #region Command User infos
        public ICommand NavigateUserInfosCommand { get; set; }
        public ICommand NavigateUserLogoutCommand { get; set; }
        #endregion


        private bool _IsDialogOpen;
        public bool IsDialogOpen { get => _IsDialogOpen; set { _IsDialogOpen = value; OnPropertyChanged(); } }

        private object _DialogContent;
        public object DialogContent { get => _DialogContent; set { _DialogContent = value; OnPropertyChanged(); } }

        public MenuBarViewModel(MainViewModel mainViewModel)
        {
            CurrentUser = mainViewModel.CurrentUser;

            UserRoleViewPermission = CurrentUser.Role.RoleActions[0].View | CurrentUser.Role.RoleActions[1].View;
            MachineViewPermission = CurrentUser.Role.RoleActions[11].View | CurrentUser.Role.RoleActions[12].View | CurrentUser.Role.RoleActions[13].View;



            #region NavigateUserConfigurationCommand
            NavigateUserConfigurationCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) =>
                {
                    mainViewModel.CurrentControl = new UserRoleManagementViewModel(CurrentUser);
                }
                );
            #endregion

            #region NavigateDatabaseBackupCommand
            NavigateDatabaseBackupCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) =>
                {
                    mainViewModel.CurrentControl = new DatabaseBackupViewModel();
                }
                );
            #endregion

            #region NavigateDatabaseRestoreCommand
            NavigateDatabaseRestoreCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) =>
                {
                    mainViewModel.CurrentControl = new DatabaseRestoreViewModel();
                }
                );
            #endregion

            #region NavigateAppShutdownCommand
            NavigateAppShutdownCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) =>
                {
                    Application.Current.Shutdown();
                }
                );
            #endregion



            #region NavigateRadQuantityCommand
            NavigateRadQuantityCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) => { mainViewModel.CurrentControl = new RadQuantityViewModel(CurrentUser); }
                );
            #endregion

            #region NavigateDoseQuantityCommand
            NavigateDoseQuantityCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) => { mainViewModel.CurrentControl = new DoseQuantityViewModel(CurrentUser); }
                );
            #endregion

            #region NavigateUnitCommand
            NavigateUnitCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) => { mainViewModel.CurrentControl = new UnitViewModel(CurrentUser); }
                );
            #endregion

            #region NavigateTMCommand
            NavigateTMCommand = new RelayCommand<object>
               (
                (p) => { return true; },
                (p) => { mainViewModel.CurrentControl = new TMViewModel(CurrentUser); }
                );
            #endregion



            #region NavigateCertificateListCommand
            NavigateCertificateListCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) =>
                {
                    mainViewModel.CurrentControl = new CertificateListViewModel(mainViewModel);
                }
                );
            #endregion

            #region NavigateCustomerCommand
            NavigateCustomerCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) => { mainViewModel.CurrentControl = new CustomerViewModel(CurrentUser); }
                );
            #endregion

            #region NavigateCityCommand
            NavigateCityCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) => { mainViewModel.CurrentControl = new CityViewModel(CurrentUser); }
                );
            #endregion

            #region NavigateMachineCommand
            NavigateMachineCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) => { mainViewModel.CurrentControl = new MachineViewModel(CurrentUser); }
                );
            #endregion



            #region OpenUserManualCommand
            OpenUserManualCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) =>
                {
                    try
                    {
                        string UserManualFile = Path.Combine(Directory.GetCurrentDirectory(), "Docs", "UserManual.pdf");
                        System.Diagnostics.Process.Start(UserManualFile);
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show(
                            messageBoxText:"Could not open file", 
                            caption:"Error", 
                            button:MessageBoxButton.OK, 
                            icon:MessageBoxImage.Information,
                            defaultResult:MessageBoxResult.OK
                            );
                    }
                    
                }
                );
            #endregion

            #region AboutMeCommand
            AboutMeCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) =>
                {
                    DialogContent = new UC_ICMS_infos_Dialog()
                    {
                       
                    };
                    IsDialogOpen = true;
                }
                );
            #endregion


            #region NavigateCertificateFormCommand
            NavigateCertificateFormCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) =>
                {
                    mainViewModel.CurrentControl = new CertificateFormViewModel(mainViewModel,null, "AddMode", CurrentUser);
                }
                );
            #endregion



            #region NavigateUserInfosCommand
            NavigateUserInfosCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) =>
                {
                    mainViewModel.CurrentControl = new UserEditInfo(mainViewModel);
                }
                );
            #endregion

            #region NavigateUserLogoutCommand
            NavigateUserLogoutCommand = new RelayCommand<object>
                (
                (p) => { return true; },
                (p) =>
                {
                   


                    LoginFormWindow loginWindow = new LoginFormWindow()
                    {
                        DataContext = new LoginFormViewModel()
                    };

                    loginWindow.Show();

                    if (p is System.Windows.Window)
                    {
                        (p as System.Windows.Window).Close();
                    }


                }
                );
            #endregion
        }


    }
}
