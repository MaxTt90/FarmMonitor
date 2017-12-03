using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeDo.Log;              
using FarmMonitor.Model;
using FarmMonitor.Web.AppCode;
namespace FarmMonitor.Web.Controllers
{
    public class TrackingController : Controller
    {
        private string json = "{{\"code\":\"{0}\",\"msg\":\"{1}\",\"guid\":\"{2}\",\"id\":{3}}}";
        //
        // GET: /Tracking/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(string url = "", int sourceId = 0, string mobile = "", string modulName = "")
        {
            string reJson = "";
            string logName = "页面浏览统计";
            try
            {
                #region 获得参数信息

                if (sourceId > 0)
                {
                    PageTools.CookieSourceId = sourceId.ToString();
                }
                else
                {
                    int.TryParse(PageTools.CookieSourceId, out sourceId);
                }
                //if (!string.IsNullOrEmpty(mobile))
                //{
                //    PageTools.CookieMobile = mobile;
                //}
                //else
                //{
                //    mobile = PageTools.CookieMobile;
                //}

                //var openid = PageTools.CookieOpenId;


                string guid = Guid.NewGuid().ToString("N");
                string uniqueUser = PageTools.CookieUniqueUser;
                if (string.IsNullOrEmpty(uniqueUser))
                {
                    uniqueUser = Guid.NewGuid().ToString("N");
                    PageTools.CookieUniqueUser = uniqueUser;
                }

                #endregion

                #region 添加日志

                FarmMonitor.BLL.LogTrackMan ltMan = new FarmMonitor.BLL.LogTrackMan();
                //LogTrack message = new LogTrack();
                ltMan.AddLog("volvo_ebook_view", sourceId, 0, 0, uniqueUser, modulName, 0, 0, 0, "", "", guid, Session.SessionID);
                LogTrack lt = ltMan.GetEntity(guid);
                reJson = string.Format(json, "1", "success", guid, lt.Id);

                #endregion
            }
            catch (Exception ex)
            {
                LogExceptionMan.AddLog(logName, WeDo.Log.Model.EnumListLog.LogLevel.ERROR, ex);
                reJson = string.Format(json, "2", "error", "", 0);
            }

            return Content(reJson);
        }

        public ActionResult Refresh(string guid, int id)
        {

            string logName = "刷新页面停留时间";
            string reJson = "";
            try
            {
                #region 获得参数信息

                int sourceId = 0;
                int.TryParse(PageTools.CookieSourceId, out sourceId);
                string mobile = PageTools.CookieMobile;
                LogTrack lt = new LogTrack();
                FarmMonitor.BLL.LogTrackMan ltMan = new BLL.LogTrackMan();
                if (id > 0)
                {
                    lt = ltMan.GetEntity(id);
                }
                else
                {
                    lt = ltMan.GetEntity(guid);
                }

                #endregion

                #region 更新数据

                if (lt != null)
                {
                    lt.RefreshTime = DateTime.Now;
                    ltMan.Update(lt);
                    reJson = string.Format(json, "1", "success", "", lt.Id);
                }
                else
                {
                    reJson = string.Format(json, "2", "fail", "", 0);
                    LogRunMan.AddLog(logName, WeDo.Log.Model.EnumListLog.LogLevel.INFO, null, "刷新对象为空");
                }

                #endregion
            }
            catch (Exception ex)
            {
                LogExceptionMan.AddLog(logName, WeDo.Log.Model.EnumListLog.LogLevel.ERROR, ex);
                reJson = string.Format(json, "2", "error", "", 0);
            }

            return Content(reJson);
        }

        public ActionResult Click(string url = "", string modulName = "")
        {
            string reJson = "";
            string logName = "点击操作统计";
            try
            {
                #region 获得参数信息

                int sourceId = 0;
                int.TryParse(PageTools.CookieSourceId, out sourceId);
                string mobile = PageTools.CookieMobile;
                string guid = Guid.NewGuid().ToString("N");
                string uniqueUser = PageTools.CookieUniqueUser;
                if (string.IsNullOrEmpty(uniqueUser))
                {
                    uniqueUser = Guid.NewGuid().ToString("N");
                    PageTools.CookieUniqueUser = uniqueUser;
                }
                //var openid = PageTools.CookieOpenId;

                #endregion

                #region 添加日志

                FarmMonitor.BLL.LogTrackMan ltMan = new FarmMonitor.BLL.LogTrackMan();
                ltMan.AddLog("volvo_ebook_click", sourceId, 0, 0, uniqueUser, modulName, 0, 0, 0, "", "", guid, Session.SessionID);

                reJson = string.Format(json, "1", "success", "", 0);

                #endregion
            }
            catch (Exception ex)
            {
                LogExceptionMan.AddLog(logName, WeDo.Log.Model.EnumListLog.LogLevel.ERROR, ex);
                reJson = string.Format(json, "2", "error", "", 0);
            }

            return Content(reJson);
        }
        public ActionResult ShareTrack(string url = "", string modulName = "")
        {
            string reJson = "";
            string logName = "分享统计";
            try
            {
                #region 获得参数信息

                int sourceId = 0;
                int.TryParse(PageTools.CookieSourceId, out sourceId);
                string mobile = PageTools.CookieMobile;
                string guid = Guid.NewGuid().ToString("N");
                string uniqueUser = PageTools.CookieUniqueUser;
                if (string.IsNullOrEmpty(uniqueUser))
                {
                    uniqueUser = Guid.NewGuid().ToString("N");
                    PageTools.CookieUniqueUser = uniqueUser;
                }

                #endregion

                #region 添加日志

                FarmMonitor.BLL.LogTrackMan ltMan = new FarmMonitor.BLL.LogTrackMan();
                ltMan.AddLog("volvo_ebook_share", sourceId, 0, 0, uniqueUser, modulName, 0, 0, 0, "", "", guid, Session.SessionID);


                //WeDo.Log.LogTrackMan.AddLog(message, WeDo.Log.Model.EnumListLog.LogLevel.INFO);
                reJson = string.Format(json, "1", "success", "", 0);

                #endregion
            }
            catch (Exception ex)
            {
                LogExceptionMan.AddLog(logName, WeDo.Log.Model.EnumListLog.LogLevel.ERROR, ex);
                reJson = string.Format(json, "2", "error", "", 0);
            }

            return Content(reJson);
        }

        #region log4net方法添加访问

        public ActionResult Add0(string url = "", int sourceId = 0, string mobile = "", string modulName = "")
        {
            string reJson = "";
            string logName = "";
            try
            {
                #region 获得参数信息

                if (sourceId > 0)
                {
                    PageTools.CookieSourceId = sourceId.ToString();
                }
                else
                {
                    int.TryParse(PageTools.CookieSourceId, out sourceId);
                }
                if (!string.IsNullOrEmpty(mobile))
                {
                    PageTools.CookieMobile = mobile;
                }
                else
                {
                    mobile = PageTools.CookieMobile;
                }
                string guid = Guid.NewGuid().ToString("N");
                string uniqueUser = PageTools.CookieUniqueUser;
                if (string.IsNullOrEmpty(uniqueUser))
                {
                    uniqueUser = Guid.NewGuid().ToString("N");
                    PageTools.CookieUniqueUser = uniqueUser;
                }

                #endregion

                #region 添加日志

                WeDo.Log.Model.LogTrackMessage message = new WeDo.Log.Model.LogTrackMessage();
                message.SourceId = sourceId;
                message.BeforeUrl = "";
                message.BrowserName = "";
                message.CookiesSign = uniqueUser;
                message.DataId = 0;
                message.DataType = 0;
                message.DeviceName = "";
                message.GUID = guid;
                message.HttpMethod = "";
                message.IP = Request.UserHostAddress;
                message.MemberId = 0;
                message.ModuleName = modulName;
                message.OpenId = mobile;
                message.OSName = "";
                message.PagePath = url;
                message.PageUrl = Request.Url.ToString();
                message.Remark = "";
                message.SearchKey = "";
                message.SearchName = "";
                message.SessionId = Session.SessionID;
                message.ShareSignId = 0;
                message.SourceId = sourceId;
                message.SystemSign = "volvo_ebook_view";
                message.UserId = 0;


                //WeDo.Log.LogTrackMan.AddLog("volvo_ebook", sourceId, 0, 0, uniqueUser, modulName, 0, 0, 0, "", mobile, guid);
                WeDo.Log.LogTrackMan.AddLog(message, WeDo.Log.Model.EnumListLog.LogLevel.INFO);
                FarmMonitor.BLL.LogTrackMan ltMan = new BLL.LogTrackMan();
                LogTrack lt = ltMan.GetEntity(guid);
                reJson = string.Format(json, "1", "success", guid, lt.Id);

                #endregion
            }
            catch (Exception ex)
            {
                LogExceptionMan.AddLog(logName, WeDo.Log.Model.EnumListLog.LogLevel.ERROR, ex);
                reJson = string.Format(json, "2", "error", "", 0);
            }

            return Content(reJson);
        }

        public ActionResult Refresh0(string guid, int id)
        {

            string logName = "刷新页面停留时间";
            string reJson = "";
            try
            {
                #region 获得参数信息

                int sourceId = 0;
                int.TryParse(PageTools.CookieSourceId, out sourceId);
                string mobile = PageTools.CookieMobile;
                LogTrack lt = new LogTrack();
                FarmMonitor.BLL.LogTrackMan ltMan = new BLL.LogTrackMan();
                if (id > 0)
                {
                    lt = ltMan.GetEntity(id);
                }
                else
                {
                    lt = ltMan.GetEntity(guid);
                }

                #endregion

                #region 更新数据

                if (lt != null)
                {
                    lt.RefreshTime = DateTime.Now;
                    ltMan.Update(lt);
                    reJson = string.Format(json, "1", "success", "", lt.Id);
                }
                else
                {
                    reJson = string.Format(json, "2", "fail", "", 0);
                    LogRunMan.AddLog(logName, WeDo.Log.Model.EnumListLog.LogLevel.INFO, null, "刷新对象为空");
                }

                #endregion
            }
            catch (Exception ex)
            {
                LogExceptionMan.AddLog(logName, WeDo.Log.Model.EnumListLog.LogLevel.ERROR, ex);
                reJson = string.Format(json, "2", "error", "", 0);
            }

            return Content(reJson);
        }


        public ActionResult Click0(string url = "", string modulName = "")
        {
            string reJson = "";
            string logName = "点击操作统计";
            try
            {
                #region 获得参数信息

                int sourceId = 0;
                int.TryParse(PageTools.CookieSourceId, out sourceId);
                string mobile = PageTools.CookieMobile;
                string guid = Guid.NewGuid().ToString("N");
                string uniqueUser = PageTools.CookieUniqueUser;
                if (string.IsNullOrEmpty(uniqueUser))
                {
                    uniqueUser = Guid.NewGuid().ToString("N");
                    PageTools.CookieUniqueUser = uniqueUser;
                }

                #endregion

                #region 添加日志

                WeDo.Log.Model.LogTrackMessage message = new WeDo.Log.Model.LogTrackMessage();
                message.SourceId = sourceId;
                message.BeforeUrl = "";
                message.BrowserName = "";
                message.CookiesSign = uniqueUser;
                message.DataId = 0;
                message.DataType = 0;
                message.DeviceName = "";
                message.GUID = guid;
                message.HttpMethod = "";
                message.IP = Request.UserHostAddress;
                message.MemberId = 0;
                message.ModuleName = modulName;
                message.OpenId = mobile;
                message.OSName = "";
                message.PagePath = url;
                message.PageUrl = Request.Url.ToString();
                message.Remark = "";
                message.SearchKey = "";
                message.SearchName = "";
                message.SessionId = Session.SessionID;
                message.ShareSignId = 0;
                message.SourceId = sourceId;
                message.SystemSign = "volvo_ebook_click";
                message.UserId = 0;

                WeDo.Log.LogTrackMan.AddLog(message, WeDo.Log.Model.EnumListLog.LogLevel.INFO);
                reJson = string.Format(json, "1", "success", "", 0);

                #endregion
            }
            catch (Exception ex)
            {
                LogExceptionMan.AddLog(logName, WeDo.Log.Model.EnumListLog.LogLevel.ERROR, ex);
                reJson = string.Format(json, "2", "error", "", 0);
            }

            return Content(reJson);
        }

        #endregion




    }
}