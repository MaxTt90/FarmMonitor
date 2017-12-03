using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FarmMonitor.Model;
using FarmMonitor.Web.AppCode.Attributes;
using Microsoft.Ajax.Utilities;

namespace FarmMonitor.Web.AppCode.Filter
{
    /// <summary>
    /// 后台用户权限证验过淲器类
    /// </summary>
    /// createTime 2015-11-16 zb copy fredjiang   
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class UserInfoAuthFilter : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            if (Startup.GetUserInfo() != null)
            {
                List<SysRightInfo> list = Startup.GetUserRightInfoList(false);
                var areas = filterContext.RouteData.DataTokens["area"];
                string filePath = "";
                if (areas == ""||areas==null)
                {
                    filePath = "~/" + filterContext.RouteData.GetRequiredString("controller") + "/" +
                               filterContext.RouteData.GetRequiredString("action");
                }
                else
                {
                    filePath = "~/"+areas+"/" + filterContext.RouteData.GetRequiredString("controller") + "/" + filterContext.RouteData.GetRequiredString("action");
                }
                if (list.Find(o => o.FilePath.ToUpper() == filePath.ToUpper()) == null)
                {
                    CallContext.SetData("access_result", "401");//无访问权限
                }
                else
                {
                    CallContext.SetData("access_result", "200");//允许访问
                    #region 设置当前选中第一级菜单
                    if (filterContext.RequestContext.HttpContext.Request.QueryString["mid"] != null)
                    {
                        int id = 0;
                        if (int.TryParse(filterContext.RequestContext.HttpContext.Request.QueryString["mid"], out id))
                        {
                            SysRightInfo menu = list.Find(o => o.Id == id);
                            if (menu != null)
                            {
                                Startup.CurrentSelectMenu = menu;
                            }
                        }
                    }
                    #endregion
                }
            }
            else
            {
                CallContext.SetData("access_result", "402"); //未登录
            }
            base.OnAuthorization(filterContext);

        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            //return base.AuthorizeCore(httpContext);
            if (CallContext.GetData("access_result").ToString() == "200")
            {
                return true;
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var accessResult = CallContext.GetData("access_result").ToString();
            if (accessResult == "401")
            {
                //无访问权限,转到无权限提示页面
                filterContext.Result = new RedirectResult("/login/noright");
            }
            else if (accessResult == "402")
            {
                ////未登录提示，转到登录页面
                //filterContext.Result = new RedirectResult("/login/login");

                var attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(IframePageAttribute), false);
                if (attrs.Length > 0)
                {
                    filterContext.Result = AjaxResult("未登录", "/login/login");
                }
                else
                {
                    var syncAttrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(SyncPageAttribute), false);
                    if (syncAttrs.Length > 0)
                    {
                        filterContext.Result = new JsonResult() { Data = new { code = -1010, msg = "未登录", url = "/login/login" } };
                    }
                    else
                    {
                        //未登录提示，转到登录页面
                        filterContext.Result = new RedirectResult("/login/login");
                    }
                }
            }
            //base.HandleUnauthorizedRequest(filterContext);
        }

        public ContentResult AjaxResult(string strMsg, string url = "")
        {
            System.Text.StringBuilder sbJs = new System.Text.StringBuilder("<script>alert(\"").Append(strMsg).Append("\");");
            if (!url.IsNullOrWhiteSpace())
            {
                sbJs.Append("if(window.top!=window)window.top.location=\"").Append(url).Append("\";");
                sbJs.Append("else window.location=\"").Append(url).Append("\";");
            }
            sbJs.Append("</script>");

            return new ContentResult()
            {
                Content = sbJs.ToString()
            };
        }
    }
}