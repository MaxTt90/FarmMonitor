using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FarmMonitor.BLL;
using FarmMonitor.Desktop.Views;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using FarmMonitor.Desktop.ViewModels;
using PresentationModule.Services;

namespace FarmMonitor.Desktop
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.TryResolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (Window)Shell;
        }

        public void Show()
        {
            Application.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<ILoginService, LoginService>(new ContainerControlledLifetimeManager());
        }
    }
}
