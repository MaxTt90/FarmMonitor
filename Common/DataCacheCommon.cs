using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace FarmMonitor.Common
{
    public class DataCacheCommon
    {
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[CacheKey];
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject);
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }


        /// <summary>
        /// 保存缓存信息，绝对过期时间
        /// </summary>
        /// <param name="key">变量名</param>
        /// <param name="value">值</param>
        /// <param name="cacheTime">缓存时间(分钟)</param>
        public static void SaveCacheValue(string key, object value, int cacheTime)
        {
            HttpRuntime.Cache.Insert(key, value, null, DateTime.Now.AddMinutes(cacheTime), TimeSpan.Zero);
        }

        /// <summary>
        /// 保存缓存信息，绝对过期时间
        /// </summary>
        /// <param name="key">变量名</param>
        /// <param name="value">值</param>
        /// <param name="cacheTime">缓存时间(分钟)</param>
        /// <param name="callbackFunction">缓存过期时执行的委托</param>
        public static void SaveCacheValue(string key, object value, int cacheTime, CacheItemRemovedCallback callbackFunction)
        {
            HttpRuntime.Cache.Insert(key, value, null, DateTime.Now.AddMinutes(cacheTime), TimeSpan.Zero, CacheItemPriority.High, callbackFunction);
        }

        /// <summary>
        /// 保存缓存信息,相对过期时间
        /// </summary>
        /// <param name="key">变量名</param>
        /// <param name="value">值</param>
        /// <param name="cacheTime">缓存时间(分钟)</param>
        public static void SaveCacheValue(string key, object value, double cacheTime)
        {
            HttpRuntime.Cache.Insert(key, value, null, DateTime.MaxValue, TimeSpan.FromMinutes(cacheTime));
        }

        /// <summary>
        /// 保存缓存信息,相对过期时间
        /// </summary>
        /// <param name="key">变量名</param>
        /// <param name="value">值</param>
        /// <param name="cacheTime">缓存时间(分钟)</param>
        /// <param name="callbackFunction">缓存过期时执行的委托</param>
        public static void SaveCacheValue(string key, object value, double cacheTime, CacheItemRemovedCallback callbackFunction)
        {
            HttpRuntime.Cache.Insert(key, value, null, DateTime.MaxValue, TimeSpan.FromMinutes(cacheTime), CacheItemPriority.High, callbackFunction);
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        public static void RemoveCache(string cacheKey)
        {
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                HttpRuntime.Cache.Remove(cacheKey);
            }
        }

        /// <summary>
        /// 获取缓存信息
        /// </summary>
        /// <param name="key">缓存名称</param>
        /// <returns>缓存对象</returns>
        /// Author FredJiang
        /// Create Date 2013年6月17日
        public static object GetCacheValue(string key)
        {
            if (HttpRuntime.Cache[key] != null)
            {
                return HttpRuntime.Cache[key];
            }
            else
            {
                return null;
            }

        }

    }
}
