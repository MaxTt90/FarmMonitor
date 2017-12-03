using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmMonitor.Web.Controllers
{
    using System.Web.Mvc;

    using FarmMonitor.BLL;
    using FarmMonitor.Web.AppCode.Filter;

    public class ChartController : Controller
    {
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


    }
}