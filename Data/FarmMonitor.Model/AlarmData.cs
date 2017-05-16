/**
* alarm_data.cs
*
* 功 能： N/A
* 类 名： alarm_data
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
    //alarm_data告警数据
    [Serializable]
    [Table("alarm_data")]
	public class AlarmData
	{
		#region Model
		private string _data_id="";  
		private int? _sensor_id;  
		private DateTime? _time;  
		
		/// <summary>
        /// 报文消息ID 值域==data_header.data_id
		/// </summary>		
		[Column("data_id")]
        public string DataId
        {
            get{ return _data_id; }
            set{ _data_id = value; }
        }    
        
		/// <summary>
        /// 传感器ID 值域==sensor.sensor_id
        /// </summary>		
        [Column("sensor_id")]
        public int? SensorId
        {
            get{ return _sensor_id; }
            set{ _sensor_id = value; }
        }    
        
		/// <summary>
        /// time 告警时间
        /// </summary>		
        [Column("time")]
        public DateTime? Time
        {
            get{ return _time; }
            set{ _time = value; }
        }    
        
		#endregion
	}
}