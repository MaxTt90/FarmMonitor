using System.Collections.Generic;
using FarmMonitor.Web.AppCode;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FarmMonitor.Web.Startup))]
namespace FarmMonitor.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

    }
}
