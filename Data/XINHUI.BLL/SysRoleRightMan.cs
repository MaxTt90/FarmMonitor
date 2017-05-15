using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FarmMonitor.DAL;
using FarmMonitor.Model;

namespace FarmMonitor.BLL
{
    public class SysRoleRightMan
    {
               	    private string dbContextType = "";
        private CustomDbContext _db = null;
        /// <summary>
        /// 构造函数,同一线程中使用相同的DBContext
        /// </summary>
        public SysRoleRightMan()
        {

        }

        /// <summary>
        /// 构造函数
        /// 如果要在当前实例中使用同一个DBContext,则使用带名称的DBContext
        /// </summary>
        /// <param name="contextName">上下文名称</param>
        public SysRoleRightMan(string contextName)
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

        public void Add(SysRoleRight entity)
        {
            db.SysRoleRights.Add(entity);
            db.SaveChanges();
        }
        public void Update(SysRoleRight entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(SysRoleRight entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            SysRoleRight entity = this.GetEntity(id);
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public SysRoleRight GetEntity(int id)
        {
            return db.SysRoleRights.Find(id);
        }
        /// <summary>
        /// 根据roleid删除对应的权限
        /// </summary>
        /// <param name="roleId">角色id</param>
        public int DeleteByRoleId(int roleId)
        {
            var list = db.SysRoleRights.Where(c => c.RoleId == roleId);
            foreach (var item in list)
            {
                db.Entry(item).State = EntityState.Deleted;
            }
            return db.SaveChanges();
        }

        /// <summary>
        /// 新增角色权限
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="rightIds">权限集合</param>
        /// <param name="createId">创建者id</param>
        /// <returns></returns>
        /// author:duanxianghai 2015-12-2 14:25:21
        public int AddRoleRightForRole(int roleId, List<int> rightIds, int createId = 0)
        {
            var pastRightId = db.SysRoleRights.Where(c => c.RoleId == roleId).Select(c => c.RightId);


            var addedRightId = rightIds.Where(c => !pastRightId.Contains(c)).ToList();

            var deleteRightId = pastRightId.Where(c => !rightIds.Contains(c)).ToList();

            List<SysRoleRight> list = new List<SysRoleRight>();
            //新增
            for (int i = 0; i < addedRightId.Count; i++)
            {
                SysRoleRight model = new SysRoleRight() { RoleId = roleId, CreateTime = DateTime.Now, RightId = addedRightId[i], CreateId = createId };
                list.Add(model);

            }

            db.SysRoleRights.AddRange(list);
            //删除
            var deleteList = db.SysRoleRights.Where(c => deleteRightId.Contains((int)c.RightId) && c.RoleId == roleId);
            db.SysRoleRights.RemoveRange(deleteList);
            var n = db.SaveChanges();


            //var r = db.SysRoleRights.Where(c => c.RoleId == roleId).Select(c => c.RightId);

            //var b = db.SysRightInfos.Where(c => !r.Contains(c.ParentId) && r.Contains(c.Id));
            //if (b.Any())
            //{
            //    list.Clear();
            //    foreach (var bb in b)
            //    {
            //        if (bb.ParentId == 0)
            //        {
            //            continue;
            //        }
            //        SysRoleRight model = new SysRoleRight() { RoleId = roleId, CreateTime = DateTime.Now, RightId = bb.ParentId, CreateId = createId };
            //        list.Add(model);

            //    }
            //    db.SysRoleRights.AddRange(list);
            //    n += db.SaveChanges();
            //}

            return n;
        }

        /// <summary>
        /// 新增角色权限
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="rightIds">权限集合</param>
        /// <param name="createId">创建者id</param>
        /// <returns></returns>
        /// author:duanxianghai 2015-12-2 14:28:37
        public int AddRoleRightForUser(int userId, List<int> rightIds, int createId = 0)
        {
            //已经具有的权限
            var pastRightId = db.SysRoleRights.Where(c => c.UserId == userId).Select(c => (int)c.RightId);

            //新选择的权限
            var addedRightId = rightIds.Where(c => !pastRightId.Contains(c)).ToList();
            //要删除的权限
            var deleteRightId = pastRightId.Where(c => !rightIds.Contains(c)).ToList();

            List<SysRoleRight> list = new List<SysRoleRight>();
            //新增
            for (int i = 0; i < addedRightId.Count; i++)
            {
                SysRoleRight model = new SysRoleRight() { UserId = userId, CreateTime = DateTime.Now, RightId = addedRightId[i], CreateId = createId };
                list.Add(model);

            }

            db.SysRoleRights.AddRange(list);
            //删除
            var deleteList = db.SysRoleRights.Where(c => deleteRightId.Contains((int)c.RightId) && c.UserId == userId);
            db.SysRoleRights.RemoveRange(deleteList);

            var n = db.SaveChanges();
            //var r = db.SysRoleRights.Where(c => c.RoleId == userId).Select(c => c.RightId);

            //var b = db.SysRightInfos.Where(c => !r.Contains(c.ParentId) && r.Contains(c.Id));
            //if (b.Any())
            //{
            //    list.Clear();
            //    foreach (var bb in b)
            //    {
            //        if (bb.ParentId == 0)
            //        {
            //            continue;
            //        }
            //        SysRoleRight model = new SysRoleRight() { RoleId = userId, CreateTime = DateTime.Now, RightId = bb.ParentId, CreateId = createId };
            //        list.Add(model);

            //    }
            //    db.SysRoleRights.AddRange(list);
            //    n += db.SaveChanges();
            //}

            return n;

        }
        public IQueryable<SysRoleRight> GetEntityList(Expression<Func<SysRoleRight, bool>> where)
        {
            return db.SysRoleRights.Where(where);
        }
    }
}
