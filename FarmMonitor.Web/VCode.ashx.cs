using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using FarmMonitor.Common;

namespace FarmMonitor.Web
{
    /// <summary>
    /// VCode 的摘要说明
    /// </summary>
    public class VCode : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string vcode = CaptchaHelper.CreateRandomCode(4);
            context.Session["img_vcode"] = vcode;
            byte[] bs = CaptchaHelper.DrawImage(vcode, 20, Color.White);
            context.Response.ContentType = "image/gif";
            context.Response.BinaryWrite(bs);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}