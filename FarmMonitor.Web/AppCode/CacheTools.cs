using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Caching;
using FarmMonitor.Model;
using FarmMonitor.BLL;
using FarmMonitor.Common;
using FarmMonitor.Web.Models;
using WeDo.Log;

namespace FarmMonitor.Web.AppCode
{
    public class CacheTools
    {


        static object LockMenuListCache = new object(); //公众号菜单缓存锁
        static object LockStateModelCache = new object(); //微信请求对象缓存锁
        static object LockMessageMappingCache = new object(); //系统回复功能消息回复缓存锁
        static object LockKnowledgeListCache = new object(); //知识库缓存锁
        static object LockDirectoryQueryListCache = new object(); //目录查询消息分类缓存锁
        static object LockDirectoryQueryMessageListCache = new object(); //目录查询消息列表缓存锁
        static object LockCustomerServiceConfigCache = new object(); //人工客服配置信息缓存锁
        static object LockDirectoryMessageClassify = new object(); //目录查询对应分类编号


        #region 客户业务缓存

        #endregion

        #region 常用对象缓存
        /// <summary>
        /// 城市表
        /// </summary>
        /// <param name="mr"></param>
        /// <param name="reset"></param>
        /// <returns></returns>
        public static List<SysCity> GetCityList(bool reset)
        {
            string key = "sysCityCache";
            List<SysCity> list = new List<SysCity>();
            if (DataCacheCommon.GetCacheValue(key) == null || reset)
            {
                SysCityMan scMan = new SysCityMan();
                list = scMan.GetList();
                DataCacheCommon.SaveCacheValue(key, list, Convert.ToDouble(30));
            }
            else
            {
                list = (List<SysCity>)DataCacheCommon.GetCacheValue(key);
            }
            return list;
        }

        /// <summary>
        /// 城市表Id找名字
        /// </summary>
        /// <param name="mr"></param>
        /// <param name="reset"></param>
        /// <returns></returns>
        public static string GetNameByCityId(int id, bool reset = false)
        {
            string key = "sysCity_" + id.ToString();
            string name = "";
            if (DataCacheCommon.GetCacheValue(key) == null || reset)
            {
                name = GetCityList(reset).Where(c => c.Id == id).FirstOrDefault().Name;
                DataCacheCommon.SaveCacheValue(key, name, Convert.ToDouble(30));
            }
            else
            {
                name = (string)DataCacheCommon.GetCacheValue(key);
            }
            return name;
        }

        #endregion

        #region 配置文件内容缓存
        /// <summary>
        /// 从缓存中获得人工客服一天中开始时间
        /// </summary>
        /// <param name="reset">是否重置</param>
        /// <returns>开始时间列表</returns>
        /// Author:fredjiang
        /// Created:2016-02-23
        public static List<string> GetCustomerServiceStartTime(bool reset)
        {
            lock (LockCustomerServiceConfigCache)
            {
                string key = "customer_service_start_time";
                List<string> list = new List<string>();
                if (DataCacheCommon.GetCacheValue(key) == null || reset)
                {
                    list = XmlTools.GetCustomerServiceStartTime();
                    if (list.Count > 0)
                    {
                        DataCacheCommon.SaveCacheValue(key, list, 30);
                    }
                }
                else
                {
                    list = (List<string>)DataCacheCommon.GetCacheValue(key);
                }
                return list;
            }
        }

        /// <summary>
        /// 从缓存中获得人工客服一天中结束时间
        /// </summary>
        /// <param name="reset">是否重置</param>
        /// <returns>结束时间列表</returns>
        /// Author:fredjiang
        /// Created:2016-02-23
        public static List<string> GetCustomerServiceEndTime(bool reset)
        {
            lock (LockCustomerServiceConfigCache)
            {
                string key = "customer_service_end_time";
                List<string> list = new List<string>();
                if (DataCacheCommon.GetCacheValue(key) == null || reset)
                {
                    list = XmlTools.GetCustomerServiceEndTime();
                    if (list.Count > 0)
                    {
                        DataCacheCommon.SaveCacheValue(key, list, 30);
                    }
                }
                else
                {
                    list = (List<string>)DataCacheCommon.GetCacheValue(key);
                }
                return list;
            }
        }

        /// <summary>
        /// 从缓存中获得人工客服一周中服务的天
        /// </summary>
        /// <param name="reset">是否重置</param>
        /// <returns>周的天列表</returns>
        /// Author:fredjiang
        /// Created:2016-02-23
        public static List<int> GetCustomerServiceWeekDay(bool reset)
        {
            lock (LockCustomerServiceConfigCache)
            {
                string key = "customer_service_week_day";
                List<int> list = new List<int>();
                if (DataCacheCommon.GetCacheValue(key) == null || reset)
                {
                    list = XmlTools.GetCustomerServiceWeekDay();
                    if (list.Count > 0)
                    {
                        DataCacheCommon.SaveCacheValue(key, list, 30);
                    }
                }
                else
                {
                    list = (List<int>)DataCacheCommon.GetCacheValue(key);
                }
                return list;
            }
        }

        /// <summary>
        /// 从缓存中获得人工客服聊天空闲时间
        /// </summary>
        /// <param name="reset">是否重置</param>
        /// <returns>空闲时间(分钟)</returns>
        /// Author:fredjiang
        /// Created:2016-02-23
        public static int GetCustomerServiceWaitingTime(bool reset)
        {
            lock (LockCustomerServiceConfigCache)
            {
                string key = "customer_service_waiting_time";
                int result = 0;
                if (DataCacheCommon.GetCacheValue(key) == null || reset)
                {
                    result = XmlTools.GetCustomerServiceWaitingTime();
                    if (result <= 0)
                    {
                        result = 5;
                    }
                    DataCacheCommon.SaveCacheValue(key, result, 30);
                }
                else
                {
                    result = (int)DataCacheCommon.GetCacheValue(key);
                }
                return result;
            }
        }
        #endregion



    }
}