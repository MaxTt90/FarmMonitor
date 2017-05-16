using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using FarmMonitor.DAL;

namespace FarmMonitor.BLL
{       
	 	//alarm_data
	public class AlarmDataMan
	{
	    private CustomDbContext db = CustomDbContext.Instance;

        //public void Add(AlarmData entity)
        //{
        //    db.AlarmDatas.Add(entity);
        //    db.SaveChanges();
        //}
        //public void Update(AlarmData entity)
        //{
        //    db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        //    db.SaveChanges();
        //}
        //public void Delete(AlarmData entity)
        //{
        //    db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
        //    db.SaveChanges();
        //}
        //public void Delete(int id)
        //{
        //    AlarmData entity = this.GetEntity(id);
        //    db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
        //    db.SaveChanges();
        //}
        //public AlarmData GetEntity(int id)
        //{
        //    return db.AlarmDatas.Find(id);
        //}  
	}
}