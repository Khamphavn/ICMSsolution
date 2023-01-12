using ICMS.HelperFunction;
using ICMS.Model.DataAccess;
using ICMS.Model.Models;
using ICMS.ViewModel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ICMS.View.UC_Dialog
{
    /// <summary>
    /// Interaction logic for UC_UserAddNew_Dialog.xaml
    /// </summary>
    public partial class UC_UserAddNew_Dialog : UserControl
    {
        public UC_UserAddNew_Dialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var vm = (UserAddNewDialogViewModel)this.DataContext;

            // new User to Database.
            User newUser = new User()
            {
                LoginName = vm.User_LoginName.Trim().ToLower(),
                FullName = vm.User_FullName.Trim(),

                Role = vm.RoleList.FirstOrDefault(s => s.Name == vm.User_RoleName.Trim()),
                IsActive = vm.User_IsActive
            };

            if (string.IsNullOrWhiteSpace(NewPasswordBox1.Password) | string.IsNullOrWhiteSpace(NewPasswordBox2.Password))
            {
                MessageBox.Show(
                        messageBoxText: "Chưa có mật khẩu cho tài khoản mới",
                        caption: "Error",
                        button: MessageBoxButton.OK,
                        icon: MessageBoxImage.Information,
                        defaultResult: MessageBoxResult.OK
                        );
            }
            else
            {
                if (NewPasswordBox1.Password.ToLower() != NewPasswordBox2.Password.ToLower())
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
                    string hashNewPasswordInput = PasswordHelper.GenerateSHA512String(NewPasswordBox2.Password.ToLower());

                    newUser.Password = hashNewPasswordInput;

                    try
                    {
                        GlobalConfig.Connection.User_Insert(newUser, GlobalConfig.CnnString("ICMSdatabase"));
                        MessageBox.Show(messageBoxText: "Tạo tài khoản mới thành công",
                                caption: "",
                                button: MessageBoxButton.OK,
                                icon: MessageBoxImage.Information,
                                defaultResult: MessageBoxResult.OK
                                );

                        DialogHost.CloseDialogCommand.Execute(null, null);
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
        }
    }
}

