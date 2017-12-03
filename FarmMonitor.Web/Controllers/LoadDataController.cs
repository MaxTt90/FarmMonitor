using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FarmMonitor.BLL;
using FarmMonitor.Model;
using FarmMonitor.Web.AppCode;
using Microsoft.Owin.BuilderProperties;

namespace FarmMonitor.Web.Areas.WeChat.Controllers
{
    public class LoadDataController : Controller
    {
        //
        // GET: /WeChat/LoadData/
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 得到省份
        /// </summary>       
        public ActionResult ProvinceLoad()
        {
            SysCityMan cityMan = new SysCityMan();
            var provList = cityMan.GetProvinceByCityLevel(1);
            return new JsonResult() { Data = provList.Select(c => new { c.Name, c.ShortName, c.ProvinceId, c.CityId, c.Id }), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /// <summary>
        /// 得到城市
        /// </summary>
        /// <param name="provId">一级城市编号id</param>      
        public ActionResult CityLoad(int provId)
        {

            SysCityMan cityMan = new SysCityMan();
            var cityList = cityMan.GetCityByProvinceIdAndCityLevel(2, provId);
            return new JsonResult() { Data = cityList.Select(c => new { c.Name, c.ShortName, c.ProvinceId, c.CityId, c.Id }), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// 得到城市
        /// </summary>
        /// <param name="provName">一级城市名称</param>      
        public ActionResult LoadCity(string provName)
        {
            SysCityMan cityMan = new SysCityMan();
            var cityList = cityMan.GetCityByProvinceIdAndCityLevel(2, provName);
            return new JsonResult() { Data = cityList.Select(c => new { c.Name, c.ShortName, c.ProvinceId, c.CityId, c.Id }), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


	}
}