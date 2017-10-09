using FarmMonitor.BLL;
using FarmMonitor.Model;
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
using Elekta.Desktop.GuiComponents.Controls;
using MahApps.Metro.Controls.Dialogs;
using PresentationModule.Services;

namespace FarmMonitor.Desktop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : ApplicationWindow
    {
        private ILoginService _loginService;

        public Shell()
        {
            InitializeComponent();
        }

        //private async void OnAccountClicked(object sender, RoutedEventArgs e)
        //{
        //    if(_loginService == null)
        //    {
        //        return;
        //    }

        //    // Do not have a user
        //    if (_loginService.CurrentUser == null)
        //    {
        //        LoginDialogData result = await this.ShowLoginAsync("Authentication", "Enter your credentials", new LoginDialogSettings { ColorScheme = MetroDialogOptions.ColorScheme, InitialUsername = "Xinhui", EnablePasswordPreview = true });
        //        if (result == null)
        //        {
        //            //User pressed cancel
        //        }
        //        else
        //        {
        //            if (_loginService != null)
        //            {
        //                var loginUser = _loginService.Login(result.Username, result.Password);
        //                if (loginUser != null)
        //                {
        //                    await this.ShowMessageAsync("Authentication Information", "Login successfully!", MessageDialogStyle.Affirmative);
        //                }
        //                else
        //                {
        //                    await this.ShowMessageAsync("Authentication Information", "Error: Invalid user name and password!", MessageDialogStyle.Affirmative);
        //                }
        //            }
        //        }
        //    }
        //    // Already has a user.
        //    else
        //    {
        //        var result = await this.ShowMessageAsync("Authentication Information", string.Format("{0} has already logged in. Do you want to log out?", _loginService.CurrentUser.Name), MessageDialogStyle.AffirmativeAndNegative);

        //        if(result == MessageDialogResult.Affirmative)
        //        {
        //            _loginService.Logout();
        //            await this.ShowMessageAsync("Authentication Information", "Logout successfully!", MessageDialogStyle.Affirmative);
        //        }
        //    }

        //}
    }
}
