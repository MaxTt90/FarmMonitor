using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FarmMonitor.Model;

namespace FarmMonitor.BLL.Interfaces
{
    public interface ISysUserInfoMan
    {
        SysUserInfo Add(SysUserInfo sysUserInfo);
        void Update(SysUserInfo sysUserInfo);
        void Delete(SysUserInfo sysUserInfo);
        void Delete(int id);
        SysUserInfo GetEntity(int id);

        IQueryable<SysUserInfo> GetAllValidUsers();
        IQueryable<SysUserInfo> GetAllUsers();

        List<SysUserInfo> GetUsersByRole(List<int> roleIds);

        SysUserInfo GetModelByUserName(string name);

        List<SysUserInfo> GetModelByPage(int pageIndex, out int sum, int pageSize);

        //For WeChat
        SysUserInfo GetUserByOpenId(string openId);

        int GetNewUserId();
    }
}
