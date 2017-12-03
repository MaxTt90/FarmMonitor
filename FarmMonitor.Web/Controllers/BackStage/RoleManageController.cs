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
    //[UserInfoAuthFilter]
    public class RoleManageController : Controller
    {
        private SysRoleInfoMan roleInfoMan = new SysRoleInfoMan();
        [UserInfoAuthFilter]
        public ActionResult Index(int pageIndex = 1)
        {
            int sum = 0;
            int total = 0;
            var userList = roleInfoMan.GetModelByPage(pageIndex, out sum, out total);
            ViewBag.UserList = userList;
            TempData["PageModel"] = new PagedViewModel() { IsFirstPage = pageIndex == 1, IsLastPage = pageIndex == sum, PageCount = sum, PageIndex = pageIndex, RequestUrl = "/rolemanage/index/", PageSum = total };
            return View();
        }
        //新增
        public ActionResult AddRole()
        {
            string name = Request.Form["name"];
            if (string.IsNullOrEmpty(name))
            {

                return Json(new { code = "0", msg = "角色名不能为空" });

            }
            var model = roleInfoMan.GetModelByUsername(name);
            if (model != null)
            {
                return Json(new { code = "1", msg = "角色名已存在" });
            }
            string code = Request.Form["code"];
            if (string.IsNullOrEmpty(code))
            {
                return Json(new { code = "0", msg = "角色代码不能为空" });

            }
            model = roleInfoMan.GetModelByCode(code);
            if (model != null)
            {
                return Json(new { code = "2", msg = "角色代码已存在" });
            }
            string abbr = Request.Form["abbr"] ?? "";
            string desc = Request.Form["desc"] ?? "";
            model = new SysRoleInfo() { Abbreviation = abbr, Code = code, Name = name, CreateTime = DateTime.Now, Description = desc };
            roleInfoMan.Add(model);

            return Json(new { code = "200", msg = "新增成功" });

        }


        public ActionResult Add()
        {
            return View();
        }

        //修改状态
        public ActionResult ChangeState(int id)
        {
            var model = roleInfoMan.GetEntity(id);
            if (model == null)
            {
                return Json(new { code = "500", msg = "参数错误" });
            }
            var state = Request.Params["state"];
            if (state == "1")
            {
                model.DataState = DataStateEnum.正常;

            }
            else if (state == "2")
            {
                model.DataState = DataStateEnum.禁用;

            }
            else if (state == "3")
            {
                model.DataState = DataStateEnum.删除;
            }
            else
            {
                return Json(new { code = "500", msg = "参数错误" });
            }
            roleInfoMan.Update(model);
            return Json(new { code = "200", msg = "修改成功" });
        }

        //编辑权限
        public ActionResult EditRole(int id)
        {
            var role = roleInfoMan.GetEntity(id);
            if (role == null)
            {
                return Json(new { code = "500", msg = "参数错误" });
            }

            string name = Request.Form["name"];
            if (string.IsNullOrEmpty(name))
            {

                return Json(new { code = "0", msg = "角色名不能为空" });

            }
            var model = roleInfoMan.GetModelByUsername(name);
            if (model != null && model.Id != role.Id)
            {
                return Json(new { code = "1", msg = "角色名已存在" });
            }
            string code = Request.Form["code"];
            if (string.IsNullOrEmpty(code))
            {
                return Json(new { code = "0", msg = "角色代码不能为空" });

            }
            model = roleInfoMan.GetModelByCode(code);
            if (model != null && code != role.Code)
            {
                return Json(new { code = "2", msg = "角色代码已存在" });
            }
            string abbr = Request.Form["abbr"] ?? "";
            string desc = Request.Form["desc"] ?? "";
            role.Name = name;
            role.Abbreviation = abbr;
            role.Description = desc;
            role.Code = code;
            role.UpdateTime = DateTime.Now;
            role.UpdateId = 0;
            roleInfoMan.Update(role);
            return Json(new { code = "200", msg = "编辑成功" });
        }


        public ActionResult Edit(int id)
        {
            var model = roleInfoMan.GetEntity(id);
            return View(model);
        }

        //加载该角色的权限树
        public ActionResult LoadRightForThisRole(int id)
        {
            var model = roleInfoMan.GetEntity(id);
            if (model == null)
            {
                return Json(new { code = "500", msg = "该角色不存在" });
            }

            var list =
                model.SysRoleRights.Select(
                    c => c.RightId
                       ).ToList();

            var l = new SysRightInfoMan().GetEntityList(c => c.DataState != DataStateEnum.删除 && c.IsPublic == false);
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (var r in l)
            {
                sb.Append(string.Format("{{ \"Id\": \"{0}\",\"checked\":\"{1}\",\"name\": \"{2}\",\"ParentId\": \"{3}\"}},", r.Id, list.Contains(r.Id), r.RightName, r.ParentId));
            }

            var tree = sb.ToString();
            tree = tree.Substring(0, tree.Length - 1);
            tree += "]";
            ViewBag.TreeData = tree;

            ViewBag.Id = id;
            return View();

        }
        //保存分配好的权限
        [HttpPost]
        public ActionResult LoadRightForThisRole(int id, string rights)
        {
            var model = roleInfoMan.GetEntity(id);
            if (model == null)
            {
                return Json(new { code = "500", msg = "该角色不存在" });
            }

            var rightList = rights.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList().Select(c => Convert.ToInt32(c)).Distinct().ToList();
            int count = -1;

            count = new SysRoleRightMan().AddRoleRightForRole(id, rightList);
            if (count < 0)
            {
                return new JsonResult() { Data = new { code = 0, msg = "保存失败" } };
            }
            return new JsonResult() { Data = new { code = 1, msg = "保存成功" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}