using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardModule.ViewModels
{
    public interface IDashboardViewModel
    {
        string Message { get; set; }

        ISatalliteMapViewModel SatalliteMapViewModel { get; }
    }
}
