using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardModule.ViewModels
{
    public class DashboardViewModel : IDashboardViewModel
    {
        public DashboardViewModel()
        {
        }

        public string Message { get; set; }
    }
}
