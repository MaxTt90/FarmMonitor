using FarmMonitor.Infrastructure;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationModule.ViewModels
{
    public class NavigationViewModel : INavigationViewModel
    {
        private readonly IRegionManager _regionManager;

        public NavigationViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            DashboardNavigateCommand = new DelegateCommand<object>(Navigate);
            MapNavigateCommand = new DelegateCommand<object>(Navigate);
            TimeLineNavigateCommand = new DelegateCommand<object>(Navigate);
            MonitorsDelegateCommand = new DelegateCommand<object>(Navigate);

            ApplicationCommands.NavigatieCommand.RegisterCommand(DashboardNavigateCommand);
        }

        public DelegateCommand<object> DashboardNavigateCommand { get; private set; }

        public DelegateCommand<object> TimeLineNavigateCommand { get; private set; }

        public DelegateCommand<object> MapNavigateCommand { get; private set; }

        public DelegateCommand<object> MonitorsDelegateCommand { get; private set; }

        private void Navigate(object navigatePath)
        {
            if( navigatePath != null )
            {
                _regionManager.RequestNavigate(RegionNames.WorkspaceRegion, navigatePath.ToString());
            }
        }
    }
}
