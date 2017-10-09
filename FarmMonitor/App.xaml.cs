using FarmMonitor.ViewModels;
using FarmMonitor.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FarmMonitor.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Run();

            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            // attempt to log in.
            var loginWindow = new LoginView(bootstrapper.Container);
            bool? logonResult = loginWindow.ShowDialog();

            if (logonResult.HasValue && logonResult.Value)
            {
                bootstrapper.Show();
            }

            ShutdownMode = ShutdownMode.OnMainWindowClose;
        }
    }
}
