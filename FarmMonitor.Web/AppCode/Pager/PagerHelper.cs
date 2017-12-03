using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;


namespace System.Web.Mvc
{
    public static class PagerHelper
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id">分页id</param>
        /// <param name="currentPageIndex">当前页</param>
        /// <param name="pageSize">分页尺寸</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="htmlAttributes">分页头标签属性</param>
        /// <param name="className">分页样式</param>
        /// <param name="mode">分页模式</param>
        /// <returns></returns>
        public static string Pager(this HtmlHelper helper, string id, int currentPageIndex, int pageSize, int recordCount, object htmlAttributes, string className,PageMode mode)
        {
            TagBuilder builder = new TagBuilder("table");
            builder.IdAttributeDotReplacement = "_";
            builder.GenerateId(id);
            builder.AddCssClass(className);
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            builder.InnerHtml = GetNormalPage(currentPageIndex, pageSize, recordCount,mode);
            return builder.ToString();
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id">分页id</param>
        /// <param name="currentPageIndex">当前页</param>
        /// <param name="pageSize">分页尺寸</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="className">分页样式</param>
        /// <returns></returns>
        public static string Pager(this HtmlHelper helper, string id, int currentPageIndex, int pageSize, int recordCount, string className)
        {
            return Pager(helper, id, currentPageIndex, pageSize, recordCount, null, className,PageMode.Normal);
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id">分页id</param>
        /// <param name="currentPageIndex">当前页</param>
        /// <param name="pageSize">分页尺寸</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns></returns>
        public static string Pager(this HtmlHelper helper,string id,int currentPageIndex,int pageSize,int recordCount)
        {
            return Pager(helper, id, currentPageIndex, pageSize, recordCount,null);
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id">分页id</param>
        /// <param name="currentPageIndex">当前页</param>
        /// <param name="pageSize">分页尺寸</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="mode">分页模式</param>
        /// <returns></returns>
        public static string Pager(this HtmlHelper helper, string id, int currentPageIndex, int pageSize, int recordCount,PageMode mode)
        {
            return Pager(helper, id, currentPageIndex, pageSize, recordCount, null,mode);
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id">分页id</param>
        /// <param name="currentPageIndex">当前页</param>
        /// <param name="pageSize">分页尺寸</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="className">分页样式</param>
        /// <param name="mode">分页模式</param>
        /// <returns></returns>
        public static string Pager(this HtmlHelper helper, string id, int currentPageIndex, int pageSize, int recordCount,string className, PageMode mode)
        {
            return Pager(helper, id, currentPageIndex, pageSize, recordCount, null,className,mode);
        }
        /// <summary>
        /// 获取普通分页
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        private static string GetNormalPage(int currentPageIndex, int pageSize, int recordCount,PageMode mode)
        {
            if (recordCount <= pageSize)
                return "";
            int pageCount = (recordCount%pageSize ==0?recordCount/pageSize:recordCount/pageSize+1);
            StringBuilder url = new StringBuilder();
            url.Append(HttpContext.Current.Request.Url.AbsolutePath+"?page={0}");
            NameValueCollection collection = HttpContext.Current.Request.QueryString;
            string[] keys = collection.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].ToLower() != "page")
                    url.AppendFormat("&{0}={1}", keys[i], collection[keys[i]]);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<tr><td>");
            //sb.AppendFormat("总共{0}条记录,共{1}页,当前第{2}页&nbsp;&nbsp;", recordCount, pageCount, currentPageIndex);
            sb.AppendFormat("页数{0}/{1}</td>", pageCount, currentPageIndex);
            if (currentPageIndex == 1)
                sb.Append("<td><span><img style=\"width:15px;height:15px;background-size:100%,100%;\" src=\"../../Content/images/pa-03.png\"></img></span></td>");
            else
            {
                string url1 = string.Format(url.ToString(), 1);
                sb.AppendFormat("<td><span><a href={0}><img style=\"width:15px;height:15px;background-size:100%,100%;\" src=\"../../Content/images/pa-03.png\"></img></a></span></td>", url1);
            }
            if (currentPageIndex > 1)
            {
                string url1 = string.Format(url.ToString(), currentPageIndex - 1);
                sb.AppendFormat("<td><span><a href={0}><img style=\"width:15px;height:15px;background-size:100%,100%;\" src=\"../../Content/images/pa-02.png\"></img></a></span></td>", url1);
            }
            else
                sb.Append("<td><span><img style=\"width:15px;height:15px;background-size:100%,100%;\" src=\"../../Content/images/pa-02.png\"></img></span></td>");
            //if(mode == PageMode.Numeric)
            //    sb.Append(GetNumericPage(currentPageIndex,pageSize,recordCount,pageCount,url.ToString()));
            if (currentPageIndex < pageCount)
            {
                string url1 = string.Format(url.ToString(), currentPageIndex + 1);
                sb.AppendFormat("<td><span><a href={0}><img style=\"width:15px;height:15px;background-size:100%,100%;\" src=\"../../Content/images/pa-01.png\"></img></a></span></td>", url1);
            }
            else
                sb.Append("<td><span><img style=\"width:15px;height:15px;background-size:100%,100%;\" src=\"../../Content/images/pa-01.png\"></img></span></td>");

            if (currentPageIndex == pageCount)
                sb.Append("<td><span><img style=\"width:15px;height:15px;background-size:100%,100%;\" src=\"../../Content/images/pa-04.png\"></img></span></td>");
            else
            {
                string url1 = string.Format(url.ToString(), pageCount);
                sb.AppendFormat("<td><span><a href={0}><img style=\"width:15px;height:15px;background-size:100%,100%;\" src=\"../../Content/images/pa-04.png\"></img></a></span></td>", url1);
            }          
            sb.Append("</tr>");
            return sb.ToString();
        }
        /// <summary>
        /// 获取数字分页
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <param name="pageCount"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetNumericPage(int currentPageIndex, int pageSize, int recordCount, int pageCount,string url)
        {
            int k = currentPageIndex / 10;
            int m = currentPageIndex % 10;
            StringBuilder sb = new StringBuilder();
            if (currentPageIndex / 10 == pageCount / 10)
            {
                if (m == 0)
                {
                    k--;
                    m = 10;
                }
                else
                    m = pageCount%10;
            }
            else
                m = 10;
            for (int i = k * 10 + 1; i <= k * 10 + m; i++)
            {
                if (i == currentPageIndex)
                    sb.AppendFormat("<span><font color=red><b>{0}</b></font></span>&nbsp;", i);
                else
                {
                    string url1 = string.Format(url.ToString(), i);
                    sb.AppendFormat("<span><a href={0}>{1}</a></span>&nbsp;",url1, i);
                }
            }
            
            return sb.ToString();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id">分页id</param>
        /// <param name="currentPageIndex">当前页</param>
        /// <param name="pageSize">分页尺寸</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="mode">分页模式</param>
        /// sunho 20160219
        public static string Pager1(this HtmlHelper helper, string id, int currentPageIndex, int pageSize, int recordCount, string className)
        {
            TagBuilder builder = new TagBuilder("div");
            builder.IdAttributeDotReplacement = "_";
            builder.GenerateId(id);
            builder.AddCssClass(className);
           // builder.MergeAttributes(new RouteValueDictionary(null));
            builder.InnerHtml = GetNormalPage1(currentPageIndex, pageSize, recordCount);
            return builder.ToString();
        }

        /// <summary>
        /// 获取普通分页
        /// </summary>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        private static string GetNormalPage1(int currentPageIndex, int pageSize, int recordCount)
        {
            if (recordCount <= pageSize)
                return "";
            int pageCount = (recordCount % pageSize == 0 ? recordCount / pageSize : recordCount / pageSize + 1);
            StringBuilder url = new StringBuilder();
            url.Append(HttpContext.Current.Request.Url.AbsolutePath + "?page={0}");
            NameValueCollection collection = HttpContext.Current.Request.QueryString;
            string[] keys = collection.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].ToLower() != "page")
                    url.AppendFormat("&{0}={1}", keys[i], collection[keys[i]]);
            }
            StringBuilder sb = new StringBuilder();
            if (currentPageIndex > 1)
            {
                string url1 = string.Format(url.ToString(), currentPageIndex - 1);
                sb.AppendFormat("<a href={0}><span class=\"pagecolor\">上一页</span></a>", url1);
            }
            else
                sb.Append("<span>上一页</span>");

            if (currentPageIndex < pageCount)
            {
                string url1 = string.Format(url.ToString(), currentPageIndex + 1);
                sb.AppendFormat("<a href={0}><span class=\"pagecolor\">下一页</span></a>", url1);
            }
            else
                sb.Append("<span>下一页</span>");

            return sb.ToString();
        }
    }
    /// <summary>
    /// 分页模式
    /// </summary>
    public enum PageMode
    {
        /// <summary>
        /// 普通分页模式
        /// </summary>
        Normal,
        /// <summary>
        /// 普通分页加数字分页
        /// </summary>
        Numeric
    }
}