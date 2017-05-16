using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace DashboardModule.ViewModels
{
    public class DashboardViewModel : BindableBase, IDashboardViewModel
    {
        public DashboardViewModel(IUnityContainer container)
        {
            Message = "Hello Dashboard.";
            SatalliteMapViewModel = container.Resolve<ISatalliteMapViewModel>();
        }
         
        public string Message { get; set; }

        public ISatalliteMapViewModel SatalliteMapViewModel { get; private set; }
    }
}
