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
	 	//collector
	public class CollectorMan
	{
	    private CustomDbContext db = CustomDbContext.Instance;

        public void Add(Collector entity)
        {
            db.Collectors.Add(entity);
            db.SaveChanges();
        }
        public void Update(Collector entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(Collector entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            Collector entity = this.GetEntity(id);
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public Collector GetEntity(int id)
        {
            return db.Collectors.Find(id);
        }
        /// <summary>
        /// 根据果园Id返回采集器Id
        /// </summary>
        /// <param name="orchardId"></param>
        /// <returns></returns>
	    public List<Collector> GetByOrchard(List<int> orchardId)
	    {
	        return db.Collectors.Where(x => orchardId.Contains(x.OrchardId)).OrderBy(x=>x.OrchardCollectorId).ToList();
	    } 
	}
}