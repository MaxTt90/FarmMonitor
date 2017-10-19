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
using DashboardModule.ViewModels;
using MapModule.Views;
using TimeLineModule.ViewModels;
using TimeLineModule.Views;
using MonitorsModule.Views;

namespace NavigationModule
{
    public class NavigationModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

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
            _container.RegisterType<object, MapView>(typeof (MapView).FullName);
            _container.RegisterType<object, MonitorsView>(typeof (MonitorsView).FullName);
            _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, typeof(NavigationView));
        }
    }
}
