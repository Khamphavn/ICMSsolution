using ICMS.Model.DataAccess;
using ICMS.Model.Models;
using ICMS.ViewModel;
using Syncfusion.Windows.Shared;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ICMS.View.UC_Component
{
    /// <summary>
    /// Interaction logic for UC_UserEditInfo.xaml
    /// </summary>
    public partial class UC_UserEditInfo : UserControl
    {
        public UC_UserEditInfo()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var vm = (UserEditInfo)this.DataContext;


            string hashCurrentPasswordInput = GenerateSHA512String(CurrentPasswordBox.Password.ToLower());


            bool isCurrentPasswordCorrect = GetAuthenticatedUser(vm.CurrentUser.LoginName, hashCurrentPasswordInput) != null;


            if (isCurrentPasswordCorrect)
            {
                if (string.IsNullOrWhiteSpace(NewPasswordBox1.Password) | string.IsNullOrWhiteSpace(NewPasswordBox2.Password))
                {
                    MessageBox.Show(
                        messageBoxText: "Cần nhập mật khẩu mới 2 lần",
                        caption: "Error",
                        button: MessageBoxButton.OK,
                        icon: MessageBoxImage.Information,
                        defaultResult: MessageBoxResult.OK
                        );
                }
                else if (NewPasswordBox1.Password.ToLower() != NewPasswordBox2.Password.ToLower())
                {
                    MessageBox.Show(
                        messageBoxText: "Mật khẩu mới khác nhau",
                        caption: "Error",
                        button: MessageBoxButton.OK,
                        icon: MessageBoxImage.Information,
                        defaultResult: MessageBoxResult.OK
                        );
                }
                else
                {
                    string hashNewPasswordInput = GenerateSHA512String(NewPasswordBox2.Password.ToLower());

                    vm.CurrentUser.Password = hashNewPasswordInput;

                    try
                    {
                        GlobalConfig.Connection.User_Update_Password(vm.CurrentUser, GlobalConfig.CnnString("ICMSdatabase"));

                        MessageBox.Show(
                                                messageBoxText: "Thay đổi mật khẩu thành công",
                                                caption: "",
                                                button: MessageBoxButton.OK,
                                                icon: MessageBoxImage.Information,
                                                defaultResult: MessageBoxResult.OK
                                                );

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                                                messageBoxText: "Thay đổi mật khẩu không thành công\n\n" + ex.Message,
                                                caption: "Error",
                                                button: MessageBoxButton.OK,
                                                icon: MessageBoxImage.Error,
                                                defaultResult: MessageBoxResult.OK
                                                );
                    }

                }
            }
            else
            {
                MessageBox.Show(
                       messageBoxText: "Mật khẩu hiện tại không đúng",
                       caption: "Error",
                       button: MessageBoxButton.OK,
                       icon: MessageBoxImage.Information,
                       defaultResult: MessageBoxResult.OK
                       );
            }

            int i = 1;

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
