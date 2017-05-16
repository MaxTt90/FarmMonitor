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
	 	//sensor
	public class SensorMan
	{
	    private CustomDbContext db = CustomDbContext.Instance;

        public void Add(Sensor entity)
        {
            db.Sensors.Add(entity);
            db.SaveChanges();
        }
        public void Update(Sensor entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(Sensor entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            Sensor entity = this.GetEntity(id);
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public Sensor GetEntity(int id)
        {
            return db.Sensors.Find(id);
        }

	    public IQueryable<Sensor> GetAll()
	    {
	        return db.Sensors;
	    } 
	}
}