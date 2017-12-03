using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmMonitor.Web.Models
{
        public class RequestTestModel
        {
            public string FromUserName { set; get; }

            public string ToUserName { set; get; }

            public string MsgType { set; get; }

            public string Event { set; get; }

            public string EventKey { set; get; }

            public string Ticket { set; get; }

            public string Contents { set; get; }

            public decimal Lat { set; get; }

            public decimal Lng { set; get; }

            public string MediaId { set; get; }

            public string PicUrl { set; get; }

            public string ThumbMediaId { set; get; }

    }
}