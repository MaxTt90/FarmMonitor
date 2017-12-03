using System.Collections.Generic;
using FarmMonitor.Common;

namespace FarmMonitor.Web.AppCode
{
    public class XmlTools
    {
        /// <summary>
        /// 获得目录查询消息分类1级编号
        /// </summary>
        /// <returns>目录查询分类编号</returns>
        /// Author:fredjiang
        /// Created:2016-02-17
        public static int GetDirectoryQueryClassifyId()
        {
            XmlCommon xc = new XmlCommon();
            string nodeName = "root/messageInfo/messageClassifyDirectoryId";
            return xc.GetXmlNodeToInt(nodeName);
        }

        /// <summary>
        /// 坐席服务一天中的开始时间点
        /// </summary>
        /// <returns>时间:分</returns>
        /// Author:fredjiang
        /// Created:2016-02-23
        public static List<string> GetCustomerServiceStartTime()
        {
            XmlCommon xc = new XmlCommon();
            string nodeName = "root/customerService/serviceTime/dayTime/startTime";
            return xc.GetNodesText(nodeName);
        }

        /// <summary>
        /// 坐席服务一天中的结束时间点
        /// </summary>
        /// <returns>时间:分</returns>
        /// Author:fredjiang
        /// Created:2016-02-23
        public static List<string> GetCustomerServiceEndTime()
        {
            XmlCommon xc = new XmlCommon();
            string nodeName = "root/customerService/serviceTime/dayTime/endTime";
            return xc.GetNodesText(nodeName);
        }

        /// <summary>
        /// 从配置文件获得粉丝空闲等待时间
        /// </summary>
        /// <returns></returns>
        /// Author:fredjiang
        /// Created:2016-02-23
        public static int GetCustomerServiceWaitingTime()
        {
            XmlCommon xc = new XmlCommon();
            string nodeName = "root/customerService/waitingTime";
            return xc.GetXmlNodeToInt(nodeName);
        }

        /// <summary>
        /// 获得坐席服务一周中那几天
        /// </summary>
        /// <returns>周中的1，2，3，4，5....</returns>
        /// Author:fredjiang
        /// Created:2016-02-23
        public static List<int> GetCustomerServiceWeekDay()
        {
            XmlCommon xc = new XmlCommon();
            string nodeName = "root/customerService/serviceTime/weekDay";
            return xc.GetIntListByNodeName(nodeName);
        }

        public static Dictionary<string, string> GetReportDesc()
        {
            XmlCommon xc = new XmlCommon();
            string nodeName = "root/Report/Item";
            return xc.GetNodes(nodeName);
        }

        /// <summary>
        /// 获得会员对应的属性选项列表
        /// </summary>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public static List<string> GetCustomerAttributeList(string attrName)
        {
            string key = "root_CustomerAttribute_" + attrName;
            var data = DataCacheCommon.GetCache(key) as List<string>;
            if (data == null)
            {
                XmlCommon xc = new XmlCommon();
                string nodeName = "root/CustomerAttribute/" + attrName;
                data = xc.GetNodesText(nodeName);
                DataCacheCommon.SaveCacheValue(key, data, 30);
            }

            return data;
        }

        /// <summary>
        /// 获得会员对应的Key_Value属性选项列表
        /// </summary>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetCustomerKeyValueAttributeList(string attrName)
        {
            string key = "root_CustomerAttribute_" + attrName;
            var data = DataCacheCommon.GetCache(key) as Dictionary<string, string>;
            if (data == null)
            {
                XmlCommon xc = new XmlCommon();
                string nodeName = "root/CustomerAttribute/" + attrName;
                data = xc.GetNodesKeyValueText(nodeName);
                DataCacheCommon.SaveCacheValue(key, data, 30);
            }
            
            return data;
        }
        /// <summary>
        /// 获得日报导入的列表名列表
        /// </summary>
        /// <returns>键值对集合</returns>
        public static Dictionary<string, string> GetMReportKeyValueList(string attrName)
        {
            XmlCommon xc = new XmlCommon();
            return xc.GetKeyValue("root/DataImport/" + attrName);
        }
    }
}