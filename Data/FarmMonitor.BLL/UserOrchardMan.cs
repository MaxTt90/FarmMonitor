using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using FarmMonitor.DAL;
using FarmMonitor.Model;

namespace FarmMonitor.BLL
{
    //SysUserRole
    public class UserOrchardMan
    {
        private string dbContextType = "";
        private CustomDbContext _db = null;
        /// <summary>
        /// 构造函数,同一线程中使用相同的DBContext
        /// </summary>
        public UserOrchardMan()
        {

        }

        /// <summary>
        /// 构造函数
        /// 如果要在当前实例中使用同一个DBContext,则使用带名称的DBContext
        /// </summary>
        /// <param name="contextName">上下文名称</param>
        public UserOrchardMan(string contextName)
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


        public void Add(UserOrchard entity)
        {
            db.UserOrchards.Add(entity);
            db.SaveChanges();
        }
        public void Update(UserOrchard entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(UserOrchard entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            UserOrchard entity = this.GetEntity(id);
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public UserOrchard GetEntity(int id)
        {
            return db.UserOrchards.Find(id);
        }

        /// <summary>
        /// 通过userid获取用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Orchard> GetListByUserId(int userId)
        {
            return db.UserOrchards.Where(x => x.UserInfoId == userId).Select(x => x.Orchard).ToList();
        }


    }
}