using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FarmMonitor.BLL;
using FarmMonitor.Common;
using FarmMonitor.Web.AppCode.Filter;
using FarmMonitor.Model;     

namespace FarmMonitor.Web.Controllers
{
    public class LoginController : Controller
    {
        [UserInfoAuthFilter]
        public ActionResult Index()
        {
            var menuInfo = Startup.GetUserRightInfoList(false).FindAll(o => o.RightType == 1 && o.ParentId == 0).OrderBy(o => o.SortId).FirstOrDefault();
                return RedirectToAction("MenuList", new {mid = menuInfo.Id});
        }

        public ActionResult Login()
        {
            if (Startup.GetUserInfo() != null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string passWord, string imgCode)
        {

            if (string.IsNullOrWhiteSpace(userName))
            {
                return Json(new { Msg = "请输入用户名", code = 400 });
            }
            if (string.IsNullOrEmpty(passWord))
            {
                return Json(new { Msg = "请输入密码", code = 401 });

            }
            if (imgCode != "1111")//添加万能验证码方便自动化测试
            {
                if (string.IsNullOrEmpty(imgCode) || Session["img_vcode"] == null || imgCode.ToLower() != Session["img_vcode"].ToString().ToLower())
                {
                    return Json(new { Msg = "验证码输入错误", code = 402 });
                }
            }
            
            var userMan = new SysUserInfoMan();

            var user = userMan.GetModelByUserName(userName);
            if (user == null)
            {
                return Json(new { Msg = "用户名或密码错误", code = 403 });
            }
            if (user.Password != EncryptCommon.EncryptMD5(passWord))
            {
                return Json(new { Msg = "用户名或密码错误", code = 404 });

            }

            Startup.SaveUserInfo(user);
            //return View("Index");
            return Json(new { Msg = "登陆成功", code = 200 });

        }

        /// <summary>
        /// 注销
        /// </summary>
        public ActionResult LogOut()
        {
            Startup.RemoveUserInfo();
            return Redirect("login");
        }

        //没有权限
        public ActionResult NoRight()
        {
            return View();
        }

        [UserInfoAuthFilter]
        public ActionResult MenuList(int mid = 0)
        {
            var model = new SysRightInfoMan().GetEntity(mid);
            if (mid > 0)
            {

                List<SysRightInfo> list = Startup.GetUserRightInfoList(false).FindAll(o => o.RightType == 1 && o.ParentId == mid && o.DataState != DataStateEnum.删除);
                if (list.Count > 0)
                {

                    ViewBag.Name = model.RightName;
                    return View(list);
                }
            }
            ViewBag.Name = model.RightName;
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        /// Author:fredjiang
        /// Created:2016-01-18
        [UserInfoAuthFilter]
        [HttpGet]
        public ActionResult ModifyPassword()
        {
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        /// Author:fredjiang
        /// Created:2016-01-18
        [UserInfoAuthFilter]
        [HttpPost]
        public ActionResult ModifyPassword(string oldPwd, string newPwd, string confirmPwd)
        {
            SysUserInfo ui = Startup.GetUserInfo();
            string code = "";
            string msg = "";
            EncryptCommon EncryptCommon = new EncryptCommon();
            if (EncryptCommon.EncryptMD5(oldPwd) == ui.Password)
            {
                if (newPwd == confirmPwd)
                {
                    SysUserInfoMan uiMan = new SysUserInfoMan();
                    SysUserInfo user = uiMan.GetEntity(ui.ID);
                    user.Password = EncryptCommon.EncryptMD5(newPwd);
                    user.UpdateTime = DateTime.Now;
                    ui.Password = user.Password;
                    uiMan.Update(user);
                    code = "1";
                    msg = "修改成功";
                }
                else
                {
                    code = "2";
                    msg = "重设密码与确认密码输入不一致！";
                }
            }
            else
            {
                code = "2";
                msg = "原始密码输入错误！";
            }
            ViewBag.code = code;
            ViewBag.msg = msg;
            return View();
        }
    }
}