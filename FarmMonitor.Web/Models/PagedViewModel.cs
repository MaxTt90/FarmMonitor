using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmMonitor.Web.Models
{
    /// <summary>
    /// 分页model
    /// </summary>
    public class PagedViewModel
    {
        private int _pageIndex = 1;
        private int _pageCount = 0;
        private bool _isLastPage = false;
        private bool _isFirstPage = false;
        private string _requestUrl = "";
        private int _pageSize = 10;
        private int _pageSum = 0;
        private int _viewCount = 10;

        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }

        /// <summary>
        /// 是否是最后一页
        /// </summary>
        public bool IsLastPage
        {
            get { return _isLastPage; }
            set { _isLastPage = value; }
        }

        /// <summary>
        /// 是否是第一页
        /// </summary>
        public bool IsFirstPage
        {
            get { return _isFirstPage; }
            set { _isFirstPage = value; }
        }

        /// <summary>
        /// 请求的url,如果没有参数，则是/controller/action/
        /// 如果有参数，则是/controller/action/?a=3&b=5
        /// </summary>
        public string RequestUrl
        {
            get { return _requestUrl; }
            set { _requestUrl = value; }
        }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int PageSum
        {
            get { return _pageSum; }
            set { _pageSum = value; }
        }

        /// <summary>
        /// 显示分页按钮最大数量
        /// </summary>
        public int ViewCount
        {
            get { return _viewCount; }
            set { _viewCount = value; }
        }
    }
}