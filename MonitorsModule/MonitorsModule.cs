using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace MonitorsModule
{
    public class MonitorsModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public MonitorsModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
        }
    }
}
