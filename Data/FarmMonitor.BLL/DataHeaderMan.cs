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
	 	//data_header
	public class DataHeaderMan
	{
	    private CustomDbContext db = CustomDbContext.Instance;

        public void Add(DataHeader entity)
        {
            db.DataHeaders.Add(entity);
            db.SaveChanges();
        }
        public void Update(DataHeader entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(DataHeader entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            DataHeader entity = this.GetEntity(id);
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public DataHeader GetEntity(int id)
        {
            return db.DataHeaders.Find(id);
        }  
	}
}