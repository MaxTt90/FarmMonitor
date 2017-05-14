using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardModule.ViewModels
{
    public class DashboardViewModel : BindableBase, IDashboardViewModel
    {
        public DashboardViewModel()
        {
            Message = "Hello Dashboard.";
        }
         
        public string Message { get; set; }
    }
}
