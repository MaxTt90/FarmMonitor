using System.Collections.Generic;
using System.Linq;
using System.Web;
using FarmMonitor.BLL;
using FarmMonitor.Model;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace FarmMonitor.Web
{
    public partial class Startup
    {
        static string SessionKeyMemberInfo = "MemberInfo";
        static string SessionKeyUserInfo = "UserInfo";
        static string SessionKeyCurrentPlatform = "CurrentPlatformOpenId";
        static string SessionKeyFirstMenu = "first_menu_info";
        // 有关配置身份验证的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // 使应用程序可以使用 Cookie 来存储已登录用户的信息
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString("/Account/Login")
            //});
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // 取消注释以下行可允许使用第三方登录提供程序登录
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication();

        }
        /// <summary>
        /// 获得登录用户对象
        /// </summary>
        /// <returns>用户对象</returns>
        /// Author:fredjiang
        /// Created:2015-10-10
        public static SysUserInfo GetUserInfo()
        {
            try
            {
                if (HttpContext.Current.Session[SessionKeyUserInfo] != null)
                {
                    return HttpContext.Current.Session[SessionKeyUserInfo] as SysUserInfo;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获得当前登录用户的权限列表集合
        /// </summary>
        /// <param name="reset">是否重置</param>
        /// <returns>用户权限集合</returns>
        ///Author:fredjiang
        ///Created:2015-10-29
        public static List<SysRightInfo> GetUserRightInfoList(bool reset)
        {
            List<SysRightInfo> list = new List<SysRightInfo>();
            SysUserInfo ui = GetUserInfo();
            string key = "current_user_right_list";
            if (ui != null)
            {
                if (HttpContext.Current.Session[key] == null || reset)
                {
                    SysRightInfoMan siMan = new SysRightInfoMan();
                    list = siMan.GetListByUser(ui).ToList();
                    if (list.Count > 0)
                    {
                        HttpContext.Current.Session[key] = list;
                    }
                }
                else
                {
                    list = HttpContext.Current.Session[key] as List<SysRightInfo>;
                }
            }
            return list;
        }

        /// <summary>
        /// 判断用户是否有操作权限
        /// </summary>
        /// <param name="rightInfoId">权限编号</param>
        /// <returns>可操作返回true,不可以操作返回false</returns>
        /// Author:fredjiang
        /// Created:2015-12-16
        public static bool IsOperateRight(int rightInfoId)
        {
            if (GetUserRightInfoList(false).Find(o => o.Id == rightInfoId) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 保存登录用户对象
        /// </summary>
        /// <param name="ui">登录用户对象</param>        
        public static void SaveUserInfo(SysUserInfo ui)
        {
            try
            {
                HttpContext.Current.Session[SessionKeyUserInfo] = ui;
            }
            catch
            {

            }
        }

        /// <summary>
        /// 清空登录用户对象
        /// </summary>      
        public static void RemoveUserInfo()
        {
            try
            {
                HttpContext.Current.Session.Clear();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 保存登录会员对象
        /// </summary>
        /// <param name="ui">登录会员对象</param>

        public static void SaveMemberInfo(Member member)
        {
            try
            {
                HttpContext.Current.Session[SessionKeyMemberInfo] = member;
            }
            catch
            {

            }
        }

        public static int GetCurrentUserId()
        {
            var model = GetUserInfo();
            if (model == null)
            {
                return 0;
            }
            return model.ID;
        }

        /// <summary>
        /// 设置或获取当前微信公众账号编号
        /// </summary>
        /// Author:fredjiang
        /// Created:2015-12-04
        public static string CurrentPlatformOpenId
        {
            set { HttpContext.Current.Session[SessionKeyCurrentPlatform] = value; }
            get
            {
                if (HttpContext.Current.Session[SessionKeyCurrentPlatform] != null)
                {
                    return HttpContext.Current.Session[SessionKeyCurrentPlatform].ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 设置或获取当前会话的选中的第一级菜单
        /// </summary>
        public static SysRightInfo CurrentSelectMenu
        {
            set { HttpContext.Current.Session[SessionKeyFirstMenu] = value; }
            get { return (SysRightInfo)HttpContext.Current.Session[SessionKeyFirstMenu]; }
        }
    }
}