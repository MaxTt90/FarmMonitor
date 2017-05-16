using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using FarmMonitor.DAL;
using FarmMonitor.Model;

namespace FarmMonitor.BLL
{
    //LogTrack
    public class LogTrackMan
    {
        private CustomDbContext db = CustomDbContext.Instance;

        public void Add(LogTrack entity)
        {
            db.LogTracks.Add(entity);
            db.SaveChanges();
        }
        public void Update(LogTrack entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(LogTrack entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            LogTrack entity = this.GetEntity(id);
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public LogTrack GetEntity(int id)
        {
            return db.LogTracks.Find(id);
        }

        /// <summary>
        /// 根据GUID获得日志对象
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <returns>跟踪日志对象</returns>
        /// Author:fredjiang
        /// Created:2015-12-29
        public LogTrack GetEntity(string guid)
        {
            return db.LogTracks.FirstOrDefault(o=>o.GUID==guid);
        }
        public void AddLog(string systemName, int sourceId, int userId, int memberId, string cookiesSign, string moduleName, int dataType, int dataId, int shareSignId, string remark, string openId, string GUID,string session)
        {
            try
            {
                LogTrack message = new LogTrack();

                message.SystemSign = systemName;
                message.SourceId = sourceId;
                message.Remark = remark;
                message.UserId = userId;
                message.DataId = dataId;
                message.DataType = dataType;
                message.MemberId = memberId;
                message.OpenId = openId;
                message.CookiesSign = cookiesSign;
                message.ModuleName = moduleName;
                message.ShareSignId = shareSignId;
                message.Remark = remark;
                message.GUID = GUID;
                message.SessionId = session;

                try
                {
                    HttpRequest req = HttpContext.Current.Request;
                    message.IP = req.UserHostAddress; //IP地址
                    message.PagePath = req.FilePath; //页面路径
                    message.PageUrl = req.Url.AbsoluteUri; //完整的URL
                    message.HttpMethod = req.HttpMethod;//请求类型
                    message.BeforeUrl = req.UrlReferrer.AbsoluteUri;//前一页面完整URL
                }
                catch
                {

                }

                Add(message);
                //Info(message);
            }
            catch
            {

            }
        }

    }
}