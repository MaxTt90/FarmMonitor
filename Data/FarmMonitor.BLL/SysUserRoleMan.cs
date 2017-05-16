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
	public class SysUserRoleMan
	{
        private string dbContextType = "";
        private CustomDbContext _db = null;
        /// <summary>
        /// 构造函数,同一线程中使用相同的DBContext
        /// </summary>
        public SysUserRoleMan()
        {

        }

        /// <summary>
        /// 构造函数
        /// 如果要在当前实例中使用同一个DBContext,则使用带名称的DBContext
        /// </summary>
        /// <param name="contextName">上下文名称</param>
        public SysUserRoleMan(string contextName)
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


        public void Add(SysUserRole entity)
        {
            db.SysUserRoles.Add(entity);
            db.SaveChanges();
        }
        public void Update(SysUserRole entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(SysUserRole entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            SysUserRole entity = this.GetEntity(id);
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public SysUserRole GetEntity(int id)
        {
            return db.SysUserRoles.Find(id);
        }

        /// <summary>
        /// 通过userid获取用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// hyp2015-11-16
        public SysUserRole GetModelByUserId(int userId)
        {
            SysUserRole sysUserRole;
            sysUserRole = db.SysUserRoles.Where(m => m.UserId == userId).First();
            return sysUserRole;
        }


	}
}