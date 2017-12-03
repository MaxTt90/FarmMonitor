using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FarmMonitor.BLL;
using FarmMonitor.Model;
using FarmMonitor.Web.AppCode.Filter;
using FarmMonitor.Web.Models;

namespace FarmMonitor.Web.Controllers
{
    using FarmMonitor.Web.AppCode;

    public class DataController : Controller
    {

        #region 数据列表

        //
        // GET: /Data/
        [UserInfoAuthFilter]
        public ActionResult Index()
        {
            var user = Startup.GetUserInfo();
            CollectorMan cMan = new CollectorMan();
            SensorMan sensorMan = new SensorMan();
            CollectDataMan collectDataMan = new CollectDataMan();
            UserOrchardMan userOrchardMan = new UserOrchardMan();
            var orchards = userOrchardMan.GetListByUserId(user.ID);
            var collectors = cMan.GetByOrchard(orchards.Select(x => x.OrchardId).ToList());
            var sensors = sensorMan.GetAll().ToList();

            ViewBag.collectors = collectors;
            ViewBag.sensors = sensors;
            return View();
        }

        [UserInfoAuthFilter]
        public ActionResult LoadIndexTable(int pageIndex = 1)
        {
            var user = Startup.GetUserInfo();
            int sum = 0;
            int total = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append("/Data/LoadIndexTable?userId=" + user.ID);

            #region 数据处理

            //采集器
            var collectorId = Request.Params["collectorId"];
            if (!string.IsNullOrEmpty(collectorId))
            {
                sb.Append("&collectorId=" + collectorId);
            }
            else
            {
                collectorId = "0";
            }
            //传感器
            var sensorId = Request.Params["sensorId"];
            if (!string.IsNullOrEmpty(sensorId))
            {
                sb.Append("&sensorId=" + sensorId);
            }
            else
            {
                sensorId = "0";
            }

            var timeStart = Request.Params["timeStart"]; //提交时间
            var timeEnd = Request.Params["timeEnd"];
            DateTime time1 = Convert.ToDateTime("1900-1-1");
            DateTime time2 = Convert.ToDateTime("2090-1-1");
            if (!string.IsNullOrEmpty(timeStart))
            {
                time1 = Convert.ToDateTime(timeStart);
                sb.Append("&timeStart=" + timeStart);
            }

            if (!(string.IsNullOrEmpty(timeEnd) || timeEnd.Equals("1")))
            {
                time2 = Convert.ToDateTime(timeEnd);
                sb.Append("&timeEnd=" + timeEnd);
            }

            #endregion

            var table = new List<CollectDataViewModels>();

            table = GetViewListByPage(
                user.ID,
                Convert.ToInt32(collectorId),
                Convert.ToInt32(sensorId),
                time1,
                time2,
                out sum,
                out total,
                pageIndex,
                10);
            
            if (table.Count == 0)
            {
                return Content("无数据");
            }
            //ViewBag.form = form;
            ViewBag.table = table;
            TempData["PageModel"] = new PagedViewModel()
                                        {
                                            PageIndex = pageIndex,
                                            PageCount = sum,
                                            IsFirstPage = pageIndex == 1,
                                            IsLastPage = pageIndex == sum,
                                            RequestUrl = sb.ToString(),
                                            PageSum = total
                                        };

            return PartialView("IndexTable");

        }

        #endregion

        #region 图表

        public ActionResult Chart()
        {
            CollectDataMan collectDataMan = new CollectDataMan();
            collectDataMan.SearchByCollectorAndSensor(1001, 2);


            return View();
        }

        #endregion

        public List<CollectDataViewModels> GetViewListByPage(
            int userId,
            int collector,
            int sensor,
            DateTime timeStart,
            DateTime timeEnd,
            out int sum,
            out int totalCount,
            int pageIndex,
            int pageSize = 10)
        {
            CollectDataMan collectDataMan = new CollectDataMan();
            var ds = collectDataMan.SearchAllByUserId(userId, timeStart, timeEnd, collector, sensor);
            List<CollectDataViewModels> t = PageTools.DataSetToIList<CollectDataViewModels>(ds, 0).ToList();

            totalCount = t.Count();
            sum = totalCount % pageSize == 0 ? totalCount / pageSize : totalCount / pageSize + 1;
            return t.Take(pageSize * pageIndex).Skip(pageSize * (pageIndex - 1)).ToList();
        }
    }
}