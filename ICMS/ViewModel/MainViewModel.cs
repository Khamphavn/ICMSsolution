using ICMS.Model.Models;

namespace ICMS.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private MenuBarViewModel _MenuBarViewModel;
        public MenuBarViewModel MenubarViewModel { get => _MenuBarViewModel; set { _MenuBarViewModel = value; OnPropertyChanged();}
        }

        private BaseViewModel _CurrentControl;
        public BaseViewModel CurrentControl { get => _CurrentControl; set { _CurrentControl = value; OnPropertyChanged(); }}

        private Certificate _CurrentCertificate;
        public Certificate CurrentCertificate { get => _CurrentCertificate; set { _CurrentCertificate = value; OnPropertyChanged(); } }

        private User _CurrentUser;
        public User CurrentUser { get => _CurrentUser; set { _CurrentUser = value; OnPropertyChanged(); } }

        public MainViewModel(User currentUser)
        {
            CurrentUser = currentUser;
            MenubarViewModel = new MenuBarViewModel(this);



            //CurrentControl = new CertificateFormViewModel(this, null, "AddMode");
            //CurrentControl = new CertificateListViewModel(this);
            //CurrentControl = new CustomerViewModel();
            //CurrentControl = new CityViewModel();
            //CurrentControl = new MachineViewModel();
            //CurrentControl = new UnitViewModel();
            //CurrentControl = new TMViewModel();
            //CurrentControl = new RadQuantityViewModel();
            //CurrentControl = new DoseQuantityViewModel();
            //CurrentControl = new UserViewModel();
            //CurrentControl = new DatabaseBackupViewModel();
            CurrentControl = new DatabaseRestoreViewModel();
        }


        #region private method


        #endregion
    }
}
