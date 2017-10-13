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

        private async void OnAccountClicked(object sender, RoutedEventArgs e)
        {
            if (_loginService == null)
            {
                return;
            }

            // Do not have a user
            if (_loginService.CurrentUser == null)
            {
                

            }
            // Already has a user.
            else
            {
            }

        }
    }
}
