using DocumentFormat.OpenXml.Drawing.Diagrams;
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
    /// Interaction logic for UC_UserEdit_Dialog.xaml
    /// </summary>
    public partial class UC_UserEdit_Dialog : UserControl
    {
        public UC_UserEdit_Dialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool updateSuccess = false;

            var vm = (UserEditDialogViewModel)this.DataContext;

            // Update User to Database.
            User newUser = new User()
            {
                UserId = vm.UpdateUser.UserId,
                LoginName = vm.User_LoginName.Trim().ToLower(),
                FullName = vm.User_FullName.Trim(),
                
                Role = vm.RoleList.FirstOrDefault(s => s.Name == vm.User_RoleName.Trim()),
                IsActive = vm.User_IsActive
            };

            try
            {
                GlobalConfig.Connection.User_Update_Infos(newUser, GlobalConfig.CnnString("ICMSdatabase"));
                updateSuccess = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(messageBoxText: "Thay đổi thông tin không thành công\n\n" + ex.Message,
                                caption: "Error",
                                button: MessageBoxButton.OK,
                                icon: MessageBoxImage.Error,
                                defaultResult: MessageBoxResult.OK
                                        );
                updateSuccess = false;
            }


            // Update password

            if (!string.IsNullOrWhiteSpace(NewPasswordBox1.Password) | !string.IsNullOrWhiteSpace(NewPasswordBox2.Password))
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

                    updateSuccess = false;
                }
                else
                {
                    string hashNewPasswordInput = PasswordHelper.GenerateSHA512String(NewPasswordBox2.Password.ToLower());

                    newUser.Password = hashNewPasswordInput;

                    try
                    {
                        GlobalConfig.Connection.User_Update_Password(newUser, GlobalConfig.CnnString("ICMSdatabase"));                  
                        updateSuccess = true;
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
                        updateSuccess = false;
                    }

                }
            }

            if (updateSuccess)
            {
                MessageBox.Show(messageBoxText: "Thay đổi thông tin tài khoản thành công",
                                caption: "",
                                button: MessageBoxButton.OK,
                                icon: MessageBoxImage.Information,
                                defaultResult: MessageBoxResult.OK
                                );

                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

    }
}
