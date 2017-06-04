using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmMonitor.Model;
using FarmMonitor.BLL;
using FarmMonitor.Infrastructure.Utilities;

namespace FarmMonitor.Services
{
    public class LoginService : ILoginService
    {
        public LoginService()
        {

        }

        public SysUserInfo CurrentUser { get; private set; }

        public SysUserInfo Login(string userName, string password)
        {
            var sysUserInfo = new SysUserInfoMan().GetModelByUsername(userName);
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
