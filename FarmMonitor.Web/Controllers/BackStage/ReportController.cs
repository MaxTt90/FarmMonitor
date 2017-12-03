using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AOWEN.BLL;
using AOWEN.Common;
using AOWEN.Model;
using AOWEN.Web.AppCode.Filter;
using AOWEN.Web.Models;
using WeDo.Log;
using WeDo.Log.Model;

namespace AOWEN.Web.Controllers.BackStage
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/
        //[UserInfoAuthFilter]
        public ActionResult Index(int f)
        {
            //FormInfoMan fiMan = new FormInfoMan();
            //var form = fiMan.GetEntity(f);

            var campaignId = Request["campaign"];
            if (!string.IsNullOrEmpty(campaignId))
            {
                ViewBag.selectCampaign = campaignId;
            }
            else
            {
                ViewBag.selectCampaign = "";
            }
            CampaignInfoMan cMan = new CampaignInfoMan();
            if (f == 0)
            {
                ViewBag.campaigns = cMan.GetList();
            }
            else
            {
            ViewBag.campaigns = cMan.GetByFormAndType(f,0);
            }
            WXSourceMan sourceMan = new WXSourceMan();
            ViewBag.sources = sourceMan.GetList();
            ViewBag.formId = f;
            return View();
        }
        [UserInfoAuthFilter]
        public ActionResult Report()
        {
            #region 角色类型判断

            var type = -1;
            var isExport = 0;
            List<SysUserInfo> users = new List<SysUserInfo>();
            SysUserInfoMan userInfoMan = new SysUserInfoMan();
            List<int> roleIds = new List<int>();
            if (Startup.IsOperateRight(10092)) //表单
            {
                type = 1;
                //roleIds.Add(2);
            }
            else if (Startup.IsOperateRight(10093)) //电子杂志
            {
                type = 2;
                //roleIds.Add(4);
            }
            if (Startup.IsOperateRight(10095)) //管理员
            {
                type = 0;
                //roleIds.Add(2);
               // roleIds.Add(4);
            }
            if (Startup.IsOperateRight(10096))//导出数据
            {
                isExport = 1;
            }

            //users = userInfoMan.GetByRole(roleIds);
            #endregion

            var formId = Request["formId"];
            if (!string.IsNullOrEmpty(formId))
            {
                if (type != 0)
                {
                    var form = new FormInfoMan().GetEntity(Convert.ToInt32(formId)) ?? new FormInfo();
                    if (form.Type != type)
                    {
                        ViewBag.selectForm = "";//没有权限查看对应表单
                    }
                }
                ViewBag.selectForm = formId;
            }
            else
            {
                ViewBag.selectForm = "";
            }
            var campaignId = Request["campaign"];
            if (!string.IsNullOrEmpty(campaignId))
            {
                if (type != 0)
                {
                    var campaign = new CampaignInfoMan().GetEntity(Convert.ToInt32(campaignId)) ?? new CampaignInfo();
                    if (campaign.Type != type)
                    {
                        ViewBag.selectCampaign = "";//没有权限查看对应活动
                    }
                }
                ViewBag.selectCampaign = campaignId;
            }
            else
            {
                ViewBag.selectCampaign = "";
            }
            //ViewBag.users = users;
            FormInfoMan fiMan = new FormInfoMan();
            ViewBag.forms = fiMan.GetListByType(type);
            WXSourceMan sourceMan = new WXSourceMan();
            ViewBag.sources = sourceMan.GetList();
            ViewBag.isExport = isExport;
            return View();
        }

        public ActionResult LoadIndexTable(int pageIndex = 1)
        {

            int sum = 0;
            int total = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append("/Report/LoadIndexTable?s=b");

            #region 数据处理

            //表单
            var formId = Request.Params["formId"];
            if (!string.IsNullOrEmpty(formId))
            {
                sb.Append("&formId=" + formId);
            }
            else
            {
                formId = "";
            }
            //活动
            var campaignId = Request.Params["campaignId"];
            if (!string.IsNullOrEmpty(campaignId))
            {
                sb.Append("&campaignId=" + campaignId);
            }
            else
            {
                campaignId = "0";
            }
            //姓名
            var name = Request.Params["name"];
            if (!string.IsNullOrEmpty(name))
            {
                sb.Append("&name=" + name);
            }
            else
            {
                name = "";
            }
            //手机号
            var mobile = Request.Params["mobile"];
            if (!string.IsNullOrEmpty(mobile))
            {
                sb.Append("&mobile=" + mobile);
            }
            else
            {
                mobile = "";
            }
            //来源渠道
            var sourceId = Request.Params["sourceId"];
            if (!string.IsNullOrEmpty(sourceId))
            {
                sb.Append("&sourceId=" + sourceId);
            }
            else
            {
                sourceId = "0";
            }
            //是否导出
            var isExport = Request.Params["isExport"];
            if (!string.IsNullOrEmpty(isExport))
            {
                sb.Append("&isExport=" + isExport);
            }
            else
            {
                isExport = "0";
            }
            ////创建者
            //var user = Request.Params["user"];
            //if (!string.IsNullOrEmpty(user))
            //{
            //    sb.Append("&user=" + user);
            //}
            //else
            //{
            //    user = "0";
            //}
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

            List<CustomerInfo> table = new List<CustomerInfo>();
            if (formId == "")
            {
                return Content("请选择表单进行查询");
            }
            table = new CustomerInfoMan().GetListByPage(Convert.ToInt32(campaignId), formId, name,
                mobile,Convert.ToInt32(sourceId),Convert.ToInt32(isExport), time1, time2, out sum, out total, pageIndex, 10);

            //var form = new FormInfoMan().GetEntity(Convert.ToInt32(formId));

            if (table.Count == 0)
            {
                return Content("无数据");
            }
            var fields = new FormFieldMan().GetExportListByForm(Convert.ToInt32(formId));
            ViewBag.fields = fields;
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

        //导出表格
        [UserInfoAuthFilter]
        public ActionResult DownloadTable()
        {
            FormFieldMan formFieldMan = new FormFieldMan();
            CampaignInfoMan campaignInfoMan = new CampaignInfoMan();

            #region 数据传输

            var formId = Request["formId"];

            var fields = formFieldMan.GetExportListByForm(Convert.ToInt32(formId));
            if (fields == null || fields.Count == 0)
            {
                return
                    Content("<script>alert('表单信息没有找到！');window.location.href='/Report/Report?formId=" + formId + "'</script>");
            }
            var campaignId = Request["campaignId"];
            if (string.IsNullOrEmpty(campaignId))
            {
                campaignId = "0";
            }
            var time1 = Request["timeStart"];
            var time2 = Request["timeEnd"];
            DateTime startTime = Convert.ToDateTime("1990-01-01");
            DateTime endTime = Convert.ToDateTime("2090-01-01");
            if (!string.IsNullOrEmpty(time1))
            {
                startTime = Convert.ToDateTime(time1);
            }
            if (!string.IsNullOrEmpty(time2))
            {
                endTime = Convert.ToDateTime(time2);
            }

            var name = Request.Params["name"];
            if (string.IsNullOrEmpty(name))
            {
                name = "";
            }
            var mobile = Request.Params["mobile"];
            if (string.IsNullOrEmpty(mobile))
            {
                mobile = "";
            }
            //来源渠道
            var sourceId = Request.Params["sourceId"];
            if (string.IsNullOrEmpty(sourceId))
            {
                sourceId = "0";
            }
            //是否导出
            var isExport = Request.Params["isExport"];
            if (string.IsNullOrEmpty(isExport))
            {
                isExport = "0";
            }

            #endregion

            var ciMan = new CustomerInfoMan();
            var list0 = ciMan.GetListBySearch(Convert.ToInt32(campaignId), Convert.ToInt32(formId), name,
                mobile, Convert.ToInt32(sourceId),Convert.ToInt32(isExport), startTime, endTime);
            var list = list0.ToList();
            if (list == null || list.Count == 0)
            {
                return
                    Content("<script>alert('客户信息没有找到！');window.location.href='/Report/Report?formId=" + formId + "'</script>");
            }

            #region 创建表格

            DataTable dt = new DataTable();
            dt.Columns.Add("活动名称", typeof (string));
            foreach (var f in fields)
            {
                dt.Columns.Add(f.Name, typeof(string));
                if (f.ParentId == 88)
                {
                    dt.Columns.Add("经销商省份", typeof(string));
                    dt.Columns.Add("经销商城市", typeof(string));
                }
            }
            dt.Columns.Add("创建日期", typeof (string));
            dt.Columns.Add("来源", typeof(string));
            dt.Columns.Add("IP", typeof(string));
            dt.Columns.Add("URL", typeof(string));
            dt.Columns.Add("唯一标识", typeof(string));
            DealerInfoMan diMan = new DealerInfoMan();
            SysCityMan cityMan = new SysCityMan();
            WXSourceMan sourceMan = new WXSourceMan();
            foreach (var customer in list)
            {
                Type t = customer.GetType();
                var dtRow = dt.NewRow();
                dtRow["活动名称"] = campaignInfoMan.CampaignName(customer.CampaignId);
                foreach (FormField f in fields)
                {
                    var field = t.GetProperty(f.Code);
                    if (field == null)
                    {
                        dtRow[f.Name] = "";
                        LogRunMan.AddLog("客户信息导出", EnumListLog.LogLevel.INFO, DateTime.Now,
                            "列名" + f.Code + "没有匹配数据");
                    }
                    else
                    {
                        if (f.ParentId == 88)
                        {
                            var dealerCode = field.GetValue(customer);
                            var dealer = diMan.GetByCode(dealerCode.ToString()) ?? new DealerInfo();
                            var province = "";
                            var city = "";
                            if (dealer.CityId != 0)
                            {
                                city = cityMan.GetEntity(dealer.CityId).ShortName;
                            }
                            if (dealer.ProvinceId != 0)
                            {
                                province = cityMan.GetEntity(dealer.ProvinceId).ShortName;
                            }
                            dtRow["经销商省份"] = province;
                            dtRow["经销商城市"] = city;
                        }
                        dtRow[f.Name] = field.GetValue(customer);
                    }
                }
                dtRow["创建日期"] = customer.CreateTime;
                var source = sourceMan.GetEntity(customer.SourceId) ?? new WXSource();
                dtRow["来源"] = source.SourceName;
                dtRow["IP"] = customer.IP;
                dtRow["URL"] = customer.ApplyUrl;
                dtRow["唯一标识"] = customer.UniqueNo;
                dt.Rows.Add(dtRow);
            }
            ciMan.UpdateToExport(list0);
            #endregion

            ExcelHelper.ExportExcel(dt, "客户信息", System.Web.HttpContext.Current);
            return Content("<script>alert('导出成功！');window.location.href='/Report/Report?formId=" + formId + "'</script>");
        }
    }
}