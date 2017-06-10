using FarmMonitor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationModule.Services
{
    public interface ILoginService
    {
        SysUserInfo CurrentUser{ get; }

        SysUserInfo Login(string userName, string password);

        void Logout();
    }
}
