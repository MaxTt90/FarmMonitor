using FarmMonitor.Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeLineModule.ViewModels;
using TimeLineModule.Views;

namespace TimeLineModule
{
    public class TimeLineModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public TimeLineModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterType<ITimeLineViewModel, TimeLineViewModel>();
        }
    }
}
