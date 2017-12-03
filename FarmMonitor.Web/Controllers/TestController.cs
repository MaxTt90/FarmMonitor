using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FarmMonitor.BLL;
using FarmMonitor.Web.AppCode;
using FarmMonitor.Web.Models;
using Microsoft.Ajax.Utilities;

namespace FarmMonitor.Web.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/
        public ActionResult Index(string m="",int s=0)
        {
            string uniqueNo = PageTools.CookieUniqueUser;
            if (uniqueNo == "")
            {
                PageTools.CookieUniqueUser = Guid.NewGuid().ToString("N");
            }
            if (m != "")
            {
                PageTools.CookieMobile = m;
            }
            else
            {
                m = PageTools.CookieMobile;
            }

            if (s>0)
            {
                PageTools.CookieSourceId = s.ToString();
            }
            else
            {
                int.TryParse(PageTools.CookieSourceId, out s);
            }
            return View();
        }

        public ActionResult GetSession()
        {
            return Json(Session.SessionID);
        }




        public ActionResult testl()
        {
            var a = GetColumnName(40);
            return Content(a);
        }
        public string GetColumnName(int count)
        {
            var dividend = count;
            var columnName = string.Empty;

            while (dividend > 0)
            {
                var modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }
	}
}