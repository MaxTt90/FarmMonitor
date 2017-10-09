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
	 	//collect_data
	public class CollectDataMan
	{
	    private CustomDbContext db = CustomDbContext.Instance;

        //public void Add(CollectData entity)
        //{
        //    db.CollectDatas.Add(entity);
        //    db.SaveChanges();
        //}
        //public void Update(CollectData entity)
        //{
        //    db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        //    db.SaveChanges();
        //}
        //public void Delete(CollectData entity)
        //{
        //    db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
        //    db.SaveChanges();
        //}
        //public void Delete(int id)
        //{
        //    CollectData entity = this.GetEntity(id);
        //    db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
        //    db.SaveChanges();
        //}
        //public CollectData GetEntity(int id)
        //{
        //    return db.CollectDatas.Find(id);
        //}  
        /// <summary>
        /// 拉取
        /// </summary>
        /// <param name="collector"></param>
        /// <param name="sensor"></param>
        /// <returns></returns>
	    public List<CollectData> SearchByCollectorAndSensor(int collector, int sensor)
	    {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from collect_data left join  data_header on collect_data.data_id = data_header.data_id");
            strSql.Append(" where data_header.collector_id=" + collector);
            strSql.Append(" and collect_data.sensor_id=" + sensor);
            var ds=DbHelperMySQL.Query(strSql.ToString());
            return ConvertToList(ds);
	    }
        /// <summary>
        /// 拉取 
        /// </summary>
        /// <param name="collector"></param>
        /// <param name="sensor"></param>
        /// <returns></returns>
        public DataSet SearchAllByUserId(int userId,DateTime timeStart, DateTime timeEnd,int collector=0, int sensor=0)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select orchard.orchard_id as OrchardId,data_header.data_id as DataId,collect_data.data as Data,data_header.time as Time,collector.collector_id as CollectorId, "
                          + " sensor.sensor_id as SensorId,sensor.cname as SensorName,sensor.unit as Unit,sensor.resolution as Resolution,sensor.scope as Scope"
                          + " from collect_data left join  data_header on collect_data.data_id = data_header.data_id");
            strSql.Append(" left join  collector on collector.collector_id = data_header.collector_id");
            strSql.Append(" left join  orchard on collector.orchard_id = orchard.orchard_id");
            strSql.Append(" left join  user_orchard on orchard.orchard_id = user_orchard.Id");
            strSql.Append(" left join  sensor ON sensor.sensor_id = collect_data.sensor_id");
            strSql.Append(" where user_orchard.UserInfoId=" + userId);
            strSql.Append(" and data_header.msg_type=2");
            strSql.Append(" and data_header.time>'" + timeStart.ToString("yyyy-MM-dd HH:mm:ss")+"'");
            strSql.Append(" and data_header.time<'" + timeEnd.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (collector != 0)
            {
            strSql.Append(" and data_header.collector_id=" + collector);
            }
            if (sensor != 0)
            {
            strSql.Append(" and collect_data.sensor_id=" + sensor);
            }
            strSql.Append(" order by data_header.time desc limit 100000");
            var ds = DbHelperMySQL.Query(strSql.ToString());
            return ds;
        }
        public List<SensorDataModel> SearchList(int userId, DateTime timeStart, DateTime timeEnd, int collector = 0, int sensor = 0)
        {
            var dataset = SearchAllByUserId(userId, timeStart, timeEnd, collector, sensor);
            return DbHelperMySQL.DataSetToIList<SensorDataModel>(dataset, 0).ToList();
        }

        public List<CollectData> ConvertToList(DataSet ds)
	    {
	        List<CollectData> list = DbHelperMySQL.DataSetToIList<CollectData>(ds, 0).ToList();
	        return list;
	    }
         
	}
}