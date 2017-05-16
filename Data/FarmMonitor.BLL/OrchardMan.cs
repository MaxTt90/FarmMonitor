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
	 	//orchard
	public class OrchardMan
	{
	    private CustomDbContext db = CustomDbContext.Instance;

        public void Add(Orchard entity)
        {
            db.Orchards.Add(entity);
            db.SaveChanges();
        }
        public void Update(Orchard entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(Orchard entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            Orchard entity = this.GetEntity(id);
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public Orchard GetEntity(int id)
        {
            return db.Orchards.Find(id);
        }  
	}
}