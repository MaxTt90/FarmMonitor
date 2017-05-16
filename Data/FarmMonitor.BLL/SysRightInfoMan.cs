using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using FarmMonitor.DAL;
using FarmMonitor.Model;
using System.Linq.Expressions;

namespace FarmMonitor.BLL
{
    //SysRightInfo
    public class SysRightInfoMan
    {
                       	    private string dbContextType = "";
        private CustomDbContext _db = null;
        /// <summary>
        /// 构造函数,同一线程中使用相同的DBContext
        /// </summary>
        public SysRightInfoMan()
        {

        }

        /// <summary>
        /// 构造函数
        /// 如果要在当前实例中使用同一个DBContext,则使用带名称的DBContext
        /// </summary>
        /// <param name="contextName">上下文名称</param>
        public SysRightInfoMan(string contextName)
        {
            dbContextType = "new";
        }
        
        private CustomDbContext db
        {
            get
            {
                if (dbContextType != "")
                {
                    //同一实例使用同一个DBContext;
                    if (_db == null)
                    {
                        _db = CustomDbContext.InstanceNew;
                    }

                    return _db;
                }
                else
                {
                    return CustomDbContext.Instance;
                }
            }
        }

        #region 基本方法
        public SysRightInfo Add(SysRightInfo entity)
        {
            var model = db.SysRightInfos.Add(entity);
            db.SaveChanges();
            return model;
        }
        public void Update(SysRightInfo entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(SysRightInfo entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            SysRightInfo entity = this.GetEntity(id);
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public SysRightInfo GetEntity(int id)
        {
            return db.SysRightInfos.Find(id);
        }
        #endregion

        /// <summary>
        /// 获得用户的关联角色权限和用户权限
        /// </summary>
        /// <param name="model">用户对象</param>
        /// <returns>权限集合</returns>
        /// Author:fredjiang
        /// Created:2015-10-28
        public IQueryable<SysRightInfo> GetListByUser(SysUserInfo model)
        {
           
            return db.SysRightInfos.Where(c => db.SysRoleRights.Where(o => o.UserId == model.ID || o.RoleId == model.Position || (db.SysUserRoles.Where(o2 => o2.UserId == model.ID).Select(s => s.RoleId).Contains(o.RoleId))).Select(s => s.RightId).Contains(c.Id) || c.IsPublic);
        }

        /// <summary>
        /// 条件查询
        /// </summary>      
        public IQueryable<SysRightInfo> GetEntityList(Expression<Func<SysRightInfo, bool>> where)
        {
            return db.SysRightInfos.Where(where);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public bool DeleteEntity(int id)
        {
            SysRightInfo model = new SysRightInfo() { Id = id };
            db.SysRightInfos.Attach(model);
            db.SysRightInfos.Remove(model);
            return db.SaveChanges() > 0;
        }


        /// <summary>
        /// 获得最大id的实体
        /// </summary>
        /// <returns></returns>
        public SysRightInfo GetMaxIdModel()
        {
            return db.SysRightInfos.OrderByDescending(c => c.Id).First();
        }

        /// <summary>
        /// 获得最大sortid的实体
        /// </summary>
        /// <returns></returns>
        public SysRightInfo GetMaxSortIdModel()
        {
            return db.SysRightInfos.OrderByDescending(c => c.SortId).First();
        }

        /// <summary>
        /// 获得子集权限菜单（二级）
        /// </summary>
        /// <param name="pid">父级id</param>
        /// <returns></returns>
        /// author:dxh 2015-11-17 13:52:54
        public List<SysRightInfo> GetSubMenu(int pid, List<SysRightInfo> list)
        {
            return list.Where(c => c.RightType == 1 && c.ParentId == pid).ToList();
        }
        /// <summary>
        /// 获得该权限下所有子级权限
        /// </summary>
        /// <param name="id">父级权限Id</param>
        ///author:duanxianghai 2015-12-4 11:49:47
        public List<SysRightInfo> GetRightsOfOneRight(int id)
        {
            var goalModelList = new List<SysRightInfo>();
            var list = GetEntityList(c => c.DataState == DataStateEnum.正常);
            foreach (var item in list)
            {
                if (item.ParentId == id)
                {
                    goalModelList.Add(item);
                    GetRightsOfOneRight(item.Id);
                }
            }
            return goalModelList;
        }

        /// <summary>
        /// 修改该权限及其子权限的状态
        /// </summary>
        /// <param name="id">权限id</param>
        /// <param name="state">要修改成的状态</param>
        public int ChangeState(int id, DataStateEnum state)
        {
            var model = GetEntity(id);
            if (model != null)
            {
                model.DataState = state;
            }

            var list = GetRightsOfOneRight(id);
            foreach (var r in list)
            {
                SysRightInfo m = new SysRightInfo() { Id = Convert.ToInt32(id), DataState = state };
                var entry = db.Entry(m);
                entry.State = EntityState.Unchanged;
                entry.Property("DataState").IsModified = true;
            }

            return db.SaveChanges();
        }
    }
}