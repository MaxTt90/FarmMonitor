using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;

namespace FarmMonitor.Infrastructure
{
    public class AppContext
    {
        private static IUnityContainer _container;

        private static AppContext _instance;

        private AppContext()
        {
        }

        public static AppContext Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppContext();
                }

                return _instance;
            }
        }

        public IUnityContainer Container
        {
            get
            {
                return _container;
            }
        }

        public void Initialize(IUnityContainer container)
        {
            Guard.ArgumentNotNull(container, "container");

            // Keeps a copy of Unity Container
            _container = container;
        }
    }
}
