using FarmMonitor.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Elekta.Desktop.GuiComponents.Controls;
using FarmMonitor.Desktop.Views;

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

#if !DEBUG
            // attempt to log in.
            var loginWindow = new LoginView(bootstrapper.Container);
            bool? loginResult = loginWindow.ShowDialog();

            if (loginResult.HasValue && loginResult.Value)
            {
                bootstrapper.Show();
            }
#else
            bootstrapper.Show();
#endif
            ShutdownMode = ShutdownMode.OnMainWindowClose;
        }
    }
}
