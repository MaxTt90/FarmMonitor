using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FarmMonitor.BLL;
using FarmMonitor.Model;
using FarmMonitor.Web.AppCode.Filter;

namespace FarmMonitor.Web.Controllers
{
    public class RightManageController : Controller
    {
        private SysRightInfoMan RightInfoMan = new SysRightInfoMan();
        [UserInfoAuthFilter]
        //权限管理首页
        public ActionResult Index()
        {

            var treedata = RightInfoMan.GetEntityList(c => c.DataState != DataStateEnum.删除).OrderBy(c => c.SortId).Select(c => new { c.Id, c.ParentId, c.RightName, name = c.RightName });

            ViewBag.TreeData = System.Web.Helpers.Json.Encode(treedata);
            return View();
        }

        //权限管理树形菜单
        public ActionResult TreeIndex()
        {

            var treedata = RightInfoMan.GetEntityList(c => c.DataState == DataStateEnum.正常).Select(c => new { c.Id, c.ParentId, c.RightName, name = c.RightName });
            return Json(treedata);
        }

        // 删除权限菜单
        public ActionResult DelRight(int id)
        {
            var model = RightInfoMan.GetEntity(id);
            if (model == null)
            {
                return new JsonResult() { Data = new { code = "500", msg = "参数错误" } };
            }
            var subs = RightInfoMan.GetRightsOfOneRight(id);
            if (subs.Count > 0)
            {
                return new JsonResult() { Data = new { code = "2", msg = "请先删除子节点" } };
            }
            //var n = RightInfoMan.ChangeState(id, DataStateEnum.删除);


            model.DataState = DataStateEnum.删除;
            RightInfoMan.Update(model);
            return new JsonResult() { Data = new { code = "1", msg = "删除成功" } };
        }


        // 禁用权限菜单
        public ActionResult ForbidRight(int id)
        {
            var n = RightInfoMan.ChangeState(id, DataStateEnum.禁用);
            return new JsonResult() { Data = new { code = "1", msg = "禁用成功" } };
        }

        //启用权限菜单
        public ActionResult StartRight(int id)
        {
            var model = RightInfoMan.GetEntity(id);
            model.DataState = DataStateEnum.正常;
            RightInfoMan.Update(model);
            return new JsonResult() { Data = new { code = "1", msg = "启用成功" } };
        }

        // 变更菜单排序
        public ActionResult Sort(int id)
        {

            var type = Request.Form["type"];
            var pidStr = Request.Form["pid"];
            int pid = 0;
            if (!int.TryParse(pidStr, out pid))
            {
                return new JsonResult() { Data = new { code = "0", msg = "参数错误" } };
            }
            var list = RightInfoMan.GetEntityList(c => c.ParentId == pid).OrderBy(c => c.SortId).ToList();
            var currntModel = RightInfoMan.GetEntity(id);
            if (type == "up")
            {

                if (list[0].Id == id)
                {
                    return new JsonResult() { Data = new { code = "0", msg = "已经是最顶级了" } };
                }
                var index = list.IndexOf(currntModel);
                var upModel = RightInfoMan.GetEntity(list[index - 1].Id);
                var sortId = currntModel.SortId;
                currntModel.SortId = upModel.SortId;
                upModel.SortId = sortId;
                RightInfoMan.Update(currntModel);
                RightInfoMan.Update(upModel);

                return new JsonResult() { Data = new { code = "1", msg = "上移成功" } };


            }
            else if (type == "down")
            {
                if (list.Last().Id == id)
                {
                    return new JsonResult() { Data = new { code = "0", msg = "已经是最末级了" } };
                }
                var index = list.IndexOf(currntModel);
                var downModel = RightInfoMan.GetEntity(list[index + 1].Id);
                var sortId = currntModel.SortId;
                currntModel.SortId = downModel.SortId;
                downModel.SortId = sortId;
                RightInfoMan.Update(currntModel);
                RightInfoMan.Update(downModel);
                return new JsonResult() { Data = new { code = "1", msg = "下移成功" } };

            }
            return new JsonResult();
        }


        // 新增权限
        public JsonResult AddRight()
        {
            var rightName = Request.Form["rightName"];
            if (string.IsNullOrEmpty(rightName))
            {
                return new JsonResult() { Data = new { code = 0, msg = "请填写权限名称" } };
            }
            var rightPath = Request.Form["rightPath"];
            //if (string.IsNullOrEmpty(rightPath))
            //{
            //    return new JsonResult() { Data = new { code = 0, msg = "请填写权限路径" } };
            //}
            var id = Request.Form["id"];
            if (string.IsNullOrEmpty(id))
            {
                return new JsonResult() { Data = new { code = 0, msg = "参数错误" } };
            }
            var rightTypeStr = Request.Form["rightType"];
            int rightType = 0;
            if (string.IsNullOrEmpty(rightTypeStr))
            {
                return new JsonResult() { Data = new { code = 0, msg = "请选择权限类型" } };
            }
            if (!int.TryParse(rightTypeStr, out rightType))
            {
                return new JsonResult() { Data = new { code = 0, msg = "参数错误" } };
            }
            var chongfu = Request.Form["chongfu"] == "1";
            if (!chongfu)
            {
                if (string.IsNullOrEmpty(rightPath))
                {
                    var l = RightInfoMan.GetEntityList(c => c.FilePath == rightPath.Trim()).ToList();
                    if (l.Count > 0)
                    {
                        if (!int.TryParse(rightTypeStr, out rightType))
                        {
                            return new JsonResult() { Data = new { code = 0, msg = "已有该权限路径" } };
                        }
                    }
                }
            }

            var rightid = Convert.ToInt32(id);
            var rightDesc = Request.Form["rightDesc"];
            // bool showMenu = Request.Form["showMenu"] == "1";
            //  var showMenu = false;
            var commonRight = Request.Form["commonRight"] == "1";
            var pathPic = Request.Form["pathPic"];
            var pathPic2 = Request.Form["pathPic2"];
            SysRightInfo right = new SysRightInfo() { DataState = DataStateEnum.正常, FilePath = rightPath, Description = rightDesc, ParentId = rightid, RightName = rightName, RightType = rightType, PicPath = pathPic, IsPublic = commonRight, Id = RightInfoMan.GetMaxIdModel().Id + 1, SortId = RightInfoMan.GetMaxSortIdModel().SortId + 1,PicPathChange = pathPic2};
            var model = RightInfoMan.Add(right);
            return new JsonResult() { Data = new { code = 1, msg = "新增成功", id = model.Id } };
        }

        // 编辑权限
        public JsonResult EditRight()
        {
            var pid = Request.Form["parentId"];
            if (string.IsNullOrEmpty(pid))
            {
                return new JsonResult() { Data = new { code = 0, msg = "请选择父级菜单" } };
            }
            var rightName = Request.Form["rightName"];
            if (string.IsNullOrEmpty(rightName))
            {
                return new JsonResult() { Data = new { code = 0, msg = "请填写权限名称" } };
            }
            var rightPath = Request.Form["rightPath"];

            var id = Request.Form["id"];
            if (string.IsNullOrEmpty(id))
            {
                return new JsonResult() { Data = new { code = 0, msg = "参数错误" } };
            }
            var rightTypeStr = Request.Form["rightType"];
            int rightType = 0;
            if (string.IsNullOrEmpty(rightTypeStr))
            {
                return new JsonResult() { Data = new { code = 0, msg = "请选择权限类型" } };
            }
            if (!int.TryParse(rightTypeStr, out rightType))
            {
                return new JsonResult() { Data = new { code = 0, msg = "参数错误" } };
            }
            var chongfu = Request.Form["chongfu"] == "1";

            if (!chongfu)
            {
                if (string.IsNullOrEmpty(rightPath))
                {


                    var l = RightInfoMan.GetEntityList(c => c.FilePath == rightPath.Trim()).ToList();
                    if (l.Count > 1)
                    {
                        if (!int.TryParse(rightTypeStr, out rightType))
                        {
                            return new JsonResult() { Data = new { code = 0, msg = "已有该权限路径" } };
                        }
                    }
                }
            }
            var rightid = Convert.ToInt32(id);
            var parentid = Convert.ToInt32(pid);
            var rightDesc = Request.Form["rightDesc"];
            //   bool showMenu = Request.Form["showMenu"] == "1";
            var commonRight = Request.Form["commonRight"] == "1";
            var pic = Request.Form["pathPic"];
            var pathPic2 = Request.Form["pathPic2"];
            var model = RightInfoMan.GetEntity(rightid);
            if (model == null)
            {
                return new JsonResult() { Data = new { code = 0, msg = "参数错误" } };
            }
            model.RightType = rightType;
            model.Description = rightDesc;
            model.RightName = rightName;
            model.FilePath = rightPath;
            if (model.ParentId != parentid)
            {
                model.ParentId = parentid;
            }
            //  model.SortId = RightInfoMan.GetMaxSortIdModel().SortId++;
            //TODO 问题：更改上级菜单的时候，sorid会带过去。
            model.IsView = false;
            model.IsPublic = commonRight;
            model.PicPath = pic;
            model.PicPathChange = pathPic2;
            // model.OperateTypeEnum = rightType;
            RightInfoMan.Update(model);
            var list = RightInfoMan.GetEntityList(c => c.DataState == DataStateEnum.正常).ToList();
            //   GetRightsOfOneRight(list, model.Id);
            var goalModelList = RightInfoMan.GetRightsOfOneRight(model.Id);

            if (goalModelList != null && goalModelList.Count > 0)
            {
                return new JsonResult() { Data = new { code = 1, msg = "更新成功", data = goalModelList.Select(c => new { name = c.RightName, Id = c.Id, ParentId = c.ParentId }) } };
            }
            else
            {
                return new JsonResult() { Data = new { code = 1, msg = "更新成功" } };
            }

        }


        // 获得权限的详细信息
        public JsonResult GetRightDetail(int id)
        {

            var model = RightInfoMan.GetEntity(id);
            if (model == null)
            {
                return new JsonResult() { Data = new { code = 0, msg = "参数错误" } };
            }
            return new JsonResult() { Data = new { model.Description, model.FilePath, model.PicPath, model.IsView, model.IsPublic, model.OperateTypeEnum, model.RightType, model.Id } };
        }

        // 初始化编辑时候的下拉tree
        public ActionResult InitChoseRightTree(int id)
        {
            var list = RightInfoMan.GetEntityList(c => c.DataState == DataStateEnum.正常).ToList();


            var goalList = RightInfoMan.GetRightsOfOneRight(id).Select(c => c.Id).ToList();
            goalList.Add(id);
            //除去自己和自己的子级菜单
            list.RemoveAll(c => goalList.Contains(c.Id));

            var rightList =
                list.OrderBy(c => c.SortId).Select(c => new { name = c.RightName, Id = c.Id, ParentId = c.ParentId }).ToList();
            ViewBag.TreeData = System.Web.Helpers.Json.Encode(rightList);
            return View();

        }

    }
}