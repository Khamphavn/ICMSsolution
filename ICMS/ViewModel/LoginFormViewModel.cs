using ICMS.Model.DataAccess;
using System.Data.SqlClient;


namespace ICMS.ViewModel
{
    public class LoginFormViewModel : BaseViewModel
    {
        private string _LoginName;
        public string LoginName { get => _LoginName; set { _LoginName = value; OnPropertyChanged(); } }

        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }


        private bool _IsAllCheckPass;
        public bool IsAllCheckPass { get => _IsAllCheckPass; set { _IsAllCheckPass = value; OnPropertyChanged(); } }


        private bool _IsFinishChecking;
        public bool IsFinishChecking { get => _IsFinishChecking; set { _IsFinishChecking = value; OnPropertyChanged(); } }



        private string _CheckMessageText;
        public string CheckMessageText { get => _CheckMessageText; set { _CheckMessageText = value; OnPropertyChanged(); } }

        //public ICommand LoginCommand { get; }
        public LoginFormViewModel()
        {
            IsAllCheckPass = true;
            IsFinishChecking = true;

        }


        #region private check

        private bool CanConnectDatabase()
        {
            using (SqlConnection connection = new SqlConnection(GlobalConfig.CnnString("ICMSdatabase")))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
                finally
                {
                    // not really necessary
                    connection.Close();
                }
            }
        }

        #endregion

    }
}
