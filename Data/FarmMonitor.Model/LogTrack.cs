/**
* LogTrack.cs
*
* 功 能： N/A
* 类 名： LogTrack
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/11/13 15:34:00   N/A    初版
*
* Copyright (c) 2015 WEDO Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海唯都企业管理咨询有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FarmMonitor.Model
{
    //LogTrack
    [Serializable]
    [Table("LogTrack")]
    public class LogTrack
    {
        #region Model
        private int _id;
        private string _systemsign = "";
        private string _httpmethod = "";
        private string _pagepath = "";
        private string _pageurl = "";
        private string _beforeurl = "";
        private string _ip = "";
        private DateTime _createtime = DateTime.Now;
        private DateTime _refreshtime = DateTime.Now;
        private int _sourceid = 0;
        private int _userid = 0;
        private int _memberid = 0;
        private string _openid = "";
        private string _sessionid = "";
        private string _browsername = "";
        private string _osname = "";
        private string _searchname = "";
        private string _searchkey = "";
        private string _cookiessign = "";
        private string _modulename = "";
        private int _dataid = 0;
        private int _datatype = 0;
        private int _sharesignid = 0;
        private string _devicename = "";
        private string _remark = "";
        private string _guid = "";

        /// <summary>
        /// 自动编号
        /// </summary>		

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 系统标识
        /// </summary>		

        public string SystemSign
        {
            get { return _systemsign; }
            set { _systemsign = value; }
        }

        /// <summary>
        /// 请求类型
        /// </summary>		
        [MaxLength(20, ErrorMessage = "字符长度超过20")]
        public string HttpMethod
        {
            get { return _httpmethod; }
            set { _httpmethod = value; }
        }

        /// <summary>
        /// 请求页面
        /// </summary>		

        public string PagePath
        {
            get { return _pagepath; }
            set { _pagepath = value; }
        }

        /// <summary>
        /// 完整Url
        /// </summary>		

        public string PageUrl
        {
            get { return _pageurl; }
            set { _pageurl = value; }
        }

        /// <summary>
        /// 前一Url
        /// </summary>		

        public string BeforeUrl
        {
            get { return _beforeurl; }
            set { _beforeurl = value; }
        }

        /// <summary>
        /// IP
        /// </summary>		

        public string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>		

        public DateTime CreateTime
        {
            get { return _createtime; }
            set { _createtime = value; }
        }

        /// <summary>
        /// 刷新时间
        /// </summary>		

        public DateTime RefreshTime
        {
            get { return _refreshtime; }
            set { _refreshtime = value; }
        }

        /// <summary>
        /// 来源标识
        /// </summary>		

        public int SourceId
        {
            get { return _sourceid; }
            set { _sourceid = value; }
        }

        /// <summary>
        /// 用户编号
        /// </summary>		

        public int UserId
        {
            get { return _userid; }
            set { _userid = value; }
        }

        /// <summary>
        /// 会员编号
        /// </summary>		

        public int MemberId
        {
            get { return _memberid; }
            set { _memberid = value; }
        }

        /// <summary>
        /// 粉丝编号
        /// </summary>		

        public string OpenId
        {
            get { return _openid; }
            set { _openid = value; }
        }

        /// <summary>
        /// 会话编号
        /// </summary>		
        [MaxLength(50, ErrorMessage = "字符长度超过50")]
        public string SessionId
        {
            get { return _sessionid; }
            set { _sessionid = value; }
        }

        /// <summary>
        /// 浏览器
        /// </summary>		
        [MaxLength(100, ErrorMessage = "字符长度超过100")]
        public string BrowserName
        {
            get { return _browsername; }
            set { _browsername = value; }
        }

        /// <summary>
        /// 操作系统
        /// </summary>		
        [MaxLength(100, ErrorMessage = "字符长度超过100")]
        public string OSName
        {
            get { return _osname; }
            set { _osname = value; }
        }

        /// <summary>
        /// 搜索引擎名称
        /// </summary>		
        [MaxLength(50, ErrorMessage = "字符长度超过50")]
        public string SearchName
        {
            get { return _searchname; }
            set { _searchname = value; }
        }

        /// <summary>
        /// 搜索关键词
        /// </summary>		
        [MaxLength(200, ErrorMessage = "字符长度超过200")]
        public string SearchKey
        {
            get { return _searchkey; }
            set { _searchkey = value; }
        }

        /// <summary>
        /// Cookies标识
        /// </summary>		
        [MaxLength(100, ErrorMessage = "字符长度超过100")]
        public string CookiesSign
        {
            get { return _cookiessign; }
            set { _cookiessign = value; }
        }

        /// <summary>
        /// 模块名称
        /// </summary>		
        [MaxLength(100, ErrorMessage = "字符长度超过100")]
        public string ModuleName
        {
            get { return _modulename; }
            set { _modulename = value; }
        }

        /// <summary>
        /// 数据编号
        /// </summary>		

        public int DataId
        {
            get { return _dataid; }
            set { _dataid = value; }
        }

        /// <summary>
        /// 数据类型:1表示消息编号,2表示活动编号,3表示问卷编号,4表示礼品编号,5表示产品编号
        /// </summary>		

        public int DataType
        {
            get { return _datatype; }
            set { _datatype = value; }
        }

        /// <summary>
        /// 分享标识编号
        /// </summary>		

        public int ShareSignId
        {
            get { return _sharesignid; }
            set { _sharesignid = value; }
        }

        /// <summary>
        /// 设备名称
        /// </summary>		

        public string DeviceName
        {
            get { return _devicename; }
            set { _devicename = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>		
        [MaxLength(200, ErrorMessage = "字符长度超过200")]
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        /// <summary>
        /// GUID
        /// </summary>		

        public string GUID
        {
            get { return _guid; }
            set { _guid = value; }
        }

        #endregion
    }
}