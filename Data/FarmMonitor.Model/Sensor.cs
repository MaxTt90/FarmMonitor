/**
* sensor.cs
*
* 功 能： N/A
* 类 名： sensor
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/11/13 15:34:00   N/A    初版
*
* Copyright (c) 2015 WEDO Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海唯都企业管理咨询有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FarmMonitor.Model
{
	//sensor
    [Serializable]
    [Table("sensor")]
	public class Sensor
	{
		#region Model
		private int _sensor_id;  
		private string _ename;  
		private string _cname;  
		private string _scope;  
		private string _unit;  
		private string _resolution;  
		
		/// <summary>
        /// 传感器类型ID编码
        /// </summary>		
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("sensor_id")]
        public int SensorId
        {
            get{ return _sensor_id; }
            set{ _sensor_id = value; }
        }    
        
		/// <summary>
		/// 英文名称
        /// </summary>		
        [Column("ename")]
        public string Ename
        {
            get{ return _ename; }
            set{ _ename = value; }
        }    
        
		/// <summary>
		/// 中文名称
        /// </summary>		
        [Column("cname")]
        public string Cname
        {
            get{ return _cname; }
            set{ _cname = value; }
        }    
        
		/// <summary>
        /// 测量范围
        /// </summary>		
        [Column("scope")]
        public string Scope
        {
            get{ return _scope; }
            set{ _scope = value; }
        }    
        
		/// <summary>
        /// 测量单位
        /// </summary>	
        [Column("unit")]
        public string Unit
        {
            get{ return _unit; }
            set{ _unit = value; }
        }    
        
		/// <summary>
        /// 分辨率
        /// </summary>		
        [Column("resolution")]
        public string Resolution
        {
            get{ return _resolution; }
            set{ _resolution = value; }
        }    
        
		#endregion
	}
}