using DashboardModule.ViewModels;
using DashboardModule.Services;
using FarmMonitor.Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace DashboardModule
{
    public class DashboardModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public DashboardModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterType<IDashboardViewModel, DashboardViewModel>();
            _container.RegisterType<ISensorPlotModelHandler, SensorPlotModelHandler>();
            _container.RegisterType<IChartProvider, LineChartProvider>();
        }
    }
}
