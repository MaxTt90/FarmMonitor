using FarmMonitor.BLL;
using MahApps.Metro.Controls;
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
using MahApps.Metro.Controls.Dialogs;

namespace FarmMonitor.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : MetroWindow
    {
        public Shell()
        {
            SysUserInfoMan uiMan = new SysUserInfoMan();
            var users = uiMan.GetAllUsers().ToList();

            InitializeComponent();
        }

        private async void ShowLoginDialogOnlyPassword(object sender, RoutedEventArgs e)
        {
            LoginDialogData result = await this.ShowLoginAsync("Authentication", "Enter your password", new LoginDialogSettings { ColorScheme = this.MetroDialogOptions.ColorScheme, ShouldHideUsername = true });
            if (result == null)
            {
                //User pressed cancel
            }
            else
            {
                MessageDialogResult messageResult = await this.ShowMessageAsync("Authentication Information", String.Format("Password: {0}", result.Password));
            }
        }
    }
}
