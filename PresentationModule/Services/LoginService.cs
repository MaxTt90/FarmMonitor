using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmMonitor.Model;
using FarmMonitor.BLL;
using FarmMonitor.BLL.Interfaces;
using FarmMonitor.Infrastructure.Utilities;

namespace PresentationModule.Services
{
    public class LoginService : ILoginService
    {
        public readonly ISysUserInfoMan _sysUserInfoMan;

        public LoginService(ISysUserInfoMan sysUserInfoMan)
        {
            _sysUserInfoMan = sysUserInfoMan;
        }

        public SysUserInfo CurrentUser { get; private set; }

        public SysUserInfo Login(string userName, string password)
        {
            var sysUserInfo = _sysUserInfoMan.GetModelByUserName(userName);
            if(sysUserInfo != null && sysUserInfo.Password == EncryptUtilities.EncryptMD5(password))
            {
                CurrentUser = sysUserInfo;
                return sysUserInfo;
            }

            return null;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
