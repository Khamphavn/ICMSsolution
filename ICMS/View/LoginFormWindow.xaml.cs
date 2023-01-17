using ICMS.Model.DataAccess;
using ICMS.Model.Models;
using ICMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ICMS.View
{
    /// <summary>
    /// Interaction logic for LoginFormWindow.xaml
    /// </summary>
    public partial class LoginFormWindow : Window
    {
        public LoginFormWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            string hashPasswordInput = GenerateSHA512String(PasswordBox.Password.ToLower());


            User user = GetAuthenticatedUser(LoginNameTxtBox.Text, hashPasswordInput);

            if (user != null)
            {
                User currentUser = user;

                MainWindow mainWindow = new MainWindow()
                {
                    DataContext = new MainViewModel(currentUser)
                };
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(
                       messageBoxText: "Đăng nhập không thành công",
                       caption: "Error",
                       button: MessageBoxButton.OK,
                       icon: MessageBoxImage.Information,
                       defaultResult: MessageBoxResult.OK
                       );
            }

        }

        private string GenerateSHA512String(string inputString)
        {
            byte[] data = Encoding.UTF8.GetBytes(inputString);
            using (SHA512 shaM = new SHA512Managed())
            {
                byte[] result = shaM.ComputeHash(data);
                return Convert.ToBase64String(result);
            }
        }

        private User GetAuthenticatedUser(string loginName, string hashPasswordInput)
        {
            User user = GlobalConfig.Connection.User_GetAuthenticatedUser(loginName, hashPasswordInput, GlobalConfig.CnnString("ICMSdatabase"));

            return user;
        }
    }
}
