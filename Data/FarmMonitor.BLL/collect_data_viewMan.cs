using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AOWEN.DAL;
using AOWEN.Model;

namespace AOWEN.BLL
{       
	 	//collect_data_view
	public class collect_data_viewMan
	{
	    private CustomDbContext db = CustomDbContext.Instance;

        public void Add(collect_data_view entity)
        {
            db.collect_data_views.Add(entity);
            db.SaveChanges();
        }
        public void Update(collect_data_view entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(collect_data_view entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            collect_data_view entity = this.GetEntity(id);
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public collect_data_view GetEntity(int id)
        {    
             return db.collect_data_views.Find(id);
        }  
	}
}