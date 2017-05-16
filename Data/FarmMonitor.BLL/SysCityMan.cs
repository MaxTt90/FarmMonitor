using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using FarmMonitor.DAL;
using FarmMonitor.Model;
//using FarmMonitor.Model;

namespace FarmMonitor.BLL
{
    //SysCity
    public class SysCityMan
    {
                	    private string dbContextType = "";
        private CustomDbContext _db = null;
        /// <summary>
        /// 构造函数,同一线程中使用相同的DBContext
        /// </summary>
        public SysCityMan()
        {

        }

        /// <summary>
        /// 构造函数
        /// 如果要在当前实例中使用同一个DBContext,则使用带名称的DBContext
        /// </summary>
        /// <param name="contextName">上下文名称</param>
        public SysCityMan(string contextName)
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

        public void Add(SysCity entity)
        {
            db.SysCitys.Add(entity);
            db.SaveChanges();
        }
        public void Update(SysCity entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(SysCity entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            SysCity entity = this.GetEntity(id);
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public SysCity GetEntity(int id)
        {
            return db.SysCitys.Find(id);
        }

        /// <summary>
        /// 通过级别获得省份
        /// </summary>
        /// <param name="CityLevel">级别【1代表省份，2代表城市，3代表县区】</param>
        /// <returns></returns>
        /// createTime zb 2015-11-20
        public List<SysCity> GetProvinceByCityLevel(int CityLevel)
        {
            return db.SysCitys.Where(c => c.CityLevel == CityLevel).ToList();
        }        

        /// <summary>
        /// 通过级别和省份Id获得城市
        /// </summary>
        /// <param name="CityLevel">级别【1代表省份，2代表城市，3代表县区】</param>
        /// <param name="ProvinceId">省份Id</param>
        /// <returns></returns>
        ///  createTime zb 2015-11-20
        public List<SysCity> GetCityByProvinceIdAndCityLevel(int CityLevel, int ProvinceId)
        {
            return db.SysCitys.Where(c => c.CityLevel == CityLevel && c.ParentId==ProvinceId).ToList();
        }

        /// <summary>
        /// 通过级别和省份Id获得城市
        /// </summary>
        /// <param name="CityLevel">级别【1代表省份，2代表城市，3代表县区】</param>
        /// <param name="provinceName">省份Id</param>
        /// <returns></returns>
        ///  createTime zb 2015-11-20
        public List<SysCity> GetCityByProvinceIdAndCityLevel(int CityLevel, string provinceName)
        {
            var province = db.SysCitys.FirstOrDefault(q => q.CityLevel == 1 && q.ShortName == provinceName);
            if (province != null)
            {
                return db.SysCitys.Where(c => c.CityLevel == CityLevel && c.ParentId == province.Id).ToList();
            }
            return new List<SysCity>();
        }
     
        public string GetNameById(int id)
        {
           string name="";
           SysCity city = GetEntity(id);
           if (city != null)
               name = city.Name;
           return name;
        }

        /// <summary>
        /// 获取城市SysCitys表的所有正常数据
        /// </summary>
        /// <returns></returns>
        public List<SysCity> GetList()
        {
            return db.SysCitys.Where(c => c.DataState == (int)DataStateEnum.正常).ToList();
        }

        /// <summary>
        /// 获得所有城市信息
        /// </summary>
        /// <returns></returns>
        /// zb 2015-11-25 19:58
        public IQueryable<SysCity> GetAllCity()
        {
            return db.SysCitys.Where(c => true);
        }
        /// <summary>
        /// 获得city的省份model
        /// </summary>
        /// <param name="id">city id</param>
        /// <returns></returns>
        /// duanxianghai 2015-12-2 09:08:01
        public SysCity GetProvinceByCityId(int id)
        {
            var model = GetEntity(id);
            if (model==null)
            {
                return null;
            }
            var province = GetEntity(model.ProvinceId);
            return province;
        }

        public SysCity GetCityByName(string city)
        {
            return db.SysCitys.FirstOrDefault(c => c.ShortName == city && c.CityLevel == 2||c.CityLevel==3);
        }
        public SysCity GetProvinceByName(string province)
        {
            return db.SysCitys.FirstOrDefault(c => c.ShortName == province && c.CityLevel == 1);
        }
    }
}