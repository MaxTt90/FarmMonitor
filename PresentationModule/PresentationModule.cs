using Microsoft.Practices.Unity;
using PresentationModule.ViewModels;
using Prism.Modularity;
using Prism.Regions;

namespace PresentationModule
{
    public class PresentationModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public PresentationModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterType<IChartViewModel, ChartViewModel>();
        }
    }
}
