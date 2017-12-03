using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace FarmMonitor.Common
{
    public class SerializationCommon
    {
        #region JSON
        /// <summary>
        /// 序列化为Json
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">序列化的对象</param>
        /// <returns>序列化后的Json字符串</returns>
        /// Author:fredjiang
        /// Created:2015-12-20
        public string JsonSerialize<T>(T t)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(t);
            return json;
        }

        /// <summary>
        /// JSON反序列化为对象
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="json">JSON字符串</param>
        /// <returns>反序化后对象</returns>
        /// Author:fredjiang
        /// Created:2015-12-20
        public T JsonDeserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> dic = (Dictionary<string, object>)jss.DeserializeObject(json);
            foreach (System.Reflection.PropertyInfo pi in typeof(T).GetProperties())
            {
                foreach (string k in dic.Keys)
                {
                    if (k.ToLower() == pi.Name.ToLower())
                    {
                        obj.GetType().GetProperty(pi.Name).SetValue(obj, dic[k], null);
                        break;
                    }
                }
            }
            return obj;
        }

        /// <summary>
        /// JSON反序列化为对象
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="json">JSON字符串</param>
        /// <returns>反序化后对象</returns>
        /// Author:fredjiang
        /// Created:2015-12-20
        public T JsonDeserialize2<T>(string json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(json);
        }
        #endregion


        #region xml

        #endregion
    }
}
