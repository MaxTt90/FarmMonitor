using DashboardModule.Views;
using FarmMonitor.Infrastructure;
using Microsoft.Practices.Unity;
using NavigationModule.ViewModels;
using NavigationModule.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeLineModule.Views;

namespace NavigationModule
{
    public class NavigationModule : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;

        public NavigationModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<INavigationViewModel, NavigationViewModel>();
            _container.RegisterType<object, DashboardView>(typeof(DashboardView).FullName);
            _container.RegisterType<object, TimeLineView>(typeof(TimeLineView).FullName);
            _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, typeof(NavigationView));
        }
    }
}
