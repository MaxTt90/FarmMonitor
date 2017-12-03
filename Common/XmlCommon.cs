using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace FarmMonitor.Common
{
    public class XmlCommon
    {
        XmlDocument pdoc = new XmlDocument();
        string xmlPath = "";
        /// <summary>
        /// 1表示加载xxxx文档
        /// </summary>
        /// <param name="fileIndex">文件编号</param>
        public XmlCommon(int fileIndex)
        {
            if (fileIndex == 1)
            {
                xmlPath = HttpRuntime.AppDomainAppPath + "\\Files\\Config\\BaseConfig.xml";
                pdoc.Load(xmlPath);
            }
        }

        /// <summary>
        /// 加载默认的xml配置文件
        /// </summary>
        public XmlCommon()
        {
            //            xmlPath = HttpRuntime.AppDomainAppPath + "\\Files\\Config\\BaseConfig.xml";
            xmlPath = HttpRuntime.AppDomainAppPath + "Files\\Config\\BaseConfig.xml";
            pdoc.Load(xmlPath);
        }

        /// <summary>
        /// 加载xml字符为xml文档对象
        /// </summary>
        /// <param name="xmlString">xml格式的字符串</param>
        public XmlCommon(string xmlString)
        {
            pdoc.LoadXml(xmlString);
        }

        #region XML基本操作方法

        /// <summary>
        /// 保存XML文档
        /// </summary>
        public void SaveXml()
        {
            pdoc.Save(xmlPath);
        }

        /// <summary>
        /// 获得一组节点
        /// </summary>
        /// <param name="noteName">节点名称</param>
        /// <returns></returns>
        public XmlNodeList GetXmlNodeList(string noteName)
        {
            XmlNodeList list = pdoc.SelectNodes(noteName);
            if (list.Count > 0)
            {
                return list;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得节点
        /// </summary>
        /// <param name="noteName">节点名称</param>
        /// <returns></returns>
        public XmlNode GetXmlNode(string nodeName)
        {
            XmlNode node = pdoc.SelectSingleNode(nodeName);
            if (node != null)
            {
                return node;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获得节点列表
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public XmlNodeList GetNodeList(string nodeName)
        {
            XmlNodeList nodeList = pdoc.SelectSingleNode(nodeName).ChildNodes;
            if (nodeList.Count > 0)
            {
                return nodeList;
            }
            return null;
        }
        /// <summary>
        /// 获得节点文本
        /// </summary>
        /// <returns>apikey</returns>
        public string GetNodeText(string noteName)
        {
            string reStr = "";
            XmlNode xn1 = pdoc.SelectSingleNode(noteName);
            if (xn1 != null)
            {
                reStr = xn1.InnerText.Trim();
            }
            return reStr;
        }

        /// <summary>
        /// 获得一组节点文本
        /// </summary>
        /// <returns>字符串集合</returns>
        public List<string> GetNodesText(string noteName)
        {
            List<string> list = new List<string>();
            XmlNode xn = GetXmlNode(noteName);
            if (xn != null && xn.ChildNodes.Count > 0)
            {
                foreach (XmlNode node in xn.ChildNodes)
                {
                    list.Add(node.InnerText);
                }
            }
            return list;
        }

        /// <summary>
        /// 获得一组节点文本及其子节点
        /// </summary>
        /// <returns>字符串集合</returns>
        public Dictionary<string, string> GetNodesKeyValueText(string noteName)
        {
            Dictionary<string, string> dics = new Dictionary<string, string>();
            XmlNode xn = GetXmlNode(noteName);
            if (xn != null && xn.ChildNodes.Count > 0)
            {
                foreach (XmlNode node in xn.ChildNodes)
                {
                    if (node.ChildNodes.Count > 0 && node.ChildNodes.Count == 2)
                    {
                        dics.Add(node.ChildNodes[0].InnerText, node.ChildNodes[1].InnerText);
                    }
                    else
                    {
                        dics.Add(node.InnerText, node.InnerText);
                    }
                }
            }
            return dics;
        }

        /// <summary>
        /// 获得一组节点文本
        /// </summary>
        /// <returns>字符串集合</returns>
        public Dictionary<string, string> GetNodes(string noteName)
        {
            Dictionary<string, string> ItemsName = new Dictionary<string, string>();
            XmlNodeList xList = GetXmlNodeList(noteName);

            if (xList.Count > 0)
            {
                foreach (XmlNode node in xList)
                {
                    try
                    {
                        ItemsName.Add(node.Attributes["Value"].Value, node.InnerText);
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                    }

                }
            }
            return ItemsName;
        }

        /// <summary>
        /// 获取收件人邮件地址和姓名
        /// </summary>
        /// <param name="xpath">XPath</param>
        /// <returns>返回Dictionary</returns>
        /// Author zhongliang
        /// Create Date 2014年6月27日 
        public Dictionary<string, string> GetEmailInfo(string xpath)
        {
            Dictionary<string, string> ItemsName = new Dictionary<string, string>();
            XmlNodeList xList = GetXmlNodeList(xpath);
            if (xList.Count > 0)
            {
                XmlNode node = xList[0];
                try
                {
                    ItemsName.Add(node.Attributes["Email"].Name, node.Attributes["Email"].Value);
                    ItemsName.Add(node.Attributes["UserName"].Name, node.Attributes["UserName"].Value);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            return ItemsName;
        }

        /// <summary>
        /// 把节点的值转为数值返回
        /// </summary>
        /// <param name="nodeName">节点名称，所有层次节点</param>
        /// <returns>数值</returns>
        public int GetXmlNodeToInt(string nodeName)
        {
            int result = 0;
            XmlNode node = pdoc.SelectSingleNode(nodeName);
            if (node != null && !string.IsNullOrEmpty(node.InnerText) && int.TryParse(node.InnerText, out result))
            {
                return result;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 把节点的值转为数值返回
        /// </summary>
        /// <param name="nodeName">节点名称，所有层次节点</param>
        /// <returns>数值</returns>
        public bool GetXmlNodeToBool(string nodeName)
        {
            bool result = false;
            XmlNode node = pdoc.SelectSingleNode(nodeName);
            if (node != null && !string.IsNullOrEmpty(node.InnerText) && Boolean.TryParse(node.InnerText, out result))
            {
                return result;
            }
            return result;
        }

        /// <summary>
        /// 向指定节点添加子节点，删除原始子节点
        /// </summary>
        /// <param name="nodeList">子节点文本列表</param>
        /// <param name="nodeName">子节点名称</param>
        /// <param name="ParentNode">父子点名称：test/aa/bb</param>
        /// Author FredJiang
        /// Create Date 2013年2月25日
        public void AddXmlNodeList(List<string> nodeList, string nodeName, string ParentNode)
        {
            XmlNode node = pdoc.SelectSingleNode(ParentNode);
            if (node != null)
            {
                //移支所有子节点
                node.RemoveAll();
                //循环添加子节点
                foreach (string s in nodeList)
                {
                    XmlNode xn = pdoc.CreateElement(nodeName);
                    xn.InnerText = s;
                    node.AppendChild(xn);
                }
                //保存数据
                SaveXml();
            }
        }

        /// <summary>
        /// 向指定节点添加子节点，删除原始子节点
        /// </summary>
        /// <param name="nodeList">子节点文本列表</param>
        /// <param name="nodeName">子节点名称</param>
        /// <param name="ParentNode">父子点名称：test/aa/bb</param>
        /// Author FredJiang
        /// Create Date 2013年2月25日
        public void AddXmlNodeList(List<int> nodeList, string nodeName, string ParentNode)
        {
            XmlNode node = pdoc.SelectSingleNode(ParentNode);
            if (node != null)
            {
                //移支所有子节点
                node.RemoveAll();
                //循环添加子节点
                foreach (int n in nodeList)
                {
                    XmlNode xn = pdoc.CreateElement(nodeName);
                    xn.InnerText = n.ToString();
                    node.AppendChild(xn);
                }
                //保存数据
                SaveXml();
            }
        }

        /// <summary>
        /// 向指定节点添加子节点，删除原始子节点
        /// </summary>
        /// <param name="nodeList">子节点文本列表</param>
        /// <param name="nodeName">子节点名称</param>
        /// <param name="ParentNode">父子点名称：test/aa/bb</param>
        /// Author FredJiang
        /// Create Date 2013年2月25日
        public void AddXmlNodeList(DataTable nodeList, string nodeName, string ParentNode)
        {
            XmlNode node = pdoc.SelectSingleNode(ParentNode);
            if (node != null)
            {
                //移支所有子节点
                node.RemoveAll();
                //循环添加子节点
                foreach (DataRow row in nodeList.Rows)
                {

                    XmlNode xn = pdoc.CreateElement(nodeName);
                    foreach (DataColumn col in nodeList.Columns)
                    {
                        XmlNode xnc = pdoc.CreateElement(col.ColumnName);
                        xnc.InnerText = row[col.ColumnName].ToString();
                        xn.AppendChild(xnc);
                    }
                    node.AppendChild(xn);
                }
                //保存数据
                SaveXml();
            }
        }

        /// <summary>
        /// 保存指定节点的文本
        /// </summary>
        /// <param name="text">文本字符串</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns>成功保存返回true,否则返回false</returns>
        public bool SetXmlNodeText(string text, string nodeName)
        {
            try
            {
                XmlNode node = pdoc.SelectSingleNode(nodeName);
                if (node != null)
                {
                    node.InnerText = text;
                    SaveXml();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 把XML节点下的子节点转为DataTable
        /// </summary>
        /// <param name="noteName">节点名称(从根节点开始完整的节点名称：BaseNode/BrandNode)</param>
        /// <returns>DataTable</returns>
        public DataTable NodeToDataTable(string noteName)
        {
            DataTable dt = new DataTable();
            XmlNode xmlNode = GetXmlNode(noteName);
            if (xmlNode != null && xmlNode.ChildNodes.Count > 0)
            {
                XmlNodeList xList = xmlNode.ChildNodes;
                if (xList.Count > 0)
                {
                    //生成列名
                    for (int i = 0; i < xList[0].ChildNodes.Count; i++)
                    {
                        if (xList[0].ChildNodes[i].Name != "#comment")
                        {
                            dt.Columns.Add(xList[0].ChildNodes[i].Name, typeof(string));
                        }
                    }
                    //添加数据行
                    foreach (XmlNode node in xList)
                    {
                        DataRow row = dt.NewRow();
                        foreach (XmlNode node2 in node.ChildNodes)
                        {
                            if (dt.Columns.Contains(node2.Name))
                            {
                                row[node2.Name] = node2.InnerText;
                            }
                        }
                        dt.Rows.Add(row);
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// 把XML节点下面的子节点的属性及文本值转为DataTable,子节点名称必须为Item
        /// </summary>
        /// <param name="noteName">完整的节点名称</param>
        /// <returns>DataTable</returns>
        public DataTable NodeAttributeToDataTable(string noteName)
        {
            DataTable dt = new DataTable();
            XmlNode xmlNode = GetXmlNode(noteName);
            if (xmlNode != null && xmlNode.ChildNodes.Count > 0)
            {
                XmlNodeList xList = xmlNode.SelectNodes("Item");
                if (xList.Count > 0)
                {
                    //生成列名
                    for (int i = 0; i < xList[0].Attributes.Count; i++)
                    {
                        dt.Columns.Add(xList[0].Attributes[i].Name, typeof(string));
                    }

                    //添加数据行
                    foreach (XmlNode node in xList)
                    {
                        DataRow row = dt.NewRow();
                        for (int j = 0; j < node.Attributes.Count; j++)
                        {
                            if (dt.Columns.Contains(node.Attributes[j].Name))
                            {
                                row[node.Attributes[j].Name] = node.Attributes[j].Value;
                            }
                        }
                        dt.Rows.Add(row);
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// 返回节点下面子节点的数值集合
        /// </summary>
        /// <param name="noteName">节点名称</param>
        /// <returns>int数值集合</returns>
        /// Author FredJiang
        /// Create Date 2013年2月28日
        public List<int> GetIntListByNodeName(string noteName)
        {
            XmlNode xn = GetXmlNode(noteName);
            List<int> list = new List<int>();
            if (xn != null)
            {
                int n = 0;
                foreach (XmlNode obj in xn.ChildNodes)
                {
                    if (int.TryParse(obj.InnerText, out n))
                    {
                        list.Add(n);
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 获取节点属性VALUE
        /// </summary>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public List<string> GetNodeValue(string nodeName)
        {
            XmlNode xn = GetXmlNode(nodeName);
            List<string> list = new List<string>();
            if (xn != null)
            {
                foreach (XmlNode obj in xn.ChildNodes)
                {
                    list.Add(@obj.Attributes["Value"].Value);
                }
            }
            return list;
        }
        #endregion

        /// <summary>
        /// 获取节点下面子节点的Key和Value值
        /// 例：<Item Key="k1">xxx</Item>
        /// </summary>
        /// <param name="nodeName">节点名称</param>
        /// <returns>键值对集合</returns>
        /// Author:fredjiang
        /// Created:2016-09-11
        public Dictionary<string, string> GetKeyValue(string nodeName)
        {
            XmlNode xn = GetXmlNode(nodeName);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (xn != null)
            {
                foreach (XmlNode obj in xn.ChildNodes)
                {
                    if (!dic.ContainsKey(@obj.Attributes["Key"].Value))
                    {
                        dic.Add(@obj.Attributes["Key"].Value, obj.InnerText);
                    }
                }
            }
            return dic;
        }
    }
}
