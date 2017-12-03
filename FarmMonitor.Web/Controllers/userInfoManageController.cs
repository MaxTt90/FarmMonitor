using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AOWEN.BLL;
using AOWEN.Model;
using AOWEN.Common;
using AOWEN.Web.AppCode.Filter;

namespace AOWEN.Web.Controllers
{
    public class UserInfoManageController : Controller
    {
        //
        // GET: /UserInfoManage/
        SysUserInfoMan userinfoman = new SysUserInfoMan();
        SysUserInfo userinfo = new SysUserInfo();

        [UserInfoAuthFilter]
        public ActionResult Index()
        {
            var userInfoList = userinfoman.GetAllUsers().ToList();
            ViewBag.UserInfoList = userInfoList;
            return View();
        }

        [UserInfoAuthFilter]
        [HttpGet]
        public ActionResult Add()
        {
            SysRoleInfoMan roleinfoman = new SysRoleInfoMan();
            var roleinfoList = roleinfoman.GetAllRolesInfo().ToList();
            ViewBag.RoleInfoList = roleinfoList;
            return View();
        }

        [UserInfoAuthFilter]
        [HttpPost]
        public ActionResult Add(AOWEN.Model.SysUserInfo model, string roleinfoName)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return Content("<script>alert('名字不能为空');window.location.href='/UserInfoManage/Add/'</script>");
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                return Content("<script>alert('密码不能为空';window.location.href='/UserInfoManage/Add/')</script>");
            }
            if (string.IsNullOrEmpty(model.Tel) && string.IsNullOrEmpty(model.Mobile))
            {
                return Content("<script>alert('电话和手机不能为空');window.location.href='/UserInfoManage/Add/'</script>");
            }
            if (string.IsNullOrEmpty(model.Province.ToString()) || string.IsNullOrEmpty(model.City.ToString()))
            {
                return Content("<script>alert('省份城市不能为空');window.location.href='/UserInfoManage/Add/'</script>");
            }

            #region 增加用户信息
            userinfo.Name = model.Name;
            userinfo.Password = EncryptCommon.EncryptMD5(model.Password);
            userinfo.UserCode = model.UserCode;
            userinfo.Sex = model.Sex;
            userinfo.Tel = model.Tel == null ? "" : model.Tel;
            userinfo.Birthday = model.Birthday;
            userinfo.Province = model.Province;
            userinfo.City = model.City;
            userinfo.Mobile = model.Mobile == null ? "" : model.Mobile;
            userinfo.Email = model.Email == null ? "" : model.Email;
            userinfo.UserState = model.UserState;
            userinfo.Remark = model.Remark == null ? "" : model.Remark;
            userinfo.CreateDate = DateTime.Now;
            userinfoman.Add(userinfo);
            #endregion

            #region 增加用户角色关联表
            SysUserRoleMan userroleman = new SysUserRoleMan();
            SysUserRole userrole = new SysUserRole();
            userrole.RoleId = Convert.ToInt32(roleinfoName);
            userrole.UserId = userinfo.ID;
            userrole.CreateTime = DateTime.Now;
            userroleman.Add(userrole);
            #endregion
            return Content("<script>alert('增加成功');window.location.href='/UserInfoManage/Index/'</script>");

        }

        [UserInfoAuthFilter]
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var userinfoModel = userinfoman.GetEntity(ID);
            #region 省市数据
            SysCityMan cityMan = new SysCityMan();
            var provinceList = cityMan.GetProvinceByCityLevel(1).ToList();
            var cityList = cityMan.GetCityByProvinceIdAndCityLevel(2, userinfoModel.Province).ToList();
            ViewBag.province = provinceList;
            ViewBag.city = cityList;
            #endregion
            #region 用户角色关联表数据
            SysUserRoleMan userroleman = new SysUserRoleMan();
            var userrole = userroleman.GetModelByUserId(ID);
            ViewBag.UserRole = userrole;
            #endregion
            #region 角色数据
            SysRoleInfoMan roleinfoman = new SysRoleInfoMan();
            var roleinfoList = roleinfoman.GetAllRolesInfo().ToList();
            ViewBag.RoleInfoList = roleinfoList;
            #endregion

            return View(userinfoModel);
        }

        [UserInfoAuthFilter]
        public ActionResult Edit(AOWEN.Model.SysUserInfo model, string roleinfoName)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return Content("<script>alert('名字不能为空');window.location.href='/UserInfoManage/Edit/@model.ID'</script>");
            }
            //if (string.IsNullOrEmpty(model.Password))
            //{
            //    return Content("<script>alert('密码不能为空';window.location.href='/UserInfoManage/Edit/@model.ID')</script>");
            //}
            if (string.IsNullOrEmpty(model.Tel) && string.IsNullOrEmpty(model.Mobile))
            {
                return Content("<script>alert('电话和手机不能为空');window.location.href='/UserInfoManage/Edit/@model.ID'</script>");
            }
            if (string.IsNullOrEmpty(model.Province.ToString()) || string.IsNullOrEmpty(model.City.ToString()))
            {
                return Content("<script>alert('省份城市不能为空');window.location.href='/UserInfoManage/Add/'</script>");
            }
            var userInfoModel = userinfoman.GetEntity(model.ID);

            #region 用户信息更新
            userInfoModel.Name = model.Name;
            userInfoModel.UserCode = model.UserCode == null ? "" : model.UserCode;
            userInfoModel.Sex = model.Sex;
            userInfoModel.Birthday = model.Birthday;
            userInfoModel.Tel = model.Tel == null ? "" : model.Tel;
            userInfoModel.Province = model.Province;
            userInfoModel.City = model.City;
            userInfoModel.Mobile = model.Mobile == null ? "" : model.Mobile;
            userInfoModel.Email = model.Email == null ? "" : model.Email;
            userInfoModel.UserState = model.UserState;
            userInfoModel.Remark = model.Remark == null ? "" : model.Remark;
            if (model.Password != "")
            {
                userInfoModel.Password = model.Password;
            }
            userinfoman.Update(userInfoModel);
            #endregion

            #region 更新用户角色关联表
            SysUserRoleMan userroleman = new SysUserRoleMan();
            var userroleModel = userroleman.GetModelByUserId(model.ID);
            userroleModel.RoleId = Convert.ToInt32(roleinfoName);
            userroleModel.UserId = model.ID;
            userroleModel.UpdateTime = DateTime.Now;
            userroleman.Update(userroleModel);
            #endregion
            return Content("<script>alert('更新成功');window.location.href='/UserInfoManage/Index/'</script>");

        }

        [UserInfoAuthFilter]
        public ActionResult Delete(int ID = 0)
        {
            var userinfoModel = userinfoman.GetEntity(ID);
            userinfoModel.UserState = UserStateEnum.删除;
            userinfoman.Update(userinfoModel);
            return Redirect("/UserInfoManage/Index");
        }
        /// <summary>
        /// 得到省份
        /// </summary>       
        public ActionResult ProvinceLoad()
        {
            SysCityMan cityMan = new SysCityMan();
            var provList = cityMan.GetProvinceByCityLevel(1);
            return new JsonResult() { Data = provList.Select(c => new { c.Name, c.ProvinceId, c.CityId, c.Id }), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        /// <summary>
        /// 得到城市
        /// </summary>
        /// <param name="provId">一级城市编号id</param>      
        public ActionResult CityLoad(int provId)
        {

            SysCityMan cityMan = new SysCityMan();
            var cityList = cityMan.GetCityByProvinceIdAndCityLevel(2, provId);
            return new JsonResult() { Data = cityList.Select(c => new { c.Name, c.ProvinceId, c.CityId, c.Id }), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}