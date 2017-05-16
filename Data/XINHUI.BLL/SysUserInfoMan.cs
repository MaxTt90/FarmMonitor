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
    //SysUserInfo
    public class SysUserInfoMan
    {
        	    private string dbContextType = "";
        private CustomDbContext _db = null;
        /// <summary>
        /// 构造函数,同一线程中使用相同的DBContext
        /// </summary>
        public SysUserInfoMan()
        {

        }

        /// <summary>
        /// 构造函数
        /// 如果要在当前实例中使用同一个DBContext,则使用带名称的DBContext
        /// </summary>
        /// <param name="contextName">上下文名称</param>
        public SysUserInfoMan(string contextName)
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

        public SysUserInfo Add(SysUserInfo entity)
        {
           var model= db.SysUserInfos.Add(entity);
            db.SaveChanges();
            return model;
        }
        public void Update(SysUserInfo entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(SysUserInfo entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            SysUserInfo entity = this.GetEntity(id);
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public SysUserInfo GetEntity(int id)
        {
            return db.SysUserInfos.Find(id);
        }

        

        /// <summary>
        /// 获得所有用户
        /// </summary>
        /// created:dxh 2015-10-29 09:36:14
        public IQueryable<SysUserInfo> GetAllUsers()
        {
            return db.SysUserInfos.Where(c => c.UserState !=UserStateEnum.删除);
        }
        /// <summary>
        /// 通过用户名获得model
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        public SysUserInfo GetModelByUsername(string name)
        {
            return db.SysUserInfos.FirstOrDefault(c => c.Name == name);
        }


        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="sum">总页数</param>
        /// <param name="pageSize">页容量</param>
        /// <returns></returns>
        /// author：duanxinaghai 2015-12-1 20:54:33
        public List<SysUserInfo> GetModelByPage(int pageIndex, out int sum, int pageSize = 10)
        {
            List<SysUserInfo> modelList = null;

            var totalCount = db.SysUserInfos.Count(c => c.UserState != UserStateEnum.删除);
            if (totalCount != 0)
            {
                modelList = db.SysUserInfos.Where(c => c.UserState != UserStateEnum.删除).OrderBy(c => c.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            if (totalCount % pageSize == 0)
            {
                sum = totalCount / pageSize;
            }
            else
            {
                sum = totalCount / pageSize + 1;
            }
            return modelList;
        }

        /// <summary>
        /// 通过用户代码获得model
        /// </summary>
        /// <param name="userCode">用户代码</param>
        /// <returns></returns>
        /// author: duanxianghai  2015-12-2 13:33:46
        public SysUserInfo GetModelByUserCode(string userCode)
        {
            return db.SysUserInfos.FirstOrDefault(c => c.UserCode == userCode);
        }

        /// <summary>
        /// 根据openId查找用户
        /// </summary> 
        /// <returns></returns>
        /// author:duanxianghai 2015-12-16 18:20:30
        public SysUserInfo GetUserByOpenId(string openId)
        {
            return db.SysUserInfos.FirstOrDefault(c => c.OpenId == openId);
        }

        /// <summary>
        /// 获得一个可用的成员Id
        /// </summary>
        /// <returns></returns>
        /// author:duanxianghai 2015-12-17 18:15:30
        public int GetNewUserId()
        {
            var n= db.SysUserInfos.Max(c => c.ID);
            n++;
            return n;
        }

        public IQueryable<SysUserInfo> GetList()
        {
            return db.SysUserInfos;
        }


        /// <summary>
        /// 分页和根据条件查询的结果
        /// </summary>   
        /// Author zhangbo update fredJiang update zhangsonghe
        /// CreateTime 2016-01-27 16:55
        public List<SysUserInfo> GetListByPage(string name,string mobile, int pageSize, int pageIndex, out int sum, out int totalCount)
        {
            sum = 0;
            totalCount=0;
            return new List<SysUserInfo>();
            //var predicate =  PredicateBuilder.True<SysUserInfo>();
            //predicate = predicate.And(w => w.UserState == UserStateEnum.在职);
          
            //if (!string.IsNullOrEmpty(name))
            //{
            //    predicate = predicate.And(w => w.Name.Contains(name));                
            //}
            //if (!string.IsNullOrEmpty(mobile))
            //{
            //    predicate = predicate.And(w => w.Mobile.Contains(mobile));
            //}

            //var tempList = db.SysUserInfos.Where(predicate);

            //totalCount = tempList.Count();
            //if (totalCount % pageSize == 0)
            //{
            //    sum = totalCount / pageSize;
            //}
            //else
            //{
            //    sum = totalCount / pageSize + 1;
            //}
            //tempList = tempList.OrderByDescending(m => m.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            //return tempList.ToList();

        }
        /// <summary>
        /// 返回该角色Id所有的用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<SysUserInfo> GetByRole(List<int> roleId)
        {
            var users = db.SysUserRoles.Where(x => roleId.Contains(x.RoleId)).Select(x=>x.SysUserInfo).ToList();
            return users;
        }
    }
}